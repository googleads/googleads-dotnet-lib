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
    /// This code example creates new companies. To determine which companies
    /// exist, run GetAllCompanies.cs.
    /// </summary>
    public class CreateCompanies : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates new companies. To determine which companies " +
                    "exist, run GetAllCompanies.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateCompanies codeExample = new CreateCompanies();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (CompanyService companyService = user.GetService<CompanyService>())
            {
                // Create an array to store local company objects.
                Company[] companies = new Company[5];

                for (int i = 0; i < 5; i++)
                {
                    Company company = new Company();
                    company.name = string.Format("Advertiser #{0}", i);
                    company.type = CompanyType.ADVERTISER;
                    companies[i] = company;
                }

                try
                {
                    // Create the companies on the server.
                    companies = companyService.createCompanies(companies);

                    if (companies != null && companies.Length > 0)
                    {
                        foreach (Company company in companies)
                        {
                            Console.WriteLine(
                                "A company with ID = '{0}', name = '{1}' and type = '{2}' was" +
                                " created.", company.id, company.name, company.type);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No companies created.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create company. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
