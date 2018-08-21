// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Common.Lib;

using NUnit.Framework;

using System;

namespace Google.Api.Ads.AdWords.Tests.Lib
{
    /// <summary>
    /// Tests for the AdWordsServiceFactory
    /// </summary>
    [TestFixture]
    internal class AdWordsServiceFactoryTests
    {
        private const string TEST_API_VERSION = "v201806";
        private const string TEST_SERVICE = "MediaService";
        private const string TEST_GROUP_NAME = "cm";

        private AdWordsServiceFactory serviceFactory;
        private ServiceSignature serviceSignature;
        private Uri testUri;

        /// <summary>
        /// Default public constructor.
        /// </summary>
        public AdWordsServiceFactoryTests()
        {
        }

        [SetUp]
        public void Init()
        {
            serviceFactory = new AdWordsServiceFactory();
            serviceFactory.Config = new AdWordsAppConfig();
            testUri = new Uri("https://google.com");
        }

        /// <summary>
        /// Test creating a service using OAuth2.
        /// </summary>
        [Test]
        public void TestCreateService()
        {
            serviceSignature =
                new AdWordsServiceSignature(TEST_API_VERSION, TEST_SERVICE, TEST_GROUP_NAME);
            serviceFactory.CreateService(serviceSignature, new AdWordsUser(), testUri);
        }
    }
}
