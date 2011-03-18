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
using System.Runtime.Serialization;

namespace Google.Api.Ads.AdWords.v13 {
  /// <summary>
  /// Handles error codes 17, 32, 40, 43, 81, 96, 112, 134
  /// </summary>
  [Serializable]
  public class ExceededLimitsException : LegacyAdWordsApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public ExceededLimitsException(int errorCode) : base(errorCode) {
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public ExceededLimitsException(int errorCode, string message) : base(errorCode, message) {
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public ExceededLimitsException(int errorCode, string message, Exception innerException)
        : base(errorCode, message, innerException) {
    }

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected ExceededLimitsException(SerializationInfo info, StreamingContext context)
        : base(info, context) {
    }
  }
}
