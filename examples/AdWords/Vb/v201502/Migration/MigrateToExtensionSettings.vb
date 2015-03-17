' Copyright 2015, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201502

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201502

  ''' <summary>
  ''' This code example migrates your feed based sitelinks at campaign level to
  ''' use extension settings. To learn more about extensionsettings, see
  ''' https://developers.google.com/adwords/api/docs/guides/extension-settings.
  ''' To learn more about migrating Feed based extensions to extension
  ''' settings, see
  ''' https://developers.google.com/adwords/api/docs/guides/migrate-to-extension-settings.
  '''
  ''' Tags: FeedService.query, FeedMappingService.query, FeedItemService.query
  ''' Tags: CampaignExtensionSettingService.mutate, CampaignFeedService.query
  ''' Tags: CampaignFeedService.mutate
  ''' </summary>
  Public Class MigrateToExtensionSettings
    Inherits ExampleBase

    ''' <summary>
    ''' The placeholder type for sitelinks. See
    ''' https://developers.google.com/adwords/api/docs/appendix/placeholders for
    ''' the list of all supported placeholder types.
    ''' </summary>
    Private Const PLACEHOLDER_TYPE_SITELINKS As Integer = 1

    ''' <summary>
    ''' Holds the placeholder field IDs for sitelinks. See
    ''' https://developers.google.com/adwords/api/docs/appendix/placeholders for
    ''' the list of all supported placeholder types.
    ''' </summary>
    Private Class SiteLinkFields
      Public Const TEXT As Long = 1
      Public Const URL As Long = 2
      Public Const LINE2 As Long = 3
      Public Const LINE3 As Long = 4
      Public Const FINAL_URLS As Long = 5
      Public Const FINAL_MOBILE_URLS As Long = 6
      Public Const TRACKING_URL_TEMPLATE As Long = 7
    End Class

    ''' <summary>
    ''' A sitelink object read from a feed.
    ''' </summary>
    Private Class SiteLinkFromFeed

      ''' <summary>
      ''' The feed ID.
      ''' </summary>
      Private feedIdField As Long

      ''' <summary>
      ''' The feed item ID.
      ''' </summary>
      Private feedItemIdField As Long

      ''' <summary>
      ''' The sitelink text.
      ''' </summary>
      Private textField As String

      ''' <summary>
      ''' The sitelink URL.
      ''' </summary>
      Private urlField As String

      ''' <summary>
      ''' The sitelink final URLs.
      ''' </summary>
      Private finalUrlsField As String()

      ''' <summary>
      ''' The sitelink final Mobile URLs.
      ''' </summary>
      Private finalMobileUrlsField As String()

      ''' <summary>
      ''' The sitelink tracking URL template.
      ''' </summary>
      Private trackingUrlTemplateField As String

      ''' <summary>
      ''' The sitelink line2 details.
      ''' </summary>
      Private line2Field As String

      ''' <summary>
      ''' The sitelink line3 details.
      ''' </summary>
      Private line3Field As String

      ''' <summary>
      ''' The sitelink scheduling details.
      ''' </summary>
      Private schedulingField As FeedItemSchedule()

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
      ''' Gets or sets the feed item ID.
      ''' </summary>
      Public Property FeedItemId As Long
        Get
          Return feedItemIdField
        End Get
        Set(ByVal value As Long)
          feedItemIdField = value
        End Set
      End Property

      ''' <summary>
      ''' Gets or sets the sitelink text.
      ''' </summary>
      Public Property Text As String
        Get
          Return textField
        End Get
        Set(ByVal value As String)
          textField = value
        End Set
      End Property

      ''' <summary>
      ''' Gets or sets the sitelink URL.
      ''' </summary>
      Public Property Url As String
        Get
          Return urlField
        End Get
        Set(ByVal value As String)
          urlField = value
        End Set
      End Property

      ''' <summary>
      ''' Gets or sets the sitelink final URLs.
      ''' </summary>
      Public Property FinalUrls As String()
        Get
          Return finalUrlsField
        End Get
        Set(ByVal value As String())
          finalUrlsField = value
        End Set
      End Property

      ''' <summary>
      ''' Gets or sets the sitelink final Mobile URLs.
      ''' </summary>
      Public Property FinalMobileUrls As String()
        Get
          Return finalMobileUrlsField
        End Get
        Set(ByVal value As String())
          finalMobileUrlsField = value
        End Set
      End Property

      ''' <summary>
      ''' Gets or sets the tracking URL template.
      ''' </summary>
      Public Property TrackingUrlTemplate As String
        Get
          Return trackingUrlTemplateField
        End Get
        Set(ByVal value As String)
          trackingUrlTemplateField = value
        End Set
      End Property

      ''' <summary>
      ''' Gets or sets the sitelink line2 details.
      ''' </summary>
      Public Property Line2 As String
        Get
          Return line2Field
        End Get
        Set(ByVal value As String)
          line2Field = value
        End Set
      End Property

      ''' <summary>
      ''' Gets or sets the sitelink line3 details.
      ''' </summary>
      Public Property Line3 As String
        Get
          Return line3Field
        End Get
        Set(ByVal value As String)
          line3Field = value
        End Set
      End Property

      ''' <summary>
      ''' Gets or sets the sitelink scheduling details.
      ''' </summary>
      Public Property Scheduling As FeedItemSchedule()
        Get
          Return schedulingField
        End Get
        Set(ByVal value As FeedItemSchedule())
          schedulingField = value
        End Set
      End Property
    End Class

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New MigrateToExtensionSettings
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
        Return "This code example migrates your feed based sitelinks at campaign level to " & _
            "use extension settings. To learn more about extensionsettings, see " & _
            "https://developers.google.com/adwords/api/docs/guides/extension-settings. To " & _
            "learn more about migrating Feed based extensions to extension settings, see " & _
            "https://developers.google.com/adwords/api/docs/guides/migrate-to-extension-settings."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get all the feeds from the user account.
      Dim feeds As Feed() = GetFeeds(user)

      For Each feed As Feed In feeds
        ' Retrieve all the sitelinks from the current feed.
        Dim feedItems As Dictionary(Of Long, SiteLinkFromFeed) = GetSiteLinksFromFeed(user, _
                                                                                      feed.id)

        ' Get all the instances where a sitelink from this feed has been added
        ' to a campaign.
        Dim campaignFeeds As CampaignFeed() = GetCampaignFeeds(user, feed, _
                                                               PLACEHOLDER_TYPE_SITELINKS)

        If Not campaignFeeds Is Nothing Then
          Dim allFeedItemsToDelete As New HashSet(Of Long)()

          For Each campaignFeed As CampaignFeed In campaignFeeds
            ' Retrieve the sitelinks that have been associated with this
            ' campaign.
            Dim feedItemIds As List(Of Long) = GetFeedItemsForCampaign(campaignFeed)

            ' Delete the campaign feed that associates the sitelinks from the
            ' feed to the campaign.
            DeleteCampaignFeed(user, campaignFeed)

            ' Create extension settings instead of sitelinks.
            CreateExtensionSetting(user, feedItems, campaignFeed.campaignId, feedItemIds)

            ' Mark the sitelinks from the feed for deletion.
            allFeedItemsToDelete.UnionWith(feedItemIds)
          Next
          ' Delete all the sitelinks from the feed.
          DeleteOldFeedItems(user, New List(Of Long)(allFeedItemsToDelete), feed.id)
        End If
      Next
    End Sub


    ''' <summary>
    ''' Gets the site links from a feed.
    ''' </summary>
    ''' <param name="user">The user that owns the feed.</param>
    ''' <param name="feedId">The feed ID.</param>
    ''' <returns>A dictionary of sitelinks from the feed, with key as the feed
    ''' item ID, and value as the sitelink.</returns>
    Private Function GetSiteLinksFromFeed(ByVal user As AdWordsUser, ByVal feedId As Long) As  _
        Dictionary(Of Long, SiteLinkFromFeed)
      Dim siteLinks As New Dictionary(Of Long, SiteLinkFromFeed)()

      ' Retrieve all the feed items from the feed.
      Dim feedItems As FeedItem() = GetFeedItems(user, feedId)

      ' Retrieve the feed's attribute mapping.
      Dim feedMappings As Dictionary(Of Long, HashSet(Of Long)) = GetFeedMapping(user, feedId,
          PLACEHOLDER_TYPE_SITELINKS)

      If Not feedItems Is Nothing Then
        For Each feedItem As FeedItem In feedItems
          Dim sitelinkFromFeed As New SiteLinkFromFeed()
          sitelinkFromFeed.FeedId = feedItem.feedId
          sitelinkFromFeed.FeedItemId = feedItem.feedItemId

          For Each attributeValue As FeedItemAttributeValue In feedItem.attributeValues
            ' This attribute hasn't been mapped to a field.
            If Not feedMappings.ContainsKey(attributeValue.feedAttributeId) Then
              Continue For
            End If
            ' Get the list of all the fields to which this attribute has been mapped.
            For Each fieldId As Long In feedMappings(attributeValue.feedAttributeId)
              ' Read the appropriate value depending on the ID of the mapped
              ' field.
              Select Case fieldId
                Case SiteLinkFields.TEXT
                  sitelinkFromFeed.Text = attributeValue.stringValue

                Case SiteLinkFields.URL
                  sitelinkFromFeed.Url = attributeValue.stringValue

                Case SiteLinkFields.FINAL_URLS
                  sitelinkFromFeed.FinalUrls = attributeValue.stringValues

                Case SiteLinkFields.FINAL_MOBILE_URLS
                  sitelinkFromFeed.FinalMobileUrls = attributeValue.stringValues

                Case SiteLinkFields.TRACKING_URL_TEMPLATE
                  sitelinkFromFeed.TrackingUrlTemplate = attributeValue.stringValue

                Case SiteLinkFields.LINE2
                  sitelinkFromFeed.Line2 = attributeValue.stringValue

                Case SiteLinkFields.LINE3
                  sitelinkFromFeed.Line3 = attributeValue.stringValue
              End Select
            Next
          Next

          sitelinkFromFeed.Scheduling = feedItem.scheduling
          siteLinks.Add(feedItem.feedItemId, sitelinkFromFeed)
        Next
      End If
      Return siteLinks
    End Function

    ''' <summary>
    ''' Gets the feed mapping for a feed.
    ''' </summary>
    ''' <param name="user">The user that owns the feed.</param>
    ''' <param name="feedId">The feed ID.</param>
    ''' <param name="placeHolderType">Type of the place holder for which feed
    ''' mappings should be retrieved.</param>
    ''' <returns>A dictionary, with key as the feed attribute ID, and value as
    ''' the set of all fields which the attribute has a mapping to.</returns>
    Private Function GetFeedMapping(ByVal user As AdWordsUser, ByVal feedId As Long, _
                                    ByVal placeHolderType As Long) _
                                    As Dictionary(Of Long, HashSet(Of Long))
      Dim feedMappingService As FeedMappingService = DirectCast(user.GetService( _
          AdWordsService.v201502.FeedMappingService), FeedMappingService)
      Dim page As FeedMappingPage = feedMappingService.query(String.Format( _
          "SELECT FeedMappingId, AttributeFieldMappings where FeedId='{0}' and " & _
          "PlaceholderType={1} and Status='ENABLED'", feedId, placeHolderType))

      Dim attributeMappings As New Dictionary(Of Long, HashSet(Of Long))()

      If Not (page.entries Is Nothing) Then
        ' Normally, a feed attribute is mapped only to one field. However,
        ' you may map it to more than one field if needed.
        For Each feedMapping As FeedMapping In page.entries
          For Each attributeMapping As AttributeFieldMapping In feedMapping.attributeFieldMappings
            If Not attributeMappings.ContainsKey(attributeMapping.feedAttributeId) Then
              attributeMappings(attributeMapping.feedAttributeId) = New HashSet(Of Long)()
            End If
            attributeMappings(attributeMapping.feedAttributeId).Add(attributeMapping.fieldId)
          Next
        Next
      End If
      Return attributeMappings
    End Function

    ''' <summary>
    ''' Gets the feeds.
    ''' </summary>
    ''' <param name="user">The user for which feeds are retrieved.</param>
    ''' <returns>The list of feeds.</returns>
    Private Function GetFeeds(ByVal user As AdWordsUser) As Feed()
      Dim feedService As FeedService = DirectCast(user.GetService( _
          AdWordsService.v201502.FeedService), FeedService)
      Dim page As FeedPage = feedService.query("SELECT Id, Name, Attributes where " & _
          "Origin='USER' and FeedStatus='ENABLED'")
      Return page.entries
    End Function

    ''' <summary>
    ''' Gets the feed items in a feed.
    ''' </summary>
    ''' <param name="user">The user that owns the feed.</param>
    ''' <param name="feedId">The feed ID.</param>
    ''' <returns>The list of feed items in the feed.</returns>
    Private Function GetFeedItems(ByVal user As AdWordsUser, ByVal feedId As Long) As FeedItem()
      Dim feedItemService As FeedItemService = DirectCast(user.GetService( _
          AdWordsService.v201502.FeedItemService), FeedItemService)
      Dim page As FeedItemPage = feedItemService.query(String.Format("Select FeedItemId, " & _
          "AttributeValues, Scheduling  where Status = 'ENABLED' and FeedId = '{0}'", feedId))
      Return page.entries
    End Function

    ''' <summary>
    ''' Deletes the old feed items for which extension settings have been
    ''' created.
    ''' </summary>
    ''' <param name="user">The user that owns the feed items.</param>
    ''' <param name="feedItemIds">IDs of the feed items to be removed.</param>
    ''' <param name="feedId">ID of the feed that holds the feed items.</param>
    Private Sub DeleteOldFeedItems(ByVal user As AdWordsUser, ByVal feedItemIds As List(Of Long), _
                                   ByVal feedId As Long)
      If feedItemIds.Count = 0 Then
        Return
      End If
      Dim operations As New List(Of FeedItemOperation)()
      For Each feedItemId As Long In feedItemIds
        Dim operation As New FeedItemOperation()
        operation.operator = [Operator].REMOVE

        operation.operand = New FeedItem()
        operation.operand.feedItemId = feedItemId
        operation.operand.feedId = feedId

        operations.Add(operation)
      Next
      Dim feedItemService As FeedItemService = DirectCast(user.GetService( _
          AdWordsService.v201502.FeedItemService), FeedItemService)
      feedItemService.mutate(operations.ToArray())
      Return
    End Sub

    ''' <summary>
    ''' Creates the extension setting fo a list of feed items.
    ''' </summary>
    ''' <param name="user">The user for which extension settings are created.
    ''' </param>
    ''' <param name="feedItems">The list of all feed items.</param>
    ''' <param name="campaignId">ID of the campaign to which extension settings
    ''' are added.</param>
    ''' <param name="feedItemIds">IDs of the feed items for which extension
    ''' settings should be created.</param>
    Private Sub CreateExtensionSetting(ByVal user As AdWordsUser, ByVal feedItems As  _
                                       Dictionary(Of Long, SiteLinkFromFeed), _
                                       ByVal campaignId As Long, _
                                       ByVal feedItemIds As List(Of Long))
      Dim extensionSetting As New CampaignExtensionSetting()
      extensionSetting.campaignId = campaignId
      extensionSetting.extensionType = FeedType.SITELINK
      extensionSetting.extensionSetting = New ExtensionSetting()

      Dim extensionFeedItems As New List(Of ExtensionFeedItem)()

      For Each feedItemId As Long In feedItemIds
        Dim feedItem As SiteLinkFromFeed = feedItems(feedItemId)

        Dim newFeedItem As New SitelinkFeedItem()
        newFeedItem.sitelinkText = feedItem.Text
        newFeedItem.sitelinkUrl = feedItem.Url
        newFeedItem.sitelinkFinalUrls = feedItem.FinalUrls
        newFeedItem.sitelinkFinalMobileUrls = feedItem.FinalMobileUrls
        newFeedItem.sitelinkTrackingUrlTemplate = feedItem.TrackingUrlTemplate
        newFeedItem.sitelinkLine2 = feedItem.Line2
        newFeedItem.sitelinkLine3 = feedItem.Line3
        newFeedItem.scheduling = feedItem.Scheduling

        extensionFeedItems.Add(newFeedItem)
      Next
      extensionSetting.extensionSetting.extensions = extensionFeedItems.ToArray()
      extensionSetting.extensionType = FeedType.SITELINK

      Dim campaignExtensionSettingService As CampaignExtensionSettingService = _
          DirectCast(user.GetService(AdWordsService.v201502.CampaignExtensionSettingService),  _
            CampaignExtensionSettingService)

      Dim operation As New CampaignExtensionSettingOperation()
      operation.operand = extensionSetting
      operation.operator = [Operator].ADD

      campaignExtensionSettingService.mutate(New CampaignExtensionSettingOperation() {operation})
      Return
    End Sub

    ''' <summary>
    ''' Deletes a campaign feed.
    ''' </summary>
    ''' <param name="user">The user.</param>
    ''' <param name="campaignFeed">The campaign feed.</param>
    ''' <returns></returns>
    Private Function DeleteCampaignFeed(ByVal user As AdWordsUser, _
                                        ByVal campaignFeed As CampaignFeed) As CampaignFeed
      Dim campaignFeedService As CampaignFeedService = DirectCast(user.GetService( _
          AdWordsService.v201502.CampaignFeedService), CampaignFeedService)

      Dim operation As New CampaignFeedOperation()
      operation.operand = campaignFeed
      operation.operator = [Operator].REMOVE

      Return campaignFeedService.mutate(New CampaignFeedOperation() {operation}).value(0)
    End Function

    ''' <summary>
    ''' Gets the list of feed items that are used by a campaign through a given
    ''' campaign feed.
    ''' </summary>
    ''' <param name="campaignFeed">The campaign feed.</param>
    ''' <returns>The list of feed items.</returns>
    Private Function GetFeedItemsForCampaign(ByVal campaignFeed As CampaignFeed) As List(Of Long)
      Dim feedItems As New List(Of Long)()
      If (campaignFeed.matchingFunction.lhsOperand.Length = 1) AndAlso _
        (TypeOf campaignFeed.matchingFunction.lhsOperand(0) Is RequestContextOperand) AndAlso _
        (DirectCast(campaignFeed.matchingFunction.lhsOperand(0),  _
            RequestContextOperand).contextType = _
                RequestContextOperandContextType.FEED_ITEM_ID) AndAlso _
        (campaignFeed.matchingFunction.operator = FunctionOperator.IN) Then

        For Each argument As ConstantOperand In campaignFeed.matchingFunction.rhsOperand
          feedItems.Add(argument.longValue)
        Next
      End If
      Return feedItems
    End Function

    ''' <summary>
    ''' Gets the campaignfeeds that use a particular feed.
    ''' </summary>
    ''' <param name="user">The user that owns the feed.</param>
    ''' <param name="feed">The feed for which campaign feeds should be
    ''' retrieved.</param>
    ''' <param name="placeholderType">The type of placeholder to restrict
    ''' search.</param>
    ''' <returns>The list of campaignfeeds.</returns>
    Private Function GetCampaignFeeds(ByVal user As AdWordsUser, ByVal feed As Feed, _
                                      ByVal placeholderType As Integer) As CampaignFeed()
      Dim campaignFeedService As CampaignFeedService = DirectCast(user.GetService( _
          AdWordsService.v201502.CampaignFeedService), CampaignFeedService)

      Dim page As CampaignFeedPage = campaignFeedService.query(String.Format( _
          "SELECT CampaignId, MatchingFunction, PlaceholderTypes where Status='ENABLED' " & _
          "and FeedId = '{0}' and PlaceholderTypes CONTAINS_ANY[{1}]", feed.id, placeholderType))
      Return page.entries
    End Function
  End Class
End Namespace
