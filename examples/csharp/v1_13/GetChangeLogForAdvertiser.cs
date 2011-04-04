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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_13;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_13 {
  /// <summary>
  /// This code example displays the change logs of a specified advertiser
  /// object. Results are limited to the first 10 records.
  ///
  /// A similar pattern can be applied to get change logs for many other object
  /// types. Run GetChangeLogObjectTypes.cs for a list of other supported object
  /// types and their id numbers.
  ///
  /// Tags: changelog.getChangeLogRecords
  /// </summary>
  class GetChangeLogForAdvertiser : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays the change logs of a specified advertiser object. " +
            "Results are limited to the first 10 records.\n\nA similar pattern can be applied " +
            "to get change logs for many other object types. Run GetChangeLogObjectTypes.cs " +
            "for a list of other supported object types and their id numbers.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetChangeLogForAdvertiser();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create ChangeLogRemoteService instance.
      ChangeLogRemoteService service = (ChangeLogRemoteService) user.GetService(
          DfaService.v1_13.ChangeLogRemoteService);

      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));

      // Create change log search criteria structure.
      ChangeLogRecordSearchCriteria searchCritera = new ChangeLogRecordSearchCriteria();
      searchCritera.pageSize = 10;
      searchCritera.objectId = advertiserId;
      // The following field has been filled in to choose advertiser change
      // logs. This values was determined using GetChangeLogObjectTypes.cs.
      searchCritera.objectTypeId = 1;

      try {
        // Get change log record set.
        ChangeLogRecordSet changeLogRecordSet = service.getChangeLogRecords(searchCritera);

        // Set up a formatter to make the change log timestamps human-readable.
        string dateFormat = "yyyy-M-d H:m:s";

        // Display the contents of each change log record
        if (changeLogRecordSet != null && changeLogRecordSet.records != null) {
          foreach (ChangeLogRecord changeLogRecord in changeLogRecordSet.records) {
            Console.WriteLine("Action \"{0}\", Context \"{1}\", Change Date \"{2}\", New Value " +
                "\"{3}\", Old Value \"{4}\", Profile Name \"{5}\" was found.",
                changeLogRecord.action, changeLogRecord.context,
                changeLogRecord.changeDate.Value.ToString(dateFormat), changeLogRecord.newValue,
                changeLogRecord.oldValue, changeLogRecord.username);
          }
        } else {
          Console.WriteLine("No change logs found for your search criteria.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve change logs. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
