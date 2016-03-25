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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.BatchJob.v201603;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Google.Api.Ads.AdWords.Tests.Util.BatchJob.v201603 {

  /// <summary>
  /// Tests for <see cref="BatchJobUtilities" /> class.
  /// </summary>
  public class BatchJobUtilitiesTest : BatchJobUtilities {

    /// <summary>
    /// The test user for initializing purposes.
    /// </summary>
    private AdWordsUser TEST_USER = new AdWordsUser();

    /// <summary>
    /// The test data for upload purposes.
    /// </summary>
    private readonly byte[] TEST_DATA = Encoding.UTF8.GetBytes(string.Concat(
        Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 1000000)));

    /// <summary>
    /// A random chunk size that is not multiple of 256K.
    /// </summary>
    private const int TEST_NON_MULTIPLE_CHUNK_SIZE = 10000;

    /// <summary>
    /// The upload progress for testing resumable uploads.
    /// </summary>
    private const int TESTDATA_UPLOAD_PROGRESS = 123456;

    /// <summary>
    /// A random chunk size for testing chunked uploads.
    /// </summary>
    private const int TEST_CHUNK_SIZE = CHUNK_SIZE_ALIGN * 12;

    /// <summary>
    /// Class to keep track of parameters passed to a <see cref="UploadChunk"/> method.
    /// </summary>
    public class UploadChunkRecord {

      /// <summary>
      /// Gets or sets the start of the chunk.
      /// </summary>
      public int start { get; set; }

      /// <summary>
      /// Gets or sets the end of the chunk.
      /// </summary>
      public int end { get; set; }
    }

    /// <summary>
    /// An array to keep track of calls to the mocked <see cref="UploadChunk"/>
    /// method.
    /// </summary>
    private List<UploadChunkRecord> uploadChunkRecords = new List<UploadChunkRecord>();

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    [SetUp]
    public void Init() {
    }

    /// <summary>
    /// Tests for <see cref="Init"/> method.
    /// </summary>
    [Test]
    public void TestInit() {
      // Any chunk size that is not a multiple of 256K should throw an
      // exception if chunking is turned on.
      Assert.Throws(typeof(ArgumentException), delegate() {
        this.Init(TEST_USER, true, TEST_NON_MULTIPLE_CHUNK_SIZE);
      });

      // Any chunk size that is a multiple of 256K should not throw an
      // exception if chunking is turned on.
      Assert.DoesNotThrow(delegate() {
        this.Init(TEST_USER, true, CHUNK_SIZE_ALIGN * 12);
      });

      // Chunk size ignored if chunking is false.
      Assert.DoesNotThrow(delegate() {
        this.Init(TEST_USER, false, TEST_NON_MULTIPLE_CHUNK_SIZE);
      });
    }

    /// <summary>
    /// Tests for uploads without chunking.
    /// </summary>
    [Test]
    public void TestUploadNoChunking() {
      uploadChunkRecords.Clear();

      // Upload with chunking turned off.
      this.Init(TEST_USER, false, TEST_NON_MULTIPLE_CHUNK_SIZE);
      Upload("http://www.example.com", false, TEST_DATA);

      // There should be one chunk record that represents the whole data.
      Assert.That(uploadChunkRecords.Count == 1);
      Assert.That(uploadChunkRecords[0].start == 0);
      Assert.That(uploadChunkRecords[0].end == TEST_DATA.Length - 1);
    }

    /// <summary>
    /// Tests for uploads with chunking.
    /// </summary>
    [Test]
    public void TestUploadWithChunking() {
      uploadChunkRecords.Clear();

      int numExpectedRecords = (int) (TEST_DATA.Length / TEST_CHUNK_SIZE) + 1;

      // Upload with chunking turned off.
      this.Init(TEST_USER, true, TEST_CHUNK_SIZE);
      Upload("http://www.example.com", false, TEST_DATA);

      // There should be TESTDATA.Length % TEST_CHUNK_SIZE + 1 chunk records.
      Assert.That(uploadChunkRecords.Count == numExpectedRecords);

      UploadChunkRecord record;

      // There should be NUM_EXPECTED_RECORDS - 1 records of size = TEST_CHUNK_SIZE
      for (int i = 0; i < numExpectedRecords - 1; i++) {
        record = uploadChunkRecords[i];
        Assert.That(record.start == i * TEST_CHUNK_SIZE);
        Assert.That(record.end == record.start + TEST_CHUNK_SIZE - 1);
      }

      // The last record should be the leftover data.
      record = uploadChunkRecords[numExpectedRecords - 1];
      Assert.That(record.start == (numExpectedRecords - 1) * TEST_CHUNK_SIZE);
      Assert.That(record.end == TEST_DATA.Length - 1);
    }

    /// <summary>
    /// Tests for uploads with chunking and resuming an interrupted upload.
    /// </summary>
    [Test]
    public void TestUploadWithResumeAndChunking() {
      uploadChunkRecords.Clear();

      int numExpectedRecords =
          (int) ((TEST_DATA.Length - TESTDATA_UPLOAD_PROGRESS) / TEST_CHUNK_SIZE) + 1;

      // Upload with chunking turned off.
      this.Init(TEST_USER, true, TEST_CHUNK_SIZE);
      Upload("http://www.example.com", true, TEST_DATA);

      // The number of records should match.
      Assert.That(uploadChunkRecords.Count == numExpectedRecords);

      UploadChunkRecord record;

      // There should be NUM_EXPECTED_RECORDS - 1 records of size = TEST_CHUNK_SIZE
      for (int i = 0; i < numExpectedRecords - 1; i++) {
        record = uploadChunkRecords[i];
        Assert.That(record.start == TESTDATA_UPLOAD_PROGRESS + i * TEST_CHUNK_SIZE);
        Assert.That(record.end == record.start + TEST_CHUNK_SIZE - 1);
      }

      // The last record should be the leftover data.
      record = uploadChunkRecords[numExpectedRecords - 1];
      Assert.That(record.start == TESTDATA_UPLOAD_PROGRESS +
          (numExpectedRecords - 1) * TEST_CHUNK_SIZE);
      Assert.That(record.end == TEST_DATA.Length - 1);
    }

    #region Mocked methods

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchJobUtilitiesTest"/> class.
    /// </summary>
    public BatchJobUtilitiesTest()
      : base(new AdWordsUser()) {
    }

    /// <summary>
    /// Uploads a chunk of data for the batch job.
    /// </summary>
    /// <param name="url">The resumable upload URL.</param>
    /// <param name="postBody">The post body.</param>
    /// <param name="start">The start of range of bytes to be uploaded.</param>
    /// <param name="end">The end of range of bytes to be uploaded.</param>
    protected override void UploadChunk(string url, byte[] postBody, int start, int end) {
      uploadChunkRecords.Add(new UploadChunkRecord() {
        start = start,
        end = end
      });
    }

    /// <summary>
    /// Gets the upload progress.
    /// </summary>
    /// <param name="url">The resumable upload URL.</param>
    /// <returns>
    /// The number of bytes uploaded so far.
    /// </returns>
    protected override int GetUploadProgress(string url) {
      return TESTDATA_UPLOAD_PROGRESS;
    }

    #endregion Mocked methods
  }
}