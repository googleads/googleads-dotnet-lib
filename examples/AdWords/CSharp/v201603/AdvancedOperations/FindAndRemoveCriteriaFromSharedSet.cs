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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example demonstrates how to find and remove shared sets and
  /// shared set criteria.
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
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
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
              AdWordsService.v201603.CampaignSharedSetService);

      Selector selector = new Selector() {
        fields = new string[] {
          CampaignSharedSet.Fields.SharedSetId,
          CampaignSharedSet.Fields.CampaignId,
          CampaignSharedSet.Fields.SharedSetName,
          CampaignSharedSet.Fields.SharedSetType
        },
        predicates = new Predicate[] {
          Predicate.Equals(CampaignSharedSet.Fields.CampaignId, campaignId),
          Predicate.In(CampaignSharedSet.Fields.SharedSetType,
              new string[] { SharedSetType.NEGATIVE_KEYWORDS.ToString() }),
        },
        paging = Paging.Default,
      };

      List<string> sharedSetIds = new List<string>();
      CampaignSharedSetPage page = new CampaignSharedSetPage();

      try {
        do {
          // Get the campaigns.
          page = campaignSharedSetService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;
            foreach (CampaignSharedSet campaignSharedSet in page.entries) {
              sharedSetIds.Add(campaignSharedSet.sharedSetId.ToString());
              Console.WriteLine("{0}) Campaign shared set ID {1} and name '{2}' found for " +
                  "campaign ID {3}.\n", i + 1, campaignSharedSet.sharedSetId,
                  campaignSharedSet.sharedSetName, campaignSharedSet.campaignId);
              i++;
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
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
          (SharedCriterionService) user.GetService(AdWordsService.v201603.SharedCriterionService);

      Selector selector = new Selector() {
        fields = new string[] {
            SharedSet.Fields.SharedSetId, Criterion.Fields.Id,
            Keyword.Fields.KeywordText, Keyword.Fields.KeywordMatchType,
            Placement.Fields.PlacementUrl
        },
        predicates = new Predicate[] {
            Predicate.In(SharedSet.Fields.SharedSetId, sharedSetIds)
        },
        paging = Paging.Default
      };

      List<SharedCriterion> sharedCriteria = new List<SharedCriterion>();
      SharedCriterionPage page = new SharedCriterionPage();

      try {
        do {
          // Get the campaigns.
          page = sharedCriterionService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;
            foreach (SharedCriterion sharedCriterion in page.entries) {
              switch (sharedCriterion.criterion.type) {
                case CriterionType.KEYWORD:
                  Keyword keyword = (Keyword) sharedCriterion.criterion;
                  Console.WriteLine("{0}) Shared negative keyword with ID {1} and text '{2}' " +
                      "was found.", i + 1, keyword.id, keyword.text);
                  break;

                case CriterionType.PLACEMENT:
                  Placement placement = (Placement) sharedCriterion.criterion;
                  Console.WriteLine("{0}) Shared negative placement with ID {1} and URL '{2}' " +
                      "was found.", i + 1, placement.id, placement.url);
                  break;

                default:
                  Console.WriteLine("{0}) Shared criteria with ID {1} was found.",
                      i + 1, sharedCriterion.criterion.id);
                  break;
              }
              i++;
              sharedCriteria.Add(sharedCriterion);
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);

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
          AdWordsService.v201603.SharedCriterionService);

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
