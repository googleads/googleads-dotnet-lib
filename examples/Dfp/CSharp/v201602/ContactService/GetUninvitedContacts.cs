// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201602;
using Google.Api.Ads.Dfp.Util.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all contacts that aren't invited yet. To create
  /// contacts, run CreateContacts.cs.
  /// </summary>
  class GetUninvitedContacts : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all contacts that aren't invited yet. To create " +
            "contacts, run CreateContacts.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetUninvitedContacts();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ContactService.
      ContactService contactService =
          (ContactService) user.GetService(DfpService.v201602.ContactService);

      // Create a statement to get all uninvited contacts.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("status = :status")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("status", ContactStatus.UNINVITED.ToString());

      // Set default for page.
      ContactPage page = new ContactPage();

      try {
        do {
          // Get contacts by statement.
          page = contactService.getContactsByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            int i = page.startIndex;
            foreach (Contact contact in page.results) {
              Console.WriteLine("{0}) Contact with ID \"{1}\" and name \"{2}\" was found.",
                  i, contact.id, contact.name);
              i++;
            }
          }
          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: " + page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get contacts. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
