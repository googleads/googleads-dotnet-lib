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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates new custom targeting keys and values. To
  /// determine which custom targeting keys and values exist, run
  /// GetAllCustomTargetingKeysAndValues.cs. To target these custom targeting
  /// keys and values, run TargetCustomCriteria.cs.
  /// </summary>
  class CreateCustomTargetingKeysAndValues : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new custom targeting keys and values. To determine " +
          "which custom targeting keys and values exist, run " +
          "GetAllCustomTargetingKeysAndValues.cs. To target these custom targeting keys and " +
          "values, run TargetCustomCriteria.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateCustomTargetingKeysAndValues();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the CustomTargetingService.
      CustomTargetingService customTargetingService =
          (CustomTargetingService) user.GetService(DfpService.v201602.CustomTargetingService);

      // Create predefined key.
      CustomTargetingKey genderKey = new CustomTargetingKey();
      genderKey.displayName = "gender";
      genderKey.name = "g";
      genderKey.type = CustomTargetingKeyType.PREDEFINED;

      // Create predefined key that may be used for content targeting.
      CustomTargetingKey genreKey = new CustomTargetingKey();
      genreKey.displayName = "genre";
      genreKey.name = "genre";
      genreKey.type = CustomTargetingKeyType.PREDEFINED;

      // Create free-form key.
      CustomTargetingKey carModelKey = new CustomTargetingKey();
      carModelKey.displayName = "car model";
      carModelKey.name = "c";
      carModelKey.type = CustomTargetingKeyType.FREEFORM;

      try {
        // Create the custom targeting keys on the server.
        CustomTargetingKey[] keys = customTargetingService.createCustomTargetingKeys(
            new CustomTargetingKey[] {genderKey, genreKey, carModelKey});

        if (keys != null) {
          foreach (CustomTargetingKey key in keys) {
            Console.WriteLine("A custom targeting key with ID \"{0}\", name \"{1}\", and display " +
                "name \"{2}\" was created.", key.id, key.name, key.displayName);
          }
        } else {
          Console.WriteLine("No keys were created.");
        }

        // Create custom targeting value for the predefined gender key.
        CustomTargetingValue genderMaleValue = new CustomTargetingValue();
        genderMaleValue.customTargetingKeyId = keys[0].id;
        genderMaleValue.displayName = "male";
        // Name is set to 1 so that the actual name can be hidden from website
        // users.
        genderMaleValue.name = "1";
        genderMaleValue.matchType = CustomTargetingValueMatchType.EXACT;

        CustomTargetingValue genderFemaleValue = new CustomTargetingValue();
        genderFemaleValue.customTargetingKeyId = keys[0].id;
        genderFemaleValue.displayName = "female";
        // Name is set to 2 so that the actual name can be hidden from website
        // users.
        genderFemaleValue.name = "2";
        genderFemaleValue.matchType = CustomTargetingValueMatchType.EXACT;

        // Create custom targeting value for the predefined genre key.
        CustomTargetingValue genreComedyValue = new CustomTargetingValue();
        genreComedyValue.customTargetingKeyId = keys[1].id;
        genreComedyValue.displayName = "comedy";
        genreComedyValue.name = "comedy";
        genreComedyValue.matchType = CustomTargetingValueMatchType.EXACT;

        CustomTargetingValue genreDramaValue = new CustomTargetingValue();
        genreDramaValue.customTargetingKeyId = keys[1].id;
        genreDramaValue.displayName = "drama";
        genreDramaValue.name = "drama";
        genreDramaValue.matchType = CustomTargetingValueMatchType.EXACT;

        // Create custom targeting value for the free-form age key. These are
        // values that would be suggested in the UI or can be used when
        // targeting with a FreeFormCustomCriteria.
        CustomTargetingValue carModelHondaCivicValue = new CustomTargetingValue();
        carModelHondaCivicValue.customTargetingKeyId = keys[2].id;
        carModelHondaCivicValue.displayName = "honda civic";
        carModelHondaCivicValue.name = "honda civic";
        // Setting match type to exact will match exactly "honda civic".
        carModelHondaCivicValue.matchType = CustomTargetingValueMatchType.EXACT;

        // Create the custom targeting values on the server.
        CustomTargetingValue[] returnValues = customTargetingService.createCustomTargetingValues(
            new CustomTargetingValue[] {genderMaleValue, genderFemaleValue, genreComedyValue,
                genreDramaValue, carModelHondaCivicValue});

        if (returnValues != null) {
          foreach (CustomTargetingValue value in returnValues) {
            Console.WriteLine("A custom targeting value with ID \"{0}\", belonging to key with " +
                "ID \"{1}\", name \"{2}\", and display name \"{3}\" was created.", value.id,
                value.customTargetingKeyId, value.name, value.displayName);
          }
        } else {
          Console.WriteLine("No values were created.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create custom targeting keys and values. Exception " +
            "says \"{0}\"", e.Message);
      }
    }
  }
}
