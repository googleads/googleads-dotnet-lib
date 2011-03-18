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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example illustrates how to create a logical user list.
  ///
  /// Tags: UserListService.mutate
  /// </summary>
  class AddLogicalUserList : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create a logical user list.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddLogicalUserList();
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
          (UserListService) user.GetService(AdWordsService.v201101.UserListService);

      LogicalUserList userList = new LogicalUserList();
      userList.name = "Mars cruise customers #" + GetTimeStamp();
      userList.description = "A list of mars cruise customers in the last year.";
      userList.status = UserListMembershipStatus.OPEN;
      userList.membershipLifeSpan = 365;

      // Make an UserInterest group for Travel > Cruises & Charters. See
      // http://code.google.com/apis/adwords/docs/appendix/verticals.html for
      // various verticals and their ids.
      UserInterest interest = new UserInterest();
      interest.name = "Mars cruise interest group";
      interest.sizeRange = SizeRange.FIFTY_THOUSAND_TO_ONE_HUNDRED_THOUSAND;
      interest.id = 206;

      LogicalUserListOperand userListOperand = new LogicalUserListOperand();
      userListOperand.Item = interest;

      UserListLogicalRule rule = new UserListLogicalRule();
      rule.@operator = UserListLogicalRuleOperator.NONE;
      rule.ruleOperands = new LogicalUserListOperand[]{userListOperand};

      userList.rules = new UserListLogicalRule[] {rule};

      UserListOperation operation = new UserListOperation();
      operation.operand = userList;
      operation.@operator = Operator.ADD;

      try {
        // Add user list.
        UserListReturnValue retval = userListService.mutate(new UserListOperation[] {operation});
        if (retval != null && retval.value != null & retval.value.Length > 0) {
          UserList tempUserList = retval.value[0];
          Console.WriteLine("Logical user list with name \"{0}\" and id {1} was added.",
              tempUserList.name, tempUserList.id);
        } else {
          Console.WriteLine("No logical user lists were added.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to add logical user lists. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
