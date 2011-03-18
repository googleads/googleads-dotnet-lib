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

using com.google.api.adwords.v13;
using com.google.api.adwords.v200909;

using System;
using System.Collections.Generic;

namespace com.google.api.adwords.lib.util {
  /// <summary>
  /// Represents a client account downloaded by sandbox download script.
  /// </summary>
  [Serializable]
  public class ClientAccount {
    /// <summary>
    /// Login email for the client account.
    /// </summary>
    private string email = "";

    /// <summary>
    /// Account information.
    /// </summary>
    private AccountInfo accountInfo = new AccountInfo();

    /// <summary>
    /// List of campaigns in this account.
    /// </summary>
    private List<LocalCampaign> campaigns = new List<LocalCampaign>();

    /// <summary>
    /// Gets or sets login email for the client account.
    /// </summary>
    public string Email {
      get {
        return email;
      }
      set {
        email = value;
      }
    }

    /// <summary>
    /// Gets or sets the account information.
    /// </summary>
    public AccountInfo AccountInfo {
      get {
        return accountInfo;
      }
      set {
        accountInfo = value;
      }
    }

    /// <summary>
    /// Gets or sets the account campaigns.
    /// </summary>
    public List<LocalCampaign> Campaigns {
      get {
        return campaigns;
      }
      set {
        campaigns = value;
      }
    }
  }

  /// <summary>
  /// Represents a campaign downloaded by sandbox download script.
  /// </summary>
  [Serializable]
  public class LocalCampaign {
    /// <summary>
    /// The underlying AdWords campaign.
    /// </summary>
    private Campaign campaign = new Campaign();

    /// <summary>
    /// Campaign targets for <see cref="campaign"/>
    /// </summary>
    private List<TargetList> campaignTargets = new List<TargetList>();

    /// <summary>
    /// List of all campaign-level criteria.
    /// </summary>
    private List<CampaignCriterion> campaignCriteria = new List<CampaignCriterion>();

    /// <summary>
    /// List of all ad groups in this campaign.
    /// </summary>
    private List<LocalAdGroup> adGroups = new List<LocalAdGroup>();

    /// <summary>
    /// Gets or sets the campaign.
    /// </summary>
    public Campaign Campaign {
      get {
        return campaign;
      }
      set {
        campaign = value;
      }
    }

    /// <summary>
    /// Gets or sets the campaign targets.
    /// </summary>
    public List<TargetList> CampaignTargets {
      get {
        return campaignTargets;
      }
      set {
        campaignTargets = value;
      }
    }

    /// <summary>
    /// Gets or sets the campaign criteria.
    /// </summary>
    public List<CampaignCriterion> CampaignCriteria {
      get {
        return campaignCriteria;
      }
      set {
        campaignCriteria = value;
      }
    }

    /// <summary>
    /// Gets or sets the adgroups.
    /// </summary>
    public List<LocalAdGroup> AdGroups {
      get {
        return adGroups;
      }
      set {
        adGroups = value;
      }
    }
  }

  /// <summary>
  /// Represents an ad group downloaded by sandbox download script.
  /// </summary>
  [Serializable]
  public class LocalAdGroup {
    /// <summary>
    /// The underlying AdWords ad group.
    /// </summary>
    private AdGroup adGroup = new AdGroup();

    /// <summary>
    /// List of all criteria in this adgroup.
    /// </summary>
    private List<AdGroupCriterion> criteria = new List<AdGroupCriterion>();

    /// <summary>
    /// List of all ads in this adgroup.
    /// </summary>
    private List<AdGroupAd> ads = new List<AdGroupAd>();

    /// <summary>
    /// Gets or sets the ad group.
    /// </summary>
    public AdGroup AdGroup {
      get {
        return adGroup;
      }
      set {
        adGroup = value;
      }
    }

    /// <summary>
    /// Gets or sets the criteria.
    /// </summary>
    public List<AdGroupCriterion> Criteria {
      get {
        return criteria;
      }
      set {
        criteria = value;
      }
    }

    /// <summary>
    /// Gets or sets the ads.
    /// </summary>
    public List<AdGroupAd> Ads {
      get {
        return ads;
      }
      set {
        ads = value;
      }
    }
  }
}
