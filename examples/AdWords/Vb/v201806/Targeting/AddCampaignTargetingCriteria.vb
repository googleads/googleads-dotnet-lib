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
Imports Google.Api.Ads.AdWords.v201806

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
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
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
            End Try
        End Sub

        ''' <summary>
        ''' Returns a description about the code example.
        ''' </summary>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return _
                    "This code example adds various types of targeting criteria to a campaign. " &
                    "To get a list of campaigns, run GetCampaigns.vb."
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
            Using campaignCriterionService As CampaignCriterionService = CType(
                user.GetService(
                    AdWordsService.v201806.CampaignCriterionService),
                CampaignCriterionService)

                ' Create locations. The IDs can be found in the documentation or
                ' retrieved with the LocationCriterionService.
                Dim california As New Location
                california.id = 21137L

                Dim mexico As New Location
                mexico.id = 2484L

                ' Create languages. The IDs can be found in the documentation or
                ' retrieved with the ConstantDataService.
                Dim english As New Language
                english.id = 1000L

                Dim spanish As New Language
                spanish.id = 1003L

                Dim criteria As New List(Of Criterion)()
                criteria.AddRange(New Criterion() { _
                                                      california, mexico, english, spanish
                                                  })

                ' Distance targeting. Area of 10 miles around the locations in the location feed.
                If feedId.HasValue Then
                    Dim radiusLocationGroup As New LocationGroups
                    radiusLocationGroup.feedId = feedId.Value

                    Dim radiusMatchingFunction As New [Function]
                    radiusMatchingFunction.operator = FunctionOperator.IDENTITY

                    Dim radiusOperand As New LocationExtensionOperand
                    radiusOperand.radius = New ConstantOperand
                    radiusOperand.radius.type = ConstantOperandConstantType.DOUBLE
                    radiusOperand.radius.unit = ConstantOperandUnit.MILES
                    radiusOperand.radius.doubleValue = 10

                    radiusMatchingFunction.lhsOperand = New FunctionArgumentOperand() _
                        {radiusOperand}

                    criteria.Add(radiusLocationGroup)
                End If

                ' Create operations to add each of the criteria above.
                Dim operations As New List(Of CampaignCriterionOperation)

                For Each criterion As Criterion In criteria
                    Dim campaignCriterion As New CampaignCriterion
                    campaignCriterion.campaignId = campaignId
                    campaignCriterion.criterion = criterion

                    Dim operation As New CampaignCriterionOperation
                    operation.operator = [Operator].ADD
                    operation.operand = campaignCriterion

                    operations.Add(operation)
                Next

                ' Add a negative campaign criterion.

                Dim negativeCriterion As New NegativeCampaignCriterion
                negativeCriterion.campaignId = campaignId

                Dim keyword As New Keyword
                keyword.text = "jupiter cruise"
                keyword.matchType = KeywordMatchType.BROAD

                negativeCriterion.criterion = keyword

                Dim negativeCriterionOperation As New CampaignCriterionOperation
                negativeCriterionOperation.operand = negativeCriterion
                negativeCriterionOperation.operator = [Operator].ADD

                operations.Add(negativeCriterionOperation)

                Try
                    ' Set the campaign targets.
                    Dim retVal As CampaignCriterionReturnValue = campaignCriterionService.mutate(
                        operations.ToArray())

                    ' Display the results.
                    If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
                        For Each criterion As CampaignCriterion In retVal.value
                            Console.WriteLine(
                                "Campaign criterion of type '{0}' was set to campaign with id " &
                                "= '{1}'.", criterion.criterion.CriterionType, criterion.campaignId)
                        Next
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to set campaign criteria.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
