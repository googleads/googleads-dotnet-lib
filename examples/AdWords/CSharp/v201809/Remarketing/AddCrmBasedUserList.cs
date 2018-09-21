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
using System.Security.Cryptography;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example adds a user list (a.k.a. audience) and uploads hashed
    /// email addresses to populate the list.
    ///
    /// <p>
    /// <em>Note:</em> It may take up to several hours for the list to be
    /// populated with members. Email addresses must be associated with a Google
    /// account. For privacy purposes, the user list size will show as zero until
    /// the list has at least 1000 members. After that, the size will be rounded
    /// to the two most significant digits.
    /// </p>
    /// </summary>
    public class AddCrmBasedUserList : ExampleBase
    {
        private static readonly string[] EMAILS = new string[]
        {
            "customer1@example.com",
            "customer2@example.com",
            " Customer3@example.com "
        };

        private const string FIRST_NAME = "John";
        private const string LAST_NAME = "Doe";
        private const string COUNTRY_CODE = "US";
        private const string ZIP_CODE = "10001";

        private SHA256 digest = SHA256.Create();

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddCrmBasedUserList codeExample = new AddCrmBasedUserList();
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
                return "This code example adds a user list (a.k.a. audience) and " +
                    "uploads hashed email addresses to populate the list.";
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
                // Create a user list.
                CrmBasedUserList userList = new CrmBasedUserList()
                {
                    name = "Customer relationship management list #" +
                        ExampleUtilities.GetRandomString(),
                    description = "A list of customers that originated from email addresses",

                    // CRM - based user lists can use a membershipLifeSpan of 10000 to indicate
                    // unlimited; otherwise normal values apply.
                    membershipLifeSpan = 30L,
                    uploadKeyType = CustomerMatchUploadKeyType.CONTACT_INFO
                };

                // Create operation.
                UserListOperation operation = new UserListOperation()
                {
                    operand = userList,
                    @operator = Operator.ADD
                };

                try
                {
                    // Add user list.
                    UserListReturnValue result = userListService.mutate(new UserListOperation[]
                    {
                        operation
                    });

                    Console.WriteLine(
                        "Created new user list with name = '{0}' and id = " + "'{1}'.",
                        result.value[0].name, result.value[0].id);

                    // Get user list ID.
                    long userListId = result.value[0].id;

                    // Prepare the emails for upload.
                    List<Member> memberList = new List<Member>();

                    // Hash normalized email addresses based on SHA-256 hashing algorithm.
                    string[] emailHashes = new string[EMAILS.Length];
                    for (int i = 0; i < EMAILS.Length; i++)
                    {
                        Member member = new Member
                        {
                            hashedEmail = ToSha256String(digest, ToNormalizedEmail(EMAILS[i]))
                        };
                        memberList.Add(member);
                    }

                    ;

                    // Add a user by first and last name.
                    AddressInfo addressInfo = new AddressInfo
                    {
                        // First and last name must be normalized and hashed.
                        hashedFirstName = ToSha256String(digest, FIRST_NAME),
                        hashedLastName = ToSha256String(digest, LAST_NAME),
                        // Country code and zip code are sent in plaintext.
                        zipCode = ZIP_CODE,
                        countryCode = COUNTRY_CODE
                    };

                    Member memberByAddress = new Member
                    {
                        addressInfo = addressInfo
                    };
                    memberList.Add(memberByAddress);

                    // Create operation to add members to the user list based on email
                    // addresses.
                    MutateMembersOperation mutateMembersOperation = new MutateMembersOperation()
                    {
                        operand = new MutateMembersOperand()
                        {
                            userListId = userListId,
                            membersList = memberList.ToArray()
                        },
                        @operator = Operator.ADD
                    };

                    // Add members to the user list based on email addresses.
                    MutateMembersReturnValue mutateMembersResult = userListService.mutateMembers(
                        new MutateMembersOperation[]
                        {
                            mutateMembersOperation
                        });

                    // Display results.
                    // Reminder: it may take several hours for the list to be populated
                    // with members.
                    foreach (UserList userListResult in mutateMembersResult.userLists)
                    {
                        Console.WriteLine(
                            "Email addresses were added to user list with " +
                            "name '{0}' and id '{1}'.", userListResult.name, userListResult.id);
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException(
                        "Failed to add user lists " +
                        "(a.k.a. audiences) and upload email addresses.", e);
                }
            }
        }

        /// <summary>
        /// Hash email address using SHA-256 hashing algorithm.
        /// </summary>
        /// <param name="digest">Provides the algorithm for SHA-256.</param>
        /// <param name="email">The email address to hash.</param>
        /// <returns>Hash email address using SHA-256 hashing algorithm.</returns>
        private static string ToSha256String(SHA256 digest, string email)
        {
            byte[] digestBytes = digest.ComputeHash(Encoding.UTF8.GetBytes(email));
            // Convert the byte array into an unhyphenated hexadecimal string.
            return BitConverter.ToString(digestBytes).Replace("-", string.Empty);
        }

        /// <summary>
        /// Removes leading and trailing whitespace and converts all characters to
        /// lower case.
        /// </summary>
        /// <param name="email">The email address to normalize.</param>
        /// <returns>A normalized copy of the string.</returns>
        private static string ToNormalizedEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
