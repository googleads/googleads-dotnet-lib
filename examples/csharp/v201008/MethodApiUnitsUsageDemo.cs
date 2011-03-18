// Copyright 2011, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util;
using Google.Api.Ads.AdWords.Util.Units;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201008 {
  /// <summary>
  /// This code example displays API method usage for this month for all methods
  /// provided by the AdWords API. Note that this data is not in real time and
  /// is refreshed every few hours.
  /// </summary>
  class MethodApiUnitsUsageDemo : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays API method usage for this month for all methods" +
            " provided by the AdWords API. Note that this data is not in real time and is" +
            " refreshed every few hours.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new MethodApiUnitsUsageDemo();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      user.ResetUnits();
      List<MethodQuotaUsage> methodQuotaUsage = UnitsUtilities.GetMethodQuotaUsage(user,
          DateTime.Now.AddMonths(-1), DateTime.Now);

      foreach (MethodQuotaUsage usage in methodQuotaUsage) {
        Console.WriteLine("{0,-50} - {1}", usage.ServiceName + "." + usage.MethodName,
            usage.Units);
      }
      Console.WriteLine("\nTotal Quota unit cost for this run: {0}.\n", user.GetUnits());
    }
  }
}
