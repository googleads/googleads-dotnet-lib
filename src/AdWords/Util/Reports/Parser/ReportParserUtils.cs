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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// A class holding utility functions for parsing reports.
    /// </summary>
    public static class ReportParserUtils
    {
        /// <summary>
        /// Sets a property of an object to a given Value. Tries to parse the given string Value
        /// into the correct type in the object.
        /// </summary>
        /// <typeparam name="A">Some type</typeparam>
        /// <param name="colVal">Holds the name of the property to set and the Value,
        /// both in string.</param>
        /// <param name="record">The object of type A whose property will be set.</param>
        /// <param name="onError">A callback action that is given the record and the
        /// value that caused a parsing error.</param>
        public static void SetColumnValue<A>(ColumnValuePair colVal, A record,
            Action<ColumnValuePair, A> onError)
        {
            if (onError == null)
            {
                throw new ArgumentNullException("The callback action cannot be null.");
            }

            var propName = GetPropNameFromColName<A>(colVal.ColName);
            var property = record.GetType().GetProperty(propName);
            var attribType = property.PropertyType;

            try
            {
                object val;
                if (attribType == typeof(double))
                {
                    val = Double.Parse(colVal.Value.TrimEnd('%'), NumberStyles.Any);
                }
                else if (attribType.IsEnum)
                {
                    val = Enum.Parse(attribType, colVal.Value);
                }
                else if (attribType == typeof(string))
                {
                    val = colVal.Value;
                }
                else if (attribType == typeof(decimal))
                {
                    val = Decimal.Parse(colVal.Value.TrimEnd('%'), NumberStyles.Any);
                }
                else if (attribType == typeof(int))
                {
                    val = Int32.Parse(colVal.Value);
                }
                else if (attribType == typeof(long))
                {
                    val = Int64.Parse(colVal.Value);
                }
                else
                {
                    throw new NotSupportedException("Column " + colVal.ColName + " has type " +
                        attribType.Name + ", which is not a supported type.");
                }

                property.SetValue(record, val);
            }
            catch (FormatException)
            {
                onError(colVal, record);
            }
        }

        /// <summary>
        /// Gets the property name from type A that is associated with the given column name.
        /// </summary>
        /// <typeparam name="A">The POCO type</typeparam>
        /// <param name="colName">The column name</param>
        /// <returns>The property name associated with the column name</returns>
        public static string GetPropNameFromColName<A>(string colName)
        {
            var colNamePropNamePair = GetColAndPropNames<A>()
                .FirstOrDefault(x => colName.Equals(x.Item1) || colName.Equals(x.Item2));

            if (colNamePropNamePair == null)
            {
                throw new ArgumentException(
                    "There are no properties annotated with the given column name");
            }

            return colNamePropNamePair.Item2;
        }

        /// <summary>
        /// Returns pairs of column names and property names of the POCO type A. The first element
        /// of each pair is the column name (if present), and the second element is the property
        /// name.
        /// </summary>
        /// <typeparam name="A">The POCO type</typeparam>
        /// <returns>Pairs of column names (if given) and property names</returns>
        public static IEnumerable<Tuple<string, string>> GetColAndPropNames<A>()
        {
            foreach (var property in typeof(A).GetMembers())
            {
                var reportCol = (ReportColumn) property.GetCustomAttributes(false)
                    .FirstOrDefault(attribute => attribute.GetType() == typeof(ReportColumn));
                if (reportCol != null)
                {
                    yield return new Tuple<string, string>(reportCol.ColumnName, property.Name);
                }
            }
        }

        /// <summary>
        /// Returns the names of the given type that are annotated [ReportColumn]
        /// </summary>
        /// <typeparam name="A">The given type.</typeparam>
        /// <returns>A list of names of the properties.</returns>
        public static IEnumerable<string> GetColumnNamesFromPocoType<A>()
        {
            return GetColAndPropNames<A>().Select(x => x.Item1 ?? x.Item2);
        }

        /// <summary>
        /// Checks that the given POCO type is valid and doesn't contain repeated column names
        /// </summary>
        public static void ValidatePocoType<A>()
        {
            var colNames = ReportParserUtils.GetColumnNamesFromPocoType<A>();
            var uniqueNames = colNames.ToDictionary(name => name);
            if (colNames.Count() != uniqueNames.Count())
            {
                throw new System.ArgumentException(
                    "The given row type contains duplicate column names.");
            }
        }
    }
}
