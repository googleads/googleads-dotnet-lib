' Copyright 2012, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201209

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201209
  ''' <summary>
  ''' This code example illustrates how to find a client customer ID for a
  ''' client email. We recommend to use this script as a one off to convert your
  ''' identifiers to IDs and store them for future use. This code example won't
  ''' work with Test Accounts. See
  ''' https://developers.google.com/adwords/api/docs/test-accounts
  '''
  ''' Tags: InfoService.get
  ''' </summary>
  Public Class GetClientCustomerId
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetClientCustomerId
      Console.WriteLine(codeExample.Description)
      Try
        Dim clientEmail As String = Long.Parse("INSERT_CLIENT_EMAIL_HERE")
        codeExample.Run(New AdWordsUser, clientEmail)
      Catch ex As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(ex))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to find a client customer ID for a client " & _
            "email. We recommend to use this script as a one off to convert your identifiers " & _
            "to IDs and store them for future use. This code example won't work with test " & _
            "accounts. See https://developers.google.com/adwords/api/docs/test-accounts"
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="clientEmail">The client email for which customer id
    ''' is retrieved.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal clientEmail As String)
      ' Get the InfoService.
      Dim infoService As InfoService = user.GetService(AdWordsService.v201209.InfoService)

      ' Ensure the clientCustomerId is not set, so that requests are made to
      ' the MCC.
      infoService.RequestHeader.clientCustomerId = Nothing

      ' Create the selector.
      Dim selector As New InfoSelector
      selector.clientEmails = New String() {clientEmail}
      selector.includeSubAccounts = True
      selector.apiUsageType = ApiUsageType.UNIT_COUNT_FOR_CLIENTS

      ' The date used doesn't matter, so use today.
      Dim dateRange As New DateRange
      dateRange.max = DateTime.Now.AddDays(-1).ToString("yyyyMMdd")
      dateRange.min = DateTime.Now.AddDays(-1).ToString("yyyyMMdd")
      selector.dateRange = dateRange

      Try
        ' Get the information for the client email address.
        Dim info As ApiUsageInfo = infoService.get(selector)

        If ((Not info Is Nothing) AndAlso (Not info.apiUsageRecords Is Nothing)) Then
          For Each record As ApiUsageRecord In info.apiUsageRecords
            Console.WriteLine("Found record with client email '{0}' and customer ID " & _
                "'{1:###-###-####}'.", record.clientEmail, record.clientCustomerId)
          Next
        Else
          Console.WriteLine("No client customer ids were found.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to get client customer id.", ex)
      End Try
    End Sub
  End Class
End Namespace
