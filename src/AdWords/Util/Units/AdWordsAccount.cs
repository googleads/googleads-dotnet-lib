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

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Util.Units {
  /// <summary>
  /// Private class used by UnitsUtilities while walking the account tree.
  /// </summary>
  internal class AdWordsAccount {
    /// <summary>
    /// Account email.
    /// </summary>
    private string email;

    /// <summary>
    /// True, if the account is an MCC.
    /// </summary>
    private bool isManager;

    /// <summary>
    /// API units consumed by this account.
    /// </summary>
    private long units;

    /// <summary>
    /// True, if this account has been visited before while walking the
    /// account hierarchy.
    /// </summary>
    private bool visited;

    /// <summary>
    /// The parent accounts for this account.
    /// </summary>
    private List<AdWordsAccount> parents = new List<AdWordsAccount>();

    /// <summary>
    /// Child accounts for this account, if this account is an MCC.
    /// </summary>
    private List<AdWordsAccount> children = new List<AdWordsAccount>();

    /// <summary>
    /// Gets or sets the account email.
    /// </summary>
    internal string Email {
      get {
        return email;
      }
      set {
        email = value;
      }
    }

    /// <summary>
    /// Gets or sets whether an account is an MCC.
    /// </summary>
    internal bool IsManager {
      get {
        return isManager;
      }
      set {
        isManager = value;
      }
    }

    /// <summary>
    /// Gets or sets API units consumed by this account.
    /// </summary>
    internal long Units {
      get {
        return units;
      }
      set {
        units = value;
      }
    }

    /// <summary>
    /// Gets or sets if this account has been visited before while walking
    /// the account hierarchy.
    /// </summary>
    internal bool Visited {
      get {
        return visited;
      }
      set {
        visited = value;
      }
    }

    /// <summary>
    /// Gets or sets the parent accounts for this account.
    /// </summary>
    internal List<AdWordsAccount> Parents {
      get {
        return parents;
      }
    }

    /// <summary>
    /// Gets or sets child accounts for this account, if this account
    /// is an MCC.
    /// </summary>
    internal List<AdWordsAccount> Children {
      get {
        return children;
      }
    }
  }
}
