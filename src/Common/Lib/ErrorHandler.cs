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

using System;
using System.Threading;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// Handles errors for an Ads API.
    /// </summary>
    public class ErrorHandler
    {
        /// <summary>
        /// Wait time in ms to wait before retrying a call. The actual retry time
        /// will be higher due to exponential backoff.
        /// </summary>
        protected const int WAIT_TIME = 30000;

        /// <summary>
        /// The application configuration.
        /// </summary>
        protected readonly AppConfig config;

        /// <summary>
        /// Number of retry attempts made.
        /// </summary>
        private int numRetries = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandler"/> class.
        /// </summary>
        /// <param name="config">The application configuration instance.</param>
        public ErrorHandler(AppConfig config)
        {
            this.config = config;
        }

        /// <summary>
        /// Does the exponential backoff.
        /// </summary>
        public virtual void DoExponentialBackoff()
        {
            Thread.Sleep(WAIT_TIME * (int) Math.Pow(2, this.numRetries));
        }

        /// <summary>
        /// Checks if there are more retry attempts left.
        /// </summary>
        /// <returns>True, if there are more retry attempts left, false otherwise.
        /// </returns>
        public virtual bool HaveMoreRetryAttemptsLeft()
        {
            return numRetries < config.RetryCount;
        }

        /// <summary>
        /// Increment the counter for attempts retried.
        /// </summary>
        public virtual void IncrementRetriedAttempts()
        {
            this.numRetries++;
        }

        /// <summary>
        /// Determines whether the exception thrown by the server is a transient
        /// error.</summary>
        /// <param name="exception">The exception.</param>
        /// <returns>True, if the server exception is a transient error, false
        /// otherwise.</returns>
        public virtual bool IsTransientError(Exception exception)
        {
            return false;
        }

        /// <summary>
        /// Prepares the system for retrying the last failed call.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public virtual void PrepareForRetry(Exception exception)
        {
        }

        /// <summary>
        /// Checks if an API call should be retried when an exception occurs.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>True, if the call should be retried, false otherwise.</returns>
        public virtual bool ShouldRetry(Exception exception)
        {
            return false;
        }
    }
}
