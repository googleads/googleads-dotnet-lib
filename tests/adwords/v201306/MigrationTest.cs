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
using Google.Api.Ads.AdWords.v201306;
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

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201306;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201306;

namespace Google.Api.Ads.AdWords.Tests.v201306 {
  /// <summary>
  /// Test cases for all the code examples under v201306\Migration.
  /// </summary>
  class MigrationTest : VersionedExampleTestsBase {
    long campaignId;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      campaignId = utils.CreateCampaign(user, BiddingStrategyType.MANUAL_CPC);
      utils.CreateLegacySitelinks(user, campaignId);
    }

    /// <summary>
    /// Tests the UpgradeLegacySitelinks VB.NET code example.
    /// </summary>
    [Test]
    public void TestUpgradeLegacySitelinksVBExample() {
      RunExample(delegate() {
        new VBExamples.UpgradeLegacySitelinks().Run(user, new long[] {campaignId});
      });
    }

    /// <summary>
    /// Tests the UpgradeLegacySitelinks C# code example.
    /// </summary>
    [Test]
    public void TestUpgradeLegacySitelinksCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.UpgradeLegacySitelinks().Run(user, new long[] {campaignId});
      });
    }
  }
}
