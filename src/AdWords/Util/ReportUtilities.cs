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
using Google.Api.Ads.Common.Util;
using Google.Api.Ads.AdWords.v13;
using Google.Api.Ads.AdWords.v201003;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml;
using System.Globalization;

namespace Google.Api.Ads.AdWords.Util {
  /// <summary>
  /// Defines report utility functions for the client library.
  /// </summary>
  public class ReportUtilities {
    /// <summary>
    /// The user associated with this object.
    /// </summary>
    private AdWordsUser user;

    /// <summary>
    /// Wait time in ms for report functions.
    /// </summary>
    private const int WAIT_TIME = 30000;

    /// <summary>
    /// Maximum length of report to read to check if it contains errors.
    /// </summary>
    private const int MAX_ERROR_LENGTH = 2 ^ 16;

    /// <summary>
    /// The callback delegate used by <see cref="BeginDownloadReportAsCsv"/>
    /// </summary>
    private GenerateReport csvReportFunction;

    /// <summary>
    /// The callback delegate used by <see cref="BeginDownloadReportAsXml"/>
    /// </summary>
    private GenerateReport xmlReportFunction;

    /// <summary>
    /// Delegate for report generation purposes.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">File path where the report is to be saved.
    /// </param>
    public delegate void GenerateReport(ReportJob reportJob, string filePath);

