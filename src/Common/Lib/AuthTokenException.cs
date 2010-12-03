// Copyright 2010, Google Inc. All Rights Reserved.
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

using System;
using System.Runtime.Serialization;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// An exception class that represents an exception thrown by ClientLogin API.
  /// </summary>
  [Serializable]
  public class AuthTokenException : AdsException {
    /// <summary>
    /// The error code associated with this Auth Exception.
    /// </summary>
    private AuthTokenErrorCode errorCode;

    /// <summary>
    /// The url that describes this error.
    /// </summary>
    private Uri errorUrl;

    /// <summary>
    /// The token associated with the captcha, if a Require Captcha error
    /// is triggered.
    /// </summary>
    private string captchaToken;

    /// <summary>
    /// The url for the captcha. If Require Captcha error is triggered, then
    /// this url should be presented to the users to unlock their accounts.
    /// </summary>
    private Uri captchaUrl;

    /// <summary>
    /// A url that describes the error for this exception.
    /// </summary>
    public Uri ErrorUrl {
      get {
        return errorUrl;
      }
      set {
        errorUrl = value;
      }
    }

    /// <summary>
    /// A token to identify the captcha if it gets triggered.
    /// </summary>
    public string CaptchaToken {
      get {
        return captchaToken;
      }
      set {
        captchaToken = value;
      }
    }

    /// <summary>
    /// The url for the captcha page page. Append this url to
    /// http://www.google.com/accounts.
    /// </summary>
    public Uri CaptchaUrl {
      get {
        return captchaUrl;
      }
      set {
        captchaUrl = value;
      }
    }

    /// <summary>
    /// The error code that caused this exception.
    /// </summary>
    public AuthTokenErrorCode ErrorCode {
      get {
        return errorCode;
      }
      set {
        errorCode = value;
      }
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="standardErrorCode">The error code for this exception.
    /// </param>
    public AuthTokenException(AuthTokenErrorCode standardErrorCode)
        : this(standardErrorCode, null, String.Empty, null, String.Empty, null) {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="standardErrorCode">The error code for this exception.
    /// </param>
    /// <param name="errorUrl">The error url for this exception.</param>
    public AuthTokenException(AuthTokenErrorCode standardErrorCode, Uri errorUrl)
        : this(standardErrorCode, errorUrl, String.Empty, null, String.Empty, null) {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="standardErrorCode">The error code for this exception.
    /// </param>
    /// <param name="errorUrl">The error url for this exception.</param>
    /// <param name="captchaToken">The captcha token, if applicable.</param>
    /// <param name="captchaUrl">The captcha url, if applicable.</param>
    public AuthTokenException(AuthTokenErrorCode standardErrorCode, Uri errorUrl,
        string captchaToken, Uri captchaUrl)
        : this(standardErrorCode, errorUrl, captchaToken, captchaUrl, String.Empty, null) {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="standardErrorCode">The error code for this exception.
    /// </param>
    /// <param name="errorUrl">The error url for this exception.</param>
    /// <param name="captchaToken">The captcha token, if applicable.</param>
    /// <param name="captchaUrl">The captcha url, if applicable.</param>
    /// <param name="message">An additional error message for this exception,
    /// added by the code throwing this exception.</param>
    public AuthTokenException(AuthTokenErrorCode standardErrorCode, Uri errorUrl,
        string captchaToken, Uri captchaUrl, string message)
        : this(standardErrorCode, errorUrl, captchaToken, captchaUrl,
            message, null) {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="standardErrorCode">The error code for this exception.
    /// </param>
    /// <param name="errorUrl">The error url for this exception.</param>
    /// <param name="captchaToken">The captcha token, if applicable.</param>
    /// <param name="captchaUrl">The captcha url, if applicable.</param>
    /// <param name="message">An additional error message for this exception,
    /// added by the code throwing this exception.</param>
    /// <param name="innerException">An inner exception that this exception
    /// will wrap around.</param>
    public AuthTokenException(AuthTokenErrorCode standardErrorCode, Uri errorUrl,
        string captchaToken, Uri captchaUrl, string message, Exception innerException)
        : base(message, innerException) {
      this.errorCode = standardErrorCode;
      this.errorUrl = errorUrl;
      this.captchaToken = captchaToken;
      this.captchaUrl = captchaUrl;
    }

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.
    /// </param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    /// <exception cref="ArgumentNullException">Thrown if
    /// <paramref name="info"/> is null.</exception>
    protected AuthTokenException(SerializationInfo info, StreamingContext context)
        : base(info, context) {
      if (info == null) {
        throw new ArgumentNullException("info");
      }
      this.errorCode = (AuthTokenErrorCode) info.GetValue("ErrorCode",
          typeof(AuthTokenErrorCode));
      this.errorUrl = (Uri) info.GetValue("ErrorUrl", typeof(Uri));
      this.captchaToken = (string) info.GetValue("CaptchaToken", typeof(string));
      this.captchaUrl = (Uri) info.GetValue("CaptchaUrl", typeof(Uri));
    }

    /// <summary>
    /// This method is called by serialization frameworks while serializing
    /// an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    /// <exception cref="ArgumentNullException">Thrown if
    /// <paramref name="info"/> is null.</exception>
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      if (info == null) {
        throw new ArgumentNullException("info");
      }
      base.GetObjectData(info, context);
      info.AddValue("ErrorCode", errorCode, typeof(AuthTokenErrorCode));
      info.AddValue("ErrorUrl", errorUrl, typeof(Uri));
      info.AddValue("CaptchaToken", captchaToken, typeof(string));
      info.AddValue("CaptchaUrl", captchaUrl, typeof(Uri));
    }
  }
}
