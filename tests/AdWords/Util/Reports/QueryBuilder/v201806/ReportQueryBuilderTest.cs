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
using Google.Api.Ads.AdWords.v201806;

using NUnit.Framework;

using System;

namespace Google.Api.Ads.AdWords.Tests.Util.Reports.QueryBuilder.v201806
{
    /// <summary>
    /// Unit tests for the <see cref="ReportQueryBuilder"/> class.
    /// </summary>
    internal class ReportQueryBuilderTest
    {
        /// <summary>
        /// Tests the standard query construction.
        /// </summary>
        [Test]
        public void TestStandardQueryConstruction()
        {
            ReportQueryBuilder queryBuilder = null;
            string query = null;

            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignId", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Where("Clicks")
                .Equals(20).During(ReportDefinitionDateRangeType.YESTERDAY).Build();

            Assert.AreEqual(
                "SELECT CampaignId, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE Clicks = 20 DURING YESTERDAY", query);
        }

        /// <summary>
        /// Tests if exceptions are thrown when a FROM clause is missing.
        /// </summary>
        [Test]
        public void TestMissingFromClause()
        {
            ReportQueryBuilder queryBuilder = new ReportQueryBuilder();
            Assert.Throws(typeof(System.ApplicationException), delegate()
            {
                string query = queryBuilder.Select("CampaignId", "Status", "Clicks", "Impressions")
                    .Where("Clicks").Equals(20).During(ReportDefinitionDateRangeType.YESTERDAY)
                    .Build();
            });
        }

        /// <summary>
        /// Tests if the query is rendered correctly when a WHERE clause is not specified.
        /// </summary>
        [Test]
        public void TestMissingWhereClause()
        {
            ReportQueryBuilder queryBuilder = new ReportQueryBuilder();
            string query = queryBuilder.Select("CampaignId", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT)
                .During(ReportDefinitionDateRangeType.YESTERDAY).Build();

            Assert.AreEqual(
                "SELECT CampaignId, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT DURING YESTERDAY", query);
        }

        /// <summary>
        /// Tests if query is rendered correctly when a DURING clause is not specified.
        /// </summary>
        [Test]
        public void TestMissingDuringClause()
        {
            ReportQueryBuilder queryBuilder = new ReportQueryBuilder();
            string query = queryBuilder.Select("CampaignId", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Where("Clicks")
                .Equals(20).Build();

            Assert.AreEqual(
                "SELECT CampaignId, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE Clicks = 20", query);
        }

        /// <summary>
        /// Tests an exception is thrown if SELECT clause is not mentioned.
        /// </summary>
        [Test]
        public void TestMissingSelectClause()
        {
            ReportQueryBuilder queryBuilder = null;
            new ReportQueryBuilder();
            string query = null;

            Assert.Throws(typeof(System.ApplicationException), delegate()
            {
                queryBuilder = new ReportQueryBuilder();

                query = queryBuilder.Where("Clicks").Equals(20)
                    .During(ReportDefinitionDateRangeType.YESTERDAY).Build();
            });
            Assert.Throws(typeof(System.ApplicationException), delegate()
            {
                queryBuilder = new ReportQueryBuilder();

                query = queryBuilder.Select().Where("Clicks").Equals(20)
                    .During(ReportDefinitionDateRangeType.YESTERDAY).Build();
            });
        }

        /// <summary>
        /// Tests if string parameters are quoted correctly.
        /// </summary>
        [Test]
        public void TestStringParameters()
        {
            ReportQueryBuilder queryBuilder = null;
            string query = null;

            // Single string parameter.
            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Where("CampaignName")
                .Equals("TEST_CAMPAIGN").During(ReportDefinitionDateRangeType.YESTERDAY).Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE CampaignName = 'TEST_CAMPAIGN' DURING YESTERDAY",
                query);

            // Multiple string parameters.
            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Where("CampaignName")
                .Equals("TEST_CAMPAIGN").Where("Status").Equals("ENABLED")
                .During(ReportDefinitionDateRangeType.YESTERDAY).Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE CampaignName = 'TEST_CAMPAIGN' " +
                "AND Status = 'ENABLED' DURING YESTERDAY", query);

