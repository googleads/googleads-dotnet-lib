// Copyright 2012, Google Inc. All Rights Reserved.
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
using System.Threading;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Default trace writer, that write trace to a file on disk.
  /// </summary>
  public class DefaultTraceWriter : TraceWriter {
    /// <summary>
    /// The filename to which we log the SOAP messages.
    /// </summary>
    private string soapFileName;

    /// <summary>
    /// The filename to which we log the request info.
    /// </summary>
    private string requestInfoFileName;

    /// <summary>
    /// Maximum number of attempts to write to log file if it is locked.
    /// </summary>
    private const int MAX_ATTEMPTS = 3;

    /// <summary>
    /// Overloaded constructor.
    /// </summary>
    /// <param name="config">The application configuration class for configuring
    /// this instance.</param>
    public DefaultTraceWriter(AppConfigBase config) {
      string logPath = "";
      if (config.LogToFile) {
        logPath = config.LogPath.TrimEnd('\\', '/') + Path.DirectorySeparatorChar;
        if (!Directory.Exists(logPath)) {
          Directory.CreateDirectory(logPath);
        }
        soapFileName = logPath + "soap_xml.log";
        requestInfoFileName = logPath + "request_info.log";
      }
    }

    /// <summary>
    /// Write the SOAP and HTTP trace logs.
    /// </summary>
    /// <param name="soapLog">The SOAP log.</param>
    /// <param name="requestLog">The HTTP request log.</param>
    public void Write(string soapLog, string requestLog) {
      WriteToFile(soapFileName, soapLog);
      WriteToFile(requestInfoFileName, requestLog);
    }

    /// <summary>
    /// Writes a log string into a specified log file.
    /// </summary>
    /// <param name="fileName">The file to which the log text should be written.
    /// </param>
    /// <param name="logText">The log text to be written to the file.</param>
    private void WriteToFile(string fileName, string logText) {
      for (int i = 0; i < MAX_ATTEMPTS; i++) {
        StreamWriter writer = null;
        try {
          writer = new StreamWriter(fileName, true);
          writer.WriteLine(logText);
          break;
        } catch (Exception) {
          Thread.Sleep(100 + new Random().Next(1000));
        } finally {
          if (writer != null) {
            writer.Close();
          }
        }
      }
    }
  }
}
