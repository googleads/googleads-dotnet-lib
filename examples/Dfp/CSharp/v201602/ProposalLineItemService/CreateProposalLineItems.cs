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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates a new proposal line items. To determine which
  /// proposal line items exist, run GetAllProposalLineItems.cs. To determine
  /// which proposals exist, run GetAllProposals.cs.
  /// </summary>
  class CreateProposalLineItems : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new proposal line items. To determine which " +
            "proposal line items exist, run GetAllProposalLineItems.cs. To determine " +
            "which proposals exist, run GetAllProposals.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateProposalLineItems();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code examples.
    /// </summary>
    /// <param name="user">The DFP user object running the code examples.</param>
    public override void Run(DfpUser user) {
      // [START get_proposal_line_item_service] MOE:strip_line
      // Get the ProposalLineItemService.
      ProposalLineItemService proposalLineItemService =
          (ProposalLineItemService) user.GetService(DfpService.v201602.ProposalLineItemService);
      // [END get_proposal_line_item_service] MOE:strip_line

      // Get the NetworkService.
      NetworkService networkService =
          (NetworkService) user.GetService(DfpService.v201602.NetworkService);

      long proposalId = long.Parse(_T("INSERT_PROPOSAL_ID_HERE"));
      long rateCardId = long.Parse(_T("INSERT_RATE_CARD_ID_HERE"));
      long productId = long.Parse(_T("INSERT_PRODUCT_ID_HERE"));

      // [START add_targeting] MOE:strip_line
      // Get the root ad unit ID used to target the whole site.
      String rootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

      // Create inventory targeting.
      InventoryTargeting inventoryTargeting = new InventoryTargeting();

      // Create ad unit targeting for the root ad unit (i.e. the whole network).
      AdUnitTargeting adUnitTargeting = new AdUnitTargeting();
      adUnitTargeting.adUnitId = rootAdUnitId;
      adUnitTargeting.includeDescendants = true;

      inventoryTargeting.targetedAdUnits = new AdUnitTargeting[] {adUnitTargeting};

      // Create targeting.
      Targeting targeting = new Targeting();
      targeting.inventoryTargeting = inventoryTargeting;
      // [END add_targeting] MOE:strip_line

      // [START create_proposal_line_item_local] MOE:strip_line
      // Create a proposal line item.
      ProposalLineItem proposalLineItem = new ProposalLineItem();
      proposalLineItem.name = "Proposal line item #" + new Random().Next(int.MaxValue);
      // [END create_proposal_line_item_local] MOE:strip_line

      // [START set_required_creation_fields] MOE:strip_line
      proposalLineItem.proposalId = proposalId;
      proposalLineItem.rateCardId = rateCardId;
      proposalLineItem.productId = productId;
      proposalLineItem.targeting = targeting;
      // [END set_required_creation_fields] MOE:strip_line

      // [START set_dates] MOE:strip_line
      // Set the length of the proposal line item to run.
      proposalLineItem.startDateTime =
          DateTimeUtilities.FromDateTime(System.DateTime.Now.AddDays(7), "America/New_York");
      proposalLineItem.endDateTime =
          DateTimeUtilities.FromDateTime(System.DateTime.Now.AddDays(30), "America/New_York");
      // [END set_dates] MOE:strip_line

      // [START set_delivery_info] MOE:strip_line
      // Set delivery specifications for the proposal line item.
      proposalLineItem.deliveryRateType = DeliveryRateType.EVENLY;
      proposalLineItem.creativeRotationType = CreativeRotationType.OPTIMIZED;
      // [END set_delivery_info] MOE:strip_line

      // [START set_billing] MOE:strip_line
      // Set billing specifications for the proposal line item.
      proposalLineItem.billingCap = BillingCap.CAPPED_CUMULATIVE;
      proposalLineItem.billingSource = BillingSource.THIRD_PARTY_VOLUME;
      // [END set_billing] MOE:strip_line

      // [START set_goal] MOE:strip_line
      // Set pricing for the proposal line item for 1000 impressions at a CPM of $2
      // for a total value of $2.
      proposalLineItem.goal = new Goal() {unitType = UnitType.IMPRESSIONS, units = 1000L};
      // [END set_goal] MOE:strip_line
      // [START set_costs] MOE:strip_line
      proposalLineItem.cost = new Money() {currencyCode = "USD", microAmount = 2000000L};
      proposalLineItem.costPerUnit = new Money() {currencyCode = "USD", microAmount = 2000000L};
      proposalLineItem.rateType = RateType.CPM;
      // [END set_costs] MOE:strip_line

      try {
        // [START create_proposal_line_item_server] MOE:strip_line
        // Create the proposal line item on the server.
        ProposalLineItem[] proposalLineItems = proposalLineItemService.createProposalLineItems(
            new ProposalLineItem[] {proposalLineItem});
        // [END create_proposal_line_item_server] MOE:strip_line

        foreach (ProposalLineItem createdProposalLineItem in proposalLineItems) {
          Console.WriteLine("A proposal line item with ID \"{0}\" and name \"{1}\" was created.",
              createdProposalLineItem.id, createdProposalLineItem.name);
        }

      } catch (Exception e) {
        Console.WriteLine("Failed to create proposal line items. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
