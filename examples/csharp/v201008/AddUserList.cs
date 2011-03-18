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
using Google.Api.Ads.AdWords.v201008;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201008 {
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
          (UserListService) user.GetService(AdWordsService.v201008.UserListService);

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
        if (retval != null && retval.value != null & retval.value.Length > 0) {
          UserList tempUserList = retval.value[0];
          Console.WriteLine("User list with name \"{0}\" and id {1} was added.",
              tempUserList.name, tempUserList.id);
        } else {
          Console.WriteLine("No user lists were added.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to add user lists. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
