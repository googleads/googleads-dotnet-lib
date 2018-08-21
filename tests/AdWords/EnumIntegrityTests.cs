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
using Google.Api.Ads.Common.Tests;

using NUnit.Framework;

using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Tests
{
    /// <summary>
    /// UnitTests to verify if the enums in the stub code are correct.
    /// </summary>
    [TestFixture]
    [Category("Smoke")]
    public class EnumIntegrityTests : ExampleTestsBase
    {
        private const string ROOT_NAMESPACE = "Google.Api.Ads.AdWords.";

        /// <summary>
        /// Default public constructor.
        /// </summary>
        public EnumIntegrityTests() : base()
        {
        }

        /// <summary>
        /// Test whether enum values are same across versions.
        /// </summary>
        [Test]
        public void TestEnumValues()
        {
            Dictionary<string, int> lookup = new Dictionary<string, int>();

            // Enumerate through each of the enums and their fields.
            StubIntegrityTestHelper.EnumerateEnumFields<AdWordsService>(ROOT_NAMESPACE,

                // For each matching enum and field, process it.
                delegate(string hashedFieldName, int enumValue)
                {
                    // If this key exists in the lookup table, then the value of that
                    // entry should match the value of the enum field we are examining.
                    int existingEnumValue = 0;
                    if (lookup.TryGetValue(hashedFieldName, out existingEnumValue))
                    {
                        Assert.AreEqual(existingEnumValue, enumValue,
                            string.Format(
                                "Enum value of {0} doesn't match. Old value: {1}, new value: {2}",
                                hashedFieldName, existingEnumValue, enumValue));
                    }
                    else
                    {
                        // Otherwise, this is a new enum type / field. Add it to the lookup map.
                        lookup[hashedFieldName] = enumValue;
                    }
                });
        }
    }
}
