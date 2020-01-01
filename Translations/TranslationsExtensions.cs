//-------------------------------------------------------------
// <copyright file="TranslationsExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Translations
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Vasont.Inspire.Models.Translations;
    using Vasont.Inspire.Models.Worker;

    /// <summary>
    /// This class extends the <see cref="InspireClient"/> class to include methods for calling 
    /// user API endpoints.
    /// </summary>
    public static class TranslationsExtensions
    {
        /// <summary>
        /// Gets all translation vendors.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>Returns a List of <see cref="TranslationVendorModel"/> objects.</returns>
        public static List<TranslationVendorModel> GetTranslationVendors(this InspireClient client)
        {
            var request = client.CreateRequest("/api/Translations/Vendors");
            return client.RequestContent<List<TranslationVendorModel>>(request);
        }

        /// <summary>
        /// Gets the queued translation jobs.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="translationVendorId">The translation vendor identifier.</param>
        /// <returns>Returns a <see cref="WorkerStateModel" /> of type <see cref="TranslationExportStateModel" />.</returns>
        public static WorkerStateModel<TranslationExportStateModel> GetQueuedTranslationJobs(this InspireClient client, long translationVendorId)
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

            var request = client.CreateRequest($"/api/Translations/Export/GetQueued/{translationVendorId}", HttpMethod.Post);

            return client.RequestContent<TranslationExportRequestModel, WorkerStateModel<TranslationExportStateModel>>(request, translationExportRequestModel);
        }

        /// <summary>
        /// Gets the translation export state.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="workerKey">The worker key.</param>
        /// <returns>
        /// Returns a <see cref="WorkerStateModel" /> of type <see cref="TranslationExportStateModel" />.
        /// </returns>
        public static WorkerStateModel<TranslationExportStateModel> GetTranslationExportState(this InspireClient client, string workerKey)
        {
            if (string.IsNullOrEmpty(workerKey))
            {
                throw new ArgumentNullException(nameof(workerKey));
            }

            string encodedWorkerKey = Uri.EscapeUriString(workerKey);
            var request = client.CreateRequest($"/api/Translations/Export/{encodedWorkerKey}/", HttpMethod.Get);

            return client.RequestContent<WorkerStateModel<TranslationExportStateModel>>(request);
        }

        /// <summary>
        /// Resets the state of the translation components to queued.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="translationComponentIds">The translation component ids to reset state to queued.</param>
        /// <returns>Returns a List of <see cref="MinimalTranslationComponentModel"/> objects.</returns>
        public static List<MinimalTranslationJobComponentModel> ResetTranslationComponentsState(this InspireClient client, List<long> translationComponentIds)
        {
            if (translationComponentIds == null)
            {
                throw new ArgumentNullException(nameof(translationComponentIds));
            }

            var request = client.CreateRequest("/api/Translations/Components/SetQueued", HttpMethod.Post);

            return client.RequestContent<List<long>, List<MinimalTranslationJobComponentModel>>(request, translationComponentIds);
        }
    }
}
