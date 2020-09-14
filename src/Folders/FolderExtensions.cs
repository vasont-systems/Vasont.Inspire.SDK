//-------------------------------------------------------------
// <copyright file="FolderExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Folders
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Vasont.Inspire.Models.Components;
    using Vasont.Inspire.Models.Security;
    using Vasont.Inspire.Models.Transfers;

    /// <summary>
    /// This class extends the <see cref="InspireClient"/> class to include methods for calling 
    /// folder API endpoints.
    /// </summary>
    public static class FolderExtensions
    {
        /// <summary>
        /// This method is used to return a list of folders that are children of the specified parent folder. 
        /// If parent folder is not specified, a list of root folders will be returned.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity to retrieve child folders from, pass a value of zero to retrieve all folders.</param>
        /// <param name="includeAllDescendantFolders">Contains a value to indicate whether or not to include all descendant folders.</param>
        /// <param name="permissionFlag">Contains an optional <see cref="PermissionFlags"/> that will be used to filter the results of the query.</param>
        /// <returns>Returns a list of <see cref="FolderBrowseModel"/> objects if found.</returns>
        [Obsolete("This method is obsolete. Please use FindFoldersByFolderId() going forward. This method will be removed in a future release.")]
        public static List<FolderBrowseModel> GetFoldersByFolderId(this InspireClient client, long folderId, bool includeAllDescendantFolders, PermissionFlags permissionFlag)
        {
            return FindFoldersByFolderId(client, folderId, includeAllDescendantFolders, permissionFlag);
        }

        /// <summary>
        /// This method is used to return a list of folders that are children of the specified parent folder. 
        /// If parent folder is not specified, a list of root folders will be returned.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity to retrieve child folders from, pass a value of zero to retrieve all folders.</param>
        /// <param name="includeAllDescendantFolders">Contains a value to indicate whether or not to include all descendant folders.</param>
        /// <param name="permissionFlag">Contains an optional <see cref="PermissionFlags"/> that will be used to filter the results of the query.</param>
        /// <returns>Returns a list of <see cref="FolderBrowseModel"/> objects if found.</returns>
        public static List<FolderBrowseModel> FindFoldersByFolderId(this InspireClient client, long folderId, bool includeAllDescendantFolders, PermissionFlags permissionFlag)
        {
            if (folderId < 0)
            {
                throw new ArgumentNullException(nameof(folderId));
            }

            var request = client.CreateRequest($"/Folders/{folderId}?allDescendants={includeAllDescendantFolders}&permissionFlag={permissionFlag}");
            return client.RequestContent<List<FolderBrowseModel>>(request);
        }

        /// <summary>
        /// This method is used to return components that are stored within a specific folder matching a specific search criteria.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity to retrieve components from, pass a value of zero to retrieve all components.</param>
        /// <param name="inputModel">Contains the <see cref="FolderComponentsQueryModel"/> input.</param>
        /// <returns>Returns a <see cref="FolderComponentsResultModel"/> object if found.</returns>
        [Obsolete("This method is obsolete. Please use FindComponentsByFolderId() going forward. This method will be removed in a future release.")]
        public static FolderComponentsResultModel GetComponentsByFolderId(this InspireClient client, long folderId, FolderComponentsQueryModel inputModel)
        {
            return FindComponentsByFolderId(client, folderId, inputModel);
        }

        /// <summary>
        /// This method is used to return components that are stored within a specific folder matching a specific search criteria.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity to retrieve components from, pass a value of zero to retrieve all components.</param>
        /// <param name="inputModel">Contains the <see cref="FolderComponentsQueryModel"/> input.</param>
        /// <returns>Returns a <see cref="FolderComponentsResultModel"/> object if found.</returns>
        public static FolderComponentsResultModel FindComponentsByFolderId(this InspireClient client, long folderId, FolderComponentsQueryModel inputModel)
        {
            if (folderId < 0)
            {
                throw new ArgumentNullException(nameof(folderId));
            }

            var request = client.CreateRequest($"/Folders/{folderId}/Components", HttpMethod.Post);
            return client.RequestContent<FolderComponentsQueryModel, FolderComponentsResultModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to return a list of folders including the specified folder, 
        /// its ancestor folders, and corresponding sibling folders.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity.</param>
        /// <returns>Returns a list of <see cref="FolderBrowseModel"/> objects if found.</returns>
        [Obsolete("This method is obsolete. Please use FindAncestorSiblingFoldersByFolderId() going forward. This method will be removed in a future release.")]
        public static List<FolderBrowseModel> GetAncestorSiblingFoldersByFolderId(this InspireClient client, long folderId)
        {
            return FindAncestorSiblingFoldersByFolderId(client, folderId);
        }

        /// <summary>
        /// This method is used to return a list of folders including the specified folder, 
        /// its ancestor folders, and corresponding sibling folders.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity.</param>
        /// <returns>Returns a list of <see cref="FolderBrowseModel"/> objects if found.</returns>
        public static List<FolderBrowseModel> FindAncestorSiblingFoldersByFolderId(this InspireClient client, long folderId)
        {
            if (folderId <= 0)
            {
                throw new ArgumentNullException(nameof(folderId));
            }

            var request = client.CreateRequest($"/Folders/{folderId}/AncestorsSiblings");
            return client.RequestContent<List<FolderBrowseModel>>(request);
        }

        /// <summary>
        /// This method is used to return folder permission details.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity.</param>
        /// <returns>Returns a <see cref="PermissionUpdateModel"/> object if found.</returns>
        [Obsolete("This method is obsolete. Please use FindFolderPermissions() going forward. This method will be removed in a future release.")]
        public static PermissionUpdateModel GetFolderPermissions(this InspireClient client, long folderId)
        {
            return FindFolderPermissions(client, folderId);
        }

        /// <summary>
        /// This method is used to return folder permission details.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity.</param>
        /// <returns>Returns a <see cref="PermissionUpdateModel"/> object if found.</returns>
        public static PermissionUpdateModel FindFolderPermissions(this InspireClient client, long folderId)
        {
            if (folderId <= 0)
            {
                throw new ArgumentNullException(nameof(folderId));
            }

            var request = client.CreateRequest($"/Folders/{folderId}/Permissions");
            return client.RequestContent<PermissionUpdateModel>(request);
        }

        /// <summary>
        /// This method is used to move a source folder to a target folder.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="sourceFolderId">Contains the source folder identity.</param>
        /// <param name="targetFolderId">Contains the target folder identity to which to move this folder to.</param>
        /// <returns>Returns a <see cref="FolderBrowseModel"/> object if found.</returns>
        public static FolderBrowseModel MoveFolder(this InspireClient client, long sourceFolderId, long targetFolderId)
        {
            if (sourceFolderId <= 0 && targetFolderId <= 0)
            {
                throw new ArgumentNullException(nameof(sourceFolderId));
            }

            var request = client.CreateRequest($"/Folders/{sourceFolderId}/Move/{targetFolderId}");
            return client.RequestContent<FolderBrowseModel>(request);
        }

        /// <summary>
        /// This method is used to create a new folder and return details about the newly created folder.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="targetFolderId">Contains the folder identity.</param>
        /// <param name="inputModel">Contains the <see cref="FolderBrowseModel"/> input.</param>
        /// <returns>Returns a <see cref="FolderBrowseModel"/> object if found.</returns>
        public static FolderBrowseModel CreateFolder(this InspireClient client, long targetFolderId, FolderBrowseModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/Folders/{targetFolderId}", HttpMethod.Post);
            return client.RequestContent<FolderBrowseModel, FolderBrowseModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to initiate an export request and return details about the successfully submitted export request.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="targetFolderId">Contains the folder identity.</param>
        /// <param name="exportId">Contains the export identity value.</param>
        /// <param name="includeSubFolders">Contains a flag indicating whether to include subfolders or not.</param>
        /// <returns>Returns a <see cref="ExportRequestModel"/> object if found.</returns>
        public static ExportRequestModel ExportFolder(this InspireClient client, long targetFolderId, long exportId = 0, bool includeSubFolders = false)
        {
            if (targetFolderId <= 0)
            {
                throw new ArgumentNullException(nameof(targetFolderId));
            }

            var request = client.CreateRequest($"/Folders/{targetFolderId}/Export/{exportId}/?includeSubFolders={includeSubFolders}", HttpMethod.Post);
            return client.RequestContent<ExportRequestModel, ExportRequestModel>(request, new ExportRequestModel());
        }

        /// <summary>
        /// This method is used to delete an existing folder.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool DeleteFolder(this InspireClient client, long folderId)
        {
            if (folderId <= 0)
            {
                throw new ArgumentNullException(nameof(folderId));
            }

            var request = client.CreateRequest($"/Folders/{folderId}", HttpMethod.Delete);
            client.RequestContent(request);
            return client.HasError;
        }

        /// <summary>
        /// This method is used to update existing folder permissions.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity.</param>
        /// <param name="inputModel">Contains the <see cref="PermissionUpdateModel"/> input.</param>
        /// <returns>Returns the updated <see cref="PermissionUpdateModel"/> object.</returns>
        public static PermissionUpdateModel UpdateFolderPermissions(this InspireClient client, long folderId, PermissionUpdateModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/Folders/{folderId}/Permissions", HttpMethod.Put);
            return client.RequestContent<PermissionUpdateModel, PermissionUpdateModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to update an existing folder.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity.</param>
        /// <param name="inputModel">Contains the <see cref="FolderBrowseModel"/> input.</param>
        /// <returns>Returns the updated <see cref="FolderBrowseModel"/> object.</returns>
        public static FolderBrowseModel UpdateFolder(this InspireClient client, long folderId, FolderBrowseModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/Folders/{folderId}", HttpMethod.Put);
            return client.RequestContent<FolderBrowseModel, FolderBrowseModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to rename an existing folder.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="folderId">Contains the folder identity.</param>
        /// <param name="inputModel">Contains the <see cref="FolderBrowseModel"/> input.</param>
        /// <returns>Returns the renamed <see cref="FolderBrowseModel"/> object.</returns>
        public static FolderBrowseModel RenameFolder(this InspireClient client, long folderId, FolderBrowseModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/Folders/{folderId}/Rename", HttpMethod.Put);
            return client.RequestContent<FolderBrowseModel, FolderBrowseModel>(request, inputModel);
        }
    }
}