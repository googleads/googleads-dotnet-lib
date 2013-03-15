// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201302;
using Google.Api.Ads.Dfp.Util.v201302;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201302 {
  /// <summary>
  /// This code example gets all contacts that aren't invited yet. To create
  /// contacts, run CreateContacts.cs.
  ///
  /// Tags: ContactService.getContactsByStatement
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
          (ContactService) user.GetService(DfpService.v201302.ContactService);

      Statement filterStatement = new StatementBuilder("").AddValue(
          "status", ContactStatus.UNINVITED.ToString()).ToStatement();

      // Set defaults for page and filterStatement.
      ContactPage page = new ContactPage();
      int offset = 0;

      try {
        do {
          // Create a statement to get all contacts.
          filterStatement.query = "where status = :status LIMIT 500 OFFSET " + offset.ToString();

          // Get contacts by statement.
          page = contactService.getContactsByStatement(filterStatement);

          if (page.results != null) {
            int i = page.startIndex;
            foreach (Contact contact in page.results) {
              Console.WriteLine("{0}) Contact with ID \"{1}\" and name \"{2}\" was found.",
                  i, contact.id, contact.name);
              i++;
            }
          }
          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of results found: " + page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get contacts. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
