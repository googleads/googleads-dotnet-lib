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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201309;
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

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201309;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201309;

namespace Google.Api.Ads.AdWords.Tests.v201309 {
  /// <summary>
  /// Test cases for all the code examples under v201309\AccountManagement.
  /// </summary>
  class AccountManagementTest : VersionedExampleTestsBase {
    /// <summary>
    /// Tests the GetAccountAlerts VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAccountAlertsVBExample() {
      ExamplesMockData mockData = LoadMockData(SoapMessages_v201309.GetAccountAlerts);
      RunMockedExample(mockData, delegate() {
        new VBExamples.GetAccountAlerts().Run(user);
      }, new WebRequestInterceptor.OnBeforeSendResponse(VerifyGetAccountAlertsRequest));
    }

    /// <summary>
    /// Tests the GetAccountAlerts C# code example.
    /// </summary>
    [Test]
    public void TestGetAccountAlertsCSharpExample() {
      ExamplesMockData mockData = LoadMockData(SoapMessages_v201309.GetAccountAlerts);
      RunMockedExample(mockData, delegate() {
        new CSharpExamples.GetAccountAlerts().Run(user);
      }, new WebRequestInterceptor.OnBeforeSendResponse(VerifyGetAccountAlertsRequest));
    }

    /// <summary>
    /// Verifies whether GetAccountAlerts is serializing the request correctly.
    /// </summary>
    /// <param name="requestUri">The request URI.</param>
    /// <param name="headers">The headers.</param>
    /// <param name="body">The body.</param>
    private void VerifyGetAccountAlertsRequest(Uri requestUri, WebHeaderCollection headers,
        string body) {
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(body);
      XmlElement node = (XmlElement) doc.GetElementsByTagName("selector")[0];
      AlertSelector selector =
          (AlertSelector) SerializationUtilities.DeserializeFromXmlTextCustomRootNs(
              node.OuterXml, typeof(AlertSelector),
              "https://adwords.google.com/api/adwords/mcm/v201309", "selector");

      Assert.AreEqual(selector.query.filterSpec, FilterSpec.ALL);
      Assert.AreEqual(selector.query.clientSpec, ClientSpec.ALL);
      Assert.AreEqual(selector.query.triggerTimeSpec, TriggerTimeSpec.ALL_TIME);
      Assert.Contains(AlertSeverity.GREEN, selector.query.severities);
      Assert.Contains(AlertSeverity.YELLOW, selector.query.severities);
      Assert.Contains(AlertSeverity.RED, selector.query.severities);

      foreach (AlertType alertType in Enum.GetValues(typeof(AlertType))) {
        Assert.Contains(alertType, selector.query.types);
      }

      Assert.AreEqual(selector.paging.startIndex, 0);
      Assert.AreEqual(selector.paging.numberResults, 500);
    }

    /// <summary>
    /// Tests the GetAccountChanges VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAccountChangesVBExample() {
      RunExample(delegate() {
        new VBExamples.GetAccountChanges().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetAccountChanges C# code example.
    /// </summary>
    [Test]
    public void TestGetAccountChangesCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetAccountChanges().Run(user);
      });
    }
  }
}
