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
        /// This method is used to retrieve a list of <see cref="RoleModel"/> from the specified query.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="roleName">Contains an optional value used to filter results by role name.</param>
        /// <param name="orderBy">Contains an optional value used to specify which model property results will be ordered by.</param>
        /// <param name="direction">Contains an optional value used to specify which direction results will be ordered.</param>
        /// <returns>Returns a list of <see cref="RoleModel"/> containing the results of the specified query.</returns>
        [Obsolete("This method is obsolete. Please use FindRoles() going forward. This method will be removed in a future release.")]
        public static List<RoleModel> GetRoles(this InspireClient client, string roleName = "", string orderBy = "Name", string direction = "asc")
        {
            return FindRoles(client, roleName, orderBy, direction);
        }

        /// <summary>
        /// This method is used to retrieve a list of <see cref="RoleModel"/> from the specified query.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="roleName">Contains an optional value used to filter results by role name.</param>
        /// <param name="orderBy">Contains an optional value used to specify which model property results will be ordered by.</param>
        /// <param name="direction">Contains an optional value used to specify which direction results will be ordered.</param>
        /// <returns>Returns a list of <see cref="RoleModel"/> containing the results of the specified query.</returns>
        public static List<RoleModel> FindRoles(this InspireClient client, string roleName = "", string orderBy = "Name", string direction = "asc")
        {
            var request = client.CreateRequest($"/Roles/{roleName}/{orderBy}/{direction}");
            return client.RequestContent<List<RoleModel>>(request);
        }

        /// <summary>
        /// Gets the specified <see cref="RoleModel"/>.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>Returns the <see cref="RoleModel"/> record of the specified role</returns>
        [Obsolete("This method is obsolete. Please use FindRole() going forward. This method will be removed in a future release.")]
        public static RoleModel GetRole(this InspireClient client, long roleId)
        {
            return FindRole(client, roleId);
        }

        /// <summary>
        /// Gets the specified <see cref="RoleModel"/>.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>Returns the <see cref="RoleModel"/> record of the specified role</returns>
        public static RoleModel FindRole(this InspireClient client, long roleId)
        {
            var request = client.CreateRequest($"/Roles/{roleId}");
            return client.RequestContent<RoleModel>(request);
        }

        /// <summary>
        /// This method is used to create a new role.
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

            var request = client.CreateRequest($"/Roles", HttpMethod.Post);
            return client.RequestContent<RoleModel, RoleModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to update an existing role.
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

            var request = client.CreateRequest($"/Roles/{roleId}", HttpMethod.Put);
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

            var request = client.CreateRequest($"/Roles/{roleId}", HttpMethod.Delete);
            client.RequestContent(request);
            return client.HasError;
        }

        #endregion
    }
}
