//-------------------------------------------------------------
// <copyright file="TranslationsExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Translations
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Vasont.Inspire.Models.Translations;
    using Vasont.Inspire.Models.Worker;

    /// <summary>
    /// This class extends the <see cref="InspireClient" /> class to include methods for calling translation API endpoints.
    /// </summary>
    public static class TranslationsExtensions
    {
        /// <summary>
        /// Get all available translation job states.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <returns>Returns a list of <see cref="TranslationJobStateModel" /> objects.</returns>
        [Obsolete("This method is obsolete. Please use RetrieveTranslationJobStates() going forward. This method will be removed in a future release.")]
        public static List<TranslationJobStateModel> GetTranslationJobStates(this InspireClient client)
        {
            return RetrieveTranslationJobStates(client);
        }

        /// <summary>
        /// Get all available translation job states.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <returns>Returns a list of <see cref="TranslationJobStateModel" /> objects.</returns>
        public static List<TranslationJobStateModel> RetrieveTranslationJobStates(this InspireClient client)
        {
            var request = client.CreateRequest($"/Translations/States");
            return client.RequestContent<List<TranslationJobStateModel>>(request);
        }

        /// <summary>
        /// Gets all translation vendors.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <returns>Returns a List of <see cref="TranslationVendorModel" /> objects.</returns>
        [Obsolete("This method is obsolete. Please use RetrieveTranslationVendors() going forward. This method will be removed in a future release.")]
        public static List<TranslationVendorModel> GetTranslationVendors(this InspireClient client)
        {
            return RetrieveTranslationVendors(client);
        }

        /// <summary>
        /// Gets all translation vendors.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <returns>Returns a List of <see cref="TranslationVendorModel" /> objects.</returns>
        public static List<TranslationVendorModel> RetrieveTranslationVendors(this InspireClient client)
        {
            var request = client.CreateRequest($"/Translations/Vendors");
            return client.RequestContent<List<TranslationVendorModel>>(request);
        }

        /// <summary>
        /// Gets the translation job by identifier.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="translationJobId">The translation job identifier.</param>
        /// <returns>Returns the <see cref="TranslationJobModel" /> object found for the requested Id.</returns>
        [Obsolete("This method is obsolete. Please use FindTranslationJob() going forward. This method will be removed in a future release.")]
        public static TranslationJobModel GetTranslationJobById(this InspireClient client, long translationJobId)
        {
            return FindTranslationJob(client, translationJobId);
        }

        /// <summary>
        /// Gets the translation job by identifier.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="translationJobId">The translation job identifier.</param>
        /// <returns>Returns the <see cref="TranslationJobModel" /> object found for the requested Id.</returns>
        public static TranslationJobModel FindTranslationJob(this InspireClient client, long translationJobId)
        {
            var request = client.CreateRequest($"/Translations/{translationJobId}");
            return client.RequestContent<TranslationJobModel>(request);
        }

        /// <summary>
        /// Finds the translation jobs for the requested translation job Ids.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="translationJobIds">The translation job ids.</param>
        /// <returns>Returns a List of the requested <see cref="MinimalTranslationJobModel" /> objects.</returns>
        public static List<MinimalTranslationJobModel> FindTranslationJobs(this InspireClient client, List<long> translationJobIds)
        {
            if (translationJobIds == null)
            {
                throw new ArgumentNullException(nameof(translationJobIds));
            }

            var request = client.CreateRequest($"/Translations/RetrieveTranslationJobs", HttpMethod.Post);

            return client.RequestContent<List<long>, List<MinimalTranslationJobModel>>(request, translationJobIds);
        }

        /// <summary>
        /// Cancels a Translation Job.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="model">The <see cref="TranslationJobModel"/> used to Cancel the Translation Job.</param>
        /// <returns>Returns the <see cref="TranslationJobModel" /> object that was canceled.</returns>
        public static TranslationJobModel PutTranslationCancel(this InspireClient client, TranslationJobModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Translations/{model.TranslationJobId}/Cancel", HttpMethod.Put);
            return client.RequestContent<TranslationJobModel, TranslationJobModel>(request, model);
        }

        /// <summary>
        /// Gets the queued translation jobs.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="translationVendorId">The translation vendor identifier.</param>
        /// <returns>Returns a <see cref="WorkerStateModel" /> of type <see cref="TranslationExportStateModel" />.</returns>
        [Obsolete("This method is obsolete. Please use FindQueuedTranslationJobs() going forward. This method will be removed in a future release.")]
        public static WorkerStateModel<TranslationExportStateModel> GetQueuedTranslationJobs(this InspireClient client, long translationVendorId)
        {
            return FindQueuedTranslationJobs(client, translationVendorId);
        }

        /// <summary>
        /// Gets the queued translation jobs.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="translationVendorId">The translation vendor identifier.</param>
        /// <returns>Returns a <see cref="WorkerStateModel" /> of type <see cref="TranslationExportStateModel" />.</returns>
        public static WorkerStateModel<TranslationExportStateModel> FindQueuedTranslationJobs(this InspireClient client, long translationVendorId)
        {
            if (translationVendorId <= 0)
            {
                throw new ArgumentNullException(nameof(translationVendorId));
            }

            TranslationExportRequestModel translationExportRequestModel = new TranslationExportRequestModel
            {
                TranslationVendorId = translationVendorId,
                SendNotification = false
            };

            var request = client.CreateRequest($"/Translations/Export/GetQueued/{translationVendorId}", HttpMethod.Post);

            return client.RequestContent<TranslationExportRequestModel, WorkerStateModel<TranslationExportStateModel>>(request, translationExportRequestModel);
        }

        /// <summary>
        /// Gets the translation export state.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="workerKey">The worker key.</param>
        /// <returns>Returns a <see cref="WorkerStateModel" /> of type <see cref="TranslationExportStateModel" />.</returns>
        [Obsolete("This method is obsolete. Please use FindTranslationExportState() going forward. This method will be removed in a future release.")]
        public static MinimalWorkerStateModel<TranslationExportStateModel> GetTranslationExportState(this InspireClient client, string workerKey)
        {
            return FindTranslationExportState(client, workerKey);
        }

        /// <summary>
        /// Gets the translation export state.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="workerKey">The worker key.</param>
        /// <returns>Returns a <see cref="WorkerStateModel" /> of type <see cref="TranslationExportStateModel" />.</returns>
        public static MinimalWorkerStateModel<TranslationExportStateModel> FindTranslationExportState(this InspireClient client, string workerKey)
        {
            if (string.IsNullOrEmpty(workerKey))
            {
                throw new ArgumentNullException(nameof(workerKey));
            }

            string encodedWorkerKey = Uri.EscapeUriString(workerKey);
            var request = client.CreateRequest($"/Translations/Export/{encodedWorkerKey}/", HttpMethod.Get);

            return client.RequestContent<MinimalWorkerStateModel<TranslationExportStateModel>>(request);
        }

        /// <summary>
        /// Resets the state of the requested translation components to queued.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="translationComponentIds">The translation component ids to reset state to queued.</param>
        /// <returns>Returns a List of <see cref="MinimalTranslationJobComponentModel" /> objects.</returns>
        public static List<MinimalTranslationJobComponentModel> ResetTranslationComponentsState(this InspireClient client, List<long> translationComponentIds)
        {
            if (translationComponentIds == null)
            {
                throw new ArgumentNullException(nameof(translationComponentIds));
            }

            var request = client.CreateRequest($"/Translations/Components/SetQueued", HttpMethod.Post);

            return client.RequestContent<List<long>, List<MinimalTranslationJobComponentModel>>(request, translationComponentIds);
        }

        /// <summary>
        /// Gets all of the translation export jobs ready to be processed.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="translationJobIds">The translation job ids.</param>
        /// <returns>Returns a List of <see cref="TranslationExportJobModel" /> objects.</returns>
        [Obsolete("This method is obsolete. Please use FindTranslationExportJobs() going forward. This method will be removed in a future release.")]
        public static List<TranslationExportJobModel> GetTranslationExportJobs(this InspireClient client, List<long> translationJobIds)
        {
            return FindTranslationExportJobs(client, translationJobIds);
        }

        /// <summary>
        /// Gets all of the translation export jobs ready to be processed.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="translationJobIds">The translation job ids.</param>
        /// <returns>Returns a List of <see cref="TranslationExportJobModel" /> objects.</returns>
        public static List<TranslationExportJobModel> FindTranslationExportJobs(this InspireClient client, List<long> translationJobIds)
        {
            if (translationJobIds == null)
            {
                throw new ArgumentNullException(nameof(translationJobIds));
            }

            var request = client.CreateRequest($"/Translations/TranslationExportJobModels", HttpMethod.Post);

            return client.RequestContent<List<long>, List<TranslationExportJobModel>>(request, translationJobIds);
        }

        /// <summary>
        /// Sets the translation job statuses for each requested <see cref="TranslationExportJobModel"/>.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="models">The List of <see cref="TranslationExportJobModel"/> objects used to set translation job statuses.</param>
        /// <returns>Returns a List of <see cref="TranslationExportJobModel" /> objects.</returns>
        public static List<TranslationExportJobModel> SetTranslationJobStatus(this InspireClient client, List<TranslationExportJobModel> models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var request = client.CreateRequest($"/Translations/Jobs/SetStatus", HttpMethod.Post);

            return client.RequestContent<List<TranslationExportJobModel>, List<TranslationExportJobModel>>(request, models);
        }

        /// <summary>
        /// Processes the message received from Transport as part of a project delivery or cancellation.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="transportProjectId">The Transport Project Id.</param>
        /// <param name="projectStatus">The status of the project.</param>
        /// <returns>Returns success or failure of the operation.</returns>
        public static bool ProcessTransportResponse(this InspireClient client, string transportProjectId, string projectStatus)
        {
            if (string.IsNullOrEmpty(transportProjectId))
            {
                throw new ArgumentNullException(nameof(transportProjectId));
            }

            HttpWebRequest request;

            if (projectStatus == "ProjectDelivery")
            {
                request = client.CreateRequest($"/Translations/ProcessTransportResponse/{transportProjectId}", HttpMethod.Post);
            }
            else if (projectStatus == "ProjectCancelled")
            {
                request = client.CreateRequest($"/Translations/ProcessTransportCancellation/{transportProjectId}", HttpMethod.Post);
            }
            else
            {
                return false;
            }

            return client.RequestContent<bool>(request);
        }

        /// <summary>
        /// Finds all translation integrations.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="activeOnly">if set to <c>true</c> [active only].</param>
        /// <returns>Returns a <see cref="TranslationIntegrationBrowseResultModel"/> object containing the results of the query.</returns>
        public static TranslationIntegrationBrowseResultModel FindTranslationIntegrations(this InspireClient client, bool activeOnly)
        {
            var request = client.CreateRequest($"/TranslationIntegrations/All/{activeOnly}", HttpMethod.Get);

            return client.RequestContent<TranslationIntegrationBrowseResultModel>(request);
        }

        /// <summary>
        /// Finds the translation integration.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns a <see cref="TranslationIntegrationModel"/> object matching the specified identifier.</returns>
        public static TranslationIntegrationModel FindTranslationIntegration(this InspireClient client, long id)
        {
            var request = client.CreateRequest($"/TranslationIntegrations/{id}", HttpMethod.Get);

            return client.RequestContent<TranslationIntegrationModel>(request);
        }

        /// <summary>
        /// Finds the translation integrations.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="TranslationIntegrationBrowseResultModel"/> object containing the results of the query.</returns>
        /// <exception cref="ArgumentNullException">model</exception>
        public static TranslationIntegrationBrowseResultModel FindTranslationIntegrations(this InspireClient client, TranslationIntegrationBrowseQueryModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/TranslationIntegrations/Query", HttpMethod.Post);

            return client.RequestContent<TranslationIntegrationBrowseQueryModel, TranslationIntegrationBrowseResultModel>(request, model);
        }

        /// <summary>
        /// Updates the translation integration.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="inputModel">The input model.</param>
        /// <returns>Returns the updated <see cref="TranslationIntegrationModel"/> object.</returns>
        /// <exception cref="ArgumentNullException">InputModel cannot be null.</exception>
        public static TranslationIntegrationModel UpdateTranslationIntegration(this InspireClient client, TranslationIntegrationModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/TranslationIntegrations/{inputModel.TranslationIntegrationId}", HttpMethod.Put);

            return client.RequestContent<TranslationIntegrationModel, TranslationIntegrationModel>(request, inputModel);
        }

        /// <summary>
        /// Creates the translation integration.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="inputModel">The input model.</param>
        /// <returns>Returns the new <see cref="TranslationIntegrationModel"/> object.</returns>
        /// <exception cref="ArgumentNullException">InputModel cannot be null.</exception>
        public static TranslationIntegrationModel CreateTranslationIntegration(this InspireClient client, TranslationIntegrationModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException(nameof(inputModel));
            }

            var request = client.CreateRequest($"/TranslationIntegrations", HttpMethod.Post);

            return client.RequestContent<TranslationIntegrationModel, TranslationIntegrationModel>(request, inputModel);
        }

        /// <summary>
        /// Deletes the translation integration.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns [true] if the Translation Integration record has been successfully deleted.</returns>
        public static bool DeleteTranslationIntegration(this InspireClient client, long id)
        {
            var request = client.CreateRequest($"/TranslationIntegrations/{id}", HttpMethod.Delete);

            return client.RequestContent<bool>(request);
        }
    }
}