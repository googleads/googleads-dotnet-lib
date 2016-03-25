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
  /// Test cases for all the code examples under v201603\Migration.
  /// </summary>
  class MigrationTest : VersionedExampleTestsBase {

    long campaignId;
    long adGroupId;
    long adId;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      campaignId = utils.CreateSearchCampaign(user, BiddingStrategyType.MANUAL_CPC);
      adGroupId = utils.CreateAdGroup(user, campaignId);
      adId = utils.CreateTextAd(user, adGroupId, false);
    }

    /// <summary>
    /// Tests the MigrateToExtensionSettings VB.NET code example.
    /// </summary>
    [Test]
    public void MigrateToExtensionSettingsVBExample() {
      RunExample(delegate() {
        new VBExamples.MigrateToExtensionSettings().Run(user);
      });
    }

    /// <summary>
    /// Tests the MigrateToExtensionSettings C# code example.
    /// </summary>
    [Test]
    public void MigrateToExtensionSettingsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.MigrateToExtensionSettings().Run(user);
      });
    }
  }
}
