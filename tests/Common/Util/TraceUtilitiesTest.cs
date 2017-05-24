// Copyright 2017, Google Inc. All Rights Reserved.
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

using System.Diagnostics;
using System.IO;
using System.Text;

namespace Google.Api.Ads.Common.Tests.Util {
  /// <summary>
  /// UnitTests for <see cref="TraceUtilities"/> class.
  /// </summary>
  [TestFixture]
  public class TraceUtilitiesTest {
    /// <summary>
    /// A test log message that is guaranteed to not contain any value that would cause false
    /// positives when testing.
    /// </summary>
    private const string TEST_LOG_MESSAGE = "test log message";

    private const string ERROR_MARKER = "Error: 1";
    private const string INFO_MARKER = "Information: 1";
    private const string WARNING_MARKER = "Warning: 1";
    private const string VERBOSE_MARKER = "Verbose: 1";

    /// <summary>
    /// Tests that deprecation warnings are written to the correct logs source at the expected log
    /// level.
    /// </summary>
    [Test]
    [Category("Small")]
    public void WriteDeprecationWarnings() {
      using (var stream = new MemoryStream()) {
        enableLoggingToMemoryStream(TraceUtilities.DEPRECATION_MESSAGES_SOURCE, stream);

        // Ensure deprecation warnings are logged at the warning level.
        TraceUtilities.WriteDeprecationWarnings(TEST_LOG_MESSAGE);
        StringAssert.Contains(WARNING_MARKER, getLogFromMemoryStream(stream));
      }
    }

    /// <summary>
    /// Tests that general warnings are written to the correct logs source at the expected log
    /// level.
    /// </summary>
    [Test]
    [Category("Small")]
    public void WriteGeneralWarnings() {
      using (var stream = new MemoryStream()) {
        enableLoggingToMemoryStream(TraceUtilities.GENERAL_WARNING_MESSAGES_SOURCE, stream);

        // Ensure general warnings are logged at the warning level.
        TraceUtilities.WriteGeneralWarnings(TEST_LOG_MESSAGE);
        StringAssert.Contains(WARNING_MARKER, getLogFromMemoryStream(stream));
      }
    }

    /// <summary>
    /// Tests that general errors are written to the correct logs source at the expected log
    /// level.
    /// </summary>
    [Test]
    [Category("Small")]
    public void WriteGeneralErrors() {
      using (var stream = new MemoryStream()) {
        enableLoggingToMemoryStream(TraceUtilities.GENERAL_WARNING_MESSAGES_SOURCE, stream);

        // Ensure general errors are logged at the error level.
        TraceUtilities.WriteGeneralErrors(TEST_LOG_MESSAGE);
        StringAssert.Contains(ERROR_MARKER, getLogFromMemoryStream(stream));
      }
    }

    /// <summary>
    /// Tests that summary success logs are written to the correct logs source at the expected log
    /// level.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestWriteSummaryRequestLogsSuccess() {
      using (var stream = new MemoryStream()) {
        enableLoggingToMemoryStream(TraceUtilities.SUMMARY_REQUEST_LOGS_SOURCE, stream);

        // Ensure success summaries are logged at the info level.
        TraceUtilities.WriteSummaryRequestLogs(TEST_LOG_MESSAGE, false);
        StringAssert.Contains(INFO_MARKER, getLogFromMemoryStream(stream));
      }
    }

    /// <summary>
    /// Tests that summary failure logs are written to the correct logs source at the expected log
    /// level.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestWriteSummaryRequestLogsFailure() {
      using (var stream = new MemoryStream()) {
        enableLoggingToMemoryStream(TraceUtilities.SUMMARY_REQUEST_LOGS_SOURCE, stream);

        // Ensure failure summaries are logged at the warning level.
        TraceUtilities.WriteSummaryRequestLogs(TEST_LOG_MESSAGE, true);
        StringAssert.Contains(WARNING_MARKER, getLogFromMemoryStream(stream));
      }
    }

    /// <summary>
    /// Tests that detailed success logs are written to the correct logs source at the expected log
    /// level.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestWriteDetailedRequestLogsSuccess() {
      using (var stream = new MemoryStream()) {
        enableLoggingToMemoryStream(TraceUtilities.DETAILED_REQUEST_LOGS_SOURCE, stream);

        // Ensure success details are logged at the verbose level.
        TraceUtilities.WriteDetailedRequestLogs(TEST_LOG_MESSAGE, false);
        StringAssert.Contains(VERBOSE_MARKER, getLogFromMemoryStream(stream));
      }
    }

    /// <summary>
    /// Tests that detailed failure logs are written to the correct logs source at the expected log
    /// level.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestWriteDetailedRequestLogsFailure() {
      using (var stream = new MemoryStream()) {
        enableLoggingToMemoryStream(TraceUtilities.DETAILED_REQUEST_LOGS_SOURCE, stream);

        // Ensure failure details are logged at the warning level.
        TraceUtilities.WriteDetailedRequestLogs(TEST_LOG_MESSAGE, true);
        StringAssert.Contains(INFO_MARKER, getLogFromMemoryStream(stream));
      }
    }

    private void enableLoggingToMemoryStream(string logSourceName, MemoryStream stream) {
        TraceSource source = TraceUtilities.GetSource(logSourceName);
        source.Switch.Level = SourceLevels.All;
        source.Listeners.Add(new TextWriterTraceListener(stream) {
          Filter = new EventTypeFilter(SourceLevels.All)
        });
    }

    private string getLogFromMemoryStream(MemoryStream stream) {
      stream.Position = 0;
      return Encoding.UTF8.GetString(stream.ToArray());
    }
  }
}

