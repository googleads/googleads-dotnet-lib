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

namespace com.google.api.adwords.lib {
  /// <summary>
  /// An enumeration for all the possible exceptions you can
  /// get from ClientLogin API.
  /// </summary>
  public enum AuthTokenErrorCode {
    /// <summary>
    /// The login request used a username or password that is not recognized.
    /// </summary>
    BadAuthentication,

    /// <summary>
    /// The account email address has not been verified. The user will need to
    /// access their Google account directly to resolve the issue before logging
    /// in using a non-Google application.
    /// </summary>
    NotVerified,

    /// <summary>
    /// The user has not agreed to terms. The user will need to access their Google
    /// account directly to resolve the issue before logging in using a non-Google application.
    /// </summary>
    TermsNotAgreed,

    /// <summary>
    /// A CAPTCHA is required. (A response with this error code will also contain an image URL
    /// and a CAPTCHA token.)
    /// </summary>
    CaptchaRequired,

    /// <summary>
    /// The error is unknown or unspecified; the request contained invalid input or was malformed.
    /// </summary>
    Unknown,

    /// <summary>
    /// The user account has been deleted.
    /// </summary>
    AccountDeleted,

    /// <summary>
    /// The user account has been disabled.
    /// </summary>
    AccountDisabled,

    /// <summary>
    /// The user's access to the specified service has been disabled. (The user account may
    /// still be valid.)
    /// </summary>
    ServiceDisabled,

    /// <summary>
    /// The service is not available; try again later.
    /// </summary>
    ServiceUnavailable
  };

  /// <summary>
  /// An exception class that represents an exception thrown by ClientLogin API.
  /// </summary>
  [Serializable]
  public class AuthTokenException : System.ApplicationException {
    /// <summary>
    /// The error code associated with this Auth Exception.
    /// </summary>
    AuthTokenErrorCode errorCode;

    /// <summary>
    /// The url that describes this error.
    /// </summary>
    string errorUrl = "";

    /// <summary>
    /// The token associated with the captcha, if a Require Captcha error
    /// is triggered.
    /// </summary>
    string captchaToken = "";

    /// <summary>
    /// The url for the captcha. If Require Captcha error is triggered, then
    /// this url should be presented to the users to unlock their accounts.
    /// </summary>
    string captchaUrl = "";

    /// <summary>
    /// A url that describes the error for this exception.
    /// </summary>
    public string ErrorUrl {
      get {return errorUrl;}
      set {errorUrl = value;}
    }

    /// <summary>
    /// A token to identify the captcha if it gets triggered.
    /// </summary>
    public string CaptchaToken {
      get {return captchaToken;}
      set {captchaToken = value;}
    }

    /// <summary>
    /// The url for the captcha page page. Append this url to
    /// http://www.google.com/accounts.
    /// </summary>
    public string CaptchaUrl {
      get {return captchaUrl;}
      set {captchaUrl = value;}
    }

    /// <summary>
    /// The error code that caused this exception.
    /// </summary>
    public AuthTokenErrorCode ErrorCode {
      get {return errorCode;}
      set {errorCode = value;}
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="standardErrorCode">The error code for this exception.
    /// </param>
    public AuthTokenException(AuthTokenErrorCode standardErrorCode)
      : this(standardErrorCode, "", "", "", "", null) {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="standardErrorCode">The error code for this exception.
    /// </param>
    /// <param name="errorUrl">The error url for this exception.</param>
    public AuthTokenException(AuthTokenErrorCode standardErrorCode,
        string errorUrl)
      : this(standardErrorCode, errorUrl, "", "", "", null) {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="standardErrorCode">The error code for this exception.
    /// </param>
    /// <param name="errorUrl">The error url for this exception.</param>
    /// <param name="captchaToken">The captcha token, if applicable.</param>
    /// <param name="captchaUrl">The captcha url, if applicable.</param>
    public AuthTokenException(AuthTokenErrorCode standardErrorCode,
        string errorUrl, string captchaToken, string captchaUrl)
      : this(standardErrorCode, errorUrl, captchaToken, captchaUrl,
             "", null) {
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
    public AuthTokenException(AuthTokenErrorCode standardErrorCode,
        string errorUrl, string captchaToken, string captchaUrl,
        string message)
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
    public AuthTokenException(AuthTokenErrorCode standardErrorCode,
        string errorUrl, string captchaToken, string captchaUrl,
        string message, Exception innerException)
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
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected AuthTokenException(SerializationInfo info,
        StreamingContext context)
      : base(info, context) {
      this.errorCode = (AuthTokenErrorCode) info.GetValue(
                        "ErrorCode", typeof(AuthTokenErrorCode));
      this.errorUrl =
        (string) info.GetValue("ErrorUrl", typeof(string));
      this.captchaToken =
        (string) info.GetValue("CaptchaToken", typeof(string));
      this.captchaUrl = (string) info.GetValue("CaptchaUrl", typeof(string));
    }

    /// <summary>
    /// This method is called by serialization frameworks while serializing
    /// an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    public override void GetObjectData(SerializationInfo info,
        StreamingContext context) {
      base.GetObjectData(info, context);
      info.AddValue("ErrorCode", errorCode, typeof(AuthTokenErrorCode));
      info.AddValue("ErrorUrl", errorCode, typeof(string));
      info.AddValue("CaptchaToken", errorCode, typeof(string));
      info.AddValue("CaptchaUrl", errorCode, typeof(string));
    }
  }
}
