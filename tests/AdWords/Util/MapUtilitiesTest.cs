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

using Google.Api.Ads.AdWords.Util;

using NUnit.Framework;

using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Tests.Util
{
    /// <summary>
    /// Tests for MapUtilities class.
    /// </summary>
    public class MapUtilitiesTest
    {
        private class MockString_StringMapEntry : IMapEntry<string, string>
        {
            public string key { get; set; }

            public string value { get; set; }
        }

        /// <summary>
        /// Internal MapUtils instance for testing purposes.
        /// </summary>
        private MapUtilities<string, string> mapUtils;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.mapUtils = new MapUtilities<string, string>();
        }

        /// <summary>
        /// Tests the Add method.
        /// </summary>
        [Test]
        public void TestAddKeyValue()
        {
            mapUtils.Add("key1", "value1");
            mapUtils.Add("key2", "value2");
            MockString_StringMapEntry[] entries = mapUtils.AsArray<MockString_StringMapEntry>();
            Assert.AreEqual(2, entries.Length);
            Assert.AreEqual(entries[0].key, "key1");
            Assert.AreEqual(entries[0].value, "value1");
            Assert.AreEqual(entries[1].key, "key2");
            Assert.AreEqual(entries[1].value, "value2");
        }

        /// <summary>
        /// Tests the AddEntry method.
        /// </summary>
        [Test]
        public void TestAddEntry()
        {
            var item1 = new MockString_StringMapEntry()
            {
                key = "key3",
                value = "value3"
            };
            var item2 = new MockString_StringMapEntry()
            {
                key = "key4",
                value = "value4"
            };

            mapUtils.AddEntry(item1);
            mapUtils.AddEntry(item2);

            MockString_StringMapEntry[] entries = mapUtils.AsArray<MockString_StringMapEntry>();
            Assert.AreEqual(2, entries.Length);
            Assert.AreEqual(entries[0].key, item1.key);
            Assert.AreEqual(entries[0].value, item1.value);
            Assert.AreEqual(entries[1].key, item2.key);
            Assert.AreEqual(entries[1].value, item2.value);
        }

        /// <summary>
        /// Tests the AddEntries method.
        /// </summary>
        [Test]
        public void TestAddEntries()
        {
            MockString_StringMapEntry item1 = new MockString_StringMapEntry()
            {
                key = "key5",
                value = "value5"
            };

            MockString_StringMapEntry item2 = new MockString_StringMapEntry()
            {
                key = "key6",
                value = "value6"
            };

            mapUtils.AddEntries(new MockString_StringMapEntry[]
            {
                item1,
                item2
            });

            MockString_StringMapEntry[] entries = mapUtils.AsArray<MockString_StringMapEntry>();
            Assert.AreEqual(2, entries.Length);
            Assert.AreEqual(entries[0].key, item1.key);
            Assert.AreEqual(entries[0].value, item1.value);
            Assert.AreEqual(entries[1].key, item2.key);
            Assert.AreEqual(entries[1].value, item2.value);
        }

        /// <summary>
        /// Tests the AsDict method.
        /// </summary>
        [Test]
        public void TestAsDict()
        {
            MockString_StringMapEntry item1 = new MockString_StringMapEntry()
            {
                key = "key7",
                value = "value7"
            };

            MockString_StringMapEntry item2 = new MockString_StringMapEntry()
            {
                key = "key8",
                value = "value8"
            };

            mapUtils.AddEntries(new MockString_StringMapEntry[]
            {
                item1,
                item2
            });

            Dictionary<string, string> entries = mapUtils.AsDict();
            Assert.AreEqual(2, entries.Count);
            Assert.True(entries.ContainsKey("key7"));
            Assert.True(entries.ContainsKey("key8"));
            Assert.AreEqual(entries["key7"], item1.value);
            Assert.AreEqual(entries["key8"], item2.value);
        }
    }
}
