// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Tests;
using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace Google.Api.Ads.AdWords.Tests {

  /// <summary>
  /// Base class for integration tests where API calls from code examples are
  /// mocked out.
  /// </summary>
  public class MockedExampleTestsBase : ExampleTestsBase {

    /// <summary>
    /// The interceptor for AdWords API requests when running mocked code
    /// examples.
    /// </summary>
    protected AdWordsRequestInterceptor awapiInterceptor =
        AdWordsRequestInterceptor.Instance as AdWordsRequestInterceptor;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public MockedExampleTestsBase()
      : base() {
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
        awapiInterceptor.Intercept = true;
        awapiInterceptor.LoadMessages(mockData.MockMessages,
             delegate(Uri requestUri, WebHeaderCollection headers, String body) {
               VerifyHttpHeaders(headers);
               VerifySoapHeaders(requestUri, body);
               callback(requestUri, headers, body);
             }
         );
        StringWriter newWriter = new StringWriter();
        Console.SetOut(newWriter);
        exampleDelegate.Invoke();
        Assert.AreEqual(newWriter.ToString().Trim(), mockData.ExpectedOutput.Trim());
      } finally {
        Console.SetOut(oldWriter);
        awapiInterceptor.Intercept = false;
      }
    }

    /// <summary>
    /// Loads the mock data for a code example.
    /// </summary>
    /// <param name="mockData">The mock data.</param>
    /// <returns>The parsed mock data.</returns>
    protected ExamplesMockData LoadMockData(string mockData) {
      List<HttpMessage> messages = new List<HttpMessage>();

      XmlDocument xDoc = XmlUtilities.CreateDocument(mockData);
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
    /// Sets the mock OAuth2 tokens.
    /// </summary>
    protected void SetMockOAuth2Tokens() {
      user.OAuthProvider.UpdatedOn = DateTime.Now;
      user.OAuthProvider.ExpiresIn = int.Parse(OAuth2RequestInterceptor.EXPIRES_IN);
      user.OAuthProvider.AccessToken = OAuth2RequestInterceptor.TEST_ACCESS_TOKEN;
    }
  }
}
