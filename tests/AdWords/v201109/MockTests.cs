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
using Google.Api.Ads.AdWords.v201109;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Google.Api.Ads.AdWords.Tests.v201109 {
  /// <summary>
  /// UnitTests for mocking a SOAP service.
  /// </summary>
  [TestFixture]
  class MockTests : BaseTests {
    /// <summary>
    /// A mocked version of CampaignService. The get() method is mocked, and
    /// merely sets a flag to mark that the method was called.
    /// </summary>
    public class MockCampaignService : CampaignService {
      /// <summary>
      /// Tracks if the get() method was called.
      /// </summary>
      bool getCalled = false;

      /// <summary>
      /// Gets whether get() method was called.
      /// </summary>
      public bool GetCalled {
        get {
          return getCalled;
        }
      }

      /// <summary>
      /// Resets this instance.
      /// </summary>
      public void Reset() {
        getCalled = false;
      }

      /// <summary>
      /// Mocked version of CampaignService::get
      /// </summary>
      /// <param name="serviceSelector">The service selector.</param>
      /// <returns>null</returns>
      public override CampaignPage get(Selector serviceSelector) {
        getCalled = true;
        return null;
      }
    }

    /// <summary>
    /// An advanced mocked version of CampaignService. The mutate() method is
    /// mocked. It makes the actual API call and sets a flag to mark that the
    /// method was called.
    /// </summary>
    public class MockCampaignServiceEx : CampaignService {
      /// <summary>
      /// Tracks if the mutate() method was called.
      /// </summary>
      bool mutateCalled = false;

      /// <summary>
      /// Gets whether mutate() method was called.
      /// </summary>
      public bool MutateCalled {
        get {
          return mutateCalled;
        }
      }

      /// <summary>
      /// Resets this instance.
      /// </summary>
      public void Reset() {
        mutateCalled = false;
      }

      /// <summary>
      /// Mocked version of CampaignService::mutate.
      /// </summary>
      /// <param name="operations">The operations.</param>
      /// <returns>The results of mutating the service values.</returns>
      [SoapHeaderAttribute("RequestHeader")]
      [SoapHeaderAttribute("ResponseHeader", Direction = SoapHeaderDirection.Out)]
      [SoapDocumentMethodAttribute("",
          RequestNamespace = "https://adwords.google.com/api/adwords/cm/v201109",
          ResponseNamespace = "https://adwords.google.com/api/adwords/cm/v201109",
          Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
      [return: XmlElementAttribute("rval")]
      public override CampaignReturnValue mutate(
        [XmlElementAttribute("operations")] CampaignOperation[] operations) {
        CampaignReturnValue retval = base.mutate(operations);
        mutateCalled = true;
        return retval;
      }
    }

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public MockTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
    }

    /// <summary>
    /// Test whether we can call the mocked version of CampaignService::get().
    /// </summary>
    [Test]
    public void TestGetAllCampaignsMockOnly() {
      MockUtilities.RegisterMockService(user, AdWordsService.v201109.CampaignService,
          typeof(MockCampaignService));
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v201109.CampaignService);
      Assert.That(campaignService is MockCampaignService);

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Name", "Status"};

      CampaignPage page = null;

      Assert.DoesNotThrow(delegate() {
        page = campaignService.get(selector);
      });

      Assert.IsNull(page);
      Assert.True((campaignService as MockCampaignService).GetCalled);
    }

    /// <summary>
    /// Test whether we can call the mocked version of
    /// CampaignService::mutate().
    /// </summary>
    [Test]
    public void TestGetAllCampaignsMockAndCallServer() {
      MockUtilities.RegisterMockService(user, AdWordsService.v201109.CampaignService,
          typeof(MockCampaignServiceEx));
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v201109.CampaignService);
      Assert.That(campaignService is MockCampaignServiceEx);

      Campaign campaign = new Campaign();
      campaign.name = "Interplanetary Cruise #" + new TestUtils().GetTimeStamp();
      campaign.status = CampaignStatus.PAUSED;
      campaign.biddingStrategy = new ManualCPC();

      Budget budget = new Budget();
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget.amount = new Money();
      budget.amount.microAmount = 50000000;

      campaign.budget = budget;

      // Set the campaign network options to GoogleSearch and SearchNetwork
      // only. Set ContentNetwork, PartnerSearchNetwork and ContentContextual
      // to false.
      campaign.networkSetting = new NetworkSetting();
      campaign.networkSetting.targetGoogleSearch = true;
      campaign.networkSetting.targetSearchNetwork = true;
      campaign.networkSetting.targetContentContextual = false;
      campaign.networkSetting.targetContentNetwork = false;
      campaign.networkSetting.targetPartnerSearchNetwork = false;

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
      operation.@operator = Operator.ADD;
      operation.operand = campaign;

      CampaignReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignService.mutate((new CampaignOperation[] {operation}));
      });

      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.AreEqual(retVal.value[0].name, campaign.name);
      Assert.AreNotEqual(retVal.value[0].id, 0);
      Assert.True((campaignService as MockCampaignServiceEx).MutateCalled);
    }
  }
}
