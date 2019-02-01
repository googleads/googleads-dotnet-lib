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
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests;

using NUnit.Framework;

using System;
using System.Linq;

namespace Google.Api.Ads.AdWords.Tests
{
    /// <summary>
    /// UnitTests for service creation.
    /// </summary>
    [TestFixture]
    public class ServiceCreationTests : ExampleTestsBase
    {
        /// <summary>
        /// Default public constructor.
        /// </summary>
        public ServiceCreationTests() : base()
        {
        }

        /// <summary>
        /// Test whether we can create all the services without any exceptions.
        /// </summary>
        [Test]
        public void TestCreateServices()
        {
            StubIntegrityTestHelper.EnumerateServices<AdWordsService>(
                delegate(ServiceSignature serviceSignature)
                {
                    Assert.DoesNotThrow(delegate()
                    {
                        AdsClient service = user.GetService(serviceSignature);
                    });
                });
        }

        /// <summary>
        /// Test that a generated service interface has expected methods.
        /// </summary>
        [Test]
        public void TestServiceInterface()
        {
            Type serviceInterface = typeof(IAdGroupService);
            var methodDictionary = serviceInterface.GetInterfaces().SelectMany(t => t.GetMethods())
                .Where(m => !m.ReturnType.FullName.Contains("Wrappers"))
                .Concat(serviceInterface.GetMethods()).ToDictionary(m => m.Name);
            Assert.That(methodDictionary, Contains.Key("get"));
            Assert.That(methodDictionary, Contains.Key("mutate"));
            Assert.That(methodDictionary, Contains.Key("mutateLabel"));
            Assert.That(methodDictionary, Contains.Key("query"));
        }
    }
}
