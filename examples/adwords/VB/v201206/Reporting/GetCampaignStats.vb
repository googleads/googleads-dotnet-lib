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
Imports Google.Api.Ads.AdWords.v201206

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201206
  ''' <summary>
  ''' This code example gets various statistics for campaigns that received at
  ''' least one impression during the last week. To get campaigns, run
  ''' GetCampaigns.vb.
  '''
  ''' Tags: CampaignService.get
  ''' </summary>
  Public Class GetCampaignStats
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetCampaignStats
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
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
        Return "This code example gets various statistics for campaigns that received at" & _
            " least one impression during the last week. To get campaigns, run" & _
            " GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the CampaignService.
      Dim campaignService As CampaignService = user.GetService( _
          AdWordsService.v201206.CampaignService)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "Name", "Status", "Impressions", "Cost", "Ctr"}

      ' Create the predicates.
      Dim predicate As New Predicate
      predicate.field = "Impressions"
      predicate.operator = PredicateOperator.GREATER_THAN
      predicate.values = New String() {"0"}
      selector.predicates = New Predicate() {predicate}

      ' Set the date range.
      Dim dateRange As New DateRange()
      dateRange.min = DateTime.Now.AddDays(-8).ToString("yyyyMMdd")
      dateRange.max = DateTime.Now.AddDays(-1).ToString("yyyyMMdd")
      selector.dateRange = dateRange

      ' Set the selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New CampaignPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get the campaigns.
          page = campaignService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset
            For Each campaign As Campaign In page.entries
              Console.WriteLine("{0}) Campaign with id = '{1}', name = '{2}' had the following " & _
                  "stats last week", i + 1, campaign.id, campaign.name)
              Console.WriteLine("  Clicks: {0}, Impressions: {1}, Cost: {2} micros. Ctr: {3}", _
                  campaign.campaignStats.clicks, campaign.campaignStats.impressions, _
                  campaign.campaignStats.cost, campaign.campaignStats.ctr)
              i += 1
            Next
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        Console.WriteLine("Number of campaigns found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve campaign(s).", ex)
      End Try
    End Sub
  End Class
End Namespace
