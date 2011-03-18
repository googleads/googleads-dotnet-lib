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
Imports Google.Api.Ads.AdWords.v13

Imports System
Imports System.Threading
Imports System.Web.Services.Protocols

Namespace Google.Api.Ads.AdWords.Examples.VB.v13
  ''' <summary>
  ''' This code example schedules a structure report and retrives its
  ''' destination url.
  ''' </summary>
  Class ReportServiceStructureDemo
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example schedules a structure report and retrives its destination url."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New ReportServiceStructureDemo
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the service.
      Dim service As ReportService = user.GetService(AdWordsService.v13.ReportService)

      ' Create the report job.
      Dim reportJob As New DefinedReportJob
      reportJob.name = "Structure Report"
      reportJob.selectedReportType = "Structure"
      reportJob.aggregationTypes = New String() {"Keyword"}
      reportJob.selectedColumns = New String() {"Campaign", "CampaignId", "AdGroup", "AdGroupId", _
          "Keyword", "KeywordId", "KeywordStatus", "MaximumCPC"}

      Try
        ' Validate the report job.
        service.validateReportJob(reportJob)

        ' Submit the request for the report.
        Dim jobId As Long = service.scheduleReportJob(reportJob)

        ' Wait until the report has been generated.
        Dim status As ReportJobStatus = service.getReportJobStatus(jobId)

        Do While ((status <> ReportJobStatus.Completed) AndAlso (status <> ReportJobStatus.Failed))
          Thread.Sleep(30000)
          status = service.getReportJobStatus(jobId)
          Console.WriteLine(("Report job status is " & status))
        Loop
        If (status = ReportJobStatus.Failed) Then
          Console.WriteLine("Job failed!")
        Else
          ' Report is ready.
          Console.WriteLine("The report is ready!")

          ' Download the report.
          Dim url As String = service.getReportDownloadUrl(jobId)
          Console.WriteLine("Download it at url {0}", url)
        End If
      Catch e As SoapException
        Console.WriteLine("Report job is invalid. Exception: {0}", e.Message)
      End Try
    End Sub
  End Class
End Namespace
