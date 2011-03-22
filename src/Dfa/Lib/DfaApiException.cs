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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfa.Util;

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// Custom exception class for wrapping DFA API SOAP exceptions.
  /// </summary>
  [Serializable]
  public class DfaApiException : DfaException {
    /// <summary>
    /// The parsed error code.
    /// </summary>
    private ErrorCode errorCode;

    /// <summary>
    /// Gets the error code.
    /// </summary>
    public ErrorCode ErrorCode {
      get {
        return errorCode;
      }
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    public DfaApiException() : base() {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="errorCode">The parsed error code from the server.</param>
    public DfaApiException(ErrorCode errorCode) : base() {
      this.errorCode = errorCode;
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="errorCode">The parsed error code from the server.</param>
    /// <param name="message">Error message for this API exception.</param>
    public DfaApiException(ErrorCode errorCode, string message) : base(message) {
      this.errorCode = errorCode;
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="errorCode">The parsed error code from the server.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public DfaApiException(ErrorCode errorCode, string message, Exception innerException)
        : base(message, innerException) {
      this.errorCode = errorCode;
    }

    /// <summary>
    /// Protected constructor. Used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected DfaApiException(SerializationInfo info, StreamingContext context)
        : base(info, context) {
      if (info == null) {
        throw new ArgumentNullException("info");
      }
      errorCode = GetValue<ErrorCode>(info, "errorCode");
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
      if (errorCode != null) {
        info.AddValue("errorCode", errorCode);
      }
    }
  }
}
