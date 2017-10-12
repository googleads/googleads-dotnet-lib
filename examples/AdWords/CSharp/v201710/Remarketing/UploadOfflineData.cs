// Copyright 2017, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201710;

using Org.BouncyCastle.Crypto.Digests;

using System;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201710 {

  /// <summary>
  /// This code example shows how to upload offline data for store sales transactions.
  /// </summary>
  public class UploadOfflineData : ExampleBase {

    private static readonly GeneralDigest digest = new Sha256Digest();

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      // The external upload ID can be any number that you use to keep track of your uploads.
      long externalUploadId = long.Parse("INSERT_EXTERNAL_UPLOAD_ID");

      // Insert the conversion type name that you'd like to attribute this upload to.
      string conversionName = "INSERT_CONVERSION_NAME";

      // Insert email addresses below for creating user identifiers.
      string[] emailAddresses = { "EMAIL_ADDRESS_1", "EMAIL_ADDRESS_2" };

      UploadOfflineData codeExample = new UploadOfflineData();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), conversionName, externalUploadId, emailAddresses);
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
        return "This code example shows how to upload offline data for store sales transactions.";
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
    public void Run(AdWordsUser user, string conversionName, long externalUploadId,
        string[] emailAddresses) {
      // Get the OfflineDataUploadService.
      OfflineDataUploadService offlineDataUploadService =
          (OfflineDataUploadService) user.GetService(
              AdWordsService.v201710.OfflineDataUploadService);

      // Create the first offline data row for upload.
      // This transaction occurred 7 days ago with amount of 200 USD.
      DateTime transactionTime1 = new DateTime();
      transactionTime1.AddDays(-7);
      long transactionAmount1 = 200000000;
      string transactionCurrencyCode1 = "USD";
      UserIdentifier[] userIdentifierList1 = new UserIdentifier[] {
        CreateUserIdentifier(OfflineDataUploadUserIdentifierType.HASHED_EMAIL,
            emailAddresses[0]),
        CreateUserIdentifier(OfflineDataUploadUserIdentifierType.STATE, "New York")
      };
      OfflineData offlineData1 = CreateOfflineDataRow(transactionTime1, transactionAmount1,
          transactionCurrencyCode1, conversionName, userIdentifierList1);

      // Create the second offline data row for upload.
      // This transaction occurred 14 days ago with amount of 450 EUR.
      DateTime transactionTime2 = new DateTime();
      transactionTime2.AddDays(-14);
      long transactionAmount2 = 450000000;
      string transactionCurrencyCode2 = "EUR";
      UserIdentifier[] userIdentifierList2 = new UserIdentifier[] {
        CreateUserIdentifier(OfflineDataUploadUserIdentifierType.HASHED_EMAIL,
            emailAddresses[1]),
        CreateUserIdentifier(OfflineDataUploadUserIdentifierType.STATE, "California")
      };
      OfflineData offlineData2 = CreateOfflineDataRow(transactionTime2, transactionAmount2,
        transactionCurrencyCode2, conversionName, userIdentifierList2);

      // Create offline data upload object.
      OfflineDataUpload offlineDataUpload = new OfflineDataUpload();
      offlineDataUpload.externalUploadId = externalUploadId;
      offlineDataUpload.offlineDataList = new OfflineData[] { offlineData1, offlineData2 };

      // Optional: You can set the type of this upload.
      // offlineDataUpload.uploadType = OfflineDataUploadType.STORE_SALES_UPLOAD_FIRST_PARTY;

      // Create an offline data upload operation.
      OfflineDataUploadOperation offlineDataUploadOperation = new OfflineDataUploadOperation();
      offlineDataUploadOperation.@operator = Operator.ADD;
      offlineDataUploadOperation.operand = offlineDataUpload;

      try {
        // Upload offline data to the server.
        OfflineDataUploadReturnValue result = offlineDataUploadService.mutate(
            new OfflineDataUploadOperation[] { offlineDataUploadOperation });
        offlineDataUpload = result.value[0];

        // Print the upload ID and status.
        Console.WriteLine("Uploaded offline data with external upload ID {0}, " +
            "and upload status {1}.", offlineDataUpload.externalUploadId,
            offlineDataUpload.uploadStatus);

        // Print any partial data errors from the response. The order of the partial
        // data errors list is the same as the uploaded offline data list in the
        // request.
        if (offlineDataUpload.partialDataErrors != null) {
          for (int i = 0; i < offlineDataUpload.partialDataErrors.Length; i++) {
            ApiError partialDataError = offlineDataUpload.partialDataErrors[i];
            Console.WriteLine("Found a partial error for offline data {0} with error string: {1}.",
                i + 1, partialDataError.errorString);
          }
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed upload offline data conversions.", e);
      }
    }

    /// <summary>
    /// Creates the offline data row from the specified transaction time, transaction micro amount,
    /// transaction currency, conversion name and user identifier list.
    /// </summary>
    /// <param name="transactionTime">The transaction time.</param>
    /// <param name="transactionMicroAmount">The transaction micro amount.</param>
    /// <param name="transactionCurrency">The transaction currency.</param>
    /// <param name="conversionName">Name of the conversion.</param>
    /// <param name="userIdentifierList">The user identifier list.</param>
    /// <returns>The offline data row.</returns>
    OfflineData CreateOfflineDataRow(DateTime transactionTime, long transactionMicroAmount,
        string transactionCurrency, string conversionName, UserIdentifier[] userIdentifierList) {
      StoreSalesTransaction storeSalesTransaction = new StoreSalesTransaction();

      // For times use the format yyyyMMdd HHmmss [tz].
      // For details, see
      // https://developers.google.com/adwords/api/docs/appendix/codes-formats#date-and-time-formats
      storeSalesTransaction.transactionTime = transactionTime.ToString("Ymd His");
      storeSalesTransaction.conversionName = conversionName;
      storeSalesTransaction.userIdentifiers = userIdentifierList;

      Money money = new Money();
      money.microAmount = transactionMicroAmount;
      MoneyWithCurrency moneyWithCurrency = new MoneyWithCurrency();
      moneyWithCurrency.money = money;
      moneyWithCurrency.currencyCode = transactionCurrency;
      storeSalesTransaction.transactionAmount = moneyWithCurrency;

      OfflineData offlineData = new OfflineData();
      offlineData.Item = storeSalesTransaction;

      return offlineData;
    }

    /// <summary>
    /// Hash a string value using SHA-256 hashing algorithm.
    /// </summary>
    /// <param name="digest">Provides the algorithm for SHA-256.</param>
    /// <param name="value">The string value (e.g. an email address) to hash.</param>
    /// <returns>The hashed value.</returns>
    private static String ToSha256String(GeneralDigest digest, String value) {
      byte[] data = Encoding.UTF8.GetBytes(value);
      byte[] digestBytes = new byte[digest.GetDigestSize()];
      digest.BlockUpdate(data, 0, data.Length);
      digest.DoFinal(digestBytes, 0);

      // Convert the byte array into an unhyphenated hexadecimal string.
      return BitConverter.ToString(digestBytes).Replace("-", string.Empty);
    }

    /// <summary>
    /// Creates the user identifier.
    /// </summary>
    /// <param name="type">The user identifier type.</param>
    /// <param name="value">The user identifier value.</param>
    /// <returns></returns>
    UserIdentifier CreateUserIdentifier(OfflineDataUploadUserIdentifierType type, string value) {
      // If the user identifier type is a hashed type, also call hash function
      // on the value.
      if (type.ToString().StartsWith("HASHED_")) {
        value = ToSha256String(digest, ToNormalizedValue(value));
      }
      UserIdentifier userIdentifier = new UserIdentifier();
      userIdentifier.userIdentifierType = type;
      userIdentifier.value = value;

      return userIdentifier;
    }

    /// <summary>
    /// Removes leading and trailing whitespace and converts all characters to
    /// lower case.
    /// </summary>
    /// <param name="value">The value to normalize.</param>
    /// <returns>The normalized value.</returns>
    private static String ToNormalizedValue(String value) {
      return value.Trim().ToLower();
    }
  }
}
