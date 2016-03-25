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

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201603;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201603;

namespace Google.Api.Ads.AdWords.Tests.v201603 {
  /// <summary>
  /// Test cases for all the code examples under v201603\AccountManagement.
  /// </summary>
  class AccountManagementTest : VersionedExampleTestsBase {

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
