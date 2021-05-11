// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdManager.Lib;

using NUnit.Framework;
using System;

using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Tests.Lib
{

    internal class MockAdManagerAppConfig : AdManagerAppConfig
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
    /// Test cases for AdManagerAppConfig.
    /// </summary>
    internal class AdManagerAppConfigTests
    {

        /// <summary>
        /// The dictionary to hold the test data.
        /// </summary>
        private Dictionary<string, string> dictSettings = new Dictionary<string, string>()
        {
            { "NetworkCode", "1234567890" },
            { "ApplicationName", "TEST_APPLICATION_NAME" },
            { "AdManagerApi.Server", "TEST_ADMANAGER_SERVER" },
            { "AuthorizationMethod", "OAuth2" },
        };

        /// <summary>
        /// Tests that various settings are read correctly.
        /// </summary>
        [Test]
        [Category("Small")]
        public void TestReadSettings()
        {
            MockAdManagerAppConfig config = new MockAdManagerAppConfig();
            config.MockReadSettings(dictSettings);

            Assert.AreEqual(dictSettings["NetworkCode"], config.NetworkCode);
            Assert.AreEqual(dictSettings["ApplicationName"], config.ApplicationName);
            Assert.AreEqual(dictSettings["AdManagerApi.Server"], config.AdManagerApiServer);
            Assert.AreEqual(dictSettings["AuthorizationMethod"],
                config.AuthorizationMethod.ToString());
        }
    }
}
