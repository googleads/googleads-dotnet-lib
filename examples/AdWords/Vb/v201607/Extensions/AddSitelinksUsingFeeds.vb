' Copyright 2016, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201607

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201607

  ''' <summary>
  ''' Holds data about sitelinks in a feed.
  ''' </summary>
  Public Class SitelinksDataHolder
    Dim feedIdField As Long
    Dim feedItemIdsField As New List(Of Long)

    Dim linkTextFeedAttributeIdField As Long
    Dim linkFinalUrlFeedAttributeIdField As Long
    Dim line2FeedAttributeIdField As Long
    Dim line3FeedAttributeIdField As Long

    ''' <summary>
    ''' Gets or sets the feed ID.
    ''' </summary>
    Public Property FeedId As Long
      Get
        Return feedIdField
      End Get
      Set(ByVal value As Long)
        feedIdField = value
      End Set
    End Property

    ''' <summary>
    ''' Gets the sitelink feed item IDs.
    ''' </summary>
    Public ReadOnly Property FeedItemIds As List(Of Long)
      Get
        Return feedItemIdsField
      End Get
    End Property

    ''' <summary>
    ''' Gets or sets the link text feed attribute ID.
    ''' </summary>
    Public Property LinkTextFeedAttributeId As Long
      Get
        Return linkTextFeedAttributeIdField
      End Get
      Set(ByVal value As Long)
        linkTextFeedAttributeIdField = value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the link URL feed attribute ID.
    ''' </summary>
    Public Property LinkFinalUrlFeedAttributeId As Long
      Get
        Return linkFinalUrlFeedAttributeIdField
      End Get
      Set(ByVal value As Long)
        linkFinalUrlFeedAttributeIdField = value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the line 2 feed attribute ID.
    ''' </summary>
    Public Property Line2FeedAttributeId As Long
      Get
        Return line2FeedAttributeIdField
      End Get
      Set(ByVal value As Long)
        line2FeedAttributeIdField = value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the line 3 feed attribute ID.
    ''' </summary>
    Public Property Line3FeedAttributeId As Long
      Get
        Return line3FeedAttributeIdField
      End Get
      Set(ByVal value As Long)
        line3FeedAttributeIdField = value
      End Set
    End Property
  End Class

  ''' <summary>
  ''' This code example adds a sitelinks feed and associates it with a campaign.
  ''' To create a campaign, run AddCampaign.cs.
  ''' </summary>
  Public Class AddSitelinksUsingFeeds
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddSitelinksUsingFeeds
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        Dim feedName As String = "INSERT_FEED_NAME_HERE"
        codeExample.Run(New AdWordsUser, campaignId, feedName)
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
        Return "This code example adds a sitelinks feed and associates it with a campaign."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign with which sitelinks are associated.
    ''' </param>
    ''' <param name="feedName">Name of the feed.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long, ByVal feedName As String)
      Dim siteLinksData As New SitelinksDataHolder
      createSitelinksFeed(user, siteLinksData, feedName)
      createSitelinksFeedItems(user, siteLinksData)
      createSitelinksFeedMapping(user, siteLinksData)
      createSitelinksCampaignFeed(user, siteLinksData, campaignId)
    End Sub

    Private Sub createSitelinksFeed( _
      ByVal user As AdWordsUser, ByVal sitelinksData As SitelinksDataHolder, _
      ByVal feedName As String)
      ' Get the FeedService.
      Dim feedService As FeedService = CType(user.GetService( _
          AdWordsService.v201607.FeedService), FeedService)

      ' Create attributes.
      Dim textAttribute As New FeedAttribute
      textAttribute.type = FeedAttributeType.STRING
      textAttribute.name = "Link Text"

      Dim finalUrlAttribute As New FeedAttribute
      finalUrlAttribute.type = FeedAttributeType.URL_LIST
      finalUrlAttribute.name = "Link Final URLs"

      Dim line2Attribute As New FeedAttribute
      line2Attribute.type = FeedAttributeType.STRING
      line2Attribute.name = "Line 2"

      Dim line3Attribute As New FeedAttribute
      line3Attribute.type = FeedAttributeType.STRING
      line3Attribute.name = "Line 3"

      ' Create the feed.
      Dim sitelinksFeed As New Feed
      sitelinksFeed.name = feedName
      sitelinksFeed.attributes = New FeedAttribute() { _
          textAttribute, finalUrlAttribute, line2Attribute, line3Attribute}
      sitelinksFeed.origin = FeedOrigin.USER

      ' Create operation.
      Dim operation As New FeedOperation
      operation.operand = sitelinksFeed
      operation.operator = [Operator].ADD

      ' Add the feed.
      Dim result As FeedReturnValue = feedService.mutate(New FeedOperation() {operation})

      Dim savedFeed As Feed = result.value(0)

      sitelinksData.FeedId = savedFeed.id

      Dim savedAttributes As FeedAttribute() = savedFeed.attributes
      sitelinksData.LinkTextFeedAttributeId = savedAttributes(0).id
      sitelinksData.LinkFinalUrlFeedAttributeId = savedAttributes(1).id
      sitelinksData.Line2FeedAttributeId = savedAttributes(2).id
      sitelinksData.Line3FeedAttributeId = savedAttributes(3).id

      Console.WriteLine("Feed with name {0}, ID {1} with linkTextAttributeId {2}, " & _
          "linkFinalUrlAttributeId {3}, line2AttributeId {4}, line3AttributeId {5} was created.", _
          savedFeed.name, savedFeed.id, savedAttributes(0).id, savedAttributes(1).id, _
          savedAttributes(2).id, savedAttributes(3).id)
    End Sub

    Private Sub createSitelinksFeedItems( _
        ByVal user As AdWordsUser, ByVal sitelinksData As SitelinksDataHolder)
      ' Get the FeedItemService.
      Dim feedItemService As FeedItemService = CType(user.GetService( _
          AdWordsService.v201607.FeedItemService), FeedItemService)

      ' Create operations to add FeedItems.
      Dim home As FeedItemOperation = newSitelinkFeedItemAddOperation(sitelinksData, _
          "Home", "http://www.example.com", "Home line 2", "Home line 3")
      Dim stores As FeedItemOperation = newSitelinkFeedItemAddOperation(sitelinksData, _
          "Stores", "http://www.example.com/stores", "Stores line 2", "Stores line 3")
      Dim onSale As FeedItemOperation = newSitelinkFeedItemAddOperation(sitelinksData, _
          "On Sale", "http://www.example.com/sale", "On Sale line 2", "On sale line 3")
      Dim support As FeedItemOperation = newSitelinkFeedItemAddOperation(sitelinksData, _
          "Support", "http://www.example.com/support", "Support line 2", "Support line 3")
      Dim products As FeedItemOperation = newSitelinkFeedItemAddOperation(sitelinksData, _
          "Products", "http://www.example.com/prods", "Products line 2", "Products line 3")

      ' This site link is using geographical targeting by specifying the
      ' criterion ID for California.
      Dim aboutUs As FeedItemOperation = newSitelinkFeedItemAddOperation(sitelinksData, _
          "About Us", "http://www.example.com/about", "About Us line 2", "About Us line 3", 21137)

      Dim operations As FeedItemOperation() = _
          New FeedItemOperation() {home, stores, onSale, support, products, aboutUs}

      Dim result As FeedItemReturnValue = feedItemService.mutate(operations)

      For Each item As FeedItem In result.value
        Console.WriteLine("FeedItem with feedItemId {0} was added.", item.feedItemId)
        sitelinksData.FeedItemIds.Add(item.feedItemId)
      Next
    End Sub

    ' See the Placeholder reference page for a list of all the placeholder types and fields.
    ' https://developers.google.com/adwords/api/docs/appendix/placeholders.html
    Private Const PLACEHOLDER_SITELINKS As Integer = 1

    ' See the Placeholder reference page for a list of all the placeholder types and fields.
    Private Const PLACEHOLDER_FIELD_SITELINK_LINK_TEXT As Integer = 1
    Private Const PLACEHOLDER_FIELD_SITELINK_FINAL_URL As Integer = 5
    Private Const PLACEHOLDER_FIELD_SITELINK_LINE_2_TEXT As Integer = 3
    Private Const PLACEHOLDER_FIELD_SITELINK_LINE_3_TEXT As Integer = 4

    Private Sub createSitelinksFeedMapping( _
       ByVal user As AdWordsUser, ByVal sitelinksData As SitelinksDataHolder)
      ' Get the FeedItemService.
      Dim feedMappingService As FeedMappingService = CType(user.GetService( _
          AdWordsService.v201607.FeedMappingService), FeedMappingService)

      ' Map the FeedAttributeIds to the fieldId constants.
      Dim linkTextFieldMapping As New AttributeFieldMapping
      linkTextFieldMapping.feedAttributeId = sitelinksData.LinkTextFeedAttributeId
      linkTextFieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_LINK_TEXT

      Dim linkFinalUrlFieldMapping As New AttributeFieldMapping
      linkFinalUrlFieldMapping.feedAttributeId = sitelinksData.LinkFinalUrlFeedAttributeId
      linkFinalUrlFieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_FINAL_URL

      Dim line2FieldMapping As New AttributeFieldMapping
      line2FieldMapping.feedAttributeId = sitelinksData.Line2FeedAttributeId
      line2FieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_LINE_2_TEXT

      Dim line3FieldMapping As New AttributeFieldMapping
      line3FieldMapping.feedAttributeId = sitelinksData.Line3FeedAttributeId
      line3FieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_LINE_3_TEXT

      ' Create the FieldMapping and operation.
      Dim feedMapping As New FeedMapping
      feedMapping.placeholderType = PLACEHOLDER_SITELINKS
      feedMapping.feedId = sitelinksData.FeedId
      feedMapping.attributeFieldMappings = New AttributeFieldMapping() { _
          linkTextFieldMapping, linkFinalUrlFieldMapping, line2FieldMapping, line3FieldMapping}
      _
      Dim operation As New FeedMappingOperation
      operation.operand = feedMapping
      operation.operator = [Operator].ADD

      ' Save the field mapping.
      Dim result As FeedMappingReturnValue = _
          feedMappingService.mutate(New FeedMappingOperation() {operation})

      For Each savedFeedMapping As FeedMapping In result.value
        Console.WriteLine("Feed mapping with ID {0} and placeholderType {1} was saved for " & _
            "feed with ID {2}.", savedFeedMapping.feedMappingId, savedFeedMapping.placeholderType, _
            savedFeedMapping.feedId)
      Next
    End Sub

    Private Sub createSitelinksCampaignFeed(ByVal user As AdWordsUser, _
      ByVal sitelinksData As SitelinksDataHolder, ByVal campaignId As Long)
      ' Get the CampaignFeedService.
      Dim campaignFeedService As CampaignFeedService = CType(user.GetService( _
          AdWordsService.v201607.CampaignFeedService), CampaignFeedService)

      ' Construct a matching function that associates the sitelink feeditems to
      ' the campaign, and set the device preference to Mobile. See the matching
      ' function guide at
      ' https://developers.google.com/adwords/api/docs/guides/feed-matching-functions
      ' for more details.
      Dim matchingFunctionString As String = String.Format( _
          "AND(" & _
          "  IN(FEED_ITEM_ID, {{{0}}})," & _
          "  EQUALS(CONTEXT.DEVICE, 'Mobile')" & _
          ")", _
          String.Join(",", sitelinksData.FeedItemIds))

      Dim campaignFeed As New CampaignFeed()
      campaignFeed.feedId = sitelinksData.FeedId
      campaignFeed.campaignId = campaignId
      campaignFeed.matchingFunction = New [Function]()
      campaignFeed.matchingFunction.functionString = matchingFunctionString

      ' Specifying placeholder types on the CampaignFeed allows the same feed
      ' to be used for different placeholders in different Campaigns.
      campaignFeed.placeholderTypes = New Integer() {PLACEHOLDER_SITELINKS}

      Dim operation As New CampaignFeedOperation
      operation.operand = campaignFeed
      operation.operator = [Operator].ADD

      Dim result As CampaignFeedReturnValue = _
          campaignFeedService.mutate(New CampaignFeedOperation() {operation})

      For Each savedCampaignFeed As CampaignFeed In result.value
        Console.WriteLine("Campaign with ID {0} was associated with feed with ID {1}", _
            savedCampaignFeed.campaignId, savedCampaignFeed.feedId)
      Next
    End Sub

    Function newSitelinkFeedItemAddOperation(ByVal sitelinksData As SitelinksDataHolder, _
        ByVal text As String, ByVal finalUrl As String, ByVal line2 As String, _
        ByVal line3 As String) As FeedItemOperation
      Return newSitelinkFeedItemAddOperation(sitelinksData, text, finalUrl, line2, line3, Nothing)
    End Function

    Function newSitelinkFeedItemAddOperation(ByVal sitelinksData As SitelinksDataHolder, _
        ByVal text As String, ByVal finalUrl As String, ByVal line2 As String, _
        ByVal line3 As String, ByVal locationId As Nullable(Of Long)) _
        As FeedItemOperation
      ' Create the FeedItemAttributeValues for our text values.
      Dim linkTextAttributeValue As New FeedItemAttributeValue
      linkTextAttributeValue.feedAttributeId = sitelinksData.LinkTextFeedAttributeId
      linkTextAttributeValue.stringValue = text

      Dim linkFinalUrlAttributeValue As New FeedItemAttributeValue
      linkFinalUrlAttributeValue.feedAttributeId = sitelinksData.LinkFinalUrlFeedAttributeId
      linkFinalUrlAttributeValue.stringValues = New String() {finalUrl}

      Dim line2AttributeValue As New FeedItemAttributeValue
      line2AttributeValue.feedAttributeId = sitelinksData.Line2FeedAttributeId
      line2AttributeValue.stringValue = line2

      Dim line3AttributeValue As New FeedItemAttributeValue
      line3AttributeValue.feedAttributeId = sitelinksData.Line3FeedAttributeId
      line3AttributeValue.stringValue = line3

      ' Create the feed item and operation.
      Dim item As New FeedItem
      item.feedId = sitelinksData.FeedId

      ' OPTIONAL: Use geographical targeting on a feed item.
      ' The IDs can be found in the documentation or retrieved with the
      ' LocationCriterionService.

      If locationId.HasValue Then
        item.geoTargeting = New Location()
        item.geoTargeting.id = locationId.Value

        ' OPTIONAL: Restrict targeting only to people physically within the location.
        Dim geoTargetingRestriction As New FeedItemGeoRestriction()
        geoTargetingRestriction.geoRestriction = GeoRestriction.LOCATION_OF_PRESENCE
        item.geoTargetingRestriction = geoTargetingRestriction
      End If

      item.attributeValues = New FeedItemAttributeValue() { _
        linkTextAttributeValue, linkFinalUrlAttributeValue, _
        line2AttributeValue, line3AttributeValue}

      Dim operation As New FeedItemOperation
      operation.operand = item
      operation.operator = [Operator].ADD
      Return operation
    End Function
  End Class

End Namespace
