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
  /// Test cases for all the code examples under v201109\CampaignManagement.
  /// </summary>
  class CampaignManagementTest : ExampleBaseTests {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      parameters = new Dictionary<string, string>();

      long campaignId = utils.CreateCampaign(user, new ManualCPC());
      long adGroupId = utils.CreateAdGroup(user, campaignId);
      long criterionId = utils.CreateKeyword(user, adGroupId);
      long adId = utils.CreateTextAd(user, adGroupId, false);
      long locationExtensionId = utils.CreateLocationExtension(user, campaignId);

      parameters["CAMPAIGN_ID"] = campaignId.ToString();
      parameters["ADGROUP_ID"] = adGroupId.ToString();
      parameters["CRITERION_ID"] = criterionId.ToString();
      parameters["AD_ID"] = adId.ToString();
      parameters["LOCATION_EXTENSION_ID"] = locationExtensionId.ToString();
    }

    /// <summary>
    /// Tests the AddExperiment VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddExperimentVBExample() {
      RunExample(new VBExamples.AddExperiment());
    }

    /// <summary>
    /// Tests the AddExperiment C# code example.
    /// </summary>
    [Test]
    public void TestAddExperimentCSharpExample() {
      RunExample(new CSharpExamples.AddExperiment());
    }

    /// <summary>
    /// Tests the AddKeywordsInBulk VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddKeywordsInBulkVBExample() {
      RunExample(new VBExamples.AddKeywordsInBulk());
    }

    /// <summary>
    /// Tests the AddKeywordsInBulk C# code example.
    /// </summary>
    [Test]
    public void TestAddKeywordsInBulkCSharpExample() {
      RunExample(new CSharpExamples.AddKeywordsInBulk());
    }

    /// <summary>
    /// Tests the AddLocationExtension VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddLocationExtensionVBExample() {
      RunExample(new VBExamples.AddLocationExtension());
    }

    /// <summary>
    /// Tests the AddLocationExtension C# code example.
    /// </summary>
    [Test]
    public void TestAddLocationExtensionCSharpExample() {
      RunExample(new CSharpExamples.AddLocationExtension());
    }

    /// <summary>
    /// Tests the AddLocationExtensionOverride VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddLocationExtensionOverrideVBExample() {
      RunExample(new VBExamples.AddLocationExtensionOverride());
    }

    /// <summary>
    /// Tests the AddLocationExtensionOverride C# code example.
    /// </summary>
    [Test]
    public void TestAddLocationExtensionOverrideCSharpExample() {
      RunExample(new CSharpExamples.AddLocationExtensionOverride());
    }

    /// <summary>
    /// Tests the AddSiteLinks VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddSiteLinksVBExample() {
      RunExample(new VBExamples.AddSiteLinks());
    }

    /// <summary>
    /// Tests the AddSiteLinks C# code example.
    /// </summary>
    [Test]
    public void TestAddSiteLinksCSharpExample() {
      RunExample(new CSharpExamples.AddSiteLinks());
    }

    /// <summary>
    /// Tests the DeleteSitelinks VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeleteSitelinksVBExample() {
      RunExample(new VBExamples.DeleteSitelinks());
    }

    /// <summary>
    /// Tests the DeleteSitelinks C# code example.
    /// </summary>
    [Test]
    public void TestDeleteSitelinksCSharpExample() {
      RunExample(new CSharpExamples.DeleteSitelinks());
    }

    /// <summary>
    /// Tests the GetAllDisapprovedAds VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAllDisapprovedAdsVBExample() {
      RunExample(new VBExamples.GetAllDisapprovedAds());
    }

    /// <summary>
    /// Tests the GetAllDisapprovedAds C# code example.
    /// </summary>
    [Test]
    public void TestGetAllDisapprovedAdsCSharpExample() {
      RunExample(new CSharpExamples.GetAllDisapprovedAds());
    }

    /// <summary>
    /// Create an experiment for promotion.
    /// </summary>
    /// <remarks> This code cannot be added in Init(), since there can be only
    /// one experiment per campaign, and if we add this to Init(), then
    /// AddExperiment tests will fail.</remarks>
    private void CreateExperimentForPromotion() {
      long campaignId = long.Parse(parameters["CAMPAIGN_ID"]);
      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);
      long criterionId = long.Parse(parameters["CRITERION_ID"]);
      parameters["EXPERIMENT_ID"] = utils.AddExperiment(user, campaignId, adGroupId,
          criterionId).ToString();
    }

    /// <summary>
    /// Tests the PromoteExperiment VB.NET code example.
    /// </summary>
    [Test]
    public void TestPromoteExperimentVBExample() {
      CreateExperimentForPromotion();
      RunExample(new VBExamples.PromoteExperiment());
    }

    /// <summary>
    /// Tests the PromoteExperiment C# code example.
    /// </summary>
    [Test]
    public void TestPromoteExperimentCSharpExample() {
      CreateExperimentForPromotion();
      RunExample(new CSharpExamples.PromoteExperiment());
    }

    /// <summary>
    /// Tests the SetAdParameters VB.NET code example.
    /// </summary>
    [Test]
    public void TestSetAdParametersVBExample() {
      RunExample(new VBExamples.SetAdParameters());
    }

    /// <summary>
    /// Tests the SetAdParameters C# code example.
    /// </summary>
    [Test]
    public void TestSetAdParametersCSharpExample() {
      RunExample(new CSharpExamples.SetAdParameters());
    }

    /// <summary>
    /// Tests the ValidateTextAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestValidateTextAdVBExample() {
      RunExample(new VBExamples.ValidateTextAd());
    }

    /// <summary>
    /// Tests the ValidateTextAd C# code example.
    /// </summary>
    [Test]
    public void TestValidateTextAdCSharpExample() {
      RunExample(new CSharpExamples.ValidateTextAd());
    }
  }
}
