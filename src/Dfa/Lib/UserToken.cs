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

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// This class represents a WSE security token to be used with DFA services.
  /// </summary>
  public class UserToken {
    /// <summary>
    /// The username obtained from LoginService.
    /// </summary>
    string username;

    /// <summary>
    /// The password token obtained from LoginService.
    /// </summary>
    string password;

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string Username {
      get {
        return username;
      } set {
        username = value;
      }
    }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password {
      get {
        return password;
      } set {
        password = value;
      }
    }

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public UserToken() : this("", "") {
    }

    /// <summary>
    /// Parameterized constructor.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    public UserToken(string username, string password) {
      this.username = username;
      this.password = password;
    }
  }
}
