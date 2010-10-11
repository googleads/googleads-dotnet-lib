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
using com.google.api.adwords.v201008;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.examples.v201008 {
  /// <summary>
  /// This code example illustrates how to retrieve all the user lists for
  /// an account.
  ///
  /// Tags: UserListService.get
  /// </summary>
  class GetAllUserLists : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to retrieve all the user lists for an account.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the UserListService.
      UserListService userListService =
          (UserListService) user.GetService(AdWordsService.v201008.UserListService);

      try {
        UserListPage page = userListService.get(new UserListSelector());

        if (page != null && page.entries != null) {
          foreach (UserList userList in page.entries) {
            Console.WriteLine("User list name is \"{0}\", id is {1}, status is \"{2}\" and " +
                "number of users is {3}.", userList.name, userList.id, userList.status,
                userList.size);
          }
        } else {
          Console.WriteLine("No user lists were found.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve user lists. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
