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

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;
using com.google.api.adwords.v200902.CampaignService;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using CampaignServiceV200902 = com.google.api.adwords.v200902.CampaignService.CampaignService;
using CampaignV200902 = com.google.api.adwords.v200902.CampaignService.Campaign;
using CampaignStatusV200902 = com.google.api.adwords.v200902.CampaignService.CampaignStatus;

namespace com.google.api.adwords.tests {
  /// <summary>
  /// UnitTests for various AdWords services.
  /// </summary>
  [TestFixture]
  public class ServiceTests {
    /// <summary>
    /// Test if v13 API calls can be made successfully.
    /// </summary>
    [Test]
    public void TestV13ApiCalls() {
      AdWordsUser user = new AdWordsUser();
      AccountService accountService =
          (AccountService) user.GetService(ApiServices.v13.AccountService);
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
            string.Format("client_{0}+{1}", i+1, accountService.emailValue.Value[0]));
      }
    }

    /// <summary>
    /// Test if v200902 API calls can be made successfully.
    /// </summary>
    [Test]
    public void TestV200902ApiCalls() {
      AdWordsUser user = new AdWordsUser();

      CampaignServiceV200902 service =
          (CampaignServiceV200902) user.GetService(ApiServices.v200902.CampaignService);

      CampaignV200902 campaign = new CampaignV200902();

      // Generate a campaign name.
      string campaignName =
        string.Format("Campaign - {0}", DateTime.Now.ToString("yyyy-M-d H:m:s"));
      campaign.name = string.Format(campaignName);

      // Required: Set the campaign status.
      campaign.status = CampaignStatusV200902.ACTIVE;
      campaign.statusSpecified = true;

      // Required: Specify the currency and budget amount.
      Budget budget = new Budget();
      Money amount = new Money();
      amount.currencyCode = "USD";
      amount.microAmountSpecified = true;
      amount.microAmount = 50000000;

      budget.amount = amount;

      // Required: Specify the bidding strategy.
      campaign.biddingStrategy = new ManualCPC();

      // Optional: Specify the budget period and delivery method.
      budget.periodSpecified = true;
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.deliveryMethodSpecified = true;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      campaign.budget = budget;

      // Define an Add operation to add the campaign.
      CampaignOperation campaignOperation = new CampaignOperation();
      campaignOperation.operatorSpecified = true;
      campaignOperation.@operator = Operator.ADD;
      campaignOperation.operand = campaign;

      CampaignReturnValue results = null;

      Assert.DoesNotThrow(
          delegate() {
            results = service.mutate(new CampaignOperation[] {campaignOperation});
          },
          "CampaignService.mutate() should not throw an exception.");

      Assert.NotNull(results);
      Assert.NotNull(results.value);
      Assert.Greater(results.value.Length, 0);

      Console.WriteLine("New campaign with name = \"{0}\" and id = " +
          "\"{1}\" was created.", results.value[0].name, results.value[0].id.id);
      Assert.AreEqual(results.value[0].name, campaign.name);
      Assert.AreEqual(results.value[0].status, campaign.status);
      Assert.AreEqual(results.value[0].budget.amount.currencyCode,
          campaign.budget.amount.currencyCode);
      Assert.AreEqual(results.value[0].budget.amount.microAmount,
          campaign.budget.amount.microAmount);
      Assert.AreEqual(results.value[0].budget.period, campaign.budget.period);
      Assert.AreEqual(results.value[0].budget.deliveryMethod, campaign.budget.deliveryMethod);
      Assert.That(results.value[0].biddingStrategy is ManualCPC);
    }

    /// <summary>
    /// Test if v13 custom exception handling works properly.
    /// </summary>
    [Test]
    public void TestV13CustomExceptions() {
      AdWordsUser user = new AdWordsUser();
      AccountService accountService =
          (AccountService) user.GetService(ApiServices.v13.AccountService);
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
