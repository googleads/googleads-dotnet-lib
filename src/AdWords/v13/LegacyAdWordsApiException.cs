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

using Google.Api.Ads.AdWords.Lib;

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Google.Api.Ads.AdWords.v13 {
  /// <summary>
  /// Custom exception class for wrapping AdWords API v13 Soap exceptions.
  /// </summary>
  [Serializable]
  public class LegacyAdWordsApiException : AdWordsException {
    /// <summary>
    /// Error code associated with this exception.
    /// </summary>
    private int errorCode = -1;

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected LegacyAdWordsApiException(SerializationInfo info, StreamingContext context)
        : base(info, context) {
      if (info == null) {
        throw new ArgumentNullException("info");
      }
      this.errorCode = (int)info.GetValue("ErrorCode", typeof(int));
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public LegacyAdWordsApiException(int errorCode) : base() {
      this.errorCode = errorCode;
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public LegacyAdWordsApiException(int errorCode, string message) : base(message) {
      this.errorCode = errorCode;
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public LegacyAdWordsApiException(int errorCode, string message, Exception innerException)
        : base(message, innerException) {
      this.errorCode = errorCode;
    }

    /// <summary>
    /// This method is called by serialization frameworks while serializing
    /// an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      if (info == null) {
        throw new ArgumentNullException("info");
      }
      base.GetObjectData(info, context);
      info.AddValue("ErrorCode", errorCode, typeof(int));
    }
  }
}
