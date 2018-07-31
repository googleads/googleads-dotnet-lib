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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Google.Api.Ads.Common.Util
{
    /// <summary>
    /// Utility class for various HTTP tasks.
    /// </summary>
    public class HttpUtilities
    {
        /// <summary>
        /// Builds an HTTP PUT request with a Range header.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="contentLength">Length of the content.</param>
        /// <param name="range">The range heaer notation.</param>
        /// <param name="config">The configuration instance for customizing the
        /// connection settings.</param>
        /// <returns>The web request for making HTTP call.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="config"/>
        /// is null.</exception>
        public static WebRequest BuildRangeRequest(string url, int contentLength, string range,
            AppConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            WebRequest request = BuildRequest(url, "PUT", config);

            request.ContentLength = contentLength;
            request.Headers["Content-Range"] = range;

            return request;
        }

        /// <summary>
        /// Builds an HTTP request with a specified method.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="config">The configuration instance for customizing the
        /// connection settings.</param>
        /// <returns>The web request for making HTTP call.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="config"/>
        /// is null.</exception>
        public static WebRequest BuildRequest(string url, string method, AppConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            WebRequest request = HttpWebRequest.Create(url);

            request.Method = method;
            request.Proxy = config.Proxy;
            request.Timeout = config.Timeout;

            HttpWebRequest httpRequest = request as HttpWebRequest;
            if (httpRequest != null)
            {
                httpRequest.UserAgent = config.GetUserAgent();

                if (config.EnableGzipCompression)
                {
                    httpRequest.AutomaticDecompression =
                        DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                else
                {
                    httpRequest.AutomaticDecompression = DecompressionMethods.None;
                }

                return httpRequest;
            }

            return request;
        }

        /// <summary>
        /// Gets the error response body.
        /// </summary>
        /// <param name="e">The web exception.</param>
        /// <returns></returns>
        public static string GetErrorResponseBody(WebException e)
        {
            WebResponse response = e.Response;
            string contents = e.Message;
            try
            {
                if (response != null)
                {
                    contents =
                        MediaUtilities.GetStreamContentsAsString(response.GetResponseStream());
                }
            }
            catch
            {
                // Nothing much to do here, since this is an exception on top of an
                // exception (e.g. IOException on top of a WebException that was a
                // timeout), and it is enough to return the original WebException.
            }

            return contents;
        }

        /// <summary>
        /// Attempts to write a post body and log the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="postBody">The post body.</param>
        /// <param name="service">The service making this request.</param>
        /// <param name="logEntry">The log entry.</param>
        /// <param name="headersToMask">The headers to mask.</param>
        public static void WritePostBodyAndLog(WebRequest request, string postBody, string service,
            LogEntry logEntry, ISet<string> headersToMask)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(postBody);
                }
            }
            finally
            {
                logEntry.LogRequest(new RequestInfo(request, postBody)
                {
                    Service = service
                }, headersToMask);
            }
        }

        /// <summary>
        /// Updates a URL endpoint with a new host.
        /// </summary>
        /// <param name="originalUri">The original URI.</param>
        /// <param name="newHost">The new host.</param>
        /// <returns>The updated URL.</returns>
        public static string UpdateEndpointHostInUrl(string originalUri, string newHost)
        {
            Uri newHostUri = new Uri(newHost);
            return new UriBuilder(originalUri)
            {
                Host = newHostUri.Host,
                Port = newHostUri.Port,
                Scheme = newHostUri.Scheme
            }.Uri.ToString();
        }
    }
}
