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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201101;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201101 {
  /// <summary>
  /// This code example gets a company by its ID. To determine which companies
  /// exist, run GetAllCompanies.cs.
  ///
  /// Tags: CompanyService.getCompany
  /// </summary>
  class GetCompany : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a company by its ID. To determine which companies " +
            "exist, run GetAllCompanies.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCompany();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the CompanyService.
      CompanyService companyService =
          (CompanyService) user.GetService(DfpService.v201101.CompanyService);

      // Set the ID of the company to get.
      long companyId = long.Parse(_T("INSERT_COMPANY_ID_HERE"));

      try {
        // Get the company.
        Company company = companyService.getCompany(companyId);

        if (company != null) {
          Console.WriteLine("Company with ID = {0}, name = {1} and type = {2} was found",
              company.id, company.name, company.type);
        } else {
          Console.WriteLine("No company found for this ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get company. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
