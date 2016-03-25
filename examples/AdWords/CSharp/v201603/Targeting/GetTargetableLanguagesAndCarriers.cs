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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example illustrates how to retrieve all carriers and languages
  /// available for targeting.
  /// </summary>
  public class GetTargetableLanguagesAndCarriers : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetTargetableLanguagesAndCarriers codeExample = new GetTargetableLanguagesAndCarriers();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to retrieve all carriers and languages " +
            "available for targeting.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the ConstantDataService.
      ConstantDataService constantDataService = (ConstantDataService) user.GetService(
          AdWordsService.v201603.ConstantDataService);

      try {
        // Get all carriers.
        Carrier[] carriers = constantDataService.getCarrierCriterion();

        // Display the results.
        if (carriers != null) {
          foreach (Carrier carrier in carriers) {
            Console.WriteLine("Carrier name is '{0}', ID is {1} and country code is '{2}'.",
                carrier.name, carrier.id, carrier.countryCode);
          }
        } else {
          Console.WriteLine("No carriers were retrieved.");
        }

        // Get all languages.
        Language[] languages = constantDataService.getLanguageCriterion();

        // Display the results.
        if (languages != null) {
          foreach (Language language in languages) {
            Console.WriteLine("Language name is '{0}', ID is {1} and code is '{2}'.",
                language.name, language.id, language.code);
          }
        } else {
          Console.WriteLine("No languages were found.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to get targetable carriers and languages.",
            e);
      }
    }
  }
}
