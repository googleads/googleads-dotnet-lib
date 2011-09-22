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
using Google.Api.Ads.Dfp.v201108;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;


namespace Google.Api.Ads.Dfp.Tests.v201108 {
  /// <summary>
  /// UnitTests for <see cref="CompanyService"/> class.
  /// </summary>
  [TestFixture]
  public class CompanyServiceTests : BaseTests {
    /// <summary>
    /// UnitTests for <see cref="CompanyService"/> class.
    /// </summary>
    private CompanyService companyService;

    /// <summary>
    /// The company to be used for further tests.
    /// </summary>
    private Company testCompany1 = null;

    /// <summary>
    /// The company to be used for further tests.
    /// </summary>
    private Company testCompany2 = null;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public CompanyServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      companyService = (CompanyService) user.GetService(DfpService.v201108.CompanyService);
      testCompany1 = utils.CreateCompany(user, CompanyType.ADVERTISER);
      testCompany2 = utils.CreateCompany(user, CompanyType.ADVERTISER);
    }


    /// <summary>
    /// Test whether we can create a company.
    /// </summary>
    [Test]
    public void TestCreateCompany() {
      Company company = new Company();
      company.name = string.Format("Company #{0}", new TestUtils().GetTimeStamp());
      company.type = CompanyType.ADVERTISER;

      Company newCompany = null;
      Assert.DoesNotThrow(delegate() {
        newCompany = companyService.createCompany(company);
      });

      Assert.NotNull(newCompany);
      Assert.AreEqual(company.name, newCompany.name);
      Assert.AreEqual(company.type, newCompany.type);
    }

    /// <summary>
    /// Test whether we can create a list of companies.
    /// </summary>
    [Test]
    public void TestCreateCompanies() {
      Company company1 = new Company();
      company1.name = string.Format("Company #{0}", new TestUtils().GetTimeStamp());
      company1.type = CompanyType.ADVERTISER;

      Company company2 = new Company();
      company2.name = string.Format("Company #{0}", new TestUtils().GetTimeStamp());
      company2.type = CompanyType.ADVERTISER;

      Company[] newCompanies = null;
      Assert.DoesNotThrow(delegate() {
        newCompanies = companyService.createCompanies(new Company[] {company1, company2});
      });

      Assert.NotNull(newCompanies);
      Assert.AreEqual(newCompanies.Length, 2);

      Assert.AreEqual(company1.name, newCompanies[0].name);
      Assert.AreEqual(company1.type, newCompanies[0].type);
      Assert.AreEqual(company2.name, newCompanies[1].name);
      Assert.AreEqual(company2.type, newCompanies[1].type);
    }

    /// <summary>
    /// Test whether we can fetch an existing company.
    /// </summary>
    [Test]
    public void TestGetCompany() {
      Company company1 = null;

      Assert.DoesNotThrow(delegate() {
        company1 = companyService.getCompany(testCompany1.id);
      });

      Assert.NotNull(company1);
      Assert.AreEqual(testCompany1.id, company1.id);
      Assert.AreEqual(testCompany1.name, company1.name);
    }

    /// <summary>
    /// Test whether we can fetch a list of existing companies that match given
    /// statement.
    /// </summary>
    [Test]
    public void TestGetCompaniesByStatement() {
      Statement statement = new Statement();
      statement.query = string.Format("WHERE id = {0} ORDER BY name LIMIT 1", testCompany1.id);
      CompanyPage page = null;

      Assert.DoesNotThrow(delegate() {
        page = companyService.getCompaniesByStatement(statement);
      });

      Assert.NotNull(page);
      Assert.NotNull(page.results);
      Assert.AreEqual(page.results.Length, 1);
      Assert.NotNull(page.results[0]);
      Assert.AreEqual(page.results[0].id, testCompany1.id);
      Assert.AreEqual(page.results[0].name, testCompany1.name);
    }

    /// <summary>
    /// Test whether we can update a company.
    /// </summary>
    [Test]
    public void TestUpdateCompany() {
      testCompany1.name = "Corp " + testCompany1.name;
      Company newCompany = null;

      Assert.DoesNotThrow(delegate() {
        newCompany = companyService.updateCompany(testCompany1);
      });

      Assert.NotNull(newCompany);
      Assert.AreEqual(newCompany.id, testCompany1.id);
      Assert.AreEqual(newCompany.name, testCompany1.name);
    }

    /// <summary>
    /// Test whether we can update a list of companies.
    /// </summary>
    [Test]
    public void TestUpdateCompanies() {
      testCompany1.name = "Corp " + testCompany1.name;
      testCompany2.name = "Corp " + testCompany2.name;

      Company[] newCompanies = null;

      Assert.DoesNotThrow(delegate() {
        newCompanies = companyService.updateCompanies(new Company[] {testCompany1, testCompany2});
      });

      Assert.NotNull(newCompanies);
      Assert.AreEqual(newCompanies.Length, 2);

      Assert.AreEqual(testCompany1.name, newCompanies[0].name);
      Assert.AreEqual(testCompany1.type, newCompanies[0].type);
      Assert.AreEqual(testCompany2.name, newCompanies[1].name);
      Assert.AreEqual(testCompany2.type, newCompanies[1].type);
    }
  }
}
