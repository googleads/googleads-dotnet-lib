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
Imports Google.Api.Ads.AdWords.v201101

Imports System
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example adds a cross-client (MCC) report definition. To get
  ''' report fields, run GetReportFields.vb. To work correctly this code
  ''' example must be run as an MCC account.
  '''
  ''' Tags: ReportDefinitionService.mutate
  ''' </summary>
  Class AddCrossClientReportDefinition
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds a cross-client (MCC) report definition. To get report " & _
            "fields, run GetReportFields.vb. To work correctly this code example must be run " & _
            "as an MCC account."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddCrossClientReportDefinition
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the ReportDefinitionService.
      Dim reportDefinitionService As ReportDefinitionService = DirectCast(user.GetService( _
          AdWordsService.v201101.ReportDefinitionService), ReportDefinitionService)

      Dim clientEmails As String() = New String() {}

      ' Since we are creating MCC reports, we need to clear clientEmail and
      ' clientCustomerId headers.
      reportDefinitionService.RequestHeader.clientEmail = Nothing
      reportDefinitionService.RequestHeader.clientCustomerId = Nothing

      Dim selector As New Selector
      selector.fields = New String() {"ExternalCustomerId", "AccountDescriptiveName", _
          "PrimaryUserLogin", "Date", "Id", "Name", "Impressions", "Clicks", "Cost"}

      Dim reportDefinition As New ReportDefinition
      reportDefinition.reportName = ("Cross-client campaign performance report #" & GetTimeStamp())
      reportDefinition.dateRangeType = ReportDefinitionDateRangeType.LAST_7_DAYS
      reportDefinition.reportType = ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT
      reportDefinition.downloadFormat = DownloadFormat.XML
      reportDefinition.selector = selector
      reportDefinition.crossClient = True

      Dim clientSelectors As New List(Of ClientSelector)

      For Each clientEmail As String In clientEmails
        Dim clientSelector As New ClientSelector
        clientSelector.login = clientEmail
        clientSelectors.Add(clientSelector)
      Next

      reportDefinition.clientSelectors = clientSelectors.ToArray
      Dim operation As New ReportDefinitionOperation
      operation.operand = reportDefinition
      operation.operator = [Operator].ADD

      Try
        Dim result As ReportDefinition() = reportDefinitionService.mutate( _
            New ReportDefinitionOperation() {operation})
        If (Not result Is Nothing) Then
          For Each tempReportDefinition As ReportDefinition In result
            Console.WriteLine("Report definition with name '{0}' and id '{1}' was added.", _
              tempReportDefinition.reportName, tempReportDefinition.id)
          Next
        Else
          Console.WriteLine("No report definitions were added.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to add report definition. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
