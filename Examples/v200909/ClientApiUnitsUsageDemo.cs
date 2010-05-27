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

using com.google.api.adwords.lib;
using com.google.api.adwords.lib.util;
using com.google.api.adwords.v13;

using System;
using System.Collections.Generic;
using System.Collections;

namespace com.google.api.adwords.examples.v200909 {
  /// <summary>
  /// This code example displays API units usage attributed to each client and
  /// sub-MCC this month. It should be noted that this data is not in
  /// real time and is refreshed every few hours.
  /// </summary>
  class ClientApiUnitsUsageDemo : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays API units usage attributed to each client and" +
            " sub-MCC this month. It should be noted that this data is not in" +
            " real time and is refreshed every few hours.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      user.ResetUnits();
      SortedList<string, long> clientUsage = null;
      long totalUnits = 0;
      long diffUnits = 0;
      DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
      DateTime endDate = DateTime.Today;
      UnitsUtilities.GetClientQuotaUsage(user, startDate, endDate, out clientUsage,
          out totalUnits, out diffUnits);

      Console.WriteLine("\nTotal units consumed between {0} and {1}: {2}",
          startDate.ToString("d MMM yyyy"), endDate.ToString("d MMM yyyy"), totalUnits);
      Console.WriteLine("\nBreakup of unit consumption by account (rolled up to MCCs)");
      Console.WriteLine("==========================================================\n");
      foreach (string email in clientUsage.Keys) {
        Console.WriteLine("{0,-40} : {1,10}", email, clientUsage[email]);
      }
      Console.WriteLine("\nDifference between units consumed and rolledup values : {0}", diffUnits);
      Console.WriteLine("\nTotal units consumed for this run : {0}", user.GetUnits());
    }
  }
}
