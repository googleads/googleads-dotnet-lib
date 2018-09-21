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

using System;
using System.Reflection;

namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// Represents a report download error.
    /// </summary>
    [Serializable()]
    public class ReportDownloadError
    {
        /// <summary>
        /// Type of error.
        /// </summary>
        private string errorType;

        /// <summary>
        /// The reason for triggering this error.
        /// </summary>
        private string trigger;

        /// <summary>
        /// The field that triggered this error, if applicable.
        /// </summary>
        private string fieldPath;

        /// <summary>
        /// The API version.
        /// </summary>
        private string apiVersion = ReportUtilities.DEFAULT_REPORT_VERSION;

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        public string ApiVersion
        {
            get { return apiVersion; }
            set { apiVersion = value; }
        }

        /// <summary>
        /// Gets or sets the type of the error.
        /// </summary>
        public string ErrorType
        {
            get { return errorType; }
            set { errorType = value; }
        }

        /// <summary>
        /// Gets or sets the reason for triggering this error.
        /// </summary>
        public string Trigger
        {
            get { return trigger; }
            set { trigger = value; }
        }

        /// <summary>
        /// Gets or sets the field that triggered this error, if applicable.
        /// </summary>
        public string FieldPath
        {
            get { return fieldPath; }
            set { fieldPath = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string key = errorType.Replace("ReportDefinitionError", "ReportDefinitionErrorReason");

            string description = GetErrorDescription(key, apiVersion);

            return string.Format("{0}. (Error: {1}, FieldPath: {2}, Trigger: {3})", description,
                errorType, this.fieldPath, this.trigger);
        }

        /// <summary>
        ///  Retrieve a description for the specified error code and API version.
        /// </summary>
        /// <param name="errorCode">An error code</param>
        /// <param name="version">An AdWords API version.</param>
        public static string GetErrorDescription(string errorCode, string version)
        {
            string typeName =
                string.Format("Google.Api.Ads.AdWords.{0}.ErrorDescriptions", version);

            Type type = Type.GetType(typeName);
            if (type != null)
            {
                MethodInfo mi = type.GetMethod("Lookup", BindingFlags.Public | BindingFlags.Static);
                if (mi != null)
                {
                    return mi.Invoke(null, new object[]
                    {
                        errorCode
                    }).ToString();
                }
            }

            return errorCode;
        }
    }
}
