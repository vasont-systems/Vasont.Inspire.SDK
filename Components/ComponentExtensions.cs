//-------------------------------------------------------------
// <copyright file="ComponentExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Vasont.Inspire.Models.Components;
    using Vasont.Inspire.Models.Components.Schema;
    using Vasont.Inspire.Models.Transfers;
    using Vasont.Inspire.Models.Worker;
    using Vasont.Inspire.SDK.Extensions;

    /// <summary>
    /// This class extends the <see cref="InspireClient"/> class to include methods for calling 
    /// component API endpoints.
    /// </summary>
    public static class ComponentExtensions
    {
        #region Public Extension Methods

        /// <summary>
        /// This method is used to retrieve and return minimal models for component types.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <returns>Returns a list of <see cref="MinimalComponentTypeModel"/> minimal component type models.</returns>
        public static List<MinimalComponentTypeModel> GetComponentTypes(this InspireClient client)
        {
            var request = client.CreateRequest("/api/ComponentTypes");
            return client.RequestContent<List<MinimalComponentTypeModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve detail about a specific component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">The component identity.</param>
        /// <returns>Returns a list of <see cref="MinimalComponentModel"/> objects.</returns>
        public static MinimalComponentModel GetComponent(this InspireClient client, long componentId)
        {
            var request = client.CreateRequest("/api/Components/" + componentId);
            return client.RequestContent<MinimalComponentModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve a batch of requested components.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentIds">The list of component identifiers.</param>
        /// <returns>Returns a <see cref="BatchComponentsResultModel"/> object containing a list of requested components.</returns>
        public static BatchComponentsResultModel GetComponents(this InspireClient client, BatchComponentsRequestModel componentIds)
        {
            var request = client.CreateRequest("/api/Components/Batch/", HttpMethod.Post);
            return client.RequestContent<BatchComponentsRequestModel, BatchComponentsResultModel>(request, componentIds);
        }

        /// <summary>
        /// This method is used to retrieve the content from a specific component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">The component identity.</param>
        /// <returns>Returns an array of bytes that represent the content of the component.</returns>
        public static byte[] GetComponentContent(this InspireClient client, long componentId)
        {
            var request = client.CreateRequest("/api/Components/" + componentId + "/Content");
            return client.RequestContent<byte[]>(request);
        }

        /// <summary>
        /// This method is used to retrieve the permission details for a specific component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">The component identity.</param>
        /// <returns>Returns the <see cref="PermissionUpdateModel"/> object.</returns>
        public static PermissionUpdateModel GetComponentPermissions(this InspireClient client, long componentId)
        {
            var request = client.CreateRequest("/api/Components/" + componentId + "/Permissions");
            return client.RequestContent<PermissionUpdateModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve the component templates based on the supplied document types.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="documentTypes">Contains a comma delimited list of document types that will be used to filter the results.</param>
        /// <returns>Returns the <see cref="MinimalComponentModel"/> object.</returns>
        public static MinimalComponentModel GetComponentTemplates(this InspireClient client, string documentTypes)
        {
            var request = client.CreateRequest("/api/Components/Templates/" + documentTypes);
            return client.RequestContent<MinimalComponentModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve a component configuration for a specified component
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">Contains the component identity.</param>
        /// <param name="forceReload">Contains a value indicating whether the configuration cache is bypassed and reloaded.</param>
        /// <returns>Returns the <see cref="ComponentConfigurationModel"/> object.</returns>
        public static ComponentConfigurationModel GetComponentConfiguration(this InspireClient client, long componentId, bool forceReload = false)
        {
            var request = client.CreateRequest("/api/Components/" + componentId + "/Configuration?force=" + forceReload);
            return client.RequestContent<ComponentConfigurationModel>(request);
        }

        /// <summary>
        /// This method is used to update permissions for a single component.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componentId">Contains the component identity.</param>
        /// <param name="inputModel">Contains the <see cref="PermissionUpdateModel"/> input.</param>
        /// <returns>Returns the updated <see cref="PermissionUpdateModel"/> object.</returns>
        public static PermissionUpdateModel UpdateComponentPermissions(this InspireClient client, long componentId, PermissionUpdateModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Components/" + componentId + "/Permissions", HttpMethod.Put);
            return client.RequestContent<PermissionUpdateModel, PermissionUpdateModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to create a new component from a template, and return details about the newly created component.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="CreateComponentFromTemplateModel"/> input.</param>
        /// <returns>Returns a <see cref="MinimalComponentModel"/> object if found.</returns>
        public static MinimalComponentModel CreateComponent(this InspireClient client, CreateComponentFromTemplateModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Components/", HttpMethod.Post);
            return client.RequestContent<CreateComponentFromTemplateModel, MinimalComponentModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to request an export process for one or more components.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="exportId">Contains the export identity.</param>
        /// <param name="inputModel">Contains the <see cref="ExportRequestModel"/> input.</param>
        /// <returns>Returns a <see cref="ExportRequestModel"/> object if found.</returns>
        public static WorkerStateModel<MinimalExportStateModel> CreateComponentsExportRequest(this InspireClient client, long exportId, ExportRequestModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Components/Export/" + exportId, HttpMethod.Post);
            return client.RequestContent<ExportRequestModel, WorkerStateModel<MinimalExportStateModel>>(request, inputModel);
        }

        /// <summary>
        /// This method is used to rename an existing component.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componentId">Contains the component identity.</param>
        /// <param name="inputModel">Contains the <see cref="MinimalComponentModel"/> input.</param>
        /// <returns>Returns a <see cref="MinimalComponentModel"/> object if found.</returns>
        public static MinimalComponentModel RenameComponent(this InspireClient client, long componentId, MinimalComponentModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Components/" + componentId + "/Rename", HttpMethod.Put);
            return client.RequestContent<MinimalComponentModel, MinimalComponentModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to set permissions on an existing component.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="PermissionUpdateModel"/> input.</param>
        /// <returns>Returns a <see cref="PermissionUpdateModel"/> object if found.</returns>
        public static PermissionUpdateModel SetComponentPermissions(this InspireClient client, PermissionUpdateModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Components/Permissions", HttpMethod.Put);
            return client.RequestContent<PermissionUpdateModel, PermissionUpdateModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to move existing components to a new folder.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componentIds">Contains a list of component identities.</param>
        /// <param name="targetFolderId">Contains the target folder identity.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool MoveComponentsToFolder(this InspireClient client, List<long> componentIds, long targetFolderId)
        {
            if (componentIds == null || !componentIds.Any())
            {
                throw new ArgumentNullException(nameof(componentIds));
            }

            var request = client.CreateRequest("/api/Components/Move/" + targetFolderId + "?componentIds=" + string.Join(",", componentIds), HttpMethod.Put);

            client.RequestContent(request);
            return client.HasError;
        }

        /// <summary>
        /// This method is used to lock a component.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componenttId">Contains the component identity.</param>
        /// <returns>Returns a <see cref="ComponentLockModel"/> object.</returns>
        public static ComponentLockModel LockComponent(this InspireClient client, long componenttId)
        {
            if (componenttId <= 0)
            {
                throw new ArgumentNullException(nameof(componenttId));
            }

            ComponentLockModel emptyModel = new ComponentLockModel();   // pass an empty model as input since the ContentLength need to be >= 0 for POST

            var request = client.CreateRequest("/api/Components/" + componenttId + "/Lock", HttpMethod.Post);
            return client.RequestContent<ComponentLockModel, ComponentLockModel>(request, emptyModel);
        }

        /// <summary>
        /// Locks the components asynchronous.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componentIds">Contains a list of component identifiers.</param>
        /// <returns>Returns a List of <see cref="BatchComponentLockModel"/> objects of the components that have been locked.</returns>
        /// <exception cref="ArgumentNullException">componentIds is required</exception>
        public static List<BatchComponentLockModel> LockComponents(this InspireClient client, List<long> componentIds)
        {
            if (componentIds == null)
            {
                throw new ArgumentNullException(nameof(componentIds));
            }

            var request = client.CreateRequest("/api/Components/Lock", HttpMethod.Post);

            return client.RequestContent<List<long>, List<BatchComponentLockModel>>(request, componentIds);
        }

        /// <summary>
        /// Unlocks the components.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componentIds">Contains a list of component identifiers.</param>
        /// <param name="confirmPendingEdits">Contains a boolean value if set to <c>true</c> [confirm pending edits].</param>
        /// <returns>Returns a List of <see cref="UnlockResultModel"/> objects.</returns>
        /// <exception cref="ArgumentNullException">componentIds is required</exception>
        public static List<UnlockResultModel> UnlockComponents(this InspireClient client, List<long> componentIds, bool confirmPendingEdits = false)
        {
            if (componentIds == null)
            {
                throw new ArgumentNullException(nameof(componentIds));
            }

            var request = client.CreateRequest("/api/Components/UnLock?confirmPendingEdits=" + confirmPendingEdits.ToString(), HttpMethod.Post);

            return client.RequestContent<List<long>, List<UnlockResultModel>>(request, componentIds);
        }

        /// <summary>
        /// This method is used to unlock a component.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componenttId">Contains the component identity.</param>
        /// <param name="confirmPendingEdits">Contains a value indicating whether it is okay to unlock the component regardless of pending edits.</param>
        /// <returns>Returns a <see cref="UnlockResultModel"/> object.</returns>
        public static UnlockResultModel UnlockComponent(this InspireClient client, long componenttId, bool confirmPendingEdits)
        {
            if (componenttId <= 0)
            {
                throw new ArgumentNullException(nameof(componenttId));
            }

            UnlockResultModel emptyModel = new UnlockResultModel();   // pass an empty model as input since the ContentLength need to be >= 0 for POST

            var request = client.CreateRequest("/api/Components/" + componenttId + "/Unlock?confirmPendingEdits=" + confirmPendingEdits, HttpMethod.Post);
            return client.RequestContent<UnlockResultModel, UnlockResultModel>(request, emptyModel);
        }

        /// <summary>
        /// This REST method is used to import components from one or more files.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient" /> that is used for communication.</param>
        /// <param name="importModel">The import model.</param>
        /// <param name="filePaths">Contains a list of local file paths that will be submitted for synchronization import.</param>
        /// <returns>
        /// Returns a <see cref="WorkerStateModel" /> of type <see cref="MinimalImportStateModel" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the import model is null.</exception>
        public static WorkerStateModel<MinimalImportStateModel> ImportComponents(this InspireClient client, ImportRequestModel importModel, List<string> filePaths)
        {
            if (importModel == null)
            {
                throw new ArgumentNullException(nameof(importModel));
            }

            // additional modifications the to request to support multi-part form posting
            string boundary = "componentsimport";
            var request = client.CreateRequest("api/Components/Import", HttpMethod.Post, contentType: "multipart/form-data; boundary=" + boundary);

            // write data out to the request stream
            using (var postStream = request.GetRequestStream())
            {
                postStream.WriteMultiPartFormData(importModel, filePaths, "importmodel", boundary);
            }

            return client.RequestContent<WorkerStateModel<MinimalImportStateModel>>(request);
        }

        /// <summary>
        /// This method is called to get the state of an import worker process.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="workerKey">Contains the import worker key to find.</param>
        /// <returns>Returns a <see cref="WorkerStateModel"/> of type <see cref="MinimalImportStateModel"/>.</returns>
        public static WorkerStateModel<MinimalImportStateModel> GetImportState(this InspireClient client, string workerKey)
        {
            if (string.IsNullOrEmpty(workerKey))
            {
                throw new ArgumentNullException(nameof(workerKey));
            }

            string encodedWorkerKey = Uri.EscapeUriString(workerKey);
            var request = client.CreateRequest("api/Components/Import?workerKey=" + encodedWorkerKey, HttpMethod.Get);

            return client.RequestContent<WorkerStateModel<MinimalImportStateModel>>(request);
        }

        /// <summary>
        /// This method is called to get the state of an import worker process.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="WorkerStateModel"/> input.</param>
        /// <returns>Returns a <see cref="WorkerStateModel"/> of type <see cref="MinimalImportStateModel"/>.</returns>
        public static WorkerStateModel<MinimalImportStateModel> GetImportState(this InspireClient client, WorkerStateModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            string encodedWorkerKey = Uri.EscapeUriString(inputModel.Key);
            var request = client.CreateRequest("api/Components/Import?workerKey=" + encodedWorkerKey, HttpMethod.Get);

            return client.RequestContent<WorkerStateModel<MinimalImportStateModel>>(request);
        }

        /// <summary>
        /// This method is called to get the state of an export worker process.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="workerKey">Contains the export worker key to find.</param>
        /// <returns>Returns a <see cref="WorkerStateModel"/> of type <see cref="MinimalExportStateModel"/>.</returns>
        public static WorkerStateModel<MinimalExportStateModel> GetExportState(this InspireClient client, string workerKey)
        {
            if (string.IsNullOrEmpty(workerKey))
            {
                throw new ArgumentNullException(nameof(workerKey));
            }

            string encodedWorkerKey = Uri.EscapeUriString(workerKey);
            var request = client.CreateRequest("api/Components/Export?workerKey=" + encodedWorkerKey, HttpMethod.Get);

            return client.RequestContent<WorkerStateModel<MinimalExportStateModel>>(request);
        }

        /// <summary>
        /// This method is called to get the state of an export worker process.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="WorkerStateModel"/> input.</param>
        /// <returns>Returns a <see cref="WorkerStateModel"/> of type <see cref="MinimalExportStateModel"/>.</returns>
        public static WorkerStateModel<MinimalExportStateModel> GetExportState(this InspireClient client, WorkerStateModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            string encodedWorkerKey = Uri.EscapeUriString(inputModel.Key);
            var request = client.CreateRequest("api/Components/Export?workerKey=" + encodedWorkerKey, HttpMethod.Get);

            return client.RequestContent<WorkerStateModel<MinimalExportStateModel>>(request);
        }

        /// <summary>
        /// Posts the component tags.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="componentId">The component identifier.</param>
        /// <param name="inputModel">The input model.</param>
        /// <returns>Returns the updated <see cref="ComponentTagModel"/> object.</returns>
        public static ComponentTagModel PostComponentTags(this InspireClient client, long componentId, ComponentTagModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest(string.Format("/api/Components/{0}/Tags", componentId), HttpMethod.Post);
            return client.RequestContent<ComponentTagModel, ComponentTagModel>(request, inputModel);
        }
        #endregion
    }
}