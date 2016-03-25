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
  /// This code example illustrates how to create a user list a.k.a. audience.
  /// </summary>
  public class AddAudience : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddAudience codeExample = new AddAudience();
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
        return "This code example illustrates how to create a user list a.k.a. audience.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the UserListService.
      AdwordsUserListService userListService =
          (AdwordsUserListService) user.GetService(AdWordsService.v201603.AdwordsUserListService);

      // Get the ConversionTrackerService.
      ConversionTrackerService conversionTrackerService =
          (ConversionTrackerService)user.GetService(AdWordsService.v201603.
              ConversionTrackerService);

      BasicUserList userList = new BasicUserList();
      userList.name = "Mars cruise customers #" + ExampleUtilities.GetRandomString();
      userList.description = "A list of mars cruise customers in the last year.";
      userList.status = UserListMembershipStatus.OPEN;
      userList.membershipLifeSpan = 365;

      UserListConversionType conversionType = new UserListConversionType();
      conversionType.name = userList.name;
      userList.conversionTypes = new UserListConversionType[] {conversionType};

      // Optional: Set the user list status.
      userList.status = UserListMembershipStatus.OPEN;

      // Create the operation.
      UserListOperation operation = new UserListOperation();
      operation.operand = userList;
      operation.@operator = Operator.ADD;

      try {
        // Add the user list.
        UserListReturnValue retval = userListService.mutate(new UserListOperation[] {operation});

        UserList[] userLists = null;
        if (retval != null && retval.value != null) {
          userLists = retval.value;
          // Get all conversion snippets
          List<string> conversionIds = new List<string>();
          foreach (BasicUserList newUserList in userLists) {
            if (newUserList.conversionTypes != null) {
              foreach (UserListConversionType newConversionType in newUserList.conversionTypes) {
                conversionIds.Add(newConversionType.id.ToString());
              }
            }
          }

          Dictionary<long, ConversionTracker> conversionsMap =
              new Dictionary<long, ConversionTracker>();

          if (conversionIds.Count > 0) {
            // Create the selector.
            Selector selector = new Selector() {
              fields = new string[] { ConversionTracker.Fields.Id },
              predicates = new Predicate[] {
                Predicate.In(ConversionTracker.Fields.Id, conversionIds)
              }
            };

            // Get all conversion trackers.
            ConversionTrackerPage page = conversionTrackerService.get(selector);

            if (page != null && page.entries != null) {
              foreach (ConversionTracker tracker in page.entries) {
                conversionsMap[tracker.id] = tracker;
              }
            }
          }

          // Display the results.
          foreach (BasicUserList newUserList in userLists) {
            Console.WriteLine("User list with name '{0}' and id '{1}' was added.",
                newUserList.name, newUserList.id);

            // Display user list associated conversion code snippets.
            if (newUserList.conversionTypes != null) {
              foreach (UserListConversionType userListConversionType in
                  newUserList.conversionTypes) {
                if (conversionsMap.ContainsKey(userListConversionType.id)) {
                  AdWordsConversionTracker conversionTracker =
                      (AdWordsConversionTracker) conversionsMap[userListConversionType.id];
                  Console.WriteLine("Conversion type code snippet associated to the list:\n{0}\n",
                      conversionTracker.snippet);
                } else {
                  throw new Exception("Failed to associate conversion type code snippet.");
                }
              }
            }
          }
        } else {
          Console.WriteLine("No user lists (a.k.a. audiences) were added.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to add user lists (a.k.a. audiences).", e);
      }
    }
  }
}
