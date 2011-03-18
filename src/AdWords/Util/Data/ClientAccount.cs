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

using Google.Api.Ads.AdWords.v13;
using Google.Api.Ads.AdWords.v200909;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Util.Data {
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
}
