// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
  /// <summary>
  /// This code example gets report fields.
  ///
  /// Tags: ReportDefinitionService.getReportFields
  /// </summary>
  public class GetReportFields : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetReportFields();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
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
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"REPORT_TYPE"};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the ReportDefinitionService.
      ReportDefinitionService reportDefinitionService = (ReportDefinitionService) user.GetService(
          AdWordsService.v201109_1.ReportDefinitionService);

      // The type of the report to get fields for.
      // E.g.: KEYWORDS_PERFORMANCE_REPORT
      ReportDefinitionReportType reportType = (ReportDefinitionReportType) Enum.Parse(
          typeof(ReportDefinitionReportType), parameters["REPORT_TYPE"]);

      try {
        // Get the report fields.
        ReportDefinitionField[] reportDefinitionFields = reportDefinitionService.getReportFields(
            reportType);
        if (reportDefinitionFields != null && reportDefinitionFields.Length > 0) {
          // Display report fields.
          writer.WriteLine("The report type '{0}' contains the following fields:", reportType);

          foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
            writer.Write("- {0} ({1})", reportDefinitionField.fieldName,
                reportDefinitionField.fieldType);
            if (reportDefinitionField.enumValues != null) {
              writer.Write(" := [{0}]", String.Join(", ", reportDefinitionField.enumValues));
            }
            writer.WriteLine();
          }
        } else {
          writer.WriteLine("This report type has no fields.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve fields for report type.", ex);
      }
    }
  }
}
