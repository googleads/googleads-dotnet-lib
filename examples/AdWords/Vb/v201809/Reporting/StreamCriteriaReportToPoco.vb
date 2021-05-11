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
    ''' This code example streams the results of an ad hoc report, collecting
    ''' total impressions by network from each line. This demonstrates how you
    ''' can extract data from a large report without holding the entire result
    ''' set in memory or using files.
    ''' </summary>
    Public Class StreamCriteriaReportToPoco
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New StreamCriteriaReportToPoco
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
                Return "This code example streams the results of an ad hoc report, collecting" &
                       " total impressions by network from each line. This demonstrates how you" &
                       " can extract data from a large report without holding the entire result" &
                       " set in memory or using files."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            ' Create the query.
            Dim query As ReportQuery = New ReportQueryBuilder() _
                    .Select("Id", "AdNetworkType1", "Impressions") _
                    .From(ReportDefinitionReportType.CRITERIA_PERFORMANCE_REPORT) _
                    .Where("Status").In("ENABLED", "PAUSED") _
                    .During(ReportDefinitionDateRangeType.LAST_7_DAYS) _
                    .Build()

            Dim reportUtilities As New ReportUtilities(user, "v201809", query,
                                                       DownloadFormat.GZIPPED_XML.ToString())

            Dim impressionsByAdNetworkType1 As New Dictionary(Of String, Long)

            Try
                Using response As ReportResponse = reportUtilities.GetResponse
                    Using gzipStream As GZipStream = New GZipStream(response.Stream,
                                                                    CompressionMode.Decompress)

                        ' Deserialize the report into a list of CriteriaReportRow.
                        ' You can also deserialize the list into your own POCOs as follows.
                        ' 1. Annotate your class properties with ReportRow annotation.
                        '
                        '  Public Class MyCriteriaReportRow
                        '
                        '    <ReportColumn>
                        '    Public Property KeywordID as Long
                        '
                        '    <ReportColumn>
                        '    Public Property Impressions as Long
                        '  End Class
                        '
                        ' 2. Deserialize into your own report rows.
                        '
                        ' Dim report As New AwReport(Of CriteriaReportRow) _
                        '                        (New AwXmlTextReader(gzipStream), "Example")
                        Using report As New AwReport(Of CriteriaReportRow) _
                            (New AwXmlTextReader(gzipStream), "Example")
                            While report.MoveNext()
                                Console.WriteLine(report.Current.Impressions)
                                Console.Write(" ")
                                Console.Write(report.Current.KeywordId)
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

    Public Class CriteriaReportRow
        Private _KeywordId As Long
        Private _Impressions As Long

        <ReportColumn("keywordID")>
        Public Property KeywordId As Long
            Get
                Return _KeywordId
            End Get
            Set(value As Long)
                _KeywordId = value
            End Set
        End Property

        <ReportColumn("impressions")>
        Public Property Impressions As Long
            Get
                Return _Impressions
            End Get
            Set(value As Long)
                _Impressions = value
            End Set
        End Property
    End Class
End Namespace
