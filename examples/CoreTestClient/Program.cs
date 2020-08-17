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
    using System.Threading;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Newtonsoft.Json;
    using Vasont.Inspire.Core.Extensions;
    using Vasont.Inspire.Core.Utility;
    using Vasont.Inspire.Models.Transfers;
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
            string resourceUrl = "https://inspire-veritas-api.vasont.com/";
            string authorityUri = "https://identity.vasont.com";
            string userName = "VRM.VRTS.DevTools@veritas.com";
            string password = "NewP@ssw0rd";
            string clientSecret = "wgWhZVDsuPqF7haDSGIt";
            string clientId = "WuraOWZCQp.inspire-veritas.vasont.com";

            string zipFile = CommandLine.Parameters.ContainsKey("zipfile") ? CommandLine.Parameters["zipfile"].ConvertToString() : string.Empty;
            long folderId = CommandLine.Parameters.ContainsKey("folderid") ? CommandLine.Parameters["folderid"].ToLong() : 0;
            string version = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;

            Console.WriteLine($"Inspire SDK Test Client - Target Framework {version}");

            // create a new inspire client configuration
            InspireClientConfiguration config = new InspireClientConfiguration(
                clientId,
                ClientAuthenticationMethods.ResourceOwnerPassword,
                authorityUri,
                resourceUrl,
                clientSecret,
                userName,
                password);

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
                            MinimalWorkerStateModel<MinimalImportStateModel> importState2 = client.GetImportState(importState.Key);
                            status = importState2.Status;
                            Console.WriteLine(" status: {0}, {1}", status, importState2.Message);
                            Thread.Sleep(10000);
                        }

                        Console.WriteLine("Done");
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
                            var usersList = client.GetAllUsers();

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
                            var myProjects = client.GetProjects();

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

                            var componentTypes = client.GetComponentTypes();

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
                            
                            var folders = client.GetFoldersByFolderId(0, true, Vasont.Inspire.Models.Security.PermissionFlags.All);

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

                            var translationVendors = client.GetTranslationVendors();

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
