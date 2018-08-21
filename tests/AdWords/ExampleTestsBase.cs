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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;

namespace Google.Api.Ads.AdWords.Tests
{
    /// <summary>
    /// UnitTests for code examples.
    /// </summary>
    [TestFixture]
    [Category("ExampleTest")]
    public class ExampleTestsBase
    {
        /// <summary>
        /// The AdWordsUser instance for running code examples.
        /// </summary>
        protected AdWordsUser user = new AdWordsUser();

        /// <summary>
        /// Default public constructor.
        /// </summary>
        public ExampleTestsBase() : base()
        {
        }

        /// <summary>
        /// Runs a code example.
        /// </summary>
        /// <param name="exampleDelegate">The delegate that initializes and runs the
        /// code example.</param>
        protected void RunExample(TestDelegate exampleDelegate)
        {
            Thread.Sleep(3000);
            StringWriter writer = new StringWriter();
            Assert.DoesNotThrow(delegate()
            {
                TextWriter oldWriter = Console.Out;
                Console.SetOut(writer);
                exampleDelegate.Invoke();
                Console.SetOut(oldWriter);
                Console.WriteLine(writer.ToString());
            });
        }

        /// <summary>
        /// Verifies the HTTP headers.
        /// </summary>
        /// <param name="headers">The HTTP headers.</param>
        protected void VerifyHttpHeaders(WebHeaderCollection headers)
        {
            Assert.AreEqual(headers["Authorization"], user.OAuthProvider.GetAuthHeader());
        }

        /// <summary>
        /// Verifies the SOAP headers.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="requestBody">The request body.</param>
        protected void VerifySoapHeaders(Uri requestUri, string requestBody)
        {
            AdWordsAppConfig config = user.Config as AdWordsAppConfig;

            if (requestUri.AbsoluteUri.StartsWith(config.AdWordsApiServer))
            {
                XmlDocument xDoc = XmlUtilities.CreateDocument(requestBody);

                XmlNamespaceManager xmlns = new XmlNamespaceManager(xDoc.NameTable);
                xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                XmlElement requestHeaders =
                    (XmlElement) xDoc.SelectSingleNode("soap:Envelope/soap:Header/child::*", xmlns);
                Assert.NotNull(requestHeaders);
                Assert.AreEqual(requestHeaders.Name, "RequestHeader");
                foreach (XmlElement childNode in requestHeaders.ChildNodes)
                {
                    switch (childNode.Name)
                    {
                        case "developerToken":
                            Assert.AreEqual(childNode.InnerText, config.DeveloperToken);
                            break;

                        case "clientCustomerId":
                            Assert.AreEqual(childNode.InnerText, config.ClientCustomerId);
                            break;

                        case "userAgent":
                            Assert.AreEqual(childNode.InnerText, config.GetUserAgent());
                            break;
                    }
                }
            }
        }
    }
}
