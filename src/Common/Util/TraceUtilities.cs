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

using Google.Api.Ads.Common.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Google.Api.Ads.Common.Util
{
    /// <summary>
    /// Provides utility methods to write to Trace stream.
    /// </summary>
    public class TraceUtilities
    {
        /// <summary>
        /// The registry for saving feature usage information..
        /// </summary>
        private static readonly AdsFeatureUsageRegistry featureUsageRegistry =
            AdsFeatureUsageRegistry.Instance;

        /// <summary>
        /// The feature ID for this class.
        /// </summary>
        private const AdsFeatureUsageRegistry.Features FEATURE_ID =
            AdsFeatureUsageRegistry.Features.Logging;

        /// <summary>
        /// Trace source for deprecation messages.
        /// </summary>
        public const string DEPRECATION_MESSAGES_SOURCE = "AdsClientLibs.DeprecationMessages";

        /// <summary>
        /// Trace source for general warning messages.
        /// </summary>
        public const string GENERAL_WARNING_MESSAGES_SOURCE =
            "AdsClientLibs.GeneralWarningMessages";

        /// <summary>
        /// Trace source for detailed HTTP request logs.
        /// </summary>
        public const string DETAILED_REQUEST_LOGS_SOURCE = "AdsClientLibs.DetailedRequestLogs";

        /// <summary>
        /// Trace source for summarized HTTP request logs.
        /// </summary>
        public const string SUMMARY_REQUEST_LOGS_SOURCE = "AdsClientLibs.SummaryRequestLogs";

        /// <summary>
        /// The list of known Trace sources.
        /// </summary>
        private static readonly Dictionary<string, TraceSource> KNOWN_TRACE_SOURCES =
            new Dictionary<string, TraceSource>()
            {
                {DEPRECATION_MESSAGES_SOURCE, new TraceSource(DEPRECATION_MESSAGES_SOURCE)},
                {GENERAL_WARNING_MESSAGES_SOURCE, new TraceSource(GENERAL_WARNING_MESSAGES_SOURCE)},
                {SUMMARY_REQUEST_LOGS_SOURCE, new TraceSource(SUMMARY_REQUEST_LOGS_SOURCE)},
                {DETAILED_REQUEST_LOGS_SOURCE, new TraceSource(DETAILED_REQUEST_LOGS_SOURCE)}
            };

        /// <summary>
        /// Initializes the <see cref="TraceUtilities"/> class.
        /// </summary>
        static TraceUtilities()
        {
        }

        /// <summary>
        /// The Trace message id.
        /// </summary>
        /// <remarks>Trace.Write needs a TRACE id to categorize messages, but since
        /// we don't provide any categorization, we will use a standard value.
        /// </remarks>
        private const int ADS_API_TRACE_ID = 1;

        /// <summary>
        /// Gets a Trace source by name.
        /// </summary>
        /// <param name="sourceName">Name of the Trace source.</param>
        /// <returns>The trace source.</returns>
        /// <exception cref="ArgumentException">Thrown if the trace source is
        /// unknown.</exception>
        public static TraceSource GetSource(string sourceName)
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            TraceSource source = CollectionUtilities.TryGetValue(KNOWN_TRACE_SOURCES, sourceName);
            if (source == null)
            {
                throw new ArgumentException(string.Format(CommonErrorMessages.UnknownTraceSource,
                    sourceName));
            }

            return source;
        }

        /// <summary>
        /// Writes the deprecation warnings.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <remarks>The trace levels may be controlled from App.config by setting
        /// the level for AdsClientLibs.DeprecationMessages trace switch.</remarks>
        public static void WriteDeprecationWarnings(string message)
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            Write(DEPRECATION_MESSAGES_SOURCE, TraceEventType.Warning, message);
        }

        /// <summary>
        /// Writes the general warnings.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <remarks>The trace levels may be controlled from App.config by setting
        /// the level for AdsClientLibs.GeneralWarningMessages trace switch.</remarks>
        public static void WriteGeneralWarnings(string message)
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            Write(GENERAL_WARNING_MESSAGES_SOURCE, TraceEventType.Warning, message);
        }

        /// <summary>
        /// Writes the general errors.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <remarks>The trace levels may be controlled from App.config by setting
        /// the level for AdsClientLibs.GeneralWarningMessages trace switch.</remarks>
        public static void WriteGeneralErrors(string message)
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            Write(GENERAL_WARNING_MESSAGES_SOURCE, TraceEventType.Error, message);
        }

        /// <summary>
        /// Writes detailed HTTP request logs.
        /// </summary>
        /// <param name="message">The HTTP request logs.</param>
        /// <param name="isError">Indicates whether or not these are error logs.</param>
        /// <remarks>The trace levels may be controlled from App.config by setting
        /// the level for AdsClientLibs.DetailedRequestLogs trace switch.</remarks>
        public static void WriteDetailedRequestLogs(string message, Boolean isError)
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            TraceEventType type = isError ? TraceEventType.Information : TraceEventType.Verbose;
            Write(DETAILED_REQUEST_LOGS_SOURCE, type, message);
        }

        /// <summary>
        /// Writes the summarized HTTP request logs.
        /// </summary>
        /// <param name="message">The summarized HTTP request logs.</param>
        /// <param name="isError">Indicates whether or not these are error logs.</param>
        /// <remarks>The trace levels may be controlled from App.config by setting
        /// the level for AdsClientLibs.SummaryRequestLogs trace switch.</remarks>
        public static void WriteSummaryRequestLogs(string message, Boolean isError)
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            TraceEventType type = isError ? TraceEventType.Warning : TraceEventType.Information;
            Write(SUMMARY_REQUEST_LOGS_SOURCE, type, message);
        }

        /// <summary>
        /// Writes to the specified Trace source.
        /// </summary>
        /// <param name="source">The trace source.</param>
        /// <param name="level">The message level.</param>
        /// <param name="message">The message.</param>
        private static void Write(string source, TraceEventType level, string message)
        {
            TraceSource messageTrace = GetSource(source);
            messageTrace.TraceEvent(level, ADS_API_TRACE_ID, message);
            messageTrace.Flush();
        }

        /// <summary>
        /// Configures the specified trace source.
        /// </summary>
        /// <param name="source">The trace source.</param>
        /// <param name="traceListener">The trace listener.</param>
        /// <param name="levels">The source levels.</param>
        public static void Configure(string source, System.Diagnostics.TraceListener traceListener,
            SourceLevels levels)
        {
            TraceSource traceSource = GetSource(source);
            traceSource.Switch = new SourceSwitch(source)
            {
                Level = levels
            };

            traceSource.Listeners.Remove("Default");
            traceSource.Listeners.Add(traceListener);
        }

        /// <summary>
        /// Configures the specified trace source.
        /// </summary>
        /// <param name="source">The trace source.</param>
        /// <param name="textWriter">The text writer.</param>
        /// <param name="levels">The source levels.</param>
        public static void Configure(string source, TextWriter textWriter, SourceLevels levels)
        {
            TextWriterTraceListener textWriterTraceListener = new TextWriterTraceListener()
            {
                Name = source,
                Writer = textWriter,
            };
            Configure(source, textWriterTraceListener, levels);
        }

        /// <summary>
        /// Configures the specified trace source to write to a file.
        /// </summary>
        /// <param name="source">The trace source.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="levels">The trace levels.</param>
        public static void Configure(string source, string filePath, SourceLevels levels)
        {
            StreamWriter textWriter = new StreamWriter(filePath, true);
            Configure(source, textWriter, levels);
        }
    }
}
