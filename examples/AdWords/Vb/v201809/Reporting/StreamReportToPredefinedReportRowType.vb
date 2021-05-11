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
Imports Google.Api.Ads.AdWords.Util.Reports
Imports Google.Api.Ads.AdWords.Util.Reports.v201809
Imports Google.Api.Ads.AdWords.v201809
Imports Google.Api.Ads.Common.Util.Reports

Imports System.IO.Compression

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example streams the results of an ad hoc report, and
    ''' returns the data in the report as objects of a predefined report row type.
    ''' </summary>
    Public Class StreamReportToPredefinedReportRowType
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New StreamReportToPredefinedReportRowType
            Console.WriteLine(codeExample.Description)
            Try
                codeExample.Run(New AdWordsUser)
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
                Return "This code example streams the results of an ad hoc report, and " &
                       "returns the data in the report as objects of a predefined report row type."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            ' Create the query.
            Dim query As String = "SELECT AccountCurrencyCode, AccountDescriptiveName" &
                                  " FROM FINAL_URL_REPORT DURING LAST_7_DAYS"

            Dim reportUtilities As New ReportUtilities(user, "v201809", query,
                                                       DownloadFormat.GZIPPED_XML.ToString())

            Dim impressionsByAdNetworkType1 As New Dictionary(Of String, Long)

            Try
                Using response As ReportResponse = reportUtilities.GetResponse
                    Using gzipStream As GZipStream = New GZipStream(response.Stream,
                                                                    CompressionMode.Decompress)

                        Using report As New AwReport(Of FinalUrlReportReportRow) _
                            (New AwXmlTextReader(gzipStream), "Example")
                            While report.MoveNext()
                                Console.WriteLine(report.Current.accountCurrencyCode)
                                Console.Write(" ")
                                Console.Write(report.Current.accountDescriptiveName)
                            End While
                        End Using
                    End Using
                End Using

                Console.WriteLine("Network, Impressions")
                For Each network As String In impressionsByAdNetworkType1.Keys
                    Console.WriteLine("{0}, {1}", network, impressionsByAdNetworkType1(network))
                Next
            Catch e As Exception
                Throw New System.ApplicationException("Failed to download report.", e)
            End Try
        End Sub
    End Class
End Namespace
