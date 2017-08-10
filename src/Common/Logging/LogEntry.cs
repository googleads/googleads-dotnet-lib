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
using System.Web;

namespace Google.Api.Ads.Common.Logging {

  /// <summary>
  /// Helper class for logging.
  /// </summary>
  public class LogEntry {

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
    /// Gets or sets the summary request log.
    /// </summary>
    public string SummaryRequestLog {
      get;
      private set;
    }

    /// <summary>
    /// Gets or sets the summary response log.
    /// </summary>
    public string SummaryResponseLog {
      get;
      private set;
    }

    /// <summary>
    /// Gets or sets the detailed request log.
    /// </summary>
    public string DetailedRequestLog {
      get;
      private set;
    }

    /// <summary>
    /// Gets or sets the detailed response log.
    /// </summary>
    public string DetailedResponseLog {
      get;
      private set;
    }

    /// <summary>
    /// Gets the detailed log.
    /// </summary>
    public string DetailedLog {
      get {
        return this.DetailedRequestLog + this.DetailedResponseLog;
      }
    }

    /// <summary>
    /// Gets the summary log.
    /// </summary>
    public string SummaryLog {
      get {
        return this.SummaryRequestLog + "," + this.SummaryResponseLog;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogEntry"/> class.
    /// </summary>
    /// <param name="config">The application configuration.</param>
    /// <param name="dateTimeProvider">The date and time provider.</param>
    public LogEntry(AppConfig config, DateTimeProvider dateTimeProvider) :
      this(config, dateTimeProvider, new DefaultTraceWriter()) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogEntry"/> class.
    /// </summary>
    /// <param name="config">The application configuration.</param>
    /// <param name="dateTimeProvider">The date and time provider.</param>
    /// <param name="traceWriter">The trace writer to write with.</param>
    internal LogEntry(AppConfig config, DateTimeProvider dateTimeProvider,
        ITraceWriter traceWriter) {
      this.config = config;
      this.dateTimeProvider = dateTimeProvider;
      TraceWriter = traceWriter;
    }

    /// <summary>
    /// Logs an HTTP request.
    /// </summary>
    /// <param name="requestInfo">The request information.</param>
    /// <param name="headersToMask">The headers to mask.</param>
    public void LogRequest(RequestInfo requestInfo, ISet<string> headersToMask) {
      LogRequest(requestInfo, headersToMask,
          GetTraceFormatterForHTTPRequests(config.MaskCredentials));
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
        TraceFormatter formatter) {
      LogRequestSummary(requestInfo, headersToMask, formatter);
      LogRequestDetails(requestInfo, headersToMask, formatter);
    }

    /// <summary>
    /// Logs the details of an HTTP request.
    /// </summary>
    /// <param name="requestInfo">The request information.</param>
    /// <param name="headersToMask">The headers to mask.</param>
    /// <param name="formatter">The <see cref="TraceFormatter"/> to use when
    /// formatting the message.</param>
    public void LogRequestDetails(RequestInfo requestInfo, ISet<string> headersToMask,
        TraceFormatter formatter) {
      this.DetailedRequestLog = GetFormattedRequestLog(requestInfo,
          headersToMask, formatter);
    }

    /// <summary>
    /// Logs the summary of an HTTP request.
    /// </summary>
    /// <param name="requestInfo">The request information.</param>
    /// <param name="requestSummary">The formatted request summary.</param>
    public void LogRequestSummary(RequestInfo requestInfo, string requestSummary) {
      this.SummaryRequestLog = string.Format(CultureInfo.InvariantCulture,
          "host={0},url={1},{2}", requestInfo.Uri.Host, requestInfo.Uri.AbsolutePath,
          requestSummary);
    }

    /// <summary>
    /// Logs the summary of an HTTP request.
    /// </summary>
    /// <param name="requestInfo">The request information.</param>
    /// <param name="headersToMask">The headers to mask.</param>
    /// <param name="formatter">The <see cref="TraceFormatter"/> to use when
    /// formatting the message.</param>
    public void LogRequestSummary(RequestInfo requestInfo, ISet<string> headersToMask,
        TraceFormatter formatter) {
      LogRequestSummary(requestInfo, 
          GetRequestSummary(requestInfo.Headers, requestInfo.Body, headersToMask, formatter));
    }

    /// <summary>
    /// Logs an HTTP response.
    /// </summary>
    /// <param name="responseInfo">The response information.</param>
    /// <param name="isFailure">True, if this is a failed response, false
    /// otherwise.</param>
    public void LogResponse(ResponseInfo responseInfo, bool isFailure) {
      LogResponse(responseInfo, isFailure, new HashSet<string>(),
          new DefaultBodyFormatter());
    }

    /// <summary>
    /// Logs an HTTP response.
    /// </summary>
    /// <param name="responseInfo">The response information.</param>
    /// <param name="isFailure">True, if this is a failed response, false
    /// otherwise.</param>
    /// <param name="fieldsToMask">The list of fields to mask.</param>
    /// <param name="formatter">The formatter to be used for formatting the
    /// response logs.</param>
    public void LogResponse(ResponseInfo responseInfo, bool isFailure,
        ISet<string> fieldsToMask, TraceFormatter formatter) {
      LogResponseSummary(isFailure, "");
      LogResponseDetails(responseInfo, fieldsToMask, formatter);
    }

    /// <summary>
    /// Logs the summary of an HTTP response..
    /// </summary>
    /// <param name="isFailure">True, if this is a failed response, false
    /// otherwise.</param>
    /// <param name="formattedMessage">Any additional details to be appended to the
    /// response logs.</param>
    public void LogResponseSummary(bool isFailure, string formattedMessage) {
      this.isFailure = isFailure;
      this.SummaryResponseLog = string.Format("Result={0},{1}", isFailure ? "Failure" : "Success",
          formattedMessage).TrimEnd(',');
    }

    /// <summary>
    /// Logs the details of an HTTP response.
    /// </summary>
    /// <param name="responseInfo">The response information.</param>
    /// <param name="fieldsToMask">The list of fields to mask.</param>
    /// <param name="formatter">The formatter to be used for formatting the
    /// response logs.</param>
    public void LogResponseDetails(ResponseInfo responseInfo, ISet<string> fieldsToMask,
        TraceFormatter formatter) {
      if (config.MaskCredentials) {
        this.DetailedResponseLog = GetFormattedResponseLog(responseInfo, fieldsToMask, formatter);
      } else {
        this.DetailedResponseLog = GetFormattedResponseLog(responseInfo);
      }
    }

    /// <summary>
    /// Writes the HTTP logs.
    /// </summary>
    public void Flush() {
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
        TraceFormatter traceFormatter) {
      StringBuilder builder = new StringBuilder();
      builder.AppendFormat("\r\n-----------------BEGIN API CALL---------------------\r\n");
      builder.AppendFormat("\r\nRequest\r\n");
      builder.AppendFormat("-------\r\n\r\n");

      StringBuilder headerBuilder = new StringBuilder();

      headerBuilder.AppendFormat("{0} {1}\r\n", requestInfo.Method, requestInfo.Uri.AbsolutePath);
      headerBuilder.AppendFormat("{0}", GetFormattedHttpHeaderLogs(MaskHeaders(
          requestInfo.Headers, headersToMask), "\r\n"));
      builder.AppendFormat("{0}\r\n\r\n{1}\r\n", headerBuilder.ToString(),
          traceFormatter.MaskContents(requestInfo.Body, headersToMask));
      return builder.ToString();
    }

    /// <summary>
    /// Gets the formatted logs for an HTTP response.
    /// </summary>
    /// <param name="responseInfo">The response information.</param>
    /// <returns>The response log text.</returns>
    private string GetFormattedResponseLog(ResponseInfo responseInfo) {
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
    private string GetFormattedResponseLog(ResponseInfo responseInfo, ISet<string> headersToMask,
        TraceFormatter traceFormatter) {
      StringBuilder builder = new StringBuilder();
      builder.AppendFormat("\r\nResponse\r\n");
      builder.AppendFormat("--------\r\n");

      builder.AppendFormat("\r\n{0}\r\n\r\n{1}\r\n",
          GetFormattedHttpHeaderLogs(MaskHeaders(responseInfo.Headers, headersToMask), "\r\n"),
          traceFormatter.MaskContents(responseInfo.Body, headersToMask));

      builder.AppendFormat("-----------------END API CALL-----------------------\r\n");
      return builder.ToString();
    }

    /// <summary>
    /// Gets the formatted logs for HTTP request or response headers.
    /// </summary>
    /// <param name="headers">The HTTP headers.</param>
    /// <param name="seps">The separator string.</param>
    /// <returns>The log output.</returns>
    private string GetFormattedHttpHeaderLogs(Dictionary<string, string> headers, string seps) {
      StringBuilder headerBuilder = new StringBuilder();
      foreach (string key in headers.Keys) {
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
        ISet<string> headersToMask) {
      Dictionary<string, string> maskedHeaders = new Dictionary<string, string>();
      foreach (string key in headers) {
        if (config.MaskCredentials && headersToMask.Contains(key)) {
          maskedHeaders[key] = TraceFormatter.MASK_PATTERN;
        } else {
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
    private TraceFormatter GetTraceFormatterForHTTPRequests(bool shouldMask) {
      TraceFormatter formatter = null;
      if (shouldMask) {
        formatter = new UrlEncodedBodyFormatter();
      } else {
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
        ISet<string> headersToMask, TraceFormatter formatter) {
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
    private string GetTimeStamp() {
      return dateTimeProvider.Now.ToString("R");
    }
  }
}
