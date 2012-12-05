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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201209;
using Google.Api.Ads.Common.Tests;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;

namespace Google.Api.Ads.AdWords.Tests {
  /// <summary>
  /// UnitTests for code examples.
  /// </summary>
  public class ExampleTestsBase {
    /// <summary>
    /// The AdWordsUser instance for running code examples.
    /// </summary>
    protected AdWordsUser user = new AdWordsUser();

    /// <summary>
    /// The interceptor for ClientLogin requests when running mocked code
    /// examples.
    /// </summary>
    protected ClientLoginRequestInterceptor clientLoginInterceptor =
        ClientLoginRequestInterceptor.Instance as ClientLoginRequestInterceptor;

    /// <summary>
    /// The interceptor for AdWords API requests when running mocked code
    /// examples.
    /// </summary>
    protected AdWordsRequestInterceptor awapiInterceptor =
        AdWordsRequestInterceptor.Instance as AdWordsRequestInterceptor;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public ExampleTestsBase() : base() {
    }

    /// <summary>
    /// Runs a code example in mocked mode.
    /// </summary>
    /// <param name="mockData">The mock data for mocking SOAP request and
    /// responses for API calls.</param>
    /// <param name="exampleDelegate">The delegate that initializes and runs the
    /// code example.</param>
    /// <param name="callback">The callback to be called before mocked responses
    /// are sent. You could use this callback to verify if the request was
    /// serialized correctly.</param>
    /// <remarks>This method is not thread safe, but since NUnit can run tests
    /// only in a single threaded mode, thread safety is not a requirement.
    /// </remarks>
    protected void RunMockedExample(ExamplesMockData mockData, TestDelegate exampleDelegate,
        WebRequestInterceptor.OnBeforeSendResponse callback) {
      TextWriter oldWriter = Console.Out;
      try {
        clientLoginInterceptor.Intercept = true;
        clientLoginInterceptor.RaiseException = false;
        awapiInterceptor.Intercept = true;
        awapiInterceptor.LoadMessages(mockData.MockMessages,
             delegate(Uri requestUri, WebHeaderCollection headers, String body) {
               VerifySoapHeaders(requestUri, body);
               callback(requestUri, headers, body);
             }
         );
        StringWriter newWriter = new StringWriter();
        Console.SetOut(newWriter);
        AdWordsAppConfig config = (user.Config as AdWordsAppConfig);
        exampleDelegate.Invoke();
        Assert.AreEqual(newWriter.ToString().Trim(), mockData.ExpectedOutput.Trim());
      } finally {
        Console.SetOut(oldWriter);
        clientLoginInterceptor.Intercept = false;
        awapiInterceptor.Intercept = false;
      }
    }

    /// <summary>
    /// Runs a code example.
    /// </summary>
    /// <param name="exampleDelegate">The delegate that initializes and runs the
    /// code example.</param>
    protected void RunExample(TestDelegate exampleDelegate) {
      Thread.Sleep(10000);
      StringWriter writer = new StringWriter();
      Assert.DoesNotThrow(delegate() {
        TextWriter oldWriter = Console.Out;
        Console.SetOut(writer);
        exampleDelegate.Invoke();
        Console.SetOut(oldWriter);
        Console.WriteLine(writer.ToString());
      });
    }

    /// <summary>
    /// Loads the mock data for a code example.
    /// </summary>
    /// <param name="mockData">The mock data.</param>
    /// <returns>The parsed mock data.</returns>
    protected ExamplesMockData LoadMockData(string mockData) {
      List<HttpMessage> messages = new List<HttpMessage>();

      XmlDocument xDoc = new XmlDocument();
      xDoc.LoadXml(mockData);
      XmlNodeList soapNodes = xDoc.SelectNodes("Example/SOAP");

      foreach (XmlElement soapNode in soapNodes) {
        messages.Add(new HttpMessage(soapNode.SelectSingleNode("Request").InnerText,
            soapNode.SelectSingleNode("Response").InnerText,
            AdWordsRequestInterceptor.SOAP_RESPONSE_TYPE));
      }
      return new ExamplesMockData(messages.ToArray(), xDoc.SelectSingleNode("Example/Output").
          InnerText);
    }

    /// <summary>
    /// Verifies the SOAP headers.
    /// </summary>
    /// <param name="requestUri">The request URI.</param>
    /// <param name="requestBody">The request body.</param>
    protected void VerifySoapHeaders(Uri requestUri, string requestBody) {
      AdWordsAppConfig config = user.Config as AdWordsAppConfig;

      if (requestUri.AbsoluteUri.StartsWith(config.AdWordsApiServer)) {
        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(requestBody);

        XmlNamespaceManager xmlns = new XmlNamespaceManager(xDoc.NameTable);
        xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        XmlElement requestHeaders = (XmlElement) xDoc.SelectSingleNode(
            "soap:Envelope/soap:Header/child::*", xmlns);
        Assert.NotNull(requestHeaders);
        Assert.AreEqual(requestHeaders.Name, "RequestHeader");
        foreach (XmlElement childNode in requestHeaders.ChildNodes) {
          switch (childNode.Name) {
            case "authToken":
              Assert.AreEqual(childNode.InnerText, "AUTH_TOKEN");
              break;

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
