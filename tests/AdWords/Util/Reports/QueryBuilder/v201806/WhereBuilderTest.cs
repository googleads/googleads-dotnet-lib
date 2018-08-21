// Copyright 2018 Google LLC
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

using Google.Api.Ads.AdWords.Util.Reports.v201806;

using NUnit.Framework;

namespace Google.Api.Ads.AdWords.Tests.Util.Reports.QueryBuilder.v201806
{
    /// <summary>
    /// Unit tests for the <see cref="WhereBuilder"/> class.
    /// </summary>
    internal class WhereBuilderTest
    {
        private class MockWhereBuilder : WhereBuilder<MockWhereBuilder>
        {
            internal MockWhereBuilder(string field) : base()
            {
                Init(field, this);
            }
        }

        private const string TEST_FIELDNAME = "CampaignId";
        private MockWhereBuilder builder = new MockWhereBuilder(TEST_FIELDNAME);

        private void TestCondition(string builderCondition, string expectedCondition)
        {
            Assert.AreEqual(string.Format("{0}{1}", TEST_FIELDNAME, expectedCondition),
                builderCondition);
        }

        /// <summary>
        /// Tests the Equals method.
        /// </summary>
        [Test]
        public void TestEquals()
        {
            TestCondition(builder.Equals("A").ToString(), " = 'A'");
            TestCondition(builder.Equals(20).ToString(), " = 20");
            TestCondition(builder.Equals(false).ToString(), " = FALSE");
            TestCondition(builder.Equals(20L).ToString(), " = 20");
        }

        /// <summary>
        /// Tests the NotEquals method.
        /// </summary>
        [Test]
        public void TestNotEquals()
        {
            TestCondition(builder.NotEquals("A").ToString(), " != 'A'");
            TestCondition(builder.NotEquals(20).ToString(), " != 20");
            TestCondition(builder.NotEquals(false).ToString(), " != FALSE");
            TestCondition(builder.NotEquals(20L).ToString(), " != 20");
        }

        /// <summary>
        /// Tests the <code>In</code> method.
        /// </summary>
        [Test]
        public void TestIn()
        {
            TestCondition(builder.In("A", "B").ToString(), " IN ['A', 'B']");
            TestCondition(builder.In(20, 30).ToString(), " IN [20, 30]");
        }

        /// <summary>
        /// Tests the <code>NotIn</code> method.
        /// </summary>
        [Test]
        public void TestNotIn()
        {
            TestCondition(builder.NotIn("A", "B").ToString(), " NOT_IN ['A', 'B']");
            TestCondition(builder.NotIn(20, 30).ToString(), " NOT_IN [20, 30]");
        }

        /// <summary>
        /// Tests the <code>Contains</code> method.
        /// </summary>
        [Test]
        public void TestContains()
        {
            TestCondition(builder.Contains("A").ToString(), " CONTAINS 'A'");
        }

        /// <summary>
        /// Tests the <code>ContainsIgnoreCase</code> method.
        /// </summary>
        [Test]
        public void TestContainsIgnoreCase()
        {
            TestCondition(builder.ContainsIgnoreCase("A").ToString(), " CONTAINS_IGNORE_CASE 'A'");
        }

        /// <summary>
        /// Tests the <code>DoesNotContain</code> method.
        /// </summary>
        [Test]
        public void TestDoesNotContain()
        {
            TestCondition(builder.DoesNotContain("A").ToString(), " DOES_NOT_CONTAIN 'A'");
        }

        /// <summary>
        /// Tests the <code>DoesNotContainIgnoreCase</code> method.
        /// </summary>
        [Test]
        public void TestDoesNotContainIgnoreCase()
        {
            TestCondition(builder.DoesNotContainIgnoreCase("A").ToString(),
                " DOES_NOT_CONTAIN_IGNORE_CASE 'A'");
        }

        /// <summary>
        /// Tests the <code>ContainsAll</code> method.
        /// </summary>
        [Test]
        public void TestContainsAll()
        {
            TestCondition(builder.ContainsAll("A", "B").ToString(), " CONTAINS_ALL ['A', 'B']");
            TestCondition(builder.ContainsAll(20, 30).ToString(), " CONTAINS_ALL [20, 30]");
        }

        /// <summary>
        /// Tests the <code>ContainsAny</code> method.
        /// </summary>
        [Test]
        public void TestContainsAny()
        {
            TestCondition(builder.ContainsAny("A", "B").ToString(), " CONTAINS_ANY ['A', 'B']");
            TestCondition(builder.ContainsAny(20, 30).ToString(), " CONTAINS_ANY [20, 30]");
        }

        /// <summary>
        /// Tests the <code>ContainsNone</code> method.
        /// </summary>
        [Test]
        public void TestContainsNone()
        {
            TestCondition(builder.ContainsNone("A", "B").ToString(), " CONTAINS_NONE ['A', 'B']");
            TestCondition(builder.ContainsNone(20, 30).ToString(), " CONTAINS_NONE [20, 30]");
            TestCondition(builder.ContainsNone(20.3, 30.4).ToString(),
                " CONTAINS_NONE [20.3, 30.4]");
            TestCondition(builder.ContainsNone(20L, 30L).ToString(), " CONTAINS_NONE [20, 30]");
        }

        /// <summary>
        /// Tests the <code>LessThan</code> method.
        /// </summary>
        [Test]
        public void TestLessThan()
        {
            TestCondition(builder.LessThan(20).ToString(), " < 20");
            TestCondition(builder.LessThan(20L).ToString(), " < 20");
            TestCondition(builder.LessThan(20.3).ToString(), " < 20.3");
        }

        /// <summary>
        /// Tests the <code>LessThanOrEqualTo</code> method.
        /// </summary>
        [Test]
        public void TestLessThanOrEqualTo()
        {
            TestCondition(builder.LessThanOrEqualTo(20).ToString(), " <= 20");
            TestCondition(builder.LessThanOrEqualTo(20L).ToString(), " <= 20");
        }

        /// <summary>
        /// Tests the <code>GreaterThan</code> method.
        /// </summary>
        [Test]
        public void TestGreaterThan()
        {
            TestCondition(builder.GreaterThan(20).ToString(), " > 20");
            TestCondition(builder.GreaterThan(20L).ToString(), " > 20");
            TestCondition(builder.GreaterThan(20.3).ToString(), " > 20.3");
        }

        /// <summary>
        /// Tests the <code>GreaterThanOrEqualTo</code> method.
        /// </summary>
        [Test]
        public void TestGreaterThanOrEqualTo()
        {
            TestCondition(builder.GreaterThanOrEqualTo(20).ToString(), " >= 20");
            TestCondition(builder.GreaterThanOrEqualTo(20L).ToString(), " >= 20");
        }

        /// <summary>
        /// Tests the <code>StartsWith</code> method.
        /// </summary>
        [Test]
        public void TestStartsWith()
        {
            TestCondition(builder.StartsWith("A").ToString(), " STARTS_WITH 'A'");
        }

        /// <summary>
        /// Tests the <code>StartsWithIgnoreCase</code> method.
        /// </summary>
        [Test]
        public void TestStartsWithIgnoreCase()
        {
            TestCondition(builder.StartsWithIgnoreCase("A").ToString(),
                " STARTS_WITH_IGNORE_CASE 'A'");
        }
    }
}
