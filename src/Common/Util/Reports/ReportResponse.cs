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

// Author: api.anash@gmail.com (Anash P. Oommen)

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.Common.Util.Reports {

  /// <summary>
  /// Represents a report response from the server.
  /// </summary>
  public class ReportResponse : IDisposable {

    /// <summary>
    /// The path to which the report was downloaded.
    /// </summary>
    private string path;

    /// <summary>
    /// The report contents in memory.
    /// </summary>
    private byte[] contents;

    /// <summary>
    /// The underlying HTTP web response.
    /// </summary>
    private WebResponse response;

    /// <summary>
    /// Delegate to be triggered when asynchronous report download is completed
    /// successfully.
    /// </summary>
    public delegate void OnSuccessCallback();

    /// <summary>
    /// Delegate to be triggered when asynchronous report download fails.
    /// </summary>
    public delegate void OnFailedCallback(AdsReportsException exception);

    /// <summary>
    /// The callback that will be triggered when the asynchronous report
    /// download is completed successfully.
    /// </summary>
    private OnSuccessCallback onSuccess;

    /// <summary>
    /// The callback that will be triggered when the asynchronous report
    /// download fails.
    /// </summary>
    private OnFailedCallback onFailed;

    /// <summary>
    /// The thread to use when downloading the report in an asynchronous manner.
    /// </summary>
    private Thread asyncThread;

    /// <summary>
    /// Flag to keep track if this report response has been disposed.
    /// </summary>
    private bool disposed = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportResponse"/> class.
    /// </summary>
    /// <param name="response">The underlying HTTP web response.</param>
    public ReportResponse(WebResponse response) {
      if (response == null) {
        throw new ArgumentNullException("Response cannot be null.");
      }
      this.response = response;
    }

    /// <summary>
    /// The callback that will be triggered when the asynchronous report
    /// download is completed successfully.
    /// </value>
    public OnSuccessCallback OnSuccess {
      get {
        return onSuccess;
      }
      set {
        onSuccess = value;
      }
    }

    /// <summary>
    /// Gets the callback that will be triggered when the asynchronous report
    /// download fails.
    /// </value>
    public OnFailedCallback OnFailed {
      get {
        return onFailed;
      }
      set {
        onFailed = value;
      }
    }

    /// <summary>
    /// Gets the report contents as a stream.
    /// </summary>
    public Stream Stream {
      get {
        return response.GetResponseStream();
      }
    }

    /// <summary>
    /// Gets the path to the downloaded report.
    /// </summary>
    public string Path {
      get {
        return path;
      }
    }

    /// <summary>
    /// Gets the report contents as an array of bytes.
    /// </summary>
    public byte[] Contents {
      get {
        Download();
        return contents;
      }
    }

    /// <summary>
    /// Gets the report contents as string.
    /// </summary>
    public string Text {
      get {
        return Encoding.UTF8.GetString(contents);
      }
    }

    /// <summary>
    /// Saves the report to a specified path.
    /// </summary>
    /// <param name="path">The path to which report is saved.</param>
    public void Save(string path) {
      if (response != null) {
        try {
          using (FileStream fileStream = File.OpenWrite(path)) {
            fileStream.SetLength(0);
            MediaUtilities.CopyStream(response.GetResponseStream(), fileStream);
            CloseWebResponse();
          }
          this.path = path;
        } catch (Exception e) {
          throw new AdsReportsException("Failed to save report. See inner exception " +
              "for more details.", e);
        }
      }
    }

    /// <summary>
    /// Saves the report to a specified path asynchronously. <see cref="OnSuccess"/>
    /// callback will be triggered when the download completes successfully,
    /// and <see cref="OnFailed"/> callback will be triggered when the download
    /// fails.
    /// </summary>
    /// <param name="path">The path to which report is saved.</param>
    public void SaveAsync(string path) {
      asyncThread = new Thread(new ThreadStart(delegate() {
        try {
          Save(path);
          if (OnSuccess != null) {
            OnSuccess();
          }
        } catch (AdsReportsException e) {
          if (onFailed != null) {
            onFailed(e);
          } else {
            throw;
          }
        }
      }));
      asyncThread.Start();
    }

    /// <summary>
    /// Downloads the report to memory.
    /// </summary>
    public void Download() {
      if (response != null) {
        try {
          MemoryStream memStream = new MemoryStream();
          MediaUtilities.CopyStream(response.GetResponseStream(), memStream);
          this.contents = memStream.ToArray();
          CloseWebResponse();
        } catch (Exception e) {
          throw new AdsReportsException("Failed to download report. See inner exception " +
              "for more details.", e);
        }
      }
    }

    /// <summary>
    /// Downloads the report to memory asynchronously. <see cref="OnSuccess"/>
    /// callback will be triggered when the download completes successfully,
    /// and <see cref="OnFailed"/> callback will be triggered when the download
    /// fails.
    /// </summary>
    /// <param name="path">The path to which report is saved.</param>
    public void DownloadAsync() {
      asyncThread = new Thread(new ThreadStart(delegate() {
        try {
          Download();
          if (OnSuccess != null) {
            OnSuccess();
          }
        } catch (AdsReportsException e) {
          if (onFailed != null) {
            onFailed(e);
          } else {
            throw;
          }
        }
      }));
      asyncThread.Start();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing,
    /// or resetting unmanaged resources.
    /// </summary>
    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and
    /// unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing) {
      if (!this.disposed) {
        if (disposing) {
          CloseWebResponse();
        }
        disposed = true;
      }
    }

    /// <summary>
    /// Closes the underlying HTTP web response.
    /// </summary>
    private void CloseWebResponse() {
      if (response != null) {
        response.Close();
        response = null;
      }
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="ReportResponse"/> class.
    /// </summary>
    ~ReportResponse() {
      Dispose(false);
    }
  }
}
