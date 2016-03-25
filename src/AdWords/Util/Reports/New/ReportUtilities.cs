// Copyright 2011, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Common.Util.Reports;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;

namespace Google.Api.Ads.AdWords.Util.Reports {

  /// <summary>
  /// Defines report utility functions for the client library.
  /// </summary>
  public class ReportUtilities : AdsReportUtilities {

    /// <summary>
    /// The feature ID for this class.
    /// </summary>
    private const AdsFeatureUsageRegistry.Features FEATURE_ID =
        AdsFeatureUsageRegistry.Features.ReportDownloader;

    /// <summary>
    /// The registry for saving feature usage information..
    /// </summary>
    private readonly AdsFeatureUsageRegistry featureUsageRegistry =
        AdsFeatureUsageRegistry.Instance;

    /// <summary>
    /// Default report version.
    /// </summary>
    internal const string DEFAULT_REPORT_VERSION = "v201603";

    /// <summary>
    /// Sets the reporting API version to use.
    /// </summary>
    private string reportVersion = DEFAULT_REPORT_VERSION;

    /// <summary>
    /// The report download url format for ad-hoc reports.
    /// </summary>
    private const string QUERY_REPORT_URL_FORMAT = "{0}/api/adwords/reportdownload/{1}?" +
        "__fmt={2}";

    /// <summary>
    /// The report download url format for ad-hoc reports.
    /// </summary>
    private const string ADHOC_REPORT_URL_FORMAT = "{0}/api/adwords/reportdownload/{1}";

    /// <summary>
    /// The list of headers to mask in the logs.
    /// </summary>
    private readonly HashSet<string> HEADERS_TO_MASK = new HashSet<string> {
        "developerToken", "Authorization"
    };

    /// <summary>
    /// The AWQL query for downloading this report.
    /// </summary>
    private string query;

    /// <summary>
    /// The format in which the report is downloaded.
    /// </summary>
    private string format;

    /// <summary>
    /// The report definition for downloading this report.
    /// </summary>
    private IReportDefinition reportDefinition;

