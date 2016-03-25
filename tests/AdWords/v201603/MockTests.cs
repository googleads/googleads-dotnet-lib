// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests;
using Google.Api.Ads.Common.Util;

using System;
using System.Net;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

using NUnit.Framework;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201603;

namespace Google.Api.Ads.AdWords.Tests.v201603 {

  /// <summary>
  /// UnitTests for mocking a SOAP service.
  /// </summary>
  [TestFixture]
  internal class MockTests : VersionedExampleTestsBase {

    /// <summary>
    /// A mocked version of CampaignService. The get() method is mocked, and
    /// merely sets a flag to mark that the method was called.
    /// </summary>
    public class MockCampaignService : CampaignService {

      /// <summary>
      /// Tracks if the get() method was called.
      /// </summary>
      private bool getCalled = false;

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
      private bool mutateCalled = false;

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
          RequestNamespace = "https://adwords.google.com/api/adwords/cm/v201603",
          ResponseNamespace = "https://adwords.google.com/api/adwords/cm/v201603",
          Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
      [return: XmlElementAttribute("rval")]
      public override CampaignReturnValue mutate(
        [XmlElementAttribute("operations")] CampaignOperation[] operations) {
        CampaignReturnValue retval = base.mutate(operations);
        mutateCalled = true;
        return retval;
      }
    }

    private long budgetId;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public MockTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      budgetId = utils.CreateBudget(user);
    }

    /// <summary>
    /// Test whether we can call the mocked version of CampaignService::get().
    /// </summary>
    [Test]
    public void TestGetAllCampaignsMockOnly() {
      ServiceSignature mockSignature = MockUtilities.RegisterMockService(user,
          AdWordsService.v201603.CampaignService, typeof(MockCampaignService));

      CampaignService campaignService = (CampaignService) user.GetService(mockSignature);
      Assert.That(campaignService is MockCampaignService);

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] { "Id", "Name", "Status" };

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
      ServiceSignature mockSignature = MockUtilities.RegisterMockService(user,
          AdWordsService.v201603.CampaignService, typeof(MockCampaignServiceEx));

      CampaignService campaignService = (CampaignService) user.GetService(mockSignature);
      Assert.That(campaignService is MockCampaignServiceEx);

      Campaign campaign = new Campaign();
      campaign.name = "Interplanetary Cruise #" + new TestUtils().GetTimeStamp();
      campaign.status = CampaignStatus.PAUSED;
      campaign.biddingStrategyConfiguration = new BiddingStrategyConfiguration();
      campaign.biddingStrategyConfiguration.biddingStrategyType = BiddingStrategyType.MANUAL_CPC;

      Budget budget = new Budget();
      budget.budgetId = budgetId;

      campaign.budget = budget;

      campaign.advertisingChannelType = AdvertisingChannelType.SEARCH;

      // Set the campaign network options to GoogleSearch and SearchNetwork
      // only. Set ContentNetwork, PartnerSearchNetwork and ContentContextual
      // to false.
      campaign.networkSetting = new NetworkSetting() {
        targetGoogleSearch = true,
        targetSearchNetwork = true,
        targetContentNetwork = false,
        targetPartnerSearchNetwork = false
      };

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
      operation.@operator = Operator.ADD;
      operation.operand = campaign;

      CampaignReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignService.mutate((new CampaignOperation[] { operation }));
      });

      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.AreEqual(retVal.value[0].name, campaign.name);
      Assert.AreNotEqual(retVal.value[0].id, 0);
      Assert.True((campaignService as MockCampaignServiceEx).MutateCalled);
    }

    /// <summary>
    /// Tests whether a request can work end-to-end.
    /// </summary>
    [Test]
    public void TestUpdateCampaignsMockRequestAndResponse() {
      ExamplesMockData mockData = LoadMockData(SoapMessages_v201603.UpdateCampaign);
      RunMockedExample(mockData, delegate() {
        new CSharpExamples.UpdateCampaign().Run(user, 12345);
      }, new WebRequestInterceptor.OnBeforeSendResponse(VerifyUpdateCampaignRequest));
    }

    /// <summary>
    /// Verifies whether UpdateCampaign example is serializing the request
    /// correctly.
    /// </summary>
    /// <param name="requestUri">The request URI.</param>
    /// <param name="headers">The headers.</param>
    /// <param name="body">The body.</param>
    private void VerifyUpdateCampaignRequest(Uri requestUri, WebHeaderCollection headers,
        string body) {
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(body);
      XmlElement node = (XmlElement) doc.GetElementsByTagName("operations")[0];
      CampaignOperation campaignOperation =
          (CampaignOperation) SerializationUtilities.DeserializeFromXmlTextCustomRootNs(
              node.OuterXml, typeof(CampaignOperation),
              "https://adwords.google.com/api/adwords/cm/v201603", "operations");

      Assert.AreEqual(campaignOperation.@operator, Operator.SET);
      Campaign campaign = campaignOperation.operand;
      Assert.AreEqual(campaign.id, 12345);
      Assert.AreEqual(campaign.status, CampaignStatus.PAUSED);
    }
  }
}
