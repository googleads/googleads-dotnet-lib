// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.v201602;

using System;
using System.Collections.Generic;
using System.Text;

using DfpDateTime = Google.Api.Ads.Dfp.v201602.DateTime;

namespace Google.Api.Ads.Dfp.Util.v201602 {
  /// <summary>
  /// A utility class that allows you to build Datetime objects from strings.
  /// </summary>
  public class DateTimeUtilities {
    /// <summary>
    /// Converts the string in format yyyyMMdd HH:mm:ss to a DateTime object
    /// with the specified time zone.
    /// </summary>
    /// <param name="dateString">The date string.</param>
    /// <param name="timeZoneId">The timeZoneId to set.</param>
    /// <returns>A Dfp Datetime object.</returns>
    public static DfpDateTime FromString(string dateString, string timeZoneId) {
      System.DateTime dateTime = System.DateTime.ParseExact(dateString, "yyyyMMdd HH:mm:ss", null);
      return FromDateTime(dateTime, timeZoneId);
    }

    /// <summary>
    /// Converts a System.DateTime object to a Dfp DateTime object with the specified timeZoneId.
    /// Does not perform time zone conversion. This means the returned DateTime value
    /// may not represent the same instant as the System.DateTime value.
    /// </summary>
    /// <param name="dateTime">The DateTime object.</param>
    /// <param name="timeZoneId">The timeZoneId to use.</param>
    /// <returns>A Dfp Datetime object.</returns>
    public static DfpDateTime FromDateTime(System.DateTime dateTime, string timeZoneId) {
      PreconditionUtilities.CheckArgumentNotNull(dateTime, "dateTime");
      PreconditionUtilities.CheckArgumentNotNull(timeZoneId, "timeZoneId");

      DfpDateTime retval = new DfpDateTime();
      retval.date = new Date();
      retval.date.year = dateTime.Year;
      retval.date.month = dateTime.Month;
      retval.date.day = dateTime.Day;
      retval.hour = dateTime.Hour;
      retval.minute = dateTime.Minute;
      retval.second = dateTime.Second;
      retval.timeZoneID = timeZoneId;
      return retval;
    }
  }
}
