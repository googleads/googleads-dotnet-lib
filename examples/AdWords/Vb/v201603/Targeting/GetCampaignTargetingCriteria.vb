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
  ''' This code example gets all targeting criteria for a campaign.  To set
  ''' campaign targeting criteria, run AddCampaignTargetingCriteria.vb. To get
  ''' campaigns, run GetCampaigns.vb.
  ''' </summary>
  Public Class GetCampaignTargetingCriteria
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetCampaignTargetingCriteria
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        codeExample.Run(New AdWordsUser, campaignId)
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
        Return "This code example gets all targeting criteria for a campaign.  To set campaign " & _
            "targeting criteria, run AddCampaignTargetingCriteria.vb. To get campaigns, run " & _
            "GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign from which targeting
    ''' criteria are retrieved.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the CampaignCriterionService.
      Dim campaignCriterionService As CampaignCriterionService = CType(user.GetService( _
          AdWordsService.v201603.CampaignCriterionService),  _
          CampaignCriterionService)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {
        Criterion.Fields.Id, Criterion.Fields.CriteriaType, CampaignCriterion.Fields.CampaignId
      }

      ' Set the filters.
      Dim predicate As New Predicate
      predicate.field = "CampaignId"
      predicate.operator = PredicateOperator.EQUALS
      predicate.values = New String() {campaignId.ToString}

      selector.predicates = New Predicate() {predicate}

      ' Set the selector paging.
      selector.paging = Paging.Default
      Dim page As New CampaignCriterionPage

      Try
        Dim i As Integer = 0
        Do
          ' Get all campaign targets.
          page = campaignCriterionService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            For Each campaignCriterion As CampaignCriterion In page.entries
              Dim negative As String = ""
              If (TypeOf campaignCriterion Is NegativeCampaignCriterion) Then
                negative = "Negative "
              End If
              Console.WriteLine("{0}) {1}Campaign targeting criterion with id = '{2}' and " & _
                  "Type = {3} was found for campaign id '{4}'", i, negative, _
                  campaignCriterion.criterion.id, campaignCriterion.criterion.type, _
                  campaignCriterion.campaignId)
              i += 1
            Next
          End If
          selector.paging.IncreaseOffset()
        Loop While (selector.paging.startIndex < page.totalNumEntries)
        Console.WriteLine("Number of campaign targeting criteria found: {0}", page.totalNumEntries)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to get campaign targeting criteria.", e)
      End Try
    End Sub
  End Class
End Namespace
