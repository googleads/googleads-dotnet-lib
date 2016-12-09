// Copyright 2012, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;
using Google.Api.Ads.Common.Util;
using NUnit.Framework;

using System;
using System.Xml;

namespace Google.Api.Ads.Common.Tests.Logging {

  /// <summary>
  /// Tests for TraceListener class.
  /// </summary>
  public class TraceListenerTests {

    /// <summary>
    /// The AppConfig instance for testing this class.
    /// </summary>
    private MockAppConfig config = new MockAppConfig();

    /// <summary>
    /// The TraceListener instance for testing this class.
    /// </summary>
    private MockTraceListener listener;

    /// <summary>
    /// The AdsUser instance for testing this class.
    /// </summary>
    private MockAdsUser user;

    /// <summary>
    /// The AdsClient instance for testing this class.
    /// </summary>
    private MockAdsClient adsClient;

    /// <summary>
    /// Initializes the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      user = new MockAdsUser(config);
      adsClient = new MockAdsClient();
      adsClient.User = user;

      MockWebResponse response = new MockWebResponse(null, null);
      response.Headers["TestResponseKey"] = "TestResponseValue";
      adsClient.LastResponse = response;

      MockWebRequest request = new MockWebRequest(response, new Uri("http://localhost"), null,
          false);
      request.Method = "POST";
      request.Headers["TestRequestKey"] = "TestRequestValue";
      adsClient.LastRequest = request;
    }

    /// <summary>
    /// Tears down the test case.
    /// </summary>
    [TearDown]
    public void Dispose() {
      listener.CleanupAfterCall();
    }

    /// <summary>
    /// Tests the HandleMessage method.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestHandleMessage() {
      config.SetPropertyFieldForTests("MaskCredentials", true);
      listener = new MockTraceListener(config);

      XmlDocument xOutgoing = XmlUtilities.CreateDocument(Resources.XmlRequest);
      listener.HandleMessage(xOutgoing, adsClient, SoapMessageDirection.OUT);
      Assert.AreEqual(xOutgoing.OuterXml, ContextStore.GetValue("SoapRequest"));

      XmlDocument xIncoming = XmlUtilities.CreateDocument(Resources.XmlResponse);
      listener.HandleMessage(xIncoming, adsClient, SoapMessageDirection.IN);
      Assert.AreEqual(xIncoming.OuterXml, ContextStore.GetValue("SoapResponse"));
      string expected = Resources.SoapLog.Replace("\r\n", "\n").Trim();
      string actual = ((string) ContextStore.GetValue("FormattedSoapLog")).
          Replace("\r\n", "\n").Trim();
      Assert.AreEqual(expected, actual);
      Assert.AreEqual(Resources.ResponseLog.Replace("\r\n", "\n"),
          ((string) ContextStore.GetValue("FormattedRequestLog")).Replace("\r\n", "\n"));
    }

    /// <summary>
    /// Tests the HandleMessage method to see if logging doesn't happen if
    /// the AdsClient instance is null.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestHandleMessageNoClient() {
      config.SetPropertyFieldForTests("MaskCredentials", true);
      listener = new MockTraceListener(config);

      XmlDocument xOutgoing = XmlUtilities.CreateDocument(Resources.XmlRequest);
      listener.HandleMessage(xOutgoing, null, SoapMessageDirection.OUT);
      Assert.AreEqual(xOutgoing.OuterXml, ContextStore.GetValue("SoapRequest"));

      XmlDocument xIncoming = XmlUtilities.CreateDocument(Resources.XmlResponse);
      listener.HandleMessage(xIncoming, null, SoapMessageDirection.IN);
      Assert.AreEqual(xIncoming.OuterXml, ContextStore.GetValue("SoapResponse"));
      Assert.IsNull(ContextStore.GetValue("FormattedSoapLog"));
      Assert.IsNull(ContextStore.GetValue("FormattedRequestLog"));
    }

    /// <summary>
    /// Tests the CleanupAfterCall method.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestCleanupAfterCall() {
      listener = new MockTraceListener(config);
      ContextStore.AddKey("SoapRequest", "SoapRequest");
      ContextStore.AddKey("SoapResponse", "SoapResponse");
      ContextStore.AddKey("FormattedSoapLog", "FormattedSoapLog");
      ContextStore.AddKey("FormattedRequestLog", "FormattedRequestLog");
      listener.CleanupAfterCall();
      Assert.Null(ContextStore.GetValue("SoapRequest"));
      Assert.Null(ContextStore.GetValue("SoapResponse"));
      Assert.Null(ContextStore.GetValue("FormattedSoapLog"));
      Assert.Null(ContextStore.GetValue("FormattedRequestLog"));
    }
  }
}
