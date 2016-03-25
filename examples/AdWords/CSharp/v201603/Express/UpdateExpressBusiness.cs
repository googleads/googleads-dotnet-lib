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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example updates an express business. To add an express
  /// business, run AddExpressBusinesses.cs.
  /// </summary>
  public class UpdateExpressBusiness : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates an express business. To add an express business, run " +
            "AddExpressBusinesses.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      UpdateExpressBusiness codeExample = new UpdateExpressBusiness();
      Console.WriteLine(codeExample.Description);
      try {
        long businessId = long.Parse("INSERT_ADWORDS_EXPRESS_BUSINESS_ID_HERE");
        codeExample.Run(new AdWordsUser(), businessId);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="businessId">The AdWords Express business id.</param>
    public void Run(AdWordsUser user, long businessId) {
      // Get the ExpressBusinessService.
      ExpressBusinessService businessService = (ExpressBusinessService)
          user.GetService(AdWordsService.v201603.ExpressBusinessService);

      // Update the website and address for the business
      ExpressBusiness business = new ExpressBusiness();
      business.id = businessId;
      business.name = "Express Interplanetary Cruise #" + ExampleUtilities.GetShortRandomString();
      business.website = "http://www.example.com/?myParam=" + businessId;

      ExpressBusinessOperation operation = new ExpressBusinessOperation();
      operation.@operator = Operator.SET;
      operation.operand = business;

      try {
        ExpressBusiness[] updatedBusinesses =
            businessService.mutate(new ExpressBusinessOperation[] { operation });

        Console.WriteLine("Express business with ID {0} and name '{1}' was updated.",
            updatedBusinesses[0].id, updatedBusinesses[0].name);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to update express business.", e);
      }
    }
  }
}