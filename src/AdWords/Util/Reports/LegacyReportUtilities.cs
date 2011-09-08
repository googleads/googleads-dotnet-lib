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
using Google.Api.Ads.AdWords.v13;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml;

namespace Google.Api.Ads.AdWords.Util.Reports {
  /// <summary>
  /// Defines report utility functions for the client library that uses the v13
  /// ReportService.
  /// </summary>
  /// <remarks>The v13 ReportService is deprecated. If you are using a method
  /// from this class, migrate to the equivalent method from
  /// <see cref="ReportUtilities"/> class instead.</remarks>
  public abstract class LegacyReportUtilities {
    /// <summary>
    /// The user associated with this object.
    /// </summary>
    private AdWordsUser user;

    /// <summary>
    /// Wait time in ms for report functions.
    /// </summary>
    protected const int WAIT_TIME = 30000;

    /// <summary>
    /// Maximum number of times to poll the report server before giving up.
    /// Defaults to maximum number of attempts that can be made in 30 minutes.
    /// </summary>
    private int maxPollingAttempts = 30 * 60 * 1000 / WAIT_TIME;

    /// <summary>
    /// Gets or sets the maximum number of times to poll the report server
    /// before giving up. Defaults to maximum number of attempts that can be
    /// made in 30 minutes.
    /// </summary>
    /// <value>
    public int MaxPollingAttempts {
      get {
        return maxPollingAttempts;
      }
      set {
        maxPollingAttempts = value;
      }
    }

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
    public LegacyReportUtilities(AdWordsUser user) {
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
    /// call.</returns>
    [Obsolete("This method uses the deprecated v13.ReportService. Use " +
        "ReportUtilities.BeginDownloadReportFromDefinition instead.")]
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
    [Obsolete("This method uses the deprecated v13.ReportService. Use " +
        "ReportUtilities.EndDownloadReportFromDefinition instead.")]
    public void EndDownloadReportAsCsv(IAsyncResult result) {
      csvReportFunction.EndInvoke(result);
    }

    /// <summary>
    /// A synchronous method to download a report as CSV.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">The location to which this report should
    /// be saved.</param>
    [Obsolete("This method uses the deprecated v13.ReportService. Use " +
        "ReportUtilities.DownloadReportFromDefinition instead.")]
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
    [Obsolete("This method uses the deprecated v13.ReportService. Use " +
        "ReportUtilities.BeginDownloadReportFromDefinition instead.")]
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
    [Obsolete("This method uses the deprecated v13.ReportService. Use " +
        "ReportUtilities.EndDownloadReportFromDefinition instead.")]
    public void EndDownloadReportAsXml(IAsyncResult result) {
      xmlReportFunction.EndInvoke(result);
    }

    /// <summary>
    /// A synchronous method to download a report as XML.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">The location to which this report should
    /// be saved.</param>
    [Obsolete("This method uses the deprecated v13.ReportService. Use " +
        "ReportUtilities.DownloadReportFromDefinition instead.")]
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
      XmlDocument xDoc = SerializationUtilities.LoadXml(reportXml);
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
            (ReportService)User.GetService(AdWordsService.v13.ReportService);
        service.validateReportJob(reportJob);

        // Submit the request for the report.
        long jobId = service.scheduleReportJob(reportJob);

        // Wait until the report has been generated.
        ReportJobStatus status = service.getReportJobStatus(jobId);

        int pollingCount = 0;
        while (status != ReportJobStatus.Completed &&
            status != ReportJobStatus.Failed && pollingCount < MaxPollingAttempts) {
          Thread.Sleep(WAIT_TIME);
          pollingCount++;
          status = service.getReportJobStatus(jobId);
        }

        if (status == ReportJobStatus.Failed) {
          throw new ReportsException(AdWordsErrorMessages.ReportGenerationFailed);
        } else if (status == ReportJobStatus.Completed) {
          return Encoding.UTF8.GetString(MediaUtilities.GetAssetDataFromUrl(
              service.getReportDownloadUrl(jobId)));
        } else {
          throw new ReportsException(AdWordsErrorMessages.ReportNumPollsExceeded);
        }
      } catch (SoapException e) {
        throw new ReportsException(AdWordsErrorMessages.ReportGenerationFailed, e);
      }
    }
  }
}
