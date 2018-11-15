' Copyright 2018 Google LLC
'
' Licensed under the Apache License, Version 2.0 (the "License")
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http:'www.apache.org/licenses/LICENSE-2.0
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
    ''' This code example adds a page feed to specify precisely which URLs to use with your
    ''' Dynamic Search Ads campaign. To create a Dynamic Search Ads campaign, run
    ''' AddDynamicSearchAdsCampaign.vb. To get campaigns, run GetCampaigns.vb.
    ''' </summary>
    Public Class AddDynamicPageFeed
        Inherits ExampleBase

        ''' <summary>
        ''' The criterion type to be used for DSA page feeds.
        ''' </summary>
        ''' <remarks>DSA page feeds use criterionType field instead of the placeholderType field 
        ''' unlike most other feed types.</remarks>
        Private Const DSA_PAGE_FEED_CRITERION_TYPE As Integer = 61

        ''' <summary>
        ''' ID that corresponds to the page URLs.
        ''' </summary>
        Private Const DSA_PAGE_URLS_FIELD_ID As Integer = 1

        ''' <summary>
        ''' ID that corresponds to the labels.
        ''' </summary>
        Private Const DSA_LABEL_FIELD_ID As Integer = 2

        ''' <summary>
        ''' Class to keep track of DSA page feed details.
        ''' </summary>
        Private Class DSAFeedDetails
            Dim feedIdField As Long
            Dim urlAttributeIdField As Long
            Dim labelAttributeIdField As Long

            Public Property FeedId As Long
                Get
                    Return feedIdField
                End Get
                Set(ByVal value As Long)
                    feedIdField = value
                End Set
            End Property

            Public Property UrlAttributeId As Long
                Get
                    Return urlAttributeIdField
                End Get
                Set(ByVal value As Long)
                    urlAttributeIdField = value
                End Set
            End Property

            Public Property LabelAttributeId As Long
                Get
                    Return labelAttributeIdField
                End Get
                Set(ByVal value As Long)
                    labelAttributeIdField = value
                End Set
            End Property
        End Class

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddDynamicSearchAdsCampaign
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
                Return _
                    "This code example adds a page feed to specify precisely which URLs to use " &
                    "with your Dynamic Search Ads campaign. To create a Dynamic Search Ads " &
                    "campaign, run AddDynamicSearchAdsCampaign.vb. To get campaigns, run " &
                    "GetCampaigns.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long, ByVal adgroupId As Long)
            Dim dsaPageUrlLabel As String = "discounts"

            Try
                ' Get the page feed details. This code example creates a new feed, but you can
                ' fetch and re-use an existing feed.
                Dim feedDetails As DSAFeedDetails = CreateFeed(user)
                CreateFeedMapping(user, feedDetails)
                CreateFeedItems(user, feedDetails, dsaPageUrlLabel)

                ' Associate the page feed with the campaign.
                UpdateCampaignDsaSetting(user, campaignId, feedDetails.FeedId)

                ' Optional: Target Web pages matching the feed's label in the ad group.
                AddDsaTargeting(user, adgroupId, dsaPageUrlLabel)
                Console.WriteLine("Dynamic page feed setup is complete for campaign ID '{0}'.",
                                  campaignId)
            Catch e As Exception
                Throw _
                    New System.ApplicationException(
                        "Failed to setup dynamic page feed for campaign.", e)
            End Try
        End Sub

        ''' <summary>
        ''' Creates the feed for DSA page URLs.
        ''' </summary>
        ''' <param name="user">The AdWords User.</param>
        ''' <returns>The feed details.</returns>
        Private Function CreateFeed(ByVal user As AdWordsUser) As DSAFeedDetails
            Using feedService As FeedService = CType(
                user.GetService(
                    AdWordsService.v201806.FeedService),
                FeedService)

                ' Create attributes.
                Dim urlAttribute As New FeedAttribute()
                urlAttribute.type = FeedAttributeType.URL_LIST
                urlAttribute.name = "Page URL"

                Dim labelAttribute As New FeedAttribute()
                labelAttribute.type = FeedAttributeType.STRING_LIST
                labelAttribute.name = "Label"

                ' Create the feed.
                Dim sitelinksFeed As New Feed()
                sitelinksFeed.name = "DSA Feed " + ExampleUtilities.GetRandomString()
                sitelinksFeed.attributes = New FeedAttribute() {urlAttribute, labelAttribute}
                sitelinksFeed.origin = FeedOrigin.USER

                ' Create operation.
                Dim operation As New FeedOperation()
                operation.operand = sitelinksFeed
                operation.operator = [Operator].ADD

                ' Add the feed.
                Dim result As FeedReturnValue = feedService.mutate(New FeedOperation() {operation})

                Dim savedFeed As Feed = result.value(0)

                Dim retval As New DSAFeedDetails
                retval.FeedId = savedFeed.id
                retval.UrlAttributeId = savedFeed.attributes(0).id
                retval.LabelAttributeId = savedFeed.attributes(1).id
                Return retval
            End Using
        End Function

        ''' <summary>
        ''' Creates the feed mapping for DSA page feeds.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="feedDetails">The feed details.</param>
        Private Sub CreateFeedMapping(ByVal user As AdWordsUser,
                                      ByVal feedDetails As DSAFeedDetails)
            Using feedMappingService As FeedMappingService = CType(
                user.GetService(
                    AdWordsService.v201806.FeedMappingService),
                FeedMappingService)

                ' Map the FeedAttributeIds to the fieldId constants.
                Dim urlFieldMapping As New AttributeFieldMapping()
                urlFieldMapping.feedAttributeId = feedDetails.UrlAttributeId
                urlFieldMapping.fieldId = DSA_PAGE_URLS_FIELD_ID

                Dim labelFieldMapping As New AttributeFieldMapping()
                labelFieldMapping.feedAttributeId = feedDetails.LabelAttributeId
                labelFieldMapping.fieldId = DSA_LABEL_FIELD_ID

                ' Create the fieldMapping and operation.
                Dim feedMapping As New FeedMapping()
                feedMapping.criterionType = DSA_PAGE_FEED_CRITERION_TYPE
                feedMapping.feedId = feedDetails.FeedId
                feedMapping.attributeFieldMappings =
                    New AttributeFieldMapping() { _
                                                    urlFieldMapping,
                                                    labelFieldMapping
                                                }

                Dim operation As New FeedMappingOperation()
                operation.operand = feedMapping
                operation.operator = [Operator].ADD

                ' Add the field mapping.
                feedMappingService.mutate(New FeedMappingOperation() {operation})
            End Using
        End Sub

        ''' <summary>
        ''' Creates the page URLs in the DSA page feed.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="feedDetails">The feed details.</param>
        ''' <param name="labelName">The pagefeed url label.</param>
        Private Sub CreateFeedItems(ByVal user As AdWordsUser, ByVal feedDetails As DSAFeedDetails,
                                    ByVal labelName As String)
            Using feedItemService As FeedItemService = CType(
                user.GetService(
                    AdWordsService.v201806.FeedItemService),
                FeedItemService)

                Dim rentalCarsUrl As String = "http://www.example.com/discounts/rental-cars"
                Dim hotelDealsUrl As String = "http://www.example.com/discounts/hotel-deals"
                Dim flightDealsUrl As String = "http://www.example.com/discounts/flight-deals"
                Dim operations() As FeedItemOperation =
                        { _
                            CreateDsaUrlAddOperation(feedDetails,
                                                     rentalCarsUrl,
                                                     labelName),
                            CreateDsaUrlAddOperation(feedDetails,
                                                     hotelDealsUrl,
                                                     labelName),
                            CreateDsaUrlAddOperation(feedDetails,
                                                     flightDealsUrl,
                                                     labelName)
                        }
                feedItemService.mutate(operations)
            End Using
        End Sub

        ''' <summary>
        ''' Creates the DSA URL add operation.
        ''' </summary>
        ''' <param name="feedDetails">The page feed details.</param>
        ''' <param name="url">The DSA page feed URL.</param>
        ''' <param name="label">DSA page feed label.</param>
        ''' <returns>The DSA URL add operation.</returns>
        Private Function CreateDsaUrlAddOperation(ByVal feedDetails As DSAFeedDetails,
                                                  ByVal url As String, ByVal label As String) _
            As FeedItemOperation
            ' Create the FeedItemAttributeValues for our text values.
            Dim urlAttributeValue As New FeedItemAttributeValue()
            urlAttributeValue.feedAttributeId = feedDetails.UrlAttributeId

            ' See https://support.google.com/adwords/answer/7166527 for page feed URL 
            ' recommendations and rules.
            urlAttributeValue.stringValues = New String() {url}

            Dim labelAttributeValue As New FeedItemAttributeValue()
            labelAttributeValue.feedAttributeId = feedDetails.LabelAttributeId
            labelAttributeValue.stringValues = New String() {label}

            ' Create the feed item and operation.
            Dim item As New FeedItem()
            item.feedId = feedDetails.FeedId

            item.attributeValues = New FeedItemAttributeValue() { _
                                                                    urlAttributeValue,
                                                                    labelAttributeValue
                                                                }

            Dim operation As New FeedItemOperation()
            operation.operand = item
            operation.operator = [Operator].ADD

            Return operation
        End Function

        ''' <summary>
        ''' Updates the campaign DSA setting to add DSA pagefeeds.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignId">The Campaign ID.</param>
        ''' <param name="feedId">The page feed ID.</param>
        Private Sub UpdateCampaignDsaSetting(ByVal user As AdWordsUser, ByVal campaignId As Long,
                                             ByVal feedId As Long)
            ' [START getDsaSetting] MOE:strip_line
            Using campaignService As CampaignService = CType(
                user.GetService(
                    AdWordsService.v201806.CampaignService),
                CampaignService)

                Dim selector As New Selector()
                selector.fields = New String() {Campaign.Fields.Id, Campaign.Fields.Settings}
                selector.predicates = New Predicate() { _
                                                          Predicate.Equals(Campaign.Fields.Id,
                                                                           campaignId)
                                                      }
                selector.paging = Paging.Default

                Dim page As CampaignPage = campaignService.get(selector)

                If page Is Nothing Or page.entries Is Nothing Or page.entries.Length = 0 Then
                    Throw New System.ApplicationException(
                        String.Format(
                            "Failed to retrieve campaign with ID = {0}.", campaignId))
                End If

                Dim selectedCampaign As Campaign = page.entries(0)

                If selectedCampaign.settings Is Nothing Then
                    Throw New System.ApplicationException("This is not a DSA campaign.")
                End If

                Dim dsaSetting As DynamicSearchAdsSetting = Nothing
                Dim campaignSettings() As Setting = selectedCampaign.settings

                For i As Integer = 0 To selectedCampaign.settings.Length - 1
                    Dim setting As Setting = campaignSettings(i)
                    If TypeOf setting Is DynamicSearchAdsSetting Then
                        dsaSetting = CType(setting, DynamicSearchAdsSetting)
                        Exit For
                    End If
                Next

                If dsaSetting Is Nothing Then
                    Throw New System.ApplicationException("This is not a DSA campaign.")
                End If
                ' [END getDsaSetting] MOE:strip_line

                ' [START updateDsaSetting] MOE:strip_line
                ' Use a page feed to specify precisely which URLs to use with your
                ' Dynamic Search Ads.
                dsaSetting.pageFeed = New PageFeed()
                dsaSetting.pageFeed.feedIds = New Long() { _
                                                             feedId
                                                         }

                ' Optional: Specify whether only the supplied URLs should be used with your
                ' Dynamic Search Ads.
                dsaSetting.useSuppliedUrlsOnly = True

                Dim campaignToUpdate As New Campaign()
                campaignToUpdate.id = campaignId
                campaignToUpdate.settings = campaignSettings

                Dim operation As New CampaignOperation()
                operation.operand = campaignToUpdate
                operation.operator = [Operator].SET

                Try
                    Dim retval As CampaignReturnValue = campaignService.mutate(
                        New CampaignOperation() {operation})
                    Console.WriteLine(
                        "DSA page feed for campaign ID '{0}' was updated with feed ID '{1}'.",
                        campaignToUpdate.id, feedId)
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to set page feed for campaign.", e)
                End Try
            End Using
            ' [END updateDsaSetting] MOE:strip_line
        End Sub

        ''' <summary>
        ''' Set custom targeting for the page feed URLs based on a list of labels.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Ad group ID.</param>
        ''' <param name="labelName">The label name.</param>
        ''' <returns>The newly created webpage criterion.</returns>
        Private Function AddDsaTargeting(ByVal user As AdWordsUser, ByVal adgroupId As Long,
                                         ByVal labelName As String) As BiddableAdGroupCriterion
            ' [START addCustomLabelTargeting] MOE:strip_line
            Using adGroupCriterionService As AdGroupCriterionService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupCriterionService),
                AdGroupCriterionService)

                ' Create a webpage criterion.
                Dim webpage As New Webpage()

                Dim parameter As New WebpageParameter()
                parameter.criterionName = "Test criterion"
                webpage.parameter = parameter

                ' Add a condition for label=specified_label_name.
                Dim condition As New WebpageCondition()
                condition.operand = WebpageConditionOperand.CUSTOM_LABEL
                condition.argument = labelName
                parameter.conditions = New WebpageCondition() {condition}

                Dim criterion As New BiddableAdGroupCriterion()
                criterion.adGroupId = adgroupId
                criterion.criterion = webpage

                ' Set a custom bid for this criterion.
                Dim biddingStrategyConfiguration As New BiddingStrategyConfiguration()

                Dim cpcBid As New CpcBid
                cpcBid.bid = New Money()
                cpcBid.bid.microAmount = 1500000

                biddingStrategyConfiguration.bids = New Bids() {cpcBid}

                criterion.biddingStrategyConfiguration = biddingStrategyConfiguration

                Dim operation As New AdGroupCriterionOperation()
                operation.operand = criterion
                operation.operator = [Operator].ADD

                Try
                    Dim retval As AdGroupCriterionReturnValue = adGroupCriterionService.mutate(
                        New AdGroupCriterionOperation() {operation})
                    Dim newCriterion As BiddableAdGroupCriterion =
                            CType(retval.value(0), BiddableAdGroupCriterion)

                    Console.WriteLine(
                        "Web page criterion with ID = {0} and status = {1} was created.",
                        newCriterion.criterion.id, newCriterion.userStatus)

                    Return newCriterion
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to create webpage criterion for " +
                                                        "custom page feed label.", e)
                End Try
            End Using
        End Function

        ' [END addCustomLabelTargeting] MOE:strip_line
    End Class
End Namespace
