//-------------------------------------------------------------
// <copyright file="ProjectExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Projects
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Vasont.Inspire.Models.Components;
    using Vasont.Inspire.Models.Projects;
    using Vasont.Inspire.Models.Security;
    using Vasont.Inspire.Models.Transfers;

    /// <summary>
    /// This class extends the <see cref="InspireClient"/> class to include methods for calling 
    /// project API endpoints.
    /// </summary>
    public static class ProjectExtensions
    {
        /// <summary>
        /// This method retrieves the projects that the specified user is assigned membership and/or ownership.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <returns>Returns a list of <see cref="ProjectModel"/> models.</returns>
        public static List<ProjectModel> GetProjects(this InspireClient client)
        {
            var request = client.CreateRequest("/api/Projects");
            return client.RequestContent<List<ProjectModel>>(request);
        }

        /// <summary>
        /// This method is used to return a project within the system.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="projectId">Contains the identity for a project to find.</param>
        /// <returns>Returns a <see cref="ProjectModel"/> if found.</returns>
        public static ProjectModel GetProject(this InspireClient client, long projectId)
        {
            var request = client.CreateRequest("/api/Projects/" + projectId);
            return client.RequestContent<ProjectModel>(request);
        }

        /// <summary>
        /// This method retrieves the projects that the specified user has recently opened.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="maximumResults">A value indicating the maximum number of results to return</param>
        /// <returns>Returns a list of <see cref="ProjectModel"/> models.</returns>
        public static List<ProjectModel> GetRecentlyOpenedProjects(this InspireClient client, int maximumResults)
        {
            var request = client.CreateRequest("/api/Projects/Recent");
            return client.RequestContent<List<ProjectModel>>(request);
        }

        /// <summary>
        /// This method is used to return projects within the system where the given userId is a member.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="maximumResults">A value indicating the maximum number of results to return</param>
        /// <returns>Returns a list of <see cref="ProjectModel"/> models.</returns>
        public static List<ProjectModel> GetAssignedProjects(this InspireClient client, int maximumResults)
        {
            var request = client.CreateRequest("/api/Projects/Assignments");
            return client.RequestContent<List<ProjectModel>>(request);
        }

        /// <summary>
        /// This method is used to return a list of available participants.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="projectId">Contains the identity for a project to find.</param>
        /// <returns>Returns a list of <see cref="MinimalUserModel"/> models.</returns>
        public static List<MinimalUserModel> GetAvailableParticipants(this InspireClient client, long projectId)
        {
            var request = client.CreateRequest("/api/Projects/" + projectId + "/AvailableParticipants");
            return client.RequestContent<List<MinimalUserModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve all the available project activities.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <returns>Returns a list of <see cref="ProjectActivityModel"/> models.</returns>
        public static List<ProjectActivityModel> GetActivities(this InspireClient client)
        {
            var request = client.CreateRequest("/api/Projects/Activities");
            return client.RequestContent<List<ProjectActivityModel>>(request);
        }

        /// <summary>
        /// This method is used to return project assignments within the system that are associated with a project.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="projectId">Contains the identity of the project that is being searched for assignments.</param>
        /// <returns>Returns a list of <see cref="ProjectAssignmentModel"/> models.</returns>
        public static List<ProjectAssignmentModel> GetProjectAssignments(this InspireClient client, long projectId)
        {
            var request = client.CreateRequest("/api/Projects/" + projectId + "/Assignments");
            return client.RequestContent<List<ProjectAssignmentModel>>(request);
        }

        /// <summary>
        /// This method is used to delete an existing project.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the project identity.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool DeleteProject(this InspireClient client, long projectId)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            var request = client.CreateRequest("api/Projects/" + projectId, HttpMethod.Delete);
            client.RequestContent(request);
            return client.HasError;
        }

        /// <summary>
        /// This method is used to delete a project participant.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the project identity.</param>
        /// <param name="model">Contains the <see cref="ProjectParticipantModel"/> that will identify which participant to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool DeleteProjectParticipant(this InspireClient client, long projectId, ProjectParticipantModel model)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest("api/Projects/" + projectId + "/Participants", HttpMethod.Delete);
            client.RequestContent(request, model);
            return client.HasError;
        }

        /// <summary>
        /// This method is used to delete a project assignment.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the project in which the assignment will be deleted.</param>
        /// <param name="projectAssignmentId">Contains the project assignment identity to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool DeleteProjectAssignment(this InspireClient client, long projectId, long projectAssignmentId)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (projectAssignmentId <= 0)
            {
                throw new ArgumentNullException(nameof(projectAssignmentId));
            }

            var request = client.CreateRequest("api/Projects/" + projectId + "/Assignments/" + projectAssignmentId, HttpMethod.Delete);
            client.RequestContent(request);
            return client.HasError;
        }

        /// <summary>
        /// This method is used to remove a component from a project assignment.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the project.</param>
        /// <param name="projectAssignmentId">Contains the identity of the project assignment.</param>
        /// <param name="componentId">Contains the identity of the component that will be removed from the assignment.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool DeleteProjectAssignmentComponent(this InspireClient client, long projectId, long projectAssignmentId, long componentId)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (projectAssignmentId <= 0)
            {
                throw new ArgumentNullException(nameof(projectAssignmentId));
            }

            if (componentId <= 0)
            {
                throw new ArgumentNullException(nameof(componentId));
            }

            var request = client.CreateRequest("api/Projects/" + projectId + "/Assignments/" + projectAssignmentId + "/Components/" + componentId, HttpMethod.Delete);
            client.RequestContent(request);
            return client.HasError;
        }

        /// <summary>
        /// This method is used to delete a project folder item model.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the parent project.</param>
        /// <param name="model">Contains the existing <see cref="ProjectFolderItemModel"/> to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool DeleteProjectFolder(this InspireClient client, long projectId, ProjectFolderItemModel model)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest("api/Projects/" + projectId + "/FolderItems/0", HttpMethod.Delete);
            client.RequestContent(request, model);
            return client.HasError;
        }

        /// <summary>
        /// This method is used to create a new project.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="inputModel">Contains the <see cref="ProjectModel"/> input.</param>
        /// <returns>Returns the <see cref="ProjectModel"/> object if found.</returns>
        public static ProjectModel CreateProject(this InspireClient client, ProjectModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Projects", HttpMethod.Post);
            return client.RequestContent<ProjectModel, ProjectModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to add project participants.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the project that will have its participants added.</param>
        /// <param name="models">Contains a list of <see cref="ProjectParticipantModel"/> related to the project that will be added.</param>
        /// <returns>Returns a list of newly created <see cref="ProjectParticipantModel"/> objects.</returns>
        public static List<ProjectParticipantModel> CreateProjectParticipants(this InspireClient client, long projectId, List<ProjectParticipantModel> models)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId + "/Participants", HttpMethod.Post);
            return client.RequestContent<List<ProjectParticipantModel>, List<ProjectParticipantModel>>(request, models);
        }

        /// <summary>
        /// This method is used to create a new project assignment.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the project in which the assignment will be created.</param>
        /// <param name="model">Contains a <see cref="ProjectAssignmentModel"/> with details about project assignments.</param>
        /// <returns>Returns the newly created <see cref="ProjectAssignmentModel"/> object.</returns>
        public static ProjectAssignmentModel CreateProjectAssignment(this InspireClient client, long projectId, ProjectAssignmentModel model)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId + "/Assignments", HttpMethod.Post);
            return client.RequestContent<ProjectAssignmentModel, ProjectAssignmentModel>(request, model);
        }

        /// <summary>
        /// This method is used to create a project folder item model.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the parent project.</param>
        /// <param name="model">Contains a <see cref="ProjectFolderItemModel"/> to create.</param>
        /// <returns>Returns the newly created <see cref="ProjectFolderItemModel"/> object.</returns>
        public static ProjectFolderItemModel CreateProjectFolder(this InspireClient client, long projectId, ProjectFolderItemModel model)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId + "/FolderItems", HttpMethod.Post);
            return client.RequestContent<ProjectFolderItemModel, ProjectFolderItemModel>(request, model);
        }

        /// <summary>
        /// This method is used to add components to an existing folder.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the parent project.</param>
        /// <param name="folderId">Contains the identity of the project folder.</param>
        /// <param name="models">A list of <see cref="MinimalComponentModel"/> objects.</param>
        /// <returns>Returns a list of <see cref="MinimalComponentModel"/> objects.</returns>
        public static List<MinimalComponentModel> AddComponentsToFolder(this InspireClient client, long projectId, long folderId, List<MinimalComponentModel> models)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (folderId <= 0)
            {
                throw new ArgumentNullException(nameof(folderId));
            }

            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId + "/Folders/" + folderId, HttpMethod.Post);
            return client.RequestContent<List<MinimalComponentModel>, List<MinimalComponentModel>>(request, models);
        }

        /// <summary>
        /// This method is used to request an export process for one or more components.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the project.</param>
        /// <param name="folderId">Contains the identity of the project folder.</param>
        /// <param name="includeSubFolders">Contains a value indicating whether components in descendant sub-folders should be exported.</param>
        /// <returns>The <see cref="ExportRequestModel"/> object with information about the export process.</returns>
        public static ExportRequestModel ExportComponents(this InspireClient client, long projectId, long folderId, bool includeSubFolders, long exportId = 0)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (folderId <= 0)
            {
                throw new ArgumentNullException(nameof(folderId));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId + "/FolderItems/" + folderId + "/Export/" + exportId + "/?includeSubFolders=" + includeSubFolders, HttpMethod.Post);
            return client.RequestContent<ExportRequestModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing project.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the project to update.</param>
        /// <param name="inputModel">Contains the <see cref="ProjectModel"/> input.</param>
        /// <returns>Returns the updated <see cref="ProjectModel"/> object.</returns>
        public static ProjectModel UpdateProject(this InspireClient client, long projectId, ProjectModel inputModel)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId, HttpMethod.Put);
            return client.RequestContent<ProjectModel, ProjectModel>(request, inputModel);
        }

        /// <summary>
        /// This method is used to update project participants.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the project that will have its participants updated.</param>
        /// <param name="models">Contains a list of <see cref="ProjectParticipantModel"/> related to the project.</param>
        /// <returns>Returns a list of <see cref="ProjectParticipantModel"/> objects.</returns>
        public static List<ProjectParticipantModel> UpdateProjectParticipants(this InspireClient client, long projectId, List<ProjectParticipantModel> models)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId + "/Participants", HttpMethod.Put);
            return client.RequestContent<List<ProjectParticipantModel>, List<ProjectParticipantModel>>(request, models);
        }

        /// <summary>
        /// This method is used to update a project assignment.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the project.</param>
        /// <param name="projectAssignmentId">Contains the identity of the project assignment to update.</param>
        /// <param name="model">Contains the <see cref="ProjectAssignmentModel"/> object.</param>
        /// <returns>Returns the <see cref="ProjectAssignmentModel"/> object.</returns>
        public static ProjectAssignmentModel UpdateProjectAssignment(this InspireClient client, long projectId, long projectAssignmentId, ProjectAssignmentModel model)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (projectAssignmentId <= 0)
            {
                throw new ArgumentNullException(nameof(projectAssignmentId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId + "/Assignments/" + projectAssignmentId, HttpMethod.Put);
            return client.RequestContent<ProjectAssignmentModel, ProjectAssignmentModel>(request, model);
        }

        /// <summary>
        /// This method is used to update a project assignment completion status.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the project.</param>
        /// <param name="projectAssignmentId">Contains the identity of the project assignment to update.</param>
        /// <param name="model">Contains the <see cref="ProjectAssignmentModel"/> object.</param>
        /// <returns>Returns the <see cref="ProjectAssignmentModel"/> object.</returns>
        public static ProjectAssignmentModel UpdateProjectAssignmentCompletionStatus(this InspireClient client, long projectId, long projectAssignmentId, ProjectAssignmentModel model)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (projectAssignmentId <= 0)
            {
                throw new ArgumentNullException(nameof(projectAssignmentId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId + "/Assignments/" + projectAssignmentId + "/State/Complete", HttpMethod.Put);
            return client.RequestContent<ProjectAssignmentModel, ProjectAssignmentModel>(request, model);
        }

        /// <summary>
        /// This method is used to rename a project folder.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectId">Contains the identity of the project.</param>
        /// <param name="itemId">Contains the identity of the folder item object.</param>
        /// <param name="model">Contains the <see cref="ProjectFolderItemModel"/> object.</param>
        /// <returns>Returns the <see cref="ProjectFolderItemModel"/> object.</returns>
        public static ProjectFolderItemModel RenameProjectFolder(this InspireClient client, long projectId, string itemId, ProjectFolderItemModel model)
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId + "/FolderItems/" + itemId + "/Rename", HttpMethod.Put);
            return client.RequestContent<ProjectFolderItemModel, ProjectFolderItemModel>(request, model);
        }

        /// <summary>
        /// This method is used to move a project folder to another target folder.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the existing project folder item model to move.</param>
        /// <param name="projectId">Contains the identity of the parent project.</param>
        /// <param name="itemId">Contains the identity of the folder item object.</param>
        /// <param name="targetFolderId">Contains the new target folder of the folder item.</param>
        /// <returns>Returns the <see cref="ProjectFolderItemModel"/> object.</returns>
        public static ProjectFolderItemModel MoveProjectFolder(this InspireClient client, ProjectFolderItemModel model, long projectId, string itemId, string targetFolderId = "")
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest("/api/Projects/" + projectId + "/FolderItems/" + itemId + "/Move/" + targetFolderId, HttpMethod.Put);
            return client.RequestContent<ProjectFolderItemModel, ProjectFolderItemModel>(request, model);
        }
    }
}