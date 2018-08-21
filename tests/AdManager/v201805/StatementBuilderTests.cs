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

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v201805;
using Google.Api.Ads.AdManager.v201805;

using NUnit.Framework;

using DateTime = Google.Api.Ads.AdManager.v201805.DateTime;

namespace Google.Api.Ads.AdManager.Tests.v201805
{
    /// <summary>
    /// UnitTests for <see cref="StatementBuilder"/> class.
    /// </summary>
    [TestFixture]
    public class StatementBuilderTests
    {
        private static bool StatementsAreEqual(Statement s1, Statement s2)
        {
            // Assumes both Statements are non-null and have non-null properties
            if (String.Compare(s1.query, s2.query, true) != 0)
            {
                return false;
            }

            if (s1.values.Length != s2.values.Length)
            {
                return false;
            }

            // Assumes Statement values are in the same order
            for (int i = 0; i < s1.values.Length; i++)
            {
                if (!StringValueMapEntriesAreEqual(s1.values[i], s2.values[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool StringValueMapEntriesAreEqual(String_ValueMapEntry entry1,
            String_ValueMapEntry entry2)
        {
            return String.Equals(entry1.key, entry2.key) &&
                ValuesAreEqual(entry1.value, entry2.value);
        }

        private delegate bool ValueEqualityDelegate(Value value1, Value value2);

        private static bool ValuesAreEqual(Value value1, Value value2)
        {
            Dictionary<Type, ValueEqualityDelegate> switcher =
                new Dictionary<Type, ValueEqualityDelegate>();
            switcher.Add(typeof(TextValue), new ValueEqualityDelegate(TextValuesAreEqual));
            switcher.Add(typeof(NumberValue), new ValueEqualityDelegate(NumberValuesAreEqual));
            switcher.Add(typeof(BooleanValue), new ValueEqualityDelegate(BooleanValuesAreEqual));
            switcher.Add(typeof(SetValue), new ValueEqualityDelegate(SetValuesAreEqual));
            switcher.Add(typeof(DateValue), new ValueEqualityDelegate(DateValuesAreEqual));
            switcher.Add(typeof(DateTimeValue), new ValueEqualityDelegate(DateTimeValuesAreEqual));

            if (switcher.ContainsKey(value1.GetType()))
            {
                return switcher[value1.GetType()](value1, value2);
            }

            throw new NotImplementedException(String.Format("Unknown Value type {0}",
                value1.GetType()));
        }

        private static bool TextValuesAreEqual(Value value1, Value value2)
        {
            return String.Equals((value1 as TextValue).value, (value2 as TextValue).value);
        }

        private static bool NumberValuesAreEqual(Value value1, Value value2)
        {
            return String.Equals((value1 as NumberValue).value, (value2 as NumberValue).value);
        }

        private static bool BooleanValuesAreEqual(Value value1, Value value2)
        {
            return (value1 as BooleanValue).value == (value2 as BooleanValue).value;
        }

        private static bool SetValuesAreEqual(Value value1, Value value2)
        {
            Value[] set1 = (value1 as SetValue).values;
            Value[] set2 = (value2 as SetValue).values;

            if (set1.Length != set2.Length)
            {
                return false;
            }

            for (int i = 0; i < set1.Length; i++)
            {
                if (!ValuesAreEqual(set1[i], set2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool DateValuesAreEqual(Value value1, Value value2)
        {
            Date date1 = (value1 as DateValue).value;
            Date date2 = (value2 as DateValue).value;
            return DatesAreEqual(date1, date2);
        }

        private static bool DateTimeValuesAreEqual(Value value1, Value value2)
        {
            DateTime dateTime1 = (value1 as DateTimeValue).value;
            DateTime dateTime2 = (value2 as DateTimeValue).value;
            return DatesAreEqual(dateTime1.date, dateTime2.date) &&
                dateTime1.hour == dateTime2.hour && dateTime1.minute == dateTime2.minute &&
                dateTime1.second == dateTime2.second &&
                String.Equals(dateTime1.timeZoneID, dateTime2.timeZoneID);
        }

        private static bool DatesAreEqual(Date date1, Date date2)
        {
            return date1.day == date2.day && date1.month == date2.month && date1.year == date2.year;
        }

        /// <summary>
        /// Tests the statement builder with a blank statement.
        /// </summary>
        [Test]
        public void TestStatementBuilderBlankStatement()
        {
            StatementBuilder statementBuilder = new StatementBuilder();

            Statement expectedStatement = new Statement();
            expectedStatement.query = "";
            expectedStatement.values = new String_ValueMapEntry[0];

            Assert.True(StatementsAreEqual(expectedStatement, statementBuilder.ToStatement()));
        }

        /// <summary>
        /// Tests the statement builder with a basic statement.
        /// </summary>
        [Test]
        public void TestStatementBuilderBasicStatement()
        {
            StatementBuilder statementBuilder = new StatementBuilder().Where("ID = 1");

            Statement expectedStatement = new Statement();
            expectedStatement.query = "WHERE ID = 1";
            expectedStatement.values = new String_ValueMapEntry[0];

            Assert.True(StatementsAreEqual(expectedStatement, statementBuilder.ToStatement()));
        }

        /// <summary>
        /// Tests that the statement builder ignores keywords.
        /// </summary>
        [Test]
        public void TestStatementBuilderIgnoresKeyword()
        {
            StatementBuilder statementBuilder = new StatementBuilder().Where("WHERE ID = 1");

            Statement expectedStatement = new Statement();
            expectedStatement.query = "WHERE ID = 1";
            expectedStatement.values = new String_ValueMapEntry[0];

            Assert.True(StatementsAreEqual(expectedStatement, statementBuilder.ToStatement()));
        }

        /// <summary>
        /// Tests that the statement builder includes a keyword prefix.
        /// </summary>
        [Test]
        public void TestStatementBuilderIncludesKeywordPrefix()
        {
            StatementBuilder statementBuilder = new StatementBuilder().Where("WHEREWITHALL = 1");

            Statement expectedStatement = new Statement();
            expectedStatement.query = "WHERE WHEREWITHALL = 1";
            expectedStatement.values = new String_ValueMapEntry[0];

            Assert.True(StatementsAreEqual(expectedStatement, statementBuilder.ToStatement()));
        }

        /// <summary>
        /// Tests all statement builder partial functions.
        /// </summary>
        [Test]
        public void TestAllStatementBuilderPartialFunctions()
        {
            StatementBuilder statementBuilder = new StatementBuilder().Select("Name, Id")
                .From("Geo_Target").Where("Targetable = :targetable").OrderBy("Id DESC")
                .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT).Offset(0).AddValue("targetable", true)
                .IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT).RemoveLimitAndOffset();
            Assert.AreEqual(null, statementBuilder.GetOffset());

            Statement expectedStatement = new Statement();
            expectedStatement.query = "SELECT Name, Id FROM Geo_Target" +
                " WHERE Targetable = :targetable ORDER BY Id DESC";
            String_ValueMapEntry targetableEntry = new String_ValueMapEntry();
            targetableEntry.key = "targetable";
            BooleanValue targetableValue = new BooleanValue();
            targetableValue.value = true;
            targetableEntry.value = targetableValue;
            expectedStatement.values = new String_ValueMapEntry[]
            {
                targetableEntry
            };
            Assert.True(StatementsAreEqual(expectedStatement, statementBuilder.ToStatement()));
        }
    }
}
