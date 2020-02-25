//-------------------------------------------------------------
// <copyright file="EditorExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Vasont.Inspire.Models.Components;
    using Vasont.Inspire.SDK.Properties;

    /// <summary>
    /// This extensions class contains methods for supporting and interacting with external-editor related API calls.
    /// </summary>
    public static class EditorExtensions
    {
        #region Editor Related Extension Methods
        /// <summary>
        /// This method is used to create an Inspire Editor record and lock for editing the specified XML component.
        /// </summary>
        /// <param name="client"><see cref="InspireClient"/> used to communication with the API endpoint.</param>
        /// <param name="model">Contains the component load editor model.</param>
        /// <returns>Returns the <see cref="MinimalEditorXmlModel"/> object that represents the editor record created.</returns>
        public static MinimalEditorXmlModel GetEditorComponent(this InspireClient client, EditorLoadModel model)
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
            var request = client.CreateRequest($"/api/Editor?{query}");
            return client.RequestContent<MinimalEditorXmlModel>(request);
        }
        #endregion
    }
}
