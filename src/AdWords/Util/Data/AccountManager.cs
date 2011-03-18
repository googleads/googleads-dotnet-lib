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
using Google.Api.Ads.AdWords.v13;
using Google.Api.Ads.AdWords.v200909;

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
    public ClientAccount[] DownloadAllAccounts() {
      AccountService accountService = (AccountService) user.GetService(
          AdWordsService.v13.AccountService, "https://sandbox.google.com");
      accountService.clientEmailValue.Value[0] = "";

      string[] clients = accountService.getClientAccounts();
      List<ClientAccount> allClients = new List<ClientAccount>();

      if (clients != null) {
        foreach (string client in clients)
          allClients.Add(DownloadAccount(client));
      }
      return allClients.ToArray();
    }

    /// <summary>
    /// Downloads a sandbox client account under an mcc. All the campaigns,
    /// adgroups, ads and criteria are downloaded by this function.
    /// </summary>
    /// <param name="clientEmail">The clientEmail for the account.</param>
    /// <returns>The ClientAccount object representing this account.</returns>
    public ClientAccount DownloadAccount(string clientEmail) {
      AccountService accountService = (AccountService) user.GetService(
          AdWordsService.v13.AccountService, "https://sandbox.google.com");

      accountService.clientEmailValue.Value[0] = clientEmail;

      ClientAccount account = new ClientAccount();
      account.Email = clientEmail;
      account.AccountInfo = accountService.getAccountInfo();
      account.Campaigns = new List<LocalCampaign>(GetAccountCampaigns(clientEmail));
      return account;
    }

    /// <summary>
    /// Restores all sandbox client accounts under an mcc. This function
    /// internally uses <see cref="UploadAccount"/> to restore all the
    /// campaigns, adgroups, ads and criteria for a client account under a
    /// sandbox MCC account.
    /// </summary>
    /// <param name="accounts">The data to be restored to the accounts.</param>
    internal void UploadAllAccounts(List<ClientAccount> accounts) {
      AccountService accountService = (AccountService) user.GetService(
          AdWordsService.v13.AccountService, "https://sandbox.google.com");
      accountService.clientEmailValue.Value[0] = "";
      accountService.getClientAccounts();

      foreach (ClientAccount account in accounts) {
        UploadAccount(account);
      }
    }

    /// <summary>
    /// Restores a sandbox client accounts under an mcc.
    /// </summary>
    /// <param name="account">The data to be restored to the accounts.</param>
    internal void UploadAccount(ClientAccount account) {
      AccountService accountService = (AccountService) user.GetService(
          AdWordsService.v13.AccountService, "https://sandbox.google.com");
      accountService.clientEmailValue.Value[0] = account.Email;

      accountService.updateAccountInfo(account.AccountInfo);
      SetCampaigns(account.Campaigns, account.Email);
    }

    /// <summary>
    /// Restores the campaigns in an account.
    /// </summary>
    /// <param name="campaigns">The campaigns to be restored.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetCampaigns(List<LocalCampaign> campaigns, string clientEmail) {
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v200909.CampaignService, "https://adwords-sandbox.google.com");
      campaignService.RequestHeader.clientEmail = clientEmail;

      foreach (LocalCampaign localCampaign in campaigns) {
        // Clear readonly fields.
        localCampaign.Campaign.idSpecified = false;
        localCampaign.Campaign.stats = null;
        localCampaign.Campaign.servingStatusSpecified = false;

        DateTime startDay = DateTime.ParseExact(localCampaign.Campaign.startDate, "yyyyMMdd", null);
        DateTime endDay = DateTime.ParseExact(localCampaign.Campaign.endDate, "yyyyMMdd", null);

        if (startDay < DateTime.Today) {
          localCampaign.Campaign.startDate = DateTime.Today.ToString("yyyyMMdd");
        }
        if (endDay > new DateTime(2037, 12, 30)) {
          localCampaign.Campaign.endDate = null;
        }
        if (localCampaign.Campaign.status == v200909.CampaignStatus.ACTIVE ||
            localCampaign.Campaign.status == v200909.CampaignStatus.PAUSED) {
          CampaignOperation operation = new CampaignOperation();
          operation.operatorSpecified = true;
          operation.@operator = Operator.ADD;
          operation.operand = localCampaign.Campaign;
          try {
            CampaignReturnValue retVal = campaignService.mutate(
                new CampaignOperation[] {operation});

            if (retVal != null && retVal.value != null) {
              foreach (Campaign newCampaign in retVal.value) {
                SetAdGroups(newCampaign.id, localCampaign.AdGroups, clientEmail);
                SetCampaignCriteria(newCampaign.id, localCampaign.CampaignCriteria,
                    clientEmail);
                SetCampaignTargets(newCampaign.id, localCampaign.CampaignTargets,
                    clientEmail);
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
    /// Sets the campaign targets for a given campaign.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="targetLists">The list of campaign targets.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetCampaignTargets(long campaignId, List<TargetList> targetLists,
        string clientEmail) {
      CampaignTargetService campaignTargetService = (CampaignTargetService) user.GetService(
          AdWordsService.v200909.CampaignTargetService, "https://adwords-sandbox.google.com");
      campaignTargetService.RequestHeader.clientEmail = clientEmail;

      List<CampaignTargetOperation> operations = new List<CampaignTargetOperation>();

      foreach (TargetList targetList in targetLists) {
        targetList.campaignIdSpecified = true;
        targetList.campaignId = campaignId;

        CampaignTargetOperation targetOperation = new CampaignTargetOperation();
        targetOperation.operatorSpecified = true;
        targetOperation.@operator = Operator.SET;
        targetOperation.operand = targetList;
        operations.Add(targetOperation);
      }
      try {
        campaignTargetService.mutate(operations.ToArray());
      } catch (Exception ex) {
        throw new SandboxSerializationException(
            AdWordsErrorMessages.FailedToAddCampaignTargetsToSandbox, ex);
      }
    }

    /// <summary>
    /// Restores ad groups in a campaign.
    /// </summary>
    /// <param name="campaignId">Campaign id.</param>
    /// <param name="adGroups">The list of adgroups to be added to
    /// this campaign.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetAdGroups(long campaignId, List<LocalAdGroup> adGroups, string clientEmail) {
      AdGroupService adGroupService = (AdGroupService) user.GetService(
          AdWordsService.v200909.AdGroupService, "https://adwords-sandbox.google.com");
      adGroupService.RequestHeader.clientEmail = clientEmail;

      foreach (LocalAdGroup localAdGroup in adGroups) {
        localAdGroup.AdGroup.campaignId = campaignId;
        localAdGroup.AdGroup.campaignIdSpecified = true;

        // Clear the readonly fields.
        localAdGroup.AdGroup.campaignName = null;
        localAdGroup.AdGroup.idSpecified = false;

        AdGroupOperation operation = new AdGroupOperation();
        operation.operatorSpecified = true;
        operation.@operator = Operator.ADD;
        operation.operand = localAdGroup.AdGroup;

        AdGroupReturnValue retVal = null;

        try {
          retVal = adGroupService.mutate(new AdGroupOperation[] {operation});

          if (retVal != null && retVal.value != null) {
            foreach (AdGroup adGroupValue in retVal.value) {
              SetAds(adGroupValue.id, localAdGroup.Ads, clientEmail);
              SetCriteria(adGroupValue.id, localAdGroup.Criteria, clientEmail);
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
    /// <param name="clientEmail">client account email.</param>
    private void SetAds(long adGroupId, List<AdGroupAd> adGroupAds, string clientEmail) {
      AdGroupAdService adService = (AdGroupAdService) user.GetService(
          AdWordsService.v200909.AdGroupAdService, "https://adwords-sandbox.google.com");
      adService.RequestHeader.clientEmail = clientEmail;

      List<AdGroupAdOperation> operations = new List<AdGroupAdOperation>();

      foreach (AdGroupAd adGroupAd in adGroupAds) {
        // Clear readonly fields.
        adGroupAd.stats = null;
        adGroupAd.ad.idSpecified = false;
        adGroupAd.ad.approvalStatusSpecified = false;
        adGroupAd.ad.disapprovalReasons = null;

        adGroupAd.adGroupIdSpecified = true;
        adGroupAd.adGroupId = adGroupId;

        AdGroupAdOperation operation = new AdGroupAdOperation();
        operation.operatorSpecified = true;
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
    /// <param name="clientEmail">client account email.</param>
    private void SetCriteria(long adGroupId, List<AdGroupCriterion> adGroupCriteria,
        string clientEmail) {
      AdGroupCriterionService criterionService = (AdGroupCriterionService) user.GetService(
          AdWordsService.v200909.AdGroupCriterionService, "https://adwords-sandbox.google.com");
      criterionService.RequestHeader.clientEmail = clientEmail;

      List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();
      foreach (AdGroupCriterion adGroupCriterion in adGroupCriteria) {
        // Clear the readonly fields.
        if (adGroupCriterion is BiddableAdGroupCriterion) {
          BiddableAdGroupCriterion biddableAgc = (BiddableAdGroupCriterion) adGroupCriterion;
          biddableAgc.approvalStatusSpecified = false;
          biddableAgc.qualityInfo = null;
          biddableAgc.systemServingStatusSpecified = false;
          biddableAgc.firstPageCpc = null;
          biddableAgc.stats = null;
          if (biddableAgc.bids is ManualCPCAdGroupCriterionBids) {
            ManualCPCAdGroupCriterionBids manualCpcAgcBids =
                (ManualCPCAdGroupCriterionBids) biddableAgc.bids;
            manualCpcAgcBids.bidSourceSpecified = false;
          } else if (biddableAgc.bids is ManualCPMAdGroupCriterionBids) {
            ManualCPMAdGroupCriterionBids manualCpmAgcBids =
                (ManualCPMAdGroupCriterionBids) biddableAgc.bids;
            manualCpmAgcBids.bidSourceSpecified = false;
          }
        }

        adGroupCriterion.criterion.idSpecified = false;

        adGroupCriterion.adGroupIdSpecified = true;
        adGroupCriterion.adGroupId = adGroupId;

        AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
        operation.@operator = Operator.ADD;
        operation.operatorSpecified = true;
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
    /// <param name="clientEmail">client account email.</param>
    private void SetCampaignCriteria(long campaignId, List<CampaignCriterion> campaignCriteria,
        string clientEmail) {
      CampaignCriterionService criterionService = (CampaignCriterionService) user.GetService(
          AdWordsService.v200909.CampaignCriterionService, "https://adwords-sandbox.google.com");
      criterionService.RequestHeader.clientEmail = clientEmail;

      List<CampaignCriterionOperation> operations = new List<CampaignCriterionOperation>();

      foreach (CampaignCriterion campaignCriterion in campaignCriteria) {
        // Clear the readonly fields.
        campaignCriterion.criterion.idSpecified = false;

        campaignCriterion.campaignIdSpecified = true;
        campaignCriterion.campaignId = campaignId;

        CampaignCriterionOperation operation = new CampaignCriterionOperation();
        operation.@operator = Operator.ADD;
        operation.operatorSpecified = true;
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

    /// <summary>
    /// Retrieves the list of all campaigns in an account.
    /// </summary>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of campaigns in the account.</returns>
    private LocalCampaign[] GetAccountCampaigns(string clientEmail) {
      List<LocalCampaign> retVal = new List<LocalCampaign>();

      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v200909.CampaignService, "https://adwords-sandbox.google.com");
      campaignService.RequestHeader.clientEmail = clientEmail;

      // Get all campaigns.
      CampaignPage page = campaignService.get(new CampaignSelector());

      if (page != null && page.entries != null) {
        if (page.entries.Length > 0) {
          foreach (Campaign campaign in page.entries) {
            LocalCampaign localCampaign = new LocalCampaign();
            localCampaign.Campaign = campaign;
            localCampaign.AdGroups.AddRange(GetAdGroups(campaign.id, clientEmail));
            localCampaign.CampaignCriteria.AddRange(GetCampaignCriteria(campaign.id, clientEmail));
            localCampaign.CampaignTargets.AddRange(GetCampaignTargets(campaign.id, clientEmail));
            retVal.Add(localCampaign);
          }
        }
      }
      return retVal.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all adgroups in a campaign.
    /// </summary>
    /// <param name="campaignId">Campaign Id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of ad groups in the campaign.</returns>
    private LocalAdGroup[] GetAdGroups(long campaignId, string clientEmail) {
      List<LocalAdGroup> retVal = new List<LocalAdGroup>();

      AdGroupService adGroupService = (AdGroupService) user.GetService(
          AdWordsService.v200909.AdGroupService, "https://adwords-sandbox.google.com");
      adGroupService.RequestHeader.clientEmail = clientEmail;

      AdGroupSelector adGroupSelector = new AdGroupSelector();
      adGroupSelector.campaignIdSpecified = true;
      adGroupSelector.campaignId = campaignId;

      AdGroupPage page = adGroupService.get(adGroupSelector);
      if (page != null && page.entries != null) {
        foreach (AdGroup adGroup in page.entries) {
          LocalAdGroup localAdGroup = new LocalAdGroup();

          localAdGroup.AdGroup = adGroup;
          localAdGroup.Ads.AddRange(GetAllAds(adGroup.id));
          localAdGroup.Criteria.AddRange(GetAdGroupCriteria(adGroup.id, clientEmail));
          retVal.Add(localAdGroup);
        }
      }
      return retVal.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all ads in an adgroup.
    /// </summary>
    /// <param name="adGroupId">The AdGroup Id.</param>
    /// <returns>List of ads in the adgroup.</returns>
    private AdGroupAd[] GetAllAds(long adGroupId) {
      AdGroupAdService adService = (AdGroupAdService) user.GetService(
          AdWordsService.v200909.AdGroupAdService, "https://adwords-sandbox.google.com");

      AdGroupAdSelector selector = new AdGroupAdSelector();
      selector.adGroupIds = new long[] {adGroupId};

      List<AdGroupAd> retVal = new List<AdGroupAd>();
      AdGroupAdPage page = adService.get(selector);

      if (page != null && page.entries != null) {
        foreach (AdGroupAd adGroupAd in page.entries) {
          // TODO(Anash): ImageAd and MobileImageAd gives mediaId, which
          // cannot be downloaded yet with v200909.
          if (adGroupAd.ad is TextAd) {
            retVal.Add(adGroupAd);
          }
        }
      }

      return retVal.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all criteria in an adgroup.
    /// </summary>
    /// <param name="adGroupId">The AdGroup Id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of criteria in the adgroup.</returns>
    private AdGroupCriterion[] GetAdGroupCriteria(long adGroupId, string clientEmail) {
      AdGroupCriterionService criterionService = (AdGroupCriterionService) user.GetService(
          AdWordsService.v200909.AdGroupCriterionService, "https://adwords-sandbox.google.com");
      AdGroupCriterionSelector selector = new AdGroupCriterionSelector();

      AdGroupCriterionIdFilter filter = new AdGroupCriterionIdFilter();
      filter.adGroupIdSpecified = true;
      filter.adGroupId = adGroupId;
      selector.idFilters = new AdGroupCriterionIdFilter[] {filter};

      criterionService.RequestHeader.clientEmail = clientEmail;

      AdGroupCriterionPage page = criterionService.get(selector);
      return (page != null && page.entries != null)? page.entries : new AdGroupCriterion[] {};
    }

    /// <summary>
    /// Retrieves the list of all criteria in a campaign.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of criteria in the campaign.</returns>
    private CampaignCriterion[] GetCampaignCriteria(long campaignId, string clientEmail) {
      CampaignCriterionService criterionService = (CampaignCriterionService) user.GetService(
          AdWordsService.v200909.CampaignCriterionService, "https://adwords-sandbox.google.com");
      CampaignCriterionSelector selector = new CampaignCriterionSelector();

      CampaignCriterionIdFilter filter = new CampaignCriterionIdFilter();
      filter.campaignIdSpecified = true;
      filter.campaignId = campaignId;
      selector.idFilters = new CampaignCriterionIdFilter[] {filter};

      criterionService.RequestHeader.clientEmail = clientEmail;

      CampaignCriterionPage page = criterionService.get(selector);
      List<CampaignCriterion> retVal = new List<CampaignCriterion>();
      return (page != null && page.entries != null)? page.entries : new CampaignCriterion[] {};
    }

    /// <summary>
    /// Retrieves the list of all targets for a campaign.
    /// </summary>
    /// <param name="campaignId">Id of the campaign for which targets
    /// are retrieved.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of targets for the campaign.</returns>
    private TargetList[] GetCampaignTargets(long campaignId, string clientEmail) {
      CampaignTargetService targetService = (CampaignTargetService) user.GetService(
          AdWordsService.v200909.CampaignTargetService, "https://adwords-sandbox.google.com");
      CampaignTargetSelector selector = new CampaignTargetSelector();
      selector.campaignIds = new long[] {campaignId};

      targetService.RequestHeader.clientEmail = clientEmail;

      CampaignTargetPage page = targetService.get(selector);
      return (page != null && page.entries != null) ? page.entries : new TargetList[] {};
    }
  }
}
