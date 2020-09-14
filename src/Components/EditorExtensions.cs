//-------------------------------------------------------------
// <copyright file="EditorExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Components
{
    using System;
    using System.Net;
    using Vasont.Inspire.Models.Components;
    using Vasont.Inspire.SDK.Properties;

    /// <summary>
    /// This extensions class contains methods for supporting and interacting with external-editor related API calls.
    /// </summary>
    public static class EditorExtensions
    {
        #region Editor Related Extension Methods
        /// <summary>
        /// This method is used to retrieve the specified Editor Xml Model.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="model">Contains the component load editor model.</param>
        /// <returns>Returns the <see cref="MinimalEditorXmlModel"/> object that represents the editor record created.</returns>
        [Obsolete("This method is obsolete. Please use FindEditorComponent() going forward. This method will be removed in a future release.")]
        public static MinimalEditorXmlModel GetEditorComponent(this InspireClient client, EditorLoadModel model)
        {
            return FindEditorComponent(client, model);
        }

        /// <summary>
        /// This method is used to retrieve the specified Editor Xml Model.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="model">Contains the component load editor model.</param>
        /// <returns>Returns the <see cref="MinimalEditorXmlModel"/> object that represents the editor record created.</returns>
        public static MinimalEditorXmlModel FindEditorComponent(this InspireClient client, EditorLoadModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.ComponentId == 0 && string.IsNullOrWhiteSpace(model.Href))
            {
                throw new ArgumentException(Resources.InvalidEditorRequestMissingComponentErrorText, nameof(model));
            }

            string queryTemplate = "&editorMode={0}&editorType={1}&schema={2}&version={3}&mapReferenceId={4}";
            string query = string.Format(queryTemplate,
                model.EditorMode,
                model.EditorType,
                WebUtility.UrlEncode(model.SchemaType),
                WebUtility.UrlEncode(model.Version),
                model.MapReferenceId);

            if (model.ResolveReferences)
            {
                query += "&resolveReferences=true";
            }

            if (model.ChangesetId != Guid.Empty)
            {
                query += "&changesetId=" + model.ChangesetId.ToString();
            }

            if (model.EditorMode == EditorMode.Review)
            {
                query += "&reviewMode=" + model.ReviewMode.ToString();
            }

            query = (model.ComponentId > 0 ? "componentId=" + model.ComponentId.ToString() : "href=" + model.Href) + query;
            var request = client.CreateRequest($"{client.Config.RoutePrefix}/Editor?{query}");
            return client.RequestContent<MinimalEditorXmlModel>(request);
        }
        #endregion
    }
}
