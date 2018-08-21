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
using Google.Api.Ads.AdWords.v201806;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code example illustrates how to create a user list a.k.a. audience.
    /// </summary>
    public class AddAudience : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddAudience codeExample = new AddAudience();
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
                return "This code example illustrates how to create a user list a.k.a. audience.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            using (AdwordsUserListService userListService =
                (AdwordsUserListService) user.GetService(AdWordsService.v201806
                    .AdwordsUserListService))
                using (ConversionTrackerService conversionTrackerService =
                    (ConversionTrackerService) user.GetService(AdWordsService.v201806
                        .ConversionTrackerService))
                {
                    BasicUserList userList = new BasicUserList
                    {
                        name = "Mars cruise customers #" + ExampleUtilities.GetRandomString(),
                        description = "A list of mars cruise customers in the last year.",
                        status = UserListMembershipStatus.OPEN,
                        membershipLifeSpan = 365
                    };

                    UserListConversionType conversionType = new UserListConversionType
                    {
                        name = userList.name
                    };
                    userList.conversionTypes = new UserListConversionType[]
                    {
                        conversionType
                    };

                    // Optional: Set the user list status.
                    userList.status = UserListMembershipStatus.OPEN;

                    // Create the operation.
                    UserListOperation operation = new UserListOperation
                    {
                        operand = userList,
                        @operator = Operator.ADD
                    };

                    try
                    {
                        // Add the user list.
                        UserListReturnValue retval = userListService.mutate(new UserListOperation[]
                        {
                            operation
                        });

                        UserList newUserList = retval.value[0];

                        Console.WriteLine("User list with name '{0}' and id '{1}' was added.",
                            newUserList.name, newUserList.id);

                        List<string> conversionIds = new List<string>();
                        Array.ForEach(userList.conversionTypes,
                            delegate(UserListConversionType item)
                            {
                                conversionIds.Add(item.id.ToString());
                            });

                        // Create the selector.
                        Selector selector = new Selector()
                        {
                            fields = new string[]
                            {
                                ConversionTracker.Fields.Id,
                                ConversionTracker.Fields.GoogleGlobalSiteTag,
                                ConversionTracker.Fields.GoogleEventSnippet
                            },
                            predicates = new Predicate[]
                            {
                                Predicate.In(ConversionTracker.Fields.Id, conversionIds.ToArray())
                            }
                        };

                        // Get all conversion trackers.
                        ConversionTrackerPage page = conversionTrackerService.get(selector);

                        if (page != null && page.entries != null)
                        {
                            foreach (ConversionTracker tracker in page.entries)
                            {
                                Console.WriteLine(
                                    "Google global site tag:\n{0}\nGoogle event snippet:\n{1}",
                                    tracker.googleGlobalSiteTag, tracker.googleGlobalSiteTag);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new System.ApplicationException(
                            "Failed to add user lists (a.k.a. audiences).", e);
                    }
                }
        }
    }
}
