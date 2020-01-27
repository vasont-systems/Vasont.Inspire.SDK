//-------------------------------------------------------------
// <copyright file="InspireClient.cs" company="Vasont Systems">
// Copyright (c) 2017 Vasont Systems. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Errors;
    using Core.Extensions;
    using IdentityModel.Client;
    using Models.Common;
    using Newtonsoft.Json;
    using Vasont.Inspire.SDK.Properties;

    /// <summary>
    /// This class is the primary client from which interaction to the Inspire Web API shall be made.
    /// </summary>
    public class InspireClient : IDisposable
    {
        #region Private Constants

        /// <summary>
        /// The delegation grant type name
        /// </summary>
        private const string DelegationGrantTypeName = "delegation";

        #endregion Private Constants

        #region Private Fields

        /// <summary>
        /// Contains the access token string.
        /// </summary>
        private readonly string internalAccessToken;

        /// <summary>
        /// Contains the calculated default token endpoint.
        /// </summary>
        private readonly string defaultTokenEndpoint;

        /// <summary>
        /// Contains a value indicating whether the class has been disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Contains a discovery result from the authority.
        /// </summary>
        private DiscoveryDocumentResponse discovery;

        /// <summary>
        /// Contains a value indicating whether the access token was verified
        /// </summary>
        private bool tokenVerified;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InspireClient"/> class.
        /// </summary>
        /// <param name="config">Contains an instance of the <see cref="InspireClientConfiguration"/> class that contains client configuration settings.</param>
        public InspireClient(InspireClientConfiguration config)
        {
            this.Config = config;
            this.defaultTokenEndpoint = new Uri(this.Config.AuthorityUri, "/connect/token").ToString();
            this.internalAccessToken = config.ClientAccessToken;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the inspire client configuration.
        /// </summary>
        public InspireClientConfiguration Config { get; }

        /// <summary>
        /// Gets a value indicating whether the client has authenticated with the server at some point
        /// </summary>
        public bool HasAuthenticated => this.TokenResponse != null && !this.TokenResponse.IsError;

        /// <summary>
        /// Gets a value indicating whether the client has an error.
        /// </summary>
        public bool HasError => this.LastErrorResponse != null || this.LastException != null;

        /// <summary>
        /// Gets the last error response model from an internal RequestContent call.
        /// </summary>
        public ErrorResponseModel LastErrorResponse { get; private set; }

        /// <summary>
        /// Gets the last exception handled within the client.
        /// </summary>
        public Exception LastException { get; private set; }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Gets or sets the client authentication token response.
        /// </summary>
        protected TokenResponse TokenResponse { get; set; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        protected string AccessToken => this.TokenResponse?.AccessToken ?? this.internalAccessToken;

        /// <summary>
        /// Gets the token endpoint for the identity authority.
        /// </summary>
        protected string TokenEndpoint => this.Config.UseDiscovery && this.discovery != null ? this.discovery.TokenEndpoint : this.defaultTokenEndpoint;

        #endregion Protected Properties

        #region Public Methods

        /// <summary>
        /// Authenticates the client with the specified scopes in a synchronous manor.
        /// </summary>
        /// <param name="scopes">The scopes to use.</param>
        /// <returns>Returns the result of the authentication.</returns>
        public bool Authenticate(string scopes = "")
        {
            return AsyncHelper.RunSync(() => this.AuthenticateAsync(scopes));
        }

        /// <summary>
        /// This method is used to retrieve and authorize the client for back-channel communication to the Identity API.
        /// </summary>
        /// <param name="scopes">Contains the scopes that are requested for the client credentials authentication.</param>
        /// <param name="cancellationToken">Contains an optional cancellation token.</param>
        /// <returns>Returns a value indicating whether the authentication was successful.</returns>
        /// <exception cref="InspireClientException">The exception is thrown if an issue occurs during discovery or client authentication.</exception>
        public async Task<bool> AuthenticateAsync(string scopes = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            bool authenticationSuccessful;
            string requestScopes = scopes + (!string.IsNullOrWhiteSpace(scopes) ? " " : string.Empty) + string.Join(" ", this.Config.TargetResourceScopes);

            if (this.Config.AuthenticationMethod == ClientAuthenticationMethods.Delegation && !string.IsNullOrWhiteSpace(this.Config.ClientAccessToken))
            {
                throw new InspireClientException(this.Config, Resources.AccessMethodRequiresTokenErrorText);
            }

            // if we're using predefined token, we must use discovery if no introspection endpoint was specified.
            if (!string.IsNullOrWhiteSpace(this.Config.ClientAccessToken) && this.Config.IntrospectionEndpoint == null && !this.Config.UseDiscovery)
            {
                throw new InspireClientException(this.Config, Resources.IntrospectionConfigNeededErrorText);
            }

            // if we're using discovery and need to call it...
            if (this.discovery == null && this.Config.UseDiscovery)
            {
                // get the discovery document.
                this.discovery = await this.RequestDiscoveryAsync(this.Config.AuthorityUri, cancellationToken);

                if (this.discovery.IsError)
                {
                    throw new InspireClientException(this.Config, this.discovery.Error);
                }
            }

            switch (this.Config.AuthenticationMethod)
            {
                case ClientAuthenticationMethods.Delegation:
                    // verify the token passed
                    this.tokenVerified = await this.VerifyAccessTokenAsync(cancellationToken).ConfigureAwait(false);

                    // the token specified was valid...
                    if (this.tokenVerified)
                    {
                        this.TokenResponse = await this.RequestDelegationAsync(requestScopes, cancellationToken).ConfigureAwait(false);
                    }

                    break;

                case ClientAuthenticationMethods.ClientCredentials:
                    this.TokenResponse = await this.RequestClientCredentialsAsync(requestScopes, cancellationToken).ConfigureAwait(false);
                    break;

                case ClientAuthenticationMethods.ResourceOwnerPassword:
                    this.TokenResponse = await this.RequestResourceOwnerPasswordAsync(this.Config.UserId, this.Config.Password, requestScopes, cancellationToken).ConfigureAwait(false);
                    break;
            }

            authenticationSuccessful = this.TokenResponse != null && !this.TokenResponse.IsError;

            // an error occurred...
            if (!authenticationSuccessful)
            {
                string errorMessage = this.TokenResponse.Error + Environment.NewLine + this.TokenResponse.ErrorDescription;
                this.LastErrorResponse = new ErrorResponseModel();
                this.LastErrorResponse.Messages.Add(new ErrorModel
                {
                    Message = errorMessage,
                    EventDate = DateTime.UtcNow
                });

                // bubble up an error response.
                throw new InspireClientException(this.Config, errorMessage);
            }

            return authenticationSuccessful;
        }

        /// <summary>
        /// This method is used to easily create a new WebRequest object for the Web API.
        /// </summary>
        /// <param name="relativeUri">Contains the relative Uri path of the web request to make against the Web API.</param>
        /// <param name="method">Contains the HttpMethod request method object.</param>
        /// <param name="noCache">Contains a value indicating whether the URL shall contain a parameter preventing the server from returning cached content.</param>
        /// <param name="credentials">Contains optional credentials </param>
        /// <param name="contentType">Contains optional content type.</param>
        /// <returns>Returns a new WebRequest object to execute.</returns>
        public HttpWebRequest CreateRequest(string relativeUri, HttpMethod method = null, bool noCache = true, ICredentials credentials = null, string contentType = "application/json")
        {
            if (string.IsNullOrWhiteSpace(relativeUri))
            {
                throw new ArgumentNullException(nameof(relativeUri));
            }

            if (method == null)
            {
                method = HttpMethod.Get;
            }

            return this.CreateRequest(relativeUri, method.Method, noCache, credentials, contentType);
        }

        /// <summary>
        /// This method is used to easily create a new WebRequest object for the Web API.
        /// </summary>
        /// <param name="relativeUri">Contains the relative Uri path of the web request to make against the Web API.</param>
        /// <param name="method">Contains the request method as a string value.</param>
        /// <param name="noCache">Contains a value indicating whether the URL shall contain a parameter preventing the server from returning cached content.</param>
        /// <param name="credentials">Contains optional credentials </param>
        /// <param name="contentType">Contains optional content type.</param>
        /// <returns>Returns a new HttpWebRequest object to execute.</returns>
        public HttpWebRequest CreateRequest(string relativeUri, string method, bool noCache = true, ICredentials credentials = null, string contentType = "application/json")
        {
            // request /Token, on success, return and store token.
            var request = WebRequest.CreateHttp(new Uri(this.Config.ResourceUri, relativeUri));
            request.Method = method;

            if (credentials == null)
            {
                credentials = CredentialCache.DefaultCredentials;
                request.UseDefaultCredentials = true;
            }

            request.Credentials = credentials;
            request.UserAgent = "Inspire SDK Client";
            request.Accept = "application/json";
            request.ContentType = contentType;

            // Set a cache policy level for the "http:" and "https" schemes.
            if (noCache)
            {
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            }

            if (this.HasAuthenticated)
            {
                request.Headers.Add("Authorization", "Bearer " + this.AccessToken);
            }

            return request;
        }

        /// <summary>
        /// This method is used to execute a web request and return the results of the request as a defined object type.
        /// </summary>
        /// <typeparam name="T">Contains the type of the object that is to be sent with the request.</typeparam>
        /// <typeparam name="TOut">Contains the type of object that is returned from the request.</typeparam>
        /// <param name="request">Contains the HttpWebRequest to execute.</param>
        /// <param name="requestBodyModel">Contains the object to serialize and submit with the request.</param>
        /// <returns>Returns the content of the request response as the specified object.</returns>
        public TOut RequestContent<T, TOut>(HttpWebRequest request, T requestBodyModel)
        {
            string content = this.RequestContent(request, requestBodyModel);
            return !string.IsNullOrWhiteSpace(content) && !this.HasError ? JsonConvert.DeserializeObject<TOut>(content) : default(TOut);
        }

        /// <summary>
        /// This method is used to execute a web request and return the results of the request as a defined object type.
        /// </summary>
        /// <typeparam name="TOut">Contains the type of object that is returned from the request.</typeparam>
        /// <param name="request">Contains the HttpWebRequest to execute.</param>
        /// <returns>Returns the content of the request response as the specified object.</returns>
        public TOut RequestContent<TOut>(HttpWebRequest request)
        {
            string content = this.RequestContent(request);
            return !string.IsNullOrWhiteSpace(content) && !this.HasError ? JsonConvert.DeserializeObject<TOut>(content) : default(TOut);
        }

        /// <summary>
        /// This method is used to execute a web request and return the results of the request as a string.
        /// </summary>
        /// <typeparam name="T">Contains the type of the object that is to be sent with the request.</typeparam>
        /// <param name="request">Contains the HttpWebRequest to execute.</param>
        /// <param name="requestBodyModel">Contains the object to serialize and submit with the request.</param>
        /// <returns>Returns the content of the request response.</returns>
        public string RequestContent<T>(HttpWebRequest request, T requestBodyModel)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (requestBodyModel == null)
            {
                throw new ArgumentNullException(nameof(requestBodyModel));
            }

            // check to ensure we're not trying to post data on a GET or other non-body request.
            if (request.Method != HttpMethod.Post.Method && request.Method != HttpMethod.Put.Method && request.Method != HttpMethod.Delete.Method)
            {
                throw new HttpRequestException(Resources.InvalidRequestTypeErrorText);
            }

            byte[] requestData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBodyModel));

            // write data out to the request stream
            using (var postStream = request.GetRequestStream())
            {
                postStream.Write(requestData, 0, requestData.Length);
            }

            return this.RequestContent(request);
        }

        /// <summary>
        /// This method is used to execute a web request and return the results of the request as a string.
        /// </summary>
        /// <param name="request">Contains the HttpWebRequest to execute.</param>
        /// <returns>Returns the content of the request response.</returns>
        public string RequestContent(HttpWebRequest request)
        {
            string resultContent = string.Empty;
            this.ResetErrors();

            try
            {
                // execute the request
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            resultContent = new StreamReader(responseStream).ReadToEnd();

                            // if the status code was an error and there's content...
                            if ((int)response.StatusCode >= 400 && !string.IsNullOrWhiteSpace(resultContent))
                            {
                                // set the error model
                                this.LastErrorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(resultContent);
                            }
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                this.LastException = webEx;

                if (webEx.Response != null)
                {
                    using (var exceptionResponse = (HttpWebResponse)webEx.Response)
                    {
                        if (exceptionResponse != null)
                        {
                            using (var responseStream = exceptionResponse.GetResponseStream())
                            {
                                if (responseStream != null)
                                {
                                    resultContent = new StreamReader(responseStream).ReadToEnd();

                                    // if the status code was an error and there's content...
                                    if ((int)exceptionResponse.StatusCode >= 400 && !string.IsNullOrWhiteSpace(resultContent))
                                    {
                                        // set the error model
                                        this.LastErrorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(resultContent);
                                    }
                                }
                            }
                        }
                    }
                }

                if (this.LastErrorResponse == null)
                {
                    var lastErrorResponse = new ErrorResponseModel();
                    lastErrorResponse.Messages.Add(new ErrorModel
                    {
                        Message = webEx.Message,
                        StackTrace = webEx.StackTrace,
                        EventDate = DateTime.UtcNow,
                        ErrorType = ErrorType.Critical,
                        PropertyName = "HttpWebResponse"
                    });

                    this.LastErrorResponse = lastErrorResponse;
                }
            }

            return resultContent;
        }

        #region IDispose Methods

        /// <summary>
        /// This method is called upon disposal of the client class.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDispose Methods

        #region Protected Methods

        /// <summary>
        /// This method is called upon disposal of the client class.
        /// </summary>
        /// <param name="disposing">Contains a value indicating whether the class is currently being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                // if we're still logged in, be sure to log off.
                if (this.HasAuthenticated)
                {
                    this.ResetSession();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// This method is used to clear any previous error objects.
        /// </summary>
        protected void ResetErrors()
        {
            this.LastErrorResponse = null;
            this.LastException = null;
        }

        #endregion Protected Methods

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// This method is used to contact an authority discovery endpoint for information for interacting with the authority.
        /// </summary>
        /// <param name="authorityUri">Contains the authority URI to contact.</param>
        /// <param name="cancellationToken">Contains a cancellation token.</param>
        /// <returns>Returns a <see cref="DiscoveryDocumentResponse"/> object when successful.</returns>
        protected async Task<DiscoveryDocumentResponse> RequestDiscoveryAsync(Uri authorityUri, CancellationToken cancellationToken)
        {
            DiscoveryDocumentResponse result;

            using (HttpClient client = new HttpClient())
            {
                result = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
                {
                    Address = authorityUri.ToString()
                },
                cancellationToken)
                .ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Requests the client credentials.
        /// </summary>
        /// <param name="scopes">The scopes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a <see cref="TokenResponse"/> object.</returns>
        protected async Task<TokenResponse> RequestClientCredentialsAsync(string scopes, CancellationToken cancellationToken)
        {
            TokenResponse result;

            using (HttpClient client = new HttpClient())
            {
                result = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = this.TokenEndpoint,
                    ClientId = this.Config.ClientId,
                    Scope = scopes,
                    ClientSecret = this.Config.ClientSecret
                }, cancellationToken)
                    .ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Verifies the token that was passed into the client against the token introspection endpoint.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>Returns a value indicating whether the token is active and valid.</returns>
        protected async Task<bool> VerifyAccessTokenAsync(CancellationToken cancellationToken)
        {
            TokenIntrospectionResponse response;

            using (HttpClient client = new HttpClient())
            {
                response = await client.IntrospectTokenAsync(new TokenIntrospectionRequest
                {
                    Address = this.discovery.IntrospectionEndpoint,
                    Token = this.AccessToken,
                    ClientId = this.Config.ClientId,
                    ClientSecret = this.Config.ClientSecret
                }, cancellationToken);
            }

            return response != null && response.IsActive;
        }

        /// <summary>
        /// This method is used to request a delegation token from the identity server that supports the custom grant type of "delegation".
        /// </summary>
        /// <param name="scopes">The scopes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a new token response from the request.</returns>
        protected async Task<TokenResponse> RequestDelegationAsync(string scopes, CancellationToken cancellationToken)
        {
            TokenResponse result;

            using (HttpClient client = new HttpClient())
            {
                result = await client.RequestTokenAsync(new TokenRequest
                {
                    Address = this.TokenEndpoint,
                    GrantType = DelegationGrantTypeName,
                    ClientId = this.Config.ClientId, // this would be the ID of the API1 client
                    ClientSecret = this.Config.ClientSecret,
                    Parameters =
                    {
                        { "scope", scopes },
                        { "token", this.Config.ClientAccessToken }
                    }
                }, cancellationToken)
                    .ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Request the resource owner credentials.
        /// </summary>
        /// <param name="userName">Contains the user name.</param>
        /// <param name="password">Contains the user password.</param>
        /// <param name="scopes">Contains the scopes.</param>
        /// <param name="cancellationToken">Contains a cancellation token.</param>
        /// <returns>Returns a <see cref="TokenResponse"/> object.</returns>
        protected async Task<TokenResponse> RequestResourceOwnerPasswordAsync(string userName, string password, string scopes, CancellationToken cancellationToken)
        {
            TokenResponse result;

            using (HttpClient client = new HttpClient())
            {
                var config = new PasswordTokenRequest
                {
                    Address = this.TokenEndpoint,
                    ClientId = this.Config.ClientId,
                    Scope = scopes,
                    ClientSecret = this.Config.ClientSecret,
                    UserName = userName,
                    Password = password
                };

                result = await client.RequestPasswordTokenAsync(config, cancellationToken)
                    .ConfigureAwait(false);
            }

            return result;
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// This method is used to reset session-related fields.
        /// </summary>
        private void ResetSession()
        {
            this.ResetErrors();
            this.TokenResponse = null;
        }

        #endregion Private Methods
    }
}