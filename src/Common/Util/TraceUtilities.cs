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

// Author: api.anash@gmail.com (Anash P. Oommen)

#define TRACE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Google.Api.Ads.Common.Util {
  /// <summary>
  /// Provides utility methods to write to Trace stream.
  /// </summary>
  public class TraceUtilities {
    /// <summary>
    /// Trace source for deprecation messages.
    /// </summary>
    private const string DEPRECATION_MESSAGES_SOURCE = "AdsClientLibs.DeprecationMessages";

    /// <summary>
    /// Trace source for general warning messages.
    /// </summary>
    private const string GENERAL_WARNING_MESSAGES_SOURCE = "AdsClientLibs.GeneralWarningMessages";

    /// <summary>
    /// The Trace message id.
    /// </summary>
    /// <remarks>Trace.Write needs a TRACE id to categorize messages, but since
    /// we don't provide any categorization, we will use a standard value.
    /// </remarks>
    private const int ADS_API_TRACE_ID = 1;

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
    /// Writes to the specified Trace source.
    /// </summary>
    /// <param name="source">The trace source.</param>
    /// <param name="level">The message level.</param>
    /// <param name="message">The message.</param>
    private static void Write(string source, TraceEventType level, string message) {
      TraceSource messageTrace = new TraceSource(source);
      messageTrace.TraceEvent(level, ADS_API_TRACE_ID, message);
      messageTrace.Flush();
    }
  }
}
