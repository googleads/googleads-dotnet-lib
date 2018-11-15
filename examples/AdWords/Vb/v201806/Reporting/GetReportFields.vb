' Copyright 2018 Google LLC
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
Imports Google.Api.Ads.AdWords.v201806

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
    ''' <summary>
    ''' This code example gets report fields.
    ''' </summary>
    Public Class GetReportFields
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New GetReportFields
            Console.WriteLine(codeExample.Description)
            Try
                Dim reportType As ReportDefinitionReportType = CType(
                    [Enum].Parse(GetType(ReportDefinitionReportType), "INSERT_REPORT_TYPE_HERE"),
                    ReportDefinitionReportType)
                codeExample.Run(New AdWordsUser, reportType)
            Catch e As Exception
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
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
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="reportType">The report type to be run.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal reportType As ReportDefinitionReportType)
            Using reportDefinitionService As ReportDefinitionService = CType(
                user.GetService(
                    AdWordsService.v201806.ReportDefinitionService),
                ReportDefinitionService)

                ' The type of the report to get fields for.
                ' E.g.: KEYWORDS_PERFORMANCE_REPORT

                Try
                    ' Get the report fields.
                    Dim reportDefinitionFields As ReportDefinitionField() =
                            reportDefinitionService.getReportFields(reportType)

                    If ((Not reportDefinitionFields Is Nothing) AndAlso
                        (reportDefinitionFields.Length > 0)) Then
                        ' Display report fields.
                        Console.WriteLine("The report type '{0}' contains the following fields:",
                                          reportType)

                        For Each reportDefinitionField As ReportDefinitionField In _
                            reportDefinitionFields
                            Console.Write("- {0} ({1})", reportDefinitionField.fieldName,
                                          reportDefinitionField.fieldType)
                            If (Not reportDefinitionField.enumValues Is Nothing) Then
                                Console.Write(" := [{0}]",
                                              String.Join(", ", reportDefinitionField.enumValues))
                            End If
                            Console.WriteLine()
                        Next
                    Else
                        Console.WriteLine("This report type has no fields.")
                    End If
                Catch e As Exception
                    Throw New _
                        System.ApplicationException("Failed to retrieve fields for report type.",
                                                    e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
