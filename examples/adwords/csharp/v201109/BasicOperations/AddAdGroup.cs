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
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example illustrates how to create an ad group. To create a
  /// campaign, run AddCampaign.cs.
  ///
  /// Tags: AdGroupService.mutate
  /// </summary>
  class AddAdGroup : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new AddAdGroup();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create an ad group. To create a campaign, " +
            "run AddCampaign.cs";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"CAMPAIGN_ID"};
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
      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201109.AdGroupService);

      long campaignId = long.Parse(parameters["CAMPAIGN_ID"]);

      // Create the ad group.
      AdGroup adGroup = new AdGroup();
      adGroup.name = string.Format("Earth to Mars Cruises #{0}", ExampleUtilities.GetTimeStamp());
      adGroup.status = AdGroupStatus.ENABLED;
      adGroup.campaignId = campaignId;

      // Set the ad group bids.
      ManualCPCAdGroupBids bids = new ManualCPCAdGroupBids();

      Bid keywordMaxCpc = new Bid();
      keywordMaxCpc.amount = new Money();
      keywordMaxCpc.amount.microAmount = 10000000;
      bids.keywordMaxCpc = keywordMaxCpc;

      adGroup.bids = bids;

      // Create the operation.
      AdGroupOperation operation = new AdGroupOperation();
      operation.@operator = Operator.ADD;
      operation.operand = adGroup;

      try {
        // Create the ad group.
        AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdGroup newAdGroup = retVal.value[0];
          writer.WriteLine("Ad group with id = '{0}' and name = '{1}' was created.",
              newAdGroup.id, newAdGroup.name);
        } else {
          writer.WriteLine("No ad group was created.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to create ad group. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
