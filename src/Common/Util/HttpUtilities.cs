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

using System;
using System.Net;

namespace Google.Api.Ads.Common.Util {

  /// <summary>
  /// Utility class for various HTTP tasks.
  /// </summary>
  public class HttpUtilities {

    /// <summary>
    /// Builds an HTTP PUT request with a Range header.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="contentLength">Length of the content.</param>
    /// <param name="range">The range heaer notation.</param>
    /// <returns>The web request for making HTTP call.</returns>
    private static WebRequest BuildRangeRequest(string url, int contentLength, string range) {
      return BuildRangeRequest(url, contentLength, range, new AppConfigBase());
    }

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
        AppConfig config) {
      if (config == null) {
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
    /// <returns>The web request for making HTTP call.</returns>
    private static WebRequest BuildRequest(string url, string method) {
      return BuildRequest(url, method, new AppConfigBase());
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
    public static WebRequest BuildRequest(string url, string method, AppConfig config) {
      if (config == null) {
        throw new ArgumentNullException("config");
      }

      HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url);

      request.Method = method;
      request.Proxy = config.Proxy;
      request.Timeout = config.Timeout;
      request.UserAgent = config.GetUserAgent();

      if (config.EnableGzipCompression) {
        (request as HttpWebRequest).AutomaticDecompression = DecompressionMethods.GZip
            | DecompressionMethods.Deflate;
      } else {
        (request as HttpWebRequest).AutomaticDecompression = DecompressionMethods.None;
      }
      return request;
    }

    /// <summary>
    /// Gets the error response body.
    /// </summary>
    /// <param name="e">The web exception.</param>
    /// <returns></returns>
    public static string GetErrorResponseBody(WebException e) {
      WebResponse response = e.Response;
      string contents = "";
      try {
        contents = MediaUtilities.GetStreamContentsAsString(
            response.GetResponseStream());
      } catch {
        contents = e.Message;
      }
      return contents;
    }
  }
}