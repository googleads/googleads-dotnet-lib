' Copyright 2013, Google Inc. All Rights Reserved.
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

' Author: thagikura@gmail.com (Takeshi Hagikura)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201302

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201302

  Public Class SiteLinksDataHolder
    Dim siteLinksFeedIdField As Long
    Dim linkTextFeedAttributeIdField As Long
    Dim linkUrlFeedAttributeIdField As Long
    Dim siteLinkFeedItemIdsField As New List(Of Long)

    Public Property SiteLinksFeedId As Long
      Get
        Return siteLinksFeedIdField
      End Get
      Set(ByVal value As Long)
        siteLinksFeedIdField = value
      End Set
    End Property

    Public Property LinkTextFeedAttributeId As Long
      Get
        Return linkTextFeedAttributeIdField
      End Get
      Set(ByVal value As Long)
        linkTextFeedAttributeIdField = value
      End Set
    End Property

    Public Property LinkUrlFeedAttributeId As Long
      Get
        Return linkUrlFeedAttributeIdField
      End Get
      Set(ByVal value As Long)
        linkUrlFeedAttributeIdField = value
      End Set
    End Property

    Public Property SiteLinkFeedItemIds As List(Of Long)
      Get
        Return siteLinkFeedItemIdsField
      End Get
      Set(ByVal value As List(Of Long))
        siteLinkFeedItemIdsField = value
      End Set
    End Property
  End Class

  ''' <summary>
  ''' This code example adds a sitelinks feed and associates it with a campaign.
  ''' To create a campaign, run AddCampaign.cs.
  '''
  ''' Tags: CampaignFeedService.mutate, FeedService.mutate, FeedItemService.mutate,
  ''' Tags: FeedMappingService.mutate
  ''' </summary>
  Public Class AddSiteLinks
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddSiteLinks
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
        Return "This code example adds a sitelinks feed and associates it with a campaign."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign with which sitelinks are associated.
    ''' </param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      Dim siteLinksData As New SiteLinksDataHolder
      createSiteLinksFeed(user, siteLinksData)
      createSiteLinksFeedItems(user, siteLinksData)
      createSiteLinksFeedMapping(user, siteLinksData)
      createSiteLinksCampaignFeed(user, siteLinksData, campaignId)
    End Sub

    Private Sub createSiteLinksFeed( _
      ByVal user As AdWordsUser, ByVal siteLinksData As SiteLinksDataHolder)
      ' Get the FeedService.
      Dim feedService As FeedService = user.GetService(AdWordsService.v201302.FeedService)

      ' Create attributes.
      Dim textAttribute As New FeedAttribute
      textAttribute.type = FeedAttributeType.STRING
      textAttribute.name = "Link Text"
      Dim urlAttribute As New FeedAttribute
      urlAttribute.type = FeedAttributeType.URL
      urlAttribute.name = "Link URL"

      ' Create the feed.
      Dim siteLinksFeed As New Feed
      siteLinksFeed.name = "Feed For Site Links"
      siteLinksFeed.attributes = New FeedAttribute() {textAttribute, urlAttribute}
      siteLinksFeed.origin = FeedOrigin.USER

      ' Create operation.
      Dim operation As New FeedOperation
      operation.operand = siteLinksFeed
      operation.operator = [Operator].ADD

      ' Add the feed.
      Dim result As FeedReturnValue = feedService.mutate(New FeedOperation() {operation})

      Dim savedFeed As Feed = result.value(0)

      siteLinksData.SiteLinksFeedId = savedFeed.id
      Dim savedAttributes As FeedAttribute() = savedFeed.attributes
      siteLinksData.LinkTextFeedAttributeId = savedAttributes(0).id
      siteLinksData.LinkUrlFeedAttributeId = savedAttributes(1).id
      Console.WriteLine("Feed with name {0} and ID {1} with linkTextAttributeId {2}" & _
          "linkUrlAttributeId {3} was created.", savedFeed.name, savedFeed.id, _
          savedAttributes(0).id, savedAttributes(1).id)
    End Sub

    Private Sub createSiteLinksFeedItems( _
        ByVal user As AdWordsUser, ByVal siteLinksData As SiteLinksDataHolder)
      ' Get the FeedItemService.
      Dim feedItemService As FeedItemService = _
        user.GetService(AdWordsService.v201302.FeedItemService)

      ' Create operations to add FeedItems.
      Dim home As FeedItemOperation = _
          newSiteLinkFeedItemAddOperation(siteLinksData, "Home", "http://www.example.com")
      Dim stores As FeedItemOperation = _
          newSiteLinkFeedItemAddOperation(siteLinksData, "Stores", "http://www.example.com/stores")
      Dim onSale As FeedItemOperation = _
          newSiteLinkFeedItemAddOperation(siteLinksData, "On Sale", "http://www.example.com/sale")
      Dim support As FeedItemOperation = _
          newSiteLinkFeedItemAddOperation(siteLinksData, "Support", _
              "http://www.example.com/support")
      Dim products As FeedItemOperation = _
          newSiteLinkFeedItemAddOperation(siteLinksData, "Products", "http://www.example.com/prods")
      Dim aboutUs As FeedItemOperation = _
          newSiteLinkFeedItemAddOperation(siteLinksData, "About Us", "http://www.example.com/about")

      Dim operations As FeedItemOperation() = _
          New FeedItemOperation() {home, stores, onSale, support, products, aboutUs}

      Dim result As FeedItemReturnValue = feedItemService.mutate(operations)
      For Each item As FeedItem In result.value
        Console.WriteLine("FeedItem with feedItemId {0} was added.", item.feedItemId)
        siteLinksData.SiteLinkFeedItemIds.Add(item.feedItemId)
      Next
    End Sub

    ' See the Placeholder reference page for a list of all the placeholder types and fields.
    ' https://developers.google.com/adwords/api/docs/appendix/placeholders.html
    Private Const PLACEHOLDER_SITELINKS As Integer = 1

    ' See the Placeholder reference page for a list of all the placeholder types and fields.
    Private Const PLACEHOLDER_FIELD_SITELINK_LINK_TEXT As Integer = 1
    Private Const PLACEHOLDER_FIELD_SITELINK_URL As Integer = 2

    Private Sub createSiteLinksFeedMapping( _
       ByVal user As AdWordsUser, ByVal siteLinksData As SiteLinksDataHolder)
      ' Get the FeedItemService.
      Dim feedMappingService As FeedMappingService = _
        user.GetService(AdWordsService.v201302.FeedMappingService)

      ' Map the FeedAttributeIds to the fieldId constants.
      Dim linkTextFieldMapping As New AttributeFieldMapping
      linkTextFieldMapping.feedAttributeId = siteLinksData.LinkTextFeedAttributeId
      linkTextFieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_LINK_TEXT
      Dim linkUrlFieldMapping As New AttributeFieldMapping
      linkUrlFieldMapping.feedAttributeId = siteLinksData.LinkUrlFeedAttributeId
      linkUrlFieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_URL

      ' Create the FieldMapping and operation.
      Dim feedMapping As New FeedMapping
      feedMapping.placeholderType = PLACEHOLDER_SITELINKS
      feedMapping.feedId = siteLinksData.SiteLinksFeedId
      feedMapping.attributeFieldMappings = _
          New AttributeFieldMapping() {linkTextFieldMapping, linkUrlFieldMapping}
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

    Private Sub createSiteLinksCampaignFeed(ByVal user As AdWordsUser, _
      ByVal siteLinksData As SiteLinksDataHolder, ByVal campaignId As Long)
      ' Get the CampaignFeedService.
      Dim campaignFeedService As CampaignFeedService = _
        user.GetService(AdWordsService.v201302.CampaignFeedService)

      Dim RequestContextOperand As New RequestContextOperand
      RequestContextOperand.contextType = RequestContextOperandContextType.FEED_ITEM_ID

      Dim functionVariable As New Google.Api.Ads.AdWords.v201302.Function
      functionVariable.lhsOperand = New FunctionArgumentOperand() {RequestContextOperand}
      functionVariable.operator = FunctionOperator.IN

      Dim operands As New List(Of FunctionArgumentOperand)
      For Each feedItemId As Long In siteLinksData.SiteLinkFeedItemIds
        Dim constantOperand As New ConstantOperand
        constantOperand.longValue = feedItemId
        constantOperand.type = ConstantOperandConstantType.LONG
        operands.Add(constantOperand)
      Next

      functionVariable.rhsOperand = operands.ToArray()
      Dim campaignFeed As New CampaignFeed
      campaignFeed.feedId = siteLinksData.SiteLinksFeedId
      campaignFeed.campaignId = campaignId
      campaignFeed.matchingFunction = functionVariable
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

    Function newSiteLinkFeedItemAddOperation(ByVal siteLinksData As SiteLinksDataHolder, _
        ByVal text As String, ByVal url As String) As FeedItemOperation
      ' Create the FeedItemAttributeValues for our text values.
      Dim linkTextAttributeValue As New FeedItemAttributeValue
      linkTextAttributeValue.feedAttributeId = siteLinksData.LinkTextFeedAttributeId
      linkTextAttributeValue.stringValue = text
      Dim linkUrlAttributeValue As New FeedItemAttributeValue
      linkUrlAttributeValue.feedAttributeId = siteLinksData.LinkUrlFeedAttributeId
      linkUrlAttributeValue.stringValue = url

      ' Create the feed item and operation.
      Dim item As New FeedItem
      item.feedId = siteLinksData.SiteLinksFeedId
      item.attributeValues = _
          New FeedItemAttributeValue() {linkTextAttributeValue, linkUrlAttributeValue}
      Dim Operation As New FeedItemOperation
      Operation.operand = item
      Operation.operator = [Operator].ADD
      Return Operation
    End Function
  End Class

End Namespace
