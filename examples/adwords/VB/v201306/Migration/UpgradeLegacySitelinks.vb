' Copyright 2013, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201306

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201306
  ''' <summary>
  ''' This code example migrates legacy sitelinks to upgraded sitelinks for a
  ''' given list of campaigns. The campaigns must be upgraded to enhanced
  ''' campaigns before you can run this example. To upgrade a campaign to
  ''' enhanced, run CampaignManagement/SetCampaignEnhanced.vb. To get all
  ''' campaigns, run BasicOperations/GetCampaigns.vb.
  '''
  ''' Tags: CampaignAdExtensionService.get, CampaignAdExtensionService.mutate
  ''' Tags: FeedService.mutate, FeedItemService.mutate
  ''' Tags: FeedMappingService.mutate, CampaignFeedService.mutate
  ''' </summary>
  Public Class UpgradeLegacySitelinks
    Inherits ExampleBase

    ''' <summary>
    ''' Data structure to hold details about a Sitelink feed.
    ''' </summary>
    Private Class SiteLinksFeed
      ''' <summary>
      ''' Feed id.
      ''' </summary>
      Dim siteLinksFeedIdField As Long

      ''' <summary>
      ''' Attribute id for sitelink text.
      ''' </summary>
      Dim linkTextFeedAttributeIdField As Long

      ''' <summary>
      ''' Attribute id for sitelink url.
      ''' </summary>
      Dim linkUrlFeedAttributeIdField As Long

      ''' <summary>
      ''' Gets or sets the attribute id for sitelink text.
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
      ''' Gets or sets the site links feed id.
      ''' </summary>
      Public Property SiteLinksFeedId As Long
        Get
          Return siteLinksFeedIdField
        End Get
        Set(ByVal value As Long)
          siteLinksFeedIdField = value
        End Set
      End Property

      ''' <summary>
      ''' Gets or sets the attribute id for sitelink url.
      ''' </summary>
      Public Property LinkUrlFeedAttributeId As Long
        Get
          Return linkUrlFeedAttributeIdField
        End Get
        Set(ByVal value As Long)
          linkUrlFeedAttributeIdField = value
        End Set
      End Property
    End Class

    ' See https://developers.google.com/adwords/api/docs/appendix/placeholders
    ' for a list of all the placeholder types and fields.
    Private Const PLACEHOLDER_SITELINKS As Integer = 1

    ' See https://developers.google.com/adwords/api/docs/appendix/placeholders
    ' for a list of all the placeholder types and fields.
    Private Const PLACEHOLDER_FIELD_SITELINK_LINK_TEXT As Integer = 1
    Private Const PLACEHOLDER_FIELD_SITELINK_URL As Integer = 2


    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New UpgradeLegacySitelinks
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        codeExample.Run(New AdWordsUser, New Long() {campaignId})
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
        Return "This code example migrates legacy sitelinks to upgraded sitelinks for a given " & _
            "list of campaigns. The campaigns must be upgraded to enhanced campaigns before " & _
            "you can run this example. To upgrade a campaign to enhanced, run " & _
            "CampaignManagement/SetCampaignEnhanced.vb. To get all campaigns, run " & _
            "BasicOperations/GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignIds">Ids of the campaign for which sitelinks are
    ''' upgraded.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignIds As Long())
      ' Get the CampaignAdExtensionService.
      Dim campaignAdExtensionService As CampaignAdExtensionService = user.GetService( _
              AdWordsService.v201306.CampaignAdExtensionService)
      ' Get the FeedMappingService.
      Dim feedMappingService As FeedMappingService = user.GetService( _
          AdWordsService.v201306.FeedMappingService)
      ' Get the FeedService.
      Dim feedService As FeedService = user.GetService(AdWordsService.v201306.FeedService)
      ' Get the FeedItemService.
      Dim feedItemService As FeedItemService = user.GetService( _
          AdWordsService.v201306.FeedItemService)
      ' Get the CampaignFeedService.
      Dim campaignFeedService As CampaignFeedService = user.GetService( _
          AdWordsService.v201306.CampaignFeedService)

      ' Try to retrieve an existing feed that has been mapped for use with
      ' sitelinks. if multiple such feeds exist, the first matching feed is
      ' retrieved. You could modify this code example to retrieve all the feeds
      ' and pick the appropriate feed based on user input.
      Dim siteLinksFeed As SiteLinksFeed = getExistingFeed(feedMappingService)

      If siteLinksFeed Is Nothing Then
        ' Create a feed for storing sitelinks.
        siteLinksFeed = createSiteLinksFeed(feedService)

        ' Map the feed for using with sitelinks.
        createSiteLinksFeedMapping(feedMappingService, siteLinksFeed)
      End If

      For Each campaignId As Long In campaignIds
        ' Get legacy sitelinks for the campaign.
        Dim extension As CampaignAdExtension = _
            getLegacySitelinksForCampaign(campaignAdExtensionService, campaignId)
        If Not extension Is Nothing Then
          ' Get the sitelinks.
          Dim legacySitelinks As Sitelink() = _
              DirectCast(extension.adExtension, SitelinksExtension).sitelinks

          ' Add the sitelinks to the feed.
          Dim siteLinkFeedItemIds As List(Of Long) = _
              createSiteLinkFeedItems(feedItemService, siteLinksFeed, legacySitelinks)

          ' Associate feeditems to the campaign.
          associateSitelinkFeedItemsWithCampaign(campaignFeedService, siteLinksFeed, _
                                                 siteLinkFeedItemIds, campaignId)

          ' Once the upgraded sitelinks are added to a campaign, the legacy
          ' sitelinks will stop serving. You can delete the legacy sitelinks
          ' once you have verified that the migration went fine. In case the
          ' migration didn't succeed, you can roll back the migration by deleting
          ' the CampaignFeed you created in the previous step.
          deleteLegacySitelinks(campaignAdExtensionService, extension)
        End If
      Next
    End Sub

    ''' <summary>
    ''' Retrieve an existing feed that is mapped to hold sitelinks. The first
    ''' active sitelinks feed is retrieved by this method.
    ''' </summary>
    ''' <param name="feedMappingService">The feed mapping service.</param>
    ''' <returns>A SiteLinksFeed if a feed is found, or null otherwise.</returns>
    Private Shared Function getExistingFeed(ByVal feedMappingService As FeedMappingService) _
        As SiteLinksFeed
      Dim selector As New Selector()
      selector.fields = New String() {"FeedId", "FeedMappingId", "PlaceholderType", "Status", _
        "AttributeFieldMappings"}

      Dim placeHolderPredicate As New Predicate()
      placeHolderPredicate.field = "PlaceholderType"
      placeHolderPredicate.[operator] = PredicateOperator.EQUALS
      placeHolderPredicate.values = New String() {PLACEHOLDER_SITELINKS.ToString()}

      Dim statusPredicate As New Predicate()
      statusPredicate.field = "Status"
      statusPredicate.[operator] = PredicateOperator.EQUALS
      statusPredicate.values = New String() {"ACTIVE"}

      selector.predicates = New Predicate() {placeHolderPredicate, statusPredicate}

      Dim page As FeedMappingPage = feedMappingService.get(selector)

      If page IsNot Nothing AndAlso page.entries IsNot Nothing AndAlso _
          (page.entries.Length > 0) Then
        For Each feedMapping As FeedMapping In page.entries
          Dim feedId As Nullable(Of Long) = feedMapping.feedId
          Dim textAttributeId As Nullable(Of Long)
          Dim urlAttributeId As Nullable(Of Long)
          For Each attributeMapping As AttributeFieldMapping In feedMapping.attributeFieldMappings
            If attributeMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_LINK_TEXT Then
              textAttributeId = attributeMapping.feedAttributeId
            ElseIf attributeMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_URL Then
              urlAttributeId = attributeMapping.feedAttributeId
            End If
          Next

          If feedId.HasValue AndAlso textAttributeId.HasValue AndAlso urlAttributeId.HasValue Then
            Dim siteLinksFeed As New SiteLinksFeed()
            siteLinksFeed.SiteLinksFeedId = feedId
            siteLinksFeed.LinkTextFeedAttributeId = textAttributeId.Value
            siteLinksFeed.LinkUrlFeedAttributeId = urlAttributeId.Value
            Return siteLinksFeed
          End If
        Next
      End If
      Return Nothing
    End Function

    ''' <summary>
    ''' Create a feed for holding upgraded sitelinks.
    ''' </summary>
    ''' <param name="feedService">The feed service.</param>
    ''' <returns>A SiteLinksFeed for holding the sitelinks.</returns>
    Private Shared Function createSiteLinksFeed(ByVal feedService As FeedService) As SiteLinksFeed
      Dim siteLinksData As New SiteLinksFeed()

      ' Create attributes.
      Dim textAttribute As New FeedAttribute()
      textAttribute.type = FeedAttributeType.STRING
      textAttribute.name = "Link Text"

      Dim urlAttribute As New FeedAttribute()
      urlAttribute.type = FeedAttributeType.URL
      urlAttribute.name = "Link URL"

      ' Create the feed.
      Dim siteLinksFeed As New Feed()
      siteLinksFeed.name = "Feed For Sitelinks"
      siteLinksFeed.attributes = New FeedAttribute() {textAttribute, urlAttribute}
      siteLinksFeed.origin = FeedOrigin.USER

      ' Create operation.
      Dim operation As New FeedOperation()
      operation.operand = siteLinksFeed
      operation.[operator] = [Operator].ADD

      ' Add the feed.
      Dim result As FeedReturnValue = feedService.mutate(New FeedOperation() {operation})

      Dim savedFeed As Feed = result.value(0)
      siteLinksData.SiteLinksFeedId = savedFeed.id
      Dim savedAttributes As FeedAttribute() = savedFeed.attributes
      siteLinksData.LinkTextFeedAttributeId = savedAttributes(0).id
      siteLinksData.LinkUrlFeedAttributeId = savedAttributes(1).id
      Return siteLinksData
    End Function

    ''' <summary>
    ''' Map the feed for use with Sitelinks.
    ''' </summary>
    ''' <param name="feedMappingService">The feed mapping service.</param>
    ''' <param name="siteLinksFeed">The feed for holding sitelinks.</param>
    Private Shared Sub createSiteLinksFeedMapping(ByVal feedMappingService As FeedMappingService, _
        ByVal siteLinksFeed As SiteLinksFeed)
      ' Map the FeedAttributeIds to the fieldId constants.
      Dim linkTextFieldMapping As New AttributeFieldMapping()
      linkTextFieldMapping.feedAttributeId = siteLinksFeed.LinkTextFeedAttributeId
      linkTextFieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_LINK_TEXT
      Dim linkUrlFieldMapping As New AttributeFieldMapping()
      linkUrlFieldMapping.feedAttributeId = siteLinksFeed.LinkUrlFeedAttributeId
      linkUrlFieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_URL

      ' Create the FieldMapping and operation.
      Dim feedMapping As New FeedMapping()
      feedMapping.placeholderType = PLACEHOLDER_SITELINKS
      feedMapping.feedId = siteLinksFeed.SiteLinksFeedId
      feedMapping.attributeFieldMappings = _
          New AttributeFieldMapping() {linkTextFieldMapping, linkUrlFieldMapping}
      Dim operation As New FeedMappingOperation()
      operation.operand = feedMapping
      operation.[operator] = [Operator].ADD

      ' Save the field mapping.
      feedMappingService.mutate(New FeedMappingOperation() {operation})
    End Sub

    ''' <summary>
    ''' Gets the legacy sitelinks for campaign.
    ''' </summary>
    ''' <param name="campaignExtensionService">The campaign extension service.</param>
    ''' <param name="campaignId">The campaign id.</param>
    ''' <returns>The CampaignAdExtension that contains the legacy sitelinks, or
    ''' null if there are no legacy sitelinks in this campaign.</returns>
    Private Shared Function getLegacySitelinksForCampaign( _
        ByVal campaignExtensionService As CampaignAdExtensionService, ByVal campaignId As Long) _
        As CampaignAdExtension
      ' Create the selector.
      Dim selector As New Selector()
      selector.fields = New String() {"CampaignId", "AdExtensionId", "Status", "DisplayText", _
        "DestinationUrl"}

      ' Filter the results for specified campaign id.
      Dim campaignPredicate As New Predicate()
      campaignPredicate.[operator] = PredicateOperator.EQUALS
      campaignPredicate.field = "CampaignId"
      campaignPredicate.values = New String() {campaignId.ToString()}

      ' Filter the results for active campaign ad extensions. You may add
      ' additional filtering conditions here as required.
      Dim statusPredicate As New Predicate()
      statusPredicate.[operator] = PredicateOperator.EQUALS
      statusPredicate.field = "Status"
      statusPredicate.values = New String() {CampaignAdExtensionStatus.ACTIVE.ToString()}

      ' Filter for sitelinks ad extension type.
      Dim typePredicate As New Predicate()
      typePredicate.[operator] = PredicateOperator.EQUALS
      typePredicate.field = "AdExtensionType"
      typePredicate.values = New String() {"SITELINKS_EXTENSION"}

      selector.predicates = New Predicate() {campaignPredicate, statusPredicate, typePredicate}

      Dim page As CampaignAdExtensionPage = campaignExtensionService.get(selector)
      If page.entries IsNot Nothing AndAlso (page.entries.Length > 0) Then
        Return page.entries(0)
      Else
        Return Nothing
      End If
    End Function

    ''' <summary>
    ''' Add legacy sitelinks to the sitelinks feed.
    ''' </summary>
    ''' <param name="feedItemService">The feed item service.</param>
    ''' <param name="siteLinksFeed">The feed for adding sitelinks.</param>
    ''' <param name="sitelinks">The list of legacy sitelinks to be added to the
    ''' feed.</param>
    ''' <returns>The list of feeditems that were added to the feed.</returns>
    Private Shared Function createSiteLinkFeedItems(ByVal feedItemService As FeedItemService, _
        ByVal siteLinksFeed As SiteLinksFeed, ByVal sitelinks As Sitelink()) As List(Of Long)
      Dim siteLinkFeedItemIds As New List(Of Long)()

      ' Create operation for adding each legacy sitelink to the sitelinks feed.
      Dim feedItemOperations As New List(Of FeedItemOperation)()

      For Each sitelink As Sitelink In sitelinks
        Dim operation As FeedItemOperation = newSiteLinkFeedItemAddOperation( _
            siteLinksFeed, sitelink.displayText, sitelink.destinationUrl)
        feedItemOperations.Add(operation)
      Next

      Dim result As FeedItemReturnValue = feedItemService.mutate(feedItemOperations.ToArray())

      ' Retrieve the feed item ids.
      For Each item As FeedItem In result.value
        siteLinkFeedItemIds.Add(item.feedItemId)
      Next
      Return siteLinkFeedItemIds
    End Function

    ''' <summary>
    ''' Creates a new operation for adding a feed item.
    ''' </summary>
    ''' <param name="siteLinksFeed">The site links feed.</param>
    ''' <param name="text">The sitelinks text.</param>
    ''' <param name="url">The sitelinks URL.</param>
    ''' <returns>A FeedItemOperation for adding the feed item.</returns>
    Private Shared Function newSiteLinkFeedItemAddOperation(ByVal siteLinksFeed As SiteLinksFeed, _
        ByVal text As String, ByVal url As String) As FeedItemOperation
      ' Create the FeedItemAttributeValues for our text values.
      Dim linkTextAttributeValue As New FeedItemAttributeValue()
      linkTextAttributeValue.feedAttributeId = siteLinksFeed.LinkTextFeedAttributeId
      linkTextAttributeValue.stringValue = text
      Dim linkUrlAttributeValue As New FeedItemAttributeValue()
      linkUrlAttributeValue.feedAttributeId = siteLinksFeed.LinkUrlFeedAttributeId
      linkUrlAttributeValue.stringValue = url

      ' Create the feed item and operation.
      Dim item As New FeedItem()
      item.feedId = SiteLinksFeed.SiteLinksFeedId
      item.attributeValues = _
          New FeedItemAttributeValue() {linkTextAttributeValue, linkUrlAttributeValue}
      Dim operation As New FeedItemOperation()
      operation.operand = item
      operation.[operator] = [Operator].ADD
      Return operation
    End Function

    ''' <summary>
    ''' Delete legacy sitelinks from a campaign.
    ''' </summary>
    ''' <param name="campaignExtensionService">The campaign extension service.
    ''' </param>
    ''' <param name="extensionToDelete">The CampaignAdExtension that holds
    ''' legacy sitelinks.</param>
    Private Shared Sub deleteLegacySitelinks( _
        ByVal campaignExtensionService As CampaignAdExtensionService, _
        ByVal extensionToDelete As CampaignAdExtension)
      Dim operation As New CampaignAdExtensionOperation()
      operation.[operator] = [Operator].REMOVE
      operation.operand = extensionToDelete

      campaignExtensionService.mutate(New CampaignAdExtensionOperation() {operation})
    End Sub

    ''' <summary>
    ''' Associates the sitelink feed items with a campaign.
    ''' </summary>
    ''' <param name="campaignFeedService">The campaign feed service.</param>
    ''' <param name="siteLinksFeed">The feed for holding the sitelinks.</param>
    ''' <param name="siteLinkFeedItemIds">The list of feed item ids to be
    ''' associated with a campaign as sitelinks.</param>
    ''' <param name="campaignId">The campaign id to which upgraded sitelinks are
    ''' added.</param>
    Private Shared Sub associateSitelinkFeedItemsWithCampaign( _
        ByVal campaignFeedService As CampaignFeedService, ByVal siteLinksFeed As SiteLinksFeed, _
        ByVal siteLinkFeedItemIds As List(Of Long), ByVal campaignId As Long)
      ' Create a custom matching function that matches the given feed items to
      ' the campaign.
      Dim requestContextOperand As New RequestContextOperand()
      requestContextOperand.contextType = RequestContextOperandContextType.FEED_ITEM_ID

      Dim matchingFunction As New [Function]()
      matchingFunction.lhsOperand = New FunctionArgumentOperand() {requestContextOperand}
      matchingFunction.[operator] = FunctionOperator.IN

      Dim operands As New List(Of FunctionArgumentOperand)()
      For Each feedItemId As Long In siteLinkFeedItemIds
        Dim constantOperand As New ConstantOperand()
        constantOperand.longValue = feedItemId
        constantOperand.type = ConstantOperandConstantType.LONG
        operands.Add(constantOperand)
      Next
      matchingFunction.rhsOperand = operands.ToArray()

      ' Create upgraded sitelinks for the campaign. Use the sitelinks feed we
      ' created, and restrict feed items by matching function.
      Dim campaignFeed As New CampaignFeed()
      campaignFeed.feedId = siteLinksFeed.SiteLinksFeedId
      campaignFeed.campaignId = campaignId
      campaignFeed.matchingFunction = matchingFunction
      campaignFeed.placeholderTypes = New Integer() {PLACEHOLDER_SITELINKS}

      Dim operation As New CampaignFeedOperation()
      operation.operand = campaignFeed
      operation.[operator] = [Operator].ADD
      campaignFeedService.mutate(New CampaignFeedOperation() {Operation})
    End Sub
  End Class
End Namespace
