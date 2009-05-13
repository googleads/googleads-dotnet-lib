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

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;

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
  internal class AccountManager {
    /// <summary>
    /// AdWords user associated with this class.
    /// </summary>
    internal AdWordsUser user = null;

    /// <summary>
    /// Default constructor for AccountManager.
    /// </summary>
    internal AccountManager() {
      user = new AdWordsUser();
    }

    /// <summary>
    /// Overloaded constructor for AccountManager.
    /// </summary>
    /// <param name="user">The AdWordsUser to be associated with this manager.</param>
    internal AccountManager(AdWordsUser user) {
      this.user = user;
    }

    /// <summary>
    /// Downloads all sandbox client accounts under an mcc. This function
    /// internally uses <see cref="DownloadAccount"/> to download each client returned
    /// by getClientAccounts method of AccountService.
    /// </summary>
    /// <returns>An array of ClientAccount objects.</returns>
    internal ClientAccount[] DownloadAllAccounts() {
      user.UseSandbox();
      AccountService accountService = (AccountService) user.GetService(
          ApiServices.v13.AccountService);
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
    internal ClientAccount DownloadAccount(string clientEmail) {
      AccountService accountService =
          (AccountService) user.GetService(ApiServices.v13.AccountService);

      accountService.clientEmailValue.Value[0] = clientEmail;

      ClientAccount account = new ClientAccount();
      account.email = clientEmail;
      account.accountInfo = accountService.getAccountInfo();
      account.campaigns = new List<CampaignEx>(GetAccountCampaigns(clientEmail));
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
          (AccountService) user.GetService(ApiServices.v13.AccountService);
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
          (AccountService) user.GetService(ApiServices.v13.AccountService);
      accountService.clientEmailValue.Value[0] = account.email;

      accountService.updateAccountInfo(account.accountInfo);
      SetAccountCampaigns(account.campaigns.ToArray(), account.email);
    }

    /// <summary>
    /// Restores the campaigns in an account.
    /// </summary>
    /// <param name="campaigns">The campaigns to be restored.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetAccountCampaigns(CampaignEx[] campaigns, string clientEmail) {
      CampaignService campaignService =
          (CampaignService) user.GetService(ApiServices.v13.CampaignService);
      campaignService.clientEmailValue.Value[0] = clientEmail;

      foreach (CampaignEx campaignEx in campaigns) {
        campaignEx.campaign.id = 0;
        if (campaignEx.campaign.startDay < DateTime.Today) {
          campaignEx.campaign.startDay = DateTime.Today;
        }
        if (campaignEx.campaign.status == CampaignStatus.Active ||
            campaignEx.campaign.status == CampaignStatus.Paused) {
          Campaign newCampaign = campaignService.addCampaign(campaignEx.campaign);
          SetAdGroups(newCampaign.id, campaignEx.adGroups.ToArray(), newCampaign.languageTargeting,
              newCampaign.geoTargeting, clientEmail);
          SetCampaignCriteria(newCampaign.id, campaignEx.criteria.ToArray(),
              newCampaign.languageTargeting, newCampaign.geoTargeting, clientEmail);
        }
      }
    }

    /// <summary>
    /// Restores adgroups in a campaign.
    /// </summary>
    /// <param name="campaignId">Campaign id.</param>
    /// <param name="adGroups">The list of adgroups to be added to
    /// this campaign.</param>
    /// <param name="languageTargeting">Campaign's language targeting.</param>
    /// <param name="geoTargeting">Campaign's geo targeting.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetAdGroups(int campaignId, AdGroupEx[] adGroups, string[] languageTargeting,
        GeoTarget geoTargeting, string clientEmail) {
      AdGroupService adgroupService =
          (AdGroupService) user.GetService(ApiServices.v13.AdGroupService);
      adgroupService.clientEmailValue.Value[0] = clientEmail;

      foreach (AdGroupEx adGroupEx in adGroups) {
        adGroupEx.adgroup.campaignId = campaignId;

        AdGroup newAdGroup = adgroupService.addAdGroup(campaignId, adGroupEx.adgroup);
        SetAds(newAdGroup.id, adGroupEx.ads.ToArray(), languageTargeting,
            geoTargeting, clientEmail);
        SetCriteria(newAdGroup.id, adGroupEx.criteria.ToArray(), languageTargeting,
            geoTargeting, clientEmail);
      }
    }

    /// <summary>
    /// Restores ads in an adgroup.
    /// </summary>
    /// <param name="adGroupId">AdGroup Id.</param>
    /// <param name="ads">Ads to be added to the adgroup.</param>
    /// <param name="languageTargeting">Campaign's language targeting.</param>
    /// <param name="geoTargeting">Campaign's geo targeting.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetAds(long adGroupId, Ad[] ads, string[] languageTargeting,
        GeoTarget geoTargeting, string clientEmail) {
      AdService adService = (AdService) user.GetService(ApiServices.v13.AdService);
      adService.clientEmailValue.Value[0] = clientEmail;

      Ad[] cleanAds = CheckAds(ads, clientEmail, languageTargeting, geoTargeting);

      if (cleanAds.Length > 0) {
        foreach (Ad ad in cleanAds) {
          ad.adGroupId = adGroupId;
        }
        Ad[] newAds = adService.addAds(cleanAds);
      }
    }

    /// <summary>
    /// Restores criteria in an adgroup.
    /// </summary>
    /// <param name="adGroupId">AdGroup Id.</param>
    /// <param name="criteria">The list of criteria to be added to
    /// the adgroup.</param>
    /// <param name="languageTargeting">Campaign's language targeting.</param>
    /// <param name="geoTargeting">Campaign's geo targeting.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetCriteria(long adGroupId, Criterion[] criteria, string[] languageTargeting,
        GeoTarget geoTargeting, string clientEmail) {
      CriterionService criterionService =
          (CriterionService) user.GetService(ApiServices.v13.CriterionService);
      criterionService.clientEmailValue.Value[0] = clientEmail;

      foreach (Criterion criterion in criteria) {
        criterion.adGroupId = 0;
        criterion.id = 0;
      }

      Criterion[] cleanCriteria = CheckCriteria(criteria, languageTargeting,
          geoTargeting, clientEmail);

      if (cleanCriteria.Length > 0) {
        foreach (Criterion criterion in cleanCriteria) {
          criterion.adGroupId = adGroupId;
        }
        Criterion[] newCriteria = criterionService.addCriteria(cleanCriteria);
      }
    }

    /// <summary>
    /// Restores criteria in a campaign.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="criteria">The list of criteria to be added to
    /// the adgroup.</param>
    /// <param name="languageTargeting">Campaign's language targeting.</param>
    /// <param name="geoTargeting">Campaign's geo targeting.</param>
    /// <param name="clientEmail">client account email.</param>
    private void SetCampaignCriteria(int campaignId, Criterion[] criteria,
        string[] languageTargeting, GeoTarget geoTargeting, string clientEmail) {
      CriterionService criterionService =
          (CriterionService) user.GetService(ApiServices.v13.CriterionService);
      criterionService.clientEmailValue.Value[0] = clientEmail;

      foreach (Criterion criterion in criteria) {
        criterion.adGroupId = 0;
        criterion.id = 0;
      }

      Criterion[] cleanCriteria = CheckCriteria(criteria, languageTargeting,
          geoTargeting, clientEmail);

      if (cleanCriteria.Length > 0) {
        criterionService.setCampaignNegativeCriteria(campaignId, cleanCriteria);
      }
    }

    /// <summary>
    /// Checks the ads for policy violation prior to adding them in an adgroup.
    /// </summary>
    /// <param name="ads">The list of ads to be checked.</param>
    /// <param name="languageTargeting">Campaign's language targeting.</param>
    /// <param name="geoTargeting">Campaign's geo targeting.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of ads, with exemption request added if applicable.
    /// Items that cannot be exempted are removed from the list.</returns>
    private Ad[] CheckAds(Ad[] ads, string clientEmail, string[] languageTargeting,
        GeoTarget geoTargeting) {
      AdService adService = (AdService) user.GetService(ApiServices.v13.AdService);
      adService.clientEmailValue.Value[0] = clientEmail;

      foreach (Ad ad in ads) {
        ad.adGroupId = 0;
        ad.id = 0;
      }

      ApiError[] errors = adService.checkAds(ads, languageTargeting, geoTargeting);

      if (errors != null) {
        foreach (ApiError error in errors) {
          if (error.isExemptable) {
            ads[error.index].exemptionRequest = "Imported using SandboxRestoreTool.";
          } else {
            Console.WriteLine("Could not import Ad");
            ads[error.index] = null;
          }
        }
      }
      return (Ad[]) EliminateNulls(new ArrayList(ads)).ToArray(typeof(Ad));
    }

    /// <summary>
    /// Checks the criteria for policy violations prior to adding them
    /// in an adgroup or campaign.
    /// </summary>
    /// <param name="criteria">List of criteria to be checked.</param>
    /// <param name="languageTargeting">Campaign's language targeting.</param>
    /// <param name="geoTargeting">Campaign's geo targeting.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of criteria, with exemption request added if applicable.
    /// Items that cannot be exempted are removed from the list.</returns>
    private Criterion[] CheckCriteria(Criterion[] criteria, string[] languageTargeting,
        GeoTarget geoTargeting, string clientEmail) {
      CriterionService criterionService =
          (CriterionService) user.GetService(ApiServices.v13.CriterionService);
      criterionService.clientEmailValue.Value[0] = clientEmail;

      ApiError[] errors = criterionService.checkCriteria(criteria, languageTargeting,
          geoTargeting);

      if (errors != null) {
        foreach (ApiError error in errors) {
          if (error.isExemptable) {
            criteria[error.index].exemptionRequest = "Imported using SandboxRestoreTool.";
          } else {
            Console.WriteLine("Could not import Criterion");
            criteria[error.index] = null;
          }
        }
      }
      return (Criterion[]) EliminateNulls(new ArrayList(criteria)).ToArray(typeof(Criterion));
    }

    /// <summary>
    /// Retrieves the list of all campaigns in an account.
    /// </summary>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of campaigns in the account.</returns>
    private CampaignEx[] GetAccountCampaigns(string clientEmail) {
      List<CampaignEx> retVal = new List<CampaignEx>();

      CampaignService campaignService =
          (CampaignService) user.GetService(ApiServices.v13.CampaignService);
      campaignService.clientEmailValue.Value[0] = clientEmail;

      Campaign[] campaigns = campaignService.getAllAdWordsCampaigns(0);

      if (campaigns != null) {
        foreach (Campaign campaign in campaigns) {
          CampaignEx campaignex = new CampaignEx();
          campaignex.campaign = campaign;
          campaignex.adGroups = new List<AdGroupEx>(GetAdGroups(campaign.id, clientEmail));
          Criterion[] criterionArray = GetCampaignCriteria(campaign.id, clientEmail);
          if (criterionArray != null) {
            campaignex.criteria = new List<Criterion>(criterionArray);
          }
          retVal.Add(campaignex);
        }
      }
      return retVal.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all adgroups in a campaign.
    /// </summary>
    /// <param name="campaignId">Campaign Id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of adgroups in the campaign.</returns>
    private AdGroupEx[] GetAdGroups(int campaignId, string clientEmail) {
      List<AdGroupEx> retVal = new List<AdGroupEx>();

      AdGroupService adgroupService =
          (AdGroupService) user.GetService(ApiServices.v13.AdGroupService);
      adgroupService.clientEmailValue.Value[0] = clientEmail;

      AdGroup[] adgroups = adgroupService.getAllAdGroups(campaignId);

      if (adgroups != null) {
        foreach (AdGroup adgroup in adgroups) {
          AdGroupEx adgroupex = new AdGroupEx();

          adgroupex.adgroup = adgroup;
          Ad[] adArray = GetAllAds(adgroup.id, clientEmail);
          if (adArray != null) {
            adgroupex.ads = new List<Ad>(adArray);
          }
          Criterion[] criterionArray = GetAdGroupCriteria(adgroup.id, clientEmail);
          if (criterionArray != null) {
            adgroupex.criteria = new List<Criterion>(criterionArray);
          }
          retVal.Add(adgroupex);
        }
      }
      return retVal.ToArray();
    }

    /// <summary>
    /// Retrieves the list of all ads in an adgroup.
    /// </summary>
    /// <param name="adGroupId">The AdGroup Id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of ads in the adgroup.</returns>
    private Ad[] GetAllAds(long adGroupId, string clientEmail) {
      AdService adService = (AdService) user.GetService(
          ApiServices.v13.AdService);
      adService.clientEmailValue.Value[0] = clientEmail;

      Ad[] ads = adService.getAllAds(new long[] {adGroupId});
      if (ads != null) {
        foreach (Ad ad in ads) {
          if (ad is ImageAd && (ad as ImageAd).image != null) {
            (ad as ImageAd).image.data = FetchImage((ad as ImageAd).image.imageUrl);
          } else if (ad is VideoAd && (ad as VideoAd).image != null) {
            (ad as VideoAd).image.data = FetchImage((ad as VideoAd).image.imageUrl);
          } else if (ad is MobileImageAd && (ad as MobileImageAd).image != null) {
            (ad as MobileImageAd).image.data = FetchImage((ad as MobileImageAd).image.imageUrl);
          } else if (ad is CommerceAd && (ad as CommerceAd).productImage != null) {
            (ad as CommerceAd).productImage.data =
                FetchImage((ad as CommerceAd).productImage.imageUrl);
          } else if (ad is LocalBusinessAd && (ad as LocalBusinessAd).customIcon != null) {
            (ad as LocalBusinessAd).customIcon.data =
                FetchImage((ad as LocalBusinessAd).customIcon.imageUrl);
          } else if (ad is LocalBusinessAd && (ad as LocalBusinessAd).businessImage != null) {
            (ad as LocalBusinessAd).businessImage.data =
                FetchImage((ad as LocalBusinessAd).businessImage.imageUrl);
          }
        }
      }
      return ads;
    }

    /// <summary>
    /// Retrieves the list of all criteria in an adgroup.
    /// </summary>
    /// <param name="adGroupId">The AdGroup Id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of criteria in the adgroup.</returns>
    private Criterion[] GetAdGroupCriteria(long adGroupId, string clientEmail) {
      CriterionService criterionService = (CriterionService) user.GetService(
          ApiServices.v13.CriterionService);
      criterionService.clientEmailValue.Value[0] = clientEmail;
      return criterionService.getAllCriteria(adGroupId);
    }

    /// <summary>
    /// Retrieves the list of all criteria in a campaign.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="clientEmail">client account email.</param>
    /// <returns>List of criteria in the campaign.</returns>
    private Criterion[] GetCampaignCriteria(int campaignId, string clientEmail) {
      CriterionService criterionService = (CriterionService) user.GetService(
          ApiServices.v13.CriterionService);
      criterionService.clientEmailValue.Value[0] = clientEmail;
      return criterionService.getCampaignNegativeCriteria(campaignId);
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

    /// <summary>
    /// Eliminate null elements in an array.
    /// </summary>
    /// <param name="aryItems">Input array.</param>
    /// <returns>Output array, which is the input array, minus all null elements.
    /// </returns>
    private static ArrayList EliminateNulls(ArrayList aryItems) {
      ArrayList retVal = new ArrayList();
      foreach (object item in aryItems) {
        if (item != null) {
          retVal.Add(item);
        }
      }
      return retVal;
    }
  }
}
