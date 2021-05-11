// Copyright 2018 Google LLC
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

using Google.Api.Ads.AdWords.Headers;

using NUnit.Framework;

using System.IO;
using System.Xml;

namespace Google.Api.Ads.AdWords.Tests.Lib
{
    /// <summary>
    /// Test cases for <see cref="ResponseHeader"/> class.
    /// </summary>
    internal class ResponseHeaderTests
    {
        private const string XML_NAMESPACE = "https://adwords.google.com/api/adwords/cm/v201705";
        private const int OPERATION_COUNT = 5;
        private const string REQUEST_ID = "abcdefghijklmnopqrstuvwxyz1234567890";
        private const string SERVICE_NAME = "BudgetService";
        private const string METHOD_NAME = "mutate";
        private const int RESPONSE_TIME = 324;

        private readonly string SOAP_XML_HEADER = $@"<ResponseHeader xmlns='{XML_NAMESPACE}'>
            <requestId>{REQUEST_ID}</requestId>
            <serviceName>{SERVICE_NAME}</serviceName>
            <methodName>{METHOD_NAME}</methodName>
            <operations>{OPERATION_COUNT}</operations>
            <responseTime>{RESPONSE_TIME}</responseTime>
        </ResponseHeader>".Trim();

        /// <summary>
        /// Tests the <see cref="ResponseHeader.ReadFrom(XmlReader, string)"/> method.
        /// </summary>
        [Test]
        [Category("Small")]
        public void TestReadFrom()
        {
            // Load the xml into an XmlReader.
            StringReader stringReader = new StringReader(SOAP_XML_HEADER);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(stringReader, settings);
            reader.MoveToContent();

            // Deserialize from the response header.
            ResponseHeader responseHeader = ResponseHeader.ReadFrom(reader, XML_NAMESPACE);

            // Check if all the properties have been deserialized correctly.
            Assert.AreEqual(OPERATION_COUNT, responseHeader.operations);
            Assert.AreEqual(REQUEST_ID, responseHeader.requestId);
            Assert.AreEqual(SERVICE_NAME, responseHeader.serviceName);
            Assert.AreEqual(METHOD_NAME, responseHeader.methodName);
            Assert.AreEqual(RESPONSE_TIME, responseHeader.responseTime);
        }
    }
}
