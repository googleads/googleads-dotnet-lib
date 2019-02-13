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

using System;
using System.Linq;

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests;
using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v201902;

using NUnit.Framework;

namespace Google.Api.Ads.AdManager.Tests
{
    /// <summary>
    /// UnitTests for service creation.
    /// </summary>
    [TestFixture]
    [Category("Smoke")]
    public class ServiceCreationTests : BaseTests
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
            StubIntegrityTestHelper.EnumerateServices<AdManagerService>(
                delegate (ServiceSignature serviceSignature)
                {
                    Assert.DoesNotThrow(delegate () { user.GetService(serviceSignature); });
                });
        }

        /// <summary>
        /// Test that a generated service interface has expected methods.
        /// </summary>
        [Test]
        public void TestServiceInterface()
        {
            Type serviceInterface = typeof(ILineItemService);
            var methodDictionary = serviceInterface.GetInterfaces().SelectMany(t => t.GetMethods())
                .Where(m => !m.ReturnType.FullName.Contains("Wrappers"))
                .Concat(serviceInterface.GetMethods()).ToDictionary(m => m.Name);
            Assert.That(methodDictionary, Contains.Key("getLineItemsByStatement"));
            Assert.That(methodDictionary, Contains.Key("createLineItems"));
            Assert.That(methodDictionary, Contains.Key("updateLineItems"));
            Assert.That(methodDictionary, Contains.Key("performLineItemAction"));
        }

        /// <summary>
        /// Test getting a service with the generic method.
        /// </summary>
        [Test]
        public void TestGetServiceTyped()
        {
            Assert.DoesNotThrow(delegate ()
            {
                user.GetService<LineItemService>();
            });

            Assert.Throws<ArgumentException>(delegate ()
            {
                user.GetService<UnregisteredService>();
            });
        }

        private class UnregisteredService : LineItemService
        {

        }
    }
}
