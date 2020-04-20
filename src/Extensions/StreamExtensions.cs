//-------------------------------------------------------------
// <copyright file="StreamExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;
    using Vasont.Inspire.Models.Transfers;

    /// <summary>
    /// This class contains static extensions for the Stream object.
    /// </summary>
    internal static class StreamExtensions
    {
        /// <summary>
        /// This extension method is used to write a multi-part form post to a specified stream.
        /// </summary>
        /// <param name="formDataStream">Contains the form data stream to write the output to.</param>
        /// <param name="formModel">Contains the form model object to serialize to the output.</param>
        /// <param name="files">Contains a list of file paths to load and write to the form data stream.</param>
        /// <param name="modelFormName">Contains the model form name used in the multi-part header formatting.</param>
        /// <param name="boundary">Contains the boundary name for the multi-part data.</param>
        /// <param name="encoding">Contains an optional encoding for string data. By default, encoding is UTF8.</param>
        public static void WriteMultiPartFormData(this Stream formDataStream, ImportRequestModel formModel, List<string> files, string modelFormName = "model", string boundary = "inspireBoundary", Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] lineBreakBytes = encoding.GetBytes("\r\n");

            byte[] folderIdContentBytes = encoding.GetBytes(string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                    boundary,
                    "FolderId",
                    formModel.FolderId));

            formDataStream.Write(folderIdContentBytes, 0, folderIdContentBytes.Length);
            formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

            byte[] projectFolderIdContentBytes = encoding.GetBytes(string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                    boundary,
                    "ProjectFolderId",
                    formModel.ProjectFolderId));

            formDataStream.Write(projectFolderIdContentBytes, 0, projectFolderIdContentBytes.Length);
            formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];

                if (File.Exists(file))
                {
                    string importFileFilename = $"ImportFiles[{i}].FileName";
                    string importFileUnzipFile = $"ImportFiles[{i}].UnzipFile";
                    string fileName = Path.GetFileName(file);

                    var importFileModel = formModel.ImportFiles.FirstOrDefault(importFile => importFile.FileName.Equals(fileName, StringComparison.InvariantCultureIgnoreCase));
                    var formModelFile = formModel.Files.FirstOrDefault(modelFile => modelFile.FileName.Equals(fileName, StringComparison.InvariantCultureIgnoreCase));

                    if (importFileModel != null && formModelFile != null)
                    {
                        byte[] importFileFilenameContentBytes = encoding.GetBytes(string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                            boundary,
                            importFileFilename,
                            importFileModel.FileName));

                        formDataStream.Write(importFileFilenameContentBytes, 0, importFileFilenameContentBytes.Length);
                        formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

                        byte[] importFileUnzipFileContentBytes = encoding.GetBytes(string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                            boundary,
                            importFileUnzipFile,
                            importFileModel.UnzipFile));

                        formDataStream.Write(importFileUnzipFileContentBytes, 0, importFileUnzipFileContentBytes.Length);
                        formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

                        byte[] fileDataContentBytes = encoding.GetBytes(string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: application/octet-stream\r\n\r\n",
                            boundary,
                            formModelFile.Name,
                            formModelFile.FileName));
                        formDataStream.Write(fileDataContentBytes, 0, fileDataContentBytes.Length);

                        byte[] fileData = File.ReadAllBytes(file);

                        // Write the file data directly to the Stream, rather than serializing it to a string.
                        formDataStream.Write(fileData, 0, fileData.Length);
                        formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);
                    }
                }
            }

            // Add the end of the request.  Start with a newline
            byte[] footerBytes = encoding.GetBytes("\r\n--" + boundary + "--\r\n");
            formDataStream.Write(footerBytes, 0, footerBytes.Length);
        }
    }
}
