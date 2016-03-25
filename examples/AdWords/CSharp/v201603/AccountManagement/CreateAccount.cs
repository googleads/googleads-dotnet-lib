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
  /// This code example illustrates how to create an account. Note by default,
  /// this account will only be accessible via its parent AdWords manager
  /// account.
  /// </summary>
  public class CreateAccount : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      CreateAccount codeExample = new CreateAccount();
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
        return "This code example illustrates how to create an account. Note by default " +
            "this account will only be accessible via its parent AdWords manager account.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the ManagedCustomerService.
      ManagedCustomerService managedCustomerService = (ManagedCustomerService) user.GetService(
          AdWordsService.v201603.ManagedCustomerService);

      // Create account.
      ManagedCustomer customer = new ManagedCustomer();
      customer.name = "Customer created with ManagedCustomerService on " +
          new DateTime().ToString();
      customer.currencyCode = "EUR";
      customer.dateTimeZone = "Europe/London";

      // Create operations.
      ManagedCustomerOperation operation = new ManagedCustomerOperation();
      operation.operand = customer;
      operation.@operator = Operator.ADD;
      try {
        ManagedCustomerOperation[] operations = new ManagedCustomerOperation[] {operation};
        // Add account.
        ManagedCustomerReturnValue result = managedCustomerService.mutate(operations);

        // Display accounts.
        if (result.value != null && result.value.Length > 0) {
          ManagedCustomer customerResult = result.value[0];
          Console.WriteLine("Account with customer ID \"{0}\" was created.",
              customerResult.customerId);
        } else {
          Console.WriteLine("No accounts were created.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create accounts.", e);
      }
    }
  }
}
