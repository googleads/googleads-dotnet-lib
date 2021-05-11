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
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;
using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;

namespace Google.Api.Ads.AdWords.Tests
{
    /// <summary>
    /// Tests the SoapFaultInspector with AdWordsApiExceptions and AdWords SOAP Faults.
    /// </summary>
    [TestFixture]
    public class AdWordsSoapFaultInspectorTests
    {
        /// <summary>
        /// The service to test faults with.
        /// </summary>
        private IClientChannel channel;

        private readonly string[] faultXmls = new string[]
        {
            // ApiExceptionFault has a namespace prefix, and not in the default namespace.
            @"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Header>
    <ns2:ResponseHeader xmlns:ns2=""https://adwords.google.com/api/adwords/mcm/v201809""
        xmlns=""https://adwords.google.com/api/adwords/cm/v201809"">
      <requestId>123</requestId>
      <serviceName>CustomerService</serviceName>
      <methodName>getCustomers</methodName>
      <operations>0</operations>
      <responseTime>106</responseTime>
    </ns2:ResponseHeader>
  </soap:Header>
  <soap:Body>
    <soap:Fault>
      <faultcode>soap:Server</faultcode>
      <faultstring>fault string 1</faultstring>
      <detail>
        <ns2:ApiExceptionFault xmlns=""https://adwords.google.com/api/adwords/cm/v201809""
            xmlns:ns2=""https://adwords.google.com/api/adwords/mcm/v201809"">
          <message>test message1</message>
          <ApplicationException.Type>ApiException</ApplicationException.Type>
          <errors xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
              xsi:type=""AuthenticationError"">
            <fieldPath>
            </fieldPath>
            <trigger>&lt;null&gt;</trigger>
            <errorString>AuthenticationError.CUSTOMER_NOT_FOUND</errorString>
            <ApiError.Type>AuthenticationError</ApiError.Type>
            <reason>CUSTOMER_NOT_FOUND</reason>
          </errors>
        </ns2:ApiExceptionFault>
      </detail>
    </soap:Fault>
  </soap:Body>
</soap:Envelope>",

            // ApiExceptionFault doesn't have a namespace prefix.
            @"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Header>
    <ResponseHeader xmlns:ns2=""https://adwords.google.com/api/adwords/cm/v201809""
        xmlns=""https://adwords.google.com/api/adwords/mcm/v201809"">
      <ns2:requestId>456</ns2:requestId>
      <ns2:serviceName>CustomerService</ns2:serviceName>
      <ns2:methodName>getCustomers</ns2:methodName>
      <ns2:operations>0</ns2:operations>
      <ns2:responseTime>47</ns2:responseTime>
    </ResponseHeader>
  </soap:Header>
  <soap:Body>
    <soap:Fault>
      <faultcode>soap:Server</faultcode>
      <faultstring>fault string 2</faultstring>
      <detail>
        <ApiExceptionFault xmlns=""https://adwords.google.com/api/adwords/mcm/v201809""
            xmlns:ns2=""https://adwords.google.com/api/adwords/cm/v201809"">
          <ns2:message>test message 2</ns2:message>
          <ns2:ApplicationException.Type>ApiException</ns2:ApplicationException.Type>
          <ns2:errors xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
              xsi:type=""ns2:AuthenticationError"">
            <ns2:fieldPath>
            </ns2:fieldPath>
            <ns2:trigger>&lt;null&gt;</ns2:trigger>
            <ns2:errorString>AuthenticationError.CUSTOMER_NOT_FOUND</ns2:errorString>
            <ns2:ApiError.Type>AuthenticationError</ns2:ApiError.Type>
            <ns2:reason>CUSTOMER_NOT_FOUND</ns2:reason>
          </ns2:errors>
        </ApiExceptionFault>
      </detail>
    </soap:Fault>
  </soap:Body>
</soap:Envelope>"
        };

        private readonly MessageVersion TestMessageVersion =
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
            SoapFaultInspector<AdWordsApiException> inspector =
                new SoapFaultInspector<AdWordsApiException>()
                {
                    ErrorType = typeof(ApiException)
                };
            Message message = Message.CreateMessage(TestMessageVersion, null);
            Assert.DoesNotThrow(
                delegate() { inspector.AfterReceiveReply(ref message, this.channel); },
                "Exception was thrown for a response that wasn't a fault.");
        }

        /// <summary>
        /// Tests that an AdWordsApiException is thrown for a fault.
        /// </summary>
        [Test]
        public void TestAdWordsApiExceptionForFault()
        {
            SoapFaultInspector<AdWordsApiException> inspector =
                new SoapFaultInspector<AdWordsApiException>()
                {
                    ErrorType = typeof(ApiException)
                };

            foreach (string faultXml in faultXmls)
            {
                XmlDocument xDoc = XmlUtilities.CreateDocument(faultXml);
                Message message = Message.CreateMessage(new XmlNodeReader(xDoc), Int32.MaxValue,
                    TestMessageVersion);

                AdWordsApiException exception = Assert.Throws<AdWordsApiException>(
                    delegate() { inspector.AfterReceiveReply(ref message, this.channel); },
                    "No exception was thrown for a SOAP Fault response");
                Assert.AreEqual(typeof(ApiException), exception.ApiException.GetType());
                ApiException apiException = (ApiException) exception.ApiException;
                Assert.AreEqual(1, apiException.errors.Length);
                Assert.AreEqual(typeof(AuthenticationError), apiException.errors[0].GetType());
                AuthenticationError error = (AuthenticationError) apiException.errors[0];
                Assert.AreEqual(AuthenticationErrorReason.CUSTOMER_NOT_FOUND, error.reason);
            }
        }
    }
}
