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
  ''' This code example gets all report definitions. To add a report
  ''' definition, run AddKeywordsPerformanceReportDefinition.vb.
  '''
  ''' Tags: ReportDefinitionService.get
  ''' </summary>
  Class GetAllReportDefinitions
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all report definitions. To add a report definition, " & _
            "run AddKeywordsPerformanceReportDefinition.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllReportDefinitions
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the ReportDefinitionService.
      Dim reportDefinitionService As ReportDefinitionService = user.GetService( _
          AdWordsService.v201101.ReportDefinitionService)

      ' Create selector.
      Dim selector As New ReportDefinitionSelector

      Try
        ' Get all report definitions.
        Dim page As ReportDefinitionPage = reportDefinitionService.get(selector)

        ' Display report definitions.
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing) _
            AndAlso (page.entries.Length > 0)) Then
          For Each reportDefinition As ReportDefinition In page.entries
            Console.WriteLine("ReportDefinition with name ""{0}"" and id ""{1}"" was found.", _
                reportDefinition.reportName, reportDefinition.id)
          Next
        Else
          Console.WriteLine("No report definitions were found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve report definitions. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
