// Copyright 2016, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example gets report fields.
  /// </summary>
  public class GetReportFields : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetReportFields codeExample = new GetReportFields();
      Console.WriteLine(codeExample.Description);
      try {
        ReportDefinitionReportType reportType = (ReportDefinitionReportType) Enum.Parse(
            typeof(ReportDefinitionReportType), "INSERT_REPORT_TYPE_HERE");
        codeExample.Run(new AdWordsUser(), reportType);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets report fields.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="reportType">The report type to be run.</param>
    public void Run(AdWordsUser user, ReportDefinitionReportType reportType) {
      // Get the ReportDefinitionService.
      ReportDefinitionService reportDefinitionService = (ReportDefinitionService) user.GetService(
          AdWordsService.v201603.ReportDefinitionService);

      try {
        // Get the report fields.
        ReportDefinitionField[] reportDefinitionFields = reportDefinitionService.getReportFields(
            reportType);
        if (reportDefinitionFields != null && reportDefinitionFields.Length > 0) {
          // Display report fields.
          Console.WriteLine("The report type '{0}' contains the following fields:", reportType);

          foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
            Console.Write("- {0} ({1})", reportDefinitionField.fieldName,
                reportDefinitionField.fieldType);
            if (reportDefinitionField.enumValues != null) {
              Console.Write(" := [{0}]", String.Join(", ", reportDefinitionField.enumValues));
            }
            Console.WriteLine();
          }
        } else {
          Console.WriteLine("This report type has no fields.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to retrieve fields for report type.", e);
      }
    }
  }
}
