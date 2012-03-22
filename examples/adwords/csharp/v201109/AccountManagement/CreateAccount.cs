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
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example illustrates how to create an account. Note by default,
  /// this account will only be accessible via parent MCC.
  ///
  /// Tags: CreateAccountService.mutate
  /// </summary>
  public class CreateAccount : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new CreateAccount();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create an account. Note by default " +
            "this account will only be accessible via parent MCC.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the CreateAccountService.
      CreateAccountService createAccountService =
          (CreateAccountService) user.GetService(AdWordsService.v201109.CreateAccountService);

      // Clear clientCustomerId field.
      createAccountService.RequestHeader.clientCustomerId = null;

      Account account = new Account();
      account.currencyCode = "EUR";
      account.dateTimeZone = "Europe/London";

      // Create the operation.
      CreateAccountOperation operation = new CreateAccountOperation();
      operation.@operator = Operator.ADD;
      operation.operand = account;
      operation.descriptiveName = "Account created with CreateAccountService";

      try {
        // Create the account. It is possible to create multiple accounts with
        // one request by sending an array of operations.
        Account[] accounts = createAccountService.mutate(new CreateAccountOperation[] {operation});

        // Display the results.
        if (accounts != null && accounts.Length > 0) {
          Account newAccount = accounts[0];
          writer.WriteLine("Account with customer ID '{0:###-###-####}' was successfully created.",
              newAccount.customerId);
        } else {
          writer.WriteLine("No accounts were created.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to create accounts.", ex);
      }
    }
  }
}
