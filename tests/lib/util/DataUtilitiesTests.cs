// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.lib.util;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace com.google.api.adwords.tests.lib.util {
  /// <summary>
  /// UnitTests for <see cref="DataUtilities"/> class.
  /// </summary>
  [TestFixture]
  public class DataUtilitiesTests {
    /// <summary>
    /// Test for DataUtilities.GetAllCategories.
    /// </summary>
    [Test]
    public void TestGetAllCategories() {
      List<Category> categories = DataUtilities.GetAllCategories();
      Assert.NotNull(categories, "Category list cannot be null.");

      // Remember to fix this test case whenever categories.csv changes.
      Assert.That(categories.Count == 717);
    }

    /// <summary>
    /// Test for DataUtilities.GetAllCountries.
    /// </summary>
    [Test]
    public void TestGetAllCountries() {
      List<Country> countries = DataUtilities.GetAllCountries();
      Assert.NotNull(countries, "Country list cannot be null.");

      // Remember to fix this test case whenever countries.csv changes.
      Assert.That(countries.Count == 233);
    }

    /// <summary>
    /// Test for DataUtilities.GetAllCurrencies.
    /// </summary>
    [Test]
    public void TestGetAllCurrencies() {
      List<Currency> currencies = DataUtilities.GetAllCurrencies();
      Assert.NotNull(currencies, "Currency list cannot be null.");

      // Remember to fix this test case whenever currencies.csv changes.
      Assert.That(currencies.Count == 50);
    }

    /// <summary>
    /// Test for DataUtilities.GetAllLanguages.
    /// </summary>
    [Test]
    public void TestGetAllLanguages() {
      List<Language> languages = DataUtilities.GetAllLanguages();
      Assert.NotNull(languages, "Language list cannot be null.");

      // Remember to fix this test case whenever languages.csv changes.
      Assert.That(languages.Count == 46);
    }

    /// <summary>
    /// Test for DataUtilities.GetAllOpRates.
    /// </summary>
    [Test]
    public void TestGetAllOpRates() {
      List<OpRates> opRates = DataUtilities.GetAllOpRates();
      Assert.NotNull(opRates, "OpRates list cannot be null.");

      // Remember to fix this test case whenever ops_rates.csv changes.
      Assert.That(opRates.Count == 90);
    }

    /// <summary>
    /// Test for DataUtilities.GetAllTimezones.
    /// </summary>
    [Test]
    public void TestGetAllTimezones() {
      List<Timezone> timeZones = DataUtilities.GetAllTimezones();
      Assert.NotNull(timeZones, "Timezone list cannot be null.");

      // Remember to fix this test case whenever timezones.csv changes.
      Assert.That(timeZones.Count == 313);
    }

    /// <summary>
    /// Test for DataUtilities.GetAllUsCities.
    /// </summary>
    [Test]
    public void TestGetAllUsCities() {
      List<UsCity> usCities = DataUtilities.GetAllUsCities();
      Assert.NotNull(usCities, "UsCity list cannot be null.");

      // Remember to fix this test case whenever us_cities.csv changes.
      Assert.That(usCities.Count == 15693);
    }

    /// <summary>
    /// Test for DataUtilities.GetAllUsCities.
    /// </summary>
    [Test]
    public void TestGetAllUsMetros() {
      List<UsMetro> usMetros = DataUtilities.GetAllUsMetros();
      Assert.NotNull(usMetros, "UsMetro list cannot be null.");

      // Remember to fix this test case whenever us_metros.csv changes.
      Assert.That(usMetros.Count == 281);
    }

    /// <summary>
    /// Test for DataUtilities.GetAllWorldCities.
    /// </summary>
    [Test]
    public void TestGetAllWorldCities() {
      List<WorldCity> worldCities = DataUtilities.GetAllWorldCities();
      Assert.NotNull(worldCities, "WorldCity list cannot be null.");

      // Remember to fix this test case whenever world_cities.csv changes.
      Assert.That(worldCities.Count == 6387);
    }

    /// <summary>
    /// Test for DataUtilities.GetAllWorldRegions.
    /// </summary>
    [Test]
    public void TestGetAllWorldRegions() {
      List<WorldRegion> worldRegions = DataUtilities.GetAllWorldRegions();
      Assert.NotNull(worldRegions, "WorldRegion list cannot be null.");

      // Remember to fix this test case whenever world_regions.csv changes.
      Assert.That(worldRegions.Count == 787);
    }

    /// <summary>
    /// Test if all the csvs in data folder are well-formed.
    /// </summary>
    [Test]
    public void TestReadDataCsvs() {
      Assembly clientLibAssembly = Assembly.GetAssembly(typeof(DataUtilities));
      if (clientLibAssembly != null) {
        string[] resources = clientLibAssembly.GetManifestResourceNames();
        if (resources != null) {
          foreach (string resource in resources) {
            Stream resourceStream = clientLibAssembly.GetManifestResourceStream(resource);
            StreamReader reader = new StreamReader(resourceStream);
            string contents = reader.ReadToEnd();
            reader.Close();
            resourceStream.Close();

            CsvFile csvFile = new CsvFile();
            csvFile.ReadFromString(contents, true);
            for (int i = 0; i < csvFile.Records.Count; i++) {
              Assert.AreEqual(csvFile.Headers.Count, csvFile.Records[i].Length,
                  "Record# " + (i + 1).ToString() + " in \"" + resource + "\" does not have " +
                  "the same number of fields as number of headers.");
            }
          }
        }
      }
    }
  }
}
