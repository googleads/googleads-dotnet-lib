// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v200909;

using System;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This code sample creates a new ad group given an existing campaign.
  /// To create a campaign, you can run AddCampaign.cs.
  /// </summary>
  class AddAdGroup : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Create an Adgroup in a given Campaign";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      AdGroupService service =
          (AdGroupService) user.GetService(AdWordsService.v200909.AdGroupService);

      AdGroup adGroup = new AdGroup();

      // Required: Set the campaign id.
      adGroup.campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));
      adGroup.campaignIdSpecified = true;

      // Optional: set the status of adgroup.
      adGroup.statusSpecified = true;
      adGroup.status = AdGroupStatus.ENABLED;

      // Optional: set a name for adgroup.
      string adGroupName = string.Format("AdGroup - {0}",
          DateTime.Now.ToString("yyyy-M-d H:m:s.ffffff"));
      adGroup.name = adGroupName;

      // Optional: Create a Manual CPC Bid.
      ManualCPCAdGroupBids bids = new ManualCPCAdGroupBids();

      // Set the keyword content max cpc.
      bids.keywordContentMaxCpc = new Bid();

      Money kwdContentMaxCpc = new Money();
      kwdContentMaxCpc.microAmountSpecified = true;
      kwdContentMaxCpc.microAmount = 100000;
      bids.keywordContentMaxCpc.amount = kwdContentMaxCpc;

      // Set the keyword max cpc.
      bids.keywordMaxCpc = new Bid();
      Money kwdMaxCpc = new Money();
      kwdMaxCpc.microAmountSpecified = true;
      kwdMaxCpc.microAmount = 150000;
      bids.keywordMaxCpc.amount = kwdMaxCpc;

      // Set the manual bid to the adgroup.
      adGroup.bids = bids;

      AdGroupOperation adGroupOperation = new AdGroupOperation();
      adGroupOperation.operatorSpecified = true;
      adGroupOperation.@operator = Operator.ADD;
      adGroupOperation.operand = adGroup;

      try {
        AdGroupReturnValue results = service.mutate(new AdGroupOperation[] {adGroupOperation});
        if (results != null && results.value != null && results.value.Length > 0) {
          Console.WriteLine("New ad group with name = \"{0}\" and id = \"{1}\" was created.",
              results.value[0].name, results.value[0].id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create ad group. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
