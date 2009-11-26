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
using com.google.api.adwords.lib.util;
using com.google.api.adwords.v200909;

using System;
using System.Threading;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This demo demonstrates construction of the campaign using bulk mutate
  /// service. The campaign and AdGroups are added synchronously. The campaign
  /// targeting, ads, and keywords are added asynchronously.
  /// </summary>
  class AddBulkMutateJob : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This demo demonstrates construction of the campaign using bulk mutate " +
            "service. The Campaign and AdGroups are added synchronously. The campaign " +
            "targeting, ads, and keywords are added asynchronously.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.</param>
    public override void Run(AdWordsUser user) {
      Campaign campaign = CreateCampaign(user);

      if (campaign == null) {
        return;
      }
      AdGroup adgroup1 = CreateAdGroup(user, campaign.id);
      AdGroup adgroup2 = CreateAdGroup(user, campaign.id);

      if (adgroup1 == null || adgroup2 == null) {
        return;
      }

      // Step 1: Add campaign targets.

      // a. Create campaign targets.
      TargetList[] campaignTargets = CreateCampaignTargets(campaign.id);
      Operation[] targetOperations = new Operation[campaignTargets.Length];

      // b. Make operations for setting them.
      for (int i = 0; i < campaignTargets.Length; i++) {
        CampaignTargetOperation operation = new CampaignTargetOperation();
        operation.operatorSpecified = true;
        operation.@operator = Operator.SET;
        operation.operand = campaignTargets[i];
        targetOperations[i] = operation;
      }

      // c. Add those operations into an operation stream.
      OperationStream targetOpStream = new OperationStream();

      targetOpStream.scopingEntityId = new EntityId();
      targetOpStream.scopingEntityId.type = EntityIdType.CAMPAIGN_ID;
      targetOpStream.scopingEntityId.typeSpecified = true;
      targetOpStream.scopingEntityId.value = campaign.id;
      targetOpStream.scopingEntityId.valueSpecified = true;

      targetOpStream.operations = targetOperations;

      // Step 2: Add ads. We create one stream for each Ad.
      // Stream for Ad 1.
      // a. Create Ad.

      AdGroupAdOperation adGroupAdOperation1 = new AdGroupAdOperation();
      adGroupAdOperation1.operatorSpecified = true;
      adGroupAdOperation1.@operator = Operator.ADD;
      adGroupAdOperation1.operand = CreateTextAd(adgroup1.id); ;

      // b. Add Ad to first operation stream.

      OperationStream adOpStream1 = new OperationStream();

      adOpStream1.scopingEntityId = new EntityId();
      adOpStream1.scopingEntityId.type = EntityIdType.CAMPAIGN_ID;
      adOpStream1.scopingEntityId.typeSpecified = true;
      adOpStream1.scopingEntityId.value = campaign.id;
      adOpStream1.scopingEntityId.valueSpecified = true;

      adOpStream1.operations = new Operation[] {adGroupAdOperation1};

      // Stream for Ad 2.
      // a. Create Ad.

      AdGroupAdOperation adGroupAdOperation2 = new AdGroupAdOperation();
      adGroupAdOperation2.operatorSpecified = true;
      adGroupAdOperation2.@operator = Operator.ADD;
      adGroupAdOperation2.operand = CreateTextAd(adgroup2.id);

      // Purposefully create a policy violation so that this Ad addition fails.
      ((adGroupAdOperation2.operand as AdGroupAd).ad as TextAd).description1 +=
          "This is a very long text.";

      // b. Add Ad to second operation stream.
      OperationStream adOpStream2 = new OperationStream();

      adOpStream2.scopingEntityId = new EntityId();
      adOpStream2.scopingEntityId.type = EntityIdType.CAMPAIGN_ID;
      adOpStream2.scopingEntityId.typeSpecified = true;
      adOpStream2.scopingEntityId.value = campaign.id;
      adOpStream2.scopingEntityId.valueSpecified = true;

      adOpStream2.operations = new Operation[] {adGroupAdOperation2};

      // Step 3: Add keywords.

      // a. Create keywords, and operations.
      AdGroupCriterionOperation adGroupCriterionOperation1 = new AdGroupCriterionOperation();
      adGroupCriterionOperation1.@operator = Operator.ADD;
      adGroupCriterionOperation1.operatorSpecified = true;
      adGroupCriterionOperation1.operand = CreateKeyword(adgroup1.id);

      AdGroupCriterionOperation adGroupCriterionOperation2 = new AdGroupCriterionOperation();
      adGroupCriterionOperation2.@operator = Operator.ADD;
      adGroupCriterionOperation2.operatorSpecified = true;
      adGroupCriterionOperation2.operand = CreateKeyword(adgroup2.id);

      // b. Add those operations to another operation stream.

      OperationStream keywordOpStream = new OperationStream();

      keywordOpStream.scopingEntityId = new EntityId();
      keywordOpStream.scopingEntityId.type = EntityIdType.CAMPAIGN_ID;
      keywordOpStream.scopingEntityId.typeSpecified = true;
      keywordOpStream.scopingEntityId.value = campaign.id;
      keywordOpStream.scopingEntityId.valueSpecified = true;

      keywordOpStream.operations = new Operation[] {adGroupCriterionOperation1,
          adGroupCriterionOperation2};

      // Step 4. Create a job.

      // a. Create a bulk job object.
      long bulkJobId = 0;
      BulkMutateJob bulkJob = null;

      bulkJob = new BulkMutateJob();
      bulkJob.numRequestParts = 3;
      bulkJob.numRequestPartsSpecified = true;

      // b. Create a part of the job.

      BulkMutateRequest bulkRequest1 = new BulkMutateRequest();
      bulkRequest1.partIndex = 0;
      bulkRequest1.partIndexSpecified = true;
      bulkRequest1.operationStreams = new OperationStream[] {targetOpStream};
      bulkJob.request = bulkRequest1;

      // c. Create job operation.
      JobOperation jobOperation1 = new JobOperation();
      jobOperation1.operatorSpecified = true;
      jobOperation1.@operator = Operator.ADD;
      jobOperation1.operand = bulkJob;

      // d. Call mutate().
      BulkMutateJobService bmjService = (BulkMutateJobService) user.GetService(
          AdWordsService.v200909.BulkMutateJobService);
      try {
        bulkJob = bmjService.mutate(jobOperation1);
        bulkJobId = bulkJob.id;
      } catch (Exception ex) {
        Console.WriteLine("Failed to create bulk mutate job. Exception says \"{0}\"", ex.Message);
        return;
      }

      // Similarly, create the next part of the job.

      // Note: since we already created a job earlier, this time we modify it.
      bulkJob = new BulkMutateJob();
      bulkJob.idSpecified = true;
      bulkJob.id = bulkJobId;

      BulkMutateRequest bulkRequest3 = new BulkMutateRequest();
      bulkRequest3.partIndex = 1;
      bulkRequest3.partIndexSpecified = true;
      bulkRequest3.operationStreams = new OperationStream[] {adOpStream1, adOpStream2};
      bulkJob.request = bulkRequest3;

      JobOperation jobOperation3 = new JobOperation();
      jobOperation3.operatorSpecified = true;
      jobOperation3.@operator = Operator.SET;
      jobOperation3.operand = bulkJob;

      try {
        bulkJob = bmjService.mutate(jobOperation3);
        bulkJobId = bulkJob.id;
      } catch (Exception ex) {
        Console.WriteLine("Failed to modify bulk mutate job with id = {0}. Exception says \"{1}\"",
            bulkJobId, ex.Message);
        return;
      }

      // Add the next job part.
      bulkJob = new BulkMutateJob();
      bulkJob.idSpecified = true;
      bulkJob.id = bulkJobId;

      BulkMutateRequest bulkRequest2 = new BulkMutateRequest();
      bulkRequest2.partIndex = 2;
      bulkRequest2.partIndexSpecified = true;
      bulkRequest2.operationStreams = new OperationStream[] {keywordOpStream};
      bulkJob.request = bulkRequest2;

      JobOperation jobOperation2 = new JobOperation();
      jobOperation2.operatorSpecified = true;
      jobOperation2.@operator = Operator.SET;
      jobOperation2.operand = bulkJob;

      try {
        bulkJob = bmjService.mutate(jobOperation2);
        bulkJobId = bulkJob.id;
      } catch (Exception ex) {
        Console.WriteLine("Failed to modify bulk mutate job with id = {0}. Exception says \"{1}\"",
            bulkJobId, ex.Message);
        return;
      }

      // Step 5: At this point, we have added all the parts. The job will start
      //automatically. Wait for the job to complete.
      bool completed = false;

      while (completed == false) {
        Thread.Sleep(2000);

        BulkMutateJobSelector selector = new BulkMutateJobSelector();
        selector.jobIds = new long[] {bulkJobId};

        try {
          BulkMutateJob[] allJobs = bmjService.get(selector);
          if (allJobs != null && allJobs.Length > 0) {
            if (allJobs[0].status == BasicJobStatus.COMPLETED ||
                allJobs[0].status == BasicJobStatus.FAILED) {
              completed = true;
              bulkJob = allJobs[0];
              break;
            }
          }
        } catch (Exception ex) {
          Console.WriteLine("Failed to fetch bulk mutate job with id = {0}. Exception says \"{1}\"",
              bulkJobId, ex.Message);
          return;
        }
      }

      if (bulkJob.status == BasicJobStatus.COMPLETED) {
        // Retrieve the job parts.
        for (int i = 0; i < bulkJob.numRequestParts; i++) {
          BulkMutateJobSelector selector = new BulkMutateJobSelector();
          selector.jobIds = new long[] {bulkJobId};
          selector.resultPartIndex = i;
          selector.resultPartIndexSpecified = true;

          BulkMutateJob[] allJobParts = bmjService.get(selector);
          foreach(BulkMutateJob jobPart in allJobParts) {
            Console.WriteLine("Part {0}/{1} of job '{2}' has successfully completed.",
                jobPart.result.partIndex + 1, bulkJob.numRequestParts, jobPart.id);
          }
        }
        Console.WriteLine("Job completed successfully!");
      } else {
        Console.WriteLine("Job could not be completed.");
      }
    }

    /// <summary>
    /// Create a campaign.
    /// </summary>
    /// <param name="user">The user for which the campaign is created.</param>
    /// <returns>A campaign object.</returns>
    private Campaign CreateCampaign(AdWordsUser user) {
      CampaignService service =
          (CampaignService) user.GetService(AdWordsService.v200909.CampaignService);

      Campaign campaign = new Campaign();

      // Generate a campaign name.
      string campaignName =
          string.Format("Campaign - {0}", DateTime.Now.ToString("yyyy-M-d H:m:s.ffffff"));
      campaign.name = string.Format(campaignName);

      // Required: Set the campaign status.
      campaign.status = CampaignStatus.PAUSED;
      campaign.statusSpecified = true;

      // Required: Specify the currency and budget amount.
      Budget budget = new Budget();
      Money amount = new Money();
      amount.microAmountSpecified = true;
      amount.microAmount = 50000000;

      budget.amount = amount;

      // Required: Specify the bidding strategy.
      campaign.biddingStrategy = new ManualCPC();

      // Optional: Specify the budget period and delivery method.
      budget.periodSpecified = true;
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.deliveryMethodSpecified = true;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      campaign.budget = budget;

      // Optional: Specify an endDate for the campaign.
      campaign.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd");

      // Define an Add operation to add the campaign.
      CampaignOperation campaignOperation = new CampaignOperation();

      campaignOperation.operatorSpecified = true;
      campaignOperation.@operator = Operator.ADD;
      campaignOperation.operand = campaign;

      try {
        CampaignReturnValue results =
            service.mutate(new CampaignOperation[] {campaignOperation});
        return results.value[0];
      } catch (Exception ex) {
        Console.WriteLine("Failed to create campaign. Exception says \"{0}\"", ex.Message);
        return null;
      }
    }

    /// <summary>
    /// Create an AdGroup.
    /// </summary>
    /// <param name="user">The user for which the campaign is created.</param>
    /// <param name="campaignId">Id of the campaign under which the AdGroup must be created.</param>
    /// <returns></returns>
    private AdGroup CreateAdGroup(AdWordsUser user, long campaignId) {
      AdGroupService service =
          (AdGroupService) user.GetService(AdWordsService.v200909.AdGroupService);

      AdGroup adGroup = new AdGroup();

      // Required: Set the campaign id.
      adGroup.campaignId = campaignId;
      adGroup.campaignIdSpecified = true;

      // Optional: set the status of AdGroup.
      adGroup.statusSpecified = true;
      adGroup.status = AdGroupStatus.ENABLED;

      // Optional: set a name for AdGroup.
      string adGroupName =
          string.Format("AdGroup - {0}", DateTime.Now.ToString("yyyy-M-d H:m:s.ffffff"));
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

      // Set the manual bid to the AdGroup.
      adGroup.bids = bids;

      AdGroupOperation adGroupOperation = new AdGroupOperation();
      adGroupOperation.operatorSpecified = true;
      adGroupOperation.@operator = Operator.ADD;
      adGroupOperation.operand = adGroup;

      try {
        AdGroupReturnValue results = service.mutate(new AdGroupOperation[] {adGroupOperation});
        return results.value[0];
      } catch (Exception ex) {
        Console.WriteLine("Failed to create AdGroup. Exception says \"{0}\"", ex.Message);
        return null;
      }
    }

    /// <summary>
    /// Create a language, country and network target for a given campaign.
    /// </summary>
    /// <param name="campaignId">Id of the campaign for which the targets should be created.</param>
    /// <returns>A list of targets.</returns>
    private TargetList[] CreateCampaignTargets(long campaignId) {
      // Create a language target - for English language.
      LanguageTarget languageTarget = new LanguageTarget();
      languageTarget.languageCode = "en";
      LanguageTargetList languageTargetList = new LanguageTargetList();
      languageTargetList.targets = new LanguageTarget[] {languageTarget};
      languageTargetList.campaignId = campaignId;
      languageTargetList.campaignIdSpecified = true;

      // Create a country target - include US, exclude metrocode 743.
      CountryTarget countryTarget = new CountryTarget();
      countryTarget.countryCode = "US";
      countryTarget.excludedSpecified = true;
      countryTarget.excluded = false;
      MetroTarget metroTarget = new MetroTarget();
      metroTarget.excludedSpecified = true;
      metroTarget.excluded = true;
      metroTarget.metroCode = "743";

      GeoTargetList geoTargetList = new GeoTargetList();
      geoTargetList.targets = new GeoTarget[] {countryTarget, metroTarget};
      geoTargetList.campaignId = campaignId;
      geoTargetList.campaignIdSpecified = true;

      // Create a network target - Google Search.
      NetworkTarget networkTarget1 = new NetworkTarget();
      networkTarget1.networkCoverageTypeSpecified = true;
      networkTarget1.networkCoverageType = NetworkCoverageType.GOOGLE_SEARCH;
      NetworkTarget networkTarget2 = new NetworkTarget();
      networkTarget2.networkCoverageTypeSpecified = true;
      networkTarget2.networkCoverageType = NetworkCoverageType.SEARCH_NETWORK;

      NetworkTargetList networkTargetList = new NetworkTargetList();
      networkTargetList.targets = new NetworkTarget[] {networkTarget1, networkTarget2};
      networkTargetList.campaignId = campaignId;
      networkTargetList.campaignIdSpecified = true;

      return new TargetList[] {languageTargetList, geoTargetList, networkTargetList};
    }

    /// <summary>
    /// Create a keyword under the specified AdGroup.
    /// </summary>
    /// <param name="adGroupId">Id of the AdGroup under which the keyword should be created.</param>
    /// <returns>The keyword, as a criterion.</returns>
    private AdGroupCriterion CreateKeyword(long adGroupId) {
      Keyword keyword = new Keyword();
      keyword.text = "mars cruise";
      keyword.matchTypeSpecified = true;
      keyword.matchType = KeywordMatchType.BROAD;

      BiddableAdGroupCriterion criterion = new BiddableAdGroupCriterion();
      criterion.adGroupId = adGroupId;
      criterion.adGroupIdSpecified = true;
      criterion.criterion = keyword;

      return criterion;
    }

    /// <summary>
    /// Create a text Ad under an AdGroup.
    /// </summary>
    /// <param name="adGroupId">Id of the AdGroup under which the text Ad should be created.</param>
    /// <returns>The newly created Ad.</returns>
    private AdGroupAd CreateTextAd(long adGroupId) {
      // Create good text Ad.
      TextAd textAd = new TextAd();
      textAd.headline = "Luxury Cruise to Mars";
      textAd.description1 = "Visit the Red Planet in style.";
      textAd.description2 = "Low-gravity fun for everyone!";
      textAd.displayUrl = "www.example.com";
      textAd.url = "http://www.example.com";

      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroupId;
      adGroupAd.adGroupIdSpecified = true;
      adGroupAd.ad = textAd;

      return adGroupAd;
    }
  }
}
