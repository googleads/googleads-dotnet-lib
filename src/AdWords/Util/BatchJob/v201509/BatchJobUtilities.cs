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
    /// The minimal chunk size to be used for resumable upload (256KB).
    /// </summary>
    private const int CHUNK_SIZE_ALIGN = 256 * 1024;

    /// <summary>
    /// The default chunk size to be used for resumable upload (4MB).
    /// </summary>
    private const int DEFAULT_CHUNK_SIZE = 4 * 1024 * 1024;
    
    /// <summary>
    /// The chunk size to be used for resumable upload.
    /// </summary>
    private readonly int CHUNK_SIZE;

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchJobUtilities"/>
    /// class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    public BatchJobUtilities(AdsUser user) : this(user, DEFAULT_CHUNK_SIZE) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchJobUtilities"/>
    /// class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    /// <param name="chunkSize">The chunk size to use for resumable upload.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="chunkSie"/>
    /// is not a multiple of 256KB.</exception>
    public BatchJobUtilities(AdsUser user, int chunkSize) {
      if ((chunkSize % CHUNK_SIZE_ALIGN) != 0) {
        throw new ArgumentException("Chunk size should be a multiple of 256KB.");
      }
      this.user = user;
      this.CHUNK_SIZE = chunkSize;
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
      Upload(url, operations, false);
    }

    /// <summary>
    /// Uploads the operations to a specified URL.
    /// </summary>
    /// <param name="url">The temporary URL returned by a batch job.</param>
    /// <param name="operations">The list of operations.</param>
    /// <param name="resumePreviousUpload">True, if a previously interrupted
    /// upload should be resumed.</param>
    public void Upload(string url, Operation[] operations, bool resumePreviousUpload) {
      byte[] postBody = Encoding.UTF8.GetBytes(GetPostBody(operations));

      int totalUploaded = 0;
      int length = postBody.Length;

      if (resumePreviousUpload) {
        totalUploaded = GetUploadProgress(url, length);
      }

      while (totalUploaded < length) {
        int start = totalUploaded;
        int end = (totalUploaded + CHUNK_SIZE - 1 < postBody.Length - 1) ?
            totalUploaded + CHUNK_SIZE - 1 : postBody.Length - 1;
        int bytesToWrite = end - start + 1;
        try {
          UploadChunk(url, postBody, start, end);
          totalUploaded += bytesToWrite;
        } catch (Exception e) {
          throw new System.ApplicationException("Failed to upload operations for batch job.", e);
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
        WebRequest request = HttpUtilities.BuildRequest(url, "GET", user.Config);

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
    /// Gets the upload progress.
    /// </summary>
    /// <param name="url">The resumable upload URL.</param>
    /// <param name="totalLength">The total length of upload.</param>
    /// <returns>The number of bytes uploaded so far.</returns>
    private int GetUploadProgress(string url, int totalLength) {
      int retval = 0;
      BulkJobErrorHandler errorHandler = new BulkJobErrorHandler(user);
      while (true) {
        WebResponse response = null;
        WebRequest request = HttpUtilities.BuildRangeRequest(url, 0,
            string.Format("bytes */{0}", totalLength), user.Config);

        LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());
        logEntry.LogRequest(request, "Truncated", HEADERS_TO_MASK);

        try {
          response = request.GetResponse();
        } catch (WebException e) {
          if (IsPartialUploadSuccessResponse(e)) {
            retval = ExtractUpperRange(e.Response.Headers["Range"], retval);

            logEntry.LogResponse(e.Response, true, "");
            logEntry.Flush();
            break;
          } else {
            HandleCloudException(errorHandler, logEntry, e);
          }
        }
      }
      return retval;
    }

    /// <summary>
    /// Extracts the upper range from a range header.
    /// </summary>
    /// <param name="rangeHeader">The range header.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The upper range from the header value, or
    /// <paramref name="defaultValue"/> if the header value cannot be parsed
    /// properly.</returns>
    private int ExtractUpperRange(string rangeHeader, int defaultValue) {
      int retval = defaultValue;
      if (rangeHeader != null) {
        string[] parts = rangeHeader.Split('-');
        if (parts.Length == 2) {
          int.TryParse(parts[1], out retval);
        }
      }
      return retval;
    }

    /// <summary>
    /// Uploads a chunk of data for the batch job.
    /// </summary>
    /// <param name="url">The resumable upload URL.</param>
    /// <param name="postBody">The post body.</param>
    /// <param name="start">The start of range of bytes to be uploaded.</param>
    /// <param name="end">The end of range of bytes to be uploaded.</param>
    private void UploadChunk(string url, byte[] postBody, int start, int end) {
      BulkJobErrorHandler errorHandler = new BulkJobErrorHandler(user);

      while (true) {
        WebResponse response = null;
        LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());

        int bytesToWrite = end - start + 1;
        HttpWebRequest request = (HttpWebRequest) HttpUtilities.BuildRangeRequest(
            url, bytesToWrite,
            string.Format("bytes {0}-{1}/{2}", start, end, postBody.Length), user.Config);

        request.ContentType = "application/xml";

        try {
          using (Stream requestStream = request.GetRequestStream()) {
            requestStream.Write(postBody, start, bytesToWrite);
          }

          logEntry.LogRequest(request, "Truncated", HEADERS_TO_MASK);

          response = request.GetResponse();

          logEntry.LogResponse(response, true, "");
          logEntry.Flush();
          return;
        } catch (WebException e) {
          response = e.Response;
          if (IsPartialUploadSuccessResponse(e)) {
            logEntry.LogResponse(e.Response, true, "");
            logEntry.Flush();
            return;
          } else {
            HandleCloudException(errorHandler, logEntry, e);
          }
        }
      }
    }

    /// <summary>
    /// Determines whether this WebException represents a partial upload
    /// success or not.
    /// </summary>
    /// <param name="e">The web exception.</param>
    /// <returns>true, if this exception represents a successful partial
    /// upload, false otherwise.</returns>
    private bool IsPartialUploadSuccessResponse(WebException e) {
      WebResponse response = e.Response;
      return (int) ((response as HttpWebResponse).StatusCode) == 308;
    }

    /// <summary>
    /// Handles the exception from Google Cloud Storage servers when uploading
    /// operations.
    /// </summary>
    /// <param name="errorHandler">The error handler.</param>
    /// <param name="logEntry">The log entry.</param>
    /// <param name="e">The web exception that was thrown by the server.</param>
    /// <returns>True if this is a success, false if this was a server error.
    /// </returns>
    private void HandleCloudException(BulkJobErrorHandler errorHandler, LogEntry logEntry,
        WebException e) {
      Exception downloadException = null;

      using (WebResponse response = e.Response) {
        string contents = HttpUtilities.GetErrorResponseBody(e);

        logEntry.LogResponse(response, false, contents);
        logEntry.Flush();

        downloadException = ParseException(e, contents);

        if (errorHandler.ShouldRetry(downloadException)) {
          errorHandler.PrepareForRetry(downloadException);
        } else {
          throw downloadException;
        }
      }
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
    private Exception ParseException(WebException e, string contents) {
      try {
        return new AdWordsBulkRequestException("A Google cloud storage exception occurred.", e,
            contents);
      } catch (Exception) {
        return e;
      }
    }
  }
}
