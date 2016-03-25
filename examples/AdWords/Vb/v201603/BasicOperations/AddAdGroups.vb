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
  ''' This code example illustrates how to create ad groups. To create
  ''' campaigns, run AddCampaigns.vb.
  ''' </summary>
  Public Class AddAdGroups
    Inherits ExampleBase
    ''' <summary>
    ''' Number of items being added / updated in this code example.
    ''' </summary>
    Const NUM_ITEMS As Integer = 5

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddAdGroups
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
        Return "This code example illustrates how to create ad groups. To create campaigns," & _
            " run AddCampaigns.vb"
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign to which ad groups are
    ''' added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupService), AdGroupService)

      Dim operations As New List(Of AdGroupOperation)

      For i As Integer = 1 To NUM_ITEMS
        ' Create the ad group.
        Dim adGroup As New AdGroup
        adGroup.name = String.Format("Earth to Mars Cruises #{0}", ExampleUtilities.GetRandomString)
        adGroup.status = AdGroupStatus.ENABLED
        adGroup.campaignId = campaignId

        ' Set the ad group bids.
        Dim biddingConfig As New BiddingStrategyConfiguration()

        Dim cpcBid As New CpcBid()
        cpcBid.bid = New Money()
        cpcBid.bid.microAmount = 10000000

        biddingConfig.bids = New Bids() {cpcBid}

        adGroup.biddingStrategyConfiguration = biddingConfig

        ' Optional: Set targeting restrictions.
        ' Depending on the criterionTypeGroup value, most TargetingSettingDetail
        ' only affect Display campaigns. However, the USER_INTEREST_AND_LIST value
        ' works for RLSA campaigns - Search campaigns targeting using a
        ' remarketing list.
        Dim targetingSetting As New TargetingSetting()

        ' Restricting to serve ads that match your ad group placements.
        ' This is equivalent to choosing "Target and bid" in the UI.
        Dim placementDetail As New TargetingSettingDetail()
        placementDetail.criterionTypeGroup = CriterionTypeGroup.PLACEMENT
        placementDetail.targetAll = False

        ' Using your ad group verticals only for bidding. This is equivalent
        ' to choosing "Bid only" in the UI.
        Dim verticalDetail As New TargetingSettingDetail()
        verticalDetail.criterionTypeGroup = CriterionTypeGroup.VERTICAL
        verticalDetail.targetAll = True

        targetingSetting.details = New TargetingSettingDetail() {placementDetail, verticalDetail}

        adGroup.settings = New Setting() {targetingSetting}

        ' Create the operation.
        Dim operation As New AdGroupOperation
        operation.operator = [Operator].ADD
        operation.operand = adGroup

        operations.Add(operation)
      Next

      Try
        ' Create the ad group.
        Dim retVal As AdGroupReturnValue = adGroupService.mutate(operations.ToArray())

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each newAdGroup As AdGroup In retVal.value
            Console.WriteLine("Ad group with id = '{0}' and name = '{1}' was created.", _
                newAdGroup.id, newAdGroup.name)
          Next
        Else
          Console.WriteLine("No ad groups were created.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create ad groups.", e)
      End Try
    End Sub
  End Class
End Namespace
