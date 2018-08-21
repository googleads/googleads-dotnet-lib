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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Tests.Mocks;

using NUnit.Framework;

using System;
using System.Net;

namespace Google.Api.Ads.AdWords.Tests.Lib
{
    /// <summary>
    /// Tests for AdWordsTraceListener class.
    /// </summary>
    [TestFixture]
    internal class AdWordsTraceListenerTests
    {
        /// <summary>
        /// The SoapeListener instance for testing this class.
        /// </summary>
        private SoapListener listener;

        /// <summary>
        /// The name of the service for testing purposes.
        /// </summary>
        private readonly string TestServiceName = "TestService";

        /// <summary>
        /// The client customer ID for testing purposes.
        /// </summary>
        private readonly string TestCustomerId = "TEST_CLIENT_CUSTOMER_ID";

        /// <summary>
        /// Initializes the test case.
        /// </summary>
        [SetUp]
        public void Init()
        {
            listener = AdWordsTraceListener.Instance;

            // Set static values for testing.
            AdWordsAppConfig config = (AdWordsAppConfig) listener.Config;
            config.GetType().GetProperty("ClientCustomerId").SetValue(config, TestCustomerId, null);
            config.GetType().GetProperty("MaskCredentials").SetValue(config, true, null);

            ((TraceListener) listener).DateTimeProvider = new MockDateTimeProvider();
        }

        /// <summary>
        /// Tears down the test case.
        /// </summary>
        [TearDown]
        public void Dispose()
        {
            listener.CleanupAfterCall();
        }

        /// <summary>
        /// Tests handling of successful requests.
        /// </summary>
        [Test]
        [Category("Logging")]
        public void TestHandleMessageSuccess()
        {
            this.TestResponseSummary(Resources.ExampleRequest, Resources.ExampleResponseSuccess,
                Resources.ExampleResponseSummarySuccess, Resources.ExampleResponseDetailsSuccess);
        }

        /// <summary>
        /// Tests the handling of unsuccessful requests.
        /// </summary>
        [Test]
        [Category("Logging")]
        public void TestHandleMessageFailure()
        {
            this.TestResponseSummary(Resources.ExampleRequest, Resources.ExampleResponseFailure,
                Resources.ExampleResponseSummaryFailure, Resources.ExampleResponseDetailsFailure);
        }

        private void TestResponseSummary(string requestBody, string responseBody,
            string expectedSummary, string expectedDetails)
        {
            WebHeaderCollection requestHeaders = new WebHeaderCollection();
            requestHeaders["TestRequestKey"] = "TestRequestValue";
            RequestInfo request = new RequestInfo()
            {
                Body = requestBody,
                Headers = requestHeaders,
                Uri = new Uri("https://localhost"),
                HttpMethod = "POST",
                Service = TestServiceName
            };

            WebHeaderCollection responseHeaders = new WebHeaderCollection();
            responseHeaders["TestResponseKey"] = "TestResponseValue";
            ResponseInfo response = new ResponseInfo()
            {
                Body = responseBody,
                Headers = responseHeaders,
                StatusCode = HttpStatusCode.OK
            };

            listener.HandleMessage(request, response);

            string expected = expectedSummary.Replace("\r\n", "\n").Trim();
            string actual = (ContextStore.GetValue("FormattedRequestLog").ToString())
                .Replace("\r\n", "\n").Trim();
            Assert.AreEqual(expected, actual);

            expected = expectedDetails.Replace("\r\n", "\n").Trim();
            actual = (ContextStore.GetValue("FormattedSoapLog").ToString()).Replace("\r\n", "\n")
                .Trim();
            Assert.AreEqual(expected, actual);
        }
    }
}
