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
  ''' This code example retrieves the unit usage for a client account for this
  ''' month.
  '''
  ''' Tags: InfoService.get
  ''' </summary>
  Public Class GetClientUnitUsage
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetClientUnitUsage
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
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
        Return "This code example retrieves the unit usage for a client account for this month."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the InfoService.
      Dim infoService As InfoService = user.GetService(AdWordsService.v201209.InfoService)

      ' Ensure the clientCustomerId is not set, so that requests are made to
      ' the MCC.
      infoService.RequestHeader.clientCustomerId = Nothing

      ' Create the selector.
      Dim selector As New InfoSelector
      selector.clientCustomerIds = New Long() {GetCustomerId(user)}
      selector.includeSubAccounts = True
      selector.apiUsageType = ApiUsageType.UNIT_COUNT_FOR_CLIENTS

      ' Create date range for retrieving unit usage.
      Dim dateRange As New DateRange
      dateRange.min = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyyMMdd")
      dateRange.max = DateTime.Now.AddDays(-1).ToString("yyyyMMdd")
      selector.dateRange = dateRange

      Try
        ' Get the client's unit usage.
        Dim info As ApiUsageInfo = infoService.get(selector)

        ' Display the results.
        If ((Not info Is Nothing) AndAlso (Not info.apiUsageRecords Is Nothing)) Then
          For Each record As ApiUsageRecord In info.apiUsageRecords
            Console.WriteLine("API Usage for customer ID '{0:###-###-####}' is {1} units.", _
                record.clientCustomerId, record.cost)
          Next
        Else
          Console.WriteLine("No API usage records were found for client.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to get unit usage for client.", ex)
      End Try
    End Sub

    ''' <summary>
    ''' Gets the customer ID.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <returns>The customer ID.</returns>
    Private Function GetCustomerId(ByVal user As AdWordsUser) As Long
      Return Long.Parse(TryCast(user.Config, AdWordsAppConfig).ClientCustomerId.Replace("-", ""))
    End Function
  End Class
End Namespace
