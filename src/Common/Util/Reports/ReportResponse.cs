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

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.Common.Util.Reports
{
    /// <summary>
    /// Represents a report response from the server.
    /// </summary>
    public class ReportResponse : IDisposable
    {
        /// <summary>
        /// The report contents in memory.
        /// </summary>
        private byte[] contents;

        /// <summary>
        /// The underlying HTTP web response.
        /// </summary>
        private WebResponse response;

        /// <summary>
        /// Flag to keep track if this report response has been disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Delegate to be triggered when asynchronous report download is completed
        /// successfully.
        /// </summary>
        public delegate void OnDownloadSuccessCallback(byte[] contents);

        /// <summary>
        /// Delegate to be triggered when asynchronous report save is completed
        /// successfully.
        /// </summary>
        public delegate void OnSaveSuccessCallback();

        /// <summary>
        /// Delegate to be triggered when asynchronous report download fails.
        /// </summary>
        public delegate void OnFailedCallback(AdsReportsException exception);

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportResponse"/> class.
        /// </summary>
        /// <param name="response">The underlying HTTP web response.</param>
        public ReportResponse(WebResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("Response cannot be null.");
            }

            this.response = response;
        }

        /// <summary>
        /// The callback that will be triggered when the asynchronous report
        /// download is completed successfully.
        /// </summary>
        public OnDownloadSuccessCallback OnDownloadSuccess { get; set; }

        /// <summary>
        /// The callback that will be triggered when the asynchronous report
        /// save is completed successfully.
        /// </summary>
        public OnSaveSuccessCallback OnSaveSuccess { get; set; }

        /// <summary>
        /// Gets the callback that will be triggered when the asynchronous report
        /// download fails.
        /// </summary>
        public OnFailedCallback OnFailed { get; set; }

        /// <summary>
        /// Gets the report contents as a stream.
        /// </summary>
        public Stream Stream
        {
            get
            {
                this.EnsureStreamIsOpen();
                return response.GetResponseStream();
            }
        }

        /// <summary>
        /// Gets the report contents as a decompressed stream.
        /// </summary>
        public Stream DecompressedStream
        {
            get { return new GZipStream(this.Stream, CompressionMode.Decompress); }
        }

        /// <summary>
        /// Gets the path to the downloaded report.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Saves the report to a specified path and closes the underlying stream.
        /// </summary>
        /// <param name="path">The path to which report is saved.</param>
        /// <exception cref="AdsReportsException">
        /// If there was an error saving the report.
        /// </exception>
        public void Save(string path)
        {
            this.EnsureStreamIsOpen();

            try
            {
                using (FileStream fileStream = File.OpenWrite(path))
                {
                    fileStream.SetLength(0);
                    this.Stream.CopyTo(fileStream);
                    this.CloseWebResponse();
                }

                this.Path = path;
            }
            catch (Exception e)
            {
                throw new AdsReportsException(
                    "Failed to save report. See inner exception " + "for more details.", e);
            }
        }

        /// <summary>
        /// Saves the report to a specified path asynchronously and closes the underlying stream.
        /// <see cref="OnSaveSuccess"/> callback will be triggered when the download completes
        /// successfully, and <see cref="OnFailed"/> callback will be triggered when the download
        /// fails.
        /// </summary>
        /// <param name="path">The path to which report is saved.</param>
        /// <exception cref="AdsReportsException">
        /// If there was an error saving the report.
        /// </exception>
        public void SaveAsync(string path)
        {
            Thread asyncThread = new Thread(new ThreadStart(delegate()
            {
                try
                {
                    Save(path);
                    if (this.OnSaveSuccess != null)
                    {
                        this.OnSaveSuccess();
                    }
                }
                catch (AdsReportsException e)
                {
                    if (this.OnFailed != null)
                    {
                        this.OnFailed(e);
                    }
                    else
                    {
                        throw;
                    }
                }
            }));
            asyncThread.Start();
        }

        /// <summary>
        /// Downloads the report to memory and closes the underlying stream.
        /// </summary>
        /// <exception cref="AdsReportsException">If there was an error downloading the report.
        /// </exception>
        public byte[] Download()
        {
            this.EnsureStreamIsOpen();

            try
            {
                MemoryStream memStream = new MemoryStream();
                this.Stream.CopyTo(memStream);
                this.contents = memStream.ToArray();
                this.CloseWebResponse();
            }
            catch (Exception e)
            {
                throw new AdsReportsException(
                    "Failed to download report. See inner exception " + "for more details.", e);
            }

            return this.contents;
        }

        /// <summary>
        /// Downloads the report to memory asynchronously and closes the underlying stream.
        /// <see cref="OnDownloadSuccess"/> callback will be triggered when the download completes
        /// successfully, and <see cref="OnFailed"/> callback will be triggered when the download
        /// fails.
        /// </summary>
        /// <exception cref="AdsReportsException">If there was an error downloading the report.
        /// </exception>
        public void DownloadAsync()
        {
            Thread asyncThread = new Thread(new ThreadStart(delegate()
            {
                try
                {
                    byte[] contents = Download();
                    if (this.OnDownloadSuccess != null)
                    {
                        this.OnDownloadSuccess(contents);
                    }
                }
                catch (AdsReportsException e)
                {
                    if (this.OnFailed != null)
                    {
                        this.OnFailed(e);
                    }
                    else
                    {
                        throw;
                    }
                }
            }));
            asyncThread.Start();
        }

        /// <summary>
        /// Checks to ensure that the underlying stream has not been closed.
        /// </summary>
        /// <exception cref="AdsReportsException">If the underlying stream has been closed.
        /// </exception>
        private void EnsureStreamIsOpen()
        {
            if (response == null)
            {
                throw new AdsReportsException("Cannot access a closed report response stream.");
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and
        /// unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.CloseWebResponse();
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Closes the underlying HTTP web response.
        /// </summary>
        private void CloseWebResponse()
        {
            if (response != null)
            {
                response.Close();
                response = null;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ReportResponse"/> class.
        /// </summary>
        ~ReportResponse()
        {
            this.Dispose(false);
        }
    }
}
