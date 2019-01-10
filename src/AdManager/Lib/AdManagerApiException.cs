// Copyright 2011, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util;

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;

namespace Google.Api.Ads.AdManager.Lib
{
    /// <summary>
    /// Custom exception class for wrapping DFP API SOAP exceptions.
    /// </summary>
    [Serializable]
    public class AdManagerApiException : AdManagerException
    {
        /// <summary>
        /// The original ApiException object from DFP API.
        /// </summary>
        private object apiException;

        /// <summary>
        /// Gets the ApiException object.
        /// </summary>
        public object ApiException
        {
            get { return apiException; }
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        public AdManagerApiException() : base()
        {
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="apiException">The underlying ApiException from the
        /// server.</param>
        public AdManagerApiException(object apiException) : base()
        {
            this.apiException = apiException;
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message
        {
            get
            {
                StringBuilder exceptionBuilder = new StringBuilder();
                exceptionBuilder.AppendFormat("{0}: {1} ", this.GetType().Name, base.Message);

                if (apiException != null)
                {
                    exceptionBuilder.AppendFormat("{0}{0}{1}{0}{0}", Environment.NewLine,
                        apiException);
                }

                return exceptionBuilder.ToString();
            }
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="apiException">The underlying ApiException from the
        /// server.</param>
        /// <param name="message">Error message for this API exception.</param>
        public AdManagerApiException(object apiException, string message) : base(message)
        {
            this.apiException = apiException;
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="apiException">The underlying ApiException from the
        /// server.</param>
        /// <param name="message">Error message for this API exception.</param>
        /// <param name="innerException">Inner exception, if any.</param>
        public AdManagerApiException(object apiException, string message, Exception innerException)
            : base(message, innerException)
        {
            this.apiException = apiException;
        }

        /// <summary>
        /// Protected constructor. Used by serialization frameworks while
        /// deserializing an exception object.
        /// </summary>
        /// <param name="info">Info about the serialization context.</param>
        /// <param name="context">A streaming context that represents the
        /// serialization stream.</param>
        protected AdManagerApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            apiException = SerializationUtilities.DeserializeFromXmlText(
                GetValue<string>(info, "apiException"), GetValue<Type>(info, "apiExceptionType"));
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
            if (apiException != null)
            {
                info.AddValue("apiException",
                    SerializationUtilities.SerializeAsXmlText(apiException));
                info.AddValue("apiExceptionType", apiException.GetType());
            }
        }
    }
}
