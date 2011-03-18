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
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example illustrates how to create an ad group. To create a
  /// campaign, run AddCampaign.cs.
  ///
  /// Tags: AdGroupService.mutate
  /// </summary>
  class AddAdGroup : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create an ad group. To create a " +
            "campaign, run AddCampaign.cs";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddAdGroup();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201101.AdGroupService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      AdGroup adGroup = new AdGroup();
      adGroup.name = string.Format("Earth to Mars Cruises #{0}", GetTimeStamp());
      adGroup.status = AdGroupStatus.ENABLED;
      adGroup.campaignId = campaignId;

      ManualCPCAdGroupBids bids = new ManualCPCAdGroupBids();

      Bid keywordMaxCpc = new Bid();
      keywordMaxCpc.amount = new Money();
      keywordMaxCpc.amount.microAmount = 10000000;
      bids.keywordMaxCpc = keywordMaxCpc;

      adGroup.bids = bids;

      AdGroupOperation operation = new AdGroupOperation();
      operation.@operator = Operator.ADD;
      operation.operand = adGroup;

      try {
        AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[] {operation});
        if (retVal != null && retVal.value != null) {
          foreach (AdGroup adGroupValue in retVal.value) {
            Console.WriteLine("Ad group with id = '{0}' and name = '{1}' was created.",
                adGroupValue.id, adGroupValue.name);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create ad group(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
