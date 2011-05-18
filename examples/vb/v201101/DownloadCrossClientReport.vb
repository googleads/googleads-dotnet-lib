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
Imports Google.Api.Ads.AdWords.v201101
Imports Google.Api.Ads.Common.Lib

Imports System
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example gets and downloads a cross-client report from a report
  ''' definition. To get a cross-client report definition, run
  ''' AddCrossClientReportDefinition.vb. Currently, there is only production
  ''' support for report download.
  ''' </summary>
  Class DownloadCrossClientReport
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets and downloads a cross-client report from a report" & _
            " definition. To get a cross-client report definition, run " & _
            "AddCrossClientReportDefinition.vb. Currently, there is only production support " & _
            "for report download."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New DownloadCrossClientReport
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      Dim reportDefinitionId As Long = Long.Parse(_T("INSERT_REPORT_DEFINITION_ID_HERE"))
      Dim fileName As String = _T("INSERT_OUTPUT_FILE_NAME_HERE")
      Dim path As String = GetHomeDir() & "\\" & fileName

      Try
        Dim reportUtilities As New ReportUtilities(user)
        ' If you know that your report is small enough to fit in memory, then
        ' you can instead use
        ' Dim report as MccReport = reportUtilities.GetMccReport( _
        '     new AdWordsAppConfig(), reportDefinitionId)
        '
        ' ' Binary report file (e.g. zip format)
        ' byte[] reportBytes = report.Contents;
        '
        ' ' Text report file (e.g. xml format)
        ' string reportText = report.Text;
        Dim report As MccReport = reportUtilities.DownloadMccReport(reportDefinitionId, path)
        Console.WriteLine("Report with definition id '{0}' was downloaded to '{1}'.", _
            reportDefinitionId, path)
      Catch ex As Exception
        Console.WriteLine("Failed to download report. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
