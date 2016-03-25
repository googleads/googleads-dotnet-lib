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
  ''' This code example adds various types of targeting criteria to a campaign.
  ''' To get a list of campaigns, run GetCampaigns.vb.
  ''' </summary>
  Public Class AddCampaignTargetingCriteria
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddCampaignTargetingCriteria
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        Dim feedIdText As String = "INSERT_LOCATION_FEED_ID_HERE"

        Dim feedId As Long? = Nothing
        Dim temp As Long = 0

        If Long.TryParse(feedIdText, temp) Then
          feedId = temp
        End If

        codeExample.Run(New AdWordsUser(), campaignId, feedId)
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
        Return "This code example adds various types of targeting criteria to a campaign. To " & _
            "get a list of campaigns, run GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign to which targeting criteria
    ''' are added.</param>
    ''' <param name="feedId">ID of a feed that has been configured for location
    ''' targeting, meaning it has an ENABLED FeedMapping with criterionType of
    ''' 77. Feeds linked to a GMB account automatically have this FeedMapping.
    ''' If you don't have such a feed, set this value to Nothing.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long, ByVal feedId As Long?)
      ' Get the CampaignCriterionService.
      Dim campaignCriterionService As CampaignCriterionService = CType(user.GetService( _
          AdWordsService.v201603.CampaignCriterionService),  _
          CampaignCriterionService)

      ' Create language criteria.
      ' See http://code.google.com/apis/adwords/docs/appendix/languagecodes.html
      ' for a detailed list of language codes.
      Dim language1 As New Language
      language1.id = 1002 ' French
      Dim languageCriterion1 As New CampaignCriterion
      languageCriterion1.campaignId = campaignId
      languageCriterion1.criterion = language1

      Dim language2 As New Language
      language2.id = 1005  ' Japanese
      Dim languageCriterion2 As New CampaignCriterion
      languageCriterion2.campaignId = campaignId
      languageCriterion2.criterion = language2

      ' Target Tier 3 income group near Miami, Florida.
      Dim incomeLocationGroups As New LocationGroups

      Dim incomeOperand As New IncomeOperand
      ' Tiers are numbered 1-10, and represent 10% segments of earners.
      ' For example, TIER_1 is the top 10%, TIER_2 is the 80-90%, etc.
      ' Tiers 6 through 10 are grouped into TIER_6_TO_10.
      incomeOperand.tier = IncomeTier.TIER_3

      Dim geoTargetOperand1 As New GeoTargetOperand
      geoTargetOperand1.locations = New Long() {1015116} ' Miami, FL.

      incomeLocationGroups.matchingFunction = New [Function]
      incomeLocationGroups.matchingFunction.lhsOperand = _
          New FunctionArgumentOperand() {incomeOperand}
      incomeLocationGroups.matchingFunction.operator = FunctionOperator.AND
      incomeLocationGroups.matchingFunction.rhsOperand = _
          New FunctionArgumentOperand() {geoTargetOperand1}

      Dim locationGroupCriterion1 As New CampaignCriterion
      locationGroupCriterion1.campaignId = campaignId
      locationGroupCriterion1.criterion = incomeLocationGroups

      ' Target places of interest near Downtown Miami, Florida.
      Dim interestLocationGroups As New LocationGroups

      Dim placesOfInterestOperand As New PlacesOfInterestOperand()
      placesOfInterestOperand.category = PlacesOfInterestOperandCategory.DOWNTOWN

      Dim geoTargetOperand2 As New GeoTargetOperand
      geoTargetOperand2.locations = New Long() {1015116} ' Miami, FL.

      interestLocationGroups.matchingFunction = New [Function]
      interestLocationGroups.matchingFunction.lhsOperand = _
          New FunctionArgumentOperand() {placesOfInterestOperand}
      interestLocationGroups.matchingFunction.operator = FunctionOperator.AND
      interestLocationGroups.matchingFunction.rhsOperand = _
          New FunctionArgumentOperand() {geoTargetOperand2}

      Dim locationGroupCriterion2 As New CampaignCriterion
      locationGroupCriterion2.campaignId = campaignId
      locationGroupCriterion2.criterion = interestLocationGroups

      Dim locationGroupCriterion3 As New CampaignCriterion

      If feedId.HasValue Then
        ' Distance targeting. Area of 10 miles around targets above.
        Dim radius As New ConstantOperand
        radius.type = ConstantOperandConstantType.DOUBLE
        radius.unit = ConstantOperandUnit.MILES
        radius.doubleValue = 10.0
        Dim distance As New LocationExtensionOperand
        distance.radius = radius

        Dim radiusLocationGroups As New LocationGroups
        radiusLocationGroups.matchingFunction = New [Function]
        radiusLocationGroups.matchingFunction.operator = FunctionOperator.IDENTITY
        radiusLocationGroups.matchingFunction.lhsOperand = New FunctionArgumentOperand() {distance}

        ' FeedID should be the ID of a feed that has been configured for location
        ' targeting, meaning it has an ENABLED FeedMapping with criterionType of
        ' 77. Feeds linked to a GMB account automatically have this FeedMapping.
        radiusLocationGroups.feedId = feedId.Value

        locationGroupCriterion3.campaignId = campaignId
        locationGroupCriterion3.criterion = radiusLocationGroups
      End If

      ' Create location criteria.
      ' See http://code.google.com/apis/adwords/docs/appendix/countrycodes.html
      ' for a detailed list of country codes.
      Dim location1 As New Location
      location1.id = 2840  ' USA
      Dim locationCriterion1 As New CampaignCriterion
      locationCriterion1.campaignId = campaignId
      locationCriterion1.criterion = location1

      Dim location2 As New Location
      location2.id = 2392 ' Japan
      Dim locationCriterion2 As New CampaignCriterion
      locationCriterion2.campaignId = campaignId
      locationCriterion2.criterion = location2

      ' Add a negative campaign keyword.
      Dim negativeCriterion As New NegativeCampaignCriterion()
      negativeCriterion.campaignId = campaignId

      Dim keyword As New Keyword()
      keyword.matchType = KeywordMatchType.BROAD
      keyword.text = "jupiter cruise"

      negativeCriterion.criterion = keyword

      Dim criteria As New List(Of CampaignCriterion)(New CampaignCriterion() {languageCriterion1, _
          languageCriterion2, locationCriterion1, locationCriterion2, negativeCriterion, _
          locationGroupCriterion1, locationGroupCriterion2})

      If feedId.HasValue Then
        criteria.Add(locationGroupCriterion3)
      End If

      Dim operations As New List(Of CampaignCriterionOperation)

      For Each criterion As CampaignCriterion In criteria
        Dim operation As New CampaignCriterionOperation
        operation.operator = [Operator].ADD
        operation.operand = criterion
        operations.Add(operation)
      Next

      Try
        ' Set the campaign targets.
        Dim retVal As CampaignCriterionReturnValue = campaignCriterionService.mutate( _
            operations.ToArray())

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          For Each criterion As CampaignCriterion In retVal.value
            Console.WriteLine("Campaign criterion of type '{0}' was set to campaign with id " & _
                "= '{1}'.", criterion.criterion.CriterionType, criterion.campaignId)
          Next
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to set campaign criteria.", e)
      End Try
    End Sub
  End Class
End Namespace