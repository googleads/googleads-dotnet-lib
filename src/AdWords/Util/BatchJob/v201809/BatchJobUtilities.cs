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
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ApiBatchJob = Google.Api.Ads.AdWords.v201809.BatchJob;

namespace Google.Api.Ads.AdWords.Util.BatchJob.v201809
{
    /// <summary>
    /// Utility methods to upload operations for a batch job, and download the
    /// results.
    /// </summary>
    public class BatchJobUtilities : BatchJobUtilitiesBase
    {
        /// <summary>
        /// Wait callback to be used when calling
        /// <see cref="WaitForPendingJob(long,int,WaitCallback)"/> method.
        /// </summary>
        /// <param name="batchJob">The batchjob instance that was retrieved by
        /// the <see cref="WaitForPendingJob(long,int,WaitCallback)"/> method when polling for job
        /// status.</param>
        /// <param name="waitedMilliseconds">The time in milliseconds for which the
        /// <see cref="WaitForPendingJob(long,int,WaitCallback)"/> method has waited so far.</param>
        /// <returns>true, if the <see cref="WaitForPendingJob(long,int,WaitCallback)"/> method should
        /// be cancelled, false otherwise.</returns>
        public delegate bool WaitCallback(ApiBatchJob batchJob, long waitedMilliseconds);

        /// <summary>
        /// The list of batch job statuses that corresponds to the job being in a
        /// pending state.
        /// </summary>
        private readonly HashSet<BatchJobStatus> PENDING_STATUSES = new HashSet<BatchJobStatus>()
        {
            BatchJobStatus.ACTIVE,
            BatchJobStatus.AWAITING_FILE,
            BatchJobStatus.CANCELING
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchJobUtilities"/>
        /// class.
        /// </summary>
        /// <param name="user">AdWords user to be used along with this
        /// utilities object.</param>
        public BatchJobUtilities(AdsUser user) : base(user)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchJobUtilities"/>
        /// class.
        /// </summary>
        /// <param name="user">AdWords user to be used along with this
        /// utilities object.</param>
        /// <param name="useChunking">if the operations should be broken into
        /// smaller chunks before uploading to the server.</param>
        /// <param name="chunkSize">The chunk size to use for resumable upload.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="chunkSize"/>
        /// is not a multiple of 256KB.</exception>
        /// <remarks>Use chunking if your network is spotty for uploads, or if it
        /// has restrictions such as speed limits or timeouts. Chunking makes your
        /// upload reliable when the network is unreliable, but it is inefficient
        /// over a good connection, since an HTTPs request has to be made for every
        /// chunk being uploaded.</remarks>
        public BatchJobUtilities(AdsUser user, bool useChunking, int chunkSize) : base(user,
            useChunking, chunkSize)
        {
        }

        /// <summary>
        /// Gets the post body for sending a request.
        /// </summary>
        /// <param name="operations">The list of operations.</param>
        /// <returns>The POST body, for using in the web request.</returns>
        protected string GetPostBody(IEnumerable<Operation> operations)
        {
            BatchJobMutateRequest request = new BatchJobMutateRequest()
            {
                operations = operations.ToArray()
            };
            return SerializationUtilities.SerializeAsXmlText(request);
        }

        /// <summary>
        /// Uploads the operations to a specified URL.
        /// </summary>
        /// <param name="url">The temporary URL returned by a batch job.</param>
        /// <param name="operations">The list of operations.</param>
        public void Upload(string url, IEnumerable<Operation> operations)
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            Upload(url, operations, false);
        }

        /// <summary>
        /// Uploads the operations to a specified URL.
        /// </summary>
        /// <param name="url">The temporary URL returned by a batch job.</param>
        /// <param name="operations">The list of operations.</param>
        /// <param name="resumePreviousUpload">True, if a previously interrupted
        /// upload should be resumed.</param>
        public void Upload(string url, IEnumerable<Operation> operations, bool resumePreviousUpload)
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            long totalUploaded = 0;

            byte[] postBody = Encoding.UTF8.GetBytes(GetPostBody(operations));
            long totalUploadSize = postBody.Length;

