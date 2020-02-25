//-------------------------------------------------------------
// <copyright file="StreamExtensions.cs" company="Vasont Systems">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;

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
        public static void WriteMultiPartFormData(this Stream formDataStream, object formModel, List<string> files, string modelFormName = "model", string boundary = "inspireBoundary", Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            long count = 0;
            byte[] lineBreakBytes = encoding.GetBytes("\r\n");
            byte[] modelContentBytes = encoding.GetBytes(string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}", boundary, modelFormName, JsonConvert.SerializeObject(formModel)));
            formDataStream.Write(modelContentBytes, 0, modelContentBytes.Length);
            formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

            // add any additional file data
            files.ForEach(file =>
            {
                count++;

                if (File.Exists(file))
                {
                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: application/octet-stream\r\n\r\n", boundary, "file" + count, Path.GetFileName(file));
                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));
                    byte[] fileData = File.ReadAllBytes(file);

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileData, 0, fileData.Length);
                }

                // write a line break
                formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);
            });

            // Add the end of the request.  Start with a newline
            byte[] footerBytes = encoding.GetBytes("\r\n--" + boundary + "--\r\n");
            formDataStream.Write(footerBytes, 0, footerBytes.Length);
        }
    }
}
