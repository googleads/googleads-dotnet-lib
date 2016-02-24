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
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example updates company comments. To determine which companies exist,
  /// run GetAllCompanies.cs.
  /// </summary>
  class UpdateCompanies : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates company comments. To detemine which companies exist, " +
            "run GetAllCompanies.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateCompanies();
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
          (CompanyService) user.GetService(DfpService.v201602.CompanyService);

      // Set the ID of the company to update.
      int companyId = int.Parse(_T("INSERT_COMPANY_ID_HERE"));

      // Create a statement to select the company by ID.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :companyId")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("companyId", companyId);

      try {
        // Get the companies by statement.
        CompanyPage page = companyService.getCompaniesByStatement(statementBuilder.ToStatement());

        Company company = page.results[0];

        // Update the company comment
        company.comment = company.comment + " Updated.";

        // Update the company on the server.
        Company[] companies = companyService.updateCompanies(new Company[] {company});

        foreach (Company updatedCompany in companies) {
          Console.WriteLine("Company with ID = {0}, name = {1}, and comment \"{2}\" was updated.",
              updatedCompany.id, updatedCompany.name, updatedCompany.comment);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update companies. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