            // String parameters with quotes and backslashes in them.
            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Where("CampaignName")
                .Equals("T'EST_\"CAMPAIGN").Where("Status").Equals("ENA\\BLED")
                .During(ReportDefinitionDateRangeType.YESTERDAY).Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE CampaignName = 'T\\'EST_\\\"CAMPAIGN' " +
                "AND Status = 'ENA\\\\BLED' DURING YESTERDAY", query);
        }

        /// <summary>
        /// Tests if multi-argument functions are quoted correctly.
        /// </summary>
        [Test]
        public void TestMultiArgumentFunctions()
        {
            ReportQueryBuilder queryBuilder = null;
            string query = null;

            // Multi-argument, string parameters and non-string parameters.
            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Where("CampaignName")
                .In("TEST_CAMPAIGN1", "TEST_CAMPAIGN2").Where("CampaignId").In(123, 456)
                .During(ReportDefinitionDateRangeType.YESTERDAY).Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE CampaignName IN " +
                "['TEST_CAMPAIGN1', 'TEST_CAMPAIGN2'] AND CampaignId IN [123, 456] " +
                "DURING YESTERDAY",
                query);
        }

        /// <summary>
        /// Tests the DURING clause with various types of input when
        /// constructing queries.
        /// </summary>
        [Test]
        public void TestDuringClauseInputTypes()
        {
            ReportQueryBuilder queryBuilder = null;
            string query = null;

            // Exception is thrown if minDate is not in yyyyMMdd format.
            Assert.Throws<ArgumentException>(delegate()
            {
                queryBuilder = new ReportQueryBuilder();
                query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                    .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT)
                    .Where("CampaignName").Equals("TEST_CAMPAIGN").During("Foo", "20140301")
                    .Build();
            });

            // Exception is thrown if maxDate is not in yyyyMMdd format.
            Assert.Throws<ArgumentException>(delegate()
            {
                queryBuilder = new ReportQueryBuilder();
                query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                    .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT)
                    .Where("CampaignName").Equals("TEST_CAMPAIGN").During("20140301", "Foo")
                    .Build();
            });

            // DURING query accepts date strings in yyyyMMdd format.
            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Where("CampaignName")
                .Equals("TEST_CAMPAIGN").During("20170101", "20170131").Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE CampaignName = 'TEST_CAMPAIGN' " +
                "DURING 20170101, 20170131", query);

            // DURING query accepts DateTime formats.
            DateTime minDate = new DateTime(2017, 1, 1);
            DateTime maxDate = new DateTime(2017, 1, 31);

            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Where("CampaignName")
                .Equals("TEST_CAMPAIGN").During(minDate, maxDate).Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE CampaignName = 'TEST_CAMPAIGN' " +
                "DURING 20170101, 20170131", query);

            // DURING supports predefined dateranges in string format.
            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Where("CampaignName")
                .Equals("TEST_CAMPAIGN").During("YESTERDAY").Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE CampaignName = 'TEST_CAMPAIGN' " +
                "DURING YESTERDAY", query);

            // Exception is thrown if DURING clause cannot be parsed into a
            // known date range type.
            Assert.Throws<ArgumentException>(delegate()
            {
                queryBuilder = new ReportQueryBuilder();
                query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                    .From("CAMPAIGN_PERFORMANCE_REPORT").Where("CampaignName")
                    .Equals("TEST_CAMPAIGN").During("Foo").Build();
            });
        }

        /// <summary>
        /// Tests the FROM clauses with various types of input when
        /// constructing queries.
        /// </summary>
        [Test]
        public void TestFromClauseInputTypes()
        {
            ReportQueryBuilder queryBuilder = null;
            string query = null;

            // FROM clause accepts ReportDefinitionReportType values.
            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Where("CampaignName")
                .In("TEST_CAMPAIGN1", "TEST_CAMPAIGN2").Where("CampaignId").In(123, 456)
                .During(ReportDefinitionDateRangeType.YESTERDAY).Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE CampaignName IN " +
                "['TEST_CAMPAIGN1', 'TEST_CAMPAIGN2'] AND CampaignId IN [123, 456] " +
                "DURING YESTERDAY",
                query);

            // FROM clause accepts ReportDefinitionReportType values, specified in string format.
            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                .From("CAMPAIGN_PERFORMANCE_REPORT").Where("CampaignName")
                .In("TEST_CAMPAIGN1", "TEST_CAMPAIGN2").Where("CampaignId").In(123, 456)
                .During(ReportDefinitionDateRangeType.YESTERDAY).Build();

            Assert.AreEqual(
                "SELECT CampaignName, Status, Clicks, Impressions FROM " +
                "CAMPAIGN_PERFORMANCE_REPORT WHERE CampaignName IN " +
                "['TEST_CAMPAIGN1', 'TEST_CAMPAIGN2'] AND CampaignId IN [123, 456] " +
                "DURING YESTERDAY",
                query);
            // Exception is thrown if FROM clause cannot be parsed into a
            // known report type.
            Assert.Throws<ArgumentException>(delegate()
            {
                queryBuilder = new ReportQueryBuilder();
                query = queryBuilder.Select("CampaignName", "Status", "Clicks", "Impressions")
                    .From("UNKNOWN_PERFORMANCE_REPORT").Where("CampaignName")
                    .Equals("TEST_CAMPAIGN").During("Foo", "20140301").Build();
            });
        }

        /// <summary>
        /// Tests field conditions for selector.
        /// </summary>
        [Test]
        public void TestFieldsInSelectClause()
        {
            ReportQueryBuilder queryBuilder = null;
            string query = null;

            // Duplicate fields and order are preserved.
            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignId", "Status", "Clicks", "Status")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Build();

            Assert.AreEqual(
                "SELECT CampaignId, Status, Clicks, Status FROM CAMPAIGN_PERFORMANCE_REPORT",
                query);

            // If multiple Select calls are done, only the last instance is kept.
            queryBuilder = new ReportQueryBuilder();
            query = queryBuilder.Select("CampaignId", "Status").Select("Clicks", "Status")
                .From(ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT).Build();

            Assert.AreEqual("SELECT Clicks, Status FROM CAMPAIGN_PERFORMANCE_REPORT", query);
        }
    }
}
