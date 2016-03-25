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
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Xml;

namespace Google.Api.Ads.AdWords.Util.BatchJob {

  /// <summary>
  /// Utility methods to upload operations for a batch job, and download the
  /// results.
  /// </summary>
  public class BatchJobUtilitiesBase {

    /// <summary>
    /// The registry for saving feature usage information..
    /// </summary>
    protected AdsFeatureUsageRegistry featureUsageRegistry = AdsFeatureUsageRegistry.Instance;

    /// <summary>
    /// The feature ID for this class.
    /// </summary>
    protected const AdsFeatureUsageRegistry.Features FEATURE_ID =
        AdsFeatureUsageRegistry.Features.BatchJobHelper;

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
    protected const int CHUNK_SIZE_ALIGN = 256 * 1024;

    /// <summary>
    /// The default chunk size to be used for resumable upload (32MB).
    /// </summary>
    private const int DEFAULT_CHUNK_SIZE = 32 * 1024 * 1024;

    /// <summary>
    /// The chunk size to be used for resumable upload.
    /// </summary>
    private int CHUNK_SIZE;

    /// <summary>
    /// A flag to choose determine whether chunking should be used when
    /// uploading operations.
    /// </summary>
    private bool useChunking;

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchJobUtilities"/>
    /// class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    public BatchJobUtilitiesBase(AdsUser user)
      : this(user, false, DEFAULT_CHUNK_SIZE) {
    }

