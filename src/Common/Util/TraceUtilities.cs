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

#define TRACE

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Google.Api.Ads.Common.Util {

  /// <summary>
  /// Provides utility methods to write to Trace stream.
  /// </summary>
  public static class TraceUtilities {

    /// <summary>
    /// Trace source for deprecation messages.
    /// </summary>
    public const string DEPRECATION_MESSAGES_SOURCE = "AdsClientLibs.DeprecationMessages";

    /// <summary>
    /// Trace source for general warning messages.
    /// </summary>
    public const string GENERAL_WARNING_MESSAGES_SOURCE = "AdsClientLibs.GeneralWarningMessages";

    /// <summary>
    /// Trace source for detailed HTTP request logs.
    /// </summary>
    public const string DETAILED_REQUEST_LOGS_SOURCE = "AdsClientLibs.DetailedRequestLogs";

    /// <summary>
    /// Trace source for summarized HTTP request logs.
    /// </summary>
    public const string SUMMARY_REQUEST_LOGS_SOURCE = "AdsClientLibs.SummaryRequestLogs";

    /// <summary>
    /// Deprecated Trace source for detailed HTTP request logs.
    /// </summary>
    public const string DEPRECATED_DETAILED_REQUEST_LOGS_SOURCE = "AdsClientLibs.SoapXmlLogs";

    /// <summary>
    /// Deprecated Trace source for summarized HTTP request logs.
    /// </summary>
    private const string DEPRECATED_SUMMARY_REQUEST_LOGS_SOURCE = "AdsClientLibs.RequestInfoLogs";

    /// <summary>
    /// The list of known Trace sources.
    /// </summary>
    private static readonly Dictionary<string, TraceSource> KNOWN_TRACE_SOURCES =
        new Dictionary<string, TraceSource>() {
          {DEPRECATION_MESSAGES_SOURCE, new TraceSource(DEPRECATION_MESSAGES_SOURCE)},
          {GENERAL_WARNING_MESSAGES_SOURCE, new TraceSource(GENERAL_WARNING_MESSAGES_SOURCE)},
          {SUMMARY_REQUEST_LOGS_SOURCE, new TraceSource(SUMMARY_REQUEST_LOGS_SOURCE)},
          {DEPRECATED_DETAILED_REQUEST_LOGS_SOURCE,
                new TraceSource(DEPRECATED_DETAILED_REQUEST_LOGS_SOURCE)},
          {DEPRECATED_SUMMARY_REQUEST_LOGS_SOURCE,
                new TraceSource(DEPRECATED_SUMMARY_REQUEST_LOGS_SOURCE)},
          {DETAILED_REQUEST_LOGS_SOURCE,
                new TraceSource(DETAILED_REQUEST_LOGS_SOURCE)},
    };

    /// <summary>
    /// Initializes the <see cref="TraceUtilities"/> class.
    /// </summary>
    static TraceUtilities() {
      ShowDeprecationWarningsAboutDeprecatedLogSources();
    }

    /// <summary>
    /// The Trace message id.
    /// </summary>
    /// <remarks>Trace.Write needs a TRACE id to categorize messages, but since
    /// we don't provide any categorization, we will use a standard value.
    /// </remarks>
    private const int ADS_API_TRACE_ID = 1;

    /// <summary>
    /// Shows the deprecation warnings about deprecated log sources.
    /// </summary>
    private static void ShowDeprecationWarningsAboutDeprecatedLogSources() {
      string[] deprecatedLogSources = new string[] {
        DEPRECATED_DETAILED_REQUEST_LOGS_SOURCE,
        DEPRECATED_SUMMARY_REQUEST_LOGS_SOURCE
      };

      foreach (string deprecatedLogSource in deprecatedLogSources) {
        if (KNOWN_TRACE_SOURCES[deprecatedLogSource].Listeners.Count > 0) {
          WriteDeprecationWarnings(string.Format(CommonErrorMessages.UsingDeprecatedLogSource,
              deprecatedLogSource));
        }
      }
    }

    /// <summary>
    /// Gets a Trace source by name.
    /// </summary>
    /// <param name="sourceName">Name of the Trace source.</param>
    /// <returns>The trace source.</returns>
    /// <exception cref="ArgumentException">Thrown if the trace source is
    /// unknown.</exception>
    public static TraceSource GetSource(string sourceName) {
      if (KNOWN_TRACE_SOURCES.ContainsKey(sourceName)) {
        return KNOWN_TRACE_SOURCES[sourceName];
      } else {
        throw new ArgumentException(string.Format(CommonErrorMessages.UnknownTraceSource,
            sourceName));
      }
    }

    /// <summary>
    /// Writes the deprecation warnings.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <remarks>The trace levels may be controlled from App.config by setting
    /// the level for AdsClientLibs.DeprecationMessages trace switch. </remarks>
    public static void WriteDeprecationWarnings(string message) {
      Write(DEPRECATION_MESSAGES_SOURCE, TraceEventType.Warning, message);
    }

    /// <summary>
    /// Writes the general warnings.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <remarks>The trace levels may be controlled from App.config by setting
    /// the level for AdsClientLibs.GeneralWarningMessages trace switch. </remarks>
    public static void WriteGeneralWarnings(string message) {
      Write(GENERAL_WARNING_MESSAGES_SOURCE, TraceEventType.Warning, message);
    }

    /// <summary>
    /// Writes the general errors.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <remarks>The trace levels may be controlled from App.config by setting
    /// the level for AdsClientLibs.GeneralWarningMessages trace switch. </remarks>
    public static void WriteGeneralErrors(string message) {
      Write(GENERAL_WARNING_MESSAGES_SOURCE, TraceEventType.Error, message);
    }

    /// <summary>
    /// Writes detailed HTTP request logs.
    /// </summary>
    /// <param name="message">The HTTP request logs.</param>
    /// <param name="isError">Indicates whether or not these are error logs.</param>
    /// <remarks>The trace levels may be controlled from App.config by setting
    /// the level for AdsClientLibs.SoapXmlLogs trace switch. </remarks>
    public static void WriteDetailedRequestLogs(string message, Boolean isError) {
      TraceEventType type = isError ? TraceEventType.Error : TraceEventType.Information;
      Write(DETAILED_REQUEST_LOGS_SOURCE, type, message);
      Write(DEPRECATED_DETAILED_REQUEST_LOGS_SOURCE, type, message);
    }

    /// <summary>
    /// Writes the summarized HTTP request logs.
    /// </summary>
    /// <param name="message">The summarized HTTP request logs.</param>
    /// <param name="isError">Indicates whether or not these are error logs.</param>
    /// <remarks>The trace levels may be controlled from App.config by setting
    /// the level for AdsClientLibs.RequestInfoLogs trace switch. </remarks>
    public static void WriteSummaryRequestLogs(string message, Boolean isError) {
      TraceEventType type = isError ? TraceEventType.Error : TraceEventType.Information;
      Write(SUMMARY_REQUEST_LOGS_SOURCE, type, message);
      Write(DEPRECATED_SUMMARY_REQUEST_LOGS_SOURCE, type, message);
    }

    /// <summary>
    /// Writes to the specified Trace source.
    /// </summary>
    /// <param name="source">The trace source.</param>
    /// <param name="level">The message level.</param>
    /// <param name="message">The message.</param>
    private static void Write(string source, TraceEventType level, string message) {
      TraceSource messageTrace = GetSource(source);
      messageTrace.TraceEvent(level, ADS_API_TRACE_ID, message);
      messageTrace.Flush();
    }
  }
}
