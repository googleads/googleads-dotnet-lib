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

using System;
using System.Collections.Generic;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example adds two rule-based remarketing user lists: one with no
  /// site visit date restrictions, and another that will only include users
  /// who visit your site in the next six months. See
  /// https://developers.google.com/adwords/api/docs/guides/rule-based-remarketing
  /// to learn more about rule based remarketing.
  /// </summary>
  public class AddRuleBasedRemarketingList : ExampleBase {
    private const string DATE_FORMAT_STRING = "yyyyMMdd";

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddRuleBasedRemarketingList codeExample = new AddRuleBasedRemarketingList();
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
        return "This code example adds two rule-based remarketing user lists: one with no " +
            "site visit date restrictions, and another that will only include users who " +
            "visit your site in the next six months. See " +
            "https://developers.google.com/adwords/api/docs/guides/rule-based-remarketing to " +
            "learn more about rule based remarketing.";
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

      // First rule item group - users who visited the checkout page and had
      // more than one item in their shopping cart.
      StringRuleItem checkoutStringRuleItem = new StringRuleItem();
      checkoutStringRuleItem.key = new StringKey();
      checkoutStringRuleItem.key.name = "ecomm_pagetype";
      checkoutStringRuleItem.op = StringRuleItemStringOperator.EQUALS;
      checkoutStringRuleItem.value = "checkout";

      RuleItem checkoutRuleItem = new RuleItem();
      checkoutRuleItem.Item = checkoutStringRuleItem;

      NumberRuleItem cartSizeNumberRuleItem = new NumberRuleItem();
      cartSizeNumberRuleItem.key = new NumberKey();
      cartSizeNumberRuleItem.key.name = "cartsize";
      cartSizeNumberRuleItem.op = NumberRuleItemNumberOperator.GREATER_THAN;
      cartSizeNumberRuleItem.value = 1;

      RuleItem cartSizeRuleItem = new RuleItem();
      cartSizeRuleItem.Item = cartSizeNumberRuleItem;

      // Combine the two rule items into a RuleItemGroup so AdWords will AND
      // their rules together.
      RuleItemGroup checkoutMultipleItemGroup = new RuleItemGroup();
      checkoutMultipleItemGroup.items = new RuleItem[] { checkoutRuleItem, cartSizeRuleItem };

      // Second rule item group - users who check out within the next 3 months.
      DateRuleItem startDateDateRuleItem = new DateRuleItem();
      startDateDateRuleItem.key = new DateKey();
      startDateDateRuleItem.key.name = "checkoutdate";
      startDateDateRuleItem.op = DateRuleItemDateOperator.AFTER;
      startDateDateRuleItem.value = DateTime.Now.ToString(DATE_FORMAT_STRING);
      RuleItem startDateRuleItem = new RuleItem();
      startDateRuleItem.Item = startDateDateRuleItem;

      DateRuleItem endDateDateRuleItem = new DateRuleItem();
      endDateDateRuleItem.key = new DateKey();
      endDateDateRuleItem.key.name = "checkoutdate";
      endDateDateRuleItem.op = DateRuleItemDateOperator.BEFORE;
      endDateDateRuleItem.value = DateTime.Now.AddMonths(3).ToString(DATE_FORMAT_STRING);
      RuleItem endDateRuleItem = new RuleItem();
      endDateRuleItem.Item = endDateDateRuleItem;

      // Combine the date rule items into a RuleItemGroup.
      RuleItemGroup checkedOutNextThreeMonthsItemGroup = new RuleItemGroup();
      checkedOutNextThreeMonthsItemGroup.items =
          new RuleItem[] { startDateRuleItem, endDateRuleItem };

      // Combine the rule item groups into a Rule so AdWords will OR the groups
      // together.
      Rule rule = new Rule();
      rule.groups = new RuleItemGroup[] {checkoutMultipleItemGroup,
        checkedOutNextThreeMonthsItemGroup};

      // Create the user list with no restrictions on site visit date.
      ExpressionRuleUserList expressionUserList = new ExpressionRuleUserList();
      expressionUserList.name = "Expression based user list created at " + DateTime.Now.ToString(
          "yyyyMMdd_HHmmss");
      expressionUserList.description = "Users who checked out in three month window OR visited " +
          "the checkout page with more than one item in their cart.";
      expressionUserList.rule = rule;

      // Create the user list restricted to users who visit your site within
      // the next six months.
      DateTime startDate = DateTime.Now;
      DateTime endDate = startDate.AddMonths(6);

      DateSpecificRuleUserList dateUserList = new DateSpecificRuleUserList();
      dateUserList.name = "Date rule user list created at " +
          DateTime.Now.ToString("yyyyMMdd_HHmmss");
      dateUserList.description = String.Format("Users who visited the site between {0} and " +
          "{1} and checked out in three month window OR visited the checkout page " +
          "with more than one item in their cart.", startDate.ToString(DATE_FORMAT_STRING),
          endDate.ToString(DATE_FORMAT_STRING));
      dateUserList.rule = rule;

      // Set the start and end dates of the user list.
      dateUserList.startDate = startDate.ToString(DATE_FORMAT_STRING);
      dateUserList.endDate = endDate.ToString(DATE_FORMAT_STRING);

      // Create operations to add the user lists.
      List<UserListOperation> operations = new List<UserListOperation>();
      foreach (UserList userList in new UserList[] { expressionUserList, dateUserList }) {
        UserListOperation operation = new UserListOperation();
        operation.operand = userList;
        operation.@operator = Operator.ADD;
        operations.Add(operation);
      }

      try {
        // Submit the operations.
        UserListReturnValue result = userListService.mutate(operations.ToArray());

        // Display the results.
        foreach (UserList userListResult in result.value) {
          Console.WriteLine("User list added with ID {0}, name '{1}', status '{2}', " +
              "list type '{3}', accountUserListStatus '{4}', description '{5}'.",
              userListResult.id,
              userListResult.name,
              userListResult.status,
              userListResult.listType,
              userListResult.accountUserListStatus,
              userListResult.description);
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to add rule based user lists.", e);
      }
    }
  }
}