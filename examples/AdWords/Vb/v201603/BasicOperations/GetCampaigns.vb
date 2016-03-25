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
  ''' This code example lists all campaigns. To add a campaign, run
  ''' AddCampaign.vb.
  ''' </summary>
  Public Class GetCampaigns
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetCampaigns
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
        Return "This code example lists all campaigns. To add a campaign, run AddCampaign.vb."
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

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {
        Campaign.Fields.Id, Campaign.Fields.Name, Campaign.Fields.Status
      }
      selector.paging = Paging.Default

      Dim page As New CampaignPage

      Try
        Do
          ' Get the campaigns.
          page = campaignService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = selector.paging.startIndex
            For Each campaign As Campaign In page.entries
              Console.WriteLine("{0}) Campaign with id = '{1}', name = '{2}' and status = " & _
                  "'{3}' was found.", i + 1, campaign.id, campaign.name, campaign.status)
              i += 1
            Next
          End If
          selector.paging.IncreaseOffset()
        Loop While (selector.paging.startIndex < page.totalNumEntries)
        Console.WriteLine("Number of campaigns found: {0}", page.totalNumEntries)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to retrieve campaign(s).", e)
      End Try
    End Sub
  End Class
End Namespace
