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
  /// This code example adds express businesses. To get express businesses, run
  /// GetExpressBusinesses.cs.
  /// </summary>
  public class AddExpressBusinesses : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds express businesses. To get express businesses, run " +
            "GetExpressBusinesses.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddExpressBusinesses codeExample = new AddExpressBusinesses();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the ExpressBusinessService.
      ExpressBusinessService businessService = (ExpressBusinessService)
          user.GetService(AdWordsService.v201603.ExpressBusinessService);

      ExpressBusiness business1 = new ExpressBusiness();
      business1.status = ExpressBusinessStatus.ENABLED;
      business1.name = "Express Interplanetary Cruise #" + ExampleUtilities.GetShortRandomString();
      business1.website = "http://www.example.com/cruise1";

      ExpressBusinessOperation operation1 = new ExpressBusinessOperation();
      operation1.@operator = Operator.ADD;
      operation1.operand = business1;

      ExpressBusiness business2 = new ExpressBusiness();
      business2.status = (ExpressBusinessStatus.ENABLED);
      business2.name = "Express Interplanetary Cruise #" + ExampleUtilities.GetShortRandomString();
      business2.website = "http://www.example.com/cruise2";

      ExpressBusinessOperation operation2 = new ExpressBusinessOperation();
      operation2.@operator = Operator.ADD;
      operation2.operand = business2;

      try {
        ExpressBusiness[] addedBusinesses = businessService.mutate(
            new ExpressBusinessOperation[] {operation1, operation2});

        Console.WriteLine("Added {0} express businesses", addedBusinesses.Length);
        foreach (ExpressBusiness addedBusiness in addedBusinesses) {
          Console.WriteLine("Added express business with ID = {0} and name '{1}'.",
              addedBusiness.id, addedBusiness.name);
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to add express business.", e);
      }
    }
  }
}