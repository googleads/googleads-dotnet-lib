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
Imports Google.Api.Ads.AdWords.v201109_1

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109_1
  ''' <summary>
  ''' This code example gets report fields.
  '''
  ''' Tags: ReportDefinitionService.getReportFields
  ''' </summary>
  Public Class GetReportFields
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetReportFields
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
        Return "This code example gets report fields."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"REPORT_TYPE"}
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
      ' Get the ReportDefinitionService.
      Dim reportDefinitionService As ReportDefinitionService = user.GetService( _
          AdWordsService.v201109_1.ReportDefinitionService)

      ' The type of the report to get fields for.
      ' E.g.: KEYWORDS_PERFORMANCE_REPORT
      Dim reportType As ReportDefinitionReportType = [Enum].Parse( _
          GetType(ReportDefinitionReportType), parameters("REPORT_TYPE"))

      Try
        ' Get the report fields.
        Dim reportDefinitionFields As ReportDefinitionField() = _
            reportDefinitionService.getReportFields(reportType)

        If ((Not reportDefinitionFields Is Nothing) AndAlso _
            (reportDefinitionFields.Length > 0)) Then
          ' Display report fields.
          writer.WriteLine("The report type '{0}' contains the following fields:", reportType)

          For Each reportDefinitionField As ReportDefinitionField In reportDefinitionFields
            writer.Write("- {0} ({1})", reportDefinitionField.fieldName, _
                reportDefinitionField.fieldType)
            If (Not reportDefinitionField.enumValues Is Nothing) Then
              writer.Write(" := [{0}]", String.Join(", ", reportDefinitionField.enumValues))
            End If
            writer.WriteLine()
          Next
        Else
          writer.WriteLine("This report type has no fields.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve fields for report type.", ex)
      End Try
    End Sub
  End Class
End Namespace
