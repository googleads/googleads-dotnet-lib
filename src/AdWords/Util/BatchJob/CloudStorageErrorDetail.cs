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

namespace Google.Api.Ads.AdWords.Util.BatchJob
{
    /// <summary>
    /// Additional details of a <see cref="CloudStorageError"/>.
    /// </summary>
    public class CloudStorageErrorDetail
    {
        private string domainField;
        private string reasonField;
        private string messageField;
        private string locationTypeField;
        private string locationField;
        private string debugInfoField;

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string domain
        {
            get { return domainField; }
            set { domainField = value; }
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        public string reason
        {
            get { return reasonField; }
            set { reasonField = value; }
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string message
        {
            get { return messageField; }
            set { messageField = value; }
        }

        /// <summary>
        /// Gets or sets the type of the location for error.
        /// </summary>
        public string locationType
        {
            get { return locationTypeField; }
            set { locationTypeField = value; }
        }

        /// <summary>
        /// Gets or sets the location of error.
        /// </summary>
        public string location
        {
            get { return locationField; }
            set { locationField = value; }
        }

        /// <summary>
        /// Gets or sets the additional debug information, if available.
        /// </summary>
        public string debugInfo
        {
            get { return debugInfoField; }
            set { debugInfoField = value; }
        }
    }
}
