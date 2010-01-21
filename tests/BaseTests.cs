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

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;

namespace com.google.api.adwords.tests {
  /// <summary>
  /// Base class for all test suites.
  /// </summary>
  class BaseTests {
    /// <summary>
    /// Default public constructor.
    /// </summary>
    public BaseTests() {
      AdWordsUser user = new AdWordsUser();
      AccountService accountService =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);
      accountService.clientEmailValue = null;
      string[] clients = accountService.getClientAccounts();
    }
  }
}
