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
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Xml;
using NUnit.Framework;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201705;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.AdWords.Tests {
  /// <summary>
  /// Tests the SoapFaultInspector with AdWordsApiExceptions and AdWords SOAP Faults.
  /// </summary>
  [TestFixture]
  public class AdWordsSoapFaultInspectorTests {
    /// <summary>
    /// The service to test faults with.
    /// </summary>
    IClientChannel channel;

    const string fault_xml =
@"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Header>
        <ResponseHeader xmlns=""https://adwords.google.com/api/adwords/cm/v201705"">
            <requestId>12345</requestId>
            <serviceName>CustomerService</serviceName>
            <methodName>getCustomers</methodName>
            <operations>1</operations>
            <responseTime>123</responseTime>
        </ResponseHeader>
    </soap:Header>
    <soap:Body>
        <soap:Fault>
            <faultcode>soap:Server</faultcode>
            <faultstring>[AuthenticationError.CUSTOMER_NOT_FOUND @ ]</faultstring>
            <detail>
                <ApiExceptionFault xmlns=""https://adwords.google.com/api/adwords/cm/v201705"">
                    <message>[AuthenticationError.CUSTOMER_NOT_FOUND @ ]</message>
                    <ApplicationException.Type>ApiException</ApplicationException.Type>
                    <errors xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""AuthenticationError"">
                        <fieldPath>Test field path content</fieldPath>
                        <trigger/>
                        <errorString>AuthenticationError.CUSTOMER_NOT_FOUND</errorString>
                        <ApiError.Type>AuthenticationError</ApiError.Type>
                        <reason>CUSTOMER_NOT_FOUND</reason>
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
    public void Init() {
      EndpointAddress endpoint = new EndpointAddress("http://www.google.com");
      BasicHttpBinding b = new BasicHttpBinding();
      this.channel = new MockAdsService(b, endpoint).InnerChannel;
    }

    /// <summary>
    /// Tests that no exception is thrown for a non-fault reply.
    /// </summary>
    [Test]
    public void TestNoExceptionForNoFault() {
      SoapFaultInspector<AdWordsApiException> inspector =
          new SoapFaultInspector<AdWordsApiException>() {
        ErrorType = typeof(ApiException)
      };
      Message message = Message.CreateMessage(TestMessageVersion, null);
      Assert.DoesNotThrow(delegate () {
        inspector.AfterReceiveReply(ref message, this.channel);
      }, "Exception was thrown for a response that wasn't a fault.");
    }

    /// <summary>
    /// Tests that an AdWordsApiException is thrown for a fault.
    /// </summary>
    [Test]
    public void TestAdWordsApiExceptionForFault() {
      SoapFaultInspector<AdWordsApiException> inspector =
          new SoapFaultInspector<AdWordsApiException>() {
        ErrorType = typeof(ApiException)
      };

      XmlDocument xDoc = XmlUtilities.CreateDocument(fault_xml);
      Message message = Message.CreateMessage(
        new XmlNodeReader(xDoc), Int32.MaxValue, TestMessageVersion);

      AdWordsApiException exception = Assert.Throws<AdWordsApiException>(delegate () {
        inspector.AfterReceiveReply(ref message, this.channel);
      }, "No exception was thrown for a SOAP Fault response");
      Assert.AreEqual(typeof(ApiException), exception.ApiException.GetType());
      ApiException apiException = (ApiException)exception.ApiException;
      Assert.AreEqual(1, apiException.errors.Length);
      Assert.AreEqual(typeof(AuthenticationError), apiException.errors[0].GetType());
      AuthenticationError error =
          (AuthenticationError)apiException.errors[0];
      Assert.AreEqual(AuthenticationErrorReason.CUSTOMER_NOT_FOUND, error.reason);
      Assert.AreEqual("Test field path content", error.fieldPath);
    }
  }
}

