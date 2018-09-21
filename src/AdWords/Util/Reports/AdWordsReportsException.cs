// Copyright 2014, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Api.Ads.Common.Util.Reports;

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// Custom exception class for handling reporting errors.
    /// </summary>
    [Serializable]
    public class AdWordsReportsException : AdsReportsException
    {
        /// <summary>
        /// The errors returned by reports server.
        /// </summary>
        private ReportDownloadError[] errors;

        /// <summary>
        /// The API version.
        /// </summary>
        private string apiVersion;

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public ReportDownloadError[] Errors
        {
            get { return errors; }
            set
            {
                foreach (ReportDownloadError error in value)
                {
                    error.ApiVersion = this.ApiVersion;
                }

                errors = value;
            }
        }

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        public string ApiVersion
        {
            get { return apiVersion; }
            set { apiVersion = value; }
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        public AdWordsReportsException() : base()
        {
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="apiVersion">The API Version.</param>
        public AdWordsReportsException(string apiVersion) : base()
        {
            this.apiVersion = apiVersion;
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="message">Error message for this API exception.</param>
        /// <param name="apiVersion">The API Version.</param>
        public AdWordsReportsException(string apiVersion, string message) : base(message)
        {
            this.apiVersion = apiVersion;
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="apiVersion">The API Version.</param>
        /// <param name="message">Error message for this API exception.</param>
        /// <param name="innerException">Inner exception, if any.</param>
        public AdWordsReportsException(string apiVersion, string message, Exception innerException)
            : base(message, innerException)
        {
            this.apiVersion = apiVersion;
        }

        /// <summary>
        /// Protected constructor. Used by serialization frameworks while
        /// deserializing an exception object.
        /// </summary>
        /// <param name="info">Info about the serialization context.</param>
        /// <param name="context">A streaming context that represents the
        /// serialization stream.</param>
        protected AdWordsReportsException(SerializationInfo info, StreamingContext context) : base(
            info, context)
        {
            if (info != null)
            {
                errors = GetValue<ReportDownloadError[]>(info, "errors");
                apiVersion = GetValue<string>(info, "apiVersion");
            }
        }

        /// <summary>
        /// This method is called by serialization frameworks while serializing
        /// an exception object.
        /// </summary>
        /// <param name="info">Info about the serialization context.</param>
        /// <param name="context">A streaming context that represents the
        /// serialization stream.</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            base.GetObjectData(info, context);
            if (info != null)
            {
                info.AddValue("errors", errors);
                info.AddValue("apiVersion", apiVersion);
            }
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <returns>The error message that explains the reason for the exception,
        /// or an empty string("").</returns>
        public override string Message
        {
            get
            {
                StringBuilder exceptionBuilder = new StringBuilder();
                exceptionBuilder.AppendFormat("{0}: {1} ", this.GetType().Name, base.Message);

                if (errors != null)
                {
                    exceptionBuilder.AppendFormat("{0}{0}{1}{0}{0}", Environment.NewLine,
                        string.Join<ReportDownloadError>(Environment.NewLine, errors));
                }

                return exceptionBuilder.ToString();
            }
        }
    }
}
