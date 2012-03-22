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
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example gets and downloads a criteria Ad Hoc report from an XML
  ''' report definition.
  ''' </summary>
  Public Class DownloadCriteriaReport
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New DownloadCriteriaReport
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
        Return "This code example gets and downloads a criteria Ad Hoc report from an XML report" & _
          " definition."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"OUTPUT_FILE_NAME"}
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
      Dim fileName As String = parameters("OUTPUT_FILE_NAME")

      Dim definition As New ReportDefinition

      definition.reportName = "Last 7 days CRITERIA_PERFORMANCE_REPORT"
      definition.reportType = ReportDefinitionReportType.CRITERIA_PERFORMANCE_REPORT
      definition.downloadFormat = DownloadFormat.GZIPPED_CSV
      definition.dateRangeType = ReportDefinitionDateRangeType.LAST_7_DAYS

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"CampaignId", "AdGroupId", "Id", "CriteriaType", "Criteria", _
          "CriteriaDestinationUrl", "Clicks", "Impressions", "Cost"}

      Dim predicate As New Predicate
      predicate.field = "Status"
      predicate.operator = PredicateOperator.IN
      predicate.values = New String() {"ACTIVE", "PAUSED"}
      selector.predicates = New Predicate() {predicate}

      definition.selector = selector
      definition.includeZeroImpressions = True

      Dim filePath As String = ExampleUtilities.GetHomeDir() & Path.DirectorySeparatorChar & _
          fileName

      Try
        Dim utilities As New ReportUtilities(user)
        ' If you know that your report is small enough to fit in memory, then
        ' you can instead use
        ' ClientReport report = new ReportUtilities(user).GetClientReport(definition);
        '
        ' ' Get the text report directly if you requested a text format
        ' ' (e.g. xml)
        ' string reportText = report.Text;
        '
        ' ' Get the binary report if you requested a binary format
        ' ' (e.g. gzip)
        ' byte[] reportBytes = report.Contents;
        '
        ' ' Deflate a zipped binary report for further processing.
        ' string deflatedReportText = Encoding.UTF8.GetString(
        '     MediaUtilities.DeflateGZipData(report.Contents));
        utilities.DownloadClientReport(Of ReportDefinition)(definition, filePath)
        writer.WriteLine("Report was downloaded to '{0}'.", filePath)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to download report.", ex)
      End Try

    End Sub
  End Class
End Namespace
