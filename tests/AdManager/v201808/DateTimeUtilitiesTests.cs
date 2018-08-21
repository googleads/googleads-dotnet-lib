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
using Google.Api.Ads.AdManager.Util.v201808;
using Google.Api.Ads.AdManager.v201808;
using NUnit.Framework;
using DateTime = Google.Api.Ads.AdManager.v201808.DateTime;

namespace Google.Api.Ads.AdManager.Tests.v201808 {

  /// <summary>
  /// UnitTests for <see cref="StatementBuilder"/> class.
  /// </summary>
  [TestFixture]
  public class DateTimeUtilitiesTests {

    /// <summary>
    /// Tests creating from System DateTime.
    /// </summary>
    [Test]
    public void TestFromDateTime() {
      System.DateTime dateTime = new System.DateTime(2015, 1, 30, 23, 59, 58);
      DateTime dfpDateTime = DateTimeUtilities.FromDateTime(dateTime, "America/New_York");

      Assert.AreEqual(dfpDateTime.date.year, 2015);
      Assert.AreEqual(dfpDateTime.date.month, 1);
      Assert.AreEqual(dfpDateTime.date.day, 30);
      Assert.AreEqual(dfpDateTime.hour, 23);
      Assert.AreEqual(dfpDateTime.minute, 59);
      Assert.AreEqual(dfpDateTime.second, 58);
      Assert.AreEqual(dfpDateTime.timeZoneID, "America/New_York");
    }

    /// <summary>
    /// Tests that fromDateTime ignores system time zone.
    /// </summary>
    [Test]
    public void TestFromDateTimeIgnoresSystemTimeZone() {
      System.DateTime dateTime = new System.DateTime(2015, 1, 30, 23, 59, 58, DateTimeKind.Utc);
      DateTime dfpDateTime = DateTimeUtilities.FromDateTime(dateTime, "America/New_York");

      Assert.AreEqual(dfpDateTime.date.year, 2015);
      Assert.AreEqual(dfpDateTime.date.month, 1);
      Assert.AreEqual(dfpDateTime.date.day, 30);
      Assert.AreEqual(dfpDateTime.hour, 23);
      Assert.AreEqual(dfpDateTime.minute, 59);
      Assert.AreEqual(dfpDateTime.second, 58);
      Assert.AreEqual(dfpDateTime.timeZoneID, "America/New_York");
    }

    /// <summary>
    /// Tests creating a DFP DateTime from a string.
    /// </summary>
    [Test]
    public void TestFromString() {
      DateTime dfpDateTime = DateTimeUtilities.FromString("20150130 23:59:58", "America/New_York");

      Assert.AreEqual(dfpDateTime.date.year, 2015);
      Assert.AreEqual(dfpDateTime.date.month, 1);
      Assert.AreEqual(dfpDateTime.date.day, 30);
      Assert.AreEqual(dfpDateTime.hour, 23);
      Assert.AreEqual(dfpDateTime.minute, 59);
      Assert.AreEqual(dfpDateTime.second, 58);
      Assert.AreEqual(dfpDateTime.timeZoneID, "America/New_York");
    }

    [Test]
    public void TestToString_nullFormat() {
      Assert.AreEqual("2018-01-01T00:00:00 America/New_York (-05)",
        DateTimeUtilities.ToString(
          new DateTime() {
            date = new Date() {
              year = 2018,
              month = 1,
              day = 1
            },
            hour = 0,
            minute = 0,
            second = 0,
            timeZoneID = "America/New_York"
          }, null)
      );
    }

    [Test]
    public void TestToString_simpleFormat() {
      Assert.AreEqual("2018-01-01",
        DateTimeUtilities.ToString(
          new DateTime() {
            date = new Date() {
              year = 2018,
              month = 1,
              day = 1
            },
            hour = 0,
            minute = 0,
            second = 0,
            timeZoneID = "America/New_York"
          }, "yyyy-MM-dd")
      );
    }
  }
}
