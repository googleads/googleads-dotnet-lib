// Copyright 2014, Google Inc. All Rights Reserved.
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

using System.Threading;

namespace Google.Api.Ads.Common.Util.Reports
{
    /// <summary>
    /// The base class for all Ads API report download utilities.
    /// </summary>
    public abstract class AdsReportUtilities
    {
        /// <summary>
        /// The duration in milliseconds to wait between each poll to see if a
        /// report is ready to be downloaded.
        /// </summary>
        private const int WAIT_PERIOD = 30 * 1000;

        /// <summary>
        /// Delegate to be triggered when the report is ready to download.
        /// </summary>
        /// <param name="response">The report response.</param>
        public delegate void OnReadyCallback(ReportResponse response);

        /// <summary>
        /// Delegate to be triggered when the report download failed.
        /// </summary>
        /// <param name="exception">The report download exception.</param>
        public delegate void OnFailedCallback(AdsReportsException exception);

        /// <summary>
        /// The user associated with this object.
        /// </summary>
        private AdsUser user;

        /// <summary>
        /// The thread to use when downloading the report in an asynchronous manner.
        /// </summary>
        private Thread asyncThread;

        /// <summary>
        /// The callback that will be triggered when the report is ready to be
        /// downloaded.
        /// </summary>
        private OnReadyCallback onReady;

        /// <summary>
        /// The callback that will be triggered when the report download fails.
        /// </summary>
        private OnFailedCallback onFailed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdsReportUtilities"/>
        /// class.
        /// </summary>
        /// <param name="user">AdWords user to be used along with this
        /// utilities object.</param>
        public AdsReportUtilities(AdsUser user)
        {
            this.user = user;
        }

        /// <summary>
        /// Returns a flag indicating whether the caller should wait more time for
        /// the report download to complete.
        /// </summary>
        /// <returns>True, if the caller should wait more, false otherwise.
        /// </returns>
        protected virtual bool ShouldWaitMore()
        {
            return false;
        }

        /// <summary>
        /// Schedules a report for download.
        /// </summary>
        protected virtual void Schedule()
        {
        }

        /// <summary>
        /// Gets the report response.
        /// </summary>
        /// <returns>The report response.</returns>
        protected abstract ReportResponse GetReport();

        /// <summary>
        /// Returns the user associated with this object.
        /// </summary>
        public AdsUser User
        {
            get { return user; }
        }

        /// <summary>
        /// Gets or sets the callback that will be triggered when the report is
        /// ready to be downloaded.
        /// </summary>
        public OnReadyCallback OnReady
        {
            get { return onReady; }
            set { onReady = value; }
        }

        /// <summary>
        /// Gets or sets the callback that will be triggered when the report
        /// download fails.
        /// </summary>
        public OnFailedCallback OnFailed
        {
            get { return onFailed; }
            set { onFailed = value; }
        }

        /// <summary>
        /// Gets the report download response asynchronously.
        /// </summary>
        public virtual void GetResponseAsync()
        {
            asyncThread = new Thread(new ThreadStart(delegate()
            {
                try
                {
                    ReportResponse response = GetResponse();
                    if (onReady != null)
                    {
                        onReady(response);
                    }
                }
                catch (AdsReportsException e)
                {
                    if (onFailed != null)
                    {
                        onFailed(e);
                    }
                    else
                    {
                        throw;
                    }
                }
            }));
            asyncThread.Start();
        }

        /// <summary>
        /// Gets the report download response.
        /// </summary>
        /// <returns>The report response.</returns>
        public virtual ReportResponse GetResponse()
        {
            Schedule();
            while (ShouldWaitMore())
            {
                Thread.Sleep(WAIT_PERIOD);
            }

            return GetReport();
        }
    }
}
