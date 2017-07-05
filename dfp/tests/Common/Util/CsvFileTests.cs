// Copyright 2011, Google Inc. All Rights Reserved.
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

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Google.Api.Ads.Common.Tests.Util {
  /// <summary>
  /// UnitTests for <see cref="CsvFile"/> class.
  /// </summary>
  [TestFixture]
  public class CsvFileTests {
    /// <summary>
    /// Test for CsvFile.Write() and CsvFile.Read()
    /// </summary>
    [Test]
    public void TestCsvReadWrite() {
      string fileName = Path.GetTempFileName();

      CsvFile csvDoc = new CsvFile();
      csvDoc.Headers.AddRange(new string[] {"item1", "item2"});
      csvDoc.Records.Add(new string[] {"a", "1"});
      csvDoc.Records.Add(new string[] {"b,c", "2"});
      csvDoc.Records.Add(new string[] {"\"d\", \"e\"", "3"});
      Assert.DoesNotThrow(
          delegate() {
            csvDoc.Write(fileName);
          },
          "CsvFile.Write() should not throw an exception.");

      // Downloaded report should be a valid csv.
      csvDoc = new CsvFile();
      Assert.DoesNotThrow(
          delegate() {
            csvDoc.Read(fileName, true);
          },
          "CsvFile should not throw an exception.");

      Assert.AreEqual(csvDoc.Headers.Count, 2);
      Assert.AreEqual(csvDoc.Headers[0], "item1");
      Assert.AreEqual(csvDoc.Headers[1], "item2");

      Assert.AreEqual(csvDoc.Records.Count, 3);

      Assert.AreEqual(csvDoc.Records[0][0], "a");
      Assert.AreEqual(csvDoc.Records[0][1], "1");
      Assert.AreEqual(csvDoc.Records[1][0], "b,c");
      Assert.AreEqual(csvDoc.Records[1][1], "2");
      Assert.AreEqual(csvDoc.Records[2][0], "\"d\", \"e\"");
      Assert.AreEqual(csvDoc.Records[2][1], "3");
    }
  }
}
