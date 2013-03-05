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
Imports Google.Api.Ads.AdWords.v201209

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201209
  ''' <summary>
  ''' This code example gets and downloads a criteria Ad Hoc report from an AWQL
  ''' query. See https://developers.google.com/adwords/api/docs/guides/awql for
  ''' AWQL documentation.
  ''' </summary>
  Public Class DownloadCriteriaReportWithAwql
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New DownloadCriteriaReportWithAwql
      Console.WriteLine(codeExample.Description)
      Try
        Dim fileName As String = "INSERT_OUTPUT_FILE_NAME"
        codeExample.Run(New AdWordsUser, fileName)
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
        Return "This code example gets and downloads a criteria Ad Hoc report from an AWQL" & _
            " query. See https://developers.google.com/adwords/api/docs/guides/awql for" & _
            " AWQL documentation."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="fileName">The file to which the report is downloaded.
    ''' </param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal fileName As String)
      Dim query As String = "SELECT CampaignId, AdGroupId, Id, Criteria, CriteriaType, " & _
          "Impressions, Clicks, Cost FROM CRITERIA_PERFORMANCE_REPORT WHERE Status IN " & _
          "[ACTIVE, PAUSED] DURING LAST_7_DAYS"

      Dim filePath As String = ExampleUtilities.GetHomeDir() + Path.DirectorySeparatorChar & _
          fileName

      Try
        ' If you know that your report is small enough to fit in memory, then
        ' you can instead use
        ' Dim utilities As New ReportUtilities(user)
        ' utilities.ReportVersion = "v201209"
        ' Dim report As ClientReport = utilities.GetClientReport(query, format)
        '
        ' ' Get the text report directly if you requested a text format
        ' ' (e.g. xml)
        ' Dim reportText As String = report.Text
        '
        ' ' Get the binary report if you requested a binary format
        ' ' (e.g. gzip)
        ' Dim reportBytes As Byte() = report.Contents
        '
        ' ' Deflate a zipped binary report for further processing.
        ' Dim deflatedReportText As String = Encoding.UTF8.GetString( _
        '     MediaUtilities.DeflateGZipData(report.Contents))
        Dim utilities As New ReportUtilities(user)
        utilities.ReportVersion = "v201209"
        utilities.DownloadClientReport(query, DownloadFormat.GZIPPED_CSV.ToString(), filePath)
        Console.WriteLine("Report was downloaded to '{0}'.", filePath)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to download report.", ex)
      End Try

    End Sub
  End Class
End Namespace
