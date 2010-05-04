// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.v13;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml;

namespace com.google.api.adwords.lib.util {
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
    const int waitTime = 30000;

    /// <summary>
    /// The callback delegate used by <see cref="BeginDownloadReportAsCsv"/>
    /// </summary>
    private GenerateReport csvReportFunction = null;

    /// <summary>
    /// The callback delegate used by <see cref="BeginDownloadReportAsXml"/>
    /// </summary>
    private GenerateReport xmlReportFunction = null;

    /// <summary>
    /// Delegate for report generation purposes.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">File path where the report is to be saved.
    /// </param>
    public delegate void GenerateReport(ReportJob reportJob, string filePath);

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
    /// Returns the user associated with this object.
    /// </summary>
    public AdWordsUser User {
      get {return user;}
      set {user = value;}
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
    public IAsyncResult BeginDownloadReportAsCsv(DefinedReportJob reportJob,
        string filePath, AsyncCallback callback) {
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
    public void DownloadReportAsCsv(DefinedReportJob reportJob, string filePath) {
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
    public IAsyncResult BeginDownloadReportAsXml(DefinedReportJob reportJob,
        string filePath, AsyncCallback callback) {
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
    public void DownloadReportAsXml(DefinedReportJob reportJob,
        string filePath) {
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
      string reportXml = GetReportXml(reportJob);
      CsvFile csvReport = ConvertXmlToCsv(reportXml);
      csvReport.Write(filePath);
      return;
    }

    /// <summary>
    /// Generates the report in XML format. This function is used by
    /// <see cref="DownloadReportAsXml"/>.
    /// </summary>
    /// <param name="reportJob">The report job to be run.</param>
    /// <param name="filePath">The location to which this report should
    /// be saved.</param>
    private void GenerateXmlReport(ReportJob reportJob, string filePath) {
      string reportXml = GetReportXml(reportJob);

      StreamWriter writer = new StreamWriter(filePath);
      writer.Write(reportXml);
      writer.Close();
    }

    /// <summary>
    /// Converts the XML report into CSV format
    /// </summary>
    /// <param name="reportXml">The downloaded XML report, as a string.</param>
    /// <returns>A CsvFile object, which contains the report in CSV format.
    /// </returns>
    private CsvFile ConvertXmlToCsv(string reportXml) {
      XmlDocument xDoc = new XmlDocument();
      xDoc.LoadXml(reportXml);
      XmlNodeList xColumnNodes =
          xDoc.SelectNodes("/report/table/columns/column/@name");

      CsvFile csvFile = new CsvFile();
      foreach (XmlNode xColumnNode in xColumnNodes) {
        csvFile.Headers.Add(xColumnNode.Value);
      }

      XmlNodeList xRecordNodes = xDoc.SelectNodes("report/table/rows/row");
      ArrayList records = new ArrayList();
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
          Thread.Sleep(waitTime);
          status = service.getReportJobStatus(jobId);
        }

        if (status == ReportJobStatus.Failed) {
          throw new ApplicationException("Report generation failed.");
        } else {
          // Download the report.
          String url = service.getReportDownloadUrl(jobId);
          WebRequest request = HttpWebRequest.Create(url);
          WebResponse response = request.GetResponse();
          Stream responseStream = response.GetResponseStream();

          MemoryStream memStream = new MemoryStream();
          byte[] buffer = new byte[4096];
          try {
            while (true) {
              int bytesRead = responseStream.Read(buffer, 0, 4046);
              if (bytesRead == 0) {
                break;
              } else {
                memStream.Write(buffer, 0, bytesRead);
              }
            }
          } finally {
            memStream.Close();
          }
          return Encoding.UTF8.GetString(memStream.ToArray());
        }
      } catch (SoapException e) {
        throw new ApplicationException(
            "Report generation failed. See innerException for details.", e);
      }
    }
  }
}
