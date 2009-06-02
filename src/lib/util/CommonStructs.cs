// Copyright 2009, Google Inc. All Rights Reserved.
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

using System;

namespace com.google.api.adwords.lib.util {
  /// <summary>
  /// Represents one category code that can be used with
  /// SiteSuggestionService.getSitesByCategoryName. See
  /// <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_categories.html">this page</a> for details.
  /// </summary>
  public struct Category {
    /// <summary>
    /// Path of the category code in the hierarchy.
    /// </summary>
    public string path;

    /// <summary>
    /// A string that identifies the category uniquely.
    /// </summary>
    public string category;
  }

  /// Represents a country or territory code you can use for targeting
  /// your ads. See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_countries.html">this page</a> for details.
  /// </summary>
  public struct Country {
    /// <summary>
    /// Name of the country.
    /// </summary>
    public string name;

    /// <summary>
    /// The code for this country.
    /// </summary>
    public string code;
  }

  /// Represents a currency code you can use to specify the currency of
  /// monetary values. See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_currency.html">this page</a> for details.
  /// </summary>
  public struct Currency {
    /// <summary>
    /// Code for the currency.
    /// </summary>
    public string code;

    /// <summary>
    /// Name of the currency.
    /// </summary>
    public string name;
  }

  /// <summary>
  /// Represents a language code you can use while targeting ads or setting
  /// the preferred language for the AdWords web interface for a user.
  /// See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_languages.html">this page</a> for details.
  /// </summary>
  public struct Language {
    /// <summary>
    /// Name of the language.
    /// </summary>
    public string name;

    /// <summary>
    /// Code to be used while targeting ads.
    /// </summary>
    public string criteriaCode;

    /// <summary>
    /// Code to be used while specifying the perferred language for AdWords
    /// web interface for a user.
    /// </summary>
    public string displayCode;
  }

  /// <summary>
  /// Represents the cost in units for an operation in AdWords API.
  /// See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_ratesheet.html">this page</a> for details.
  /// </summary>
  public struct OpRates {
    /// <summary>
    /// Name of the service.
    /// </summary>
    public string serviceName;

    /// <summary>
    /// Name of the method.
    /// </summary>
    public string methodName;

    /// <summary>
    /// Rate of the API call.
    /// </summary>
    public int rate;

    /// <summary>
    /// Is the API rate for a single call, or for each item used in a
    /// call? If this field is true, then you have to multiply rate by
    /// number of items in the operation to get the actual cost for
    /// this method call.
    /// </summary>
    public bool isPerItem;
  }

  /// Represents a time zone code used by AccountInfo.timeZoneId.
  /// See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_timezones.html">this page</a> for details.
  /// </summary>
  public struct Timezone {
    /// <summary>
    /// The timezone code.
    /// </summary>
    public string code;
  }

  /// <summary>
  /// Represents a city code you can use for targeting ads at cities in the
  /// United States. See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_us_cities.html">this page</a> for details.
  /// </summary>
  public struct UsCity {
    /// <summary>
    /// The state name for this city.
    /// </summary>
    public string state;

    /// <summary>
    /// Code for this city.
    /// </summary>
    public string code;
  }

  /// <summary>
  /// Represents a metro code you can use to target ads at metropolitan regions
  /// in the United States. See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_us_metros.html">this page</a> for details.
  /// </summary>
  public struct UsMetro {
    /// <summary>
    /// State name for this city.
    /// </summary>
    public string state;

    /// <summary>
    /// Metro name for this city.
    /// </summary>
    public string metro;

    /// <summary>
    /// Code for this metro region.
    /// </summary>
    public string code;
  }

  /// <summary>
  /// Represents a world city code you can use for targeting ads at
  /// cities across the world.
  /// See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_cities.html">this page</a> for details.
  /// </summary>
  public struct WorldCity {
    /// <summary>
    /// The country for this city.
    /// </summary>
    public string country;

    /// <summary>
    /// Code for this city.
    /// </summary>
    public string code;
  }

  /// <summary>
  /// Represents a region codes that you can use to target ads at
  /// specific geographical regions of the world.
  /// See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_regions.html">this page</a> for details.
  /// </summary>
  public struct WorldRegion {
    /// <summary>
    /// The country to which this region belongs.
    /// </summary>
    public string country;

    /// <summary>
    /// The code for this region.
    /// </summary>
    public string code;

    /// <summary>
    /// The name of this region.
    /// </summary>
    public string region;
  }

  /// <summary>
  /// Represents the quota usage of a method in AdWords API, in units.
  /// </summary>
  public struct MethodQuotaUsage {
    /// <summary>
    /// Name of the service.
    /// </summary>
    public string serviceName;

    /// <summary>
    /// Name of the method.
    /// </summary>
    public string methodName;

    /// <summary>
    /// Units consumed.
    /// </summary>
    public long units;
  }
}
