' Copyright 2017, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License")
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http:'www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

Imports System.Security.Cryptography
Imports System.Text
Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201710

Namespace Google.Api.Ads.AdWords.Examples.VB.v201710

  ''' <summary>
  ''' This code example shows how to upload offline data for store sales transactions.
  ''' </summary>
  Public Class UploadOfflineData
    Inherits ExampleBase

    Private digest As SHA256 = SHA256.Create()

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      ' The external upload ID can be any number that you use to keep track of your uploads.
      Dim externalUploadId As Long = Long.Parse("INSERT_EXTERNAL_UPLOAD_ID")

      ' Insert the conversion type name that you'd like to attribute this upload to.
      Dim conversionName As String = "INSERT_CONVERSION_NAME"

      ' Insert email addresses below for creating user identifiers.
      Dim emailAddresses As String() = {"EMAIL_ADDRESS_1", "EMAIL_ADDRESS_2"}

      ' Insert advertiser upload time. // For times, use the format yyyyMMdd HHmmss tz. For
      ' more details on formats, see:
      ' https//developers.google.com/adwords/api/docs/appendix/codes-formats#date-And-time-formats
      ' For time zones, see:
      ' https//developers.google.com/adwords/api/docs/appendix/codes-formats#timezone-ids
      Dim advertiserUploadTime As String = "INSERT_ADVERTISER_UPLOAD_TIME"

      ' Insert bridge map version ID.
      Dim bridgeMapVersionId As String = "INSERT_BRIDGEMAP_VERSION_ID"

      ' Specify the upload type (STORE_SALES_UPLOAD_FIRST_PARTY or STORE_SALES_UPLOAD_THIRD_PARTY)
      Dim uploadType As OfflineDataUploadType = DirectCast([Enum].Parse(
          GetType(OfflineDataUploadType), "INSERT_UPLOAD_TYPE"), OfflineDataUploadType)

      ' Insert partner ID.
      Dim partnerId As Integer = Integer.Parse("INSERT_PARTNER_ID")

      Dim codeExample As New UploadOfflineData
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, conversionName, externalUploadId, emailAddresses,
            advertiserUploadTime, bridgeMapVersionId, uploadType, partnerId)
      Catch e As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example shows how to upload offline data for store sales transactions."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="conversionName">The conversion type name that you'd like to attribute this
    ''' upload to.</param>
    ''' <param name="externalUploadId">The external upload ID can be any number that you use to
    ''' keep track of your uploads.</param>
    ''' <param name="emailAddresses">The email addresses for creating user identifiers.</param>
    ''' <param name="advertiserUploadTime">The advertiser upload time. For times, use the format
    ''' yyyyMMdd HHmmss tz. For more details on formats, see
    ''' https//developers.google.com/adwords/api/docs/appendix/codes-formats#date-And-time-formats
    ''' For time zones, see:
    ''' https//developers.google.com/adwords/api/docs/appendix/codes-formats#timezone-ids</param>
    ''' <param name="bridgeMapVersionId">The version ID of the bridge map.</param>
    ''' <param name="uploadType">The offline data upload type.</param>
    ''' <param name="partnerId">The partner ID</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal conversionName As String,
        ByVal externalUploadId As Long, ByVal emailAddresses As String(),
        ByVal advertiserUploadTime As String, ByVal bridgeMapVersionId As String,
        ByVal uploadType As OfflineDataUploadType, ByVal partnerId As Integer)
      Using offlineDataUploadService As OfflineDataUploadService = CType(user.GetService(
          AdWordsService.v201710.OfflineDataUploadService),
              OfflineDataUploadService)

        ' Create the first offline data row for upload.
        ' This transaction occurred 7 days ago with amount of 200 USD.
        Dim transactionTime1 As New DateTime()
        transactionTime1.AddDays(-7)
        Dim transactionAmount1 As Long = 200000000
        Dim transactionCurrencyCode1 As String = "USD"
        Dim userIdentifierList1 As UserIdentifier() = {
          CreateUserIdentifier(OfflineDataUploadUserIdentifierType.HASHED_EMAIL,
              emailAddresses(0)),
          CreateUserIdentifier(OfflineDataUploadUserIdentifierType.STATE, "New York")
        }

        Dim offlineData1 As OfflineData = CreateOfflineDataRow(transactionTime1,
            transactionAmount1, transactionCurrencyCode1, conversionName, userIdentifierList1)

        ' Create the second offline data row for upload.
        ' This transaction occurred 14 days ago with amount of 450 EUR.
        Dim transactionTime2 As New DateTime()
        transactionTime2.AddDays(-14)
        Dim transactionAmount2 As Long = 450000000
        Dim transactionCurrencyCode2 As String = "EUR"
        Dim userIdentifierList2 As UserIdentifier() = {
          CreateUserIdentifier(OfflineDataUploadUserIdentifierType.HASHED_EMAIL,
              emailAddresses(1)),
          CreateUserIdentifier(OfflineDataUploadUserIdentifierType.STATE, "California")
        }
        Dim offlineData2 As OfflineData = CreateOfflineDataRow(transactionTime2,
            transactionAmount2, transactionCurrencyCode2, conversionName, userIdentifierList2)

        ' Create offline data upload object.
        Dim offlineDataUpload As New OfflineDataUpload()
        offlineDataUpload.externalUploadId = externalUploadId
        offlineDataUpload.offlineDataList = New OfflineData() {offlineData1, offlineData2}

        ' Set the type And metadata of this upload.
        offlineDataUpload.uploadType = uploadType
        Dim uploadMetadata As New UploadMetadata()

        Select Case uploadType
          Case OfflineDataUploadType.STORE_SALES_UPLOAD_FIRST_PARTY
            Dim firstPartyMetaData As New FirstPartyUploadMetadata()
            firstPartyMetaData.loyaltyRate = 1
            firstPartyMetaData.transactionUploadRate = 1
            uploadMetadata.Item = firstPartyMetaData

          Case OfflineDataUploadType.STORE_SALES_UPLOAD_THIRD_PARTY
            Dim thirdPartyMetadata As New ThirdPartyUploadMetadata()
            thirdPartyMetadata.loyaltyRate = 1.0
            thirdPartyMetadata.transactionUploadRate = 1.0
            thirdPartyMetadata.advertiserUploadTime = advertiserUploadTime
            thirdPartyMetadata.validTransactionRate = 1.0
            thirdPartyMetadata.partnerMatchRate = 1.0
            thirdPartyMetadata.partnerUploadRate = 1.0
            thirdPartyMetadata.bridgeMapVersionId = bridgeMapVersionId
            thirdPartyMetadata.partnerId = partnerId
            uploadMetadata.Item = thirdPartyMetadata
        End Select

        offlineDataUpload.uploadMetadata = uploadMetadata

        ' Create an offline data upload operation.
        Dim offlineDataUploadOperation As New OfflineDataUploadOperation()
        offlineDataUploadOperation.operator = [Operator].ADD
        offlineDataUploadOperation.operand = offlineDataUpload

        Try
          ' Upload offline data to the server.
          Dim result As OfflineDataUploadReturnValue = offlineDataUploadService.mutate(
            New OfflineDataUploadOperation() {offlineDataUploadOperation})
          offlineDataUpload = result.value(0)

          ' Print the upload ID and status.
          Console.WriteLine("Uploaded offline data with external upload ID {0}, " +
              "and upload status {1}.", offlineDataUpload.externalUploadId,
              offlineDataUpload.uploadStatus)

          ' Print any partial data errors from the response. The order of the partial
          ' data errors list Is the same as the uploaded offline data list in the
          ' request.
          If Not offlineDataUpload.partialDataErrors Is Nothing Then
            For i As Integer = 0 To offlineDataUpload.partialDataErrors.Length
              Dim partialDataError As ApiError = offlineDataUpload.partialDataErrors(i)
              Console.WriteLine("Found a partial error for offline data {0} with error " +
                  "string: {1}.", i + 1, partialDataError.errorString)
            Next
          End If
        Catch e As Exception
          Throw New System.ApplicationException("Failed to upload offline conversions.", e)
        End Try
      End Using
    End Sub

    ''' <summary>
    ''' Creates the offline data row from the specified transaction time, transaction micro amount,
    ''' transaction currency, conversion name And user identifier list.
    ''' </summary>
    ''' <param name="transactionTime">The transaction time.</param>
    ''' <param name="transactionMicroAmount">The transaction micro amount.</param>
    ''' <param name="transactionCurrency">The transaction currency.</param>
    ''' <param name="conversionName">Name of the conversion.</param>
    ''' <param name="userIdentifierList">The user identifier list.</param>
    ''' <returns>The offline data.</returns>
    Function CreateOfflineDataRow(ByVal transactionTime As DateTime,
        ByVal transactionMicroAmount As Long, ByVal transactionCurrency As String,
        ByVal conversionName As String, ByVal userIdentifierList As UserIdentifier()) _
        As OfflineData
      Dim storeSalesTransaction As New StoreSalesTransaction()

      ' For times use the format yyyyMMdd HHmmss [tz].
      ' For details, see
      ' https://developers.google.com/adwords/api/docs/appendix/codes-formats#date-And-time-formats
      storeSalesTransaction.transactionTime = transactionTime.ToString("Ymd His")
      storeSalesTransaction.conversionName = conversionName
      storeSalesTransaction.userIdentifiers = userIdentifierList

      Dim money As New Money()
      money.microAmount = transactionMicroAmount
      Dim moneyWithCurrency As New RemarketingMoneyWithCurrency()
      moneyWithCurrency.money = money
      moneyWithCurrency.currencyCode = transactionCurrency
      storeSalesTransaction.transactionAmount = moneyWithCurrency

      Dim offlineData As New OfflineData()
      offlineData.Item = storeSalesTransaction

      Return offlineData
    End Function

    ''' <summary>
    ''' Hash a string value using SHA-256 hashing algorithm.
    ''' </summary>
    ''' <param name="digest">Provides the algorithm for SHA-256.</param>
    ''' <param name="value">The string value (e.g. an email address) to hash.</param>
    ''' <returns>The hashed value.</returns>
    Private Shared Function ToSha256String(ByVal digest As SHA256,
                                           ByVal value As String) As String
      Dim digestBytes As Byte() = digest.ComputeHash(Encoding.UTF8.GetBytes(value))
      ' Convert the byte array into an unhyphenated hexadecimal string.
      Return BitConverter.ToString(digestBytes).Replace("-", String.Empty)
    End Function

    ''' <summary>
    ''' Creates the user identifier.
    ''' </summary>
    ''' <param name="type">The user identifier type.</param>
    ''' <param name="value">The user identifier value.</param>
    ''' <returns></returns>
    Function CreateUserIdentifier(ByVal type As OfflineDataUploadUserIdentifierType,
        ByVal value As String) As UserIdentifier
      ' If the user identifier type Is a hashed type, also call hash function
      ' on the value.
      If type.ToString().StartsWith("HASHED_") Then
        value = ToSha256String(digest, ToNormalizedValue(value))
      End If
      Dim userIdentifier As New UserIdentifier()
      userIdentifier.userIdentifierType = type
      userIdentifier.value = value

      Return userIdentifier
    End Function

    ''' <summary>
    ''' Removes leading And trailing whitespace And converts all characters to
    ''' lower case.
    ''' </summary>
    ''' <param name="value">The value to normalize.</param>
    ''' <returns>The normalized value.</returns>
    Private Shared Function ToNormalizedValue(ByVal value As String) As String
      Return value.Trim().ToLower()
    End Function

  End Class

End Namespace
