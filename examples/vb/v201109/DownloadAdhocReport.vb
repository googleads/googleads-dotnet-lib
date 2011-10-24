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
Imports Google.Api.Ads.AdWords.Util.Reports
Imports Google.Api.Ads.AdWords.v201109

Imports System
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example gets and downloads an Ad Hoc report from a XML report
  ''' definition.
  ''' </summary>
  Class DownloadAdhocReport
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets and downloads an Ad Hoc report from a XML report" & _
          " definition."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New DownloadAdhocReport
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      Dim fileName As String = _T("INSERT_OUTPUT_FILE_NAME_HERE")

      Dim definition As New ReportDefinition

      definition.reportName = "Last 7 days ADGROUP_PERFORMANCE_REPORT"
      definition.reportType = ReportDefinitionReportType.ADGROUP_PERFORMANCE_REPORT
      definition.downloadFormat = DownloadFormat.CSV
      definition.dateRangeType = ReportDefinitionDateRangeType.LAST_7_DAYS

      Dim selector As New Selector
      selector.fields = New String() {"CampaignId", "Id", "Impressions", "Clicks", "Cost"}

      Dim predicate As New Predicate
      predicate.field = "Status"
      predicate.operator = PredicateOperator.IN
      predicate.values = New String() {"ENABLED", "PAUSED"}
      selector.predicates = New Predicate() {predicate}

      definition.includeZeroImpressions = True

      Dim filePath As String = GetHomeDir() & Path.DirectorySeparatorChar & fileName

      Try
        Dim utilities As New ReportUtilities(user)
        ' If you know that your report is small enough to fit in memory, then
        ' you can instead use
        ' ClientReport report = new ReportUtilities().DownloadClientReport(
        '     new AdWordsAppConfig(), definition);
        '
        ' ' Binary report file (e.g. zip format)
        ' byte[] reportBytes = report.Contents;
        '
        ' ' Text report file (e.g. xml format)
        ' string reportText = report.Text;
        utilities.DownloadClientReport(Of ReportDefinition)(definition, filePath)
        Console.WriteLine("Report was downloaded to '{0}'.", filePath)
      Catch ex As Exception
        Console.WriteLine("Failed to download report. Exception says ""{0}""", ex.Message)
      End Try

    End Sub
  End Class
End Namespace
