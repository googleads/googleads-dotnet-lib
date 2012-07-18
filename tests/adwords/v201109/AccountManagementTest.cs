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
using Google.Api.Ads.AdWords.v201109;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201109;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201109;

namespace Google.Api.Ads.AdWords.Tests.v201109 {
  /// <summary>
  /// Test cases for all the code examples under v201109\AccountManagement.
  /// </summary>
  class AccountManagementTest : ExampleBaseTests {
    string clientEmailAddress;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      clientEmailAddress = "client_1+" + (user.Config as AdWordsAppConfig).Email;
    }

    /// <summary>
    /// Tests the CreateAccount VB.NET code example.
    /// </summary>
    [Test]
    public void TestCreateAccountVBExample() {
      RunExample(delegate() {
        new VBExamples.CreateAccount().Run(user);
      });
    }

    /// <summary>
    /// Tests the CreateAccount C# code example.
    /// </summary>
    [Test]
    public void TestCreateAccountCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.CreateAccount().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetAccountAlerts VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAccountAlertsVBExample() {
      RunExample(delegate() {
        new VBExamples.GetAccountAlerts().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetAccountAlerts C# code example.
    /// </summary>
    [Test]
    public void TestGetAccountAlertsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetAccountAlerts().Run(user);
      });
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

    /// <summary>
    /// Tests the GetAccountHierarchy VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAccountHierarchyVBExample() {
      RunExample(delegate() {
        new VBExamples.GetAccountHierarchy().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetAccountHierarchy C# code example.
    /// </summary>
    [Test]
    public void TestGetAccountHierarchyCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetAccountHierarchy().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetClientCustomerId VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetClientCustomerIdVBExample() {
      RunExample(delegate() {
        new VBExamples.GetClientCustomerId().Run(user, clientEmailAddress);
      });
    }

    /// <summary>
    /// Tests the GetClientCustomerId C# code example.
    /// </summary>
    [Test]
    public void TestGetClientCustomerIdCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetClientCustomerId().Run(user, clientEmailAddress);
      });
    }

    /// <summary>
    /// Tests the GetClientUnitUsage VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetClientUnitUsageVBExample() {
      RunExample(delegate() {
        new VBExamples.GetClientUnitUsage().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetClientUnitUsage C# code example.
    /// </summary>
    [Test]
    public void TestGetClientUnitUsageCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetClientUnitUsage().Run(user);
      });
    }
  }
}
