// Copyright 2015, Google Inc. All Rights Reserved.
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

using System;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201502;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201502 {

  /// <summary>
  /// This code example demonstrates how to find and remove shared sets and
  /// shared set criteria.
  /// 
  /// Tags: SharedSetService.mutate, SharedSetCriterionService.mutate
  /// Tags: CampaignSharedSetService.mutate
  /// </summary>
  public class FindAndRemoveCriteriaFromSharedSet : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example demonstrates how to find and remove shared sets and shared " +
            "set criteria.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      FindAndRemoveCriteriaFromSharedSet codeExample = new FindAndRemoveCriteriaFromSharedSet();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign from which items shared
    /// criteria are removed.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the list of shared sets that are attached to the campaign.
      List<string> sharedSetIds = GetSharedSetIds(user, campaignId);

      // Get the shared criteria in those shared sets.
      List<SharedCriterion> sharedCriteria = GetSharedCriteria(user, sharedSetIds);

      // Remove the shared criteria from the shared sets.
      RemoveSharedCriteria(user, sharedCriteria);
    }

    /// <summary>
    /// Gets the shared set IDs associated with a campaign.
    /// </summary>
    /// <param name="user">The user that owns the campaign.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <returns>The list of shared set IDs associated with the campaign.</returns>
    private List<string> GetSharedSetIds(AdWordsUser user, long campaignId) {
      CampaignSharedSetService campaignSharedSetService =
          (CampaignSharedSetService) user.GetService(
              AdWordsService.v201502.CampaignSharedSetService);

      Selector selector = new Selector() {
        fields = new string[] { "SharedSetId", "CampaignId", "SharedSetName", "SharedSetType" },
        predicates = new Predicate[] {
          new Predicate() {
            field = "CampaignId",
            @operator = PredicateOperator.EQUALS,
            values = new string[] {campaignId.ToString()}
          },
          new Predicate() {
            field = "SharedSetType",
            @operator = PredicateOperator.IN,
            values = new string[] {SharedSetType.NEGATIVE_KEYWORDS.ToString()}
          }
        }
      };

      List<string> sharedSetIds = new List<string>();

      int offset = 0;
      int pageSize = 500;

      CampaignSharedSetPage page = new CampaignSharedSetPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the campaigns.
          page = campaignSharedSetService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (CampaignSharedSet campaignSharedSet in page.entries) {
              sharedSetIds.Add(campaignSharedSet.sharedSetId.ToString());
              Console.WriteLine("Campaign shared set ID {0} and name '{1}' found for campaign " +
                  "ID {2}.\n", campaignSharedSet.sharedSetId, campaignSharedSet.sharedSetName,
                  campaignSharedSet.campaignId);
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        return sharedSetIds;
      } catch (Exception e) {
        throw new Exception("Failed to get shared set ids for campaign.", e);
      }
    }

    /// <summary>
    /// Gets the shared criteria in a shared set.
    /// </summary>
    /// <param name="user">The user that owns the shared set.</param>
    /// <param name="sharedSetIds">The shared criteria IDs.</param>
    /// <returns>The list of shared criteria.</returns>
    private List<SharedCriterion> GetSharedCriteria(AdWordsUser user,
        List<string> sharedSetIds) {
      SharedCriterionService sharedCriterionService =
          (SharedCriterionService) user.GetService(AdWordsService.v201502.SharedCriterionService);

      Selector selector = new Selector() {
        fields = new string[] { "SharedSetId", "Id", "KeywordText", "KeywordMatchType",
            "PlacementUrl" },
        predicates = new Predicate[] {
          new Predicate() {
            field = "SharedSetId",
            @operator = PredicateOperator.IN,
            values = sharedSetIds.ToArray()
          }
        }
      };

      List<SharedCriterion> sharedCriteria = new List<SharedCriterion>();

      int offset = 0;
      int pageSize = 500;

      SharedCriterionPage page = new SharedCriterionPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the campaigns.
          page = sharedCriterionService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (SharedCriterion sharedCriterion in page.entries) {
              switch (sharedCriterion.criterion.type) {
                case CriterionType.KEYWORD:
                  Keyword keyword = (Keyword) sharedCriterion.criterion;
                  Console.WriteLine("Shared negative keyword with ID {0} and text '{1}' was " +
                      "found.", keyword.id, keyword.text);
                  break;

                case CriterionType.PLACEMENT:
                  Placement placement = (Placement) sharedCriterion.criterion;
                  Console.WriteLine("Shared negative placement with ID {0} and URL '{1}' " +
                      "was found.", placement.id, placement.url);
                  break;

                default:
                  Console.WriteLine("Shared criteria with ID {0} was found.",
                      sharedCriterion.criterion.id);
                  break;
              }

              sharedCriteria.Add(sharedCriterion);
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);

        return sharedCriteria;
      } catch (Exception e) {
        throw new Exception("Failed to get shared criteria.", e);
      }
    }

    /// <summary>
    /// Removes a list of shared criteria.
    /// </summary>
    /// <param name="user">The user that owns the shared criteria.</param>
    /// <param name="sharedCriteria">The list shared criteria to be removed.</param>
    private void RemoveSharedCriteria(AdWordsUser user, List<SharedCriterion> sharedCriteria) {
      if (sharedCriteria.Count == 0) {
        Console.WriteLine("No shared criteria to remove.");
        return;
      }

      SharedCriterionService sharedCriterionService = (SharedCriterionService) user.GetService(
          AdWordsService.v201502.SharedCriterionService);

      List<SharedCriterionOperation> operations = new List<SharedCriterionOperation>();

      foreach (SharedCriterion sharedCriterion in sharedCriteria) {
        operations.Add(new SharedCriterionOperation() {
          @operator = Operator.REMOVE,
          operand = new SharedCriterion() {
            sharedSetId = sharedCriterion.sharedSetId,
            criterion = new Criterion() {
              id = sharedCriterion.criterion.id
            }
          }
        });
      }
      try {
        SharedCriterionReturnValue sharedCriterionReturnValue = sharedCriterionService.mutate(
            operations.ToArray());

        foreach (SharedCriterion removedCriterion in sharedCriterionReturnValue.value) {
          Console.WriteLine("Shared criterion ID {0} was successfully removed from shared " +
              "set ID {1}.", removedCriterion.criterion.id, removedCriterion.sharedSetId);
        }
      } catch (Exception e) {
        throw new Exception("Failed to remove shared criteria.", e);
      }
    }
  }
}
