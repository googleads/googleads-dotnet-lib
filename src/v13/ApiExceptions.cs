// Copyright 2009, Google Inc. All Rights Reserved.
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
using System.Runtime.Serialization;


namespace com.google.api.adwords.v13 {
  /// <summary>
  /// Custom exception class for wrapping AdWords API v13 Soap exceptions.
  /// </summary>
  [Serializable]
  public class ApiException : ApplicationException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public ApiException(int errorCode)
      : base() {
      this.errorCode = errorCode;
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public ApiException(int errorCode, string message)
      : base(message) {
      this.errorCode = errorCode;
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public ApiException(int errorCode, string message, Exception innerException)
      : base(message, innerException) {
      this.errorCode = errorCode;
    }

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected ApiException(SerializationInfo info, StreamingContext context)
      : base(info, context) {
      this.errorCode = (int) info.GetValue("ErrorCode", typeof(int));
    }

    /// <summary>
    /// This method is called by serialization frameworks while serializing
    /// an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
      info.AddValue("ErrorCode", errorCode, typeof(int));
    }

    /// <summary>
    /// Error code associated with this exception.
    /// </summary>
    private int errorCode;
  }

  /// <summary>
  /// Handles error codes 84, 85, 86, 119, 162, 163, 164, 165, 183
  /// </summary>
  public class AccountException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public AccountException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public AccountException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public AccountException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected AccountException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 50, 52, 53, 106, 107, 108, 109, 110, 114,
  /// 118, 129, 130, 132
  /// </summary>
  public class BillingException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public BillingException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public BillingException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public BillingException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected BillingException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 0, 18, 55, 60, 95, 98, 117, 143, 155, 166
  /// </summary>
  public class GoogleInternalException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public GoogleInternalException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public GoogleInternalException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public GoogleInternalException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected GoogleInternalException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 100, 101, 102, 103, 104, 105
  /// </summary>
  public class WebPageException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public WebPageException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public WebPageException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public WebPageException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected WebPageException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 116, 139
  /// </summary>
  public class SandboxException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public SandboxException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public SandboxException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public SandboxException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected SandboxException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 1, 2, 3, 8, 41, 42, 73, 88, 89, 94, 115, 131, 184
  /// </summary>
  public class InvalidRequestException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public InvalidRequestException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public InvalidRequestException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public InvalidRequestException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected InvalidRequestException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 21, 120, 121
  /// </summary>
  public class PolicyViolationException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public PolicyViolationException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public PolicyViolationException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public PolicyViolationException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected PolicyViolationException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 25, 71, 76, 77, 78, 79, 90, 91, 92, 93, 128, 141,
  /// 147, 170, 171, 172, 173, 174, 176, 177, 188, 189, 207
  /// </summary>
  public class InvalidOperationException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public InvalidOperationException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public InvalidOperationException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public InvalidOperationException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected InvalidOperationException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 6, 7, 9, 10, 12, 13, 14, 15, 16, 19, 20, 22, 23,
  /// 24, 26, 27, 28, 29, 30, 31, 33, 34, 35, 36, 37, 38, 39, 44, 45, 46,
  /// 47, 48, 49, 51, 54, 57, 59, 70, 72, 74, 75, 80, 82, 83, 97, 99, 111,
  /// 122, 123, 124, 125, 127, 133, 137, 140, 142, 144, 145, 146, 149, 153,
  /// 156, 157, 158, 186, 190, 206,
  /// </summary>
  public class InvalidParameterException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public InvalidParameterException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public InvalidParameterException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public InvalidParameterException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected InvalidParameterException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 4, 5, 61, 62, 63, 138
  /// </summary>
  public class PermissionException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public PermissionException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public PermissionException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public PermissionException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected PermissionException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 58, 87
  /// </summary>
  public class ConcurrencyException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public ConcurrencyException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public ConcurrencyException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public ConcurrencyException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected ConcurrencyException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }

  /// <summary>
  /// Handles error codes 17, 32, 40, 43, 81, 96, 112, 134
  /// </summary>
  public class ExceededLimitsException : ApiException {
    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    public ExceededLimitsException(int errorCode)
      : base(errorCode) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    public ExceededLimitsException(int errorCode, string message)
      : base(errorCode, message) {}

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="errorCode">Error code for this API exception.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public ExceededLimitsException(int errorCode, string message, Exception innerException)
      : base(errorCode, message, innerException) {}

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected ExceededLimitsException(SerializationInfo info, StreamingContext context)
      : base(info, context) {}
  }
}
