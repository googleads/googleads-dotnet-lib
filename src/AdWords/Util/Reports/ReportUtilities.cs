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
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace Google.Api.Ads.AdWords.Util.Reports {
  /// <summary>
  /// Defines report utility functions for the client library.
  /// </summary>
  public class ReportUtilities : LegacyReportUtilities {
    /// <summary>
    /// Maximum length of report to read to check if it contains errors.
    /// </summary>
    private const int MAX_ERROR_LENGTH = 4096;

    /// <summary>
    /// The regex to check if a report download is valid or if it contains
    /// errors.
    /// </summary>
    private const string REPORT_ERROR_REGEX = "\\!\\!\\!([^\\|]*)\\|\\|\\|(.*)";

    /// <summary>
    /// The report download url format.
    /// </summary>
    private const string REPORT_URL_FORMAT = "{0}/api/adwords/reportdownload?__rd={1}";

    /// <summary>
    /// Downloads an MCC report.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <returns>An Mcc report object.</returns>
    private delegate MccReport GetMccReportFunction(long reportDefinitionId,
        bool returnMoneyInMicros);

    /// <summary>
    /// Downloads an MCC report and saves it to disk.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <returns>An Mcc report object.</returns>
    private delegate MccReport DownloadMccReportFunction(long reportDefinitionId,
        bool returnMoneyInMicros, string path);

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportUtilities"/> class.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    public ReportUtilities(AdWordsUser user) : base(user) {
    }

    /// <summary>
    /// Downloads a client report.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <returns>A client report object.</returns>
    public ClientReport GetClientReport(long reportDefinitionId) {
      return GetClientReport(reportDefinitionId, true);
    }

    /// <summary>
    /// Downloads a client report.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <returns>A client report object.</returns>
    public ClientReport GetClientReport(long reportDefinitionId, bool returnMoneyInMicros) {
      ClientReport retval = new ClientReport();
      AdWordsAppConfig config = (AdWordsAppConfig) User.Config;
      if (string.IsNullOrEmpty(config.AuthToken)) {
        config.AuthToken = new AuthToken(config, "adwords", config.Email, config.Password).
            GetToken();
      }
      string clientId = !string.IsNullOrEmpty(config.ClientEmail) ? config.ClientEmail :
          config.ClientCustomerId;

      string downloadUrl = string.Format(REPORT_URL_FORMAT, config.AdWordsApiServer,
          reportDefinitionId);

      MemoryStream memStream = new MemoryStream();
      byte[] preview = DownloadReportToStream(downloadUrl, config.Proxy, clientId, config.AuthToken,
          returnMoneyInMicros, memStream);
      if (!IsValidReport(preview)) {
        throw new ReportsException(AdWordsErrorMessages.ReportIsInvalid + " - " +
            ConvertPreviewBytesToString(preview), null, null);
      }
      retval.Contents = memStream.ToArray();
      return retval;
    }

    /// <summary>
    /// Downloads a client report and saves it to disk.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <param name="path">The path to which the report should be saved.</param>
    public void DownloadClientReport(long reportDefinitionId, string path) {
      DownloadClientReport(reportDefinitionId, true, path);
    }

    /// <summary>
    /// Downloads a client report and saves it to disk.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="path">The path to which the report should be saved.</param>
    public void DownloadClientReport(long reportDefinitionId, bool returnMoneyInMicros,
        string path) {
      AdWordsAppConfig config = (AdWordsAppConfig) User.Config;
      if (string.IsNullOrEmpty(config.AuthToken)) {
        config.AuthToken = new AuthToken(config, "adwords", config.Email, config.Password).
            GetToken();
      }
      string clientId = !string.IsNullOrEmpty(config.ClientEmail) ? config.ClientEmail :
          config.ClientCustomerId;

      string downloadUrl = string.Format(REPORT_URL_FORMAT, config.AdWordsApiServer,
          reportDefinitionId);

      byte[] preview = DownloadReportToDisk(downloadUrl, config.Proxy, clientId, config.AuthToken,
          returnMoneyInMicros, path);
      if (!IsValidReport(preview)) {
        throw new ReportsException(AdWordsErrorMessages.ReportIsInvalid + " - " +
            ConvertPreviewBytesToString(preview), null, null);
      }
    }

    /// <summary>
    /// Downloads an MCC report.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <returns>An Mcc report object.</returns>
    public MccReport GetMccReport(long reportDefinitionId) {
      return GetMccReport(reportDefinitionId, true);
    }

    /// <summary>
    /// Downloads an MCC report.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <returns>An Mcc report object.</returns>
    public MccReport GetMccReport(long reportDefinitionId, bool returnMoneyInMicros) {
      return DownloadMccReport(reportDefinitionId, returnMoneyInMicros,
          null);
    }

    /// <summary>
    /// Downloads an MCC report and saves it to disk.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <param name="path">The path to which the report should be saved.</param>
    /// <returns>An Mcc report object.</returns>
    public MccReport DownloadMccReport(long reportDefinitionId, string path) {
      return DownloadMccReport(reportDefinitionId, true, path);
    }

    /// <summary>
    /// Downloads an MCC report and saves it to disk.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="path">The path to which the report should be saved.</param>
    /// <returns>An Mcc report object.</returns>
    public MccReport DownloadMccReport(long reportDefinitionId, bool returnMoneyInMicros,
        string path) {
      throw new NotSupportedException("This functionality is currently not supported by the " +
          "client library, since cross-client reports were unlaunched. See " +
          "http://adwordsapi.blogspot.com/2011/03/update-to-reporting-service-in-adwords.html" +
          " for details.");
      AdWordsAppConfig config = (AdWordsAppConfig) User.Config;
      MccReport retval = new MccReport();

      if (string.IsNullOrEmpty(config.AuthToken)) {
        config.AuthToken = new AuthToken(config, "adwords", config.Email, config.Password).
            GetToken();
      }
      string clientId = !string.IsNullOrEmpty(config.ClientEmail) ? config.ClientEmail :
          config.ClientCustomerId;

      string queryToken = "new";

      int pollingCount = 0;

      while (pollingCount < MaxPollingAttempts) {
        string downloadUrl = string.Format(REPORT_URL_FORMAT + "&qt={2}", config.AdWordsApiServer,
            reportDefinitionId, queryToken);
        MccReportResponse reportResponse = GetMccReportResponse(downloadUrl, clientId,
            config.AuthToken, returnMoneyInMicros, config.Proxy);

        switch (reportResponse.StatusCode) {
          case HttpStatusCode.InternalServerError:
            throw new ReportsException(AdWordsErrorMessages.ReportIsInvalid, null,
                reportResponse.ReportStatus);

          case HttpStatusCode.OK:
            queryToken = reportResponse.QueryToken;
            Thread.Sleep(WAIT_TIME);
            pollingCount++;
            break;

          case HttpStatusCode.Moved:
            byte[] preview = null;
            if (string.IsNullOrEmpty(path)) {
              MemoryStream memStream = new MemoryStream();
              DownloadReportToStream(reportResponse.FollowupUrl, config.Proxy, clientId,
                  config.AuthToken, returnMoneyInMicros, memStream);
              retval.Contents = memStream.ToArray();
            } else {
              DownloadReportToDisk(reportResponse.FollowupUrl, config.Proxy, clientId,
                  config.AuthToken, returnMoneyInMicros, path);
              retval.Path = path;
            }

            if (!IsValidReport(preview)) {
              throw new ReportsException(AdWordsErrorMessages.ReportIsInvalid + " - " +
                  ConvertPreviewBytesToString(preview), null, reportResponse.ReportStatus);
            }

            retval.ReportStatus = reportResponse.ReportStatus;
            return retval;

          default:
            // There are situations under which the server can return other
            // codes. 503 RATE_EXCEEDED is a good example.
            throw new ReportsException(AdWordsErrorMessages.ReportIsInvalid);
        }
      }
      throw new ReportsException(AdWordsErrorMessages.ReportNumPollsExceeded);
    }

    /// <summary>
    /// Begins downloading an MCC report asynchronously.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="callback">The callback to be called once the report
    /// is saved.</param>
    /// <returns>The IAsyncResult object associated with this asynchronous
    /// call.</returns>
    public IAsyncResult BeginGetMccReport(long reportDefinitionId, bool returnMoneyInMicros,
        AsyncCallback callback) {
      GetMccReportFunction getMccReport = GetMccReport;
      return getMccReport.BeginInvoke(reportDefinitionId, returnMoneyInMicros, callback,
          getMccReport);
    }

    /// <summary>
    /// Ends downloading an MCC report asynchronously.
    /// </summary>
    /// <returns>The IAsyncResult object returned from
    /// <see cref="BeginGetMccReport"/></returns>
    /// <returns>An Mcc report object.</returns>
    public MccReport EndGetMccReport(IAsyncResult result) {
      return (result.AsyncState as GetMccReportFunction).EndInvoke(result);
    }

    /// <summary>
    /// Begins downloading an MCC report to disk asynchronously.
    /// </summary>
    /// <param name="reportDefinitionId">The report definition id.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="path">The path to which the report should be saved.</param>
    /// <param name="callback">The callback to be called once the report
    /// is saved.</param>
    /// <returns>The IAsyncResult object associated with this asynchronous
    /// call.</returns>
    public IAsyncResult BeginDownloadMccReport(long reportDefinitionId, bool returnMoneyInMicros,
        string path, AsyncCallback callback) {
      DownloadMccReportFunction downloadMccReport = DownloadMccReport;
      return downloadMccReport.BeginInvoke(reportDefinitionId, returnMoneyInMicros, path,
          callback, downloadMccReport);
    }

    /// <summary>
    /// Ends downloading an MCC report to disk asynchronously.
    /// </summary>
    /// <returns>The IAsyncResult object returned from
    /// <see cref="BeginGetMccReport"/></returns>
    /// <returns>An Mcc report object.</returns>
    public MccReport EndDownloadMccReport(IAsyncResult result) {
      return (result.AsyncState as DownloadMccReportFunction).EndInvoke(result);
    }

    /// <summary>
    /// Gets the MCC report response.
    /// </summary>
    /// <param name="downloadUrl">The download URL.</param>
    /// <param name="clientId">The client id.</param>
    /// <param name="authToken">The auth token.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="proxy">The proxy server to be used for connecting to the
    /// server.</param>
    /// <returns>The response from the server.</returns>
    private MccReportResponse GetMccReportResponse(string downloadUrl, string clientId,
        string authToken, bool returnMoneyInMicros, WebProxy proxy) {
      MccReportResponse retval = new MccReportResponse();

      WebRequest request = HttpWebRequest.Create(downloadUrl);
      request.Proxy = proxy;
      (request as HttpWebRequest).AllowAutoRedirect = false;
      request.Headers.Add("clientEmail: " + clientId);
      request.Headers.Add("Authorization: GoogleLogin auth=" + authToken);
      request.Headers.Add("returnMoneyInMicros: " + returnMoneyInMicros.ToString().ToLower());

      WebResponse response = null;
      try {
        response = request.GetResponse();
      } catch (WebException ex) {
        response = ex.Response;
      }

      retval.StatusCode = (response as HttpWebResponse).StatusCode;
      if (retval.StatusCode == HttpStatusCode.MovedPermanently) {
        retval.FollowupUrl = response.Headers["Location"];
      }
      MemoryStream memStream = new MemoryStream();
      MediaUtilities.CopyStream(response.GetResponseStream(), memStream);
      retval.ReportStatus = ParseReportResponse(Encoding.UTF8.GetString(memStream.ToArray()));
      return retval;
    }

    /// <summary>
    /// Parses the report response.
    /// </summary>
    /// <param name="contents">The report response xml.</param>
    /// <returns>The parsed report response.</returns>
    private MccReportStatus ParseReportResponse(string contents) {
      MccReportStatus reportResponse = new MccReportStatus();
      XmlDocument xDoc = SerializationUtilities.LoadXml(contents);

      XmlNode node = null;

      node = xDoc.SelectSingleNode("/reportResponse/queryToken/text()");
      if (node != null) {
        reportResponse.queryToken = node.Value;
      }

      node = xDoc.SelectSingleNode("/reportResponse/state/text()");
      if (node != null) {
        reportResponse.state = (MccReportStatus.State) Enum.Parse(typeof(MccReportStatus.State),
            node.Value);
      }

      node = xDoc.SelectSingleNode("/reportResponse/total/text()");
      if (node != null) {
        reportResponse.total = int.Parse(node.Value);
      }

      node = xDoc.SelectSingleNode("/reportResponse/success/text()");
      if (node != null) {
        reportResponse.success = int.Parse(node.Value);
      }

      node = xDoc.SelectSingleNode("/reportResponse/fail/text()");
      if (node != null) {
        reportResponse.fail = int.Parse(node.Value);
      }

      node = xDoc.SelectSingleNode("/reportResponse/failureReason/text()");
      if (node != null) {
        reportResponse.failureReason = node.Value;
      }

      XmlNodeList accountNodes = xDoc.SelectNodes("/reportResponse/failures/*");

      foreach (XmlNode accountNode in accountNodes) {
        MccReportStatus.Account account = new MccReportStatus.Account();
        XmlNode accountIdNode = accountNode.SelectSingleNode("id");
        if (accountNode != null) {
          account.id = accountIdNode.InnerText;
        }
        reportResponse.failures.Add(account);
      }

      return reportResponse;
    }

    /// <summary>
    /// Downloads the report to disk.
    /// </summary>
    /// <param name="downloadUrl">The report download URL.</param>
    /// <param name="proxy">The web proxy to be used for HTTP requests.</param>
    /// <param name="clientId">The client id who owns this report.</param>
    /// <param name="authToken">The auth token for authorizing report download.
    /// </param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="path">The path to which the report is downloaded.</param>
    /// <returns>A preview of <see cref="MAX_ERROR_LENGTH" bytes./></returns>
    private byte[] DownloadReportToDisk(string downloadUrl, WebProxy proxy, string clientId,
        string authToken, bool returnMoneyInMicros, string path) {
      using (FileStream fs = File.OpenWrite(path)) {
        return DownloadReportToStream(downloadUrl, proxy, clientId, authToken,
            returnMoneyInMicros, fs);
      }
    }

    /// <summary>
    /// Downloads the report to disk.
    /// </summary>
    /// <param name="downloadUrl">The report download URL.</param>
    /// <param name="proxy">The web proxy to be used for HTTP requests.</param>
    /// <param name="clientId">The client id who owns this report.</param>
    /// <param name="authToken">The auth token for authorizing report download.
    /// </param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="outputStream">The output stream to which the downloaded
    /// report should be saved.</param>
    /// <returns>A preview of <see cref="MAX_ERROR_LENGTH" bytes./></returns>
    private byte[] DownloadReportToStream(string downloadUrl, WebProxy proxy, string clientId,
        string authToken, bool returnMoneyInMicros, Stream outputStream) {
      WebRequest request = HttpWebRequest.Create(downloadUrl);
      request.Proxy = proxy;

      if (!clientId.Contains("@")) {
        request.Headers.Add("clientCustomerId: " + clientId);
      } else {
        request.Headers.Add("clientEmail: " + clientId);
      }

      if (!string.IsNullOrEmpty(authToken)) {
        request.Headers.Add("Authorization: GoogleLogin auth=" + authToken);
      }

      request.Headers.Add("returnMoneyInMicros: " + returnMoneyInMicros.ToString().ToLower());

      WebResponse response = request.GetResponse();
      return MediaUtilities.CopyStreamWithPreview(response.GetResponseStream(),
          outputStream, MAX_ERROR_LENGTH);
    }

    /// <summary>
    /// Converts the preview bytes to string.
    /// </summary>
    /// <param name="previewBytes">The preview bytes.</param>
    /// <returns>The preview bytes as a text.</returns>
    private string ConvertPreviewBytesToString(byte[] previewBytes) {
      if (previewBytes == null) {
        return "";
      }

      // It is possible that our byte array doesn't end at a valid utf-8 string
      // boundary, so we use a progressive decoder to decode bytes as far as
      // possible.
      Decoder decoder = Encoding.UTF8.GetDecoder();
      char[] charArray = new char[previewBytes.Length];
      int bytesUsed;
      int charsUsed;
      bool completed;

      decoder.Convert(previewBytes, 0, previewBytes.Length, charArray, 0, charArray.Length, true,
          out bytesUsed, out charsUsed, out completed);
      return new string(charArray, 0, charsUsed);
    }

    /// <summary>
    /// Determines whether the report is valid or not.
    /// </summary>
    /// <param name="previewBytes">First n bytes of a report to be inspected
    /// for report validity.
    /// </param>
    /// <returns>True if the report is valid, false otherwise.</returns>
    private bool IsValidReport(byte[] previewBytes) {
      string previewString = ConvertPreviewBytesToString(previewBytes);
      if (!string.IsNullOrEmpty(previewString)) {
        if (Regex.IsMatch(previewString, REPORT_ERROR_REGEX)) {
          return false;
        }
      }
      return true;
    }
  }
}
