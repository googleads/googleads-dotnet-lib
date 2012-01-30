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

using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Util.Data {
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
    /// Gets the campaign criteria.
    /// </summary>
    public List<CampaignCriterion> CampaignCriteria {
      get {
        return campaignCriteria;
      }
    }

    /// <summary>
    /// Gets the adgroups.
    /// </summary>
    public List<LocalAdGroup> AdGroups {
      get {
        return adGroups;
      }
    }
  }
}
