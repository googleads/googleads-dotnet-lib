// Copyright 2010, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfa.Util.ApiCodes;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfa.Util {
  /// <summary>
  /// Represents an error code returned by DFA servers.
  /// </summary>
  [Serializable()]
  public class ErrorCode {
    /// <summary>
    /// Error code.
    /// </summary>
    int code = 0;

    /// <summary>
    /// Error description.
    /// </summary>
    string description = "";

    /// <summary>
    /// Map of all error codes.
    /// </summary>
    static Dictionary<int, ErrorCode> allCodes;

    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public int Code {
      get {
        return code;
      }
      set {
        code = value;
      }
    }

    /// <summary>
    /// Gets or sets the error description.
    /// </summary>
    public string Description {
      get {
        return description;
      }
      set {
        description = value;
      }
    }

    /// <summary>
    /// Static constructor.
    /// </summary>
    static ErrorCode() {
      LoadErrorCodes();
    }

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public ErrorCode() : this(0, "") {
    }

    /// <summary>
    /// Parameterized constructor.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    public ErrorCode(int code, string description) {
      this.code = code;
      this.description = description;
    }

    /// <summary>
    /// Load all error codes into memory.
    /// </summary>
    private static void LoadErrorCodes() {
      allCodes = new Dictionary<int, ErrorCode>();
      CsvFile reader = new CsvFile();
      reader.ReadFromString(CodeCsvs.ErrorCodes, true);
      List<ErrorCode> retVal = new List<ErrorCode>();

      foreach (string[] item in reader.Records) {
        allCodes.Add(int.Parse(item[0]), new ErrorCode(int.Parse(item[0]), item[1]));
      }
    }

    /// <summary>
    /// Gets an error code given the error number.
    /// </summary>
    /// <param name="id">Error number.</param>
    /// <returns>The error code.</returns>
    public static ErrorCode FromCode(int code) {
      return allCodes[code];
    }
  }
}
