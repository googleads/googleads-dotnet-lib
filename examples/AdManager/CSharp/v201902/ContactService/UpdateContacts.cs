// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v201902;
using Google.Api.Ads.AdManager.Util.v201902;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    /// This code example updates contact addresses. To determine which contacts
    /// exist, run GetAllContacts.cs.
    /// </summary>
    public class UpdateContacts : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example updates contact comments. To determine which contacts " +
                    "exist, run GetAllContacts.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateContacts codeExample = new UpdateContacts();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ContactService contactService = user.GetService<ContactService>())
            {
                // Set the ID of the contact to update.
                long contactId = long.Parse(_T("INSERT_CONTACT_ID_HERE"));

                try
                {
                    StatementBuilder statementBuilder = new StatementBuilder()
                        .Where("id = :id")
                        .OrderBy("id ASC")
                        .Limit(1)
                        .AddValue("id", contactId);

                    // Get the contact.
                    ContactPage page =
                        contactService.getContactsByStatement(statementBuilder.ToStatement());
                    Contact contact = page.results[0];

                    // Update the address of the contact.
                    contact.address = "123 New Street, New York, NY, 10011";

                    // Update the contact on the server.
                    Contact[] contacts = contactService.updateContacts(new Contact[]
                    {
                        contact
                    });

                    // Display results.
                    foreach (Contact updatedContact in contacts)
                    {
                        Console.WriteLine(
                            "Contact with ID \"{0}\", name \"{1}\", and comment \"{2}\" was " +
                            "updated.", updatedContact.id, updatedContact.name,
                            updatedContact.comment);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update contacts. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
