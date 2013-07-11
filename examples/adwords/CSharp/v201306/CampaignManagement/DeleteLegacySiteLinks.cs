// Copyright 2013, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201306;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201306 {
  /// <summary>
  /// This code example shows how to delete site links from an existing
  /// campaign. To add site links to an existing campaign, run AddSiteLinks.cs.
  /// To get existing campaigns, run GetCampaigns.cs.
  ///
  /// Tags: CampaignAdExtensionService.mutate
  /// </summary>
  public class DeleteLegacySitelinks : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      DeleteLegacySitelinks codeExample = new DeleteLegacySitelinks();
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
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to remove site links from an existing campaign. To " +
            "add site links to an existing campaign, run AddSiteLinks.cs. To get existing " +
            "campaigns, run GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign from which sitelinks are
    /// deleted.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService)user.GetService(AdWordsService.v201306.
          CampaignAdExtensionService);

      long siteLinkExtensionId = -1;

      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdExtensionId", "Status"};

      // Filter the results for specified campaign id.
      Predicate campaignPredicate = new Predicate();
      campaignPredicate.@operator = PredicateOperator.EQUALS;
      campaignPredicate.field = "CampaignId";
      campaignPredicate.values = new string[] {campaignId.ToString()};

      // Filter the results for active campaign ad extensions.
      Predicate statusPredicate = new Predicate();
      statusPredicate.@operator = PredicateOperator.EQUALS;
      statusPredicate.field = "Status";
      statusPredicate.values = new string[] {CampaignAdExtensionStatus.ACTIVE.ToString()};

      // Filter for sitelinks ad extension type.
      Predicate typePredicate = new Predicate();
      typePredicate.@operator = PredicateOperator.EQUALS;
      typePredicate.field = "AdExtensionType";
      typePredicate.values = new string[] {"SITELINKS_EXTENSION"};

      selector.predicates = new Predicate[] {campaignPredicate, statusPredicate, typePredicate};

      // Get the campaign ad extension containing sitelinks.
      CampaignAdExtensionPage page = campaignExtensionService.get(selector);
      if (page != null && page.entries != null && page.entries.Length > 0) {
        siteLinkExtensionId = page.entries[0].adExtension.id;
      }

      // There are no site link extensions in this campaign.
      if (siteLinkExtensionId == -1) {
        return;
      }

      CampaignAdExtension campaignAdExtension = new CampaignAdExtension();
      campaignAdExtension.campaignId = campaignId;
      campaignAdExtension.adExtension = new AdExtension();
      campaignAdExtension.adExtension.id = siteLinkExtensionId;

      CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
      operation.@operator = Operator.REMOVE;
      operation.operand = campaignAdExtension;

      try {
        CampaignAdExtensionReturnValue retVal =
            campaignExtensionService.mutate(new CampaignAdExtensionOperation[] {operation});
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          CampaignAdExtension campaignExtension = retVal.value[0];
          Console.WriteLine("Deleted a campaign ad extension with id = \"{0}\" and " +
              "status = \"{1}\"", campaignExtension.adExtension.id, campaignExtension.status);
          foreach (Sitelink siteLink in
              (campaignExtension.adExtension as SitelinksExtension).sitelinks) {
            Console.WriteLine("-- Site link text is \"{0}\" and destination url is {1}",
                siteLink.displayText, siteLink.destinationUrl);
          }
        } else {
          Console.WriteLine("No site links were deleted.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to delete site links.", ex);
      }
    }
  }
}
