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
using Google.Api.Ads.Common.Util;
using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v201902;
using Google.Api.Ads.AdManager.v201902;
using NUnit.Framework;
using DateTime = Google.Api.Ads.AdManager.v201902.DateTime;


namespace Google.Api.Ads.AdManager.Tests.v201902 {

  /// <summary>
  /// UnitTests for <see cref="PqlUtilities"/> class.
  /// </summary>
  [TestFixture]
  class PqlUtilitiesTests {

    [Test]
    public void TestGetObjectTextValue() {
      TextValue textValue = new TextValue();
      textValue.value = "value1";
      Assert.AreEqual("value1", PqlUtilities.GetValue(textValue));
    }

    [Test]
    public void TestGetObjectNumberValue() {
      NumberValue numberValue = new NumberValue();
      numberValue.value = "-1";
      Assert.AreEqual("-1", PqlUtilities.GetValue(numberValue));
    }

    [Test]
    public void TestGetObjectDateValue() {
      Date date = new Date();
      date.year = 2012;
      date.month = 12;
      date.day = 2;
      DateValue dateValue = new DateValue();
      dateValue.value = date;
      Assert.AreEqual(date, PqlUtilities.GetValue(dateValue));
    }

    [Test]
    public void TestGetObjectDateTimeValue() {
      DateTime dateTime = new DateTime();
      Date date = new Date();
      date.year = 2012;
      date.month = 12;
      date.day = 2;
      dateTime.date = date;
      dateTime.hour = 12;
      dateTime.minute = 45;
      dateTime.second = 0;
      dateTime.timeZoneId = "Asia/Shanghai";
      DateTimeValue dateTimeValue = new DateTimeValue();
      dateTimeValue.value = dateTime;
      Assert.AreEqual(dateTime, PqlUtilities.GetValue(dateTimeValue));
    }

    [Test]
    public void TestGetObjectTextSetValue() {
      TextValue textValue1 = new TextValue();
      TextValue textValue2 = new TextValue();
      textValue1.value = "value1";
      textValue2.value = "value2";

      SetValue setValue = new SetValue();
      setValue.values = new Value[] {textValue1, textValue2};
      List<object> value = PqlUtilities.GetValue(setValue) as List<object>;
      Assert.AreEqual(2, value.Count);
      Assert.True(value.Contains("value1"));
      Assert.True(value.Contains("value2"));
    }


    [Test]
    public void TestGetObjectCommaTextSetValue() {
      TextValue textValue1 = new TextValue();
      TextValue textValue2 = new TextValue();
      textValue1.value = "value1";
      textValue2.value = "comma \",\" separated";

      SetValue setValue = new SetValue();
      setValue.values = new Value[] { textValue1, textValue2 };
      List<object> value = PqlUtilities.GetValue(setValue) as List<object>;
      Assert.AreEqual(2, value.Count);
      Assert.True(value.Contains("value1"));
      Assert.True(value.Contains("comma \",\" separated"));
    }

    [Test]
    public void TestGetObjectNumberSetValue() {
      NumberValue numberValue1 = new NumberValue();
      NumberValue numberValue2 = new NumberValue();
      numberValue1.value = "1";
      numberValue2.value = "2";

      SetValue setValue = new SetValue();
      setValue.values = new Value[] { numberValue1, numberValue2 };
      List<object> value = PqlUtilities.GetValue(setValue) as List<object>;
      Assert.AreEqual(2, value.Count);
      Assert.True(value.Contains("1"));
      Assert.True(value.Contains("2"));
    }

    [Test]
    public void TestGetObjectDateSetValue() {
      Date date = new Date();
      date.year = 2012;
      date.month = 12;
      date.day = 2;
      DateValue dateValue = new DateValue();
      dateValue.value = date;

      SetValue setValue = new SetValue();
      setValue.values = new Value[] { dateValue };
      List<object> value = PqlUtilities.GetValue(setValue) as List<object>;
      Assert.AreEqual(1, value.Count);
      Assert.True(value.Contains(date));
    }

    [Test]
    public void TestGetObjectDateTimeSetValue() {
      DateTime dateTime = new DateTime();
      Date date = new Date();
      date.year = 2012;
      date.month = 12;
      date.day = 2;
      dateTime.date = date;
      dateTime.hour = 12;
      dateTime.minute = 45;
      dateTime.second = 0;
      dateTime.timeZoneId = "Asia/Shanghai";
      DateTimeValue dateTimeValue = new DateTimeValue();
      dateTimeValue.value = dateTime;

      SetValue setValue = new SetValue();
      setValue.values = new Value[] { dateTimeValue };
      List<object> value = PqlUtilities.GetValue(setValue) as List<object>;
      Assert.AreEqual(1, value.Count);
      Assert.True(value.Contains(dateTime));
    }

    [Test]
    public void TestGetObjectMixedSetValue() {
      TextValue textValue = new TextValue();
      DateValue dateValue = new DateValue();
      textValue.value = "value1";
      Date date = new Date();
      date.year = 2012;
      date.month = 12;
      date.day = 2;
      dateValue.value = date;

      SetValue setValue = new SetValue();
      setValue.values = new Value[] { textValue, dateValue };
      Assert.Throws<ArgumentException>(() => PqlUtilities.GetValue(setValue));
    }

    [Test]
    public void TestGetObjectNestedSetValue() {
      SetValue setValue = new SetValue();
      SetValue innerSetValue = new SetValue();
      TextValue textValue = new TextValue();
      textValue.value = "value1";
      innerSetValue.values = new Value[] { textValue };
      setValue.values = new Value[] {innerSetValue};
      Assert.Throws<ArgumentException>(() => PqlUtilities.GetValue(setValue));
    }

    [Test]
    public void TestGetTextValueCommaTextSetValue() {
      TextValue textValue1 = new TextValue();
      TextValue textValue2 = new TextValue();
      textValue1.value = "value1";
      textValue2.value = "comma \",\" separated";

      SetValue setValue = new SetValue();
      setValue.values = new Value[] { textValue1, textValue2 };
      Row row = new Row();
      row.values = new Value[] { setValue };
      string[] stringValues = PqlUtilities.GetRowStringValues(row);
      Assert.AreEqual(1, stringValues.Length);
      Assert.AreEqual("value1,\"comma \"\",\"\" separated\"", stringValues[0]);
    }

    [Test]
    public void TestGetTextValueNumberSetValue() {
      NumberValue numberValue1 = new NumberValue();
      NumberValue numberValue2 = new NumberValue();
      numberValue1.value = "1";
      numberValue2.value = "2";

      SetValue setValue = new SetValue();
      setValue.values = new Value[] { numberValue1, numberValue2 };
      Row row = new Row();
      row.values = new Value[] { setValue };
      string[] stringValues = PqlUtilities.GetRowStringValues(row);
      Assert.AreEqual(1, stringValues.Length);
      Assert.AreEqual("1,2", stringValues[0]);
    }
  }
}
