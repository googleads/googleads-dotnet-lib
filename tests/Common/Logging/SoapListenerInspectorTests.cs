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
using System.ServiceModel;
using System.ServiceModel.Channels;
using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Tests.Mocks;
using NUnit.Framework;

namespace Google.Api.Ads.Common.Tests.Logging {

  /// <summary>
  /// UnitTests for <see cref="SoapListenerInspector"/> class.
  /// </summary>
  [TestFixture]
  public class SoapListenerInspectorTests  {
    /// <summary>
    /// The service to test applying headers to.
    /// </summary>
    IClientChannel channel;

    /// <summary>
    /// The request Message for testing.
    /// </summary>
    private Message request;

    /// <summary>
    /// The response Message for testing.
    /// </summary>
    private Message response;

    /// <summary>
    /// The test message version.
    /// </summary>
    readonly MessageVersion TestMessageVersion =
        MessageVersion.CreateVersion(EnvelopeVersion.Soap11);

    /// <summary>
    /// The name of the service for testing purposes.
    /// </summary>
    const string TestServiceName = "TestService";

    static readonly string ExpectedRequestXml = string.Format(
        "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">{0}" +
        "  <s:Body>{0}" +
        "    <string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/\">request body</string>{0}" +
        "  </s:Body>{0}" +
        "</s:Envelope>", Environment.NewLine);

    static readonly string ExpectedResponseXml = string.Format(
        "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">{0}" +
        "  <s:Body>{0}" +
        "    <string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/\">response body</string>{0}" +
        "  </s:Body>{0}" +
        "</s:Envelope>", Environment.NewLine);

    /// <summary>
    /// Initialize this test class instance.
    /// </summary>
    [SetUp]
    public void Init() {
      EndpointAddress endpoint = new EndpointAddress("http://www.google.com");
      BasicHttpBinding binding = new BasicHttpBinding();
      this.channel = new MockAdsService(binding, endpoint).InnerChannel;
      this.request = Message.CreateMessage(TestMessageVersion, "", "request body");
      this.request.Headers.Clear();
      this.response = Message.CreateMessage(TestMessageVersion, "", "response body");
      this.response.Headers.Clear();
    }

    /// <summary>
    /// Test that the message state is valid and can be read
    /// after the inspector is applied.
    /// </summary>
    [Test]
    public void TestMessageIsValidState() {
      SoapListenerInspector inspector = new SoapListenerInspector(
        new MockAdsUser(new MockAppConfig()), TestServiceName);
      inspector.BeforeSendRequest(ref request, channel);
      inspector.AfterReceiveReply(ref response, channel);
      Assert.AreEqual(MessageState.Created, request.State);
      Assert.AreEqual(MessageState.Created, response.State);
    }

    /// <summary>
    /// Test that the correct RequestInfo is logged.
    /// </summary>
    [Test]
    public void TestCorrectRequestInfo() {
      MockAdsUser user = new MockAdsUser(new MockAppConfig());
      MockTraceListener listener = new MockTraceListener(user.Config);
      user.Listeners.Add(listener);
      SoapListenerInspector inspector = new SoapListenerInspector(user, TestServiceName);

      HttpRequestMessageProperty requestProperties = new HttpRequestMessageProperty();
      requestProperties.Headers.Add("Authorization", "Bearer 1234");
      requestProperties.Method = "POST";
      request.Properties.Add(HttpRequestMessageProperty.Name, requestProperties);

      inspector.BeforeSendRequest(ref request, channel);
      inspector.AfterReceiveReply(ref response, channel);

      Assert.AreEqual(requestProperties.Headers, listener.LastRequestInfo.Headers);
      Assert.AreEqual(requestProperties.Method, listener.LastRequestInfo.HttpMethod);
      Assert.AreEqual(ExpectedRequestXml, listener.LastRequestInfo.Body);
      Assert.AreEqual(TestServiceName, listener.LastRequestInfo.Service);
    }

    /// <summary>
    /// Test that the correct ResponseInfo is logged.
    /// </summary>
    [Test]
    public void TestCorrectResponseInfo() {
      MockAdsUser user = new MockAdsUser(new MockAppConfig());
      MockTraceListener listener = (MockTraceListener)user.Listeners[0];
      SoapListenerInspector inspector = new SoapListenerInspector(user, TestServiceName);

      HttpResponseMessageProperty responseProperties = new HttpResponseMessageProperty();
      responseProperties.Headers.Add("X-TestHeader", "Hello World");
      responseProperties.StatusCode = System.Net.HttpStatusCode.OK;
      response.Properties.Add(HttpResponseMessageProperty.Name, responseProperties);

      inspector.BeforeSendRequest(ref request, channel);
      inspector.AfterReceiveReply(ref response, channel);

      Assert.AreEqual(responseProperties.Headers, listener.LastResponseInfo.Headers);
      Assert.AreEqual(responseProperties.StatusCode, listener.LastResponseInfo.StatusCode);
      Assert.AreEqual(ExpectedResponseXml, listener.LastResponseInfo.Body);
    }

    [Test]
    public void TestInvalidXmlDoesNotThrow() {
      MockAdsUser user = new MockAdsUser(new MockAppConfig());
      MockTraceListener listener = (MockTraceListener)user.Listeners[0];
      SoapListenerInspector inspector = new SoapListenerInspector(user, TestServiceName);

      Message invalidXmlRequest = Message.CreateMessage(TestMessageVersion, "", "\u0003");
      Assert.DoesNotThrow(() => inspector.BeforeSendRequest(ref invalidXmlRequest, channel));
      Assert.DoesNotThrow(() => inspector.AfterReceiveReply(ref response, channel));
    }
  }
}