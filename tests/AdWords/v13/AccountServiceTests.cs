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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v13;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v13 {
  /// <summary>
  /// UnitTests for <see cref="AccountService"/> class.
  /// </summary>
  [TestFixture]
  class AccountServiceTests : BaseTests {
    /// <summary>
    /// AccountService object to be used in this test.
    /// </summary>
    private AccountService accountService;

    /// <summary>
    /// Config object to be used in tests.
    /// </summary>
    private AdWordsAppConfig config = new AdWordsAppConfig();

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AccountServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      accountService = (AccountService) user.GetService(AdWordsService.v13.AccountService);
    }

    /// <summary>
    /// Test whether we can fetch information for account using v13.
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestGetAccountInfo() {
      accountService.clientEmailValue = new clientEmail();
      accountService.clientEmailValue.Value = new string[] {config.ClientEmail};
      Assert.That(accountService.getAccountInfo() is AccountInfo);
    }

    /// <summary>
    /// Test whether we can fetch all client account information using v13.
    /// </summary>
    [Test]
    public void TestGetClientAccountInfos() {
      accountService.clientEmailValue = null;

      object results = accountService.getClientAccountInfos();
      Assert.That(results == null || results is ClientAccountInfo[]);
    }

    /// <summary>
    /// Test whether we can fetch all client accounts using v13.
    /// </summary>
    [Test]
    public void TestGetClientAccounts() {
      accountService.clientEmailValue = null;

      object results = accountService.getClientAccounts();
      Assert.That(results == null || results is string[]);
    }

    /// <summary>
    /// Test whether we can retrieve MCC alerts using v13.
    /// </summary>
    [Test]
    public void TestGetMccAlerts() {
      accountService.clientEmailValue = null;

      MccAlert[] alerts = null;
      Assert.DoesNotThrow(delegate() {
        alerts = accountService.getMccAlerts();
      });
      Assert.That(alerts == null || alerts.Length > 0);
    }

    /// <summary>
    /// Test whether we can update email promotions preferences in account using v13.
    /// </summary>
    [Test]
    public void TestUpdateAccountInfo() {
      accountService.clientEmailValue = new clientEmail();
      accountService.clientEmailValue.Value = new string[] {config.ClientEmail};

      AccountInfo info = new AccountInfo();
      info.emailPromotionsPreferences = new EmailPromotionsPreferences();
      info.emailPromotionsPreferences.accountPerformanceEnabled  = true;
      info.emailPromotionsPreferences.marketResearchEnabled  = false;
      info.emailPromotionsPreferences.newsletterEnabled  = false;
      info.emailPromotionsPreferences.promotionsEnabled  = false;

      Assert.DoesNotThrow(delegate() {
        accountService.updateAccountInfo(info);
      });
    }
  }
}
