// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Tests.Mocks;
using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System.IO.Compression;

namespace Google.Api.Ads.Common.Tests.Util {
  /// <summary>
  /// UnitTests for <see cref="CsvFile"/> class.
  /// </summary>
  [TestFixture]
  public class MediaUtilitiesTests {
    const string FILE_CONTENTS = "Hello world";

    Uri fileUri = null;
    byte[] compressedData;
    MemoryStream sourceStream;
    MemoryStream targetStream;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      String fileName = Path.GetTempFileName();
      using (StreamWriter writer = new StreamWriter(fileName)) {
        writer.Write(FILE_CONTENTS);
      }
      fileUri = new Uri(fileName);

      MemoryStream dataStream = new MemoryStream();
      byte[] data = Encoding.UTF8.GetBytes(FILE_CONTENTS);
      using (GZipStream gzipStream = new GZipStream(dataStream, CompressionMode.Compress)) {
        gzipStream.Write(data, 0, data.Length);
      }
      compressedData = dataStream.ToArray();

      sourceStream = new MemoryStream();
      targetStream = new MemoryStream();
      sourceStream.Write(data, 0, data.Length);
      sourceStream.Seek(0, SeekOrigin.Begin);
    }

    /// <summary>
    /// Tears down this instance.
    /// </summary>
    [TearDown]
    public void Teardown() {
      sourceStream.Close();
      targetStream.Close();
    }


    /// <summary>
    /// Tests if data can be retrieved from a url.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestGetAssetDataFromUrl1() {
      byte[] data = MediaUtilities.GetAssetDataFromUrl(fileUri, new MockAppConfig());
      Assert.AreEqual(FILE_CONTENTS, Encoding.UTF8.GetString(data));
    }

    /// <summary>
    /// Tests if data can be retrieved from a url.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestGetAssetDataFromUrl2() {
      byte[] data = MediaUtilities.GetAssetDataFromUrl(fileUri.AbsoluteUri, new MockAppConfig());
      Assert.AreEqual(FILE_CONTENTS, Encoding.UTF8.GetString(data));
    }

    /// <summary>
    /// Tests if data can be deflated properly.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestDeflateGZipData() {
      byte[] deflatedData = MediaUtilities.DeflateGZipData(compressedData);
      Assert.AreEqual(FILE_CONTENTS, Encoding.UTF8.GetString(deflatedData));
    }

    /// <summary>
    /// Tests if data can be read from a stream as string.
    /// preview.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestGetStreamContentsAsString() {
      string contents = MediaUtilities.GetStreamContentsAsString(sourceStream);
      Assert.AreEqual(FILE_CONTENTS, contents);
    }
  }
}
