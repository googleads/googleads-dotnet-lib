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
using Google.Api.Ads.Common.Lib;

using NUnit.Framework;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201603;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201603;

namespace Google.Api.Ads.AdWords.Tests.v201603 {

  /// <summary>
  /// Test cases for all the code examples under v201603\Extensions.
  /// </summary>
  internal class ExtensionsTest : VersionedExampleTestsBase {
    private long campaignId;
    private string gmbAccessToken = "";

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      campaignId = utils.CreateSearchCampaign(user, BiddingStrategyType.MANUAL_CPC);

      // Load defaults from config file.
      AdWordsAppConfig appConfig = new AdWordsAppConfig();
      appConfig.OAuth2RefreshToken = appConfig.GMBOAuth2RefreshToken;

      AdsOAuthProviderForApplications oAuth2Provider =
          new OAuth2ProviderForApplications(appConfig);
      oAuth2Provider.RefreshAccessToken();

      gmbAccessToken = oAuth2Provider.AccessToken;
    }

    /// <summary>
    /// Tests the AddSitelinks VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddSitelinksVBExample() {
      RunExample(delegate() {
        new VBExamples.AddSitelinks().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddSitelinks C# code example.
    /// </summary>
    [Test]
    public void TestAddSitelinksCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddSitelinks().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddSitelinksUsingFeeds VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddSitelinksUsingFeedsVBExample() {
      string feedName = "SitelinkFeed" + utils.GetTimeStampAlpha();
      RunExample(delegate() {
        new VBExamples.AddSitelinksUsingFeeds().Run(user, campaignId, feedName);
      });
    }

    /// <summary>
    /// Tests the AddSitelinksUsingFeeds C# code example.
    /// </summary>
    [Test]
    public void TestAddSitelinksUsingFeedsCSharpExample() {
      string feedName = "SitelinkFeed" + utils.GetTimeStampAlpha();
      RunExample(delegate() {
        new CSharpExamples.AddSitelinksUsingFeeds().Run(user, campaignId, feedName);
      });
    }

    /// <summary>
    /// Tests the AddGoogleMyBusinessLocationExtension C# code example.
    /// </summary>
    [Test]
    public void TestAddGoogleMyBusinessLocationExtensionCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;

      RunExample(delegate() {
        new CSharpExamples.AddGoogleMyBusinessLocationExtensions().Run(user,
            config.GMBLoginEmail, gmbAccessToken, null);
      });
    }

    /// <summary>
    /// Tests the AddGoogleMyBusinessLocationExtension VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddGoogleMyBusinessLocationExtensionVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;

      RunExample(delegate() {
        new VBExamples.AddGoogleMyBusinessLocationExtensions().Run(user, config.GMBLoginEmail,
            gmbAccessToken, null);
      });
    }
  }
}
