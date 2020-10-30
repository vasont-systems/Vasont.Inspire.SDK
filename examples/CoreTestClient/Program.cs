//-------------------------------------------------------------
// <copyright file="Program.cs" company="Vasont Systems">
// Copyright (c) 2019 Vasont Systems. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace CoreTestClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Versioning;
    using System.Text;
    using System.Threading;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Newtonsoft.Json;
    using Vasont.Inspire.Core.Extensions;
    using Vasont.Inspire.Core.Utility;
    using Vasont.Inspire.Models.Transfers;
    using Vasont.Inspire.Models.Translations;
    using Vasont.Inspire.Models.Worker;
    using Vasont.Inspire.SDK;
    using Vasont.Inspire.SDK.Application;
    using Vasont.Inspire.SDK.Components;
    using Vasont.Inspire.SDK.Folders;
    using Vasont.Inspire.SDK.Projects;
    using Vasont.Inspire.SDK.Security;
    using Vasont.Inspire.SDK.Translations;

    /// <summary>
    /// This class is the main program for the example.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// This is the main entry point of the application.
        /// </summary>
        /// <param name="args">Contains an array of command line arguments.</param>
        public static void Main(string[] args)
        {
            string resourceUrl = CommandLine.Parameters.ContainsKey("resourceUrl") ? CommandLine.Parameters["resourceUrl"].ConvertToString() : string.Empty;
            string authorityUri = CommandLine.Parameters.ContainsKey("authorityUri") ? CommandLine.Parameters["authorityUri"].ConvertToString() : string.Empty;
            string userName = CommandLine.Parameters.ContainsKey("userName") ? CommandLine.Parameters["userName"].ConvertToString() : string.Empty;
            string password = CommandLine.Parameters.ContainsKey("password") ? CommandLine.Parameters["password"].ConvertToString() : string.Empty;
            string clientSecret = CommandLine.Parameters.ContainsKey("clientSecret") ? CommandLine.Parameters["clientSecret"].ConvertToString() : string.Empty;
            string clientId = CommandLine.Parameters.ContainsKey("clientId") ? CommandLine.Parameters["clientId"].ConvertToString() : string.Empty;
            string zipFile = CommandLine.Parameters.ContainsKey("zipfile") ? CommandLine.Parameters["zipfile"].ConvertToString() : string.Empty;
            long folderId = CommandLine.Parameters.ContainsKey("folderid") ? CommandLine.Parameters["folderid"].ToLong() : 0;
            long componentId = CommandLine.Parameters.ContainsKey("componentid") ? CommandLine.Parameters["componentid"].ToLong() : 0;
            string version = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;

            Console.WriteLine($"Inspire SDK Test Client - Target Framework {version}");
            Console.WriteLine($"resourceUrl: {resourceUrl}");
            Console.WriteLine($"authorityUri: {authorityUri}");
            Console.WriteLine($"userName: {userName}");
            Console.WriteLine($"password: {password}");
            Console.WriteLine($"clientSecret: {clientSecret}");
            Console.WriteLine($"clientId: {clientId}");
            Console.WriteLine($"zipFile: {zipFile}");
            Console.WriteLine($"folderId: {folderId}");
            Console.WriteLine($"componentId: {componentId}");
            // create a new inspire client configuration
            InspireClientConfiguration config = new InspireClientConfiguration(
                clientId,
                ClientAuthenticationMethods.ResourceOwnerPassword,
                authorityUri,
                resourceUrl,
                clientSecret,
                userName,
                password);

            if (componentId > 0)
            {
                // Test GetComponentContent
                using (InspireClient client = new InspireClient(config))
                {
                    if (AsyncHelper.RunSync(() => client.AuthenticateAsync()))
                    {
                        byte[] bytes = client.GetComponentContent(componentId);

                        UTF8Encoding utf8 = new UTF8Encoding(true, true);
                        Console.WriteLine(utf8.GetString(bytes, 0, bytes.Length));
                    }
                }
            }

            if (File.Exists(zipFile))
            {
                Console.WriteLine($"Attempt to import the file \"{zipFile}\" to folder id: {folderId}");

                using (InspireClient client = new InspireClient(config))
                {
                    if (AsyncHelper.RunSync(() => client.AuthenticateAsync()))
                    {
                        FileInfo zipFileInfo = new FileInfo(zipFile);
                        List<IFormFile> formFiles = new List<IFormFile>();
                        List<ImportRequestFileModel> importFiles = new List<ImportRequestFileModel>();

                        ImportRequestFileModel file = new ImportRequestFileModel();
                        file.FileName = zipFileInfo.Name;
                        file.UnzipFile = true;
                        importFiles.Add(file);

                        using (var stream = File.OpenRead(zipFile))
                        {
                            string fileName = Path.GetFileName(stream.Name);
                            string formFieldName = "Files";

                            formFiles.Add(
                                new FormFile(stream, 0, stream.Length, formFieldName, fileName)
                                {
                                    Headers = new HeaderDictionary(),
                                    ContentDisposition = $"form-data; name=\"{formFieldName}\"; filename=\"{fileName}\"",
                                    ContentType = "application/octet-stream"
                                });
                        }

                        ImportRequestModel model = new ImportRequestModel
                        {
                            Files = formFiles,
                            ImportFiles = importFiles,
                            FolderId = 0,
                            ProjectFolderId = 0
                        };

                        List<string> filePaths = new List<string>();
                        filePaths.Add(zipFile);

                        MinimalWorkerStateModel<MinimalImportStateModel> importState = client.ImportComponents(model, filePaths);

                        WorkerStatus status = importState.Status;
                        while (status != WorkerStatus.Complete && status != WorkerStatus.Failed)
                        {
                            MinimalWorkerStateModel<MinimalImportStateModel> importState2 = client.FindImportState(importState.Key);
                            status = importState2.Status;
                            Console.WriteLine(" status: {0}, {1}", status, importState2.Message);
                            Thread.Sleep(10000);
                        }

                        Console.WriteLine("Done");
                    }
                }
            }
            else
            {
                using (InspireClient client = new InspireClient(config))
                {
                    if (AsyncHelper.RunSync(() => client.AuthenticateAsync()))
                    {
                        Console.WriteLine("Attempt to test Import with No Files");
                        ImportRequestModel model = new ImportRequestModel
                        {
                            Files = new List<IFormFile>(),
                            ImportFiles = new List<ImportRequestFileModel>(),
                            FolderId = 0,
                            ProjectFolderId = 0
                        };

                        MinimalWorkerStateModel<MinimalImportStateModel> importState = client.ImportComponents(model, new List<string>());

                        if (importState == null)
                        {
                            var errorModel = client.LastErrorResponse;
                            if (errorModel != null)
                            {
                                var errorMessages = errorModel.Messages.FirstOrDefault();
                                Console.WriteLine(" status: {0}, {1}", "Failed", errorMessages.Message);
                            }
                            else
                            {
                                Console.WriteLine(" status: {0}, {1}", "Failed", "Last Error Response is Null");
                            }
                        }
                        else
                        {
                            WorkerStatus status = importState.Status;

                            while (status != WorkerStatus.Complete && status != WorkerStatus.Failed)
                            {
                                MinimalWorkerStateModel<MinimalImportStateModel> importState2 = client.FindImportState(importState.Key);
                                status = importState2.Status;
                                Console.WriteLine(" status: {0}, {1}", status, importState2.Message);
                                Thread.Sleep(10000);
                            }

                            var errorMessage = importState.Issues.FirstOrDefault();
                            Console.WriteLine(" status: {0}, {1}", status, errorMessage.Message);
                        }
                    }
                }
            }

            // create a new inspire client which will authenticate automatically.
            using (InspireClient client = new InspireClient(config))
            {
                try
                {
                    // get app information for the target resource
                    var appInfo = client.RetrieveAppInfo();

                    if (appInfo != null)
                    {
                        // display the application information found.
                        var appInfoValue = JsonConvert.SerializeObject(appInfo, Formatting.Indented);
                        Console.WriteLine("Application Information:" + Environment.NewLine + "--------------");
                        Console.WriteLine(appInfoValue);

                        // call the authentication routine...
                        if (AsyncHelper.RunSync(() => client.AuthenticateAsync()))
                        {
                            Console.WriteLine("Authentication Success!");

                            // get all users defined in the system
                            var usersList = client.RetrieveUsers();

                            Console.WriteLine("Users:" + Environment.NewLine + "--------------");

                            if (usersList != null && usersList.Any())
                            {
                                Console.WriteLine(JsonConvert.SerializeObject(usersList, Formatting.Indented));
                            }
                            else
                            {
                                Console.WriteLine("No users found.");
                            }

                            // get all of the current user's projects 
                            var myProjects = client.RetrieveProjects();

                            Console.WriteLine("My Projects:" + Environment.NewLine + "--------------");

                            if (myProjects != null && myProjects.Any())
                            {
                                myProjects.ForEach(project =>
                                {
                                    Console.WriteLine(project.Title);
                                });
                            }
                            else
                            {
                                Console.WriteLine("No assigned projects found.");
                            }

                            var componentTypes = client.RetrieveComponentTypes();

                            Console.WriteLine("Component Types:" + Environment.NewLine + "--------------");

                            if (componentTypes != null && componentTypes.Any())
                            {
                                componentTypes.ForEach(componentType =>
                                {
                                    Console.WriteLine($"{componentType.Name} | {componentType.DocumentType} | {componentType.Description}");
                                });
                            }
                            else
                            {
                                Console.WriteLine("No component types found.");
                            }
                            
                            var folders = client.FindFoldersByFolderId(0, true, Vasont.Inspire.Models.Security.PermissionFlags.All);

                            Console.WriteLine("Folders:" + Environment.NewLine + "--------------");

                            if (folders != null && folders.Any())
                            {
                                folders.ForEach(folder =>
                                {
                                    Console.WriteLine($"{folder.FolderId} | {folder.Name} | {folder.Description}");
                                });
                            }
                            else
                            {
                                Console.WriteLine("No folders found.");
                            }

                            var translationVendors = client.RetrieveTranslationVendors();

                            Console.WriteLine("Translation Vendors:" + Environment.NewLine + "--------------");

                            if (translationVendors != null && translationVendors.Any())
                            {
                                translationVendors.ForEach(translationVendor =>
                                {
                                    Console.WriteLine($"{translationVendor.TranslationVendorId} | {translationVendor.Name} | {translationVendor.Description}");
                                });
                            }
                            else
                            {
                                Console.WriteLine("No translation vendors found.");
                            }

                            Console.WriteLine();

                            // Test Translation Integration methods
                            var model = new TranslationIntegrationModel
                            {
                                DisplayName = "Test Translation Integration",
                                Description = "Testing Translation Integration records via SDK",
                                IntegrationMethod = IntegrationMethodType.Manual,
                                CreatedDate = DateTime.UtcNow,
                                CreatedBy = 1,
                                Active = true,
                                ConfigurationJson = string.Empty
                            };

                            // Create new Translation Integration record
                            model = client.CreateTranslationIntegration(model);

                            if (model.TranslationIntegrationId > 0)
                            {
                                Console.WriteLine($"Creation of Translation Integration record successful with identity '{model.TranslationIntegrationId}'");

                                // Query for it by ID
                                var findModel = client.FindTranslationIntegration(model.TranslationIntegrationId);

                                if (findModel != null && findModel.TranslationIntegrationId > 0)
                                {
                                    Console.WriteLine($"Found a Translation Integration record with identifier '{model.TranslationIntegrationId}'");

                                    Console.WriteLine(JsonConvert.SerializeObject(findModel));

                                    string updateName = "Testing Updating Translation Integration";
                                    findModel.DisplayName = updateName;

                                    findModel = client.UpdateTranslationIntegration(findModel);

                                    // Check the display name on the returned model
                                    if (findModel.DisplayName.Equals(updateName))
                                    {
                                        // Get the model from the sdk again to be sure it updated in the database
                                        findModel = client.FindTranslationIntegration(model.TranslationIntegrationId);

                                        if (findModel.DisplayName.Equals(updateName))
                                        {
                                            Console.WriteLine($"Successfully Updated Translation Integration record");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Failed to update the Translation Integration record");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Failed to update the Translation Integration record");
                                    }

                                    // Find all Translation Integrations
                                    var allTranslationIntegrations = client.FindTranslationIntegrations(false);

                                    if (allTranslationIntegrations != null && allTranslationIntegrations.TranslationIntegrations.Any())
                                    {
                                        Console.WriteLine($"Translation Integrations found. {Environment.NewLine} Id | Name | Description");

                                        allTranslationIntegrations.TranslationIntegrations.ForEach(translationIntegration =>
                                        {
                                            Console.WriteLine($"{translationIntegration.TranslationIntegrationId} | {translationIntegration.DisplayName} | {translationIntegration.Description}");
                                        });
                                    }
                                    else
                                    {
                                        Console.WriteLine("No Translation Integrations found.");
                                    }

                                    // Delete the test record
                                    if (client.DeleteTranslationIntegration(findModel.TranslationIntegrationId))
                                    {
                                        Console.WriteLine($"Successfully deleted the Translation Integration record with identity '{findModel.TranslationIntegrationId}'");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Failed to delete the Translation Integration record");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Failed to find a Translation Integration record with identifier '{model.TranslationIntegrationId}'");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Failed to create a Translation Integration record");
                            }
                        }
                        else if (client.HasError)
                        {
                            Console.WriteLine("Authentication Failed! Reason = :" + Environment.NewLine + client.LastErrorResponse);
                        }
                    }
                }
                catch (InspireClientException inEx)
                {
                    Console.WriteLine("Error: " + inEx.Message);
                }
            }

            // wait
            Console.WriteLine("Press any key to close the console.");
            Console.ReadKey();
        }
    }
}
