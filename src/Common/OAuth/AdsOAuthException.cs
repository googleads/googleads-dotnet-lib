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

using Google.Api.Ads.Common.Util;
using Google.Apis.Auth.OAuth2.Responses;

using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// Exception thrown when OAuth authentication with Ads server fails.
    /// </summary>
    [Serializable]
    public class AdsOAuthException : AdsException
    {
        /// <summary>
        /// Gets the error information, if available.
        /// </summary>
        public TokenErrorResponse Error { get; private set; }

        /// <summary>
        /// Gets the HTTP status code of error, or null if unknown.
        /// </summary>
        public HttpStatusCode? StatusCode { get; private set; }

        /// <summary>
        /// Public constructor.
        /// </summary>
        public AdsOAuthException() : base()
        {
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="message">Error message for this API exception.</param>
        public AdsOAuthException(string message) : this(message, null, null, null)
        {
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="message">Error message for this API exception.</param>
        /// <param name="innerException">Inner exception, if any.</param>
        public AdsOAuthException(string message, Exception innerException)
            : this(message, innerException, null, null)
        {
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="message">Error message for this API exception.</param>
        /// <param name="innerException">Inner exception, if any.</param>
        /// <param name="error">The error details from server.</param>
        /// <param name="statusCode">HTTP status code, if available.</param>
        public AdsOAuthException(string message, Exception innerException, TokenErrorResponse error,
            HttpStatusCode? statusCode) : base(message, innerException)
        {
            this.Error = error;
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Protected constructor. Used by serialization frameworks while
        /// deserializing an exception object.
        /// </summary>
        /// <param name="info">Info about the serialization context.</param>
        /// <param name="context">A streaming context that represents the
        /// serialization stream.</param>
        protected AdsOAuthException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            Error = (TokenErrorResponse) SerializationUtilities.DeserializeFromXmlText(
                GetValue<string>(info, "error"), typeof(TokenErrorResponse));

            HttpStatusCode temp;
            if (Enum.TryParse(GetValue<string>(info, "statusCode"), out temp))
            {
                this.StatusCode = temp;
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

            if (Error != null)
            {
                info.AddValue("error", SerializationUtilities.SerializeAsXmlText(Error));
            }

            if (StatusCode != null)
            {
                info.AddValue("statusCode", StatusCode);
            }
        }
    }
}
