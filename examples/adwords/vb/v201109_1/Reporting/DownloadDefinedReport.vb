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
Imports Google.Api.Ads.AdWords.Util.Reports

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109_1
  ''' <summary>
  ''' This code example gets and downloads a report from an existing report definition.
  ''' </summary>
  Public Class DownloadDefinedReport
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New DownloadDefinedReport
      Console.WriteLine(codeExample.Description)
      Try
        Dim reportDefinitionId As Long = Long.Parse("INSERT_REPORT_DEFINITION_ID_HERE")
        Dim fileName As String = "INSERT_OUTPUT_FILE_NAME"
        codeExample.Run(New AdWordsUser, reportDefinitionId, fileName)
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
        Return "This code example gets and downloads a report from an existing report definition."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="reportDefinitionId">Id of the report to be downloaded.
    ''' </param>
    ''' <param name="fileName">File to which report is downloaded.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal reportDefinitionId As Long, _
        ByVal fileName As String)
      Dim filePath As String = (ExampleUtilities.GetHomeDir() & Path.DirectorySeparatorChar & _
          fileName)

      Try
        ' Download the report.
        Dim utilities As New ReportUtilities(user)
        ' If you know that your report is small enough to fit in memory, then
        ' you can instead use
        ' ClientReport report = new ReportUtilities(user).GetClientReport(reportDefinitionId);
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
        utilities.DownloadClientReport(Of Long)(reportDefinitionId, filePath)
        Console.WriteLine("Report with definition id '{0}' was downloaded to '{1}'.", _
            reportDefinitionId, filePath)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to download report.", ex)
      End Try
    End Sub
  End Class
End Namespace
