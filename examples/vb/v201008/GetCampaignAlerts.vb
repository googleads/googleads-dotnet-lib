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
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example illustrates how to retrieve campaign alerts for a user.
  ''' The alerts are restricted to a maximum of 10 entries.
  '''
  ''' Tags: AlertService.get
  ''' </summary>
  Class GetCampaignAlerts
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to retrieve campaign alerts for a user. The " & _
            "alerts are restricted to a maximum of 10 entries."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetCampaignAlerts
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AlertService.
      Dim alertService As AlertService = user.GetService(AdWordsService.v201008.AlertService)

      ' Create the alert query.
      Dim query As New AlertQuery
      query.filterSpec = FilterSpec.ALL
      query.clientSpec = ClientSpec.ALL
      query.triggerTimeSpec = TriggerTimeSpec.ALL_TIME
      query.severities = New AlertSeverity() {AlertSeverity.GREEN, AlertSeverity.YELLOW, _
          AlertSeverity.RED}
      query.types = New AlertType() {AlertType.CAMPAIGN_ENDING, AlertType.CAMPAIGN_ENDED}

      ' Create the selector.
      Dim selector As New AlertSelector
      selector.query = query
      selector.paging = New Paging
      selector.paging.startIndex = 0
      selector.paging.numberResults = 10

      Try
        Dim page As AlertPage = alertService.get(selector)
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing) AndAlso _
            (page.entries.Length > 0)) Then
          Console.WriteLine("Retrieved {0} alerts out of {1}.", page.entries.Length, _
              page.totalNumEntries)

          For i As Integer = 0 To page.entries.Length - 1
            Dim alert As Alert = page.entries(i)
            Console.WriteLine("{0}) Customer Id is {1:###-###-####}, Alert type is '{2}', " & _
                "Severity is {3}", (i + 1), alert.clientCustomerId, alert.alertType, _
                alert.alertSeverity)
            For j As Integer = 0 To alert.details.Length - 1
              Console.WriteLine("  - Triggered at {0}", alert.details(j).triggerTime)
            Next j
          Next i
        Else
          Console.WriteLine("No alerts were found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve alerts. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
