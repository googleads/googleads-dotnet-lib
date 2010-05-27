// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v201003;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;

namespace com.google.api.adwords.tests.v201003 {
  /// <summary>
  /// UnitTests to see if wsdl.exe dropped any class.
  /// </summary>
  [TestFixture]
  class TypeTests : BaseTests {
    /// <summary>
    /// Default public constructor.
    /// </summary>
    public TypeTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
    }

    /// <summary>
    /// Test whether we can add ad extension override to a given campaign.
    /// </summary>
    [Test]
    public void TestForAllClasses() {
      string[] exemptableTypes = {
          "GeoLocationError",             // not worried about error types.
          "CampaignAdExtensionError",     // not worried about error types.
          "AdExtensionError",             // not worried about error types.
          "PolicyViolationError.Part",    // becomes PolicyViolationErrorPart
          "AdExtensionOverrideError",     // not worried about error types.
      };
      bool passed = true;

      XmlDocument doc = new XmlDocument();
      doc.LoadXml(ClassMap.V201003);
      XmlNodeList allLinks = doc.SelectNodes("descendant::a[@href != 'javascript:void(0)']");
      Assembly dotnetLibrary = Assembly.GetAssembly(typeof(AdWordsUser));

      foreach (XmlElement link in allLinks) {
        bool checkType = true;
        string typeName = "";
        if (link.InnerText.EndsWith("...")) {
          typeName = link.Attributes["title"].Value;
        } else {
          typeName = link.InnerText;
        }
        foreach (string exemptableType in exemptableTypes) {
          if (exemptableType == typeName) {
            checkType = false;
            break;
          }
        }

        if (!checkType) {
          continue;
        }

        typeName = "com.google.api.adwords.v201003." + typeName;
        Type t = dotnetLibrary.GetType(typeName);
        if (t == null) {
          Console.WriteLine("Cannot load {0}", typeName);
          passed = false;
        } else {
          Console.WriteLine("Can load {0}", typeName);
        }
      }
      if (passed == false) {
        Assert.Fail("Failed to load some types.");
      }
    }
  }
}
