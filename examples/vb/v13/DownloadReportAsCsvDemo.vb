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
Imports Google.Api.Ads.AdWords.v13

Imports System
Imports System.Web.Services.Protocols

Namespace Google.Api.Ads.AdWords.Examples.VB.v13
  ''' <summary>
  ''' This code example shows how to use AdWordsUtilities to download a report
  ''' in CSV format.
  ''' </summary>
  Class DownloadReportAsCsvDemo
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example shows how to use AdWordsUtilities to download a report " & _
            "in CSV format."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New DownloadReportAsCsvDemo
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      Dim utilities As New ReportUtilities(user)

      ' Create the report job.
      Dim reportJob As New DefinedReportJob
      reportJob.name = "Keyword Report"
      reportJob.selectedReportType = "Keyword"
      reportJob.aggregationTypes = New String() {"Daily"}
      reportJob.adWordsType = AdWordsType.SearchOnly
      reportJob.endDay = DateTime.Today
      reportJob.startDay = New DateTime(2009, 1, 1)
      reportJob.selectedColumns = New String() {"Campaign", "AdGroup", "Keyword", _
          "KeywordStatus", "KeywordMinCPC", "KeywordDestUrlDisplay", "Impressions", _
          "Clicks", "CTR", "AveragePosition"}

      Dim filePath As String = "C:\report.csv"

      ' Option 1: Call the async version and wait on delegate handle
      ' Suited for UI as well as console applications, depending on
      ' the context.
      Dim result As IAsyncResult = utilities.BeginDownloadReportAsCsv(reportJob, filePath, Nothing)
      result.AsyncWaitHandle.WaitOne()

      Try
        utilities.EndDownloadReportAsCsv(result)
      Catch ex As SoapException
        Console.WriteLine("An exception occurred while generating reports. Message says : {0}", _
            ex.Message)
      End Try

      ' Option 2: Call the normal version. Most suited for console
      ' applications.
      Try
        utilities.DownloadReportAsCsv(reportJob, filePath)
      Catch ex As SoapException
        Console.WriteLine("An exception occurred while generating reports. Message says : {0}", _
            ex.Message)
      End Try

      ' Option 3: Call the async version get a call on your callback
      ' Most suited for UI applications.
      utilities.BeginDownloadReportAsCsv(reportJob, filePath, New AsyncCallback( _
          AddressOf reportDownloadCallback))
    End Sub

    ''' <summary>
    ''' Callback method ReportUtilities.BeginDownloadReportAsCsv
    ''' </summary>
    ''' <param name="handle">The IAsyncResult handle for downloading report.
    ''' </param>
    Private Sub reportDownloadCallback(ByVal handle As IAsyncResult)
      Dim asyncDelegate As ReportUtilities.GenerateReport = DirectCast(handle.AsyncState,  _
          ReportUtilities.GenerateReport)
      Try
        asyncDelegate.EndInvoke(handle)
      Catch ex As SoapException
        Console.WriteLine("An exception occurred while generating reports. Message says : {0}", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
