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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example adds a keywords performance report. To get ad groups,
  /// run GetAllAdGroups.cs. To get report fields, run GetReportFields.cs.
  ///
  /// Tags: ReportDefinitionService.mutate
  /// </summary>
  class AddKeywordsPerformanceReportDefinition : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a keywords performance report. To get ad groups, " +
            "run GetAllAdGroups.cs. To get report fields, run GetReportFields.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddKeywordsPerformanceReportDefinition();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the GetReportDefinitionService.
      ReportDefinitionService reportDefinitionService = (ReportDefinitionService) user.GetService(
          AdWordsService.v201101.ReportDefinitionService);

      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));
      string startDate = _T("INSERT_START_DATE_HERE");
      string endDate = _T("INSERT_END_DATE_HERE");

      // Create ad group predicate.
      Predicate adGroupPredicate = new Predicate();
      adGroupPredicate.field = "AdGroupId";
      adGroupPredicate.@operator = PredicateOperator.EQUALS;
      adGroupPredicate.values = new string[] {adGroupId.ToString()};

      // Create selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdGroupId", "Id", "KeywordText", "KeywordMatchType",
      "Impressions", "Clicks", "Cost"};
      selector.predicates = new Predicate[] {adGroupPredicate};
      selector.dateRange = new DateRange();
      selector.dateRange.min = startDate;
      selector.dateRange.max = endDate;

      // Create report definition.
      ReportDefinition reportDefinition = new ReportDefinition();
      reportDefinition.reportName = "Keywords performance report #" + GetTimeStamp();
      reportDefinition.dateRangeType = ReportDefinitionDateRangeType.CUSTOM_DATE;
      reportDefinition.reportType = ReportDefinitionReportType.KEYWORDS_PERFORMANCE_REPORT;
      reportDefinition.downloadFormat = DownloadFormat.XML;
      reportDefinition.selector = selector;

      // Create operations.
      ReportDefinitionOperation operation = new ReportDefinitionOperation();
      operation.operand = reportDefinition;
      operation.@operator = Operator.ADD;

      try {
        // Add report definition.
        ReportDefinition[] result = reportDefinitionService.mutate(
            new ReportDefinitionOperation[] {operation});

        // Display report definitions.
        if (result != null) {
          foreach (ReportDefinition temp in result) {
            Console.WriteLine("Report definition with name '{0}' and id '{1}' was added.",
                temp.reportName, temp.id);
          }
        } else {
          Console.WriteLine("No report definitions were added.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to add report definition. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
