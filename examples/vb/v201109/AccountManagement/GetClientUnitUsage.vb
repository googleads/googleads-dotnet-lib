' Copyright 2011, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201109

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example retrieves the unit usage for a client account for this
  ''' month.
  '''
  ''' Tags: InfoService.get
  ''' </summary>
  Class GetClientUnitUsage
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetClientUnitUsage
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
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
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {}
    End Function

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="parameters">The parameters for running the code
    ''' example.</param>
    ''' <param name="writer">The stream writer to which script output should be
    ''' written.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser, ByVal parameters As  _
        Dictionary(Of String, String), ByVal writer As TextWriter)
      ' Get the InfoService.
      Dim infoService As InfoService = user.GetService(AdWordsService.v201109.InfoService)

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
      dateRange.max = DateTime.Now.ToString("yyyyMMdd")
      selector.dateRange = dateRange

      Try
        ' Get the client's unit usage.
        Dim info As ApiUsageInfo = infoService.get(selector)

        ' Display the results.
        If ((Not info Is Nothing) AndAlso (Not info.apiUsageRecords Is Nothing)) Then
          For Each record As ApiUsageRecord In info.apiUsageRecords
            writer.WriteLine("API Usage for customer ID '{0:###-###-####}' is {1} units.", _
                record.clientCustomerId, record.cost)
          Next
        Else
          writer.WriteLine("No API usage records were found for client.")
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to get unit usage for client. Exception says ""{0}""", ex.Message)
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