            // If this file has been uploaded partially, just upload the remaining
            // part.
            if (resumePreviousUpload)
            {
                totalUploaded = GetUploadProgress(url);
                byte[] temp = new byte[totalUploadSize - totalUploaded];
                Array.Copy(postBody, totalUploaded, temp, 0, totalUploadSize - totalUploaded);
                postBody = temp;
            }

            Upload(url, postBody, totalUploaded, totalUploadSize);
        }

        /// <summary>
        /// Uploads the operations to a specified URL in a streamed manner.
        /// </summary>
        /// <param name="uploadProgress">The upload progress tracker.</param>
        /// <param name="operations">The list of operations.</param>
        /// <returns>The updated progress tracker.</returns>
        public BatchUploadProgress StreamUpload(BatchUploadProgress uploadProgress,
            IEnumerable<Operation> operations)
        {
            String postBody = GetPostBody(operations);
            return StreamUpload(uploadProgress, postBody);
        }

        /// <summary>
        /// Downloads the batch job results from a specified URL.
        /// </summary>
        /// <param name="url">The download URL from a batch job.</param>
        /// <returns>The results from the batch job.</returns>
        public BatchJobMutateResponse Download(string url)
        {
            return ParseResponse(DownloadResults(url));
        }

        /// <summary>
        /// Parses the response from Google Cloud Storage servers.
        /// </summary>
        /// <param name="contents">The response body.</param>
        /// <returns>A BatchJobMutateResponse object, generated by parsing the
        /// response from the server.</returns>
        private BatchJobMutateResponse ParseResponse(string contents)
        {
            return ParseResponse<BatchJobMutateResponseEnvelope>(contents).mutateResponse;
        }

        /// <summary>
        /// Wait for the job to complete.
        /// </summary>
        /// <param name="batchJobId">ID of the job to wait for completion.</param>
        /// <returns><c>false</c>, if the job is still pending, false otherwise.</returns>
        public bool WaitForPendingJob(long batchJobId)
        {
            return WaitForPendingJob(batchJobId, int.MaxValue, null);
        }

        /// <summary>
        /// Wait for the job to complete.
        /// </summary>
        /// <param name="batchJobId">ID of the job to wait for completion.</param>
        /// <param name="numMilliSecondsToWait">The number of milliseconds to wait for job
        /// completion.</param>
        /// <returns><c>false</c>, if the job is still pending, false otherwise.</returns>
        public bool WaitForPendingJob(long batchJobId, int numMilliSecondsToWait)
        {
            return WaitForPendingJob(batchJobId, numMilliSecondsToWait, null);
        }

        /// <summary>
        /// Wait for the job to complete.
        /// </summary>
        /// <param name="batchJobId">ID of the job to wait for completion.</param>
        /// <param name="numMilliSecondsToWait">The number of milliseconds to wait for job
        /// completion.</param>
        /// <param name="callback">The callback to be called whenever the method polls the
        /// server for job status.</param>
        /// <returns><c>false</c>, if the job is still pending, true otherwise.</returns>
        public bool WaitForPendingJob(long batchJobId, int numMilliSecondsToWait,
            WaitCallback callback)
        {
            BatchJobService batchJobService =
                (BatchJobService) User.GetService(AdWordsService.v201809.BatchJobService);

            long totalMillisecondsWaited = 0;
            long pollAttempts = 0;
            bool cancelWait = false;
            bool isPending = true;
            do
            {
                int sleepMillis = (int) Math.Pow(2, pollAttempts) * POLL_INTERVAL_SECONDS_BASE *
                    1000;

                if (totalMillisecondsWaited + sleepMillis > numMilliSecondsToWait)
                {
                    sleepMillis = (int) (numMilliSecondsToWait - totalMillisecondsWaited);
                }

                Thread.Sleep(sleepMillis);
                totalMillisecondsWaited += sleepMillis;
                pollAttempts++;

                Selector selector = new Selector()
                {
                    fields = new string[]
                    {
                        ApiBatchJob.Fields.Id,
                        ApiBatchJob.Fields.Status,
                        ApiBatchJob.Fields.DownloadUrl,
                        ApiBatchJob.Fields.ProcessingErrors,
                        ApiBatchJob.Fields.ProgressStats
                    },
                    predicates = new Predicate[]
                    {
                        Predicate.Equals(ApiBatchJob.Fields.Id, batchJobId)
                    }
                };

                ApiBatchJob batchJob = batchJobService.get(selector).entries[0];
                isPending = PENDING_STATUSES.Contains(batchJob.status);

                if (callback != null)
                {
                    cancelWait = callback(batchJob, totalMillisecondsWaited);
                }
            } while (isPending && totalMillisecondsWaited < numMilliSecondsToWait && !cancelWait);

            return !isPending;
        }

