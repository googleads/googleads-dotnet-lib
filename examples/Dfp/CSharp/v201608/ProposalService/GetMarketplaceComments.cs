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
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201608;
using Google.Api.Ads.Dfp.v201608;
using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201608 {
  /// <summary>
  /// This example gets the Marketplace comments for a programmatic proposal.
  /// </summary>
  public class GetMarketplaceComments : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets the Marketplace comments for a programmatic proposal.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      GetMarketplaceComments codeExample = new GetMarketplaceComments();
      long proposalId = long.Parse("INSERT_PROPOSAL_ID_HERE");
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new DfpUser(), proposalId);
      } catch (Exception e) {
        Console.WriteLine("Failed to get Marketplace comments. Exception says \"{0}\"",
            e.Message);
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public void Run(DfpUser dfpUser, long proposalId) {
      ProposalService proposalService =
          (ProposalService) dfpUser.GetService(DfpService.v201608.ProposalService);

      // Create a statement to select Marketplace comments.
      int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("proposalId = :proposalId")
          .AddValue("proposalId", proposalId);

      // Retrieve a small amount of Marketplace comments at a time, paging through until all
      // Marketplace comments have been retrieved.
      int totalResultSetSize = 0;
      do {
        MarketplaceCommentPage page = proposalService.getMarketplaceCommentsByStatement(
            statementBuilder.ToStatement());

        // Print out some information for each Marketplace comment.
        if (page.results != null) {
          totalResultSetSize = page.totalResultSetSize;
          int i = page.startIndex;
          foreach (MarketplaceComment marketplaceComment in page.results) {
            String creationTimeString = new System.DateTime(
                day: marketplaceComment.creationTime.date.day,
                month: marketplaceComment.creationTime.date.month,
                year: marketplaceComment.creationTime.date.year,
                hour: marketplaceComment.creationTime.hour,
                minute: marketplaceComment.creationTime.minute,
                second: marketplaceComment.creationTime.second
            ).ToString("s");
            Console.WriteLine(
                "{0}) Marketplace comment with creation time \"{1}\" " +
                    "and comment \"{2}\" was found.",
                i++,
                creationTimeString,
                marketplaceComment.comment
            );
          }
        }

        statementBuilder.IncreaseOffsetBy(pageSize);
      } while (statementBuilder.GetOffset() < totalResultSetSize);

      Console.WriteLine("Number of results found: {0}", totalResultSetSize);
    }
  }
}
