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
  /// Handles error codes 6, 7, 9, 10, 12, 13, 14, 15, 16, 19, 20, 22, 23,
  /// 24, 26, 27, 28, 29, 30, 31, 33, 34, 35, 36, 37, 38, 39, 44, 45, 46,
  /// 47, 48, 49, 51, 54, 57, 59, 70, 72, 74, 75, 80, 82, 83, 97, 99, 111,
  /// 122, 123, 124, 125, 127, 133, 137, 140, 142, 144, 145, 146, 149, 153,
  /// 156, 157, 158, 186, 190, 206,
  /// </summary>
  [Serializable]
  public class InvalidParameterException : LegacyAdWordsApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public InvalidParameterException(int errorCode): base(errorCode) {
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public InvalidParameterException(int errorCode, string message) : base(errorCode, message) {
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public InvalidParameterException(int errorCode, string message, Exception innerException)
        : base(errorCode, message, innerException) {
    }

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected InvalidParameterException(SerializationInfo info, StreamingContext context)
        : base(info, context) {
    }
  }
}