    /// <summary>
    /// Returns the user associated with this object.
    /// </summary>
    public AdWordsUser User {
      get {
        return user;
      }
      set {
        user = value;
      }
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="user">AdWords user to be used along with this
    /// utilities object.</param>
    public ReportUtilities(AdWordsUser user) {
      this.user = user;
      csvReportFunction = new GenerateReport(GenerateCsvReport);
      xmlReportFunction = new GenerateReport(GenerateXmlReport);
    }

    /// <summary>
    /// An asynchronous way to download a report as CSV. See
    /// DownloadReportAsCsvDemo for an example on how to use
    /// this method.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">The location to which this report should
    /// be saved.</param>
    /// <param name="callback">The callback to be called once the report
    /// is saved.</param>
    /// <returns>The IAsyncResult object associated with this asynchronous
    /// call. </returns>
    public IAsyncResult BeginDownloadReportAsCsv(ReportJob reportJob, string filePath,
        AsyncCallback callback) {
      return csvReportFunction.BeginInvoke(reportJob, filePath, callback, csvReportFunction);
    }

    /// <summary>
    /// This is the counterpart of <see cref="BeginDownloadReportAsCsv"/>.
    /// Call this function to complete the asynchronous call.
    /// </summary>
    /// <param name="result">The IAsyncResult object, returned from
    /// <see cref="BeginDownloadReportAsCsv"/></param>
    /// <remarks>This call should be enclosed in a try-catch block block,
    /// since any exception generated during the asynchronous call will
    /// be thrown at this stage.</remarks>
    public void EndDownloadReportAsCsv(IAsyncResult result) {
      csvReportFunction.EndInvoke(result);
    }

    /// <summary>
    /// A synchronous method to download a report as CSV.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">The location to which this report should
    /// be saved.</param>
    public void DownloadReportAsCsv(ReportJob reportJob, string filePath) {
      csvReportFunction.Invoke(reportJob, filePath);
    }

    /// <summary>
    /// An asynchronous way to download a report as XML. See
    /// DownloadReportAsXmlDemo for an example on how to use
    /// this method.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">The location to which this report should
    /// be saved.</param>
    /// <param name="callback">The callback to be called once the report
    /// is saved.</param>
    /// <returns>The IAsyncResult object associated with this asynchronous
    /// call. See <see cref="EndDownloadReportAsXml"/> to complete the
    /// call sequence.
    /// </returns>
    public IAsyncResult BeginDownloadReportAsXml(ReportJob reportJob, string filePath,
        AsyncCallback callback) {
      return xmlReportFunction.BeginInvoke(reportJob, filePath, callback, xmlReportFunction);
    }

    /// <summary>
    /// This is the counterpart of <see cref="EndDownloadReportAsXml"/>.
    /// Call this function to complete the asynchronous call.
    /// </summary>
    /// <param name="result">The IAsyncResult object, returned from
    /// <see cref="EndDownloadReportAsXml"/></param>
    /// <remarks>This call should be enclosed in a try-catch block block,
    /// since any exception generated during the asynchronous call will
    /// be thrown at this stage.</remarks>
    public void EndDownloadReportAsXml(IAsyncResult result) {
      xmlReportFunction.EndInvoke(result);
    }

    /// <summary>
    /// A synchronous method to download a report as XML.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">The location to which this report should
    /// be saved.</param>
    public void DownloadReportAsXml(ReportJob reportJob, string filePath) {
      xmlReportFunction.Invoke(reportJob, filePath);
    }

    /// <summary>
    /// Generates the report in CSV format. This function is used by
    /// <see cref="DownloadReportAsCsv"/>.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">The location to which this report should
    /// be saved.</param>
    private void GenerateCsvReport(ReportJob reportJob, string filePath) {
      ConvertXmlToCsv(GetReportXml(reportJob)).Write(filePath);
    }

    /// <summary>
    /// Generates the report in XML format. This function is used by
    /// <see cref="DownloadReportAsXml"/>.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">The location to which this report should
    /// be saved.</param>
    private void GenerateXmlReport(ReportJob reportJob, string filePath) {
      using (StreamWriter writer = new StreamWriter(filePath)) {
        writer.Write(GetReportXml(reportJob));
      }
    }

    /// <summary>
    /// Converts the XML report into CSV format
    /// </summary>
    /// <param name="reportXml">The downloaded XML report, as a string.</param>
    /// <returns>A CsvFile object, which contains the report in CSV format.
    /// </returns>
    private static CsvFile ConvertXmlToCsv(string reportXml) {
      XmlDocument xDoc = new XmlDocument();
      xDoc.LoadXml(reportXml);
      XmlNodeList xColumnNodes =
          xDoc.SelectNodes("/report/table/columns/column/@name");

      CsvFile csvFile = new CsvFile();
      foreach (XmlNode xColumnNode in xColumnNodes) {
        csvFile.Headers.Add(xColumnNode.Value);
      }

      XmlNodeList xRecordNodes = xDoc.SelectNodes("report/table/rows/row");

      foreach (XmlElement xRecord in xRecordNodes) {
        string[] items = new string[csvFile.Headers.Count];

        foreach (XmlAttribute attr in xRecord.Attributes) {
          for (int i = 0; i < csvFile.Headers.Count; i++) {
            if (csvFile.Headers[i] == attr.Name) {
              items[i] = attr.Value;
            }
          }
        }
        csvFile.Records.Add(items);
      }
      return csvFile;
    }

    /// <summary>
    /// Runs a report job and returns the XML as a string. This function
    /// is used by other report generating functions.
    /// </summary>
    /// <param name="reportJob">The report job to be run on the server.</param>
    /// <returns>The downloaded XML report, as a string.</returns>
    private string GetReportXml(ReportJob reportJob) {
      try {
        ReportService service =
            (ReportService) User.GetService(AdWordsService.v13.ReportService);
        service.validateReportJob(reportJob);

        // Submit the request for the report.
        long jobId = service.scheduleReportJob(reportJob);

        // Wait until the report has been generated.
        ReportJobStatus status = service.getReportJobStatus(jobId);

        while (status != ReportJobStatus.Completed &&
            status != ReportJobStatus.Failed) {
          Thread.Sleep(WAIT_TIME);
          status = service.getReportJobStatus(jobId);
        }

        if (status == ReportJobStatus.Failed) {
          throw new ReportsException(AdWordsErrorMessages.ReportGenerationFailed);
        } else {
          return Encoding.UTF8.GetString(MediaUtilities.GetAssetDataFromUrl(
              service.getReportDownloadUrl(jobId)));
        }
      } catch (SoapException e) {
        throw new ReportsException(AdWordsErrorMessages.ReportGenerationFailed, e);
      }
    }

    /// <summary>
    /// Downloads a new instance of a report to a file.
    /// </summary>
    /// <param name="reportDefinitionId">The id of the ReportDefinition to
    /// download.</param>
    /// <param name="filePath">The path of the file to download the report
    /// to.</param>
    public void DownloadReportDefinition(long reportDefinitionId, string filePath) {
      DownloadReportDefinition(reportDefinitionId, filePath, new AdWordsAppConfig(), false);
    }

    /// <summary>
    /// Downloads a new instance of a report to a file.
    /// </summary>
    /// <param name="reportDefinitionId">The id of the ReportDefinition to
    /// download.</param>
    /// <param name="filePath">The path of the file to download the report
    /// to.</param>
    /// <param name="config">The configuration to be used while downloading
    /// this report.</param>
    public void DownloadReportDefinition(long reportDefinitionId, string filePath,
        AdWordsAppConfig config) {
      DownloadReportDefinition(reportDefinitionId, filePath, config, false);
    }

    /// <summary>
    /// Downloads a new instance of a report to a file.
    /// </summary>
    /// <param name="reportDefinitionId">The id of the ReportDefinition to
    /// download.</param>
    /// <param name="filePath">The path of the file to download the report
    /// to.</param>
    /// <param name="config">The configuration to be used while downloading
    /// this report.</param>
    /// <param name="downloadInMicros">True, if the monetary units in reports
    /// should be downloaded in micros and not in actual currency values. See
    /// http://adwordsapi.blogspot.com/2010/11/change-to-currency-formatting-in-report.html
    /// for details.</param>
    public void DownloadReportDefinition(long reportDefinitionId, string filePath,
        AdWordsAppConfig config, bool downloadInMicros) {
      Uri downloadUrl = new Uri(string.Format(CultureInfo.InvariantCulture,
          "{0}/api/adwords/reportdownload?__rd={1}", config.AdWordsApiServer, reportDefinitionId));

      ReportDefinitionService service = (ReportDefinitionService) user.GetService(
         AdWordsService.v201003.ReportDefinitionService);

      WebRequest request = HttpWebRequest.Create(downloadUrl);

      if (!string.IsNullOrEmpty(service.RequestHeader.clientCustomerId)) {
        request.Headers.Add("clientCustomerId: " + service.RequestHeader.clientCustomerId);
      } else if (!string.IsNullOrEmpty(service.RequestHeader.clientEmail)) {
        request.Headers.Add("clientEmail: " + service.RequestHeader.clientEmail);
      }

      if (!string.IsNullOrEmpty(service.RequestHeader.authToken)) {
        request.Headers.Add("Authorization: GoogleLogin auth=" + service.RequestHeader.authToken);
      }

      request.Headers.Add("returnMoneyInMicros: " + downloadInMicros.ToString().ToLower());

      if (config.Proxy != null) {
        request.Proxy = config.Proxy;
      }

      try {
        WebResponse response = request.GetResponse();
        DownloadReportToFile(filePath, response);
      } catch (Exception ex) {
        throw new ReportsException(AdWordsErrorMessages.ReportGenerationFailed, ex);
      }
    }

    /// <summary>
    /// Downloads a report to file.
    /// </summary>
    /// <param name="filePath">The file to which report should be downloaded.
    /// </param>
    /// <param name="response">The http web response for downloading report.
    /// </param>
    private static void DownloadReportToFile(string filePath, WebResponse response) {
      FileStream outputStream = File.Create(filePath);
      using (Stream responseStream = response.GetResponseStream()) {
        MediaUtilities.CopyStream(responseStream, outputStream);
      }
      outputStream.Close();
      if (!IsValidReport(filePath)) {
        throw new ReportsException(AdWordsErrorMessages.ReportIsInvalid);
      }
    }

    /// <summary>
    /// Checks if a downloaded report is valid.
    /// </summary>
    /// <param name="filePath">Path of downloaded report.</param>
    private static bool IsValidReport(string filePath) {
      using (StreamReader reader = new StreamReader(filePath)) {
        char[] buffer = new char[MAX_ERROR_LENGTH];
        reader.Read(buffer, 0, MAX_ERROR_LENGTH);
        string contents = new string(buffer);
        if (!string.IsNullOrEmpty(contents)) {
          return !Regex.IsMatch(contents, "\\!\\!\\!([^\\|]*)\\|\\|\\|(.*)");
        }
      }
      return true;
    }
  }
}
