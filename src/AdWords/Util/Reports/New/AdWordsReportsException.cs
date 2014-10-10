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

using Google.Api.Ads.Common.Util.Reports;

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Google.Api.Ads.AdWords.Util.Reports {

  /// <summary>
  /// Custom exception class for handling reporting errors.
  /// </summary>
  [Serializable]
  public class AdWordsReportsException : AdsReportsException {
    private ReportDownloadError[] errors;

    public ReportDownloadError[] Errors {
      get {
        return errors;
      }
      set {
        errors = value;
      }
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    public AdWordsReportsException()
      : base() {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="message">Error message for this API exception.</param>
    public AdWordsReportsException(string message)
      : base(message) {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public AdWordsReportsException(string message, Exception innerException)
      : base(message, innerException) {
    }

    /// <summary>
    /// Protected constructor. Used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected AdWordsReportsException(SerializationInfo info, StreamingContext context)
      : base(info, context) {
      if (info != null) {
        errors = (ReportDownloadError[]) info.GetValue("errors", typeof(ReportDownloadError[]));
      }
    }

    /// <summary>
    /// This method is called by serialization frameworks while serializing
    /// an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      if (info == null) {
        throw new ArgumentNullException("info");
      }
      base.GetObjectData(info, context);
      if (info != null) {
        info.AddValue("errors", errors);
      }
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      StringBuilder exceptionBuilder = new StringBuilder();
      exceptionBuilder.AppendFormat("{0}: {1}", this.GetType().Name, this.Message);

      if (errors != null) {
        exceptionBuilder.AppendFormat("{0}{0}{1}{0}{0}", Environment.NewLine,
            string.Join<ReportDownloadError>(Environment.NewLine, errors));
      }

      if (this.InnerException != null) {
        exceptionBuilder.AppendFormat(" ---> {0}", this.InnerException);
        exceptionBuilder.AppendFormat("{0}   --- End of inner exception stack trace ---{0}",
            Environment.NewLine);
      }

      exceptionBuilder.Append(this.StackTrace);

      return exceptionBuilder.ToString();
    }
  }
}
