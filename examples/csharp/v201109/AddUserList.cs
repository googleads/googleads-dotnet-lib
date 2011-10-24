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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example illustrates how to create a user list.
  ///
  /// Tags: UserListService.mutate
  /// </summary>
  class AddUserList : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create a user list.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddUserList();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the UserListService.
      UserListService userListService =
          (UserListService) user.GetService(AdWordsService.v201109.UserListService);

      // Get the ConversionTrackerService.
      ConversionTrackerService conversionTrackerService =
          (ConversionTrackerService)user.GetService(AdWordsService.v201109.
              ConversionTrackerService);

      RemarketingUserList userList = new RemarketingUserList();
      userList.name = "Mars cruise customers #" + GetTimeStamp();
      userList.description = "A list of mars cruise customers in the last year.";
      userList.status = UserListMembershipStatus.OPEN;
      userList.membershipLifeSpan = 365;

      UserListConversionType conversionType = new UserListConversionType();
      conversionType.name = userList.name;
      userList.conversionTypes = new UserListConversionType[] {conversionType};

      UserListOperation operation = new UserListOperation();
      operation.operand = userList;
      operation.@operator = Operator.ADD;

      try {
        // Add user list.
        UserListReturnValue retval = userListService.mutate(new UserListOperation[] {operation});
        UserList[] userLists = null;
        if (retval != null && retval.value != null & retval.value.Length > 0) {
          userLists = retval.value;
          // Get all conversion snippets
          List<string> conversionIds = new List<string>();
          foreach (RemarketingUserList tempUserList in userLists) {
            if (tempUserList.conversionTypes != null) {
              foreach (UserListConversionType tempConversionType in userList.conversionTypes) {
                conversionIds.Add(tempConversionType.id.ToString());
              }
            }
          }

          Dictionary<long, ConversionTracker> conversionsMap =
              new Dictionary<long, ConversionTracker>();

          if (conversionIds.Count > 0) {
            // Create selector.
            Predicate conversionTypePredicate = new Predicate();
            conversionTypePredicate.field = "Id";
            conversionTypePredicate.@operator = PredicateOperator.IN;
            conversionTypePredicate.values = conversionIds.ToArray();

            Selector selector = new Selector();
            selector.fields = new string[] {"Id"};
            selector.predicates = new Predicate[] {conversionTypePredicate};

            // Get all conversion trackers.
            ConversionTrackerPage page = conversionTrackerService.get(selector);

            if (page != null && page.entries != null) {
              foreach (ConversionTracker tracker in page.entries) {
                conversionsMap[tracker.id] = tracker;
              }
            }
          }

          // Display results.
          foreach (RemarketingUserList tempUserList in userLists) {
            Console.WriteLine("User list with name '{0}' and id '{1}' was added.",
               tempUserList.name, tempUserList.id);

            // Display user list associated conversion code snippets.
            if (tempUserList.conversionTypes != null) {
              foreach (UserListConversionType tempConversionType
                  in tempUserList.conversionTypes) {
                AdWordsConversionTracker conversionTracker =
                    (AdWordsConversionTracker)conversionsMap[tempConversionType.id];
                Console.WriteLine("Conversion type code snippet associated to the list:\n{0}\n",
                  conversionTracker.snippet);
              }
            }
          }
        } else {
          Console.WriteLine("No user lists were added.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to add user lists. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
