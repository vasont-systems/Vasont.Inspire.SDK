//-------------------------------------------------------------
// <copyright file="UserExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Security
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Vasont.Inspire.Models.Security;
    using Vasont.Inspire.Models.Utilities;

    /// <summary>
    /// This class extends the <see cref="InspireClient"/> class to include methods for calling 
    /// user API endpoints.
    /// </summary>
    public static class UserExtensions
    {
        #region Public Extension Methods

        /// <summary>
        /// This method is used to retrieve a list of users from the system.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="SelectUserRoleModel"/> objects if found.</returns>
        public static List<SelectUserRoleModel> GetAllUsers(this InspireClient client)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/UserRoles/Users");
            return client.RequestContent<List<SelectUserRoleModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve information about a specific user.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="userId">Contains the user identity.</param>
        /// <returns>Returns <see cref="UserModel"/> object if found.</returns>
        public static UserModel GetUser(this InspireClient client, long userId)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Users/{userId}");
            return client.RequestContent<UserModel>(request);
        }

        /// <summary>
        /// This method is used to find users based on the search criteria.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="userName">Contains the name of the user.</param>
        /// <param name="email">Contains the email of the user.</param>
        /// <param name="phone">Contains the phone number of the user.</param>
        /// <param name="orderBy">Contains the order by field.</param>
        /// <param name="direction">Contains the direction of sort, "ascending" by default.</param>
        /// <returns>Returns a list of <see cref="UserModel"/> objects if found.</returns>
        public static List<UserModel> GetUsers(this InspireClient client, string userName = "", string email = "", string phone = "", string orderBy = "UserName", string direction = "ascending")
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Users?userName={userName}&email={email}&phone={phone}&orderBy={orderBy}&direction={direction}");
            return client.RequestContent<List<UserModel>>(request);
        }

        /// <summary>
        /// This method is used to create a new user.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="UserModel"/> input.</param>
        /// <returns>Returns the newly created <see cref="UserModel"/> object.</returns>
        public static UserModel CreateUser(this InspireClient client, UserModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Users", HttpMethod.Post);
            return client.RequestContent<UserModel, UserModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to update an existing user.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="userId">Contains the user identity.</param>
        /// <param name="inputModel">Contains the <see cref="UserModel"/> input.</param>
        /// <returns>Returns the updated <see cref="UserModel"/> object.</returns>
        public static UserModel UpdateUser(this InspireClient client, long userId, UserModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Users/{userId}", HttpMethod.Put);
            return client.RequestContent<UserModel, UserModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to delete an existing user.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="userId">Contains the user identity to delete.</param>
        /// <returns>Returns a value indicating whether the delete request was successful.</returns>
        public static bool DeleteUser(this InspireClient client, long userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Users/{userId}", HttpMethod.Delete);
            client.RequestContent(request);
            return client.HasError;
        }

        #endregion
    }
}
