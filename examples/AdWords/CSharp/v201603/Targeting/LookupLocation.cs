// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example gets location criteria by name.
  /// </summary>
  public class LookupLocation : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      LookupLocation codeExample = new LookupLocation();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets location criteria by name.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the LocationCriterionService.
      LocationCriterionService locationCriterionService =
          (LocationCriterionService) user.GetService(AdWordsService.v201603.
              LocationCriterionService);

      string[] locationNames = new string[] { "Paris", "Quebec", "Spain", "Deutschland" };

      Selector selector = new Selector() {
        fields = new string[] {
          Location.Fields.Id, Location.Fields.LocationName, LocationCriterion.Fields.CanonicalName,
          Location.Fields.DisplayType, Location.Fields.ParentLocations,
          LocationCriterion.Fields.Reach, Location.Fields.TargetingStatus
        },

        predicates = new Predicate[] {
          // Location names must match exactly, only EQUALS and IN are supported.
          Predicate.In(Location.Fields.LocationName, locationNames),

          // Set the locale of the returned location names.
          Predicate.Equals(LocationCriterion.Fields.Locale, "en")
        }
      };

      try {
        // Make the get request.
        LocationCriterion[] locationCriteria = locationCriterionService.get(selector);

        // Display the resulting location criteria.
        foreach (LocationCriterion locationCriterion in locationCriteria) {
          string parentLocations = "";
          if (locationCriterion.location != null &&
              locationCriterion.location.parentLocations != null) {
            foreach (Location location in locationCriterion.location.parentLocations) {
              parentLocations += GetLocationString(location) + ", ";
            }
            parentLocations.TrimEnd(',', ' ');
          } else {
            parentLocations = "N/A";
          }
          Console.WriteLine("The search term '{0}' returned the location '{1}' of type '{2}' " +
              "with parent locations '{3}',  reach '{4}' and targeting status '{5}.",
              locationCriterion.searchTerm, locationCriterion.location.locationName,
              locationCriterion.location.displayType, parentLocations, locationCriterion.reach,
              locationCriterion.location.targetingStatus);
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to get location criteria.", e);
      }
    }

    /// <summary>
    /// Gets a string representation for a location.
    /// </summary>
    /// <param name="location">The location</param>
    /// <returns>The string representation</returns>
    public string GetLocationString(Location location) {
      return string.Format("{0} ({1})", location.locationName, location.displayType);
    }
  }
}
