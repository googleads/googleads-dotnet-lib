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
Imports Google.Api.Ads.AdWords.v201109

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example gets report fields.
  '''
  ''' Tags: ReportDefinitionService.getReportFields
  ''' </summary>
  Class GetReportFields
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets report fields."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetReportFields
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
          AdWordsService.v201109.ReportDefinitionService)

      ' The type of the report to get fields for. Ex: KEYWORDS_PERFORMANCE_REPORT
      Dim reportType As ReportDefinitionReportType = System.Enum.Parse( _
          GetType(ReportDefinitionReportType), _T("INSERT_REPORT_TYPE_HERE"))

      Try
        ' Get report fields.
        Dim reportDefinitionFields As ReportDefinitionField() = _
            reportDefinitionService.getReportFields(reportType)
        If ((Not reportDefinitionFields Is Nothing) AndAlso _
            (reportDefinitionFields.Length > 0)) Then
          ' Display report fields.
          Console.WriteLine("The report type '{0}' contains the following fields:", reportType)

          For Each reportDefinitionField As ReportDefinitionField In reportDefinitionFields
            Console.Write("- {0} ({1})", reportDefinitionField.fieldName, _
                reportDefinitionField.fieldType)
            If (Not reportDefinitionField.enumValues Is Nothing) Then
              Console.Write(" := [{0}]", String.Join(", ", reportDefinitionField.enumValues))
            End If
            Console.WriteLine()
          Next
        Else
          Console.WriteLine("This report type has no fields.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve fields for report type. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
