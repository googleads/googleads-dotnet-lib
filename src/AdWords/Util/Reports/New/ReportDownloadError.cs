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
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Util.Reports {
  /// <summary>
  /// Represents a report download error.
  /// </summary>
  [Serializable()]
  public class ReportDownloadError {
    /// <summary>
    /// Type of error.
    /// </summary>
    string errorType;

    /// <summary>
    /// The reason for triggering this error.
    /// </summary>
    string trigger;

    /// <summary>
    /// The field that triggered this error, if applicable.
    /// </summary>
    string fieldPath;

    /// <summary>
    /// Gets or sets the type of the error.
    /// </summary>
    public string ErrorType {
      get {
        return errorType;
      }
      set {
        errorType = value;
      }
    }

    /// <summary>
    /// Gets or sets the reason for triggering this error.
    /// </summary>
    public string Trigger {
      get {
        return trigger;
      }
      set {
        trigger = value;
      }
    }

    /// <summary>
    /// Gets or sets the field that triggered this error, if applicable.
    /// </summary>
    public string FieldPath {
      get {
        return fieldPath;
      }
      set {
        fieldPath = value;
      }
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      return string.Format("Error: {0}, Trigger: {1}, FieldPath: {2}", errorType, trigger,
          fieldPath);
    }
  }
}
