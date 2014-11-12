' Copyright 2014, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201409

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201409
  ''' <summary>
  ''' This code example illustrates how to retrieve ad group level mobile bid
  ''' modifiers for a campaign.
  '''
  ''' AdGroupBidModifierService.get
  ''' </summary>
  Public Class GetAdGroupBidModifiers
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetAdGroupBidModifiers
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        codeExample.Run(New AdWordsUser, campaignId)
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
        Return "This code example illustrates how to retrieve ad group level mobile bid" & _
            " modifiers for a campaign."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign for which adgroup bid
    ''' modifiers are retrieved.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the AdGroupBidModifierService.
      Dim adGroupBidModifierService As AdGroupBidModifierService = CType(user.GetService( _
          AdWordsService.v201409.AdGroupBidModifierService),  _
          AdWords.v201409.AdGroupBidModifierService)

      Const PAGE_SIZE As Integer = 500

      ' Get all ad group bid modifiers for the campaign.
      Dim selector As New Selector()
      selector.fields = New String() {"CampaignId", "AdGroupId", "BidModifier", _
        "BidModifierSource", "CriteriaType", "Id"}

      Dim predicate As New Predicate()
      predicate.field = "CampaignId"
      predicate.[operator] = PredicateOperator.EQUALS
      predicate.values = New String() {campaignId.ToString()}
      selector.predicates = New Predicate() {predicate}

      ' Set the selector paging.
      selector.paging = New Paging()

      Dim offset As Integer = 0
      Dim pageSize As Integer = PAGE_SIZE

      Dim page As New AdGroupBidModifierPage()

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get the campaigns.
          page = adGroupBidModifierService.get(selector)

          ' Display the results.
          If (Not page Is Nothing) AndAlso (Not page.entries Is Nothing) Then
            Dim i As Integer = offset
            For Each adGroupBidModifier As AdGroupBidModifier In page.entries
              Dim bidModifier As String = ""
              If adGroupBidModifier.bidModifierSpecified Then
                bidModifier = adGroupBidModifier.bidModifier.ToString()
              Else
                bidModifier = "UNSET"
              End If
              Console.WriteLine("{0}) Campaign ID {1}, AdGroup ID {2}, Criterion ID {3} has " & _
                  "ad group level modifier: {4}, source = {5}.", _
                  i + 1, adGroupBidModifier.campaignId, _
                  adGroupBidModifier.adGroupId, adGroupBidModifier.criterion.id, _
                  bidModifier, adGroupBidModifier.bidModifierSource)
              i = i + 1
            Next
          End If
          offset += pageSize
        Loop While offset < page.totalNumEntries
        Console.WriteLine("Number of adgroup bid modifiers found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve adgroup bid modifiers.", ex)
      End Try
    End Sub
  End Class
End Namespace
