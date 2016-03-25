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
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example lists all campaigns using an AWQL query. See
  ''' https://developers.google.com/adwords/api/docs/guides/awql for AWQL
  ''' documentation. To add a campaign, run AddCampaign.vb.
  ''' </summary>
  Public Class GetCampaignsWithAwql
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetCampaignsWithAwql
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
        Return "This code example lists all campaigns using an AWQL query. See " & _
            "https://developers.google.com/adwords/api/docs/guides/awql for AWQL " & _
            "documentation. To add a campaign, run AddCampaign.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the CampaignService.
      Dim campaignService As CampaignService = CType(user.GetService( _
          AdWordsService.v201603.CampaignService), CampaignService)

      ' Create the query.
      Dim query As String = "SELECT Id, Name, Status ORDER BY Name"

      Dim offset As Long = 0
      Dim pageSize As Long = 500

      Dim page As New CampaignPage()

      Try
        Do
          Dim queryWithPaging As String = String.Format("{0} LIMIT {1}, {2}", query, offset, _
              pageSize)

          ' Get the campaigns.
          page = campaignService.query(queryWithPaging)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = CInt(offset)
            For Each campaign As Campaign In page.entries
              Console.WriteLine("{0}) Campaign with id = '{1}', name = '{2}' and status = " & _
                  "'{3}' was found.", i, campaign.id, campaign.name, campaign.status)
              i += 1
            Next
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        Console.WriteLine("Number of campaigns found: {0}", page.totalNumEntries)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to retrieve campaign(s).", e)
      End Try
    End Sub
  End Class
End Namespace
