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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;

using NUnit.Framework;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace Google.Api.Ads.Common.Tests.Lib {
  /// <summary>
  /// Tests for TraceListener class.
  /// </summary>
  public class TraceListenerTests {
    /// <summary>
    /// The AppConfig instance for testing this class.
    /// </summary>
    MockAppConfig config = new MockAppConfig();

    /// <summary>
    /// The TraceWriter instance for testing this class.
    /// </summary>
    MockTraceWriter writer = new MockTraceWriter();

    /// <summary>
    /// The TraceListener instance for testing this class.
    /// </summary>
    MockTraceListener listener;

    /// <summary>
    /// The AdsUser instance for testing this class.
    /// </summary>
    MockAdsUser user;

    /// <summary>
    /// The AdsClient instance for testing this class.
    /// </summary>
    MockAdsClient adsClient;

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

      writer.Reset();
    }

    /// <summary>
    /// See if various properties of this class can be set properly.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestProperties() {
      listener = new MockTraceListener(config);
      listener.Writer = writer;
      Assert.AreEqual(writer, listener.Writer);
    }

    /// <summary>
    /// Test the overloaded constructor for TraceListener.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestConstructor() {
      config.SetPropertyFieldForTests("LogToFile", false);
      listener = new MockTraceListener(config);
      Assert.IsNull(listener.Writer);

      config.SetPropertyFieldForTests("LogToFile", true);
      listener = new MockTraceListener(config);
      Assert.IsInstanceOf<DefaultTraceWriter>(listener.Writer);
    }

    /// <summary>
    /// Tests the HandleMessage method.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestHandleMessage() {
      config.SetPropertyFieldForTests("LogToFile", true);
      config.SetPropertyFieldForTests("MaskCredentials", true);
      listener = new MockTraceListener(config);
      listener.Writer = writer;

      XmlDocument xOutgoing = new XmlDocument();
      xOutgoing.LoadXml(Resources.XmlRequest);
      listener.HandleMessage(xOutgoing, adsClient, SoapMessageDirection.OUT);
      Assert.AreEqual(xOutgoing.OuterXml, ContextStore.GetValue("SoapRequest"));

      XmlDocument xIncoming = new XmlDocument();
      xIncoming.LoadXml(Resources.XmlResponse);
      listener.HandleMessage(xIncoming, adsClient, SoapMessageDirection.IN);
      Assert.AreEqual(xIncoming.OuterXml, ContextStore.GetValue("SoapResponse"));
      Assert.AreEqual(Resources.SoapLog, writer.SoapLog);
      Assert.AreEqual(Resources.ResponseLog, writer.RequestLog);
    }

    /// <summary>
    /// Tests the HandleMessage method to see if logging doesn't happen if
    /// the AdsClient instance is null.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestHandleMessageNoClient() {
      config.SetPropertyFieldForTests("LogToFile", true);
      config.SetPropertyFieldForTests("MaskCredentials", true);
      listener = new MockTraceListener(config);
      listener.Writer = writer;

      XmlDocument xOutgoing = new XmlDocument();
      xOutgoing.LoadXml(Resources.XmlRequest);
      listener.HandleMessage(xOutgoing, null, SoapMessageDirection.OUT);
      Assert.AreEqual(xOutgoing.OuterXml, ContextStore.GetValue("SoapRequest"));

      XmlDocument xIncoming = new XmlDocument();
      xIncoming.LoadXml(Resources.XmlResponse);
      listener.HandleMessage(xIncoming, null, SoapMessageDirection.IN);
      Assert.AreEqual(xIncoming.OuterXml, ContextStore.GetValue("SoapResponse"));
      Assert.IsNull(writer.SoapLog);
      Assert.IsNull(writer.RequestLog);
    }

    /// <summary>
    /// Test the GetTimeStamp method.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestGetTimeStamp() {
      listener = new MockTraceListener(config);
      Assert.DoesNotThrow(delegate() {
        listener.GetTimeStampForTest();
      });
    }

    /// <summary>
    /// Tests the CleanupAfterCall method.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestCleanupAfterCall() {
      config.SetPropertyFieldForTests("LogToFile", true);
      listener = new MockTraceListener(config);
      ContextStore.AddKey("SoapRequest", "SoapRequest");
      ContextStore.AddKey("SoapResponse", "SoapResponse");
      listener.CleanupAfterCall();
      Assert.Null(ContextStore.GetValue("SoapRequest"));
      Assert.Null(ContextStore.GetValue("SoapResponse"));
    }
  }
}
