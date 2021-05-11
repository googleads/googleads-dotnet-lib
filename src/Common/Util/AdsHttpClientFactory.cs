// Copyright 2018 Google LLC
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
using Google.Apis.Http;

using System.Net.Http;

namespace Google.Api.Ads.Common.Util
{
    /// <summary>
    /// An <see cref="HttpClientFactory"/> implementation that allows setting proxy server.
    /// </summary>
    internal class AdsHttpClientFactory : HttpClientFactory
    {
        /// <summary>
        /// The configuration class for obtaining proxy instance.
        /// </summary>
        private AppConfig config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdsHttpClientFactory"/> class.
        /// </summary>
        /// <param name="config">The configuration instance.</param>
        internal AdsHttpClientFactory(AppConfig config) : base()
        {
            this.config = config;
        }

        /// <summary>
        /// Creates a HTTP message handler. Override this method to mock a message handler.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override HttpMessageHandler CreateHandler(CreateHttpClientArgs args)
        {
            if (config.Proxy != null)
            {
                HttpClientHandler webRequestHandler = new HttpClientHandler()
                {
                    UseProxy = true,
                    Proxy = config.Proxy,
                    UseCookies = false
                };

                return webRequestHandler;
            }
            else
            {
                return base.CreateHandler(args);
            }
        }
    }
}
