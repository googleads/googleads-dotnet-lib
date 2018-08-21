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

using System.Text.RegularExpressions;

namespace Google.Api.Ads.AdWords.Tests.Util.Reports.QueryBuilder.v201806
{
    /// <summary>
    /// Unit tests for the <see cref="SelectQueryBuilder"/> class.
    /// </summary>
    internal class SelectQueryBuilderTest
    {
        /// <summary>
        /// Tests the standard query construction.
        /// </summary>
        [Test]
        public void TestStandardQueryConstruction()
        {
            SelectQueryBuilder queryBuilder = null;
            string query = null;

            queryBuilder = new SelectQueryBuilder();
            query = queryBuilder.Select("CampaignId", "Status", "Clicks", "Impressions")
                .Where("Clicks").Equals(20).OrderByAscending("CampaignId")
                .OrderByDescending("Status").Limit(1, 20).Build();

            Assert.AreEqual(
                "SELECT CampaignId, Status, Clicks, Impressions WHERE Clicks = 20 " +
                "ORDER BY CampaignId ASC, Status DESC LIMIT 1, 20", query);
        }

        /// <summary>
        /// Tests an exception is thrown if SELECT clause is not mentioned.
        /// </summary>
        [Test]
        public void TestMissingSelectClause()
        {
            SelectQueryBuilder queryBuilder = null;
            string query = null;

            // Select clause cannot be missing.
            Assert.Throws(typeof(System.ApplicationException), delegate()
            {
                queryBuilder = new SelectQueryBuilder();

                query = queryBuilder.Where("Clicks").Equals(20).Build();
            });

            // Fields cannot be empty.
            Assert.Throws(typeof(System.ApplicationException), delegate()
            {
                queryBuilder = new SelectQueryBuilder();

                query = queryBuilder.Select().Where("Clicks").Equals(20).Build();
            });
        }

        /// <summary>
        /// Tests if the query is rendered correctly when a WHERE clause is not specified.
        /// </summary>
        [Test]
        public void TestMissingWhereClause()
        {
            SelectQueryBuilder queryBuilder = new SelectQueryBuilder();
            string query = queryBuilder.Select("CampaignId", "Status", "Clicks", "Impressions")
                .OrderByAscending("CampaignId").OrderByDescending("Status").Limit(1, 20).Build();

            Assert.AreEqual(
                "SELECT CampaignId, Status, Clicks, Impressions ORDER BY " +
                "CampaignId ASC, Status DESC LIMIT 1, 20", query);
        }

        /// <summary>
        /// Tests if the query is rendered correctly when a ORDER BY clause is not specified.
        /// </summary>
        [Test]
        public void TestMissingOrderByClause()
        {
            SelectQueryBuilder queryBuilder = null;
            string query = null;

            queryBuilder = new SelectQueryBuilder();
            query = queryBuilder.Select("CampaignId", "Status", "Clicks", "Impressions")
                .Where("Clicks").Equals(20).Limit(1, 20).Build();

            Assert.AreEqual(
                "SELECT CampaignId, Status, Clicks, Impressions WHERE Clicks = 20 " + "LIMIT 1, 20",
                query);
        }

        /// <summary>
        /// Tests if the query is rendered correctly when a LIMIT clause is not specified.
        /// </summary>
        [Test]
        public void TestMissingLimitClause()
        {
            SelectQueryBuilder queryBuilder = null;
            string query = null;

            queryBuilder = new SelectQueryBuilder();
            query = queryBuilder.Select("CampaignId", "Status", "Clicks", "Impressions")
                .Where("Clicks").Equals(20).OrderByAscending("CampaignId")
                .OrderByDescending("Status").Build();

            Assert.AreEqual(
                "SELECT CampaignId, Status, Clicks, Impressions WHERE Clicks = 20 " +
                "ORDER BY CampaignId ASC, Status DESC", query);
        }

        /// <summary>
        /// Tests if string parameters are quoted correctly.
        /// </summary>
        [Test]
        public void TestStringParameters()
        {
            SelectQueryBuilder queryBuilder = null;
            string query = null;

            // Single string parameter.
            queryBuilder = new SelectQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .Where("CampaignName").Equals("TEST_CAMPAIGN").Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions WHERE " +
                "CampaignName = 'TEST_CAMPAIGN'", query);

            // Multiple string parameters.
            queryBuilder = new SelectQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .Where("CampaignName").Equals("TEST_CAMPAIGN").Where("Status").Equals("ENABLED")
                .Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions WHERE " +
                "CampaignName = 'TEST_CAMPAIGN' AND Status = 'ENABLED'", query);

            // String parameters with quotes and backslashes in them.
            queryBuilder = new SelectQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .Where("CampaignName").Equals("T'EST_\"CAMPAIGN").Where("Status")
                .Equals("ENA\\BLED").Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions WHERE " +
                "CampaignName = 'T\\'EST_\\\"CAMPAIGN' AND Status = 'ENA\\\\BLED'", query);
        }

        /// <summary>
        /// Tests if multi-argument functions are quoted correctly.
        /// </summary>
        [Test]
        public void TestMultiArgumentFunctions()
        {
            SelectQueryBuilder queryBuilder = null;
            string query = null;

            // Multi-argument, string parameters and non-string parameters.
            queryBuilder = new SelectQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .Where("CampaignName").In("TEST_CAMPAIGN1", "TEST_CAMPAIGN2").Where("CampaignId")
                .In(123, 456).Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions WHERE " +
                "CampaignName IN ['TEST_CAMPAIGN1', 'TEST_CAMPAIGN2'] AND CampaignId IN [123, 456]",
                query);
        }

        /// <summary>
        /// Tests field conditions for selector.
        /// </summary>
        [Test]
        public void TestFieldsInSelectClause()
        {
            SelectQueryBuilder queryBuilder = null;
            string query = null;

            // Duplicate fields are removed.
            queryBuilder = new SelectQueryBuilder();
            query = queryBuilder.Select("CampaignId", "Status", "Clicks", "Status").Build();
            int count = new Regex(Regex.Escape("Status")).Matches(query).Count;

            Assert.AreEqual(1, count);

            // If multiple Select calls are done, only the last instance is kept.
            queryBuilder = new SelectQueryBuilder();
            query = queryBuilder.Select("CampaignId", "Status").Select("Clicks", "Status").Build();

            Assert.AreEqual("SELECT Clicks, Status", query);
        }
    }
}
