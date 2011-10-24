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
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example illustrates how to retrieve all carriers available for
  /// targeting.
  ///
  /// Tags: ConstantDataService.getCarrierCriterion
  /// </summary>
  class GetAllCarriers : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to retrieve all carriers available for " +
            "targeting.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllCarriers();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the ConstantDataService.
      ConstantDataService constantDataService = (ConstantDataService) user.GetService(
          AdWordsService.v201109.ConstantDataService);

      try {
        // Get all carriers from ConstantDataService.
        Carrier[] carriers = constantDataService.getCarrierCriterion();

        if (carriers != null) {
          foreach (Carrier carrier in carriers) {
            Console.WriteLine("Carrier name is '{0}', ID is {1} and country code is '{2}'.",
                carrier.name, carrier.id, carrier.countryCode);
          }
        } else {
          Console.WriteLine("No carriers were retrieved.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get carriers. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
