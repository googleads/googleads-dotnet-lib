// Copyright 2018 Google LLC
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
using Google.Api.Ads.AdWords.v201809;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example adds two rule-based remarketing user lists: one with no
    /// site visit date restrictions, and another that will only include users
    /// who visit your site in the next six months. See
    /// https://developers.google.com/adwords/api/docs/guides/rule-based-remarketing
    /// to learn more about rule based remarketing.
    /// </summary>
    public class AddRuleBasedRemarketingList : ExampleBase
    {
        private const string DATE_FORMAT_STRING = "yyyyMMdd";

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddRuleBasedRemarketingList codeExample = new AddRuleBasedRemarketingList();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdWordsUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while running this code example. {0}",
                    ExampleUtilities.FormatException(e));
            }
        }

        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example adds two rule-based remarketing user lists: one with " +
                    "no site visit date restrictions, and another that will only include users " +
                    "who visit your site in the next six months. See " +
                    "https://developers.google.com/adwords/api/docs/guides/rule-based-remarketing" +
                    " to learn more about rule based remarketing.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            using (AdwordsUserListService userListService =
                (AdwordsUserListService) user.GetService(AdWordsService.v201809
                    .AdwordsUserListService))
            {
                // First rule item group - users who visited the checkout page and had
                // more than one item in their shopping cart.
                StringRuleItem checkoutStringRuleItem = new StringRuleItem
                {
                    key = new StringKey
                    {
                        name = "ecomm_pagetype"
                    },
                    op = StringRuleItemStringOperator.EQUALS,
                    value = "checkout"
                };

                RuleItem checkoutRuleItem = new RuleItem
                {
                    Item = checkoutStringRuleItem
                };

                NumberRuleItem cartSizeNumberRuleItem = new NumberRuleItem
                {
                    key = new NumberKey
                    {
                        name = "cartsize"
                    },
                    op = NumberRuleItemNumberOperator.GREATER_THAN,
                    value = 1
                };

                RuleItem cartSizeRuleItem = new RuleItem
                {
                    Item = cartSizeNumberRuleItem
                };

                // Combine the two rule items into a RuleItemGroup so AdWords will AND
                // their rules together.
                RuleItemGroup checkoutMultipleItemGroup = new RuleItemGroup
                {
                    items = new RuleItem[]
                    {
                        checkoutRuleItem,
                        cartSizeRuleItem
                    }
                };

                // Second rule item group - users who check out within the next 3 months.
                DateRuleItem startDateDateRuleItem = new DateRuleItem
                {
                    key = new DateKey
                    {
                        name = "checkoutdate"
                    },
                    op = DateRuleItemDateOperator.AFTER,
                    value = DateTime.Now.ToString(DATE_FORMAT_STRING)
                };
                RuleItem startDateRuleItem = new RuleItem
                {
                    Item = startDateDateRuleItem
                };

                DateRuleItem endDateDateRuleItem = new DateRuleItem
                {
                    key = new DateKey
                    {
                        name = "checkoutdate"
                    },
                    op = DateRuleItemDateOperator.BEFORE,
                    value = DateTime.Now.AddMonths(3).ToString(DATE_FORMAT_STRING)
                };
                RuleItem endDateRuleItem = new RuleItem
                {
                    Item = endDateDateRuleItem
                };

                // Combine the date rule items into a RuleItemGroup.
                RuleItemGroup checkedOutNextThreeMonthsItemGroup = new RuleItemGroup
                {
                    items = new RuleItem[]
                    {
                        startDateRuleItem,
                        endDateRuleItem
                    }
                };

                // Combine the rule item groups into a Rule so AdWords knows how to apply the rules.
                Rule rule = new Rule
                {
                    groups = new RuleItemGroup[]
                    {
                        checkoutMultipleItemGroup,
                        checkedOutNextThreeMonthsItemGroup
                    },

                    // ExpressionRuleUserLists can use either CNF Or DNF For matching. CNF means
                    // 'at least one item in each rule item group must match', and DNF means 'at
                    // least one entire rule item group must match'.
                    // DateSpecificRuleUserList only supports DNF. You can also omit the rule
                    // type altogether To Default To DNF.
                    ruleType = UserListRuleTypeEnumsEnum.DNF
                };

                // Third and fourth rule item groups.
                // Visitors of a page who visited another page. See
                // https://developers.google.com/adwords/api/docs/reference/latest/AdwordsUserListService.StringKey
                // for more details.
                StringKey urlStringKey = new StringKey()
                {
                    name = "url__"
                };

                StringRuleItem site1StringRuleItem = new StringRuleItem
                {
                    key = urlStringKey,
                    op = StringRuleItemStringOperator.EQUALS,
                    value = "example.com/example1"
                };
                RuleItem site1RuleItem = new RuleItem
                {
                    Item = site1StringRuleItem
                };

                StringRuleItem site2StringRuleItem = new StringRuleItem
                {
                    key = (urlStringKey),
                    op = (StringRuleItemStringOperator.EQUALS),
                    value = ("example.com/example2")
                };
                RuleItem site2RuleItem = new RuleItem
                {
                    Item = (site2StringRuleItem)
                };

                // Create two RuleItemGroups to show that a visitor browsed two sites.
                RuleItemGroup site1RuleItemGroup = new RuleItemGroup
                {
                    items = new RuleItem[]
                    {
                        site1RuleItem
                    }
                };
                RuleItemGroup site2RuleItemGroup = new RuleItemGroup
                {
                    items = new RuleItem[]
                    {
                        site2RuleItem
                    }
                };

                // Create two rules to show that a visitor browsed two sites.
                Rule userVisitedSite1Rule = new Rule
                {
                    groups = new RuleItemGroup[]
                    {
                        site1RuleItemGroup
                    }
                };

                Rule userVisitedSite2Rule = new Rule
                {
                    groups = new RuleItemGroup[]
                    {
                        site2RuleItemGroup
                    }
                };

                // Create the user list with no restrictions on site visit date.
                ExpressionRuleUserList expressionUserList = new ExpressionRuleUserList();
                string creationTimeString = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                expressionUserList.name =
                    "Expression based user list created at " + creationTimeString;
                expressionUserList.description = "Users who checked out in three month window OR " +
                    "visited the checkout page with more than one item in their cart.";
                expressionUserList.rule = rule;

                // Optional: Set the prepopulationStatus to REQUESTED to include past users
                // in the user list.
                expressionUserList.prepopulationStatus =
                    RuleBasedUserListPrepopulationStatus.REQUESTED;

                // Create the user list restricted to users who visit your site within
                // the next six months.
                DateTime startDate = DateTime.Now;
                DateTime endDate = startDate.AddMonths(6);

                DateSpecificRuleUserList dateUserList = new DateSpecificRuleUserList
                {
                    name = "Date rule user list created at " + creationTimeString,
                    description = string.Format(
                        "Users who visited the site between {0} and " +
                        "{1} and checked out in three month window OR visited the checkout page " +
                        "with more than one item in their cart.",
                        startDate.ToString(DATE_FORMAT_STRING),
                        endDate.ToString(DATE_FORMAT_STRING)),
                    rule = rule,

                    // Set the start and end dates of the user list.
                    startDate = startDate.ToString(DATE_FORMAT_STRING),
                    endDate = endDate.ToString(DATE_FORMAT_STRING)
                };

                // Create the user list where "Visitors of a page who did visit another page".
                // To create a user list where "Visitors of a page who did not visit another
                // page", change the ruleOperator from AND to AND_NOT.
                CombinedRuleUserList combinedRuleUserList = new CombinedRuleUserList
                {
                    name = "Combined rule user list created at " + creationTimeString,
                    description = "Users who visited two sites.",
                    leftOperand = userVisitedSite1Rule,
                    rightOperand = userVisitedSite2Rule,
                    ruleOperator = CombinedRuleUserListRuleOperator.AND
                };

                // Create operations to add the user lists.
                List<UserListOperation> operations = new List<UserListOperation>();
                foreach (UserList userList in new UserList[]
                {
                    expressionUserList,
                    dateUserList,
                    combinedRuleUserList
                })
                {
                    UserListOperation operation = new UserListOperation
                    {
                        operand = userList,
                        @operator = Operator.ADD
                    };
                    operations.Add(operation);
                }

                try
                {
                    // Submit the operations.
                    UserListReturnValue result = userListService.mutate(operations.ToArray());

                    // Display the results.
                    foreach (UserList userListResult in result.value)
                    {
                        Console.WriteLine(
                            "User list added with ID {0}, name '{1}', status '{2}', " +
                            "list type '{3}', accountUserListStatus '{4}', description '{5}'.",
                            userListResult.id, userListResult.name, userListResult.status,
                            userListResult.listType, userListResult.accountUserListStatus,
                            userListResult.description);
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to add rule based user lists.",
                        e);
                }
            }
        }
    }
}
