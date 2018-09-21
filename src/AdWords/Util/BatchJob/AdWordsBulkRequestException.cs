// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;

using Newtonsoft.Json;

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Google.Api.Ads.AdWords.Util.BatchJob
{
    /// <summary>
    /// Represents an exception when trying to upload or download operations for
    /// a batch job.
    /// </summary>
    public class AdWordsBulkRequestException : AdWordsException
    {
        private CloudStorageError errorField;

        /// <summary>
        /// Gets or sets the underlying <see cref="CloudStorageError"/> error.
        /// </summary>
        public CloudStorageError error
        {
            get { return errorField; }
            set { errorField = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdWordsBulkRequestException"/> class.
        /// </summary>
        /// <param name="jsonErrorText">The JSON error response from the cloud
        /// storage server.</param>
        public AdWordsBulkRequestException(string jsonErrorText) : base()
        {
            ParseJsonError(jsonErrorText);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdWordsBulkRequestException"/> class.
        /// </summary>
        /// <param name="message">Error message for this API exception.</param>
        /// <param name="jsonErrorText">The JSON error response from the cloud
        /// storage server.</param>
        public AdWordsBulkRequestException(string message, string jsonErrorText) : base(message)
        {
            ParseJsonError(jsonErrorText);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdWordsBulkRequestException"/> class.
        /// </summary>
        /// <param name="message">Error message for this API exception.</param>
        /// <param name="innerException">Inner exception, if any.</param>
        /// <param name="jsonErrorText">The JSON error response from the cloud
        /// storage server.</param>
        public AdWordsBulkRequestException(string message, Exception innerException,
            string jsonErrorText) : base(message, innerException)
        {
            ParseJsonError(jsonErrorText);
        }

        /// <summary>
        /// Parses the JSON error text.
        /// </summary>
        /// <param name="jsonErrorText">The JSON error response from the cloud
        /// storage server.</param>
        private void ParseJsonError(string jsonErrorText)
        {
            CloudStorageErrorResponse temp =
                JsonConvert.DeserializeObject<CloudStorageErrorResponse>(jsonErrorText);
            this.errorField = temp.error;
        }

        /// <summary>
        /// Protected constructor. Used by serialization frameworks while
        /// deserializing an exception object.
        /// </summary>
        /// <param name="info">Info about the serialization context.</param>
        /// <param name="context">A streaming context that represents the
        /// serialization stream.</param>
        protected AdWordsBulkRequestException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            ParseJsonError(GetValue<string>(info, "error"));
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
            if (error != null)
            {
                info.AddValue("error", JsonConvert.SerializeObject(error));
            }
        }
    }
}
