// Copyright 2016, Google Inc. All Rights Reserved.
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

using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.v201603 {

  /// <summary>
  /// A class to lookup human-friendly descriptions for feed validation errors.
  /// </summary>
  public class FeedValidationErrors {

    /// <summary>
    /// A dictionary to hold the error messages.
    /// </summary>
    private static readonly Dictionary<int, string> codes;

    /// <summary>
    /// Static constructor to initialize the error lookup dictionary.
    /// </summary>
    static FeedValidationErrors() {
      codes = new Dictionary<int, string>();
      codes[-1] = "UNDEFINED ERROR";
      codes[1] = "STRING TOO SHORT";
      codes[2] = "STRING TOO LONG";
      codes[3] = "INVALID DESTINATION URL";
      codes[4] = "VALUE NOT SPECIFIED";
      codes[5] = "INVALID DOMESTIC PHONE NUMBER FORMAT";
      codes[6] = "INVALID PHONE NUMBER";
      codes[7] = "PHONE NUMBER NOT SUPPORTED FOR COUNTRY";
      codes[8] = "PREMIUM RATE NUMBER NOT ALLOWED";
      codes[9] = "DISALLOWED NUMBER TYPE";
      codes[10] = "VALUE OUT OF RANGE";
      codes[11] = "CALLTRACKING NOT SUPPORTED FOR COUNTRY";
      codes[12] = "CUSTOMER NOT WHITELISTED FOR CALLTRACKING";
      codes[13] = "INVALID COUNTRY CODE";
      codes[14] = "INVALID APP ID";
      codes[15] = "MISSING ATTRIBUTES FOR FIELDS";
      codes[16] = "INVALID TYPE ID";
      codes[17] = "INVALID EMAIL ADDRESS";
      codes[18] = "INVALID HTTPS URL";
      codes[19] = "MISSING DELIVERY ADDRESS";
      codes[26] = "START DATE AFTER END DATE";
      codes[28] = "MISSING FEED ITEM START TIME";
      codes[29] = "MISSING FEED ITEM END TIME";
      codes[31] = "MISSING FEED ITEM ID";
      codes[35] = "VANITY PHONE NUMBER NOT ALLOWED";
      codes[36] = "INVALID REVIEW EXTENSION SNIPPET";
      codes[37] = "INVALID NUMBER FORMAT";
      codes[38] = "INVALID DATE FORMAT";
      codes[39] = "INVALID PRICE FORMAT";
      codes[40] = "UNKNOWN PLACEHOLDER FIELD";
      codes[41] = "MISSING ENHANCED SITELINK DESCRIPTION LINE";
      codes[42] = "REVIEW EXTENSION SOURCE INELIGIBLE";
      codes[43] = "HYPHENS IN REVIEW EXTENSION SNIPPET";
      codes[44] = "DOUBLE QUOTES IN REVIEW EXTENSION SNIPPET";
      codes[45] = "QUOTES IN REVIEW EXTENSION SNIPPET";
      codes[46] = "INVALID FORM ENCODED PARAMS";
      codes[47] = "INVALID URL PARAMETER NAME";
      codes[48] = "NO GEOCODING RESULT";
      codes[49] = "SOURCE NAME IN REVIEW EXTENSION TEXT";
      codes[50] = "CARRIER SPECIFIC SHORT NUMBER NOT ALLOWED";
      codes[51] = "INVALID PLACEHOLDER FIELD ID";
      codes[52] = "INVALID URL TAG";
      codes[53] = "LIST TOO LONG";
      codes[54] = "INVALID ATTRIBUTES COMBINATION";
      codes[55] = "DUPLICATE VALUES";
      codes[56] = "INVALID CALL CONVERSION TYPE ID";
      codes[57] = "CANNOT SET WITHOUT FINAL URLS";
      codes[58] = "CANNOT SET WITH FINAL URLS";
    }

    /// <summary>
    /// Looks up an error description by code.
    /// </summary>
    /// <param name='key'>The error code.</param>
    /// <returns>The error description, or the key if no description can be found.</returns>
    public static string Lookup(int key) {
      if (codes.ContainsKey(key)) {
        return codes[key];
      } else {
        return key.ToString();
      }
    }
  }
}
