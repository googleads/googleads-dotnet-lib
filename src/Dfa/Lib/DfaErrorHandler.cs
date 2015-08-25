// Copyright 2013, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfa.Util;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// Handles DFA API Errors.
  /// </summary>
  public class DfaErrorHandler : ErrorHandler {
    private static readonly ErrorCode TOKEN_EXPIRED_CODE = ErrorCode.FromCode(4);
    private const string TOKEN_EXPIRED_MESSAGE = "Authentication token has expired.";

    /// <summary>
    /// The service associated with this error handler.
    /// </summary>
    private DfaSoapClient service = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="DfaErrorHandler"/> class.
    /// </summary>
    /// <param name="service">The service associated with this error handler.
    /// </param>
    public DfaErrorHandler(DfaSoapClient service)
      : base(service.User.Config) {
        this.service = service;
    }

    /// <summary>
    /// Gets the user.
    /// </summary>
    private DfaUser User {
      get {
        return this.service.User as DfaUser;
      }
    }

    /// <summary>
    /// Gets the config.
    /// </summary>
    private DfaAppConfig Config {
      get {
        return this.User.Config as DfaAppConfig;
      }
    }

    /// <summary>
    /// Checks if an API call should be retried when an exception occurs.
    /// </summary>
    /// <param name="ex">The exception.</param>
    /// <returns>
    /// True, if the call should be retried, false otherwise.
    /// </returns>
    public override bool ShouldRetry(Exception ex) {
      return HaveMoreRetryAttemptsLeft() && IsExpiredCredentialsError(ex);
    }

    /// <summary>
    /// Prepares the system for retrying the last failed call.
    /// </summary>
    /// <param name="ex">The exception.</param>
    public override void PrepareForRetry(Exception ex) {
      if (IsExpiredCredentialsError(ex)) {
        DfaCredentialsExpiredException e = (DfaCredentialsExpiredException) ex;
        LoginUtil.Cache.InvalidateToken(e.ExpiredCredential);
        this.Config.DfaAuthToken = null;
        this.service.Token = null;
        IncrementRetriedAttempts();
      } else {
        throw ex;
      }
    }

    /// <summary>
    /// Determines whether an exception corrsponds to an expired credential.
    /// </summary>
    /// <param name="ex">The exception.</param>
    /// <returns>True if the exception corresponds to an expired credential,
    /// false otherwise.</returns>
    public static bool IsExpiredCredentialsError(Exception ex) {
      return ex is DfaCredentialsExpiredException;
    }

    /// <summary>
    /// Determines whether the exception thrown by the server is due to a login
    /// token expiration.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>True, if the server exception is a AuthToken invalid error,
    /// false otherwise.</returns>
    public static bool IsTokenExpiredError(Exception exception) {
      if (exception is DfaApiException) {
        DfaApiException dfaException = (DfaApiException) exception;
        if (dfaException.ErrorCode == TOKEN_EXPIRED_CODE && dfaException.Message.CompareTo(
            TOKEN_EXPIRED_MESSAGE) == 0) {
          return true;
        }
      }
      return false;
    }
  }
}
