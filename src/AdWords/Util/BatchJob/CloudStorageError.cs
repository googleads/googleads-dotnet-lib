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
    /// An error from Google Cloud Storage servers.
    /// </summary>
    public class CloudStorageError
    {
        private int codeField;
        private string messageField;
        private CloudStorageErrorDetail[] errorsField;

        /// <summary>
        /// Gets or sets the HTTP error code.
        /// </summary>
        public int code
        {
            get { return codeField; }
            set { codeField = value; }
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
        /// Gets or sets the additional error details.
        /// </summary>
        public CloudStorageErrorDetail[] errors
        {
            get { return errorsField; }
            set { errorsField = value; }
        }
    }
}
