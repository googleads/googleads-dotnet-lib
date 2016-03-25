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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201603;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201603;

namespace Google.Api.Ads.AdWords.Tests.v201603 {
  /// <summary>
  /// Test cases for all the code examples under
  /// v201603\AudienceAndConversionManagement.
  /// </summary>
  class RemarketingTest : VersionedExampleTestsBase {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
    }

    /// <summary>
    /// Tests the AddAudience VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddAudienceVBExample() {
      RunExample(delegate() {
        new VBExamples.AddAudience().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddAudience C# code example.
    /// </summary>
    [Test]
    public void TestAddAudienceCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddAudience().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddRuleBasedRemarketingList VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddRuleBasedRemarketingListVBExample() {
      RunExample(delegate() {
        new VBExamples.AddRuleBasedRemarketingList().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddRuleBasedRemarketingList C# code example.
    /// </summary>
    [Test]
    public void TestAddRuleBasedRemarketingListCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddRuleBasedRemarketingList().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddConversionTracker VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddConversionTrackerVBExample() {
      RunExample(delegate() {
        new VBExamples.AddConversionTracker().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddConversionTracker C# code example.
    /// </summary>
    [Test]
    public void TestAddConversionTrackerCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddConversionTracker().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddCrmBasedUserList VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddCrmBasedUserListVBExample() {
      RunExample(delegate() {
        new VBExamples.AddCrmBasedUserList().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddCrmBasedUserList C# code example.
    /// </summary>
    [Test]
    public void TestAddCrmBasedUserListCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddCrmBasedUserList().Run(user);
      });
    }
  }
}
