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
using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Tests.Mocks;
using NUnit.Framework;

using System;
using System.Net;

namespace Google.Api.Ads.Common.Tests.Logging {

  /// <summary>
  /// Tests for TraceListener class.
  /// </summary>
  [TestFixture]
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
    /// The name of the service for testing purposes.
    /// </summary>
    readonly string TestServiceName = "TestService";

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

      WebHeaderCollection requestHeaders = new WebHeaderCollection();
      requestHeaders["TestRequestKey"] = "TestRequestValue";
      RequestInfo request = new RequestInfo() {
        Body = Resources.XmlRequest,
        Headers = requestHeaders,
        Uri = new Uri("https://localhost"),
        HttpMethod = "POST",
        Service = TestServiceName
      };

      WebHeaderCollection responseHeaders = new WebHeaderCollection();
      responseHeaders["TestResponseKey"] = "TestResponseValue";
      ResponseInfo response = new ResponseInfo() {
        Body = Resources.XmlResponse,
        Headers = responseHeaders,
        StatusCode = HttpStatusCode.OK
      };

      listener.HandleMessage(request, response);
      string expected = Resources.SoapLog.Replace("\r\n", "\n").Trim();
      string actual = (ContextStore.GetValue("FormattedSoapLog").ToString())
          .Replace("\r\n", "\n").Trim();
      Assert.AreEqual(expected, actual);

      expected = Resources.ResponseLog.Replace("\r\n", "\n").Trim();
      actual = (ContextStore.GetValue("FormattedRequestLog").ToString())
          .Replace("\r\n", "\n").Trim();
      Assert.AreEqual(expected, actual);
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
