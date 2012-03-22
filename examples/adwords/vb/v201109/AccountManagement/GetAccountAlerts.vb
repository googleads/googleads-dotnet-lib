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
  ''' This code example gets all alerts for all clients of an MCC account.
  ''' The effective user (ClientCustomerId or AuthToken) must be an MCC user
  ''' to get results.
  '''
  ''' Tags: AlertService.get
  ''' </summary>
  Public Class GetAccountAlerts
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetAccountAlerts
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, codeExample.GetParameters, Console.Out)
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
        Return "This code example gets all alerts for all clients of an MCC account. The " & _
            "effective user (ClientCustomerId or AuthToken) must be an MCC user to get results."
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
      ' Get the AlertService.
      Dim alertService As AlertService = user.GetService(AdWordsService.v201109.AlertService)

      ' Create the selector.
      Dim selector As New AlertSelector

      ' Create the alert query.
      Dim query As New AlertQuery
      query.filterSpec = FilterSpec.ALL
      query.clientSpec = ClientSpec.ALL
      query.triggerTimeSpec = TriggerTimeSpec.ALL_TIME
      query.severities = New AlertSeverity() {AlertSeverity.GREEN, AlertSeverity.YELLOW, _
          AlertSeverity.RED}

      ' Enter all possible values of AlertType to get all alerts. If you are
      ' interested only in specific alert types, then you may also do it as
      ' follows:
      ' query.types = new AlertType() {AlertType.CAMPAIGN_ENDING,
      '     AlertType.CAMPAIGN_ENDED}
      query.types = [Enum].GetValues(GetType(AlertType))
      selector.query = query

      ' Set paging for selector.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New AlertPage

      Try
        Do
          ' Get account alerts.
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          page = alertService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset
            For Each alert As Alert In page.entries
              writer.WriteLine("{0}) Customer Id is {1:###-###-####}, Alert type is '{2}', " & _
                  "Severity is {3}", (i + 1), alert.clientCustomerId, alert.alertType, _
                  alert.alertSeverity)
              For j As Integer = 0 To alert.details.Length - 1
                writer.WriteLine("  - Triggered at {0}", alert.details(j).triggerTime)
              Next j
              i += 1
            Next
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        writer.WriteLine("Number of alerts found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve alerts.", ex)
      End Try
    End Sub
  End Class
End Namespace
