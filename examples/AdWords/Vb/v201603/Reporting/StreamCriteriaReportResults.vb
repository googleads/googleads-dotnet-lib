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
Imports System.IO.Compression
Imports System.Xml

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example streams the results of an ad hoc report, collecting
  ''' total impressions by network from each line. This demonstrates how you
  ''' can extract data from a large report without holding the entire result
  ''' set in memory or using files.
  ''' </summary>
  Public Class StreamCriteriaReportResults
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New StreamCriteriaReportResults
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
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
        Return "This code example streams the results of an ad hoc report, collecting" & _
            " total impressions by network from each line. This demonstrates how you" & _
            " can extract data from a large report without holding the entire result" & _
            " set in memory or using files."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' </param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Create the query.
      Dim query As String = "SELECT Id, AdNetworkType1, Impressions FROM " & _
          "CRITERIA_PERFORMANCE_REPORT WHERE Status IN [ENABLED, PAUSED] DURING LAST_7_DAYS"

      Dim reportUtilities As New ReportUtilities(user, "v201603", query, _
                                     DownloadFormat.GZIPPED_XML.ToString())

      Dim impressionsByAdNetworkType1 As New Dictionary(Of String, Long)

      Try
        Using response As ReportResponse = reportUtilities.GetResponse
          Using gzipStream As GZipStream = New GZipStream(response.Stream, _
              CompressionMode.Decompress)
            Using reader As XmlTextReader = New XmlTextReader(gzipStream)
              While reader.Read
                Select Case reader.NodeType
                  Case XmlNodeType.Element ' The node is an Element.
                    If reader.Name = "row" Then
                      ParseRow(impressionsByAdNetworkType1, reader)
                    End If
                End Select
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

    ''' <summary>
    ''' Parses a report row.
    ''' </summary>
    ''' <param name="impressionsByAdNetworkType1">The map that keeps track of
    ''' the impressions grouped by by ad network type1.</param>
    ''' <param name="reader">The XML reader that parses the report.</param>
    Private Sub ParseRow(ByVal impressionsByAdNetworkType1 As Dictionary(Of String, Long), _
        ByVal reader As XmlTextReader)
      Dim network As String = Nothing
      Dim impressions As Long = 0

      While reader.MoveToNextAttribute
        Select Case reader.Name
          Case "network"
            network = reader.Value

          Case "impressions"
            impressions = Long.Parse(reader.Value)
        End Select
      End While

      If Not (network Is Nothing) Then
        If Not (impressionsByAdNetworkType1.ContainsKey(network)) Then
          impressionsByAdNetworkType1(network) = 0
        End If
        impressionsByAdNetworkType1(network) += impressions
      End If
    End Sub
  End Class
End Namespace
