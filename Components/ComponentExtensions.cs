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
    using Vasont.Inspire.Models.Versioning;
    using Vasont.Inspire.Models.Worker;
    using Vasont.Inspire.Models.Workflow;
    using Vasont.Inspire.SDK.Extensions;

    /// <summary>
    /// This class extends the <see cref="InspireClient"/> class to include methods for calling 
    /// component API endpoints.
    /// </summary>
    public static class ComponentExtensions
    {
        #region TODO: Review location: Component Type Methods
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
        #endregion

        #region Public Component Extension Methods

        /// <summary>
        /// This method is used to retrieve the component workflow templates.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <returns>Returns a list of <see cref="WorkflowTemplateModel"/> objects.</returns>
        public static List<WorkflowTemplateModel> GetComponentWorkflowTemplates(this InspireClient client)
        {
            var request = client.CreateRequest($"/api/Components/WorkflowTemplates");
            return client.RequestContent<List<WorkflowTemplateModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific component's workflow templates.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">Contains the unique identity of the component.</param>
        /// <returns>Returns a list of <see cref="WorkflowTemplateModel"/> objects.</returns>
        public static WorkflowModel GetComponentWorkflowTemplate(this InspireClient client, long componentId)
        {
            var request = client.CreateRequest($"/api/Components/{componentId}/WorkflowTemplate");
            return client.RequestContent<WorkflowModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve the component templates based on the supplied document types.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="documentTypes">Contains a comma delimited list of document types that will be used to filter the results.</param>
        /// <returns>Returns the <see cref="MinimalComponentModel"/> object.</returns>
        public static List<MinimalComponentModel> GetComponentTemplates(this InspireClient client, string documentTypes = "")
        {
            var request = client.CreateRequest($"/api/Components/Templates/{documentTypes}");
            return client.RequestContent<List<MinimalComponentModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a batch of requested components.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentIds">The list of component identifiers.</param>
        /// <returns>Returns a <see cref="BatchComponentsResultModel"/> object containing a list of requested components.</returns>
        public static BatchComponentsResultModel GetComponents(this InspireClient client, BatchComponentsRequestModel componentIds)
        {
            var request = client.CreateRequest("/api/Components/Batch", HttpMethod.Post);
            return client.RequestContent<BatchComponentsRequestModel, BatchComponentsResultModel>(request, componentIds);
        }

        /// <summary>
        /// This method is used to retrieve minimal detail about a specific component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">The component identity.</param>
        /// <returns>Returns a list of <see cref="MinimalComponentModel"/> objects.</returns>
        public static MinimalComponentModel GetComponent(this InspireClient client, long componentId)
        {
            var request = client.CreateRequest($"/api/Components/{componentId}");
            return client.RequestContent<MinimalComponentModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve the detail of the specific component.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="componentId">The component identifier.</param>
        /// <returns>Returns the detailed component model.</returns>
        public static DetailedComponentModel GetComponentDetail(this InspireClient client, long componentId)
        {
            var request = client.CreateRequest($"/api/Components/{componentId}/Details");
            return client.RequestContent<DetailedComponentModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve the content from a specific component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">The component identity.</param>
        /// <returns>Returns an array of bytes that represent the content of the component.</returns>
        public static byte[] GetComponentContent(this InspireClient client, long componentId)
        {
            var request = client.CreateRequest($"/api/Components/{componentId}/Content");
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
            var request = client.CreateRequest($"/api/Components/{componentId}/Permissions");
            return client.RequestContent<PermissionUpdateModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve the specified components elements.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">The list of component identifier.</param>
        /// <param name="allowedElements">Contains an optional list of allowed element names.</param>
        /// <returns>Returns a <see cref="BatchComponentsResultModel"/> object containing a list of requested components.</returns>
        public static List<XmlComponentElementModel> GetComponentElements(this InspireClient client, long componentId, List<string> allowedElements)
        {
            var request = client.CreateRequest($"/api/Components/{componentId}", HttpMethod.Post);
            return client.RequestContent<List<string>, List<XmlComponentElementModel>>(request, allowedElements);
        }

        /// <summary>
        /// This method is used approve one or more component states.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componentIds">Contains the component identities.</param>
        /// <returns>Returns a value indicating success.</returns>
        public static bool ApproveComponents(this InspireClient client, List<long> componentIds)
        {
            if (componentIds == null)
            {
                throw new ArgumentNullException(nameof(componentIds));
            }

            var request = client.CreateRequest($"/api/Components/Approve", HttpMethod.Post);
            return client.RequestContent<List<long>, bool>(request, componentIds);
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
            var request = client.CreateRequest($"/api/Components/{componentId}/Configuration?force={forceReload}");
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

            var request = client.CreateRequest($"/api/Components/{componentId}/Permissions", HttpMethod.Put);
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

            var request = client.CreateRequest("/api/Components", HttpMethod.Post);
            return client.RequestContent<CreateComponentFromTemplateModel, MinimalComponentModel>(request, inputModel);
        }

        /// <summary>
        /// Saves the component content as a different component target.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="SaveAsComponentModel"/> input.</param>
        /// <returns>Returns a <see cref="MinimalComponentModel"/> object if found.</returns>
        public static MinimalComponentModel CreateComponentAs(this InspireClient client, SaveAsComponentModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Components/SaveAs", HttpMethod.Post);
            return client.RequestContent<SaveAsComponentModel, MinimalComponentModel>(request, inputModel);
        }

        /// <summary>
        /// Creates a new component content branch.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="SaveAsBranchModel"/> input.</param>
        /// <returns>Returns a <see cref="MinimalComponentModel"/> object if found.</returns>
        public static MinimalComponentModel CreateComponentBranch(this InspireClient client, SaveAsBranchModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Components/Branch", HttpMethod.Post);
            return client.RequestContent<SaveAsBranchModel, MinimalComponentModel>(request, inputModel);
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

            var request = client.CreateRequest($"/api/Components/Export/{exportId}", HttpMethod.Post);
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

            var request = client.CreateRequest($"/api/Components/{componentId}/Rename", HttpMethod.Put);
            return client.RequestContent<MinimalComponentModel, MinimalComponentModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to set permissions on an existing component.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="PermissionUpdateModel"/> input.</param>
        /// <returns>Returns a <see cref="PermissionUpdateModel"/> object if found.</returns>
        public static PermissionUpdateModel UpdateComponentPermissions(this InspireClient client, PermissionUpdateModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Components/Permissions", HttpMethod.Put);
            return client.RequestContent<PermissionUpdateModel, PermissionUpdateModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to set permissions on an existing component.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="PermissionUpdateModel"/> input.</param>
        /// <returns>Returns a <see cref="PermissionUpdateModel"/> object if found.</returns>
        [Obsolete("This method's naming convention is obsolete. Please use UpdateComponentPermissions(). This method will be removed in a future release.")]
        public static PermissionUpdateModel SetComponentPermissions(this InspireClient client, PermissionUpdateModel inputModel)
        {
            return client.UpdateComponentPermissions(inputModel);
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

            var request = client.CreateRequest($"/api/Components/Move/{targetFolderId}", HttpMethod.Put);

            client.RequestContent(request, componentIds);
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

            var request = client.CreateRequest($"/api/Components/{componenttId}/Lock", HttpMethod.Post);
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

            var request = client.CreateRequest($"/api/Components/UnLock?confirmPendingEdits={confirmPendingEdits}", HttpMethod.Post);
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

            var request = client.CreateRequest($"/api/Components/{componenttId}/Unlock?confirmPendingEdits={confirmPendingEdits}", HttpMethod.Post);
            return client.RequestContent<string, UnlockResultModel>(request, string.Empty);
        }

        /// <summary>
        /// This method is used to delete a specific component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">The component identity.</param>
        /// <returns>Returns a <see cref="DeleteResultModel"/> object.</returns>
        public static DeleteResultModel DeleteComponent(this InspireClient client, long componentId)
        {
            var request = client.CreateRequest($"/api/Components/{componentId}", HttpMethod.Delete);
            return client.RequestContent<DeleteResultModel>(request);
        }

        /// <summary>
        /// Deletes the specified components.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componentIds">Contains a list of component identifiers.</param>
        /// <returns>Returns a value indicating the success of the deletion action.</returns>
        /// <exception cref="ArgumentNullException">componentIds is required</exception>
        public static bool DeleteComponents(this InspireClient client, List<long> componentIds)
        {
            if (componentIds == null)
            {
                throw new ArgumentNullException(nameof(componentIds));
            }

            var request = client.CreateRequest($"/api/Components", HttpMethod.Delete);
            client.RequestContent<List<long>, List<UnlockResultModel>>(request, componentIds);
            return !client.HasError;
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
        public static MinimalWorkerStateModel<MinimalImportStateModel> ImportComponents(this InspireClient client, ImportRequestModel importModel, List<string> filePaths)
        {
            if (importModel == null)
            {
                throw new ArgumentNullException(nameof(importModel));
            }

            // additional modifications the to request to support multi-part form posting
            string boundary = "componentsimport";
            var request = client.CreateRequest("/api/Components/Import", HttpMethod.Post, contentType: "multipart/form-data; boundary=" + boundary);

            // write data out to the request stream
            using (var postStream = request.GetRequestStream())
            {
                postStream.WriteMultiPartFormData(importModel, filePaths, "importmodel", boundary);
            }

            return client.RequestContent<MinimalWorkerStateModel<MinimalImportStateModel>>(request);
        }

        /// <summary>
        /// This method is called to get the state of an import worker process.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="workerKey">Contains the import worker key to find.</param>
        /// <returns>Returns a <see cref="WorkerStateModel"/> of type <see cref="MinimalImportStateModel"/>.</returns>
        public static MinimalWorkerStateModel<MinimalImportStateModel> GetImportState(this InspireClient client, string workerKey)
        {
            if (string.IsNullOrEmpty(workerKey))
            {
                throw new ArgumentNullException(nameof(workerKey));
            }

            string encodedWorkerKey = Uri.EscapeUriString(workerKey);
            var request = client.CreateRequest($"/api/Components/Import?workerKey={encodedWorkerKey}", HttpMethod.Get);

            return client.RequestContent<MinimalWorkerStateModel<MinimalImportStateModel>>(request);
        }

        /// <summary>
        /// This method is called to get the state of an import worker process.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="WorkerStateModel"/> input.</param>
        /// <returns>Returns a <see cref="WorkerStateModel"/> of type <see cref="MinimalImportStateModel"/>.</returns>
        public static MinimalWorkerStateModel<MinimalImportStateModel> GetImportState(this InspireClient client, WorkerStateModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            string encodedWorkerKey = Uri.EscapeUriString(inputModel.Key);
            var request = client.CreateRequest($"/api/Components/Import?workerKey={encodedWorkerKey}", HttpMethod.Get);

            return client.RequestContent<MinimalWorkerStateModel<MinimalImportStateModel>>(request);
        }

        /// <summary>
        /// This method is called to get the state of an export worker process.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="workerKey">Contains the export worker key to find.</param>
        /// <returns>Returns a <see cref="WorkerStateModel"/> of type <see cref="MinimalExportStateModel"/>.</returns>
        public static MinimalWorkerStateModel<MinimalExportStateModel> GetExportState(this InspireClient client, string workerKey)
        {
            if (string.IsNullOrEmpty(workerKey))
            {
                throw new ArgumentNullException(nameof(workerKey));
            }

            string encodedWorkerKey = Uri.EscapeUriString(workerKey);
            var request = client.CreateRequest($"/api/Components/Export?workerKey={encodedWorkerKey}", HttpMethod.Get);

            return client.RequestContent<MinimalWorkerStateModel<MinimalExportStateModel>>(request);
        }

        /// <summary>
        /// This method is called to get the state of an export worker process.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="WorkerStateModel"/> input.</param>
        /// <returns>Returns a <see cref="WorkerStateModel"/> of type <see cref="MinimalExportStateModel"/>.</returns>
        public static MinimalWorkerStateModel<MinimalExportStateModel> GetExportState(this InspireClient client, WorkerStateModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            string encodedWorkerKey = Uri.EscapeUriString(inputModel.Key);
            var request = client.CreateRequest($"/api/Components/Export?workerKey={encodedWorkerKey}", HttpMethod.Get);

            return client.RequestContent<MinimalWorkerStateModel<MinimalExportStateModel>>(request);
        }

        #endregion

        #region Public Tag Related Methods

        /// <summary>
        /// Posts the component tags.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="componentId">The component identifier.</param>
        /// <param name="inputModel">The input model.</param>
        /// <returns>Returns the updated <see cref="ComponentTagModel"/> object.</returns>
        public static ComponentTagModel UpdateComponentTags(this InspireClient client, long componentId, ComponentTagModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/{componentId}/Tags", HttpMethod.Post);
            return client.RequestContent<ComponentTagModel, ComponentTagModel>(request, inputModel);
        }

        /// <summary>
        /// Posts the component tags.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="componentId">The component identifier.</param>
        /// <param name="inputModel">The input model.</param>
        /// <returns>Returns the updated <see cref="ComponentTagModel"/> object.</returns>
        [Obsolete("This method's naming convention is obsolete. Please use UpdateComponentTags(). This method will be removed in a future release.")]
        public static ComponentTagModel PostComponentTags(this InspireClient client, long componentId, ComponentTagModel inputModel)
        {
            return client.UpdateComponentTags(componentId, inputModel);
        }
        #endregion

        #region Public Component Relation Methods     

        /// <summary>
        /// This method is used to repair an invalid component relationship.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentRelationsQueryModel"/> input model.</param>
        /// <returns>Returns a <see cref="ComponentRelationsResultModel"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static ComponentRelationsRepairResultModel RepairComponentRelationship(this InspireClient client, ComponentRelationModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/{inputModel.SourceComponent.ComponentId}/RepairRelationship", HttpMethod.Post);
            return client.RequestContent<ComponentRelationModel, ComponentRelationsRepairResultModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to repair an invalid component relationship.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentAutoRepairRequestModel"/> input model.</param>
        /// <returns>Returns a <see cref="MinimalWorkerStateModel{ComponentAutoRepairStateModel}"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static MinimalWorkerStateModel<ComponentAutoRepairStateModel> AutoRepairComponentRelationships(this InspireClient client, ComponentAutoRepairRequestModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/AutoRepairRelationships", HttpMethod.Post);
            return client.RequestContent<ComponentAutoRepairRequestModel, MinimalWorkerStateModel<ComponentAutoRepairStateModel>>(request, inputModel);
        }

        /// <summary>
        /// Gets the component relations.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentRelationsQueryModel"/> input model.</param>
        /// <returns>Returns a <see cref="ComponentRelationsResultModel"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static ComponentRelationsResultModel GetComponentRelations(this InspireClient client, ComponentRelationsQueryModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/ComponentRelations", HttpMethod.Post);
            return client.RequestContent<ComponentRelationsQueryModel, ComponentRelationsResultModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to get the relations for the specified component and return as a CSV formatted report file contents.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentRelationsQueryModel"/> input model.</param>
        /// <returns>Returns a <see cref="ComponentRelationsResultModel"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static byte[] GetComponentRelationsReport(this InspireClient client, ComponentRelationsQueryModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/ComponentRelations/Export", HttpMethod.Post);
            return client.RequestContent<ComponentRelationsQueryModel, byte[]>(request, inputModel);
        }

        /// <summary>
        /// Gets the component dependencies.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentRelationsQueryModel"/> input model.</param>
        /// <returns>Returns a <see cref="ComponentDependenciesResultModel"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static ComponentDependenciesResultModel GetComponentDependencies(this InspireClient client, ComponentRelationsQueryModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/ComponentDependencies", HttpMethod.Post);
            return client.RequestContent<ComponentRelationsQueryModel, ComponentDependenciesResultModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to get the dependencies for the specified component and return as a CSV formatted report file contents.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentRelationsQueryModel"/> input model.</param>
        /// <returns>Returns a <see cref="ComponentRelationsResultModel"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static byte[] GetComponentDependenciesReport(this InspireClient client, ComponentRelationsQueryModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/ComponentDependencies/Export", HttpMethod.Post);
            return client.RequestContent<ComponentRelationsQueryModel, byte[]>(request, inputModel);
        }

        /// <summary>
        /// This method is used to retrieve the branch relations for the specified component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="sourceComponentId">Contains the source component identity to retrieve branch relations.</param>
        /// <returns>Returns a <see cref="List{BranchReferenceModel}"/> model.</returns>
        public static List<BranchReferenceModel> GetComponentBranchRelations(this InspireClient client, long sourceComponentId)
        {
            var request = client.CreateRequest($"/api/Components/{sourceComponentId}/BranchRelations", HttpMethod.Get);
            return client.RequestContent<List<BranchReferenceModel>>(request);
        }

        /// <summary>
        /// This method is used to get the branches for the specified component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentRelationsQueryModel"/> input model.</param>
        /// <returns>Returns a <see cref="ComponentRelationsResultModel"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static ComponentRelationsResultModel GetComponentBranches(this InspireClient client, ComponentRelationsQueryModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/ComponentBranches", HttpMethod.Post);
            return client.RequestContent<ComponentRelationsQueryModel, ComponentRelationsResultModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to get the branches for the specified component and return as a CSV formatted report file contents.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentRelationsQueryModel"/> input model.</param>
        /// <returns>Returns a <see cref="ComponentRelationsResultModel"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static byte[] GetComponentBranchesReport(this InspireClient client, ComponentRelationsQueryModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/ComponentBranches/Export", HttpMethod.Post);
            return client.RequestContent<ComponentRelationsQueryModel, byte[]>(request, inputModel);
        }

        #endregion

        #region Translation Related Extension Methods
        /// <summary>
        /// This method is used to get the translations for the specified component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentRelationsQueryModel"/> input model.</param>
        /// <returns>Returns a <see cref="ComponentRelationsResultModel"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static ComponentRelationsResultModel GetComponentTranslations(this InspireClient client, ComponentRelationsQueryModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/ComponentTranslations", HttpMethod.Post);
            return client.RequestContent<ComponentRelationsQueryModel, ComponentRelationsResultModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to get the translations for the specified component and return as a CSV formatted report file contents.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentRelationsQueryModel"/> input model.</param>
        /// <returns>Returns a <see cref="ComponentRelationsResultModel"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static byte[] GetComponentTranslationsReport(this InspireClient client, ComponentRelationsQueryModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/ComponentTranslations/Export", HttpMethod.Post);
            return client.RequestContent<ComponentRelationsQueryModel, byte[]>(request, inputModel);
        }

        #endregion

        #region History Relation Extension Methods
        /// <summary>
        /// This method is used to get the history for the specified component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="inputModel">The <see cref="ComponentHistoryQueryModel"/> input model.</param>
        /// <param name="usePostMethod">Contains a value indicating whether the POST method is used in query.</param>
        /// <returns>Returns a <see cref="ComponentHistoryResultModel"/> model.</returns>
        /// <exception cref="ArgumentNullException">thrown if the model is not set.</exception>
        public static ComponentHistoryResultModel GetComponentHistory(this InspireClient client, ComponentHistoryQueryModel inputModel, bool usePostMethod = false)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/api/Components/{inputModel.ComponentId}/History", usePostMethod ? HttpMethod.Post : HttpMethod.Get);
            return client.RequestContent<ComponentHistoryQueryModel, ComponentHistoryResultModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to get the history for the specified component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">Contains the component identity.</param>
        /// <param name="changesetId">Contains the specific history changeset record identity.</param>
        /// <returns>Returns a <see cref="ComponentHistoryResultModel"/> model.</returns>
        public static MinimalComponentHistoryModel GetComponentHistoryDetails(this InspireClient client, long componentId, Guid changesetId)
        {
            var request = client.CreateRequest($"/api/Components/{componentId}/History/{changesetId}");
            return client.RequestContent<MinimalComponentHistoryModel>(request);
        }

        /// <summary>
        /// This method is used to get the history for the specified component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="componentId">Contains the component identity.</param>
        /// <param name="changesetId">Contains the specific history changeset record identity.</param>
        /// <param name="inputModel">Contains the <see cref="RestoreOptionsModel"/> model.</param>
        /// <returns>Returns a <see cref="ComponentHistoryResultModel"/> model.</returns>
        public static ChangesetModel RestoreComponentFromHistory(this InspireClient client, long componentId, Guid changesetId, RestoreOptionsModel inputModel)
        {
            var request = client.CreateRequest($"/api/Components/{componentId}/History/{changesetId}", HttpMethod.Post);
            return client.RequestContent<RestoreOptionsModel, ChangesetModel>(request, inputModel);
        }
        #endregion
    }
}