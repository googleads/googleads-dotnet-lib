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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.BatchJob;
using Google.Api.Ads.AdWords.Util.BatchJob.v201809;
using Google.Api.Ads.AdWords.v201809;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Google.Api.Ads.AdWords.Tests.Util.BatchJob.v201809
{
    /// <summary>
    /// Tests for <see cref="BatchJobUtilities" /> class.
    /// </summary>
    public class BatchJobUtilitiesTest : BatchJobUtilities
    {
        /// <summary>
        /// The test user for initializing purposes.
        /// </summary>
        private AdWordsUser TEST_USER = new AdWordsUser();

        /// <summary>
        /// The test data for upload purposes.
        /// </summary>
        private readonly byte[] TEST_DATA =
            Encoding.UTF8.GetBytes(
                string.Concat(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 1000000)));

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
        /// The number of batches for streamed upload.
        /// </summary>
        private const int NUM_BATCHES_FOR_STREAMED_UPLOAD = 10;

        /// <summary>
        /// The number of keyword operations to generate for testing purposes.
        /// </summary>
        private const int NUM_KEYWORD_OPERATIONS = 100000;

        /// <summary>
        /// Class to keep track of parameters passed to a <see cref="UploadChunk"/> method.
        /// </summary>
        public class UploadChunkRecord
        {
            /// <summary>
            /// Gets or sets the start of the chunk.
            /// </summary>
            public int Start { get; set; }

            /// <summary>
            /// Gets or sets the end of the chunk.
            /// </summary>
            public int End { get; set; }

            /// <summary>
            /// Gets or sets the start file offset on the server.
            /// </summary>
            public long StartOffset { get; set; }

            /// <summary>
            /// Gets or sets the total size of the upload.
            /// </summary>
            public long? TotalUploadSize { get; set; }
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
        public void Init()
        {
        }

        /// <summary>
        /// Tests for streaming upload without chunking.
        /// </summary>
        [Test]
        public void TestStreamedUploadNoChunking()
        {
            uploadChunkRecords.Clear();

            // Initialize the uploader for chunked upload.
            this.Init(TEST_USER, false, 0);

            // Generate operations for upload.
            Operation[] operations = GetKeywordOperations(123);

            // Start the upload.
            BatchUploadProgress progress = this.BeginStreamUpload("http://www.example.com");

            // Split the upload into NUM_BATCHES_FOR_STREAMED_UPLOAD batches.
            int[] batchSizes = new int[NUM_BATCHES_FOR_STREAMED_UPLOAD];

            for (int i = 0; i < NUM_BATCHES_FOR_STREAMED_UPLOAD; i++)
            {
                Operation[] operationsToStream =
                    new Operation[NUM_KEYWORD_OPERATIONS / NUM_BATCHES_FOR_STREAMED_UPLOAD];

                int dataLength = Encoding.UTF8.GetBytes(GetPostBody(operationsToStream)).Length;
                int paddedLength = CHUNK_SIZE_ALIGN - (dataLength % CHUNK_SIZE_ALIGN);
                batchSizes[i] = dataLength + paddedLength;

                Array.Copy(operations, i * NUM_BATCHES_FOR_STREAMED_UPLOAD, operationsToStream, 0,
                    NUM_BATCHES_FOR_STREAMED_UPLOAD);
                progress = this.StreamUpload(progress, operationsToStream);
            }

            this.EndStreamUpload(progress);

            // There should be NUM_BATCHES_FOR_STREAMED_UPLOAD + 1 batches.
            Assert.That(uploadChunkRecords.Count == NUM_BATCHES_FOR_STREAMED_UPLOAD + 1);

            // StartOffset tests.
            Assert.AreEqual(0, uploadChunkRecords[0].StartOffset);

            for (int i = 1; i < NUM_BATCHES_FOR_STREAMED_UPLOAD + 1; i++)
            {
                Assert.AreEqual(uploadChunkRecords[i].StartOffset,
                    uploadChunkRecords[i - 1].StartOffset + (uploadChunkRecords[i - 1].End -
                        uploadChunkRecords[i - 1].Start) + 1);
            }

            // Start, End, totalUploadSize tests.
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(0, uploadChunkRecords[i].Start);
                Assert.AreEqual(batchSizes[i] - 1, uploadChunkRecords[i].End);
                Assert.IsNull(uploadChunkRecords[i].TotalUploadSize);
            }

            // Start, End, totalUploadSize tests.
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(0, uploadChunkRecords[i].Start);
                Assert.AreEqual(batchSizes[i] - 1, uploadChunkRecords[i].End);
                Assert.IsNull(uploadChunkRecords[i].TotalUploadSize);
            }

            // Last record.
            Assert.AreEqual(0, uploadChunkRecords[10].Start);
            Assert.AreEqual(POSTAMBLE.Length, uploadChunkRecords[10].End + 1);
            Assert.AreEqual(uploadChunkRecords[10].StartOffset + POSTAMBLE.Length,
                uploadChunkRecords[10].TotalUploadSize);
        }

        /// <summary>
        /// Tests for streaming upload with chunking.
        /// </summary>
        [Test]
        [Category("MissingMonoSupport")]
        public void TestStreamedUploadWithChunking()
        {
            uploadChunkRecords.Clear();

            // Initialize the uploader for chunked upload.
            this.Init(TEST_USER, true, CHUNK_SIZE_ALIGN * 12);

            // Generate operations for upload.
            Operation[] operations = GetKeywordOperations(123);

            // Start the upload.
            BatchUploadProgress progress = this.BeginStreamUpload("http://www.example.com");

            // Split the upload into NUM_BATCHES_FOR_STREAMED_UPLOAD batches.
            int[] batchSizes = new int[NUM_BATCHES_FOR_STREAMED_UPLOAD];

            const int NUM_OPERATIONS_TO_UPLOAD_PER_BATCH =
                NUM_KEYWORD_OPERATIONS / NUM_BATCHES_FOR_STREAMED_UPLOAD;

            long uploadRequestCount = 0;
            for (int i = 0; i < NUM_BATCHES_FOR_STREAMED_UPLOAD; i++)
            {
                Operation[] operationsToStream = new Operation[NUM_OPERATIONS_TO_UPLOAD_PER_BATCH];

                Array.Copy(operations, i * NUM_OPERATIONS_TO_UPLOAD_PER_BATCH, operationsToStream,
                    0, NUM_OPERATIONS_TO_UPLOAD_PER_BATCH);

                long oldProgress = progress.BytesUploaded;
                progress = this.StreamUpload(progress, operationsToStream);
                long additionalDataCount = progress.BytesUploaded - oldProgress;
                uploadRequestCount += (additionalDataCount) / CHUNK_SIZE;
                if ((additionalDataCount % CHUNK_SIZE) != 0)
                {
                    uploadRequestCount += 1;
                }
            }

            this.EndStreamUpload(progress);
            uploadRequestCount += 1;

            // There should be uploadRequestCount entries in uploadChunkRecords.
            Assert.That(uploadChunkRecords.Count == uploadRequestCount);

            for (int i = 0; i < uploadRequestCount - 1; i++)
            {
                long start = uploadChunkRecords[i].StartOffset + uploadChunkRecords[i].Start;
                long end = uploadChunkRecords[i].StartOffset + uploadChunkRecords[i].End;
                long uploaded = end - start;
                // uploaded size is always a multiple of 256K
                if (end - start == CHUNK_SIZE - 1)
                {
                    Assert.Pass(string.Format("Chunk {0} is aligned with CHUNK_SIZE.", i));
                }
                else
                {
                    Assert.That((uploaded + 1) % (256 * 1024) == 0,
                        string.Format("Chunk {0} is not aligned with 256K.", i));
                }
            }
        }

        /// <summary>
        /// Tests for the GetPayload() method.
        /// </summary>
        [Test]
        public void TestGetPayload()
        {
            // Generate operations for upload.
            Operation[] operations = GetKeywordOperations(123);
            string postBody = GetPostBody(operations);

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(postBody);

            string operationsOnly = xDoc.DocumentElement.InnerXml;
            string payload = GetPayload(0, postBody);

            // Ensure that operations are not duplicated in the payload
            // for the initial upload.
            Assert.AreEqual(AllIndexesOf(operationsOnly, payload).Count(), 1);

            // Ensure that the SOAP envelope is not part of the payload
            // if we already have an upload in progress.
            payload = GetPayload(1, postBody);
            Assert.AreEqual(operationsOnly, payload);
        }

        /// <summary>
        /// Gets alls the indexes of <paramref name="needle"/> in <paramref name="haystack"/>.
        /// </summary>
        /// <param name="needle">The substring to search for.</param>
        /// <param name="haystack">The string to search for <paramref name="needle"/>.</param>
        /// <returns>A list of all the indices where match was found.</returns>
        private static List<int> AllIndexesOf(string needle, string haystack)
        {
            var indexes = new List<int>();
            int index = 0;

            do
            {
                index = haystack.IndexOf(needle, index);
                if (index != -1)
                {
                    indexes.Add(index);
                    index++;
                }
            } while (index != -1);

            return indexes;
        }

        /// <summary>
        /// Tests for <see cref="Init"/> method.
        /// </summary>
        [Test]
        [Category("MissingMonoSupport")]
        public void TestInit()
        {
            // Any chunk size that is not a multiple of 256K should throw an
            // exception if chunking is turned on.
            Assert.Throws(typeof(ArgumentException),
                delegate() { this.Init(TEST_USER, true, TEST_NON_MULTIPLE_CHUNK_SIZE); });

            // Any chunk size that is a multiple of 256K should not throw an
            // exception if chunking is turned on.
            Assert.DoesNotThrow(delegate() { this.Init(TEST_USER, true, CHUNK_SIZE_ALIGN * 12); });

            // Chunk size ignored if chunking is false.
            Assert.DoesNotThrow(delegate()
            {
                this.Init(TEST_USER, false, TEST_NON_MULTIPLE_CHUNK_SIZE);
            });
        }

        /// <summary>
        /// Tests for uploads without chunking.
        /// </summary>
        [Test]
        public void TestUploadNoChunking()
        {
            uploadChunkRecords.Clear();

            // Upload with chunking turned off.
            this.Init(TEST_USER, false, TEST_NON_MULTIPLE_CHUNK_SIZE);
            Upload("http://www.example.com", TEST_DATA, 0);

            // There should be one chunk record that represents the whole data.
            Assert.That(uploadChunkRecords.Count == 1);
            Assert.That(uploadChunkRecords[0].Start == 0);
            Assert.That(uploadChunkRecords[0].End == TEST_DATA.Length - 1);
        }

        /// <summary>
        /// Tests for uploads with chunking.
        /// </summary>
        [Test]
        [Category("MissingMonoSupport")]
        public void TestUploadWithChunking()
        {
            uploadChunkRecords.Clear();

            int numExpectedRecords = (int) (TEST_DATA.Length / TEST_CHUNK_SIZE) + 1;

            // Upload with chunking turned off.
            this.Init(TEST_USER, true, TEST_CHUNK_SIZE);
            Upload("http://www.example.com", TEST_DATA, 0);

            // There should be TESTDATA.Length % TEST_CHUNK_SIZE + 1 chunk records.
            Assert.That(uploadChunkRecords.Count == numExpectedRecords);

            UploadChunkRecord record;

            // There should be NUM_EXPECTED_RECORDS - 1 records of size = TEST_CHUNK_SIZE
            for (int i = 0; i < numExpectedRecords - 1; i++)
            {
                record = uploadChunkRecords[i];
                Assert.That(record.Start == i * TEST_CHUNK_SIZE);
                Assert.That(record.End == record.Start + TEST_CHUNK_SIZE - 1);
            }

            // The last record should be the leftover data.
            record = uploadChunkRecords[numExpectedRecords - 1];
            Assert.That(record.Start == (numExpectedRecords - 1) * TEST_CHUNK_SIZE);
            Assert.That(record.End == TEST_DATA.Length - 1);
        }

        /// <summary>
        /// Tests for uploads with chunking and resuming an interrupted upload.
        /// </summary>
        [Test]
        [Category("MissingMonoSupport")]
        public void TestUploadWithResumeAndChunking()
        {
            uploadChunkRecords.Clear();

            Operation[] operations = GetKeywordOperations(1000);
            string postBody = GetPostBody(operations);
            byte[] data = Encoding.UTF8.GetBytes(postBody);

            this.Init(TEST_USER, true, TEST_CHUNK_SIZE);
            Upload("http://www.example.com", operations, true);

            long numExpectedRecords =
                1 + (data.Length - TESTDATA_UPLOAD_PROGRESS) / TEST_CHUNK_SIZE;
            Assert.That(uploadChunkRecords.Count == numExpectedRecords);

            for (int i = 0; i < numExpectedRecords; i++)
            {
                Assert.AreEqual(uploadChunkRecords[i].Start, TEST_CHUNK_SIZE * i);
                if (i == numExpectedRecords - 1)
                {
                    Assert.AreEqual(uploadChunkRecords[i].End,
                        data.Length - TESTDATA_UPLOAD_PROGRESS - 1);
                }
                else
                {
                    Assert.AreEqual(uploadChunkRecords[i].End,
                        uploadChunkRecords[i].Start + TEST_CHUNK_SIZE - 1);
                }

                Assert.AreEqual(uploadChunkRecords[i].StartOffset, TESTDATA_UPLOAD_PROGRESS);
                Assert.AreEqual(uploadChunkRecords[i].TotalUploadSize, data.Length);
            }
        }

        /// <summary>
        /// Tests for GetTextToLog method.
        /// </summary>
        [Test]
        public void TestGetTextToLog()
        {
            // When using ASCII characters only, you should get the actual number of chars
            // requested, since 1 byte == 1 char.
            string textToLog = "ABCDE";
            Assert.AreEqual("ABC", GetTextToLog(Encoding.UTF8.GetBytes(textToLog), 0, 3));

            // If you pass indices out of range of the array, exception is thrown.
            Assert.Throws<ArgumentOutOfRangeException>(delegate()
            {
                GetTextToLog(Encoding.UTF8.GetBytes(textToLog), 10, 20);
            });
            string utf8TextToLog = "こんにちは"; // Hello

            // こ is // \u3053, and its UTF-8 representation is \xe3\x81\x93.
            // So you get back 1 char.
            Assert.AreEqual("こ", GetTextToLog(Encoding.UTF8.GetBytes(utf8TextToLog), 0, 3));

            // When you request 4 bytes, the first 3 bytes are used to decode to こ, and the fourth
            // byte is malformed. So unicode replacement character (\uFFFD) is used.
            Assert.AreEqual("こ\uFFFD", GetTextToLog(Encoding.UTF8.GetBytes(utf8TextToLog), 0, 4));

            // When you request 3 bytes, the stream is misaligned, so you get three unicode
            // replacement characters (\uFFFD\uFFFD\uFFFD).
            Assert.AreEqual("\uFFFD\uFFFD\uFFFD",
                GetTextToLog(Encoding.UTF8.GetBytes(utf8TextToLog), 1, 3));
        }

        /// <summary>
        /// Gets an array of keyword operations for testing upload.
        /// </summary>
        /// <param name="adGroupId">The ad group ID.</param>
        /// <returns>An array of operations.</returns>
        private Operation[] GetKeywordOperations(long adGroupId)
        {
            List<Operation> operations = new List<Operation>();
            for (int i = 0; i < NUM_KEYWORD_OPERATIONS; i++)
            {
                // Create the keyword.
                Keyword keyword = new Keyword();
                keyword.text = "Test keyword " + i;
                keyword.matchType = KeywordMatchType.BROAD;

                // Create the biddable ad group criterion.
                BiddableAdGroupCriterion keywordCriterion = new BiddableAdGroupCriterion();
                keywordCriterion.adGroupId = adGroupId;
                keywordCriterion.criterion = keyword;

                // Optional: Set the user status.
                keywordCriterion.userStatus = UserStatus.PAUSED;

                // Create the operations.
                AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
                operation.@operator = Operator.ADD;
                operation.operand = keywordCriterion;

                operations.Add(operation);
            }

            return operations.ToArray();
        }

        #region Mocked methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchJobUtilitiesTest"/> class.
        /// </summary>
        public BatchJobUtilitiesTest() : base(new AdWordsUser())
        {
        }

        /// <summary>
        /// Uploads a chunk of data for the batch job.
        /// </summary>
        /// <param name="url">The resumable upload URL.</param>
        /// <param name="postBody">The post body.</param>
        /// <param name="start">The start of range of bytes to be uploaded.</param>
        /// <param name="end">The end of range of bytes to be uploaded.</param>
        /// <param name="startOffset">The start offset in the stream to upload to.</param>
        /// <param name="totalUploadSize">If specified, this indicates the total
        /// size of the upload. When doing a streamed upload, this value will be
        /// null for all except the last chunk.</param>
        protected override void UploadChunk(string url, byte[] postBody, int start, int end,
            long startOffset, long? totalUploadSize)
        {
            uploadChunkRecords.Add(new UploadChunkRecord()
            {
                Start = start,
                End = end,
                StartOffset = startOffset,
                TotalUploadSize = totalUploadSize
            });
        }

        /// <summary>
        /// Gets the upload progress.
        /// </summary>
        /// <param name="url">The resumable upload URL.</param>
        /// <returns>
        /// The number of bytes uploaded so far.
        /// </returns>
        protected override int GetUploadProgress(string url)
        {
            return TESTDATA_UPLOAD_PROGRESS;
        }

        #endregion Mocked methods
    }
}
