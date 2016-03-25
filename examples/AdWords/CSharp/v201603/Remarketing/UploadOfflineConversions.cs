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
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example imports offline conversion values for specific clicks to
  /// your account. To get Google Click ID for a click, run
  /// CLICK_PERFORMANCE_REPORT. To set up a conversion tracker, run the
  /// AddConversionTracker.cs example.
  /// </summary>
  public class UploadOfflineConversions : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      string conversionName = "INSERT_CONVERSION_NAME_HERE";
      // GCLID needs to be newer than 30 days.
      string gClId = "INSERT_GOOGLE_CLICK_ID_HERE";
      //  The conversion time should be higher than the click time.
      string conversionTime = "INSERT_CONVERSION_TIME_HERE";
      double conversionValue = double.Parse("INSERT_CONVERSION_VALUE_HERE");

      UploadOfflineConversions codeExample = new UploadOfflineConversions();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), conversionName, gClId, conversionTime, conversionValue);
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
        return "This code example imports offline conversion values for specific clicks to " +
          "your account. To get Google Click ID for a click, run CLICK_PERFORMANCE_REPORT. " +
            " To set up a conversion tracker, run the AddConversionTracker.cs example.";
      }
    }

    /// <summary>
    /// Runs the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="conversionName">The name of the upload conversion to be
    /// created.</param>
    /// <param name="gClid">The Google Click ID of the click for which offline
    /// conversions are uploaded.</param>
    /// <param name="conversionValue">The conversion value to be uploaded.
    /// </param>
    /// <param name="conversionTime">The conversion time, in yyyymmdd hhmmss
    /// format.</param>
    public void Run(AdWordsUser user, String conversionName, String gClid, String conversionTime,
        double conversionValue) {
       // Get the OfflineConversionFeedService.
      OfflineConversionFeedService offlineConversionFeedService =
          (OfflineConversionFeedService) user.GetService(
              AdWordsService.v201603.OfflineConversionFeedService);

      try {
        // Associate offline conversions with the existing named conversion tracker. If
        // this tracker was newly created, it may be a few hours before it can accept
        // conversions.
        OfflineConversionFeed feed = new OfflineConversionFeed();
        feed.conversionName = conversionName;
        feed.conversionTime = conversionTime;
        feed.conversionValue = conversionValue;
        feed.googleClickId = gClid;

        OfflineConversionFeedOperation offlineConversionOperation =
            new OfflineConversionFeedOperation();
        offlineConversionOperation.@operator = Operator.ADD;
        offlineConversionOperation.operand = feed;

        OfflineConversionFeedReturnValue offlineConversionRetval =
            offlineConversionFeedService.mutate(
                new OfflineConversionFeedOperation[] {offlineConversionOperation});

        OfflineConversionFeed newFeed = offlineConversionRetval.value[0];

        Console.WriteLine("Uploaded offline conversion value of {0} for Google Click ID = " +
            "'{1}' to '{2}'.", newFeed.conversionValue, newFeed.googleClickId,
            newFeed.conversionName);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed upload offline conversions.", e);
      }
    }
  }
}
