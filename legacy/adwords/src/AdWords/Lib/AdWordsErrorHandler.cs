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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Headers;
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// Handles AdWords API Errors.
  /// </summary>
  public class AdWordsErrorHandler : ErrorHandler {
    private readonly AdWordsUser user;

    /// <summary>
    /// The error thrown when an auth token expires.
    /// </summary>
    private const string COOKIE_INVALID_ERROR = "AuthenticationError.GOOGLE_ACCOUNT_COOKIE_INVALID";

    /// <summary>
    /// The error thrown when an oauth token expires.
    /// </summary>
    private const string OAUTH_TOKEN_EXPIRED_ERROR = "AuthenticationError.OAUTH_TOKEN_INVALID";

    /// <summary>
    /// The error thrown when an oauth token expires.
    /// </summary>
    private const string RATE_EXCEEDED_ERROR = "RateExceededError.RATE_EXCEEDED";

    /// <summary>
    /// The error thrown when an unexpected internal error occurs.
    /// </summary>
    private const string INTERNAL_ERROR = "InternalApiError.UNEXPECTED_INTERNAL_API_ERROR";

    /// <summary>
    /// Initializes a new instance of the <see cref="AdWordsErrorHandler"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    public AdWordsErrorHandler(AdWordsUser user)
      : base(user.Config) {
        this.user = user;
    }

    /// <summary>
    /// Gets the config.
    /// </summary>
    private AdWordsAppConfig Config {
      get {
        return this.user.Config as AdWordsAppConfig;
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
      if (HaveMoreRetryAttemptsLeft()) {
        return IsExpiredCredentialsError(ex) || IsTransientError(ex);
      } else {
        return false;
      }
    }

    /// <summary>
    /// Prepares the system for retrying the last failed call.
    /// </summary>
    /// <param name="ex">The exception.</param>
    public override void PrepareForRetry(Exception ex) {
      try {
        if (ex is AdWordsCredentialsExpiredException) {
          this.user.OAuthProvider.RefreshAccessToken();
        } else if (IsTransientError(ex)) {
          DoExponentialBackoff();
        }
        IncrementRetriedAttempts();
      } catch (Exception e) {
        // We threw an exception while trying to recover from another
        // exception. The second exception may contain additional details
        // (e.g. exact reason why OAuth token refresh failed.), so we
        // raise an ApplicationException with the message from the second
        // exception and the first exception as inner exception. Ideally,
        // we'd like to return both the exception objects, but this is
        // a reasonable tradeoff.
        string msg = string.Format("An error occured while retrying a failed API call : " +
            "{0}. See inner exception for more details.", e.Message);
        throw new ApplicationException(msg, ex);
      }
    }

    /// <summary>
    /// Determines whether an exception corrsponds to an expired credential.
    /// </summary>
    /// <param name="ex">The exception.</param>
    /// <returns>True if the exception corresponds to an expired credential,
    /// false otherwise.</returns>
    public static bool IsExpiredCredentialsError(Exception ex) {
      return ex is AdWordsCredentialsExpiredException;
    }

    /// <summary>
    /// Determines whether the exception thrown by the server is an OAuth token
    /// expired error.
    /// </summary>
    /// <param name="ex">The exception.</param>
    /// <returns>True, if the server exception is a OAuth token expired error,
    /// false otherwise.</returns>
    public static bool IsOAuthTokenExpiredError(Exception ex) {
      if (ex is AdWordsApiException) {
        return MatchesError((AdWordsApiException) ex, new string[] {OAUTH_TOKEN_EXPIRED_ERROR});
      } else if (ex is ReportsException) {
        return MatchesError((ReportsException) ex, new string[] {OAUTH_TOKEN_EXPIRED_ERROR});
      }
      return false;
    }

    /// <summary>
    /// Determines whether the exception thrown by the server matches a known
    /// error.
    /// </summary>
    /// <param name="ex">The exception.</param>
    /// <param name="errorMessage">The known error message.</param>
    /// <returns>True, if the server exception matches the known error, false
    /// otherwise.</returns>
    private static bool MatchesError(ReportsException ex, string[] errorMessages) {
      foreach (ReportDownloadError error in ex.Errors) {
        foreach (String errorMessage in errorMessages) {
          if (error.ErrorType.Contains(errorMessage)) {
            return true;
          }
        }
      }
      return false;
    }

    /// <summary>
    /// Determines whether the exception thrown by the server matches a known
    /// error.
    /// </summary>
    /// <param name="awapiException">The awapi exception.</param>
    /// <param name="errorMessage">The known error message.</param>
    /// <returns>True, if the server exception matches the known error, false
    /// otherwise.</returns>
    private static bool MatchesError(AdWordsApiException awapiException, string[] errorMessages) {
      object[] errors = (object[]) awapiException.ApiException.GetType().
          GetProperty("errors").GetValue(awapiException.ApiException, null);
      if (errors != null) {
        for (int i = 0; i < errors.Length; i++) {
          string errorString = (string) errors[i].GetType().GetProperty("errorString").
              GetValue(errors[i], null);
          foreach (String errorMessage in errorMessages) {
            if (String.Compare(errorString, errorMessage, true) == 0) {
              return true;
            }
          }
        }
      }
      return false;
    }
  }
}
