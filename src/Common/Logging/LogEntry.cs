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
using Google.Api.Ads.Common.Util;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Google.Api.Ads.Common.Logging
{
    /// <summary>
    /// Helper class for logging.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// The application configuration.
        /// </summary>
        private AppConfig config;

        /// <summary>
        /// The date and time provider.
        /// </summary>
        private DateTimeProvider dateTimeProvider;

        /// <summary>
        /// The flag to indicate whether this request is a failure or not.
        /// </summary>
        private bool isFailure;

        /// <summary>
        /// The ITraceWriter to use when writing logs.
        /// </summary>
        internal ITraceWriter TraceWriter { get; set; }

        /// <summary>
        /// Gets or sets the request info.
        /// </summary>
        private RequestInfo requestInfo;

        /// <summary>
        /// Gets or sets the response info.
        /// </summary>
        private ResponseInfo responseInfo;

        /// <summary>
        /// The max length of a summary log error message.
        /// </summary>
        private const int MAX_SUMMARY_ERROR_LENGTH = 16000;

        /// <summary>
        /// The string to use when truncating a summary log error.
        /// </summary>
        private const string ELLIPSIS = "...";

        /// <summary>
        /// A regular expression used to find newlines.
        /// </summary>
        private static readonly Regex NEWLINE_REGEX = new Regex("\\n", RegexOptions.Compiled);

        /// <summary>
        /// Gets or sets the detailed request log.
        /// </summary>
        public string DetailedRequestLog { get; private set; }

        /// <summary>
        /// Gets or sets the detailed response log.
        /// </summary>
        public string DetailedResponseLog { get; private set; }

        /// <summary>
        /// Gets the detailed log.
        /// </summary>
        public string DetailedLog
        {
            get { return this.DetailedRequestLog + this.DetailedResponseLog; }
        }

        /// <summary>
        /// Gets the summary log.
        /// </summary>
        public string SummaryLog
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Request made: " +
                    "Host: {0}, Service: {1}, Method: {2}, {3}: {4}, Request ID: {5}, " +
                    "ResponseTime(ms): {6}, OperationsCount: {7}, IsFault: {8}, FaultMessage: {9}",
                    requestInfo.Uri.Host, requestInfo.Service, requestInfo.Method,
                    requestInfo.IdentifierName, requestInfo.IdentifierValue, responseInfo.RequestId,
                    responseInfo.ResponseTimeMs, responseInfo.OperationCount, this.isFailure,
                    TruncateErrorMessage(responseInfo.ErrorMessage));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="config">The application configuration.</param>
        /// <param name="dateTimeProvider">The date and time provider.</param>
        public LogEntry(AppConfig config, DateTimeProvider dateTimeProvider) : this(config,
            dateTimeProvider, new DefaultTraceWriter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="config">The application configuration.</param>
        /// <param name="dateTimeProvider">The date and time provider.</param>
        /// <param name="traceWriter">The trace writer to write with.</param>
        internal LogEntry(AppConfig config, DateTimeProvider dateTimeProvider,
            ITraceWriter traceWriter)
        {
            this.config = config;
            this.dateTimeProvider = dateTimeProvider;
            TraceWriter = traceWriter;
        }

        /// <summary>
        /// Logs an HTTP request.
        /// </summary>
        /// <param name="requestInfo">The request information.</param>
        /// <param name="headersToMask">The headers to mask.</param>
        public void LogRequest(RequestInfo requestInfo, ISet<string> headersToMask)
        {
            TraceFormatter formatter = GetTraceFormatterForHTTPRequests(config.MaskCredentials);
            LogRequest(requestInfo, headersToMask, formatter);
        }

        /// <summary>
        /// Logs an HTTP request.
        /// </summary>
        /// <param name="requestInfo">The request information.</param>
        /// <param name="headersToMask">The headers to mask.</param>
        /// <param name="formatter">The <see cref="TraceFormatter"/> to use when
        /// formatting the message.</param>
        /// <returns>The request logs</returns>
        public void LogRequest(RequestInfo requestInfo, ISet<string> headersToMask,
            TraceFormatter formatter)
        {
            this.requestInfo = requestInfo;
            this.DetailedRequestLog = GetFormattedRequestLog(requestInfo, headersToMask, formatter);
        }

        /// <summary>
        /// Logs an HTTP response.
        /// </summary>
        /// <param name="responseInfo">The response information.</param>
        public void LogResponse(ResponseInfo responseInfo)
        {
            LogResponse(responseInfo, new HashSet<string>(), new DefaultBodyFormatter());
        }

        /// <summary>
        /// Logs an HTTP response.
        /// </summary>
        /// <param name="responseInfo">The response information.</param>
        /// <param name="fieldsToMask">The list of fields to mask.</param>
        /// <param name="formatter">The formatter to be used for formatting the
        /// response logs.</param>
        public void LogResponse(ResponseInfo responseInfo, ISet<string> fieldsToMask,
            TraceFormatter formatter)
        {
            this.responseInfo = responseInfo;
            this.isFailure = !string.IsNullOrEmpty(responseInfo.ErrorMessage);
            LogResponseDetails(responseInfo, fieldsToMask, formatter);
        }

        /// <summary>
        /// Logs the details of an HTTP response.
        /// </summary>
        /// <param name="responseInfo">The response information.</param>
        /// <param name="fieldsToMask">The list of fields to mask.</param>
        /// <param name="formatter">The formatter to be used for formatting the
        /// response logs.</param>
        public void LogResponseDetails(ResponseInfo responseInfo, ISet<string> fieldsToMask,
            TraceFormatter formatter)
        {
            if (config.MaskCredentials)
            {
                this.DetailedResponseLog =
                    GetFormattedResponseLog(responseInfo, fieldsToMask, formatter);
            }
            else
            {
                this.DetailedResponseLog = GetFormattedResponseLog(responseInfo);
            }
        }

        /// <summary>
        /// Writes the HTTP logs.
        /// </summary>
        public void Flush()
        {
            TraceWriter.WriteDetailedRequestLogs(this.DetailedLog, isFailure);
            TraceWriter.WriteSummaryRequestLogs(this.SummaryLog, isFailure);
        }

        /// <summary>
        /// Gets the formatted logs for an HTTP request.
        /// </summary>
        /// <param name="requestInfo">The request information.</param>
        /// <param name="headersToMask">The headers to mask.</param>
        /// <param name="traceFormatter">The trace formatter to use when formatting
        /// the message.</param>
        /// <returns></returns>
        private string GetFormattedRequestLog(RequestInfo requestInfo, ISet<string> headersToMask,
            TraceFormatter traceFormatter)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n-----------------BEGIN API CALL---------------------\r\n");
            builder.AppendFormat("\r\nRequest\r\n");
            builder.AppendFormat("-------\r\n\r\n");

            StringBuilder headerBuilder = new StringBuilder();

            headerBuilder.AppendFormat("{0} {1}\r\n", requestInfo.HttpMethod,
                requestInfo.Uri.AbsolutePath);
            headerBuilder.AppendFormat("{0}",
                GetFormattedHttpHeaderLogs(MaskHeaders(requestInfo.Headers, headersToMask),
                    "\r\n"));
            builder.AppendFormat("{0}\r\n\r\n{1}\r\n", headerBuilder.ToString(),
                traceFormatter.MaskContents(requestInfo.Body, headersToMask));
            return builder.ToString();
        }

        /// <summary>
        /// Gets the formatted logs for an HTTP response.
        /// </summary>
        /// <param name="responseInfo">The response information.</param>
        /// <returns>The response log text.</returns>
        private string GetFormattedResponseLog(ResponseInfo responseInfo)
        {
            return GetFormattedResponseLog(responseInfo, new HashSet<string>(),
                new DefaultBodyFormatter());
        }

        /// <summary>
        /// Gets the formatted logs for an HTTP response.
        /// </summary>
        /// <param name="responseInfo">The response information.</param>
        /// <param name="headersToMask">The headers to mask.</param>
        /// <param name="traceFormatter">The trace formatter to use when formatting
        /// the message.</param>
        /// <returns>The response log text.</returns>
        private string GetFormattedResponseLog(ResponseInfo responseInfo,
            ISet<string> headersToMask, TraceFormatter traceFormatter)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\nResponse\r\n");
            builder.AppendFormat("--------\r\n");

            builder.AppendFormat("\r\n{0}\r\n\r\n{1}\r\n",
                GetFormattedHttpHeaderLogs(MaskHeaders(responseInfo.Headers, headersToMask),
                    "\r\n"), traceFormatter.MaskContents(responseInfo.Body, headersToMask));

            builder.AppendFormat("-----------------END API CALL-----------------------\r\n");
            return builder.ToString();
        }

        /// <summary>
        /// Gets the formatted logs for HTTP request or response headers.
        /// </summary>
        /// <param name="headers">The HTTP headers.</param>
        /// <param name="seps">The separator string.</param>
        /// <returns>The log output.</returns>
        private string GetFormattedHttpHeaderLogs(Dictionary<string, string> headers, string seps)
        {
            StringBuilder headerBuilder = new StringBuilder();
            foreach (string key in headers.Keys)
            {
                headerBuilder.AppendFormat("{0}: {1}{2}", key, headers[key], seps);
            }

            headerBuilder.AppendFormat("TimeStamp: {0}{1}", this.GetTimeStamp(), seps);
            return headerBuilder.ToString();
        }

        /// <summary>
        /// Masks the headers.
        /// </summary>
        /// <param name="headers">The list of all headers</param>
        /// <param name="headersToMask">The list of headers to mask.</param>
        /// <returns>The list of headers, after masking.</returns>
        private Dictionary<string, string> MaskHeaders(WebHeaderCollection headers,
            ISet<string> headersToMask)
        {
            Dictionary<string, string> maskedHeaders = new Dictionary<string, string>();
            foreach (string key in headers)
            {
                if (config.MaskCredentials && headersToMask.Contains(key))
                {
                    maskedHeaders[key] = TraceFormatter.MASK_PATTERN;
                }
                else
                {
                    maskedHeaders[key] = headers[key];
                }
            }

            return maskedHeaders;
        }

        /// <summary>
        /// Gets the trace formatter for formatting an HTTP request.
        /// </summary>
        /// <param name="shouldMask">True, if fields should be masked, false
        /// otherwise.</param>
        /// <returns>A trace formatter for formatting HTTP request.</returns>
        private TraceFormatter GetTraceFormatterForHTTPRequests(bool shouldMask)
        {
            TraceFormatter formatter = null;
            if (shouldMask)
            {
                formatter = new UrlEncodedBodyFormatter();
            }
            else
            {
                formatter = new DefaultBodyFormatter();
            }

            return formatter;
        }

        /// <summary>
        /// Gets the request summary for logging.
        /// </summary>
        /// <param name="headers">The HTTP request headers.</param>
        /// <param name="body">The HTTP request body.</param>
        /// <param name="headersToMask">The set of headers to mask</param>
        /// <param name="formatter">The formatter to use for masking</param>
        /// <returns>The request summary for logging.</returns>
        private string GetRequestSummary(WebHeaderCollection headers, string body,
            ISet<string> headersToMask, TraceFormatter formatter)
        {
            Dictionary<string, string> maskedHeader = MaskHeaders(headers, headersToMask);
            NameValueCollection collection = HttpUtility.ParseQueryString(body);
            string loggedHeaders = GetFormattedHttpHeaderLogs(maskedHeader, ", ");
            string details = formatter.MaskContents(body, headersToMask);

            return loggedHeaders + details;
        }

        /// <summary>
        /// Gets the current timestamp as a formatted string.
        /// </summary>
        /// <returns>The current timestamp.</returns>
        private string GetTimeStamp()
        {
            return dateTimeProvider.Now.ToString("R");
        }

        /// <summary>
        /// Truncates an error message for display, if necessary.
        /// </summary>
        /// <param name="errorMessage">The error message to be displayed.</param>
        /// <returns>An error message suitable for display, possibly truncated.</returns>
        private string TruncateErrorMessage(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                return null;
            }

            string truncatedMessage = NEWLINE_REGEX.Replace(errorMessage, " ");
            if (truncatedMessage.Length > MAX_SUMMARY_ERROR_LENGTH)
            {
                truncatedMessage = new StringBuilder(MAX_SUMMARY_ERROR_LENGTH)
                    .Append(truncatedMessage.Substring(0,
                        MAX_SUMMARY_ERROR_LENGTH - ELLIPSIS.Length)).Append(ELLIPSIS).ToString();
            }

            return truncatedMessage;
        }
    }
}
