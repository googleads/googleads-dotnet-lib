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
    /// This code example shows how to upload offline data for store sales transactions.
    /// </summary>
    public class UploadOfflineData : ExampleBase
    {
        private SHA256 digest = SHA256.Create();

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            // The external upload ID can be any number that you use to keep track of your uploads.
            long externalUploadId = long.Parse("INSERT_EXTERNAL_UPLOAD_ID");

            // Insert the conversion type name that you'd like to attribute this upload to.
            string conversionName = "INSERT_CONVERSION_NAME";

            // Insert email addresses below for creating user identifiers.
            string[] emailAddresses =
            {
                "EMAIL_ADDRESS_1",
                "EMAIL_ADDRESS_2"
            };

            // Insert advertiser upload time. // For times, use the format yyyyMMdd HHmmss tz. For
            // more details on formats, see:
            // https://developers.google.com/adwords/api/docs/appendix/codes-formats#date-and-time-formats
            // For time zones, see:
            // https://developers.google.com/adwords/api/docs/appendix/codes-formats#timezone-ids
            string advertiserUploadTime = "INSERT_ADVERTISER_UPLOAD_TIME";

            // Insert bridge map version ID.
            string bridgeMapVersionId = "INSERT_BRIDGEMAP_VERSION_ID";

            // Insert partner ID.
            int partnerId = int.Parse("INSERT_PARTNER_ID");

            // Specify the upload type (STORE_SALES_UPLOAD_FIRST_PARTY or
            // STORE_SALES_UPLOAD_THIRD_PARTY)
            OfflineDataUploadType uploadType =
                (OfflineDataUploadType) Enum.Parse(typeof(OfflineDataUploadType),
                    "INSERT_UPLOAD_TYPE");

            UploadOfflineData codeExample = new UploadOfflineData();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdWordsUser(), conversionName, externalUploadId, emailAddresses,
                    advertiserUploadTime, bridgeMapVersionId, uploadType, partnerId);
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
                return "This code example shows how to upload offline data for store sales " +
                    "transactions.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="conversionName">The conversion type name that you'd like to attribute this
        /// upload to.</param>
        /// <param name="externalUploadId">The external upload ID can be any number that you use to
        /// keep track of your uploads.</param>
        /// <param name="emailAddresses">The email addresses for creating user identifiers.</param>
        /// <param name="advertiserUploadTime">The advertiser upload time. For times, use the format
        /// yyyyMMdd HHmmss tz. For more details on formats, see:
        /// https://developers.google.com/adwords/api/docs/appendix/codes-formats#date-and-time-formats
        /// For time zones, see:
        /// https://developers.google.com/adwords/api/docs/appendix/codes-formats#timezone-ids
        /// </param>
        /// <param name="bridgeMapVersionId">The version ID of the bridge map.</param>
        /// <param name="uploadType">The type of data upload.</param>
        /// <param name="partnerId">The partner ID</param>
        public void Run(AdWordsUser user, string conversionName, long externalUploadId,
            string[] emailAddresses, string advertiserUploadTime, string bridgeMapVersionId,
            OfflineDataUploadType uploadType, int partnerId)
        {
            using (OfflineDataUploadService offlineDataUploadService =
                (OfflineDataUploadService) user.GetService(AdWordsService.v201809
                    .OfflineDataUploadService))
            {
                offlineDataUploadService.RequestHeader.partialFailure = true;

                // Create the first offline data row for upload.
                // This transaction occurred 7 days ago with amount of 200 USD.
                DateTime transactionTime1 = DateTime.Now;
                transactionTime1.AddDays(-7);
                long transactionAmount1 = 200000000;
                string transactionCurrencyCode1 = "USD";
                UserIdentifier[] userIdentifierList1 = new UserIdentifier[]
                {
                    CreateUserIdentifier(OfflineDataUploadUserIdentifierType.HASHED_EMAIL,
                        emailAddresses[0]),
                    CreateUserIdentifier(OfflineDataUploadUserIdentifierType.STATE, "New York")
                };
                OfflineData offlineData1 = CreateOfflineDataRow(transactionTime1,
                    transactionAmount1, transactionCurrencyCode1, conversionName,
                    userIdentifierList1);

                // Create the second offline data row for upload.
                // This transaction occurred 14 days ago with amount of 450 EUR.
                DateTime transactionTime2 = DateTime.Now;
                transactionTime2.AddDays(-14);
                long transactionAmount2 = 450000000;
                string transactionCurrencyCode2 = "EUR";
                UserIdentifier[] userIdentifierList2 = new UserIdentifier[]
                {
                    CreateUserIdentifier(OfflineDataUploadUserIdentifierType.HASHED_EMAIL,
                        emailAddresses[1]),
                    CreateUserIdentifier(OfflineDataUploadUserIdentifierType.STATE, "California")
                };
                OfflineData offlineData2 = CreateOfflineDataRow(transactionTime2,
                    transactionAmount2, transactionCurrencyCode2, conversionName,
                    userIdentifierList2);

                // Create offline data upload object.
                OfflineDataUpload offlineDataUpload = new OfflineDataUpload
                {
                    externalUploadId = externalUploadId,
                    offlineDataList = new OfflineData[]
                    {
                        offlineData1,
                        offlineData2
                    },

                    // Set the type and metadata of this upload.
                    uploadType = uploadType
                };
                StoreSalesUploadCommonMetadata storeSalesMetaData = null;

                switch (uploadType)
                {
                    case OfflineDataUploadType.STORE_SALES_UPLOAD_FIRST_PARTY:
                        storeSalesMetaData = new FirstPartyUploadMetadata()
                        {
                            loyaltyRate = 1,
                            transactionUploadRate = 1
                        };
                        break;

                    case OfflineDataUploadType.STORE_SALES_UPLOAD_THIRD_PARTY:
                        storeSalesMetaData = new ThirdPartyUploadMetadata()
                        {
                            loyaltyRate = 1.0,
                            transactionUploadRate = 1.0,
                            advertiserUploadTime = advertiserUploadTime,
                            validTransactionRate = 1.0,
                            partnerMatchRate = 1.0,
                            partnerUploadRate = 1.0,
                            bridgeMapVersionId = bridgeMapVersionId,
                            partnerId = partnerId
                        };
                        break;
                }

                UploadMetadata uploadMetadata = new UploadMetadata
                {
                    Item = storeSalesMetaData
                };
                offlineDataUpload.uploadMetadata = uploadMetadata;

                // Create an offline data upload operation.
                OfflineDataUploadOperation offlineDataUploadOperation =
                    new OfflineDataUploadOperation
                    {
                        @operator = Operator.ADD,
                        operand = offlineDataUpload
                    };

                // Keep the operations in an array, so it may be reused later for error processing.
                List<OfflineDataUploadOperation>
                    operations = new List<OfflineDataUploadOperation>();
                operations.Add(offlineDataUploadOperation);

                try
                {
                    // Upload offline data to the server.
                    OfflineDataUploadReturnValue result =
                        offlineDataUploadService.mutate(operations.ToArray());
                    offlineDataUpload = result.value[0];

                    // Print the upload ID and status.
                    Console.WriteLine(
                        "Uploaded offline data with external upload ID {0}, " +
                        "and upload status {1}.", offlineDataUpload.externalUploadId,
                        offlineDataUpload.uploadStatus);

                    // Print any partial failure errors from the response.
                    if (result.partialFailureErrors != null)
                    {
                        foreach (ApiError apiError in result.partialFailureErrors)
                        {
                            // Get the index of the failed operation from the error's field path
                            // elements.
                            int operationIndex = apiError.GetOperationIndex();
                            if (operationIndex != -1)
                            {
                                OfflineDataUpload failedOfflineDataUpload =
                                    operations[operationIndex].operand;
                                // Get the index of the entry in the offline data list from the
                                // error's field path elements.
                                int offlineDataListIndex =
                                    apiError.GetFieldPathIndex("offlineDataList");
                                Console.WriteLine(
                                    "Offline data list entry {0} in operation {1} with external " +
                                    "upload ID {2} and type '{3}' has triggered a failure for " +
                                    "the following reason: '{4}'.",
                                    offlineDataListIndex, operationIndex,
                                    failedOfflineDataUpload.externalUploadId,
                                    failedOfflineDataUpload.uploadType, apiError.errorString);
                            }
                            else
                            {
                                Console.WriteLine(
                                    "A failure has occurred for the following reason: {0}",
                                    apiError.errorString);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed upload offline data conversions.",
                        e);
                }
            }
        }

        /// <summary>
        /// Creates the offline data row from the specified transaction time, transaction micro
        /// amount, transaction currency, conversion name and user identifier list.
        /// </summary>
        /// <param name="transactionTime">The transaction time.</param>
        /// <param name="transactionMicroAmount">The transaction micro amount.</param>
        /// <param name="transactionCurrency">The transaction currency.</param>
        /// <param name="conversionName">Name of the conversion.</param>
        /// <param name="userIdentifierList">The user identifier list.</param>
        /// <returns>The offline data row.</returns>
        private OfflineData CreateOfflineDataRow(DateTime transactionTime,
            long transactionMicroAmount, string transactionCurrency, string conversionName,
            UserIdentifier[] userIdentifierList)
        {
            StoreSalesTransaction storeSalesTransaction = new StoreSalesTransaction
            {
                // For times use the format yyyyMMdd HHmmss [tz].
                // For details, see
                // https://developers.google.com/adwords/api/docs/appendix/codes-formats#date-and-time-formats
                transactionTime = transactionTime.ToString("yyyyMMdd HHmmss"),
                conversionName = conversionName,
                userIdentifiers = userIdentifierList
            };

            Money money = new Money
            {
                microAmount = transactionMicroAmount
            };
            RemarketingMoneyWithCurrency moneyWithCurrency = new RemarketingMoneyWithCurrency
            {
                money = money,
                currencyCode = transactionCurrency
            };
            storeSalesTransaction.transactionAmount = moneyWithCurrency;

            OfflineData offlineData = new OfflineData
            {
                Item = storeSalesTransaction
            };

            return offlineData;
        }

        /// <summary>
        /// Hash a string value using SHA-256 hashing algorithm.
        /// </summary>
        /// <param name="digest">Provides the algorithm for SHA-256.</param>
        /// <param name="value">The string value (e.g. an email address) to hash.</param>
        /// <returns>The hashed value.</returns>
        private static string ToSha256String(SHA256 digest, string value)
        {
            byte[] digestBytes = digest.ComputeHash(Encoding.UTF8.GetBytes(value));
            // Convert the byte array into an unhyphenated hexadecimal string.
            return BitConverter.ToString(digestBytes).Replace("-", string.Empty);
        }

        /// <summary>
        /// Creates the user identifier.
        /// </summary>
        /// <param name="type">The user identifier type.</param>
        /// <param name="value">The user identifier value.</param>
        /// <returns></returns>
        private UserIdentifier CreateUserIdentifier(OfflineDataUploadUserIdentifierType type,
            string value)
        {
            // If the user identifier type is a hashed type, also call hash function
            // on the value.
            if (type.ToString().StartsWith("HASHED_"))
            {
                value = ToSha256String(digest, ToNormalizedValue(value));
            }

            UserIdentifier userIdentifier = new UserIdentifier
            {
                userIdentifierType = type,
                value = value
            };

            return userIdentifier;
        }

        /// <summary>
        /// Removes leading and trailing whitespace and converts all characters to
        /// lower case.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <returns>The normalized value.</returns>
        private static string ToNormalizedValue(string value)
        {
            return value.Trim().ToLower();
        }
    }
}
