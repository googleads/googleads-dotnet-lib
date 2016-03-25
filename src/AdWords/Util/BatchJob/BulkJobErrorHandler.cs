// Copyright 2015, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Common.Lib;

using System;
using System.Net;

namespace Google.Api.Ads.AdWords.Util.BatchJob {

  /// <summary>
  /// Error handler for bulk job requests.
  /// </summary>
  public class BulkJobErrorHandler : ErrorHandler {
    private readonly AdsUser user;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdWordsErrorHandler"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    public BulkJobErrorHandler(AdsUser user)
      : base(user.Config) {
        this.user = user;
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
        return IsTransientError(ex);
      } else {
        return false;
      }
    }

    /// <summary>
    /// Determines whether the exception thrown by the server is a transient
    /// error.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>
    /// True, if the server exception is a transient error, false
    /// otherwise.
    /// </returns>
    public override bool IsTransientError(Exception exception) {
      HttpStatusCode httpCode = 0;
      if (exception is AdWordsBulkRequestException) {
        AdWordsBulkRequestException e = (AdWordsBulkRequestException) exception;
        if (e.error != null) {
          httpCode = (HttpStatusCode) e.error.code;
        }
      } else if (exception is WebException) {
        WebException e = (WebException) exception;
        if (e.Response != null && e.Response is HttpWebResponse) {
          httpCode = (e.Response as HttpWebResponse).StatusCode;
        }
      } else {
        return false;
      }
      switch (httpCode) {
        case HttpStatusCode.InternalServerError:
        case HttpStatusCode.BadGateway:
        case HttpStatusCode.ServiceUnavailable:
        case HttpStatusCode.GatewayTimeout:
        case HttpStatusCode.NotFound:
          return true;

        default:
          return false;
      }
    }

    /// <summary>
    /// Prepares the system for retrying the last failed call.
    /// </summary>
    /// <param name="exception">The exception.</param>
    public override void PrepareForRetry(Exception exception) {
      try {
        if (IsTransientError(exception)) {
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
        throw new ApplicationException(msg, e);
      }
    }
  }
}
