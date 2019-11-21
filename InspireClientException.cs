//-------------------------------------------------------------
// <copyright file="InspireClientException.cs" company="Vasont Systems">
// Copyright (c) Vasont Systems. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace Vasont.Inspire.SDK
{
    using System;

    /// <summary>
    /// This class extends the default client exception to include additional configuration detail.
    /// </summary>
    [Serializable]
    public class InspireClientException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InspireClientException"/> class.
        /// </summary>
        public InspireClientException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InspireClientException"/> class.
        /// </summary>
        /// <param name="config">Contains the optional inspire client configuration settings.</param>
        /// <param name="message">Contains a message.</param>
        /// <param name="innerException">Contains an optional inner exception.</param>
        public InspireClientException(InspireClientConfiguration config, string message = "", Exception innerException = null)
            : base(message, innerException)
        {
            this.ClientConfiguration = config;           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InspireClientException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InspireClientException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InspireClientException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public InspireClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Gets the inspire client configuration settings.
        /// </summary>
        public InspireClientConfiguration ClientConfiguration { get; }
    }
}
