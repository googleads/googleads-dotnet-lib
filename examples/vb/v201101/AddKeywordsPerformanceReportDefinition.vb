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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example adds a keywords performance report. To get ad groups,
  ''' run GetAllAdGroups.vb. To get report fields, run GetReportFields.vb.
  '''
  ''' Tags: ReportDefinitionService.mutate
  ''' </summary>
  Class AddKeywordsPerformanceReportDefinition
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds a keywords performance report. To get ad groups, " & _
            "run GetAllAdGroups.vb. To get report fields, run GetReportFields.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddKeywordsPerformanceReportDefinition
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the GetReportDefinitionService.
      Dim reportDefinitionService As ReportDefinitionService = user.GetService( _
          AdWordsService.v201101.ReportDefinitionService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_AD_GROUP_ID_HERE"))
      Dim startDate As String = _T("INSERT_START_DATE_HERE")
      Dim endDate As String = _T("INSERT_END_DATE_HERE")

      ' Create ad group predicate.
      Dim adGroupPredicate As New Predicate
      adGroupPredicate.field = "AdGroupId"
      adGroupPredicate.operator = PredicateOperator.EQUALS
      adGroupPredicate.values = New String() {adGroupId.ToString}

      ' Create selector.
      Dim selector As New Selector
      selector.fields = New String() {"AdGroupId", "Id", "KeywordText", "KeywordMatchType", _
          "Impressions", "Clicks", "Cost"}
      selector.predicates = New Predicate() {adGroupPredicate}
      selector.dateRange = New DateRange
      selector.dateRange.min = startDate
      selector.dateRange.max = endDate

      ' Create report definition.
      Dim reportDefinition As New ReportDefinition
      reportDefinition.reportName = ("Keywords performance report #" & GetTimeStamp)
      reportDefinition.dateRangeType = ReportDefinitionDateRangeType.CUSTOM_DATE
      reportDefinition.reportType = ReportDefinitionReportType.KEYWORDS_PERFORMANCE_REPORT
      reportDefinition.downloadFormat = DownloadFormat.XML
      reportDefinition.selector = selector

      ' Create operations.
      Dim operation As New ReportDefinitionOperation
      operation.operand = reportDefinition
      operation.operator = [Operator].ADD

      Try
        ' Add report definition.
        Dim result As ReportDefinition() = reportDefinitionService.mutate( _
            New ReportDefinitionOperation() {operation})

        ' Display report definitions.
        If (Not result Is Nothing) Then
          For Each temp As ReportDefinition In result
            Console.WriteLine("Report definition with name '{0}' and id '{1}' was added.", _
                temp.reportName, temp.id)
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
