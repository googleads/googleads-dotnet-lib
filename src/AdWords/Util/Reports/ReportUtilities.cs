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
using System.Web;
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
    /// The report download url format for ad-hoc reports.
    /// </summary>
    private const string ADHOC_REPORT_URL_FORMAT = "{0}/api/adwords/reportdownload/v201109";
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
    /// <param name="reportDefinitionOrId">The report definition or id.</param>
    /// <typeparam name="T">The type of ReportDefinition object.</typeparam>
    /// <returns>A client report object.</returns>
    public ClientReport GetClientReport<T>(T reportDefinitionOrId) {
      return GetClientReport(reportDefinitionOrId, true);
    }

    /// <summary>
    /// Downloads a client report.
    /// </summary>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="reportDefinitionOrId">The report definition or id.</param>
    /// <typeparam name="T">The type of ReportDefinition object.</typeparam>
    /// <returns>A client report object.</returns>
    public ClientReport GetClientReport<T>(T reportDefinitionOrId, bool returnMoneyInMicros) {
      ClientReport retval = new ClientReport();
      AdWordsAppConfig config = (AdWordsAppConfig) User.Config;

      string postBody = null;
      string downloadUrl;
      if (typeof(T) == typeof(long)) {
        downloadUrl = string.Format(REPORT_URL_FORMAT, config.AdWordsApiServer,
            reportDefinitionOrId);
      } else {
        downloadUrl = string.Format(ADHOC_REPORT_URL_FORMAT, config.AdWordsApiServer);
        postBody = "__rdxml=" + HttpUtility.UrlEncode(ConvertDefinitionToXml(reportDefinitionOrId));
      }

      MemoryStream memStream = new MemoryStream();
      byte[] preview = DownloadReportToStream(downloadUrl, config, returnMoneyInMicros, memStream,
          postBody);
      if (!IsValidReport(preview)) {
        throw new ReportsException(AdWordsErrorMessages.ReportIsInvalid + " - " +
            ConvertPreviewBytesToString(preview), null);
      }
      retval.Contents = memStream.ToArray();
      return retval;
    }

    /// <summary>
    /// Downloads a client report and saves it to disk.
    /// </summary>
    /// <param name="reportDefinitionOrId">The report definition or id.</param>
    /// <typeparam name="T">The type of ReportDefinition object.</typeparam>
    /// <param name="path">The path to which the report should be saved.</param>
    public void DownloadClientReport<T>(T reportDefinitionOrId, string path) {
      DownloadClientReport(reportDefinitionOrId, true, path);
    }

    /// <summary>
    /// Downloads a client report and saves it to disk.
    /// </summary>
    /// <param name="reportDefinitionOrId">The report definition or id.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="path">The path to which the report should be saved.</param>
    /// <typeparam name="T">The type of ReportDefinition object.</typeparam>
    public void DownloadClientReport<T>(T reportDefinitionOrId, bool returnMoneyInMicros,
        string path) {
      AdWordsAppConfig config = (AdWordsAppConfig) User.Config;

      string postBody = null;
      string downloadUrl;
      if (typeof(T) == typeof(long)) {
        downloadUrl = string.Format(REPORT_URL_FORMAT, config.AdWordsApiServer,
            reportDefinitionOrId);
      } else {
        downloadUrl = string.Format(ADHOC_REPORT_URL_FORMAT, config.AdWordsApiServer);
        postBody = "__rdxml=" + HttpUtility.UrlEncode(ConvertDefinitionToXml(reportDefinitionOrId));
      }

      byte[] preview = DownloadReportToDisk(downloadUrl, config, returnMoneyInMicros, path,
          postBody);
      if (!IsValidReport(preview)) {
        throw new ReportsException(AdWordsErrorMessages.ReportIsInvalid + " - " +
            ConvertPreviewBytesToString(preview), null);
      }
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

    /// <summary>
    /// Downloads the report to disk.
    /// </summary>
    /// <param name="downloadUrl">The report download URL.</param>
    /// <param name="config">The configuration settings to be used when
    /// downloading the report.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="path">The path to which the report is downloaded.</param>
    /// <param name="postBody">The additional contents to be added to request
    /// POST body.</param>
    /// <returns>A preview of <see cref="MAX_ERROR_LENGTH"/> bytes.</returns>
    private byte[] DownloadReportToDisk(string downloadUrl, AdWordsAppConfig config,
        bool returnMoneyInMicros, string path, string postBody) {
      using (FileStream fs = File.OpenWrite(path)) {
        return DownloadReportToStream(downloadUrl, config, returnMoneyInMicros, fs, postBody);
      }
    }

    /// <summary>
    /// Downloads the report to disk.
    /// </summary>
    /// <param name="downloadUrl">The report download URL.</param>
    /// <param name="config">The configuration settings to be used when
    /// downloading the report.</param>
    /// <param name="returnMoneyInMicros">True, if the report values should be
    /// downloaded in micros.</param>
    /// <param name="outputStream">The output stream to which the downloaded
    /// report should be saved.</param>
    /// <param name="postBody">The additional contents to be added to request
    /// POST body.</param>
    /// <returns>A preview of <see cref="MAX_ERROR_LENGTH" /> bytes.</returns>
    private byte[] DownloadReportToStream(string downloadUrl, AdWordsAppConfig config,
        bool returnMoneyInMicros, Stream outputStream, string postBody) {
      WebRequest request = HttpWebRequest.Create(downloadUrl);
      if (!string.IsNullOrEmpty(postBody)) {
        request.Method = "POST";
      }
      request.Proxy = config.Proxy;

      if (!string.IsNullOrEmpty(config.ClientEmail)) {
        request.Headers.Add("clientEmail: " + config.ClientEmail);
      } else if (!string.IsNullOrEmpty(config.ClientCustomerId)) {
        request.Headers.Add("clientCustomerId: " + config.ClientCustomerId);
      }
      request.ContentType = "application/x-www-form-urlencoded";
      if (config.AuthorizationMethod == AdWordsAuthorizationMethod.OAuth) {
        if (this.User.OAuthProvider != null) {
          AdsOAuthProvider provider = this.User.OAuthProvider;
          provider.GenerateAccessToken();
          request.Headers["Authorization"] = provider.GetAuthHeader(downloadUrl);
        } else {
          throw new AdWordsApiException(null, AdWordsErrorMessages.OAuthProviderCannotBeNull);
        }
      } else if (config.AuthorizationMethod == AdWordsAuthorizationMethod.ClientLogin) {
        string authToken = (!string.IsNullOrEmpty(config.AuthToken)) ? config.AuthToken :
            new AuthToken(config, AdWordsSoapClient.SERVICE_NAME, config.Email,
                config.Password).GetToken();
        request.Headers["Authorization"] = "GoogleLogin auth=" + authToken;
      }

      request.Headers.Add("returnMoneyInMicros: " + returnMoneyInMicros.ToString().ToLower());

      if (!string.IsNullOrEmpty(postBody)) {
        using (StreamWriter writer = new StreamWriter(request.GetRequestStream())) {
          writer.Write(postBody);
        }
      }

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
