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
    /// Gets the criteria.
    /// </summary>
    public List<AdGroupCriterion> Criteria {
      get {
        return criteria;
      }
    }

    /// <summary>
    /// Gets the ads.
    /// </summary>
    public List<AdGroupAd> Ads {
      get {
        return ads;
      }
    }
  }
}
