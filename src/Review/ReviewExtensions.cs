//-------------------------------------------------------------
// <copyright file="ReviewExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Review
{
    using System.Collections.Generic;
    using System.Net.Http;
    using Vasont.Inspire.Models.Reviews;
    using Vasont.Inspire.Models.Security;
    using Vasont.Inspire.Models.Workflow;

    /// <summary>
    /// This class contains extension methods for calling component review related API endpoints.
    /// </summary>
    public static class ReviewExtensions
    {
        /// <summary>
        /// This method is used to retrieve a specific review model.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="reviewId">Contains the unique identity of the review.</param>
        /// <returns>Returns a <see cref="ReviewModel"/> model if found.</returns>
        public static ReviewModel GetReview(this InspireClient client, long reviewId)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{reviewId}");
            return client.RequestContent<ReviewModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific edit review model.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="reviewId">Contains the unique identity of the review.</param>
        /// <returns>Returns a <see cref="ReviewerEditModel"/> model if found.</returns>
        public static ReviewerEditModel GetReviewEdit(this InspireClient client, long reviewId)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{reviewId}/Edit");
            return client.RequestContent<ReviewerEditModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific detailed review model.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="reviewId">Contains the unique identity of the review.</param>
        /// <returns>Returns a <see cref="ReviewDetailModel"/> model if found.</returns>
        public static ReviewDetailModel GetReviewDetail(this InspireClient client, long reviewId)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{reviewId}/Detail");
            return client.RequestContent<ReviewDetailModel>(request);
        }

        /// <summary>
        /// This method is used to retrieve a reviews browse result.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="model">Contains the model for review browsing.</param>
        /// <returns>Returns a list of <see cref="ReviewBrowseResultModel"/> objects.</returns>
        public static ReviewBrowseResultModel GetReviews(this InspireClient client, ReviewBrowseQueryModel model)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/Browse", HttpMethod.Post);
            return client.RequestContent<ReviewBrowseQueryModel, ReviewBrowseResultModel>(request, model);
        }

        /// <summary>
        /// This method is used to retrieve a list of associated review components for the specified review.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="reviewId">Contains the review identity.</param>
        /// <returns>Returns a list of <see cref="ReviewComponentModel"/> objects.</returns>
        public static List<ReviewComponentModel> GetReviewComponents(this InspireClient client, long reviewId)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{reviewId}/Components", HttpMethod.Get);
            return client.RequestContent<List<ReviewComponentModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific review component specified.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="reviewId">Contains the review identity.</param>
        /// <param name="reviewComponentId">Contains the review component identity to retrieve.</param>
        /// <returns>Returns the review component <see cref="ReviewComponentModel"/> object.</returns>
        public static ReviewComponentModel GetReviewComponent(this InspireClient client, long reviewId, long reviewComponentId)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{reviewId}/Components/{reviewComponentId}", HttpMethod.Get);
            return client.RequestContent<ReviewComponentModel>(request);
        }

        /// <summary>
        /// This method is used to return active reviews within the system that are associated with the user.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <returns>Returns a list of <see cref="ReviewAssignmentModel"/> models if found.</returns>
        public static List<ReviewAssignmentModel> GetUserReviewAssignments(this InspireClient client)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/Assignments");
            return client.RequestContent<List<ReviewAssignmentModel>>(request);
        }

        /// <summary>
        /// This REST endpoint retrieves the users that have the permissions to create reviews.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <returns>Returns a <see cref="ReviewDetailModel"/> model if found.</returns>
        public static List<MinimalUserModel> GetReviewCoordinators(this InspireClient client)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/Coordinators");
            return client.RequestContent<List<MinimalUserModel>>(request);
        }

        /// <summary>
        /// This method is used to update the review.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="model">Contains the review model to update.</param>
        /// <returns>Returns a list of <see cref="ReviewModel"/> objects.</returns>
        public static ReviewModel UpdateReview(this InspireClient client, ReviewModel model)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{model.ReviewId}", HttpMethod.Put);
            return client.RequestContent<ReviewModel, ReviewModel>(request, model);
        }

        /// <summary>
        /// This method is used to return review components within a review that have been updated after a specified date.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="model">Contains the changed components query model.</param>
        /// <returns>Returns a list of <see cref="WorkflowTemplateModel"/> objects.</returns>
        public static List<ReviewerComponentModel> UpdateReviewComponents(this InspireClient client, ReviewChangedComponentQueryModel model)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{model.ReviewId}/Changed", HttpMethod.Post);
            return client.RequestContent<ReviewChangedComponentQueryModel, List<ReviewerComponentModel>>(request, model);
        }

        /// <summary>
        /// This REST method is used to update a review component states.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="reviewId">Contains the identity of the review to update.</param>
        /// <param name="reviewComponentId">Contains the identity of the review component.</param>
        /// <param name="completed">Contains a value indicating whether the review component should be completed.</param>
        /// <returns>Returns a list of <see cref="WorkflowTemplateModel"/> objects.</returns>
        public static ReviewerComponentModel UpdateReviewComponentState(this InspireClient client, long reviewId, long reviewComponentId, bool completed = true)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{reviewId}/Components/{reviewComponentId}/State?completed={completed}", HttpMethod.Put);
            return client.RequestContent<ReviewerComponentModel>(request);
        }

        /// <summary>
        /// This method is used to create a new review.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="model">Contains the review model to create the new review.</param>
        /// <returns>Returns a list of <see cref="WorkflowTemplateModel"/> objects.</returns>
        public static ReviewModel CreateReview(this InspireClient client, ReviewModel model)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews", HttpMethod.Post);
            return client.RequestContent<ReviewModel, ReviewModel>(request, model);
        }

        /// <summary>
        /// This method is used to complete a reviewer session.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="model">Contains the review model to create the new review.</param>
        /// <returns>Returns a list of <see cref="WorkflowTemplateModel"/> objects.</returns>
        public static ReviewModel CompleteReviewerSession(this InspireClient client, ReviewModel model)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{model.ReviewId}/CompleteReviewing", HttpMethod.Put);
            return client.RequestContent<ReviewModel, ReviewModel>(request, model);
        }

        /// <summary>
        /// This method is used to complete a review.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="model">Contains the review model to create the new review.</param>
        /// <returns>Returns a list of <see cref="WorkflowTemplateModel"/> objects.</returns>
        public static ReviewModel CompleteReview(this InspireClient client, ReviewModel model)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{model.ReviewId}/Complete", HttpMethod.Put);
            return client.RequestContent<ReviewModel, ReviewModel>(request, model);
        }

        /// <summary>
        /// This method is used to cancel a review.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="reviewId">Contains the identity of the review.</param>
        /// <param name="model">Contains the review model to create the new review.</param>
        /// <returns>Returns a list of <see cref="WorkflowTemplateModel"/> objects.</returns>
        public static bool CancelReview(this InspireClient client, long reviewId, ReviewCancellationModel model)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{reviewId}/Cancel", HttpMethod.Put);
            return client.RequestContent<ReviewCancellationModel, bool>(request, model);
        }

        /// <summary>
        /// This method is used to delete a review.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="reviewId">Contains the identity of the review.</param>
        /// <param name="model">Contains the <see cref="ReviewCancellationModel"/> to be used to cancel the review.</param>
        /// <returns>Returns a list of <see cref="WorkflowTemplateModel"/> objects.</returns>
        public static bool DeleteReview(this InspireClient client, long reviewId, ReviewCancellationModel model)
        {
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Reviews/{reviewId}", HttpMethod.Delete);
            return client.RequestContent<ReviewCancellationModel, bool>(request, model);
        }
    }
}
