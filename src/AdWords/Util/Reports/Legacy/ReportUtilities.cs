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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Xml;

namespace Google.Api.Ads.AdWords.Util.Reports.Legacy {
  /// <summary>
  /// Defines report utility functions for the client library.
  /// </summary>
  [Obsolete("This class is deprecated, use Google.Api.Ads.AdWords.Util.Reports.ReportUtilities " +
      "instead. See http://goo.gl/LX016X for migration details.")]
  public class ReportUtilities {
    /// <summary>
    /// The user associated with this object.
    /// </summary>
    private AdWordsUser user;

    /// <summary>
    /// Default report version.
    /// </summary>
    private const string DEFAULT_REPORT_VERSION = "v201406";

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
    /// Gets or sets the reporting API version to use.
    /// </summary>
    public string ReportVersion {
      get {
        return reportVersion;
      }
      set {
        reportVersion = value;
      }
    }

    /// <summary>
    /// Returns the user associated with this object.
    /// </summary>
    public AdWordsUser User {
      get {
        return user;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportUtilities"/> class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    public ReportUtilities(AdWordsUser user) {
      this.user = user;
    }

    /// <summary>
    /// Downloads a report into memory.
    /// </summary>
    /// <param name="query">The AWQL query for report definition. See
    /// https://developers.google.com/adwords/api/docs/guides/awql for AWQL
    /// documentation.</param>
    /// <param name="format">The report format.</param>
    /// <returns>The client report.</returns>
    public ClientReport GetClientReport(string query, string format) {
      DeprecationUtilities.ShowDeprecationMessage(this.GetType());

      AdWordsAppConfig config = (AdWordsAppConfig) User.Config;
      string downloadUrl = string.Format(QUERY_REPORT_URL_FORMAT, config.AdWordsApiServer,
            reportVersion, format);
      string postData = string.Format("__rdquery={0}", HttpUtility.UrlEncode(query));
      return GetClientReportInternal(downloadUrl, postData);
    }

    /// <summary>
    ///  Downloads a report into memory.
    /// </summary>
    /// <param name="reportDefinition">The report definition.</param>
    /// <returns>The client report.</returns>
    public ClientReport GetClientReport<T>(T reportDefinition) {
      DeprecationUtilities.ShowDeprecationMessage(this.GetType());
      AdWordsAppConfig config = (AdWordsAppConfig) User.Config;

      string postBody = "__rdxml=" + HttpUtility.UrlEncode(ConvertDefinitionToXml(
          reportDefinition));
      string downloadUrl = string.Format(ADHOC_REPORT_URL_FORMAT, config.AdWordsApiServer,
            reportVersion);
      return GetClientReportInternal(downloadUrl, postBody);
    }

    /// <summary>
    /// Downloads a report to disk.
    /// </summary>
    /// <param name="query">The AWQL query for report definition.</param>
    /// <param name="format">The report format.</param>
    /// <param name="path">The path to which report should be downloaded.
    /// </param>
    /// <returns>The client report.</returns>
    public ClientReport DownloadClientReport(string query, string format, string path) {
      DeprecationUtilities.ShowDeprecationMessage(this.GetType());

      AdWordsAppConfig config = (AdWordsAppConfig) User.Config;
      string downloadUrl = string.Format(QUERY_REPORT_URL_FORMAT, config.AdWordsApiServer,
            reportVersion, format);
      string postData = string.Format("__rdquery={0}", HttpUtility.UrlEncode(query));
      return DownloadClientReportInternal(downloadUrl, postData, path);
    }

    /// <summary>
    /// Downloads a report to disk.
    /// </summary>
    /// <param name="reportDefinition">The report definition.</param>
    /// <param name="path">The path to which report should be downloaded.
    /// </param>
    /// <returns>The client report.</returns>
    public ClientReport DownloadClientReport<T>(T reportDefinition, string path) {
      DeprecationUtilities.ShowDeprecationMessage(this.GetType());
      AdWordsAppConfig config = (AdWordsAppConfig) User.Config;

      string postBody = "__rdxml=" + HttpUtility.UrlEncode(ConvertDefinitionToXml(
          reportDefinition));
      string downloadUrl = string.Format(ADHOC_REPORT_URL_FORMAT, config.AdWordsApiServer,
            reportVersion);
      return DownloadClientReportInternal(downloadUrl, postBody, path);
    }

    /// <summary>
    /// Downloads the client report.
    /// </summary>
    /// <param name="downloadUrl">The download URL.</param>
    /// <param name="postBody">The HTTP POST request body.</param>
    /// <param name="path">The path to which report should be downloaded.
    /// </param>
    /// <returns>The client report.</returns>
    private ClientReport GetClientReportInternal(string downloadUrl, string postBody) {
      MemoryStream memStream = new MemoryStream();
      DownloadReportToStream(downloadUrl, postBody, memStream);

      ClientReport retval = new ClientReport();
      retval.Contents = memStream.ToArray();
      return retval;
    }

    /// <summary>
    /// Downloads the client report.
    /// </summary>
    /// <param name="downloadUrl">The download URL.</param>
    /// <param name="postBody">The HTTP POST request body.</param>
    /// <param name="path">The path to which report should be downloaded.
    /// </param>
    /// <returns>The client report.</returns>
    private ClientReport DownloadClientReportInternal(string downloadUrl, string postBody,
        string path) {
      ClientReport retval = new ClientReport();
      using (FileStream fileStream = File.OpenWrite(path)) {
        fileStream.SetLength(0);
        DownloadReportToStream(downloadUrl, postBody, fileStream);
        retval.Path = path;
        return retval;
      }
    }

    /// <summary>
    /// Downloads a report to stream.
    /// </summary>
    /// <param name="downloadUrl">The download url.</param>
    /// <param name="postBody">The POST body.</param>
    /// <param name="outputStream">The stream to which report is downloaded.
    /// </param>
    private void DownloadReportToStream(string downloadUrl, string postBody, Stream outputStream) {
      AdWordsErrorHandler errorHandler = new AdWordsErrorHandler(user);
      while (true) {
        WebResponse response = null;
        HttpWebRequest request = BuildRequest(downloadUrl, postBody);
        try {
          response = request.GetResponse();
          MediaUtilities.CopyStream(response.GetResponseStream(), outputStream);
          return;
        } catch (WebException ex) {
          Exception reportsException = null;

          try {
            response = ex.Response;

            if (response != null) {
              MemoryStream memStream = new MemoryStream();
              MediaUtilities.CopyStream(response.GetResponseStream(), memStream);
              String exceptionBody = Encoding.UTF8.GetString(memStream.ToArray());
              reportsException = ParseException(exceptionBody);
            }

            if (AdWordsErrorHandler.IsOAuthTokenExpiredError(reportsException)) {
              reportsException = new AdWordsCredentialsExpiredException(
                  request.Headers["Authorization"]);
            }
          } catch (Exception) {
            reportsException = ex;
          }
          if (errorHandler.ShouldRetry(reportsException)) {
            errorHandler.PrepareForRetry(reportsException);
          } else {
            throw reportsException;
          }
        } finally {
          if (response != null) {
            response.Close();
          }
        }
      }
    }

    /// <summary>
    /// Builds an HTTP request for downloading reports.
    /// </summary>
    /// <param name="downloadUrl">The download url.</param>
    /// <param name="postBody">The POST body.</param>
    /// <returns>A webrequest to download reports.</returns>
    private HttpWebRequest BuildRequest(string downloadUrl, string postBody) {
      AdWordsAppConfig config = user.Config as AdWordsAppConfig;

      HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(downloadUrl);
      request.Method = "POST";
      request.Proxy = config.Proxy;
      request.Timeout = config.Timeout;
      request.UserAgent = config.GetUserAgent();

      request.Headers.Add("clientCustomerId: " + config.ClientCustomerId);
      request.ContentType = "application/x-www-form-urlencoded";
      if (config.EnableGzipCompression) {
        (request as HttpWebRequest).AutomaticDecompression = DecompressionMethods.GZip
            | DecompressionMethods.Deflate;
      } else {
        (request as HttpWebRequest).AutomaticDecompression = DecompressionMethods.None;
      }
      if (this.User.OAuthProvider != null) {
        request.Headers["Authorization"] = this.User.OAuthProvider.GetAuthHeader();
      } else {
        throw new AdWordsApiException(null, AdWordsErrorMessages.OAuthProviderCannotBeNull);
      }

      request.Headers.Add("developerToken: " + config.DeveloperToken);
      // The client library will use only apiMode = true.
      request.Headers.Add("apiMode", "true");

      using (StreamWriter writer = new StreamWriter(request.GetRequestStream())) {
        writer.Write(postBody);
      }
      return request;
    }

    /// <summary>
    /// Parses the error response into an exception.
    /// </summary>
    /// <param name="errors">The error response from the server.</param>
    /// <returns></returns>
    private ReportsException ParseException(string errorsXml) {
      XmlDocument xDoc = new XmlDocument();
      xDoc.LoadXml(errorsXml);
      XmlNodeList errorNodes = xDoc.DocumentElement.SelectNodes("ApiError");
      List<ReportDownloadError> errorList = new List<ReportDownloadError>();
      foreach (XmlElement errorNode in errorNodes) {
        ReportDownloadError downloadError = new ReportDownloadError();
        downloadError.ErrorType = errorNode.SelectSingleNode("type").InnerText;
        downloadError.FieldPath = errorNode.SelectSingleNode("fieldPath").InnerText;
        downloadError.Trigger = errorNode.SelectSingleNode("trigger").InnerText;

        errorList.Add(downloadError);
      }
      ReportsException ex = new ReportsException("Report download errors occurred, see errors " +
          "field for more details.");
      ex.Errors = errorList.ToArray();
      return ex;
    }

    /// <summary>
    /// Converts the report definition to XML format.
    /// </summary>
    /// <typeparam name="T">The type of ReportDefinition.</typeparam>
    /// <param name="definition">The report definition.</param>
    /// <returns>The report definition serialized as an xml.</returns>
    private string ConvertDefinitionToXml<T>(T definition) {
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
  }
}
