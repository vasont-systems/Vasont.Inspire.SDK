//-------------------------------------------------------------
// <copyright file="ConfigurationExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Models.Components.Schema;
    using Models.Configuration;
    using Models.Plugins;
    using Models.Projects;
    using Models.Transfers;
    using Models.Webhooks;
    using Vasont.Inspire.Models.Common;
    using Vasont.Inspire.Models.Components;

    /// <summary>
    /// This class extends the <see cref="InspireClient"/> class to include methods for calling
    /// configuration API endpoints.
    /// </summary>
    public static class ConfigurationExtensions
    {
        #region Public Attribute Methods

        /// <summary>
        /// This method is used to retrieve and return attributes.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="AttributeModel"/> from the application.</returns>
        public static List<AttributeModel> GetAttributes(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/Attributes");
            return client.RequestContent<List<AttributeModel>>(request);
        }

        /// <summary>
        /// This Method used to retrieve and return a specific attribute.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="attributeId">Contains the attribute identity to retrieve metadata for.</param>
        /// <returns>Returns a specific <see cref="AttributeModel"/> from the application.</returns>
        public static AttributeModel GetAttribute(this InspireClient client, long attributeId)
        {
            var request = client.CreateRequest($"/Configurations/Attributes/{attributeId}");
            return client.RequestContent<AttributeModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing attribute.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the attribute model to update.</param>
        /// <returns>Returns the updated <see cref="AttributeModel"/> model from the application.</returns>
        public static AttributeModel UpdateAttribute(this InspireClient client, AttributeModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Attributes/{model.AttributeId}", HttpMethod.Put);
            return client.RequestContent<AttributeModel, AttributeModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new attribute.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the attribute model to create.</param>
        /// <returns>Returns a specific <see cref="AttributeModel"/> that was created.</returns>
        public static AttributeModel CreateAttribute(this InspireClient client, AttributeModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Attributes", HttpMethod.Post);
            return client.RequestContent<AttributeModel, AttributeModel>(request, model);
        }

        /// <summary>
        /// This Method is used to delete the specified attribute.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="attributeId">Contains the attribute identity to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveAttribute(this InspireClient client, long attributeId)
        {
            var request = client.CreateRequest($"/Configurations/Attributes/{attributeId}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public Attribute Methods

        #region Public Profile Attribute Controller Methods

        /// <summary>
        /// This method is used to retrieve a list of minimal profile attributes.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="MinimalProfileAttributeModel"/> from the application.</returns>
        public static List<MinimalProfileAttributeModel> GetProfileAttributes(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/ProfileAttributes");
            return client.RequestContent<List<MinimalProfileAttributeModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a list of profile attribute items.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="ProfileAttributeItemModel"/> from the application.</returns>
        public static List<ProfileAttributeItemModel> GetProfileAttributeItems(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/ProfileAttributes/Selector/Items");
            return client.RequestContent<List<ProfileAttributeItemModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific profile attributes.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="attributeId">Contains the attribute identity to retrieve metadata for.</param>
        /// <returns>Returns a specific <see cref="ProfileAttributeModel"/> from the application.</returns>
        public static ProfileAttributeModel GetProfileAttribute(this InspireClient client, long attributeId)
        {
            var request = client.CreateRequest($"/Configurations/ProfileAttributes/{attributeId}");
            return client.RequestContent<ProfileAttributeModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing profile attribute.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the profile attribute model to update.</param>
        /// <returns>Returns the updated <see cref="ProfileAttributeModel"/> model from the application.</returns>
        public static ProfileAttributeModel UpdateProfileAttribute(this InspireClient client, ProfileAttributeModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/ProfileAttributes/{model.AttributeId}", HttpMethod.Put);
            return client.RequestContent<ProfileAttributeModel, ProfileAttributeModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new profile attribute.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the profile attribute model to create.</param>
        /// <returns>Returns a specific <see cref="ProfileAttributeModel"/> that was created.</returns>
        public static ProfileAttributeModel CreateProfileAttribute(this InspireClient client, ProfileAttributeModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/ProfileAttributes", HttpMethod.Post);
            return client.RequestContent<ProfileAttributeModel, ProfileAttributeModel>(request, model);
        }

        /// <summary>
        /// This Method is used to delete the specified attribute.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="attributeId">Contains the attribute identity to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveProfileAttribute(this InspireClient client, long attributeId)
        {
            var request = client.CreateRequest($"/Configurations/ProfileAttributes/{attributeId}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public Profile Attribute Controller Methods

        #region Public Schema Standard Methods

        /// <summary>
        /// This method is used to retrieve a list of schema standards.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of schema standards from the application.</returns>
        public static List<string> GetSchemaStandards(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/SchemaStandards");
            return client.RequestContent<List<string>>(request);
        }

        #endregion Public Schema Standard Methods

        #region Public Component Type Methods

        /// <summary>
        /// This method is used to retrieve a list of component types.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="ComponentTypeModel"/> from the application.</returns>
        public static List<ComponentTypeModel> GetComponentTypes(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/ComponentTypes");
            return client.RequestContent<List<ComponentTypeModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific component type.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componentTypeId">Contains the component type identity to retrieve metadata for.</param>
        /// <returns>Returns a specific <see cref="ComponentTypeModel"/> from the application.</returns>
        public static ComponentTypeModel GetComponentType(this InspireClient client, long componentTypeId)
        {
            var request = client.CreateRequest($"/Configurations/ComponentTypes/{componentTypeId}");
            return client.RequestContent<ComponentTypeModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing component type.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the component type model to update.</param>
        /// <returns>Returns the updated <see cref="ComponentTypeModel"/> model from the application.</returns>
        public static ComponentTypeModel UpdateComponentType(this InspireClient client, ComponentTypeModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/ComponentTypes/{model.ComponentTypeId}", HttpMethod.Put);
            return client.RequestContent<ComponentTypeModel, ComponentTypeModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new component type.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the component type model to create.</param>
        /// <returns>Returns a specific <see cref="ComponentTypeModel"/> that was created.</returns>
        public static ComponentTypeModel CreateComponentType(this InspireClient client, ComponentTypeModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/ComponentTypes", HttpMethod.Post);
            return client.RequestContent<ComponentTypeModel, ComponentTypeModel>(request, model);
        }

        /// <summary>
        /// This Method is used to delete the specified component type.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componentTypeId">Contains the component type identity to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveComponentType(this InspireClient client, long componentTypeId)
        {
            var request = client.CreateRequest($"/Configurations/ComponentTypes/{componentTypeId}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public Component Type Methods

        #region Public Element Methods

        /// <summary>
        /// This method is used to retrieve a list of elements.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="ElementModel"/> from the application.</returns>
        public static List<ElementModel> GetElements(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/Elements");
            return client.RequestContent<List<ElementModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific element.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="elementId">Contains the record identity to retrieve.</param>
        /// <returns>Returns a specific <see cref="ElementModel"/> from the application.</returns>
        public static ElementModel GetElement(this InspireClient client, long elementId)
        {
            var request = client.CreateRequest($"/Configurations/Elements/{elementId}");
            return client.RequestContent<ElementModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing element model.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the element model to update.</param>
        /// <returns>Returns the updated <see cref="ElementModel"/> model from the application.</returns>
        public static ElementModel UpdateElement(this InspireClient client, ElementModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Elements/{model.ElementId}", HttpMethod.Put);
            return client.RequestContent<ElementModel, ElementModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new element.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the element model to create.</param>
        /// <returns>Returns a specific <see cref="ElementModel"/> that was created.</returns>
        public static ElementModel CreateElement(this InspireClient client, ElementModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Elements", HttpMethod.Post);
            return client.RequestContent<ElementModel, ElementModel>(request, model);
        }

        /// <summary>
        /// This Method is used to delete the specified attribute.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="elementId">Contains the element identity to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveElement(this InspireClient client, long elementId)
        {
            var request = client.CreateRequest($"/Configurations/Elements/{elementId}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public Element Methods

        #region Public Export Methods

        /// <summary>
        /// This method is used to retrieve a list of exports.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="ExportConfigurationModel"/> objects from the application.</returns>
        public static List<ExportConfigurationModel> GetExports(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/Exports");
            return client.RequestContent<List<ExportConfigurationModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific export.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="exportId">Contains the record identity to retrieve.</param>
        /// <returns>Returns a specific <see cref="ExportConfigurationModel"/> from the application.</returns>
        public static ExportConfigurationModel GetExport(this InspireClient client, long exportId)
        {
            var request = client.CreateRequest($"/Configurations/Exports/{exportId}");
            return client.RequestContent<ExportConfigurationModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing export model.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the export model to update.</param>
        /// <returns>Returns the updated <see cref="ExportConfigurationModel"/> model from the application.</returns>
        public static ExportConfigurationModel UpdateExport(this InspireClient client, ExportConfigurationModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Exports/{model.ExportId}", HttpMethod.Put);
            return client.RequestContent<ExportConfigurationModel, ExportConfigurationModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new export.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the <see cref="ExportConfigurationModel"/> to create.</param>
        /// <returns>Returns the new <see cref="ExportConfigurationModel"/> that was created.</returns>
        public static ExportConfigurationModel CreateExport(this InspireClient client, ExportConfigurationModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Exports", HttpMethod.Post);
            return client.RequestContent<ExportConfigurationModel, ExportConfigurationModel>(request, model);
        }

        /// <summary>
        /// This Method is used to delete a specified export.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="exportId">Contains the export identity to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveExport(this InspireClient client, long exportId)
        {
            var request = client.CreateRequest($"/Configurations/Exports/{exportId}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        /// <summary>
        /// This method is used to retrieve a list of export plugins.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="PluginModel"/> objects from the application.</returns>
        public static List<PluginModel> GetExportPlugins(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/Exports/Plugins");
            return client.RequestContent<List<PluginModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a list of export plugin parameters for a specified plugin.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="pluginId">Contains the plugin identity to retrieve metadata for.</param>
        /// <returns>Returns a list of <see cref="PluginParameterModel"/> objects from the application.</returns>
        public static List<PluginParameterModel> GetExportPluginParameters(this InspireClient client, long pluginId)
        {
            var request = client.CreateRequest($"/Configurations/Exports/Plugins/{pluginId}/Parameters");
            return client.RequestContent<List<PluginParameterModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific export component type selection model.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="componentTypeId">Contains the specific export component type.</param>
        /// <param name="exportType">Contains the <see cref="ExportType"/> type that will be retrieved, Standard is default.</param>
        /// <returns>Returns a list of <see cref="ExportSelectionModel"/> objects from the application.</returns>
        public static ExportSelectionModel GetExportComponentType(this InspireClient client, long componentTypeId, ExportType exportType = ExportType.Standard)
        {
            var request = client.CreateRequest($"/Configurations/Exports/ComponentTypes/{componentTypeId}?exportType={exportType}");
            return client.RequestContent<ExportSelectionModel>(request);
        }

        #endregion Public Export Methods

        #region Public Link Method Methods

        /// <summary>
        /// This method is used to retrieve a list of link methods.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="LinkMethodModel"/> from the application.</returns>
        public static List<LinkMethodModel> GetLinkMethods(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/LinkMethods");
            return client.RequestContent<List<LinkMethodModel>>(request);
        }

        #endregion Public Link Method Methods

        #region Public Xml Link Type Methods

        /// <summary>
        /// This method is used to retrieve a list of XML link types.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="XmlLinkTypeModel"/> from the application.</returns>
        public static List<XmlLinkTypeModel> GetXmlLinkTypes(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/XmlLinkTypes");
            return client.RequestContent<List<XmlLinkTypeModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific XML link type.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="xmlLinkTypeId">Contains the record identity to retrieve.</param>
        /// <returns>Returns a specific <see cref="XmlLinkTypeModel"/> from the application.</returns>
        public static XmlLinkTypeModel GetXmlLinkType(this InspireClient client, long xmlLinkTypeId)
        {
            var request = client.CreateRequest($"/Configurations/XmlLinkTypes/{xmlLinkTypeId}");
            return client.RequestContent<XmlLinkTypeModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing XML link type model.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to update.</param>
        /// <returns>Returns the updated <see cref="XmlLinkTypeModel"/> model from the application.</returns>
        public static XmlLinkTypeModel UpdateXmlLinkType(this InspireClient client, XmlLinkTypeModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/XmlLinkTypes/{model.XmlLinkTypeId}", HttpMethod.Put);
            return client.RequestContent<XmlLinkTypeModel, XmlLinkTypeModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new XML Link type.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to create.</param>
        /// <returns>Returns a specific <see cref="XmlLinkTypeModel"/> that was created.</returns>
        public static XmlLinkTypeModel CreateXmlLinkType(this InspireClient client, XmlLinkTypeModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/XmlLinkTypes", HttpMethod.Post);
            return client.RequestContent<XmlLinkTypeModel, XmlLinkTypeModel>(request, model);
        }

        /// <summary>
        /// This Method is used to remove the specified XML link type.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="xmlLinkTypeId">Contains the identity of record to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveXmlLinkType(this InspireClient client, long xmlLinkTypeId)
        {
            var request = client.CreateRequest($"/Configurations/XmlLinkTypes/{xmlLinkTypeId}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public Xml Link Type Methods

        #region Public Relation Methods

        /// <summary>
        /// This method is used to retrieve a list of Relation.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="RelationModel"/> from the application.</returns>
        public static List<RelationModel> GetRelations(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/Relations");
            return client.RequestContent<List<RelationModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific Relation.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="relationId">Contains the record identity to retrieve.</param>
        /// <returns>Returns a specific <see cref="RelationModel"/> from the application.</returns>
        public static RelationModel GetRelation(this InspireClient client, long relationId)
        {
            var request = client.CreateRequest($"/Configurations/Relations/{relationId}");
            return client.RequestContent<RelationModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing relation model.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to update.</param>
        /// <returns>Returns the updated <see cref="RelationModel"/> model from the application.</returns>
        public static RelationModel UpdateRelation(this InspireClient client, RelationModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Relations/{model.RelationId}", HttpMethod.Put);
            return client.RequestContent<RelationModel, RelationModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new relation.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to create.</param>
        /// <returns>Returns a specific <see cref="RelationModel"/> that was created.</returns>
        public static RelationModel CreateRelation(this InspireClient client, RelationModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Relations", HttpMethod.Post);
            return client.RequestContent<RelationModel, RelationModel>(request, model);
        }

        /// <summary>
        /// This Method is used to remove the specified relation.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="relationId">Contains the identity of record to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveRelation(this InspireClient client, long relationId)
        {
            var request = client.CreateRequest($"/Configurations/Relations/{relationId}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public Relation Methods

        #region Public Tag Methods
        /// <summary>
        /// This method is used to retrieve a specific tag.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="tagId">Contains the record identity to retrieve.</param>
        /// <returns>Returns a specific <see cref="TagModel"/> from the application.</returns>
        public static TagModel GetTag(this InspireClient client, long tagId)
        {
            var request = client.CreateRequest($"/Configurations/Tags/{tagId}");
            return client.RequestContent<TagModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing tag.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to update.</param>
        /// <returns>Returns the updated <see cref="TagModel"/> model from the application.</returns>
        public static TagModel UpdateTag(this InspireClient client, TagModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Tags/{model.TagId}", HttpMethod.Put);
            return client.RequestContent<TagModel, TagModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new tag.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to create.</param>
        /// <returns>Returns a specific <see cref="TagModel"/> that was created.</returns>
        public static TagModel CreateTag(this InspireClient client, TagModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Tags", HttpMethod.Post);
            return client.RequestContent<TagModel, TagModel>(request, model);
        }

        /// <summary>
        /// This Method is used to remove the specified tag.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="tagId">Contains the identity of record to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveTag(this InspireClient client, long tagId)
        {
            var request = client.CreateRequest($"/Configurations/Tags/{tagId}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public Tag Methods

        #region Public Project Activity Controller Methods

        /// <summary>
        /// This method is used to retrieve a list of project activities.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="ProjectActivityModel"/> from the application.</returns>
        public static List<ProjectActivityModel> GetProjectActivities(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/ProjectActivities");
            return client.RequestContent<List<ProjectActivityModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific project activity.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectActivityId">Contains the record identity to retrieve.</param>
        /// <returns>Returns a specific <see cref="ProjectActivityModel"/> from the application.</returns>
        public static ProjectActivityModel GetProjectActivity(this InspireClient client, long projectActivityId)
        {
            var request = client.CreateRequest($"/Configurations/ProjectActivities/{projectActivityId}");
            return client.RequestContent<ProjectActivityModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing project activity.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to update.</param>
        /// <returns>Returns the updated <see cref="ProjectActivityModel"/> model from the application.</returns>
        public static ProjectActivityModel UpdateProjectActivity(this InspireClient client, ProjectActivityModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/ProjectActivities/{model.ProjectActivityId}", HttpMethod.Put);
            return client.RequestContent<ProjectActivityModel, ProjectActivityModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new project activity.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to create.</param>
        /// <returns>Returns a specific <see cref="ProjectActivityModel"/> that was created.</returns>
        public static ProjectActivityModel CreateProjectActivity(this InspireClient client, ProjectActivityModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/ProjectActivities", HttpMethod.Post);
            return client.RequestContent<ProjectActivityModel, ProjectActivityModel>(request, model);
        }

        /// <summary>
        /// This Method is used to remove the specified project activity.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="projectActivityId">Contains the identity of record to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveProjectActivity(this InspireClient client, long projectActivityId)
        {
            var request = client.CreateRequest($"/Configurations/ProjectActivities/{projectActivityId}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public Project Activity Controller Methods

        #region Public Languages Methods
        /// <summary>
        /// This method is used to retrieve a list of languages.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="dictionaryOnly">Contains a value indicating whether the languages that are for editor dictionary only are returned.</param>
        /// <returns>Returns a list of <see cref="LanguageModel"/> from the application.</returns>
        public static List<LanguageModel> GetLanguages(this InspireClient client, bool dictionaryOnly = false)
        {
            var request = client.CreateRequest($"/Languages?dictionaryOnly={dictionaryOnly}");
            return client.RequestContent<List<LanguageModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific language.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="languageCode">Contains the record identity to retrieve.</param>
        /// <returns>Returns a specific <see cref="LanguageModel"/> from the application.</returns>
        public static LanguageModel GetLanguage(this InspireClient client, string languageCode)
        {
            var request = client.CreateRequest($"/Configurations/Languages/{languageCode}");
            return client.RequestContent<LanguageModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing language.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to update.</param>
        /// <returns>Returns the updated <see cref="LanguageModel"/> model from the application.</returns>
        public static LanguageModel UpdateLanguage(this InspireClient client, LanguageModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Languages/{model.LanguageCode}", HttpMethod.Put);
            return client.RequestContent<LanguageModel, LanguageModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new language.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to create.</param>
        /// <returns>Returns a specific <see cref="LanguageModel"/> that was created.</returns>
        public static LanguageModel CreateLanguage(this InspireClient client, LanguageModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/Languages", HttpMethod.Post);
            return client.RequestContent<LanguageModel, LanguageModel>(request, model);
        }

        /// <summary>
        /// This Method is used to remove the specified language.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="languageCode">Contains the identity of record to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveLanguage(this InspireClient client, string languageCode)
        {
            var request = client.CreateRequest($"/Configurations/Languages/{languageCode}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public Languages Methods

        #region Public Webhook Configurations Methods

        /// <summary>
        /// This method is used to retrieve a list of web hook configurations.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="WebhookConfigurationModel"/> from the application.</returns>
        public static List<WebhookConfigurationModel> GetWebHookConfigurations(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/WebhookConfigurations");
            return client.RequestContent<List<WebhookConfigurationModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific web hook configuration.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="webHookConfigurationId">Contains the record identity to retrieve.</param>
        /// <returns>Returns a specific <see cref="WebhookConfigurationModel"/> from the application.</returns>
        public static WebhookConfigurationModel GetWebHookConfiguration(this InspireClient client, long webHookConfigurationId)
        {
            var request = client.CreateRequest($"/Configurations/WebhookConfigurations/{webHookConfigurationId}");
            return client.RequestContent<WebhookConfigurationModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing web hook configuration.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to update.</param>
        /// <returns>Returns the updated <see cref="WebhookConfigurationModel"/> model from the application.</returns>
        public static WebhookConfigurationModel UpdateWebHookConfiguration(this InspireClient client, WebhookConfigurationModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/WebhookConfigurations/{model.Name}", HttpMethod.Put);
            return client.RequestContent<WebhookConfigurationModel, WebhookConfigurationModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new web hook configuration.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to create.</param>
        /// <returns>Returns a specific <see cref="WebhookConfigurationModel"/> that was created.</returns>
        public static WebhookConfigurationModel CreateWebHookConfiguration(this InspireClient client, WebhookConfigurationModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/WebhookConfigurations", HttpMethod.Post);
            return client.RequestContent<WebhookConfigurationModel, WebhookConfigurationModel>(request, model);
        }

        /// <summary>
        /// This Method is used to remove the specified web hook configuration.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="name">Contains the identity of record to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveWebHookConfiguration(this InspireClient client, string name)
        {
            var request = client.CreateRequest($"/Configurations/WebhookConfigurations/{name}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public Webhook Configurations Methods

        #region Public File Extension Methods

        /// <summary>
        /// This method is used to retrieve a list of file extensions.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <returns>Returns a list of <see cref="FileExtensionModel"/> from the application.</returns>
        public static List<FileExtensionModel> GetFileExtensions(this InspireClient client)
        {
            var request = client.CreateRequest($"/Configurations/FileExtensions");
            return client.RequestContent<List<FileExtensionModel>>(request);
        }

        /// <summary>
        /// This method is used to retrieve a specific file extension configuration.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="extensionId">Contains the record identity to retrieve.</param>
        /// <returns>Returns a specific <see cref="FileExtensionModel"/> from the application.</returns>
        public static FileExtensionModel GetFileExtension(this InspireClient client, long extensionId)
        {
            var request = client.CreateRequest($"/Configurations/FileExtensions/{extensionId}");
            return client.RequestContent<FileExtensionModel>(request);
        }

        /// <summary>
        /// This method is used to update an existing file extension.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to update.</param>
        /// <returns>Returns the updated <see cref="FileExtensionModel"/> model from the application.</returns>
        public static FileExtensionModel UpdateFileExtension(this InspireClient client, FileExtensionModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/FileExtensions/{model.FileExtensionId}", HttpMethod.Put);
            return client.RequestContent<FileExtensionModel, FileExtensionModel>(request, model);
        }

        /// <summary>
        /// This Method is used to create a new file extension blacklist configuration.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="model">Contains the model to create.</param>
        /// <returns>Returns a specific <see cref="FileExtensionModel"/> that was created.</returns>
        public static FileExtensionModel CreateFileExtension(this InspireClient client, FileExtensionModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var request = client.CreateRequest($"/Configurations/FileExtensions", HttpMethod.Post);
            return client.RequestContent<FileExtensionModel, FileExtensionModel>(request, model);
        }

        /// <summary>
        /// This Method is used to remove the specified file extension configuration.
        /// </summary>
        /// <param name="client">Contains the <see cref="InspireClient"/> that is used for communication.</param>
        /// <param name="extensionId">Contains the identity of record to delete.</param>
        /// <returns>Returns a boolean value indicating whether the action succeeded or not.</returns>
        public static bool RemoveFileExtension(this InspireClient client, long extensionId)
        {
            var request = client.CreateRequest($"/Configurations/FileExtensions/{extensionId}", HttpMethod.Delete);
            client.RequestContent(request);
            return !client.HasError;
        }

        #endregion Public File Extension Methods
    }
}