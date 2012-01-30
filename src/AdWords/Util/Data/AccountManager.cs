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

using Google.Api.Ads.AdWords;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Util.Data {
  /// <summary>
  /// This class manages the backup and restore of sandbox accounts.
  /// You can use DownloadAllAccounts to download all the details
  /// of all the client accounts under your sandbox MCC account -
  /// Campaigns, AdGroups, Ads and Criteria. Similarly you can use
  /// UploadAllAccounts to restore a previously backed-up Sandbox
  /// account. See DataUtilities.RestoreSandboxContents and
  /// DataUtilities.DownloadSandboxContents to see how this class
  /// can be used to save a sandbox account contents into an XML file
  /// and later restore it.
  /// </summary>
  public class AccountManager {
    /// <summary>
    /// AdWords user associated with this class.
    /// </summary>
    private AdWordsUser user = null;

    /// <summary>
    /// The AdWords API sandbox server url.
    /// </summary>
    private const string SANDBOX_SERVER = "https://adwords-sandbox.google.com";

    /// <summary>
    /// Default constructor for AccountManager.
    /// </summary>
    public AccountManager() {
      user = new AdWordsUser();
    }

    /// <summary>
    /// Overloaded constructor for AccountManager.
    /// </summary>
    /// <param name="user">The AdWordsUser to be associated with this manager.
    /// </param>
    public AccountManager(AdWordsUser user) {
      this.user = user;
    }

    /// <summary>
    /// Downloads all sandbox client accounts under an mcc. This function
    /// internally uses <see cref="DownloadAccount"/> to download each client
    /// returned by getClientAccounts method of AccountService.
    /// </summary>
    /// <returns>An array of ClientAccount objects.</returns>
    public LocalClientAccount[] DownloadAllAccounts(string[] campaignFields, string[] adGroupFields,
        string[] adFields, string[] criterionFields, string[] campaignCriterionFields) {
      // Get the ServicedAccountService.
      ServicedAccountService servicedAccountService =
          (ServicedAccountService) user.GetService(AdWordsService.v201109.
              ServicedAccountService);

      ServicedAccountSelector selector = new ServicedAccountSelector();
      selector.enablePaging = false;

      ServicedAccountGraph graph = servicedAccountService.get(selector);

      List<LocalClientAccount> allClients = new List<LocalClientAccount>();

      if (graph != null && graph.accounts != null) {
        for (int i = 0; i < graph.accounts.Length; i++) {
          allClients.Add(DownloadAccount(graph.accounts[i].customerId, campaignFields,
              adGroupFields, adFields, criterionFields, campaignCriterionFields));
        }
      }
      return allClients.ToArray();
    }

    /// <summary>
    /// Downloads a sandbox client account under an mcc. All the campaigns,
    /// adgroups, ads and criteria are downloaded by this function.
    /// </summary>
    /// <param name="customerId">The customer id for the account</param>
    /// <param name="campaignFields">The list of names of campaign fields that
    /// should be saved.</param>
    /// <param name="adGroupFields">The list of names of ad group fields that
    /// should be saved.</param>
    /// <param name="adFields">The list of names of ad fields that should be
    /// saved.</param>
    /// <param name="criterionFields">The list of names of criterion fields that
    /// should be saved.</param>
    /// <param name="campaignCriterionFields">The list of names of campaign
    /// criterion fields that should be saved.</param>
    /// <returns>The LocalClientAccount object representing this account.
    /// </returns>
    public LocalClientAccount DownloadAccount(long customerId, string[] campaignFields,
        string[] adGroupFields, string[] adFields, string[] criterionFields,
        string[] campaignCriterionFields) {
      LocalClientAccount account = new LocalClientAccount();
      account.CustomerId = customerId;
      account.Campaigns = new List<LocalCampaign>(GetAccountCampaigns(customerId, campaignFields,
          adGroupFields, adFields, criterionFields, campaignCriterionFields));
      return account;
    }

    /// <summary>
    /// Retrieves the list of all campaigns in an account.
    /// </summary>
    /// <param name="customerId">The customer id for the account</param>
    /// <param name="campaignFields">The list of names of campaign fields that
    /// should be saved.</param>
    /// <param name="adGroupFields">The list of names of ad group fields that
    /// should be saved.</param>
    /// <param name="adFields">The list of names of ad fields that should be
    /// saved.</param>
    /// <param name="criterionFields">The list of names of criterion fields that
    /// should be saved.</param>
    /// <param name="campaignCriterionFields">The list of names of campaign
    /// criterion fields that should be saved.</param>
    /// <returns>List of campaigns in the account.</returns>
    private LocalCampaign[] GetAccountCampaigns(long customerId, string[] campaignFields,
        string[] adGroupFields, string[] adFields, string[] criterionFields,
        string[] campaignCriterionFields) {
      List<LocalCampaign> retVal = new List<LocalCampaign>();

      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v201109.CampaignService, SANDBOX_SERVER);
      campaignService.RequestHeader.clientCustomerId = customerId.ToString();

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = campaignFields;

      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      CampaignPage page = new CampaignPage();

      do {
        selector.paging.startIndex = offset;
        selector.paging.numberResults = pageSize;

        page = campaignService.get(selector);
        if (page != null && page.entries != null) {
          int i = offset;
          foreach (Campaign campaign in page.entries) {
            LocalCampaign localCampaign = new LocalCampaign();
            localCampaign.Campaign = campaign;
            localCampaign.AdGroups.AddRange(GetAdGroups(campaign.id, customerId, adGroupFields,
                adFields, criterionFields));
            localCampaign.CampaignCriteria.AddRange(GetCampaignCriteria(campaign.id, customerId,
                campaignCriterionFields));
            retVal.Add(localCampaign);
          }
        }
        offset += pageSize;
      } while (offset < page.totalNumEntries);

      return retVal.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all adgroups in a campaign.
    /// </summary>
    /// <param name="campaignId">Campaign Id.</param>
    /// <param name="customerId">The customer id for the account</param>
    /// <param name="adGroupFields">The list of names of ad group fields that
    /// should be saved.</param>
    /// <param name="adFields">The list of names of ad fields that should be
    /// saved.</param>
    /// <param name="criterionFields">The list of names of criterion fields that
    /// should be saved.</param>
    /// <returns>List of ad groups in the campaign.</returns>
    private LocalAdGroup[] GetAdGroups(long campaignId, long customerId, string[] adGroupFields,
        string[] adFields, string[] criterionFields) {
      List<LocalAdGroup> retVal = new List<LocalAdGroup>();

      AdGroupService adGroupService = (AdGroupService) user.GetService(
          AdWordsService.v201109.AdGroupService, SANDBOX_SERVER);
      adGroupService.RequestHeader.clientCustomerId = customerId.ToString();

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = adGroupFields;

      // Set a filter condition.
      Predicate predicate = new Predicate();
      predicate.field = "CampaignId";
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.values = new string[] {campaignId.ToString()};
      selector.predicates = new Predicate[] {predicate};

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      AdGroupPage page = new AdGroupPage();

      do {
        selector.paging.startIndex = offset;
        selector.paging.numberResults = pageSize;

        page = adGroupService.get(selector);
        if (page != null && page.entries != null) {
          int i = offset;
          foreach (AdGroup adGroup in page.entries) {
            LocalAdGroup localAdGroup = new LocalAdGroup();

            localAdGroup.AdGroup = adGroup;
            localAdGroup.Ads.AddRange(GetAllAds(adGroup.id, customerId, adFields));
            localAdGroup.Criteria.AddRange(GetAdGroupCriteria(adGroup.id, customerId,
                criterionFields));
            retVal.Add(localAdGroup);
          }
        }
        offset += pageSize;
      } while (offset < page.totalNumEntries);
      return retVal.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all ads in an adgroup.
    /// </summary>
    /// <param name="adGroupId">The AdGroup Id.</param>
    /// <param name="customerId">The customer id for the account</param>
    /// <param name="adFields">The list of names of ad fields that should be
    /// saved.</param>
    /// <returns>List of ads in the adgroup.</returns>
    private AdGroupAd[] GetAllAds(long adGroupId, long customerId, string[] adFields) {
      AdGroupAdService adService = (AdGroupAdService) user.GetService(
          AdWordsService.v201109.AdGroupAdService, SANDBOX_SERVER);

      adService.RequestHeader.clientCustomerId = customerId.ToString();

      // Create a selector and set the filters.
      Selector selector = new Selector();

      // Set the fields to select.
      selector.fields = adFields;

      // Restrict the fetch to only the selected AdGroupId.
      Predicate adGroupPredicate = new Predicate();
      adGroupPredicate.field = "AdGroupId";
      adGroupPredicate.@operator = PredicateOperator.EQUALS;
      adGroupPredicate.values = new string[] {adGroupId.ToString()};

      // By default disabled ads aren't returned by the selector. To return
      // them include the DISABLED status in the statuses field.
      Predicate statusPredicate = new Predicate();
      statusPredicate.field = "Status";
      statusPredicate.@operator = PredicateOperator.IN;
      statusPredicate.values = new string[] {AdGroupAdStatus.ENABLED.ToString(),
          AdGroupAdStatus.PAUSED.ToString(), AdGroupAdStatus.DISABLED.ToString()};
      selector.predicates = new Predicate[] {adGroupPredicate, statusPredicate};

      // Select selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      AdGroupAdPage page = new AdGroupAdPage();
            List<AdGroupAd> retVal = new List<AdGroupAd>();

      do {
        selector.paging.startIndex = offset;
        selector.paging.numberResults = pageSize;

        page = adService.get(selector);

        if (page != null && page.entries != null) {
          int i = offset;

          foreach (AdGroupAd adGroupAd in page.entries) {
            retVal.Add(adGroupAd);
          }
        }
        offset += pageSize;
      } while (offset < page.totalNumEntries);

      return retVal.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all criteria in an adgroup.
    /// </summary>
    /// <param name="adGroupId">The AdGroup Id.</param>
    /// <param name="customerId">The customer id for the account</param>
    /// <param name="criterionFields">The list of names of criterion fields that
    /// should be saved.</param>
    /// <returns>List of criteria in the adgroup.</returns>
    private AdGroupCriterion[] GetAdGroupCriteria(long adGroupId, long customerId,
        string[] criterionFields) {
      AdGroupCriterionService criterionService = (AdGroupCriterionService) user.GetService(
          AdWordsService.v201109.AdGroupCriterionService, "https://adwords-sandbox.google.com");

      criterionService.RequestHeader.clientCustomerId = customerId.ToString();


      // Create a selector.
      Selector selector = new Selector();
      selector.fields = criterionFields;

      // Restrict the fetch to only the selected AdGroupId.
      Predicate adGroupPredicate = new Predicate();
      adGroupPredicate.field = "AdGroupId";
      adGroupPredicate.@operator = PredicateOperator.EQUALS;
      adGroupPredicate.values = new string[] {adGroupId.ToString()};

      selector.predicates = new Predicate[] {adGroupPredicate};

      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      AdGroupCriterionPage page = new AdGroupCriterionPage();
      List<AdGroupCriterion> retVal = new List<AdGroupCriterion>();

      do {
        selector.paging.startIndex = offset;
        selector.paging.numberResults = pageSize;

        page = criterionService.get(selector);

        if (page != null && page.entries != null) {
          retVal.AddRange(page.entries);
        }
        offset += pageSize;
      } while (offset < page.totalNumEntries);
      return retVal.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all criteria in a campaign.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="customerId">The customer id for the account</param>
    /// <param name="campaignCriterionFields">The list of names of campaign
    /// criterion fields that should be saved.</param>
    /// <returns>List of criteria in the campaign.</returns>
    private CampaignCriterion[] GetCampaignCriteria(long campaignId, long customerId,
        string[] campaignCriterionFields) {
      CampaignCriterionService criterionService = (CampaignCriterionService) user.GetService(
          AdWordsService.v201109.CampaignCriterionService, "https://adwords-sandbox.google.com");

      // Create selector.
      Selector selector = new Selector();
      selector.fields = campaignCriterionFields;

      Predicate predicate = new Predicate();
      predicate.field = "CampaignId";
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.values = new string[] {campaignId.ToString()};

      selector.predicates = new Predicate[] {predicate};

      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      CampaignCriterionPage page = new CampaignCriterionPage();
      List<CampaignCriterion> retVal = new List<CampaignCriterion>();

      do {
        selector.paging.startIndex = offset;
        selector.paging.numberResults = pageSize;

        // Get all campaign targets.
        page = criterionService.get(selector);
        if (page != null && page.entries != null) {
          retVal.AddRange(page.entries);
        }
        offset += pageSize;
      } while (offset < page.totalNumEntries);
      return retVal.ToArray();
    }

    /// <summary>
    /// Restores all sandbox client accounts under an mcc. This function
    /// internally uses <see cref="UploadAccount"/> to restore all the
    /// campaigns, adgroups, ads and criteria for a client account under a
    /// sandbox MCC account.
    /// </summary>
    /// <param name="accounts">The data to be restored to the accounts.</param>
    internal void UploadAllAccounts(List<LocalClientAccount> accounts) {
      foreach (LocalClientAccount account in accounts) {
        UploadAccount(account);
      }
    }

    /// <summary>
    /// Restores a sandbox client accounts under an mcc.
    /// </summary>
    /// <param name="account">The data to be restored to the accounts.</param>
    internal void UploadAccount(LocalClientAccount account) {
      SetCampaigns(account.Campaigns, account.CustomerId);
    }

    /// <summary>
    /// Restores the campaigns in an account.
    /// </summary>
    /// <param name="campaigns">The campaigns to be restored.</param>
    /// <param name="customerId">The customer id for the account</param>
    private void SetCampaigns(List<LocalCampaign> campaigns, long customerId) {
      if (campaigns.Count == 0) {
        return;
      }
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v201109.CampaignService, "https://adwords-sandbox.google.com");
      campaignService.RequestHeader.clientCustomerId = customerId.ToString();

      foreach (LocalCampaign localCampaign in campaigns) {
        // Clear readonly fields.
        localCampaign.Campaign.idSpecified = false;

        if (localCampaign.Campaign.startDate != null) {
          DateTime startDay = DateTime.ParseExact(localCampaign.Campaign.startDate, "yyyyMMdd",
              null);
          if (startDay < DateTime.Today) {
            localCampaign.Campaign.startDate = DateTime.Today.ToString("yyyyMMdd");
          }
        }

        if (localCampaign.Campaign.endDate != null) {
          DateTime endDay = DateTime.ParseExact(localCampaign.Campaign.endDate, "yyyyMMdd", null);
          if (endDay > new DateTime(2037, 12, 30)) {
            localCampaign.Campaign.endDate = null;
          }
        }
        if (localCampaign.Campaign.status == CampaignStatus.ACTIVE ||
            localCampaign.Campaign.status == CampaignStatus.PAUSED) {
          CampaignOperation operation = new CampaignOperation();
          operation.@operator = Operator.ADD;
          operation.operand = localCampaign.Campaign;
          try {
            CampaignReturnValue retVal = campaignService.mutate(
                new CampaignOperation[] {operation});

            if (retVal != null && retVal.value != null) {
              foreach (Campaign newCampaign in retVal.value) {
                SetAdGroups(newCampaign.id, localCampaign.AdGroups, customerId);
                SetCampaignCriteria(newCampaign.id, localCampaign.CampaignCriteria,
                    customerId);
              }
            }
          } catch (Exception ex) {
            throw new SandboxSerializationException(
                AdWordsErrorMessages.FailedToAddCampaignsToSandbox, ex);
          }
        }
      }
    }

    /// <summary>
    /// Restores ad groups in a campaign.
    /// </summary>
    /// <param name="campaignId">Campaign id.</param>
    /// <param name="adGroups">The list of adgroups to be added to
    /// this campaign.</param>
    /// <param name="customerId">The customer id for the account</param>
    private void SetAdGroups(long campaignId, List<LocalAdGroup> adGroups, long customerId) {
      if (adGroups.Count == 0) {
        return;
      }

      AdGroupService adGroupService = (AdGroupService) user.GetService(
          AdWordsService.v201109.AdGroupService, "https://adwords-sandbox.google.com");
      adGroupService.RequestHeader.clientCustomerId = customerId.ToString();

      foreach (LocalAdGroup localAdGroup in adGroups) {
        localAdGroup.AdGroup.campaignId = campaignId;
        localAdGroup.AdGroup.idSpecified = false;

        AdGroupOperation operation = new AdGroupOperation();
        operation.@operator = Operator.ADD;
        operation.operand = localAdGroup.AdGroup;

        AdGroupReturnValue retVal = null;

        try {
          retVal = adGroupService.mutate(new AdGroupOperation[] {operation});

          if (retVal != null && retVal.value != null) {
            foreach (AdGroup adGroupValue in retVal.value) {
              SetAds(adGroupValue.id, localAdGroup.Ads, customerId);
              SetCriteria(adGroupValue.id, localAdGroup.Criteria, customerId);
            }
          }
        } catch (Exception ex) {
          throw new SandboxSerializationException(
              AdWordsErrorMessages.FailedToAddAdGroupsToSandbox, ex);
        }
      }
    }

    /// <summary>
    /// Restores ads in an ad group.
    /// </summary>
    /// <param name="adGroupId">AdGroup Id.</param>
    /// <param name="adGroupAds">AdGroup Ads to be added to the adgroup.</param>
    /// <param name="customerId">The customer id for the account</param>
    private void SetAds(long adGroupId, List<AdGroupAd> adGroupAds, long customerId) {
      if (adGroupAds.Count == 0) {
        return;
      }
      AdGroupAdService adService = (AdGroupAdService) user.GetService(
          AdWordsService.v201109.AdGroupAdService, "https://adwords-sandbox.google.com");
      adService.RequestHeader.clientCustomerId = customerId.ToString();

      List<AdGroupAdOperation> operations = new List<AdGroupAdOperation>();

      foreach (AdGroupAd adGroupAd in adGroupAds) {
        adGroupAd.ad.idSpecified = false;
        adGroupAd.adGroupId = adGroupId;

        AdGroupAdOperation operation = new AdGroupAdOperation();
        operation.@operator = Operator.ADD;
        operation.operand = adGroupAd;
        operations.Add(operation);
      }

      try {
        adService.mutate(operations.ToArray());
      } catch (Exception ex) {
        throw new SandboxSerializationException(AdWordsErrorMessages.FailedToAddAdsToSandbox, ex);
      }
    }

    /// <summary>
    /// Restores criteria in an ad group.
    /// </summary>
    /// <param name="adGroupId">AdGroup Id.</param>
    /// <param name="adGroupCriteria">The list of criteria to be added to the ad
    /// group.</param>
    /// <param name="customerId">The customer id for the account</param>
    private void SetCriteria(long adGroupId, List<AdGroupCriterion> adGroupCriteria,
        long customerId) {
      if (adGroupCriteria.Count == 0) {
        return;
      }
      AdGroupCriterionService criterionService = (AdGroupCriterionService) user.GetService(
          AdWordsService.v201109.AdGroupCriterionService, "https://adwords-sandbox.google.com");
      criterionService.RequestHeader.clientCustomerId = customerId.ToString();

      List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();
      foreach (AdGroupCriterion adGroupCriterion in adGroupCriteria) {
        adGroupCriterion.criterion.idSpecified = false;
        adGroupCriterion.adGroupId = adGroupId;

        AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
        operation.@operator = Operator.ADD;
        operation.operand = adGroupCriterion;
        operations.Add(operation);
      }
      try {
        criterionService.mutate(operations.ToArray());
      } catch (Exception ex) {
        throw new SandboxSerializationException(
            AdWordsErrorMessages.FailedToAddCriteriaToSandbox, ex);
      }
    }

    /// <summary>
    /// Restores criteria in a campaign.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="campaignCriteria">The list of criteria to be added to
    /// the campaign.</param>
    /// <param name="customerId">The customer id for the account</param>
    private void SetCampaignCriteria(long campaignId, List<CampaignCriterion> campaignCriteria,
        long customerId) {
      if (campaignCriteria.Count == 0) {
        return;
      }
      CampaignCriterionService criterionService = (CampaignCriterionService) user.GetService(
          AdWordsService.v201109.CampaignCriterionService, "https://adwords-sandbox.google.com");
      criterionService.RequestHeader.clientCustomerId = customerId.ToString();

      List<CampaignCriterionOperation> operations = new List<CampaignCriterionOperation>();

      foreach (CampaignCriterion campaignCriterion in campaignCriteria) {
        // Clear the readonly fields.
        campaignCriterion.criterion.idSpecified = false;
        campaignCriterion.campaignIdSpecified = true;
        campaignCriterion.campaignId = campaignId;

        CampaignCriterionOperation operation = new CampaignCriterionOperation();
        operation.@operator = Operator.ADD;
        operation.operand = campaignCriterion;
        operations.Add(operation);
      }
      try {
        criterionService.mutate(operations.ToArray());
      } catch (Exception ex) {
        throw new SandboxSerializationException(
            AdWordsErrorMessages.FailedToAddCampaignCriteriaToSandbox, ex);
      }
    }
  }
}
