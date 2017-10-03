// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Google.Api.Ads.Common.Tests.Util {

  /// <summary>
  /// UnitTests for <see cref="LogEntry"/> class.
  /// </summary>
  [TestFixture]
  public class TraceHelperTests {
    /// <summary>
    /// The mock date and time provider.
    /// </summary>
    private readonly DateTimeProvider DATE_PROVIDER = new MockDateTimeProvider();

    /// <summary>
    /// The mock application configuration.
    /// </summary>
    private readonly AppConfig CONFIG = new MockAppConfig();

    /// <summary>
    /// The <see cref="LogEntry"/> instance for running tests.
    /// </summary>
    private readonly LogEntry logEntry;

    /// <summary>
    /// The SOAP trace formatter.
    /// </summary>
    private readonly SoapTraceFormatter SOAP_FORMATTER = new SoapTraceFormatter();

    /// <summary>
    /// An HTTP request for testing purposes.
    /// </summary>
    WebRequest testRequest;

    /// <summary>
    /// An HTTP response for testing purposes.
    /// </summary>
    WebResponse testResponse;

    /// <summary>
    /// The temporary file that stores the SOAP request and response.
    /// </summary>
    private string tempFile;

    /// <summary>
    /// The keys to mask in the request XML.
    /// </summary>
    private ISet<string> KEYS = new HashSet<string>() { "KEY1", "KEY2" };

    /// <summary>
    /// The template string for logged requests.
    /// </summary>
    private const string LOGGED_REQUEST = @"
-----------------BEGIN API CALL---------------------

Request
-------

GET {0}
TimeStamp: {1}


{2}";

    /// <summary>
    /// The template string for logged responses.
    /// </summary>
    private const string LOGGED_RESPONSE = @"

Response
--------

Content-Length: {0}
Content-Type: application/octet-stream
TimeStamp: {1}


{2}
-----------------END API CALL-----------------------";

    /// <summary>
    /// Initializes a new instance of the <see cref="TraceHelperTests"/> class.
    /// </summary>
    public TraceHelperTests() {
      logEntry = new LogEntry(CONFIG, DATE_PROVIDER);
    }

    /// <summary>
    /// Creates the test file for writing the SOAP request and response.
    /// </summary>
    /// <param name="path">The temporary path for creating the file.</param>
    private void CreateTestFile(string path) {
      FileStream fs = File.OpenWrite(path);
      using (StreamWriter writer = new StreamWriter(fs)) {
        writer.Write(Resources.SoapRequest);
      }
      fs.Close();
    }

    /// <summary>
    /// Normalize line endings to the current Environment.NewLine.
    /// </summary>
    /// <param name="str">The string to normalize.</param>
    private string NormalizeNewLines(string str) {
      return str.Replace("\r\n", "\n").Replace("\n", System.Environment.NewLine);
    }

    /// <summary>
    /// Initializes this test instance.
    /// </summary>
    [SetUp]
    public void Init() {
      tempFile = Path.GetTempFileName();
      CreateTestFile(tempFile);
      testRequest = HttpWebRequest.Create(tempFile);
      testResponse = testRequest.GetResponse();
    }

    /// <summary>
    /// Cleanups this test instance.
    /// </summary>
    [TearDown]
    public void Cleanup() {
      try {
        File.Delete(tempFile);
      } catch {
      }
    }

    /// <summary>
    /// Test for TraceHelper.LogRequest method with masking turned on.
    /// </summary>
    [Test]
    public void TestLogRequestWithMasking() {
      logEntry.LogRequest(new RequestInfo(testRequest, Resources.SoapRequest), KEYS,
          SOAP_FORMATTER);
      string log = NormalizeNewLines(logEntry.DetailedRequestLog.Trim());

      string maskedMessage = SOAP_FORMATTER.MaskContents(Resources.SoapRequest, KEYS).Trim();
      string expectedMessage = NormalizeNewLines(string.Format(LOGGED_REQUEST,
          testRequest.RequestUri.AbsolutePath,
          DATE_PROVIDER.Now.ToString("R"), maskedMessage).Trim());
      Assert.AreEqual(expectedMessage, log);
    }

    /// <summary>
    /// Test for TraceHelper.LogResponse method with masking turned on.
    /// </summary>
    [Test]
    public void TestLogResponseWithMasking() {
      logEntry.LogRequest(new RequestInfo(testRequest, Resources.SoapRequest), KEYS,
          SOAP_FORMATTER);
      string log = NormalizeNewLines(logEntry.DetailedRequestLog.Trim());

      string maskedMessage = SOAP_FORMATTER.MaskContents(Resources.SoapRequest, KEYS).Trim();
      string expectedMessage = NormalizeNewLines(string.Format(LOGGED_REQUEST, 
          testRequest.RequestUri.AbsolutePath,
          DATE_PROVIDER.Now.ToString("R"), maskedMessage).Trim());

      Assert.AreEqual(expectedMessage, log);
    }
  }
}
