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

using com.google.api.adwords.lib;
using com.google.api.adwords.lib.util;
using com.google.api.adwords.v13;

using System;
using System.IO;
using System.Web.Services.Protocols;

namespace com.google.api.adwords.examples.v13 {
  /// <summary>
  /// This code example shows how to use AdWordsUtilities to download a report
  /// in XML format.
  /// </summary>
  class DownloadReportAsXmlDemo : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to use AdWordsUtilities to download a report in " +
            "XML format.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      ReportUtilities utilities = new ReportUtilities(user);

      // Create the report job.
      DefinedReportJob reportJob = new DefinedReportJob();
      reportJob.name = "Keyword Report";
      reportJob.selectedReportType = "Keyword";
      reportJob.aggregationTypes = new String[] {"Daily"};
      reportJob.adWordsType = AdWordsType.SearchOnly;
      reportJob.adWordsTypeSpecified = true;
      reportJob.endDay = DateTime.Today;  // defaults to today
      reportJob.startDay = new DateTime(2009, 1, 1);
      reportJob.selectedColumns = new String[] {
          "Campaign", "AdGroup", "Keyword", "KeywordStatus", "KeywordMinCPC",
          "KeywordDestUrlDisplay", "Impressions", "Clicks", "CTR",
          "AveragePosition"};

      string filePath = "C:\\report.xml";

      // Option 1: Call the async version and wait on delegate handle
      // Suited for UI as well as console applications, depending on
      // the context.
      IAsyncResult result = utilities.BeginDownloadReportAsXml(reportJob, filePath, null);
      result.AsyncWaitHandle.WaitOne();
      try {
        utilities.EndDownloadReportAsXml(result);
      } catch (SoapException ex) {
        Console.WriteLine("An exception occurred while generating reports. " +
            "Message says : {0}", ex.Message);
      }

      // Option 2: Call the normal version. Most suited for console
      // applications.
      try {
        utilities.DownloadReportAsXml(reportJob, filePath);
      } catch (SoapException ex) {
        Console.WriteLine("An exception occurred while generating reports. Message says : {0}",
            ex.Message);
      }

      // Option 3: Call the async version get a call on your callback
      // Most suited for UI applications.
      utilities.BeginDownloadReportAsXml(reportJob, filePath,
          new AsyncCallback(reportDownloadCallback));
    }

    void reportDownloadCallback(IAsyncResult result) {
      ReportUtilities.GenerateReport asyncDelegate =
          (ReportUtilities.GenerateReport) result.AsyncState;
      try {
        asyncDelegate.EndInvoke(result);
      } catch (SoapException ex) {
        Console.WriteLine("An exception occurred while generating reports. Message says : {0}",
            ex.Message);
      }
    }
  }
}
