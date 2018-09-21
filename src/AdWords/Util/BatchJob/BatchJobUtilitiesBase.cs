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
using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Util;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Google.Api.Ads.AdWords.Util.BatchJob
{
    /// <summary>
    /// Utility methods to upload operations for a batch job, and download the
    /// results.
    /// </summary>
    public class BatchJobUtilitiesBase
    {
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
        /// The polling interval base to be used for exponential backoff.
        /// </summary>
        protected const int POLL_INTERVAL_SECONDS_BASE = 30;

        /// <summary>
        /// The postamble for streamed uploads.
        /// </summary>
        /// <remarks>This is the trailing string when serializing BatchJobMutateRequest
        /// type.</remarks>
        protected const string POSTAMBLE = "</mutate>";

        /// <summary>
        /// The user associated with this object.
        /// </summary>
        private AdsUser user;

        /// <summary>
        /// The list of headers to mask in the logs.
        /// </summary>
        private readonly HashSet<string> HEADERS_TO_MASK = new HashSet<string>
        {
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
        protected int CHUNK_SIZE;

        /// <summary>
        /// A flag to choose determine whether chunking should be used when
        /// uploading operations.
        /// </summary>
        private bool useChunking;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchJobUtilitiesBase"/>
        /// class.
        /// </summary>
        /// <param name="user">AdWords user to be used along with this
        /// utilities object.</param>
        public BatchJobUtilitiesBase(AdsUser user) : this(user, false, DEFAULT_CHUNK_SIZE)
        {
        }

        /// <summary>
        /// Determines if the code is running on mono.
        /// </summary>
        private static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchJobUtilitiesBase"/>
        /// class.
        /// </summary>
        /// <param name="user">AdWords user to be used along with this
        /// utilities object.</param>
        /// <param name="useChunking">if the operations should be broken into
        /// smaller chunks before uploading to the server.</param>
        /// <param name="chunkSize">The chunk size to use for resumable upload.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="chunkSize"/>
        /// is not a multiple of 256KB.</exception>
        /// <remarks>Use chunking if your network is spotty for uploads, or if it
        /// has restrictions such as speed limits or timeouts. Chunking makes your
        /// upload reliable when the network is unreliable, but it is inefficient
        /// over a good connection, since an HTTPs request has to be made for every
        /// chunk being uploaded.</remarks>
        public BatchJobUtilitiesBase(AdsUser user, bool useChunking, int chunkSize)
        {
            Init(user, useChunking, chunkSize);
        }

        /// <summary>
        /// Initializes the class.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="useChunking">if the operations should be broken into
        /// smaller chunks before uploading to the server.</param>
        /// <param name="chunkSize">The chunk size to use for resumable upload.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="chunkSize"/>
        /// is not a multiple of 256KB.</exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown if chunked mode is used on Mono platform, or chunk size is not a
        /// multiple of 256KB.
        /// </exception>
        protected void Init(AdsUser user, bool useChunking, int chunkSize)
        {
            if (IsRunningOnMono() && useChunking)
            {
                // https://bugzilla.xamarin.com/show_bug.cgi?id=28287.
                // 308 gets interpreted as a ProtocolError, and mono nulls out
                // WebException.Response.
                throw new ArgumentException("Chunked mode is not supported in mono.");
            }

            this.useChunking = useChunking;
            if (useChunking && (chunkSize % CHUNK_SIZE_ALIGN) != 0)
            {
                throw new ArgumentException("Chunk size should be a multiple of 256KB.");
            }

            this.user = user;
            this.CHUNK_SIZE = chunkSize;
        }

        /// <summary>
        /// Returns the user associated with this object.
        /// </summary>
        public AdsUser User
        {
            get { return user; }
        }

        /// <summary>
        /// Generates a resumable upload URL for a job. This method should be used prior
        /// to calling the Upload() method when using API version >=v201601.
        /// </summary>
        /// <returns>The resumable upload URL.</returns>
        /// <param name="url">The temporary upload URL from BatchJobService.</param>
        public string GetResumableUploadUrl(string url)
        {
            WebRequest request = HttpUtilities.BuildRequest(url, "POST", user.Config);
            request.ContentType = "application/xml";
            request.ContentLength = 0;
            request.Headers["x-goog-resumable"] = "start";

            WebResponse response = null;

            LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());
            logEntry.LogRequest(GenerateRequestInfo(request, ""), HEADERS_TO_MASK);

            try
            {
                response = request.GetResponse();
                string contents =
                    MediaUtilities.GetStreamContentsAsString(response.GetResponseStream());
                logEntry.LogResponse(GenerateResponseInfo(response, contents, ""));
                logEntry.Flush();
                return response.Headers["Location"];
            }
            catch (WebException e)
            {
                string contents = HttpUtilities.GetErrorResponseBody(e);
                logEntry.LogResponse(GenerateResponseInfo(e.Response, "", contents));
                logEntry.Flush();
                throw ParseException(e, contents);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        /// <summary>
        /// Begins a streamed upload.
        /// </summary>
        /// <param name="url">The upload URL.</param>
        /// <returns>A <see cref="BatchUploadProgress"/> object that tracks the
        /// progress of upload.</returns>
        public BatchUploadProgress BeginStreamUpload(string url)
        {
            return new BatchUploadProgress()
            {
                BytesUploaded = 0,
                Url = url
            };
        }

        /// <summary>
        /// Completes a streamed upload.
        /// </summary>
        /// <param name="uploadProgress">The upload progress.</param>
        /// <returns>The updated <see cref="BatchUploadProgress"/> object.</returns>
        public BatchUploadProgress EndStreamUpload(BatchUploadProgress uploadProgress)
        {
            // Upload the postamble to mark the end of upload.
            List<byte> bytes = new List<byte>(Encoding.UTF8.GetBytes(POSTAMBLE));

            long totalUploadSize = (uploadProgress.BytesUploaded + bytes.Count);
            Upload(uploadProgress.Url, bytes.ToArray(), uploadProgress.BytesUploaded,
                totalUploadSize);
            uploadProgress.BytesUploaded += bytes.Count;
            return uploadProgress;
        }

        /// <summary>
        /// Uploads the operations to a specified URL.
        /// </summary>
        /// <param name="url">The temporary URL returned by a batch job.</param>
        /// <param name="postBody">The data to be uploaded.</param>
        /// <param name="startOffset">The start offset within the upload stream.</param>
        protected void Upload(string url, byte[] postBody, long startOffset)
        {
            Upload(url, postBody, startOffset, postBody.Length);
        }

        /// <summary>
        /// Uploads the operations to a specified URL.
        /// </summary>
        /// <param name="url">The temporary URL returned by a batch job.</param>
        /// <param name="data">The data to be uploaded.</param>
        /// <param name="startOffset">The start offset for uploading data. If you
        /// are performing a streamed upload, then this parameter determines the
        /// file offset to write this data on the server.</param>
        /// <param name="totalUploadSize">If specified, this indicates the total
        /// size of the upload. When doing a streamed upload, this value will be
        /// null for all except the last chunk.</param>
        /// <exception cref="System.ApplicationException">Thrown if the upload fails.
        /// </exception>
        protected void Upload(string url, byte[] data, long startOffset, long? totalUploadSize)
        {
            int totalUploaded = 0;
            int length = data.Length;

            while (totalUploaded < length)
            {
                int start = totalUploaded;
                int end = 0;

                if (this.useChunking)
                {
                    // The payload should further be broken down into smaller chunks.
                    end = (totalUploaded + CHUNK_SIZE < length)
                        ? totalUploaded + CHUNK_SIZE
                        : length;
                }
                else
                {
                    // No need to split the payload, upload the whole content in one
                    // single request.
                    end = length;
                }

                int bytesToWrite = end - start;
                try
                {
                    UploadChunk(url, data, start, end - 1, startOffset, totalUploadSize);
                    totalUploaded += bytesToWrite;
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException(
                        "Failed to upload operations for batch job.", e);
                }
            }
        }

        /// <summary>
        /// Performs a streamed upload.
        /// </summary>
        /// <param name="uploadProgress">The upload progress.</param>
        /// <param name="postBody">The post body to be stream uploaded.</param>
        /// <returns>The updated <see cref="BatchUploadProgress"/> object.</returns>
        protected BatchUploadProgress StreamUpload(BatchUploadProgress uploadProgress,
            string postBody)
        {
            string payloadToUpload = GetPayload(uploadProgress.BytesUploaded, postBody);

            // Pad the payload to match a block boundary.
            List<byte> bytes = new List<byte>(Encoding.UTF8.GetBytes(payloadToUpload));
            int padLength = CHUNK_SIZE_ALIGN - (bytes.Count % CHUNK_SIZE_ALIGN);
            string padding = new String(' ', padLength);
            bytes.AddRange(Encoding.UTF8.GetBytes(padding));

            // Since we don't know the totalUploadSize at this point, we pass a null
            // instead.
            Upload(uploadProgress.Url, bytes.ToArray(), uploadProgress.BytesUploaded, null);
            uploadProgress.BytesUploaded += bytes.Count;
            return uploadProgress;
        }

        /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <param name="bytesUploaded">The bytes uploaded.</param>
        /// <param name="postBody">The post body.</param>
        /// <returns></returns>
        protected static string GetPayload(long bytesUploaded, string postBody)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(postBody);

            // Extract the operations.
            string operationsOnly = xDoc.DocumentElement.InnerXml;

            // Clear the operations from the postBody, so only the envelope remains.
            xDoc.DocumentElement.InnerXml = "";
            string envelope = xDoc.OuterXml;

            // Split the envelope into preamble and postamble.
            int splitPoint = envelope.IndexOf(POSTAMBLE);
            string preamble = envelope.Substring(0, splitPoint);

            string payloadToUpload = "";
            if (bytesUploaded == 0)
            {
                payloadToUpload = preamble + operationsOnly;
            }
            else
            {
                payloadToUpload = operationsOnly;
            }

            return payloadToUpload;
        }

        /// <summary>
        /// Downloads the batch job results from a specified URL.
        /// </summary>
        /// <param name="url">The download URL from a batch job.</param>
        /// <returns>The results from the batch job.</returns>
        protected string DownloadResults(string url)
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            BulkJobErrorHandler errorHandler = new BulkJobErrorHandler(user);

            while (true)
            {
                WebRequest request = HttpUtilities.BuildRequest(url, "GET", user.Config);
                WebResponse response = null;

                LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());
                logEntry.LogRequest(GenerateRequestInfo(request, ""), HEADERS_TO_MASK);

                try
                {
                    response = request.GetResponse();
                    string contents =
                        MediaUtilities.GetStreamContentsAsString(response.GetResponseStream());

                    logEntry.LogResponse(GenerateResponseInfo(response, contents, ""));
                    logEntry.Flush();

                    return contents;
                }
                catch (WebException e)
                {
                    HandleCloudException(errorHandler, logEntry, e);
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the upload progress.
        /// </summary>
        /// <param name="url">The resumable upload URL.</param>
        /// <returns>The number of bytes uploaded so far.</returns>
        protected virtual int GetUploadProgress(string url)
        {
            int totalLength = 0;
            int retval = 0;
            BulkJobErrorHandler errorHandler = new BulkJobErrorHandler(user);
            while (true)
            {
                WebResponse response = null;

                // As per https://cloud.google.com/storage/docs/resumable-uploads-xml#step_4wzxhzdk17query_title_for_the_upload_status,
                // one should be passing bytes */Length, where length is the actual
                // length of bytes that was being uploaded during the request that was
                // interrupted. In practice, passing length as 0 also works.
                WebRequest request = HttpUtilities.BuildRangeRequest(url, 0,
                    string.Format("bytes */{0}", totalLength), user.Config);

                LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());
                logEntry.LogRequest(GenerateRequestInfo(request, ""), HEADERS_TO_MASK);

                try
                {
                    response = request.GetResponse();

                    // This block of code is hit if the user uploaded without chunking and
                    // then called this method.
                    string contents =
                        MediaUtilities.GetStreamContentsAsString(response.GetResponseStream());
                    Dictionary<string, object> temp =
                        JsonConvert.DeserializeObject<Dictionary<string, object>>(contents);
                    int.TryParse(temp["size"].ToString(), out retval);
                    logEntry.LogResponse(GenerateResponseInfo(response, "", ""));
                    logEntry.Flush();
                    break;
                }
                catch (WebException e)
                {
                    // This block of code is hit if if chunking is enabled and the
                    // operations upload is incomplete. The server responds with a 308
                    // status code. See
                    // https://cloud.google.com/storage/docs/resumable-uploads-xml#step_4wzxhzdk17query_title_for_the_upload_status
                    // for more details.
                    if (IsPartialUploadSuccessResponse(e))
                    {
                        retval = ExtractUpperRange(e.Response.Headers["Range"], retval) + 1;

                        logEntry.LogResponse(GenerateResponseInfo(e.Response, "", ""));
                        logEntry.Flush();
                        break;
                    }
                    else
                    {
                        HandleCloudException(errorHandler, logEntry, e);
                    }
                }
                finally
                {
                    if (response != null)
                    {
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
        private int ExtractUpperRange(string rangeHeader, int defaultValue)
        {
            int retval = defaultValue;
            if (rangeHeader != null)
            {
                string[] parts = rangeHeader.Split('-');
                if (parts.Length == 2)
                {
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
        /// <param name="start">The start of range of bytes from the postBody to
        /// be uploaded.</param>
        /// <param name="end">The end of range of bytes from the postBody to be
        /// uploaded.</param>
        /// <param name="startOffset">The start offset in the stream to upload to.</param>
        /// <param name="totalUploadSize">If specified, this indicates the total
        /// size of the upload. When doing a streamed upload, this value will be
        /// null for all except the last chunk.</param>
        protected virtual void UploadChunk(string url, byte[] postBody, int start, int end,
            long startOffset, long? totalUploadSize)
        {
            BulkJobErrorHandler errorHandler = new BulkJobErrorHandler(user);

            string totalUploadSizeForRequest =
                (totalUploadSize == null) ? "*" : totalUploadSize.ToString();

            while (true)
            {
                WebResponse response = null;
                LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());

                long rangeStart = start + startOffset;
                long rangeEnd = end + startOffset;

                int bytesToWrite = end - start + 1;
                string rangeHeader = string.Format("bytes {0}-{1}/{2}", rangeStart, rangeEnd,
                    totalUploadSizeForRequest);
                HttpWebRequest request =
                    (HttpWebRequest) HttpUtilities.BuildRangeRequest(url, bytesToWrite, rangeHeader,
                        user.Config);

                request.ContentType = "application/xml";

                string textToLog = GetTextToLog(postBody, start, bytesToWrite);

                try
                {
                    logEntry.LogRequest(GenerateRequestInfo(request, textToLog), HEADERS_TO_MASK);

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(postBody, start, bytesToWrite);
                    }

                    response = request.GetResponse();

                    logEntry.LogResponse(GenerateResponseInfo(response, "", ""));
                    logEntry.Flush();
                    return;
                }
                catch (WebException e)
                {
                    response = e.Response;
                    if (IsPartialUploadSuccessResponse(e))
                    {
                        logEntry.LogResponse(GenerateResponseInfo(e.Response, "", ""));
                        logEntry.Flush();
                        return;
                    }
                    else
                    {
                        HandleCloudException(errorHandler, logEntry, e);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Attempt to convert a byte array into a UTF-8 string for logging.
        /// </summary>
        /// <param name="bytesToDecode">The bytes to decode.</param>
        /// <param name="start">The start byte index.</param>
        /// <param name="numBytes">The number of bytes to decode.</param>
        /// <returns>The partially decoded string.</returns>
        /// <remarks>It is possible that the encoder cannot decode correctly, if the byte array is
        /// not aligned with a UTF-8 boundary. In such cases, a \uFFFD character is used.</remarks>
        protected static string GetTextToLog(byte[] bytesToDecode, int start, int numBytes)
        {
            return new string(new UTF8Encoding().GetChars(bytesToDecode, start, numBytes));
        }

        /// <summary>
        /// Determines whether this WebException represents a partial upload
        /// success or not.
        /// </summary>
        /// <param name="e">The web exception.</param>
        /// <returns>true, if this exception represents a successful partial
        /// upload, false otherwise.</returns>
        private bool IsPartialUploadSuccessResponse(WebException e)
        {
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
            WebException e)
        {
            Exception downloadException = null;

            using (WebResponse response = e.Response)
            {
                string contents = HttpUtilities.GetErrorResponseBody(e);
                logEntry.LogResponse(GenerateResponseInfo(response, "", contents));
                logEntry.Flush();

                downloadException = ParseException(e, contents);

                if (errorHandler.ShouldRetry(downloadException))
                {
                    errorHandler.PrepareForRetry(downloadException);
                }
                else
                {
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
        protected T ParseResponse<T>(string contents)
        {
            XmlDocument xDoc = XmlUtilities.CreateDocument(contents);

            string wrappedXml = string.Format(@"
          <?xml version='1.0' encoding='UTF-8'?>
          <root xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
                xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
            {0}
          </root>", xDoc.DocumentElement.OuterXml).Trim();

            return (T) SerializationUtilities.DeserializeFromXmlText(wrappedXml, typeof(T));
        }

        /// <summary>
        /// Parses an error response from Google Cloud Storage servers..
        /// </summary>
        /// <param name="e">The web exception from the server.</param>
        /// <param name="contents">The error contents from the server.</param>
        /// <returns>A parsed exception.</returns>
        private Exception ParseException(WebException e, string contents)
        {
            try
            {
                return new AdWordsBulkRequestException("A Google cloud storage exception occurred.",
                    e, contents);
            }
            catch (Exception)
            {
                return e;
            }
        }

        private RequestInfo GenerateRequestInfo(WebRequest request, string body)
        {
            return new RequestInfo(request, body)
            {
                Service = "BatchJobService",
                Method = "mutate",
                IdentifierName = "clientCustomerId",
                IdentifierValue = ((AdWordsAppConfig) this.user.Config).ClientCustomerId
            };
        }

        private ResponseInfo GenerateResponseInfo(WebResponse response, string body, string error)
        {
            return new ResponseInfo(response, body)
            {
                ErrorMessage = error
            };
        }
    }
}
