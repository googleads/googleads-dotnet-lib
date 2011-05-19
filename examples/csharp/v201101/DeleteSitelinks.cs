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
  /// This code example shows how to delete site links from an existing
  /// campaign. To add site links to an existing campaign, run AddSiteLinks.cs.
  /// To get existing campaigns, run GetAllCampaigns.cs.
  ///
  /// Tags: CampaignAdExtensionService.mutate
  /// </summary>
  class DeleteSitelinks : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to remove site links from an existing campaign. To " +
            "add site links to an existing campaign, run AddSiteLinks.cs. To get existing " +
            "campaigns, run GetAllCampaigns.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeleteSitelinks();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService)user.GetService(AdWordsService.v201101.
          CampaignAdExtensionService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));
      long siteLinkExtensionId = -1;

      // Get the campaign ad extension containing sitelinks.
      Selector selector = new Selector();

      Predicate campaignPredicate = new Predicate();
      campaignPredicate.@operator = PredicateOperator.EQUALS;
      campaignPredicate.field = "CampaignId";
      campaignPredicate.values = new string[] {campaignId.ToString()};

      Predicate statusPredicate = new Predicate();
      statusPredicate.@operator = PredicateOperator.EQUALS;
      statusPredicate.field = "Status";
      statusPredicate.values = new string[] {
          CampaignAdExtensionStatus.ACTIVE.ToString()
      };

      selector.predicates = new Predicate[] {campaignPredicate, statusPredicate};
      selector.fields = new string[] {"AdExtensionId", "Status"};

      CampaignAdExtensionPage page = campaignExtensionService.get(selector);
      if (page != null && page.entries != null) {
        foreach (CampaignAdExtension extension in page.entries) {
          if (extension.adExtension is SitelinksExtension) {
            siteLinkExtensionId = extension.adExtension.id;
            break;
          }
        }
      }

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
          foreach (CampaignAdExtension campaignExtension in retVal.value) {
            Console.WriteLine("Deleted a campaign ad extension with id = \"{0}\" and " +
                "status = \"{1}\"", campaignExtension.adExtension.id, campaignExtension.status);
            foreach (Sitelink siteLink in
                (campaignExtension.adExtension as SitelinksExtension).sitelinks) {
              Console.WriteLine("-- Site link text is \"{0}\" and destination url is {1}",
                  siteLink.displayText, siteLink.destinationUrl);
            }
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to delete site links. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