    /// <summary>
    /// Determines if the code is running on mono.
    /// </summary>
    private static bool IsRunningOnMono() {
      return Type.GetType("Mono.Runtime") != null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchJobUtilities"/>
    /// class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    /// <param name="useChunking">if the operations should be broken into
    /// smaller chunks before uploading to the server.</param>
    /// <param name="chunkSize">The chunk size to use for resumable upload.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="chunkSie"/>
    /// is not a multiple of 256KB.</exception>
    /// <remarks>Use chunking if your network is spotty for uploads, or if it
    /// has restrictions such as speed limits or timeouts. Chunking makes your
    /// upload reliable when the network is unreliable, but it is inefficient
    /// over a good connection, since an HTTPs request has to be made for every
    /// chunk being uploaded.</remarks>
    public BatchJobUtilitiesBase(AdsUser user, bool useChunking, int chunkSize) {
      Init(user, useChunking, chunkSize);
    }

    protected void Init(AdsUser user, bool useChunking, int chunkSize) {
      if (IsRunningOnMono() && useChunking) {
        // https://bugzilla.xamarin.com/show_bug.cgi?id=28287.
        // 308 gets interpreted as a ProtocolError, and mono nulls out WebException.Response.
        throw new ArgumentException("Chunked mode is not supported in mono.");
      }

      this.useChunking = useChunking;
      if (useChunking && (chunkSize % CHUNK_SIZE_ALIGN) != 0) {
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
    /// Generates a resumable upload URL for a job. This method should be used prior
    /// to calling the Upload() method when using API version >=v201601.
    /// </summary>
    /// <returns>The resumable upload URL.</returns>
    /// <param name="url">The temporary upload URL from BatchJobService.</param>
    public string GetResumableUploadUrl(string url) {
      WebRequest request = HttpUtilities.BuildRequest(url, "POST", user.Config);
      request.ContentType = "application/xml";
      request.ContentLength = 0;
      request.Headers["x-goog-resumable"] = "start";

      WebResponse response = null;

      LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());
      logEntry.LogRequest(request, "", HEADERS_TO_MASK);

      try {
        response = request.GetResponse();
        string contents = MediaUtilities.GetStreamContentsAsString(
            response.GetResponseStream());
        logEntry.LogResponse(response, false, contents);
        logEntry.Flush();
        return response.Headers["Location"];
      } catch (WebException e) {
        string contents = HttpUtilities.GetErrorResponseBody(e);
        logEntry.LogResponse(e.Response, false, contents);
        logEntry.Flush();
        throw ParseException(e, contents);
      } finally {
        if (response != null) {
          response.Close();
        }
      }
    }

    protected void Upload(string url, bool resumePreviousUpload, byte[] postBody) {
      int totalUploaded = 0;
      int length = postBody.Length;

      if (resumePreviousUpload) {
        totalUploaded = GetUploadProgress(url);
      }

      while (totalUploaded < length) {
        int start = totalUploaded;
        int end = 0;

        if (this.useChunking) {
          // The payload should further be broken down into smaller chunks.
          end = (totalUploaded + CHUNK_SIZE - 1 < postBody.Length - 1) ?
             totalUploaded + CHUNK_SIZE - 1 : postBody.Length - 1;
        } else {
          // No need to split the payload, upload the whole content in one
          // single request.
          end = postBody.Length - 1;
        }

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
    protected string DownloadResults(string url) {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);

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

          return contents;
        } catch (WebException e) {
          HandleCloudException(errorHandler, logEntry, e);
        } finally {
          if (response != null) {
            response.Close();
          }
        }
      }
    }

    /// <summary>
    /// Gets the upload progress.
    /// </summary>
    /// <param name="url">The resumable upload URL.</param>
    /// <param name="totalLength">The total length of upload.</param>
    /// <returns>The number of bytes uploaded so far.</returns>
    protected virtual int GetUploadProgress(string url) {
      int totalLength = 0;
      int retval = 0;
      BulkJobErrorHandler errorHandler = new BulkJobErrorHandler(user);
      while (true) {
        WebResponse response = null;

        // As per https://cloud.google.com/storage/docs/resumable-uploads-xml#step_4wzxhzdk17query_title_for_the_upload_status,
        // one should be passing bytes */Length, where length is the actual
        // length of bytes that was being uploaded during the request that was
        // interrupted. In practice, passing length as 0 also works.
        // MOE:begin_strip
        // TODO(Anash): Investigate a long term fix. This solution works for now
        // because GCE implementation is more liberal than what the actual
        // HTTP protocol requires.
        // MOE:end_strip
        WebRequest request = HttpUtilities.BuildRangeRequest(url, 0,
            string.Format("bytes */{0}", totalLength), user.Config);

        LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());
        logEntry.LogRequest(request, "Truncated", HEADERS_TO_MASK);

        try {
          response = request.GetResponse();

          // This block of code is hit if the user uploaded without chunking and
          // then called this method.
          string contents = MediaUtilities.GetStreamContentsAsString(
            response.GetResponseStream());
          JavaScriptSerializer serializer = new JavaScriptSerializer();
          Dictionary<string, object> temp =
              (Dictionary<string, object>) serializer.DeserializeObject(contents);
          int.TryParse(temp["size"].ToString(), out retval);
          logEntry.LogResponse(response, true, "");
          logEntry.Flush();
          break;
        } catch (WebException e) {
          // This block of code is hit if if chunking is enabled and the
          // operations upload is incomplete. The server responds with a 308
          // status code. See
          // https://cloud.google.com/storage/docs/resumable-uploads-xml#step_4wzxhzdk17query_title_for_the_upload_status
          // for more details.
          if (IsPartialUploadSuccessResponse(e)) {
            retval = ExtractUpperRange(e.Response.Headers["Range"], retval);

            logEntry.LogResponse(e.Response, true, "");
            logEntry.Flush();
            break;
          } else {
            HandleCloudException(errorHandler, logEntry, e);
          }
        } finally {
          if (response != null) {
            response.Close();
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
    protected virtual void UploadChunk(string url, byte[] postBody, int start, int end) {
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
          logEntry.LogRequest(request, "Truncated", HEADERS_TO_MASK);

          using (Stream requestStream = request.GetRequestStream()) {
            requestStream.Write(postBody, start, bytesToWrite);
          }

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
        } finally {
          if (response != null) {
            response.Close();
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
      HttpWebResponse response = e.Response as HttpWebResponse;
      return response != null && (int) response.StatusCode == 308;
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
    /// Parses the XML response from the server into a type object.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="contents">The XML contents.</param>
    /// <returns>The parsed object</returns>
    protected T ParseResponse<T>(string contents) {
      XmlDocument xDoc = new XmlDocument();
      xDoc.LoadXml(contents);

      string wrappedXml = string.Format(@"
          <?xml version='1.0' encoding='UTF-8'?>
          <root xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
                xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
            {0}
          </root>", xDoc.DocumentElement.OuterXml).Trim();

      return (T) SerializationUtilities.DeserializeFromXmlText(
          wrappedXml, typeof(T));
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
