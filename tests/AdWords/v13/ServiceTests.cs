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

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace com.google.api.adwords.tests.v13 {
  /// <summary>
  /// UnitTests for various AdWords services.
  /// </summary>
  [TestFixture]
  public class ServiceTests {
    /// <summary>
    /// Default public constructor.
    /// </summary>
    public ServiceTests() : base() {
    }

    /// <summary>
    /// Test if v13 API calls can be made successfully.
    /// </summary>
    [Test]
    public void TestV13ApiCalls() {
      AdWordsUser user = new AdWordsUser();
      AccountService accountService =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);
      accountService.clientEmailValue = null;
      accountService.clientCustomerIdValue = null;

      string[] clients = null;
      Assert.DoesNotThrow(
          delegate() {
            clients = accountService.getClientAccounts();
          }, "AccountService.getClientAccounts() should not throw an exception.");
      Assert.NotNull(clients);
      Assert.AreEqual(clients.Length, 5);
      for(int i=0;i<clients.Length;i++) {
        Assert.AreEqual(clients[i],
            string.Format("client_{0}+{1}", i + 1, accountService.emailValue.Value[0]));
      }
    }

    /// <summary>
    /// Test if v13 custom exception handling works properly.
    /// </summary>
    [Test]
    public void TestV13CustomExceptions() {
      AdWordsUser user = new AdWordsUser();
      AccountService accountService =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);
      accountService.clientEmailValue = null;
      accountService.clientCustomerIdValue = null;

      int[] errorCodes = {86, 53, 117, 101, 116, 41, 120, 176, 125, 61, 58, 43};
      Type[] exceptionTypes = {
        typeof(AccountException), typeof(BillingException), typeof(GoogleInternalException),
        typeof(WebPageException), typeof(SandboxException), typeof(InvalidRequestException),
        typeof(PolicyViolationException),
        typeof(com.google.api.adwords.v13.InvalidOperationException),
        typeof(InvalidParameterException), typeof(PermissionException),
        typeof(ConcurrencyException), typeof(ExceededLimitsException)
      };

      for (int i = 0; i < errorCodes.Length; i++) {
        accountService.emailValue.Value[0] =
            string.Format("{0}++{1}", ApplicationConfiguration.email, errorCodes[i]);
        Assert.Throws(
            exceptionTypes[i],
            delegate() {
              accountService.getClientAccounts();
            });
      }
    }
  }
}
