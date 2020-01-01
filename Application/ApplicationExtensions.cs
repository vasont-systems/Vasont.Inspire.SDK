//-------------------------------------------------------------
// <copyright file="ApplicationExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Application
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Vasont.Inspire.Models.Common;
    using Vasont.Inspire.Models.Components;

    /// <summary>
    /// This class extends the <see cref="InspireClient"/> class to include methods for calling 
    /// application API endpoints.
    /// </summary>
    public static class ApplicationExtensions
    {
        #region Public Extension Methods

        /// <summary>
        /// This method is used to return the tenant information for the application instance.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a <see cref="TenantDetailModel"/> object if found.</returns>
        public static TenantDetailModel GetAppInfo(this InspireClient client)
        {
            var request = client.CreateRequest("/api/AppInfo");
            return client.RequestContent<TenantDetailModel>(request);
        }

        /// <summary>
        /// This method is used to simply return OK to test and keep the server alive.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a string OK if the server responded.</returns>
        public static string KeepAlive(this InspireClient client)
        {
            var request = client.CreateRequest("/api/KeepAlive");
            return client.RequestContent<string>(request);
        }
        
        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns>Returns the requested <see cref="TagModel"/> object.</returns>
        public static TagModel GetTag(this InspireClient client, long tagId)
        {
            var request = client.CreateRequest(string.Format("/api/Configurations/Tags/{0}", tagId));
            return client.RequestContent<TagModel>(request);
        }

        /// <summary>
        /// Puts the tag.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="tagId">The tag identifier.</param>
        /// <param name="inputModel">The input model.</param>
        /// <returns>Returns the updated <see cref="TagModel"/> object.</returns>
        public static TagModel PutTag(this InspireClient client, long tagId, TagModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest(string.Format("/api/Configurations/Tags/{0}", tagId), HttpMethod.Put);

            return client.RequestContent<TagModel, TagModel>(request, inputModel);
        }

        /// <summary>
        /// Posts the tag.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="inputModel">The input model.</param>
        /// <returns>Returns the new <see cref="TagModel"/> object.</returns>
        public static TagModel PostTag(this InspireClient client, TagModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Configurations/Tags/", HttpMethod.Post);

            return client.RequestContent<TagModel, TagModel>(request, inputModel);
        }

        /// <summary>
        /// Deletes the tag.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns>Returns a true/false indicating if the Delete was successful.</returns>
        public static bool DeleteTag(this InspireClient client, long tagId)
        {
            if (tagId <= 0)
            {
                throw new ArgumentNullException(nameof(tagId));
            }

            var request = client.CreateRequest(string.Format("/api/Configurations/Tags/{0}", tagId), HttpMethod.Delete);
            client.RequestContent(request);
            
            return !client.HasError;
        }

        /// <summary>
        /// This method is used to get all tags in the system.
        /// </summary>
        /// <param name="client">The Inspire client.</param>
        /// <returns>Returns a list of all <see cref="TagModel"/> objects in the Inspire system.</returns>
        public static List<TagModel> GetAllTags(this InspireClient client)
        {
            var request = client.CreateRequest("/api/Tags");
            return client.RequestContent<List<TagModel>>(request);
        }
        #endregion
    }
}
