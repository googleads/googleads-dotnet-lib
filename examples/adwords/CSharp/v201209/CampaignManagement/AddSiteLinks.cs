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
using Google.Api.Ads.AdWords.v201209;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201209 {
  /// <summary>
  /// This code example shows how to add site links to an existing
  /// campaign. To create a campaign, run AddCampaign.cs.
  ///
  /// Tags: CampaignAdExtensionService.mutate
  /// </summary>
  public class AddSiteLinks : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddSiteLinks codeExample = new AddSiteLinks();
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
        return "This code example shows how to add site links to an existing campaign. To " +
            "create a campaign, run AddCampaign.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the camapign to which sitelinks are
    /// added.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService)user.GetService(AdWordsService.v201209.
          CampaignAdExtensionService);

      // Create the sitelinks.
      SitelinksExtension siteLinkExtension = new SitelinksExtension();

      Sitelink siteLink1 = new Sitelink();
      siteLink1.displayText = "Music";
      siteLink1.destinationUrl = "http://www.example.com/music";

      Sitelink siteLink2 = new Sitelink();
      siteLink2.displayText = "DVDs";
      siteLink2.destinationUrl = "http://www.example.com/dvds";

      Sitelink siteLink3 = new Sitelink();
      siteLink3.displayText = "New albums";
      siteLink3.destinationUrl = "http://www.example.com/albums/new";

      siteLinkExtension.sitelinks = new Sitelink[] {siteLink1, siteLink2, siteLink3};

      CampaignAdExtension campaignAdExtension = new CampaignAdExtension();
      campaignAdExtension.adExtension = siteLinkExtension;
      campaignAdExtension.campaignId = campaignId;

      // Create the operation.
      CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
      operation.@operator = Operator.ADD;
      operation.operand = campaignAdExtension;

      try {
        // Create the sitelinks.
        CampaignAdExtensionReturnValue retVal =
            campaignExtensionService.mutate(new CampaignAdExtensionOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          CampaignAdExtension campaignExtension = retVal.value[0];
          Console.WriteLine("Created a campaign ad extension with id = \"{0}\" and " +
              "status = \"{1}\"", campaignExtension.adExtension.id, campaignExtension.status);
          foreach (Sitelink siteLink in
              (campaignExtension.adExtension as SitelinksExtension).sitelinks) {
            Console.WriteLine("-- Site link text is \"{0}\" and destination url is {1}",
                siteLink.displayText, siteLink.destinationUrl);
          }
        } else {
          Console.WriteLine("No sitelinks were created.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add site links.", ex);
      }
    }
  }
}
