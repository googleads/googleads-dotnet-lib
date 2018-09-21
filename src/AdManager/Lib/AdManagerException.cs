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

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;

namespace Google.Api.Ads.AdManager.Lib
{
    /// <summary>
    /// Base class for all exceptions specific to DFP.
    /// </summary>
    [Serializable]
    public class AdManagerException : AdsException
    {
        /// <summary>
        /// Public constructor.
        /// </summary>
        public AdManagerException() : base()
        {
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="message">Error message for this API exception.</param>
        public AdManagerException(string message) : base(message)
        {
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="message">Error message for this API exception.</param>
        /// <param name="innerException">Inner exception, if any.</param>
        public AdManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Protected constructor. Used by serialization frameworks while
        /// deserializing an exception object.
        /// </summary>
        /// <param name="info">Info about the serialization context.</param>
        /// <param name="context">A streaming context that represents the
        /// serialization stream.</param>
        protected AdManagerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
