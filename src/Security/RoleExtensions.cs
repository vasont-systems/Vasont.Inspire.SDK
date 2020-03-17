//-------------------------------------------------------------
// <copyright file="RoleExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Security
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Vasont.Inspire.Models.Security;

    /// <summary>
    /// This class extends the <see cref="InspireClient"/> class to include methods for calling 
    /// role API endpoints.
    /// </summary>
    public static class RoleExtensions
    {
        #region Public Extension Methods

        /// <summary>
        /// This method is used to retrieve a specific role model from the system.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="roleId">The role identity.</param>
        /// <returns>Returns <see cref="RoleModel"/> object if found.</returns>
        public static RoleModel GetAllRoles(this InspireClient client, long roleId)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Roles/{roleId}");
            return client.RequestContent<RoleModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve roles that match the search criteria.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="orderBy">The field to order by the list of roles.</param>
        /// <param name="direction">The direction of the order by, ascending or descending.</param>
        /// <returns>Returns <see cref="RoleModel"/> object if found.</returns>
        public static List<RoleModel> GetRoles(this InspireClient client, string roleName = "", string orderBy = "", string direction = "")
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Roles?roleName={roleName}&orderBy={orderBy}&direction={direction}");
            return client.RequestContent<List<RoleModel>>(request);
        }

        /// <summary>
        /// This method is used to create a new role, and return the new RoleModel.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="RoleModel"/> input.</param>
        /// <returns>Returns the newly created <see cref="RoleModel"/> object.</returns>
        public static RoleModel CreateRole(this InspireClient client, RoleModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Roles", HttpMethod.Post);
            return client.RequestContent<RoleModel, RoleModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to update an existing role, and return the updated RoleModel.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="roleId">Contains the role identity.</param>
        /// <param name="inputModel">Contains the <see cref="RoleModel"/> input.</param>
        /// <returns>Returns the updated <see cref="RoleModel"/> object.</returns>
        public static RoleModel UpdateRole(this InspireClient client, long roleId, RoleModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Roles/{roleId}", HttpMethod.Put);
            return client.RequestContent<RoleModel, RoleModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to delete an existing role.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="roleId">Contains the role identity to delete.</param>
        /// <returns>Returns a value indicating whether the delete request was successful.</returns>
        public static bool DeleteRole(this InspireClient client, long roleId)
        {
            if (roleId <= 0)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Roles/{roleId}", HttpMethod.Delete);
            client.RequestContent(request);
            return client.HasError;
        }

        #endregion
    }
}
