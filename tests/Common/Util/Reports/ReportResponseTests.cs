// Copyright 2014, Google Inc. All Rights Reserved.
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
using System.IO;
using System.Text;
using System.Net;
using Google.Api.Ads.Common.Util.Reports;
using System.Threading;

namespace Google.Api.Ads.Common.Tests.Util {
  /// <summary>
  /// UnitTests for <see cref="ReportResponse"/> class.
  /// </summary>
  [TestFixture]
  public class ReportResponseTests {
    const string FILE_CONTENTS = "Hello world";

    Uri fileUri = null;
    WebResponse webResponse = null;

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

      webResponse = FileWebRequest.Create(fileUri.AbsoluteUri).GetResponse();
    }

    /// <summary>
    /// Tears down this instance.
    /// </summary>
    [TearDown]
    public void Teardown() {
      webResponse.Close();
    }


    /// <summary>
    /// Tests if report response can be retrieved as a stream.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestGetStream() {
      ReportResponse response = new ReportResponse(webResponse);

      using (MemoryStream stream = new MemoryStream())
      using (StreamWriter writer = new StreamWriter(stream)) {
        writer.Write(FILE_CONTENTS);
        writer.Flush();
        stream.Position = 0;

        FileAssert.AreEqual(stream, response.Stream, "Streams do not match");
      }
    }

    /// <summary>
    /// Tests if report response can be downloaded synchronously.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestDownload() {
      ReportResponse response = new ReportResponse(webResponse);
      this.AssertContentsAreEqual(response.Download());
    }

    /// <summary>
    /// Tests if report response can be downloaded asynchronously.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestDownloadAsync() {
      Boolean success = false;
      ManualResetEvent waiter = new ManualResetEvent(false);
      ReportResponse response = new ReportResponse(webResponse);

      byte[] contents = null;
      response.OnDownloadSuccess = ret => {
        contents = ret;
        success = true;
        waiter.Set();
      };

      response.OnFailed = e => waiter.Set();

      response.DownloadAsync();
      waiter.WaitOne();

      Assert.IsTrue(success, "DownloadAsync triggered OnFailed");
      this.AssertContentsAreEqual(contents);
    }

    /// <summary>
    /// Tests if report response can be saved to file synchronously.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestSave() {
      ReportResponse response = new ReportResponse(webResponse);

      String fileName = Path.GetTempFileName();
      response.Save(fileName);

      this.AssertSaveWasSuccessful(fileName, response);
    }

    /// <summary>
    /// Tests if report response can be downloaded asynchronously.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestSaveAsync() {
      Boolean success = false;
      ManualResetEvent waiter = new ManualResetEvent(false);
      ReportResponse response = new ReportResponse(webResponse);

      response.OnSaveSuccess = () => {
        success = true;
        waiter.Set();
      };

      response.OnFailed = e => waiter.Set();

      String fileName = Path.GetTempFileName();
      response.SaveAsync(fileName);
      waiter.WaitOne();

      Assert.IsTrue(success, "SaveAsync triggered OnFailed.");
      this.AssertSaveWasSuccessful(fileName, response);
    }

    private void AssertContentsAreEqual(byte[] contents) {
      Assert.IsNotNull(contents);
      Assert.AreEqual(Encoding.UTF8.GetBytes(FILE_CONTENTS), contents, "Byte arrays do not match");
    }

    private void AssertSaveWasSuccessful(string fileName, ReportResponse response) {
      Assert.AreEqual(fileName, response.Path);
      using (StreamReader reader = new StreamReader(fileName)) {
        Assert.AreEqual(FILE_CONTENTS, reader.ReadToEnd(), "File contents do not match");
      }
    }
  }
}
