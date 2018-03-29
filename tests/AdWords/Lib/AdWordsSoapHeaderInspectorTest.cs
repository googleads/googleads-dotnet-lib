// Copyright 2017, Google Inc. All Rights Reserved.
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

using System;
using NUnit.Framework;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Xml;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Headers;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.AdWords.Tests.Lib {
  /// <summary>
  /// AdWords SOAP header inspector tests.
  /// </summary>
  [TestFixture]
  public class AdWordsSoapHeaderInspectorTest {

    /// <summary>
    /// The service to test applying headers to.
    /// </summary>
    IClientChannel channel;

    /// <summary>
    /// The request message to test applying headers to.
    /// </summary>
    Message request;

    /// <summary>
    /// The response message to test reading headers from.
    /// </summary>
    Message response;

    /// <summary>
    /// The test message version.
    /// </summary>
    readonly MessageVersion TestMessageVersion =
        MessageVersion.CreateVersion(EnvelopeVersion.Soap11);

    /// <summary>
    /// Initialize this test class instance.
    /// </summary>
    [SetUp]
    public void Init() {
      EndpointAddress endpoint = new EndpointAddress("http://www.google.com");
      BasicHttpBinding binding = new BasicHttpBinding();
      this.channel = new MockAdsService(binding, endpoint).InnerChannel;
      this.request = Message.CreateMessage(TestMessageVersion, "", "request body");
      this.response = Message.CreateMessage(TestMessageVersion, "", "response body");
    }

    /// <summary>
    /// Tests that setting a null header throws.
    /// </summary>
    [Test]
    public void TestNullHeaderInvalid() {
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector() {
        User = new AdWordsUser(),
        RequestHeader = null
      };

      Assert.Throws(typeof(ArgumentNullException), delegate () {
        inspector.BeforeSendRequest(ref this.request, this.channel);
      }, "No exception was thrown for a null header");
    }

    /// <summary>
    /// Tests that the inspector requires a Config file.
    /// </summary>
    [Test]
    public void TestNullUserInvalid() {
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector() {
        User = null,
        RequestHeader = new RequestHeader() {
          developerToken = "ABCDEF"
        }
      };

      Assert.Throws(typeof(ArgumentNullException), delegate () {
        inspector.BeforeSendRequest(ref this.request, this.channel);
      }, "No exception was thrown for a null Config");
    }

    /// <summary>
    /// Tests that the inspector requires a developer token.
    /// </summary>
    [Test]
    public void TestDeveloperTokenRequired() {
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector() {
        User = new AdWordsUser(),
        RequestHeader = new RequestHeader() {
          developerToken = null
        }
      };

      Assert.Throws(typeof(ArgumentNullException), delegate () {
        inspector.BeforeSendRequest(ref this.request, this.channel);
      }, "No exception was thrown for an null developer token");

      inspector.RequestHeader.developerToken = "";
      Assert.Throws(typeof(ArgumentNullException), delegate () {
        inspector.BeforeSendRequest(ref this.request, this.channel);
      }, "No exception was thrown for an empty developer token");
    }

    /// <summary>
    /// Tests that a valid header is applied.
    /// </summary>
    [Test]
    public void TestValidHeaderApplied() {
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector() {
        User = new AdWordsUser(),
      };
      RequestHeader header = new RequestHeader() {
        clientCustomerId = "123",
        validateOnly = true,
        developerToken = "ABCDEF",
        partialFailure = true
      };

      inspector.RequestHeader = (RequestHeader)header.Clone();
      inspector.BeforeSendRequest(ref this.request, this.channel);
      Assert.AreEqual(1, this.request.Headers.Count);
      foreach(RequestHeader appliedHeader in request.Headers) {
        Assert.AreEqual(header.clientCustomerId, appliedHeader.clientCustomerId);
        Assert.AreEqual(header.validateOnly, appliedHeader.validateOnly);
        Assert.AreEqual(header.developerToken, appliedHeader.developerToken);
        Assert.AreEqual(header.partialFailure, appliedHeader.partialFailure);
        Assert.AreEqual(inspector.User.Config.GetUserAgent(), appliedHeader.userAgent);
      }
    }

    /// <summary>
    /// Tests that updates to the RequestHeader in a AdWordsSoapService are applied in the request.
    /// </summary>
    [Test]
    public void TestHeaderUpdatesApplied() {
      AdsServiceInspectorBehavior behavior = new AdsServiceInspectorBehavior();
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector();
      behavior.Add(inspector);

      AdWordsSoapClient<IMockAdsService> service = new AdWordsSoapClient<IMockAdsService>(
          new BasicHttpBinding(), new EndpointAddress("https://www.google.com"));
#if NET452
      service.Endpoint.Behaviors.Add(behavior);
#else
      service.Endpoint.EndpointBehaviors.Add(behavior);
#endif
      Assert.IsNull(service.RequestHeader);
      RequestHeader expected = new RequestHeader() {
        userAgent = "Google Test",
        clientCustomerId = "12345"
      };
      service.RequestHeader = expected;
      Assert.AreEqual(expected, inspector.RequestHeader);

      // Test removing a customer ID
      expected.clientCustomerId = null;
      Assert.AreEqual(expected, inspector.RequestHeader);
    }

    /// <summary>
    /// Tests that ClientCustomerId is not serialized when null or empty.
    /// </summary>
    [Test]
    public void TestClientCustomerIdOptional() {
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector() {
        User = new AdWordsUser(),
        RequestHeader = new RequestHeader() {
          developerToken = "ABCDEF"
        }
      };

      inspector.BeforeSendRequest(ref this.request, this.channel);
      Assert.AreEqual(1, request.Headers.Count);
      Assert.That(!request.Headers[0].ToString().Contains("<clientCustomerId>"));

      inspector.RequestHeader.clientCustomerId = "";
      inspector.BeforeSendRequest(ref this.request, this.channel);
      Assert.AreEqual(1, request.Headers.Count);
      Assert.That(!request.Headers[0].ToString().Contains("<clientCustomerId>"));
    }

    /// <summary>
    /// Tests that serialized boolean values are lowercased.
    /// </summary>
    [Test]
    public void TestBooleanIsLowerCase() {
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector() {
        User = new AdWordsUser(),
        RequestHeader = new RequestHeader() {
          developerToken = "ABCDEF",
          validateOnly = true,
          partialFailure =  true
        }
      };

      inspector.BeforeSendRequest(ref this.request, this.channel);
      Assert.AreEqual(1, request.Headers.Count);
      Assert.That(request.Headers[0].ToString().Contains("<validateOnly>true</validateOnly>"));
      Assert.That(request.Headers[0].ToString().Contains("<partialFailure>true</partialFailure>"));
    }

    /// <summary>
    /// Tests that an ApiCallEntry is added to the user.
    /// </summary>
    [Test]
    public void TestCallEntryAddedToUser() {
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector() {
        User = new AdWordsUser(),
      };

      ResponseHeader responseHeader = new ResponseHeader() {
        operations = 10,
        responseTime = 1234,
        serviceName = "CampaignService",
        methodName = "mutate"
      };
      response.Headers.Clear();
      response.Headers.Add(MessageHeader.CreateHeader(
          "ResponseHeader", 
          ResponseHeader.PLACEHOLDER_NAMESPACE,
          responseHeader));
      inspector.AfterReceiveReply(ref this.response, null);

      ApiCallEntry[] entries = inspector.User.GetCallDetails();
      Assert.AreEqual(1, entries.Length);
      ApiCallEntry entry = entries[0];
      Assert.AreEqual(10, entry.OperationCount);
      Assert.AreEqual("mutate", entry.Method);
      Assert.AreEqual("CampaignService", entry.Service);
    }

    /// <summary>
    /// Tests that a call entry is correctly extracted from a complete SOAP response.
    /// </summary>
    [Test]
    public void TestCallEntryExtractedFromXml() {
      AdWordsUser user = new AdWordsUser();
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector() {
        User = user
      };
      XmlDocument xDoc = XmlUtilities.CreateDocument(SoapMessages_v201802.UpdateCampaign);
      XmlElement xResponse = (XmlElement)xDoc.SelectSingleNode("/Example/SOAP/Response");
      xDoc.LoadXml(xResponse.InnerText);
      this.response =
          Message.CreateMessage(new XmlNodeReader(xDoc), Int32.MaxValue, TestMessageVersion);

      inspector.AfterReceiveReply(ref this.response, null);

      // API no longer returns operation count.
      Assert.AreEqual(user.GetTotalOperationCount(), 0);
      Assert.AreEqual(user.GetOperationCountForLastCall(), 0);

      ApiCallEntry[] callEntries = user.GetCallDetails();
      Assert.AreEqual(callEntries.Length, 1);
      ApiCallEntry callEntry = user.GetCallDetails()[0];

      Assert.AreEqual(0, callEntry.OperationCount);
      Assert.AreEqual("mutate", callEntry.Method);
      Assert.AreEqual("CampaignService", callEntry.Service);
    }


    /// <summary>
    /// Tests that the Message state is valid and can be read after the inspector
    /// is applied.
    /// </summary>
    [Test]
    public void TestMessageStateIsValid() {
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector() {
        User = new AdWordsUser(),
      };
      RequestHeader requestHeader = new RequestHeader() {
        clientCustomerId = "123",
        validateOnly = true,
        developerToken = "ABCDEF"
      };
      inspector.RequestHeader = (RequestHeader)requestHeader.Clone();

      ResponseHeader responseHeader = new ResponseHeader() {
        operations = 10,
        responseTime = 1000,
      };
      response.Headers.Clear();
      response.Headers.Add(MessageHeader.CreateHeader(
          "ResponseHeader",
          ResponseHeader.PLACEHOLDER_NAMESPACE,
          responseHeader));

      inspector.BeforeSendRequest(ref this.request, this.channel);
      inspector.AfterReceiveReply(ref this.response, null);

      Assert.AreEqual(MessageState.Created, request.State);
      Assert.AreEqual(MessageState.Created, response.State);
    }

    /// <summary>
    /// Tests that a response with no header does not cause an exception.
    /// </summary>
    [Test]
    public void TestEmptyResponseHeader() {
      AdWordsSoapHeaderInspector inspector = new AdWordsSoapHeaderInspector();
      this.response.Headers.Clear();
      Assert.DoesNotThrow(() => inspector.AfterReceiveReply(ref this.response, null));
    }
  }
}
