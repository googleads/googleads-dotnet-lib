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

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace com.google.api.adwords.tests {
  /// <summary>
  /// Base class for all test suites.
  /// </summary>
  class BaseTests {
    /// <summary>
    /// The AdWords user to be used for tests.
    /// </summary>
    protected AdWordsUser user = new AdWordsUser();

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public BaseTests() {
      // Make sure we don't hit the authtoken endpoint really bad.
      Thread.Sleep(2000);
      AccountService accountService =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);
      accountService.clientEmailValue = null;
      string[] clients = accountService.getClientAccounts();
    }
  }
}
