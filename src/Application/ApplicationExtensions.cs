//-------------------------------------------------------------
// <copyright file="ApplicationExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Application
{
    using Vasont.Inspire.Models.Common;

    /// <summary>
    /// This class extends the <see cref="InspireClient"/> class to include methods for calling 
    /// application API endpoints.
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// This method is used to return the tenant information for the application instance.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a <see cref="TenantDetailModel"/> object if found.</returns>
        public static TenantDetailModel GetAppInfo(this InspireClient client)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Application/AppInfo");
            return client.RequestContent<TenantDetailModel>(request);
        }

        /// <summary>
        /// This method is used to simply return OK to test and keep the server alive.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a string OK if the server responded.</returns>
        public static string KeepAlive(this InspireClient client)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Misc/KeepAlive");
            return client.RequestContent<string>(request);
        }
    }
}
