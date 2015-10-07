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
using Google.Api.Ads.AdWords.v201509;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;

namespace Google.Api.Ads.AdWords.Util.BatchJob.v201509 {

  /// <summary>
  /// Utility methods to upload operations for a batch job, and download the
  /// results.
  /// </summary>
  public class BatchJobUtilities {

    /// <summary>
    /// The user associated with this object.
    /// </summary>
    private AdsUser user;

    /// <summary>
    /// The list of headers to mask in the logs.
    /// </summary>
    private readonly HashSet<string> HEADERS_TO_MASK = new HashSet<string> {
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchJobUtilities"/>
    /// class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    public BatchJobUtilities(AdsUser user) {
      this.user = user;
    }

    /// <summary>
    /// Returns the user associated with this object.
    /// </summary>
    public AdsUser User {
      get {
        return user;
      }
    }

    /// <summary>
    /// Gets the post body for sending a request.
    /// </summary>
    /// <param name="operations">The list of operations.</param>
    /// <returns>The POST body, for using in the web request.</returns>
    private string GetPostBody(Operation[] operations) {
      BatchJobMutateRequest request = new BatchJobMutateRequest() {
        operations = operations.ToArray()
      };
      return SerializationUtilities.SerializeAsXmlText(request);
    }

    /// <summary>
    /// Uploads the operations to a specified URL.
    /// </summary>
    /// <param name="url">The temporary URL returned by a batch job.</param>
    /// <param name="operations">The list of operations.</param>
    public void Upload(string url, Operation[] operations) {
      BulkJobErrorHandler errorHandler = new BulkJobErrorHandler(user);
      string postBody = GetPostBody(operations);

      while (true) {
        WebResponse response = null;
        HttpWebRequest request = BuildRequest(url, postBody);

        LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());
        logEntry.LogRequest(request, postBody, HEADERS_TO_MASK);

        try {
          response = request.GetResponse();
          string contents = MediaUtilities.GetStreamContentsAsString(
                  response.GetResponseStream());
          logEntry.LogResponse(response, false, contents);
          logEntry.Flush();
          break;
        } catch (WebException e) {
          HandleCloudException(errorHandler, logEntry, e);
        }
      }
    }

    /// <summary>
    /// Downloads the batch job results from a specified URL.
    /// </summary>
    /// <param name="url">The download URL from a batch job.</param>
    /// <returns>The results from the batch job.</returns>
    public BatchJobMutateResponse Download(string url) {
      BulkJobErrorHandler errorHandler = new BulkJobErrorHandler(user);

      while (true) {
        WebRequest request = HttpWebRequest.Create(url);
        request.Method = "GET";

        WebResponse response = null;

        LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());
        logEntry.LogRequest(request, "", HEADERS_TO_MASK);

        try {
          response = request.GetResponse();
          string contents = MediaUtilities.GetStreamContentsAsString(
                  response.GetResponseStream());
          logEntry.LogResponse(response, false, contents);
          logEntry.Flush();

          return ParseResponse(contents);
        } catch (WebException e) {
          HandleCloudException(errorHandler, logEntry, e);
        }
      }
    }

    /// <summary>
    /// Handles the exception from Google Cloud Storage servers when uploading
    /// operations.
    /// </summary>
    /// <param name="errorHandler">The error handler.</param>
    /// <param name="logEntry">The log entry.</param>
    /// <param name="e">The web exception that was thrown by the server.</param>
    private void HandleCloudException(BulkJobErrorHandler errorHandler, LogEntry logEntry,
        WebException e) {
      string contents = "";
      Exception downloadException = null;

      using (WebResponse response = e.Response) {
        try {
          contents = MediaUtilities.GetStreamContentsAsString(
              response.GetResponseStream());
        } catch {
          contents = e.Message;
        }

        logEntry.LogResponse(response, true, contents);
        logEntry.Flush();

        downloadException = ParseException(e, contents);
      }
      if (errorHandler.ShouldRetry(downloadException)) {
        errorHandler.PrepareForRetry(downloadException);
      } else {
        throw downloadException;
      }
      return;
    }

    /// <summary>
    /// Parses the response from Google Cloud Storage servers.
    /// </summary>
    /// <param name="contents">The response body.</param>
    /// <returns>A BatchJobMutateResponse object, generated by parsing the
    /// response from the server.</returns>
    private BatchJobMutateResponse ParseResponse(string contents) {
      XmlDocument xDoc = new XmlDocument();
      xDoc.LoadXml(contents);

      string wrappedXml = string.Format(@"
          <?xml version='1.0' encoding='UTF-8'?>
          <root xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
                xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
            {0}
          </root>", xDoc.DocumentElement.OuterXml).Trim();

      BatchJobMutateResponseEnvelope mutateResponseEnvelope =
          (BatchJobMutateResponseEnvelope) SerializationUtilities.DeserializeFromXmlText(
              wrappedXml, typeof(BatchJobMutateResponseEnvelope));
      return mutateResponseEnvelope.mutateResponse;
    }

    /// <summary>
    /// Parses an error response from Google Cloud Storage servers..
    /// </summary>
    /// <param name="e">The web exception from the server.</param>
    /// <param name="contents">The error contents from the server.</param>
    /// <returns>A parsed exception.</returns>
    public Exception ParseException(WebException e, string contents) {
      return new AdWordsBulkRequestException("A Google cloud storage exception occurred.", e,
          contents);
    }

    /// <summary>
    /// Builds an HTTP request for uploading operations.
    /// </summary>
    /// <param name="uploadUrl">The upload URL for the batch job.</param>
    /// <param name="postBody">The POST body.</param>
    /// <returns>A webrequest to upload operations to the batch job.</returns>
    private HttpWebRequest BuildRequest(string uploadUrl, string postBody) {
      AdWordsAppConfig config = this.User.Config as AdWordsAppConfig;

      byte[] data = Encoding.UTF8.GetBytes(postBody);

      HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(uploadUrl);
      request.Method = "POST";
      request.Proxy = config.Proxy;
      request.Timeout = config.Timeout;
      request.UserAgent = config.GetUserAgent();

      request.ContentType = "application/xml";
      request.ContentLength = data.Length;
      if (config.EnableGzipCompression) {
        (request as HttpWebRequest).AutomaticDecompression = DecompressionMethods.GZip
            | DecompressionMethods.Deflate;
      } else {
        (request as HttpWebRequest).AutomaticDecompression = DecompressionMethods.None;
      }

      using (StreamWriter writer = new StreamWriter(request.GetRequestStream())) {
        writer.Write(postBody);
      }
      return request;
    }
  }
}
