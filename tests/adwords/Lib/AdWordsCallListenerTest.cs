// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Examples.CSharp.v201302;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Tests.v201302;
using Google.Api.Ads.AdWords.v201302;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests;
using Google.Api.Ads.Common.Util;


using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

namespace Google.Api.Ads.AdWords.Tests.Lib {
  /// <summary>
  /// Test cases for AdWordsCallListener.
  /// </summary>
  class AdWordsCallListenerTest : VersionedExampleTestsBase {

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      user.ResetCallHistory();
    }

    /// <summary>
    /// Integration test to make sure Api Call Listener works.
    /// </summary>
    [Test]
    [Category("Integration")]
    public void TestGetAccountAlertsCSharpExample() {
      ExamplesMockData mockData = LoadMockData(SoapMessages_v201302.GetAccountAlerts);
      RunMockedExample(mockData, delegate() {
        new GetAccountAlerts().Run(user);
        Assert.AreEqual(user.GetTotalOperationCount(), 2);
        Assert.AreEqual(user.GetOperationCountForLastCall(), 2);
        ApiCallEntry[] callEntries = user.GetCallDetails();
        Assert.AreEqual(callEntries.Length, 1);
        ApiCallEntry callEntry = user.GetCallDetails()[0];
        Assert.AreEqual(callEntry.OperationCount, 2);
        Assert.AreEqual(callEntry.Method, "get");
        Assert.AreEqual(callEntry.Service.Signature.ServiceName, "AlertService");
      }, new WebRequestInterceptor.OnBeforeSendResponse(delegate(Uri uri,
            WebHeaderCollection headers, String body) {
      }));
    }

    /// <summary>
    /// Tests if SOAP messages are handled correctly.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestHandleMessage() {
      try {
        ContextStore.AddKey("SoapMethod", "get");

        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(SoapMessages_v201302.GetAccountAlerts);
        XmlElement xRequest = (XmlElement) xDoc.SelectSingleNode("/Example/SOAP/Response");
        xDoc.LoadXml(xRequest.InnerText);
        AlertService service = (AlertService) user.GetService(AdWordsService.v201302.AlertService);

        AdWordsCallListener.Instance.HandleMessage(xDoc, service, SoapMessageDirection.IN);

        Assert.AreEqual(user.GetTotalOperationCount(), 2);
        Assert.AreEqual(user.GetOperationCountForLastCall(), 2);
        ApiCallEntry[] callEntries = user.GetCallDetails();
        Assert.AreEqual(callEntries.Length, 1);
        ApiCallEntry callEntry = user.GetCallDetails()[0];

        Assert.AreEqual(callEntry.OperationCount, 2);
        Assert.AreEqual(callEntry.Method, "get");
        Assert.AreEqual(callEntry.Service.Signature.ServiceName, "AlertService");
      } finally {
        ContextStore.RemoveKey("SoapMethod");
      }
    }
  }
}
