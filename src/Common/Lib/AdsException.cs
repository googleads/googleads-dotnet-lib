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

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// Base class for all exceptions thrown by the library related
    /// to an Ads API call.
    /// </summary>
    [Serializable]
    public abstract class AdsException : Exception
    {
        /// <summary>
        /// Protected constructor.
        /// </summary>
        protected AdsException() : base()
        {
        }

        /// <summary>
        /// Protected constructor.
        /// </summary>
        /// <param name="message">Error message for this API exception.</param>
        protected AdsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Protected constructor.
        /// </summary>
        /// <param name="message">Error message for this API exception.</param>
        /// <param name="innerException">Inner exception, if any.</param>
        protected AdsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Protected constructor, used by serialization frameworks while
        /// deserializing an exception object.
        /// </summary>
        /// <param name="info">Info about the serialization context.</param>
        /// <param name="context">A streaming context that represents the
        /// serialization stream.</param>
        protected AdsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets a specified field from serialization stream.
        /// </summary>
        /// <typeparam name="T">The type of field.</typeparam>
        /// <param name="info">The serialization context.</param>
        /// <param name="fieldName">The serialization field name.</param>
        /// <returns>The deserialized value of field.</returns>
        protected T GetValue<T>(SerializationInfo info, string fieldName)
        {
            return (T) info.GetValue(fieldName, typeof(T));
        }
    }
}