    /// <summary>
    /// True, if the user wants to use AWQL for downloading reports, false if
    /// the user wants to use reportDefinition instead.
    /// </summary>
    private bool usesQuery = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportUtilities" /> class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    /// <param name="query">The AWQL for downloading reports.</param>
    /// <param name="format">The report download format.</param>
    public ReportUtilities(AdWordsUser user, string query, string format)
      : this(user, DEFAULT_REPORT_VERSION, query, format) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportUtilities"/> class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    /// <param name="reportDefinition">The report definition.</param>
    public ReportUtilities(AdWordsUser user, IReportDefinition reportDefinition)
      : this(user, DEFAULT_REPORT_VERSION, reportDefinition) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportUtilities" /> class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    /// <param name="query">The AWQL for downloading reports.</param>
    /// <param name="format">The report download format.</param>
    /// <param name="reportVersion">The report version.</param>
    public ReportUtilities(AdWordsUser user, string reportVersion, string query, string format)
      : base(user) {
      this.reportVersion = reportVersion;
      this.query = query;
      this.format = format;
      this.usesQuery = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportUtilities"/> class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    /// <param name="reportDefinition">The report definition.</param>
    /// <param name="reportVersion">The report version.</param>
    public ReportUtilities(AdWordsUser user, string reportVersion,
        IReportDefinition reportDefinition)
      : base(user) {
      this.reportVersion = reportVersion;
      this.reportDefinition = reportDefinition;
      this.usesQuery = false;
    }

    /// <summary>
    /// Gets the report response.
    /// </summary>
    /// <returns>
    /// The report response.
    /// </returns>
    protected override ReportResponse GetReport() {
      AdWordsAppConfig config = (AdWordsAppConfig) User.Config;
      string postBody;
      string downloadUrl;

      if (usesQuery) {
        downloadUrl = string.Format(QUERY_REPORT_URL_FORMAT, config.AdWordsApiServer,
              reportVersion, format);
        postBody = string.Format("__rdquery={0}", HttpUtility.UrlEncode(query));
      } else {
        downloadUrl = string.Format(ADHOC_REPORT_URL_FORMAT, config.AdWordsApiServer,
              reportVersion);
        postBody = "__rdxml=" + HttpUtility.UrlEncode(ConvertDefinitionToXml(
          reportDefinition));
      }
      return DownloadReport(downloadUrl, postBody);
    }

    /// <summary>
    /// Downloads a report to stream.
    /// </summary>
    /// <param name="downloadUrl">The download url.</param>
    /// <param name="postBody">The POST body.</param>
    private ReportResponse DownloadReport(string downloadUrl, string postBody) {
      AdWordsErrorHandler errorHandler = new AdWordsErrorHandler(this.User as AdWordsUser);
      while (true) {
        WebResponse response = null;
        HttpWebRequest request = null;
        LogEntry logEntry = new LogEntry(User.Config, new DefaultDateTimeProvider());

        try {
          request = BuildRequest(downloadUrl, postBody, logEntry);
          response = request.GetResponse();

          logEntry.LogResponse(response, false, "Response truncated.");
          logEntry.Flush();
          return new ReportResponse(response);
        } catch (WebException e) {
          Exception reportsException = null;

          string contents = HttpUtilities.GetErrorResponseBody(e);
          logEntry.LogResponse(e.Response, true, contents);
          logEntry.Flush();

          reportsException = ParseException(e, contents);

          if (AdWordsErrorHandler.IsOAuthTokenExpiredError(reportsException) && request != null) {
            reportsException = new AdWordsCredentialsExpiredException(
                request.Headers["Authorization"]);
          }
          if (errorHandler.ShouldRetry(reportsException)) {
            errorHandler.PrepareForRetry(reportsException);
          } else {
            throw reportsException;
          }
        } finally {
          featureUsageRegistry.Clear();
        }
      }
    }

    /// <summary>
    /// Builds an HTTP request for downloading reports.
    /// </summary>
    /// <param name="downloadUrl">The download url.</param>
    /// <param name="postBody">The POST body.</param>
    /// <param name="logEntry">The logEntry to write the HTTP logs.</param>
    /// <returns>A webrequest to download reports.</returns>
    private HttpWebRequest BuildRequest(string downloadUrl, string postBody, LogEntry logEntry) {
      AdWordsAppConfig config = this.User.Config as AdWordsAppConfig;

      HttpWebRequest request = (HttpWebRequest) HttpUtilities.BuildRequest(downloadUrl, "POST",
          config);
      request.Headers.Add("clientCustomerId: " + config.ClientCustomerId);
      request.ContentType = "application/x-www-form-urlencoded";

      if (this.User.OAuthProvider != null) {
        request.Headers["Authorization"] = this.User.OAuthProvider.GetAuthHeader();
      } else {
        throw new AdWordsApiException(null, AdWordsErrorMessages.OAuthProviderCannotBeNull);
      }

      request.Headers.Add("developerToken: " + config.DeveloperToken);
      // The client library will use only apiMode = true.
      request.Headers.Add("apiMode", "true");

      request.Headers.Add("skipReportHeader", config.SkipReportHeader.ToString().ToLower());
      request.Headers.Add("skipReportSummary", config.SkipReportSummary.ToString().ToLower());
      request.Headers.Add("skipColumnHeader", config.SkipColumnHeader.ToString().ToLower());

      // Send the includeZeroImpressions header only if the user explicitly
      // requested it through the config object.
      if (config.IncludeZeroImpressions.HasValue) {
        request.Headers.Add("includeZeroImpressions", config.IncludeZeroImpressions.ToString().
            ToLower());
      }

      HttpUtilities.WritePostBodyAndLog(request, postBody, logEntry, HEADERS_TO_MASK);
      return request;
    }

    /// <summary>
    /// Parses the error response into an exception.
    /// </summary>
    /// <param name="exception">The original exception.</param>
    /// <param name="contents">The errors XML.</param>
    /// <returns>An AdWords Reports exception that represents the error.</returns>
    private AdWordsReportsException ParseException(Exception exception, string contents) {
      List<ReportDownloadError> errorList = new List<ReportDownloadError>();
      try {
        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(contents);
        XmlNodeList errorNodes = xDoc.DocumentElement.SelectNodes("ApiError");
        foreach (XmlElement errorNode in errorNodes) {
          ReportDownloadError downloadError = new ReportDownloadError();
          downloadError.ErrorType = errorNode.SelectSingleNode("type").InnerText;
          downloadError.FieldPath = errorNode.SelectSingleNode("fieldPath").InnerText;
          downloadError.Trigger = errorNode.SelectSingleNode("trigger").InnerText;
          errorList.Add(downloadError);
        }
      } catch {
      }
      AdWordsReportsException retval = new AdWordsReportsException(this.reportVersion,
          AdWordsErrorMessages.ReportingExceptionOccurred, exception);
      retval.Errors = errorList.ToArray();
      return retval;
    }

    /// <summary>
    /// Converts the report definition to XML format.
    /// </summary>
    /// <param name="definition">The report definition.</param>
    /// <returns>The report definition serialized as an XML string.</returns>
    private string ConvertDefinitionToXml(IReportDefinition definition) {
      string xml = SerializationUtilities.SerializeAsXmlText(definition).Replace(
          "ReportDefinition", "reportDefinition");
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(xml);
      XmlNodeList xmlNodes = doc.SelectNodes("descendant::*");
      foreach (XmlElement node in xmlNodes) {
        node.RemoveAllAttributes();
      }
      return doc.OuterXml;
    }

    /// <summary>
    /// Gets the report download response.
    /// </summary>
    /// <returns>The report response.</returns>
    public override ReportResponse GetResponse() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);;

      return base.GetResponse();
    }

    /// <summary>
    /// Gets the report download response asynchronously.
    /// </summary>
    public override void GetResponseAsync() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);;

      base.GetResponseAsync();
    }
  }
}
