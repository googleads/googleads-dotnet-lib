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

using NUnit.Framework;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Tests.Lib
{
    internal class MockAdWordsAppConfig : AdWordsAppConfig
    {
        /// <summary>
        /// Allows the test cases to call ReadSettings method for testing purposes.
        /// </summary>
        /// <param name="dictSettings">The configuration settings.</param>
        /// <remarks>AppConfigBase class loads its settings from App.config, and the
        /// framework calls ReadSettings method to load the values. However, this is
        /// a protected method, so we expose ReadSettings in the mock version to
        /// allow easier configuration of AppConfig while running test cases.
        /// </remarks>
        public void MockReadSettings(Dictionary<string, string> dictSettings) =>
            base.ReadSettings(dictSettings);
    }

    /// <summary>
    /// Test cases for AdWordsAppConfig.
    /// </summary>
    internal class AdWordsAppConfigTests
    {
        /// <summary>
        /// A user agent string with control chars in it.
        /// </summary>
        private const string CONTROL_CHARS_USERAGENT = "Useragent \u0001";

        /// <summary>
        /// A user agent string with unicode characters in it.
        /// </summary>
        private const string UNICODE_USERAGENT = "Useragent \u1234";

        /// <summary>
        /// A user agent with only printable ASCII characters in it.
        /// </summary>
        private const string ASCII_USERAGENT = "Useragent";

        /// <summary>
        /// The dictionary to hold the test data.
        /// </summary>
        private Dictionary<string, string> dictSettings = new Dictionary<string, string>()
        {
            {"ClientCustomerId", "1234567890"},
            {"DeveloperToken", "TEST_DEVELOPER_TOKEN"},
            {"GoogleMyBusiness.LoginEmail", "TEST_LOGIN_EMAIL"},
            {"GoogleMyBusiness.OAuth2RefreshToken", "TEST_REFRESH_TOKEN"},
            {"MerchantCenter.AccountId", "123456"},
            {"UserAgent", "TEST_USER_AGENT"},
            {"AdWordsApi.Server", "TEST_API_SERVER"},
            {"SkipReportHeader", "true"},
            {"SkipReportSummary", "true"},
            {"SkipColumnHeader", "false"},
            {"IncludeZeroImpressions", "true"},
            {"UseRawEnumValues", "true"}
        };

        /// <summary>
        /// Tests to make sure User agent can be set properly, and neccessary
        /// validations are done.
        /// </summary>
        [Test]
        [Category("Small")]
        public void TestSetUserAgent()
        {
            MockAdWordsAppConfig config = new MockAdWordsAppConfig();

            Assert.Throws(typeof(ArgumentException),
                delegate() { config.UserAgent = CONTROL_CHARS_USERAGENT; });

            Assert.Throws(typeof(ArgumentException),
                delegate() { config.UserAgent = UNICODE_USERAGENT; });

            Assert.DoesNotThrow(delegate() { config.UserAgent = ASCII_USERAGENT; });
        }

        /// <summary>
        /// Tests that various settings are read correctly.
        /// </summary>
        [Test]
        [Category("Small")]
        public void TestReadSettings()
        {
            MockAdWordsAppConfig config = new MockAdWordsAppConfig();
            config.MockReadSettings(dictSettings);

            Assert.AreEqual(dictSettings["ClientCustomerId"], config.ClientCustomerId);
            Assert.AreEqual(dictSettings["DeveloperToken"], config.DeveloperToken);
            Assert.AreEqual(dictSettings["GoogleMyBusiness.LoginEmail"], config.GMBLoginEmail);
            Assert.AreEqual(dictSettings["GoogleMyBusiness.OAuth2RefreshToken"],
                config.GMBOAuth2RefreshToken);
            Assert.AreEqual(dictSettings["MerchantCenter.AccountId"],
                config.MerchantCenterId.ToString());

            Assert.AreEqual(dictSettings["UserAgent"], config.UserAgent);
            Assert.AreEqual(dictSettings["AdWordsApi.Server"], config.AdWordsApiServer);
            Assert.AreEqual(dictSettings["SkipReportHeader"],
                config.SkipReportHeader.ToString().ToLower());
            Assert.AreEqual(dictSettings["SkipReportSummary"],
                config.SkipReportSummary.ToString().ToLower());
            Assert.AreEqual(dictSettings["SkipColumnHeader"],
                config.SkipColumnHeader.ToString().ToLower());
            Assert.AreEqual(dictSettings["IncludeZeroImpressions"],
                config.IncludeZeroImpressions.ToString().ToLower());
            Assert.AreEqual(dictSettings["UseRawEnumValues"],
                config.UseRawEnumValues.ToString().ToLower());
        }
    }
}
