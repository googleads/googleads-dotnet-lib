' Copyright 2016, Google Inc. All Rights Reserved.
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

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.Util.Reports
Imports Google.Api.Ads.AdWords.v201603
Imports Google.Api.Ads.Common.Util.Reports

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
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
      Catch e As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(e))
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
        Dim utilities As New ReportUtilities(user, "v201603", query, _
                                             DownloadFormat.GZIPPED_CSV.ToString())
        Using reportResponse As ReportResponse = utilities.GetResponse()
          reportResponse.Save(filePath)
        End Using
        Console.WriteLine("Report was downloaded to '{0}'.", filePath)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to download report.", e)
      End Try

    End Sub
  End Class
End Namespace
