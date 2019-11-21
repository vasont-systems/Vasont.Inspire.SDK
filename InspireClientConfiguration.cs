//-------------------------------------------------------------
// <copyright file="InspireClientConfiguration.cs" company="Vasont Systems">
// Copyright (c) Vasont Systems. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK
{
    using System;
    
    /// <summary>
    /// Contains an enumerated list of SDK client authentication methods.
    /// </summary>
    public enum ClientAuthenticationMethods
    {
        /// <summary>
        /// The client shall authenticate with a client secret.
        /// </summary>
        ClientCredentials,

        /// <summary>
        /// The client shall authenticate with the resource owner using a user id and password.
        /// </summary>
        /// <remarks>
        /// The spec recommends using the resource owner password grant only for "trusted" (or legacy) applications.
        /// </remarks>
        ResourceOwnerPassword
    }

    /// <summary>
    /// This class contains inspire client SDK configuration options.
    /// </summary>
    public class InspireClientConfiguration
    {
        #region Constants
        /// <summary>
        /// Contains the default inspire SDK client id.
        /// </summary>
        public const string DefaultClientId = "inspiresdk";

#pragma warning disable S1075 // URIs should not be hardcoded
        /// <summary>
        /// Contains the default authority URL.
        /// </summary>
        public const string DefaultAuthorityUrl = "https://identity.vasont.com/"; // NOSONAR
#pragma warning restore S1075 // URIs should not be hardcoded

        /// <summary>
        /// Contains the default inspire resource scope name.
        /// </summary>
        public const string DefaultTargetResourceScope = "inspireapi";
        #endregion

        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InspireClientConfiguration"/> class.
        /// </summary>
        public InspireClientConfiguration()
            : this(DefaultClientId, ClientAuthenticationMethods.ClientCredentials, DefaultAuthorityUrl, "/")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InspireClientConfiguration"/> class.
        /// </summary>
        /// <param name="clientId">Contains the client identity to use for authentication.</param>
        /// <param name="clientAuthenticationMethod">Contains an optional client authentication method.</param>
        /// <param name="authorityUri">Contains the authority URI that will validate the client credentials and produce access token.</param>
        /// <param name="resourceUri">Contains the API resource URI that will be accessed by the client.</param>
        /// <param name="clientSecret">Contains an optional client secret key for client credential authentication method.</param>
        /// <param name="userId">Contains the user id used for password authentication method.</param>
        /// <param name="password">Contains the user password used for password authentication method.</param>
        /// <param name="useDiscovery">Contains a value indicating whether the authority discovery endpoint will be used for token endpoint lookup.</param>
        public InspireClientConfiguration(string clientId, ClientAuthenticationMethods clientAuthenticationMethod,
            string authorityUri, string resourceUri, string clientSecret = "", string userId = "", string password = "", bool useDiscovery = true)
            : this(clientId, clientAuthenticationMethod, new Uri(authorityUri), new Uri(resourceUri), clientSecret, userId, password, useDiscovery)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InspireClientConfiguration"/> class.
        /// </summary>
        /// <param name="clientId">Contains the client identity to use for authentication.</param>
        /// <param name="clientAuthenticationMethod">Contains an optional client authentication method.</param>
        /// <param name="authorityUri">Contains the authority URI that will validate the client credentials and produce access token.</param>
        /// <param name="resourceUri">Contains the API resource URI that will be accessed by the client.</param>
        /// <param name="clientSecret">Contains an optional client secret key for client credential authentication method.</param>
        /// <param name="userId">Contains the user id used for password authentication method.</param>
        /// <param name="password">Contains the user password used for password authentication method.</param>
        /// <param name="useDiscovery">Contains a value indicating whether the authority discovery endpoint will be used for token endpoint lookup.</param>
        public InspireClientConfiguration(string clientId, ClientAuthenticationMethods clientAuthenticationMethod,
            Uri authorityUri, Uri resourceUri, string clientSecret = "", string userId = "", string password = "", bool useDiscovery = true)
        {
            this.ClientId = clientId;
            this.AuthenticationMethod = clientAuthenticationMethod;
            this.AuthorityUri = authorityUri;
            this.ResourceUri = resourceUri;
            this.ClientSecret = clientSecret;
            this.UserId = userId;
            this.Password = password;
            this.UseDiscovery = useDiscovery;
            this.TargetResourceScopes = new[] { DefaultTargetResourceScope };
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the SDK client identity.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client authentication method used.
        /// </summary>
        public ClientAuthenticationMethods AuthenticationMethod { get; set; }

        /// <summary>
        /// Gets or sets the SSO Authority URI.
        /// </summary>
        public Uri AuthorityUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authority discovery endpoint will be used for token endpoint lookup.
        /// </summary>
        public bool UseDiscovery { get; set; }

        /// <summary>
        /// Gets or sets the API resource URI.
        /// </summary>
        public Uri ResourceUri { get; set; }

        /// <summary>
        /// Gets a list of target resource scopes.
        /// </summary>
        public string[] TargetResourceScopes { get; }

        /// <summary>
        /// Gets or sets the client secret value used for client credential authentication.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the user name used for password client authentication.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the password used for password client authentication.
        /// </summary>
        public string Password { get; set; }
        #endregion
    }
}
