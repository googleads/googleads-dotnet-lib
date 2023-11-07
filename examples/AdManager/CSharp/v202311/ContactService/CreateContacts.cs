// Copyright 2019 Google LLC
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
using Google.Api.Ads.AdManager.v202311;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example creates new contacts. To determine which contacts exist,
    /// run GetAllContacts.cs.
    /// </summary>
    public class CreateContacts : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This code example creates new contacts. To determine which contacts exist, " +
                    "run GetAllContacts.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateContacts codeExample = new CreateContacts();
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
                // Set the IDs of the companies for the contacts.
                long advertiserCompanyId = long.Parse(_T("INSERT_ADVERTISER_COMPANY_ID_HERE"));
                long agencyCompanyId = long.Parse(_T("INSERT_AGENCY_COMPANY_ID_HERE"));

                // Create an advertiser contact.
                Contact advertiserContact = new Contact();
                advertiserContact.name = "Mr. Advertiser #" + GetTimeStamp();
                advertiserContact.email = "advertiser@advertising.com";
                advertiserContact.companyId = advertiserCompanyId;

                // Create an agency contact.
                Contact agencyContact = new Contact();
                agencyContact.name = "Ms. Agency #" + GetTimeStamp();
                agencyContact.email = "agency@agencies.com";
                agencyContact.companyId = agencyCompanyId;

                try
                {
                    // Create the contacts on the server.
                    Contact[] contacts = contactService.createContacts(new Contact[]
                    {
                        advertiserContact,
                        agencyContact
                    });

                    foreach (Contact contact in contacts)
                    {
                        Console.WriteLine("A contact with ID \"{0}\" and name \"{1}\" was created.",
                            contact.id, contact.name);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create contacts. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
