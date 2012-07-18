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
using Google.Api.Ads.AdWords.v201109_1;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201109_1;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201109_1;

namespace Google.Api.Ads.AdWords.Tests.v201109_1 {
  /// <summary>
  /// Test cases for all the code examples under v201109_1\CampaignManagement.
  /// </summary>
  class CampaignManagementTest : ExampleBaseTests {
    long campaignId;
    long adGroupId;
    long criterionId;
    long adId;
    long locationExtensionId;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      campaignId = utils.CreateCampaign(user, new ManualCPC());
      adGroupId = utils.CreateAdGroup(user, campaignId);
      criterionId = utils.CreateKeyword(user, adGroupId);
      adId = utils.CreateTextAd(user, adGroupId, false);
      locationExtensionId = utils.CreateLocationExtension(user, campaignId);
    }

    /// <summary>
    /// Tests the AddExperiment VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddExperimentVBExample() {
      RunExample(delegate() {
        new VBExamples.AddExperiment().Run(user, campaignId, adGroupId, criterionId);
      });
    }

    /// <summary>
    /// Tests the AddExperiment C# code example.
    /// </summary>
    [Test]
    public void TestAddExperimentCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddExperiment().Run(user, campaignId, adGroupId, criterionId);
      });
    }

    /// <summary>
    /// Tests the AddKeywordsInBulk VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddKeywordsInBulkVBExample() {
      RunExample(delegate() {
        new VBExamples.AddKeywordsInBulk().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddKeywordsInBulk C# code example.
    /// </summary>
    [Test]
    public void TestAddKeywordsInBulkCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddKeywordsInBulk().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddLocationExtension VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddLocationExtensionVBExample() {
      RunExample(delegate() {
        new VBExamples.AddLocationExtension().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddLocationExtension C# code example.
    /// </summary>
    [Test]
    public void TestAddLocationExtensionCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddLocationExtension().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddLocationExtensionOverride VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddLocationExtensionOverrideVBExample() {
      RunExample(delegate() {
        new VBExamples.AddLocationExtensionOverride().Run(user, adId, locationExtensionId);
      });
    }

    /// <summary>
    /// Tests the AddLocationExtensionOverride C# code example.
    /// </summary>
    [Test]
    public void TestAddLocationExtensionOverrideCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddLocationExtensionOverride().Run(user, adId, locationExtensionId);
      });
    }

    /// <summary>
    /// Tests the AddSiteLinks VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddSiteLinksVBExample() {
      RunExample(delegate() {
        new VBExamples.AddSiteLinks().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddSiteLinks C# code example.
    /// </summary>
    [Test]
    public void TestAddSiteLinksCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddSiteLinks().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the DeleteSitelinks VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeleteSitelinksVBExample() {
      RunExample(delegate() {
        new VBExamples.DeleteSitelinks().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the DeleteSitelinks C# code example.
    /// </summary>
    [Test]
    public void TestDeleteSitelinksCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.DeleteSitelinks().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the GetAllDisapprovedAds VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAllDisapprovedAdsVBExample() {
      RunExample(delegate() {
        new VBExamples.GetAllDisapprovedAds().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the GetAllDisapprovedAds C# code example.
    /// </summary>
    [Test]
    public void TestGetAllDisapprovedAdsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetAllDisapprovedAds().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the PromoteExperiment VB.NET code example.
    /// </summary>
    [Test]
    public void TestPromoteExperimentVBExample() {
      long experimentId = CreateExperimentForPromotion();
      RunExample(delegate() {
        new VBExamples.PromoteExperiment().Run(user, experimentId);
      });
    }

    /// <summary>
    /// Tests the PromoteExperiment C# code example.
    /// </summary>
    [Test]
    public void TestPromoteExperimentCSharpExample() {
      long experimentId = CreateExperimentForPromotion();
      RunExample(delegate() {
        new CSharpExamples.PromoteExperiment().Run(user, experimentId);
      });
    }

    /// <summary>
    /// Tests the SetAdParameters VB.NET code example.
    /// </summary>
    [Test]
    public void TestSetAdParametersVBExample() {
      RunExample(delegate() {
        new VBExamples.SetAdParameters().Run(user, adGroupId, criterionId);
      });
    }

    /// <summary>
    /// Tests the SetAdParameters C# code example.
    /// </summary>
    [Test]
    public void TestSetAdParametersCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.SetAdParameters().Run(user, adGroupId, criterionId);
      });
    }

    /// <summary>
    /// Tests the ValidateTextAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestValidateTextAdVBExample() {
      RunExample(delegate() {
        new VBExamples.ValidateTextAd().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the ValidateTextAd C# code example.
    /// </summary>
    [Test]
    public void TestValidateTextAdCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.ValidateTextAd().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Create an experiment for promotion.
    /// </summary>
    /// <returns>Experiment id for promotion.</returns>
    /// <remarks> This code cannot be added in Init(), since there can be only
    /// one experiment per campaign, and if we add this to Init(), then
    /// AddExperiment tests will fail.</remarks>
    private long CreateExperimentForPromotion() {
      return utils.AddExperiment(user, campaignId, adGroupId, criterionId);
    }
  }
}
