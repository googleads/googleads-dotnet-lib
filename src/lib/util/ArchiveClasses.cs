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
using System.Collections.Generic;

using com.google.api.adwords.v13;

namespace com.google.api.adwords.lib.util {
  /// <summary>
  /// Represents a client account downloaded by sandbox download script.
  /// </summary>
  internal class ClientAccount {
    /// <summary>
    /// email of the client account.
    /// </summary>
    internal string email = "";

    /// <summary>
    /// Account information.
    /// </summary>
    internal AccountInfo accountInfo = new AccountInfo();

    /// <summary>
    /// List of all campaigns.
    /// </summary>
    internal List<CampaignEx> campaigns = new List<CampaignEx>();
  }

  /// <summary>
  /// Represents a campaign downloaded by sandbox download script.
  /// </summary>
  public class CampaignEx {
    /// <summary>
    /// The underlying AdWords campaign.
    /// </summary>
    public Campaign campaign = new Campaign();

    /// <summary>
    /// List of all campaign-level criteria.
    /// </summary>
    public List<Criterion> criteria = new List<Criterion>();

    /// <summary>
    /// List of all adgroups in this campaign.
    /// </summary>
    public List<AdGroupEx> adGroups = new List<AdGroupEx>();
  }

  /// <summary>
  /// Represents an adgroup downloaded by sandbox download script.
  /// </summary>
  public class AdGroupEx {
    /// <summary>
    /// The underlying AdWords adgroup.
    /// </summary>
    public AdGroup adgroup = new AdGroup();

    /// <summary>
    /// List of all criteria in this adgroup.
    /// </summary>
    public List<Criterion> criteria = new List<Criterion>();

    /// <summary>
    /// List of all ads in this adgroup.
    /// </summary>
    public List<Ad> ads = new List<Ad>();
  }
}
