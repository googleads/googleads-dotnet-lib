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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v201902;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.AdManager.Tests
{
    /// <summary>
    /// Tests the SoapFaultInspector with AdManagerApiExceptions and DFP SOAP Faults.
    /// </summary>
    [TestFixture]
    public class AdManagerSoapFaultInspectorTests
    {
        /// <summary>
        /// The service to test faults with.
        /// </summary>
        IClientChannel channel;

        const string fault_xml =
            @"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Header>
    <ResponseHeader xmlns=""https://www.google.com/apis/ads/publisher/v201902"">
      <requestId>1234567890</requestId>
      <responseTime>123</responseTime>
    </ResponseHeader>
  </soap:Header>
  <soap:Body>
    <soap:Fault>
      <faultcode>soap:Server</faultcode>
      <faultstring>[PublisherQueryLanguageContextError.UNEXECUTABLE]</faultstring>
      <detail>
        <ApiExceptionFault xmlns=""https://www.google.com/apis/ads/publisher/v201902"">
          <message>[PublisherQueryLanguageContextError.UNEXECUTABLE]</message>
          <errors xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
              xsi:type=""PublisherQueryLanguageContextError"">
            <fieldPath>Mapping requested for unknown identifer</fieldPath>
            <fieldPathElements>
              <field>Mapping requested for unknown identifer</field>
            </fieldPathElements>
            <trigger>
            </trigger>
            <errorString>PublisherQueryLanguageContextError.UNEXECUTABLE</errorString>
            <reason>UNEXECUTABLE</reason>
          </errors>
        </ApiExceptionFault>
      </detail>
    </soap:Fault>
  </soap:Body>
</soap:Envelope>";

        readonly MessageVersion TestMessageVersion =
            MessageVersion.CreateVersion(EnvelopeVersion.Soap11);

        /// <summary>
        /// Initialize this test class instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            EndpointAddress endpoint = new EndpointAddress("http://www.google.com");
            BasicHttpBinding b = new BasicHttpBinding();
            this.channel = new MockAdsService(b, endpoint).InnerChannel;
        }

        /// <summary>
        /// Tests that no exception is thrown for a non-fault reply.
        /// </summary>
        [Test]
        public void TestNoExceptionForNoFault()
        {
            SoapFaultInspector<AdManagerApiException> inspector =
                new SoapFaultInspector<AdManagerApiException>()
                {
                    ErrorType = typeof(ApiException)
                };
            Message message = Message.CreateMessage(TestMessageVersion, null);
            Assert.DoesNotThrow(
                delegate() { inspector.AfterReceiveReply(ref message, this.channel); },
                "Exception was thrown for a response that wasn't a fault.");
        }

        /// <summary>
        /// Tests that a AdManagerApiException is thrown for a fault.
        /// </summary>
        [Test]
        public void TestAdManagerApiExceptionForFault()
        {
            SoapFaultInspector<AdManagerApiException> inspector =
                new SoapFaultInspector<AdManagerApiException>()
                {
                    ErrorType = typeof(AdManager.v201902.ApiException)
                };

            XmlDocument xDoc = XmlUtilities.CreateDocument(fault_xml);
            Message message = Message.CreateMessage(new XmlNodeReader(xDoc), Int32.MaxValue,
                TestMessageVersion);

            AdManagerApiException exception = Assert.Throws<AdManagerApiException>(
                delegate() { inspector.AfterReceiveReply(ref message, this.channel); },
                "No exception was thrown for a SOAP Fault response");
            Assert.AreEqual(typeof(ApiException), exception.ApiException.GetType());
            ApiException apiException = (ApiException) exception.ApiException;
            Assert.AreEqual(1, apiException.errors.Length);
            Assert.AreEqual(typeof(PublisherQueryLanguageContextError),
                apiException.errors[0].GetType());
            PublisherQueryLanguageContextError error =
                (PublisherQueryLanguageContextError) apiException.errors[0];
            Assert.AreEqual(PublisherQueryLanguageContextErrorReason.UNEXECUTABLE, error.reason);
            Assert.AreEqual("Mapping requested for unknown identifer", error.fieldPath);
        }
    }
}
