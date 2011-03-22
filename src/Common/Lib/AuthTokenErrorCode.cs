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

using System;
using System.Runtime.Serialization;

namespace Google.Api.Ads.Common.Lib {
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
}