        /// <summary>
        /// Try to cancel a job.
        /// </summary>
        /// <param name="batchJobId">ID of the batch job to cancel.</param>
        /// <exception cref="AdWordsApiException">Thrown if an API error occurred
        /// when cancelling the job.</exception>
        public void TryToCancelJob(long batchJobId)
        {
            BatchJobService batchJobService =
                (BatchJobService) User.GetService(AdWordsService.v201809.BatchJobService);
            try
            {
                BatchJobOperation batchJobSetOperation = new BatchJobOperation()
                {
                    @operator = Operator.SET,
                    operand = new ApiBatchJob()
                    {
                        id = batchJobId,
                        status = BatchJobStatus.CANCELING
                    }
                };

                batchJobService.mutate(new BatchJobOperation[]
                {
                    batchJobSetOperation
                });
            }
            catch (AdWordsApiException)
            {
                // Rethrow the API exception.
                throw;
            }
        }

        #region mono_vbnc_bug_40793_workaround

        /* Mono VBNC compiler has issues calling generic methods whose signatures contain generic
         * type. These methods are provided as workaround for Mono users on VB.NET.
         * See https://bugzilla.xamarin.com/show_bug.cgi?id=40793 for details.
         */

        /// <summary>
        /// Uploads the operations to a specified URL.
        /// </summary>
        /// <param name="url">The temporary URL returned by a batch job.</param>
        /// <param name="operations">The list of operations.</param>
        /// <remarks>Use this method as a workaround for
        /// https://bugzilla.xamarin.com/show_bug.cgi?id=40793 if you are using VB.NET on Mono.
        /// Otherwise use <see cref="Upload(string, IEnumerable{Operation})"/> method.</remarks>
        public void Upload(string url, Operation[] operations)
        {
            Upload(url, (IEnumerable<Operation>) operations);
        }

        /// <summary>
        /// Uploads the operations to a specified URL.
        /// </summary>
        /// <param name="url">The temporary URL returned by a batch job.</param>
        /// <param name="operations">The list of operations.</param>
        /// <param name="resumePreviousUpload">True, if a previously interrupted
        /// upload should be resumed.</param>
        /// <remarks>Use this method as a workaround for
        /// https://bugzilla.xamarin.com/show_bug.cgi?id=40793 if you are using VB.NET on Mono.
        /// Otherwise use <see cref="Upload(string, IEnumerable{Operation}, bool)"/> method.</remarks>
        public void Upload(string url, Operation[] operations, bool resumePreviousUpload)
        {
            Upload(url, (IEnumerable<Operation>) operations, resumePreviousUpload);
        }

        /// <summary>
        /// Uploads the operations to a specified URL in a streamed manner.
        /// </summary>
        /// <param name="uploadProgress">The upload progress tracker.</param>
        /// <param name="operations">The list of operations.</param>
        /// <returns>The updated progress tracker.</returns>
        /// <remarks>Use this method as a workaround for
        /// https://bugzilla.xamarin.com/show_bug.cgi?id=40793 if you are using VB.NET on Mono.
        /// Otherwise use <see cref="StreamUpload(BatchUploadProgress, IEnumerable{Operation})"/>
        /// method.</remarks>
        public BatchUploadProgress StreamUpload(BatchUploadProgress uploadProgress,
            Operation[] operations)
        {
            return StreamUpload(uploadProgress, (IEnumerable<Operation>) operations);
        }

        #endregion
    }
}
