// Copyright 2010, Google Inc. All Rights Reserved.
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
using com.google.api.adwords.v13;
using com.google.api.adwords.v200909;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace com.google.api.adwords.lib.util {
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
    AdWordsUser user = null;

    /// <summary>
    /// Default constructor for AccountManager.
    /// </summary>
    public AccountManager() {
      user = new AdWordsUser();
    }

    /// <summary>
    /// Overloaded constructor for AccountManager.
    /// </summary>
    /// <param name="user">The AdWordsUser to be associated with this manager.</param>
    public AccountManager(AdWordsUser user) {
      this.user = user;
    }

    /// <summary>
    /// Downloads all sandbox client accounts under an mcc. This function
    /// internally uses <see cref="DownloadAccount"/> to download each client returned
    /// by getClientAccounts method of AccountService.
    /// </summary>
    /// <returns>An array of ClientAccount objects.</returns>
    public ClientAccount[] DownloadAllAccounts() {
      user.UseSandbox();
      AccountService accountService = (AccountService) user.GetService(
          AdWordsService.v13.AccountService);
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
      AccountService accountService =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);

      accountService.clientEmailValue.Value[0] = clientEmail;

      ClientAccount account = new ClientAccount();
      account.Email = clientEmail;
      account.AccountInfo = accountService.getAccountInfo();
      account.Campaigns = new List<LocalCampaign>(GetAccountCampaigns(clientEmail));
      return account;
    }

    /// <summary>
    /// Restores all sandbox client accounts under an mcc. This function
    /// internally uses <see cref="UploadAccount"/> to restore all the campaigns, adgroups,
    /// ads and criteria for a client account under a sandbox MCC account.
    /// </summary>
    /// <param name="accounts">The data to be restored to the accounts.</param>
    internal void UploadAllAccounts(ClientAccount[] accounts) {
      user.UseSandbox();
      AccountService accountService =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);
      accountService.clientEmailValue.Value[0] = "";

      string[] clients = accountService.getClientAccounts();

      foreach (ClientAccount account in accounts) {
        UploadAccount(account);
      }
    }

    /// <summary>
    /// Restores a sandbox client accounts under an mcc.
    /// </summary>
    /// <param name="account">The data to be restored to the accounts.</param>
    internal void UploadAccount(ClientAccount account) {
      AccountService accountService =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);
      accountService.clientEmailValue.Value[0] = account.Email;

      accountService.updateAccountInfo(account.AccountInfo);
      SetAccountCampaigns(account.Campaigns.ToArray(), account.Email);
    }

    /// <summary>
    /// Restores the campaigns in an account.
    /// </summary>
    /// <param name="campaigns">The campaigns to be restored.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetAccountCampaigns(LocalCampaign[] campaigns, string clientEmail) {
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v200909.CampaignService);
      campaignService.RequestHeader.clientEmail = clientEmail;

      foreach (LocalCampaign localCampaign in campaigns) {
        // clear readonly fields.
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
        if (localCampaign.Campaign.status == com.google.api.adwords.v200909.CampaignStatus.ACTIVE ||
            localCampaign.Campaign.status == com.google.api.adwords.v200909.CampaignStatus.PAUSED) {
          CampaignOperation operation = new CampaignOperation();
          operation.operatorSpecified = true;
          operation.@operator = Operator.ADD;
          operation.operand = localCampaign.Campaign;
          try {
            CampaignReturnValue result = campaignService.mutate(
                new CampaignOperation[] { operation });

            if (result != null && result.value != null) {
              foreach (Campaign newCampaign in result.value) {
                SetAdGroups(newCampaign.id, localCampaign.AdGroups.ToArray(), clientEmail);
                SetCampaignCriteria(newCampaign.id, localCampaign.CampaignCriteria.ToArray(),
                    clientEmail);
                SetCampaignTargets(newCampaign.id, localCampaign.CampaignTargets.ToArray(),
                    clientEmail);
              }
            }
          } catch (Exception ex) {
            throw new System.ApplicationException("Could not add campaign(s). See inner exception" +
                " for details.", ex);
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
    private void SetCampaignTargets(long campaignId, TargetList[] targetLists, string clientEmail) {
      CampaignTargetService campaignTargetService =
          (CampaignTargetService) user.GetService(AdWordsService.v200909.CampaignTargetService);
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
        throw new System.ApplicationException("Could not add campaign target(s). See inner" +
            " exception for details.", ex);
      }
    }

    /// <summary>
    /// Restores ad groups in a campaign.
    /// </summary>
    /// <param name="campaignId">Campaign id.</param>
    /// <param name="adGroups">The list of adgroups to be added to
    /// this campaign.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetAdGroups(long campaignId, LocalAdGroup[] adGroups, string clientEmail) {
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v200909.AdGroupService);
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

        AdGroupReturnValue retval = null;

        try {
          retval = adGroupService.mutate(new AdGroupOperation[] { operation });

          if (retval != null && retval.value != null) {
            foreach (AdGroup adGroupValue in retval.value) {
              SetAds(adGroupValue.id, localAdGroup.Ads.ToArray(), clientEmail);
              SetCriteria(adGroupValue.id, localAdGroup.Criteria.ToArray(), clientEmail);
            }
          }
        } catch (Exception ex) {
          throw new System.ApplicationException("Could not add adgroup(s). See inner exception" +
              " for details.", ex);
        }
      }
    }

    /// <summary>
    /// Restores ads in an ad group.
    /// </summary>
    /// <param name="adGroupId">AdGroup Id.</param>
    /// <param name="adGroupAds">AdGroup Ads to be added to the adgroup.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetAds(long adGroupId, AdGroupAd[] adGroupAds, string clientEmail) {
      AdGroupAdService adService =
          (AdGroupAdService) user.GetService(AdWordsService.v200909.AdGroupAdService);
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
        throw new System.ApplicationException("Could not add adgroup(s). See inner exception" +
            " for details.", ex);
      }
    }

    /// <summary>
    /// Restores criteria in an ad group.
    /// </summary>
    /// <param name="adGroupId">AdGroup Id.</param>
    /// <param name="criteria">The list of criteria to be added to
    /// the ad group.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetCriteria(long adGroupId, AdGroupCriterion[] adGroupCriteria,
        string clientEmail) {
      AdGroupCriterionService criterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v200909.AdGroupCriterionService);
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
        throw new System.ApplicationException("Could not add adgroup(s). See inner exception" +
            " for details.", ex);
      }
    }

    /// <summary>
    /// Restores criteria in a campaign.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="criteria">The list of criteria to be added to
    /// the campaign.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetCampaignCriteria(long campaignId, CampaignCriterion[] campaignCriteria,
        string clientEmail) {
      CampaignCriterionService criterionService = (CampaignCriterionService) user.GetService(
          AdWordsService.v200909.CampaignCriterionService);
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
        throw new System.ApplicationException("Could not add adgroup(s). See inner exception" +
            " for details.", ex);
      }
    }

    /// <summary>
    /// Retrieves the list of all campaigns in an account.
    /// </summary>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of campaigns in the account.</returns>
    private LocalCampaign[] GetAccountCampaigns(string clientEmail) {
      List<LocalCampaign> retval = new List<LocalCampaign>();

      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v200909.CampaignService);
      campaignService.RequestHeader.clientEmail = clientEmail;

      // Get all campaigns.
      CampaignPage page = campaignService.get(new CampaignSelector());

      // Display campaigns.
      if (page != null && page.entries != null) {
        if (page.entries.Length > 0) {
          foreach (Campaign campaign in page.entries) {
            LocalCampaign localCampaign = new LocalCampaign();
            localCampaign.Campaign = campaign;
            localCampaign.AdGroups = new List<LocalAdGroup>(GetAdGroups(campaign.id, clientEmail));
            CampaignCriterion[] campaignCriteria = GetCampaignCriteria(campaign.id, clientEmail);
            if (campaignCriteria != null) {
              localCampaign.CampaignCriteria = new List<CampaignCriterion>(campaignCriteria);
            }
            TargetList[] targetsArray = GetCampaignTargets(campaign.id, clientEmail);
            if (targetsArray != null) {
              localCampaign.CampaignTargets = new List<TargetList>(targetsArray);
            }
            retval.Add(localCampaign);
          }
        }
      }
      return retval.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all adgroups in a campaign.
    /// </summary>
    /// <param name="campaignId">Campaign Id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of ad groups in the campaign.</returns>
    private LocalAdGroup[] GetAdGroups(long campaignId, string clientEmail) {
      List<LocalAdGroup> retval = new List<LocalAdGroup>();

      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v200909.AdGroupService);
      adGroupService.RequestHeader.clientEmail = clientEmail;

      AdGroupSelector adGroupSelector = new AdGroupSelector();
      adGroupSelector.campaignIdSpecified = true;
      adGroupSelector.campaignId = campaignId;

      AdGroupPage page = adGroupService.get(adGroupSelector);
      if (page != null && page.entries != null) {
        foreach (AdGroup adGroup in page.entries) {
          LocalAdGroup adgroupex = new LocalAdGroup();

          adgroupex.AdGroup = adGroup;
          AdGroupAd[] ads = GetAllAds(adGroup.id, clientEmail);
          if (ads != null) {
            adgroupex.Ads = new List<AdGroupAd>(ads);
          }
          AdGroupCriterion[] adGroupCriteria = GetAdGroupCriteria(adGroup.id, clientEmail);
          if (adGroupCriteria != null) {
            adgroupex.Criteria = new List<AdGroupCriterion>(adGroupCriteria);
          }
          retval.Add(adgroupex);
        }
      }
      return retval.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all ads in an adgroup.
    /// </summary>
    /// <param name="adGroupId">The AdGroup Id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of ads in the adgroup.</returns>
    private AdGroupAd[] GetAllAds(long adGroupId, string clientEmail) {
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v200909.AdGroupAdService);

      AdGroupAdSelector selector = new AdGroupAdSelector();
      selector.adGroupIds = new long[] { adGroupId };

      List<AdGroupAd> retval = new List<AdGroupAd>();
      AdGroupAdPage page = service.get(selector);

      if (page != null && page.entries != null) {
        foreach (AdGroupAd adGroupAd in page.entries) {
          // TODO(Anash): ImageAd and MobileImageAd gives mediaId, which cannot be
          // downloaded yet with v200909.
          if (adGroupAd.ad is TextAd) {
            retval.Add(adGroupAd);
          }
        }
      }

      return retval.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all criteria in an adgroup.
    /// </summary>
    /// <param name="adGroupId">The AdGroup Id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of criteria in the adgroup.</returns>
    private AdGroupCriterion[] GetAdGroupCriteria(long adGroupId, string clientEmail) {
      AdGroupCriterionService criterionService = (AdGroupCriterionService) user.GetService(
          AdWordsService.v200909.AdGroupCriterionService);
      AdGroupCriterionSelector selector = new AdGroupCriterionSelector();

      AdGroupCriterionIdFilter filter = new AdGroupCriterionIdFilter();
      filter.adGroupIdSpecified = true;
      filter.adGroupId = adGroupId;
      selector.idFilters = new AdGroupCriterionIdFilter[] { filter };

      criterionService.RequestHeader.clientEmail = clientEmail;

      AdGroupCriterionPage page = criterionService.get(selector);
      List<AdGroupCriterion> retval = new List<AdGroupCriterion>();
      if (page != null && page.entries != null) {
        foreach (AdGroupCriterion adGroupCriterion in page.entries) {
          retval.Add(adGroupCriterion);
        }
      }
      return retval.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all criteria in a campaign.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of criteria in the campaign.</returns>
    private CampaignCriterion[] GetCampaignCriteria(long campaignId, string clientEmail) {
      CampaignCriterionService criterionService = (CampaignCriterionService) user.GetService(
          AdWordsService.v200909.CampaignCriterionService);
      CampaignCriterionSelector selector = new CampaignCriterionSelector();

      CampaignCriterionIdFilter filter = new CampaignCriterionIdFilter();
      filter.campaignIdSpecified = true;
      filter.campaignId = campaignId;
      selector.idFilters = new CampaignCriterionIdFilter[] { filter };

      criterionService.RequestHeader.clientEmail = clientEmail;

      CampaignCriterionPage page = criterionService.get(selector);
      List<CampaignCriterion> retval = new List<CampaignCriterion>();
      if (page != null && page.entries != null) {
        foreach (CampaignCriterion campaignCriterion in page.entries) {
          retval.Add(campaignCriterion);
        }
      }
      return retval.ToArray();
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
          AdWordsService.v200909.CampaignTargetService);
      CampaignTargetSelector selector = new CampaignTargetSelector();
      selector.campaignIds = new long[] { campaignId };

      targetService.RequestHeader.clientEmail = clientEmail;

      CampaignTargetPage page = targetService.get(selector);

      List<TargetList> retval = new List<TargetList>();
      // Display campaigns.
      if (page != null && page.entries != null) {
        foreach (TargetList targetList in page.entries) {
          retval.Add(targetList);
        }
      }
      return retval.ToArray();
    }

    /// <summary>
    /// Fetches an image from the internet.
    /// </summary>
    /// <param name="url">The image url.</param>
    /// <returns>Returns the image data, as an array of bytes.</returns>
    private static byte[] FetchImage(string url) {
      WebRequest request = HttpWebRequest.Create(url);
      WebResponse response = request.GetResponse();

      Stream responseStream = response.GetResponseStream();

      MemoryStream memStream = new MemoryStream();
      byte[] strmBuffer = new byte[4096];

      int bytesRead = responseStream.Read(strmBuffer, 0, 4096);
      while (bytesRead != 0) {
        memStream.Write(strmBuffer, 0, bytesRead);
        bytesRead = responseStream.Read(strmBuffer, 0, 4096);
      }
      responseStream.Close();
      return memStream.ToArray();
    }
  }
}
