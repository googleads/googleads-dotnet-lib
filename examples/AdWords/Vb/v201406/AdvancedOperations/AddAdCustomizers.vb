' Copyright 2014, Google Inc. All Rights Reserved.
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

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201406

Imports System
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.v201406
  ''' <summary>
  ''' A container for metadata related to an ad customizers feed.
  ''' </summary>
  Public Class CustomizersDataHolder

    ''' <summary>
    ''' The feed ID.
    ''' </summary>
    Private feedIdField As Long

    ''' <summary>
    ''' The name feed attribute ID.
    ''' </summary>
    Private nameFeedAttributeIdField As Long

    ''' <summary>
    ''' The price feed attribute ID.
    ''' </summary>
    Private priceFeedAttributeIdField As Long

    ''' <summary>
    ''' The date feed attribute ID.
    ''' </summary>
    Private dateFeedAttributeIdField As Long

    ''' <summary>
    ''' The feed item IDs.
    ''' </summary>
    Private feedItemIdsField As New List(Of Long)

    ''' <summary>
    ''' Gets or sets the feed ID.
    ''' </summary>
    Public Property FeedId As Long
      Get
        Return feedIdField
      End Get
      Set(value As Long)
        feedIdField = value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name feed attribute identifier.
    ''' </summary>
    Public Property NameFeedAttributeId As Long
      Get
        Return nameFeedAttributeIdField
      End Get
      Set(value As Long)
        nameFeedAttributeIdField = value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the price feed attribute ID.
    ''' </summary>
    Public Property PriceFeedAttributeId As Long
      Get
        Return priceFeedAttributeIdField
      End Get
      Set(value As Long)
        priceFeedAttributeIdField = value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the date feed attribute ID.
    ''' </summary>
    Public Property DateFeedAttributeId As Long
      Get
        Return dateFeedAttributeIdField
      End Get
      Set(value As Long)
        dateFeedAttributeIdField = value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the feed item IDs.
    ''' </summary>
    Public Property FeedItemIds As List(Of Long)
      Get
        Return feedItemIdsField
      End Get
      Set(value As List(Of Long))
        feedItemIdsField = value
      End Set
    End Property
  End Class

  ''' <summary>
  ''' This code example adds an ad customizer feed and associates it with the
  ''' customer. Then it adds an ad in two different adgroups that uses the
  ''' feed to populate dynamic data.
  '''
  ''' Tags: CustomerFeedService.mutate, FeedItemService.mutate
  ''' Tags: FeedMappingService.mutate
  ''' Tags: FeedService.mutate, AdGroupAdService.mutate
  ''' </summary>
  Public Class AddAdCustomizers
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddAdCustomizers
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId1 As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        Dim adGroupId2 As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        codeExample.Run(New AdWordsUser(), adGroupId1, adGroupId2)
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
        Return "This code example adds an ad customizer feed and associates it with the " & _
            "customer. Then it adds an ad in two different adgroups that uses the feed to " & _
            "populate dynamic data."
      End Get
    End Property

    ' See the Placeholder reference page for a list of all the placeholder
    ' types and fields.
    ' https://developers.google.com/adwords/api/docs/appendix/placeholders
    Private Const PLACEHOLDER_AD_CUSTOMIZER As Integer = 10

    Private Const PLACEHOLDER_FIELD_PRICE As Integer = 3
    Private Const PLACEHOLDER_FIELD_DATE As Integer = 4
    Private Const PLACEHOLDER_FIELD_STRING As Integer = 5

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId1">Id of the first adgroup to which ads with ad
    ''' customizers are added.</param>
    ''' <param name="adGroupId2">Id of the second adgroup to which ads with ad
    ''' customizers are added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId1 As Long, ByVal adGroupId2 As Long)
      ' Create a customizer feed. One feed per account can be used for all ads.
      Dim dataHolder As CustomizersDataHolder = CreateCustomizerFeed(user)

      ' Create a feed mapping to map the fields with customizer IDs.
      CreateFeedMapping(user, dataHolder)

      ' Add feed items containing the values we'd like to place in ads.
      CreateCustomizerFeedItems(user, New Long() {adGroupId1, adGroupId2}, dataHolder)

      ' Create a customer (account-level) feed with a matching function that
      ' determines when to use this feed. For this case we use the "IDENTITY"
      ' matching function that is always true just to associate this feed with
      ' the customer. The targeting is done within the feed items using the
      ' campaignTargeting, adGroupTargeting, or keywordTargeting attributes.
      CreateCustomerFeed(user, dataHolder)

      ' All set! We can now create ads with customizations.
      CreateAdsWithCustomizations(user, New Long() {adGroupId1, adGroupId2})
    End Sub

    ''' <summary>
    ''' Creates a new Feed for ad customizers.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <returns>A new CustomizersDataHolder, populated with the feed ID and
    ''' attribute IDs of the new Feed.</returns>
    Private Shared Function CreateCustomizerFeed(ByVal user As AdWordsUser) _
        As CustomizersDataHolder
      ' Get the FeedService.
      Dim feedService As FeedService = CType(user.GetService( _
          AdWordsService.v201406.FeedService), FeedService)

      Dim customizerFeed As New Feed
      customizerFeed.name = "CustomizerFeed"

      Dim nameAttribute As New FeedAttribute
      nameAttribute.name = "Name"
      nameAttribute.type = FeedAttributeType.STRING

      Dim priceAttribute As New FeedAttribute
      priceAttribute.name = "Price"
      priceAttribute.type = FeedAttributeType.STRING

      Dim dateAttribute As New FeedAttribute
      dateAttribute.name = "Date"
      dateAttribute.type = FeedAttributeType.DATE_TIME

      customizerFeed.attributes = New FeedAttribute() { _
        nameAttribute, priceAttribute, dateAttribute _
      }

      Dim feedOperation As New FeedOperation
      feedOperation.operand = customizerFeed
      feedOperation.operator = [Operator].ADD

      Dim addedFeed As Feed = feedService.mutate(New FeedOperation() {feedOperation}).value(0)

      Dim dataHolder As New CustomizersDataHolder
      dataHolder.FeedId = addedFeed.id
      dataHolder.NameFeedAttributeId = addedFeed.attributes(0).id
      dataHolder.PriceFeedAttributeId = addedFeed.attributes(1).id
      dataHolder.DateFeedAttributeId = addedFeed.attributes(2).id

      Console.WriteLine("Feed with name '{0}' and ID {1} was added with:", addedFeed.name, _
          dataHolder.FeedId)
      Console.WriteLine("  Name attribute ID {0}", dataHolder.NameFeedAttributeId)
      Console.WriteLine("  Price attribute ID {0}", dataHolder.PriceFeedAttributeId)
      Console.WriteLine("  Date attribute ID {0}", dataHolder.DateFeedAttributeId)

      Return dataHolder
    End Function

    ''' <summary>
    ''' Creates a new FeedMapping that indicates how the data holder's feed
    ''' should be interpreted in the context of ad customizers.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="dataHolder">The data holder that contains metadata about
    ''' the customizer Feed.</param>
    Private Shared Sub CreateFeedMapping(ByVal user As AdWordsUser, _
                                         ByVal dataHolder As CustomizersDataHolder)
      ' Get the FeedMappingService.
      Dim feedMappingService As FeedMappingService = CType(user.GetService( _
          AdWordsService.v201406.FeedMappingService), FeedMappingService)

      Dim feedMapping As New FeedMapping
      feedMapping.feedId = dataHolder.FeedId
      feedMapping.placeholderType = PLACEHOLDER_AD_CUSTOMIZER

      Dim attributeFieldMappings As New List(Of AttributeFieldMapping)
      Dim attributeFieldMapping As AttributeFieldMapping

      attributeFieldMapping = New AttributeFieldMapping()
      attributeFieldMapping.feedAttributeId = dataHolder.NameFeedAttributeId
      attributeFieldMapping.fieldId = PLACEHOLDER_FIELD_STRING
      attributeFieldMappings.Add(attributeFieldMapping)

      attributeFieldMapping = New AttributeFieldMapping()
      attributeFieldMapping.feedAttributeId = dataHolder.PriceFeedAttributeId
      attributeFieldMapping.fieldId = PLACEHOLDER_FIELD_PRICE
      attributeFieldMappings.Add(attributeFieldMapping)

      attributeFieldMapping = New AttributeFieldMapping()
      attributeFieldMapping.feedAttributeId = dataHolder.DateFeedAttributeId
      attributeFieldMapping.fieldId = PLACEHOLDER_FIELD_DATE
      attributeFieldMappings.Add(attributeFieldMapping)

      feedMapping.attributeFieldMappings = attributeFieldMappings.ToArray()

      Dim feedMappingOperation As New FeedMappingOperation
      feedMappingOperation.operand = feedMapping
      feedMappingOperation.operator = [Operator].ADD

      Dim addedFeedMapping As FeedMapping = _
          feedMappingService.mutate(New FeedMappingOperation() {feedMappingOperation}).value(0)

      Console.WriteLine("Feed mapping with ID {0} and placeholder type {1} was added for " & _
          "feed with ID {2}.", addedFeedMapping.feedMappingId, addedFeedMapping.placeholderType, _
          addedFeedMapping.feedId)
    End Sub

    ''' <summary>
    ''' Creates FeedItems with the values to use in ad customizations for each
    ''' ad group in <code>adGroupIds</code>.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupIds">IDs of adgroups to which ad customizations are
    ''' made.</param>
    ''' <param name="dataHolder">The data holder that contains metadata about
    ''' the customizer Feed.</param>
    Private Shared Sub CreateCustomizerFeedItems(ByVal user As AdWordsUser, _
                                                 ByVal adGroupIds As Long(), _
                                                 ByVal dataHolder As CustomizersDataHolder)
      ' Get the FeedItemService.
      Dim feedItemService As FeedItemService = CType(user.GetService( _
          AdWordsService.v201406.FeedItemService), FeedItemService)

      Dim feedItemOperations As New List(Of FeedItemOperation)
      feedItemOperations.Add(CreateFeedItemAddOperation("Mars", "$1234.56", _
          "20140601 000000", adGroupIds(0), dataHolder))
      feedItemOperations.Add(CreateFeedItemAddOperation("Venus", "$1450.00", _
          "20140615 120000", adGroupIds(1), dataHolder))

      Dim feedItemReturnValue As FeedItemReturnValue = feedItemService.mutate( _
          feedItemOperations.ToArray)

      For Each addedFeedItem As FeedItem In feedItemReturnValue.value
        Console.WriteLine("Added feed item with ID {0}", addedFeedItem.feedItemId)
        dataHolder.FeedItemIds.Add(addedFeedItem.feedItemId)
      Next
    End Sub

    ''' <summary>
    ''' Creates a FeedItemOperation that will create a FeedItem with the
    ''' specified values and ad group target when sent to
    ''' FeedItemService.mutate.
    ''' </summary>
    ''' <param name="nameValue">The value for the name attribute of the
    ''' FeedItem.</param>
    ''' <param name="priceValue">The value for the price attribute of the
    ''' FeedItem.</param>
    ''' <param name="dateValue">The value for the date attribute of the
    ''' FeedItem.</param>
    ''' <param name="adGroupId">The ID of the ad group to target with the
    ''' FeedItem.</param>
    ''' <param name="dataHolder">The data holder that contains metadata about
    ''' the customizer Feed.</param>
    ''' <returns>A new FeedItemOperation for adding a FeedItem.</returns>
    Private Shared Function CreateFeedItemAddOperation(ByVal nameValue As String, _
        ByVal priceValue As String, ByVal dateValue As String, ByVal adGroupId As Long, _
        ByVal dataHolder As CustomizersDataHolder) As FeedItemOperation
      Dim feedItem As New FeedItem
      feedItem.feedId = dataHolder.FeedId
      Dim attributeValues As New List(Of FeedItemAttributeValue)

      Dim nameAttributeValue As New FeedItemAttributeValue
      nameAttributeValue.feedAttributeId = dataHolder.NameFeedAttributeId
      nameAttributeValue.stringValue = nameValue
      attributeValues.Add(nameAttributeValue)

      Dim priceAttributeValue As New FeedItemAttributeValue
      priceAttributeValue.feedAttributeId = dataHolder.PriceFeedAttributeId
      priceAttributeValue.stringValue = priceValue
      attributeValues.Add(priceAttributeValue)

      Dim dateAttributeValue As New FeedItemAttributeValue
      dateAttributeValue.feedAttributeId = dataHolder.DateFeedAttributeId
      dateAttributeValue.stringValue = dateValue
      attributeValues.Add(dateAttributeValue)

      feedItem.attributeValues = attributeValues.ToArray

      feedItem.adGroupTargeting = New FeedItemAdGroupTargeting
      feedItem.adGroupTargeting.TargetingAdGroupId = adGroupId

      Dim feedItemOperation As New FeedItemOperation
      feedItemOperation.operand = feedItem
      feedItemOperation.operator = [Operator].ADD

      Return feedItemOperation
    End Function

    ''' <summary>
    ''' Creates a CustomerFeed that will associate the data holder's Feed with
    ''' the ad customizer placeholder type.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="dataHolder">The data holder that contains metadata about
    ''' the customizer Feed.</param>
    Private Shared Sub CreateCustomerFeed(ByVal user As AdWordsUser, _
                                          ByVal dataHolder As CustomizersDataHolder)
      ' Get the CustomerFeedService.
      Dim customerFeedService As CustomerFeedService = CType(user.GetService( _
          AdWordsService.v201406.CustomerFeedService), CustomerFeedService)

      Dim customerFeed As New CustomerFeed
      customerFeed.feedId = dataHolder.FeedId
      customerFeed.placeholderTypes = New Integer() {PLACEHOLDER_AD_CUSTOMIZER}

      ' Create a matching function that will always evaluate to true.
      Dim customerMatchingFunction As New [Function]()
      Dim constOperand As New ConstantOperand
      constOperand.type = ConstantOperandConstantType.BOOLEAN
      constOperand.booleanValue = True
      customerMatchingFunction.lhsOperand = New FunctionArgumentOperand() {constOperand}
      customerMatchingFunction.operator = FunctionOperator.IDENTITY
      customerFeed.matchingFunction = customerMatchingFunction

      ' Create an operation to add the customer feed.
      Dim customerFeedOperation As New CustomerFeedOperation
      customerFeedOperation.operand = customerFeed
      customerFeedOperation.operator = [Operator].ADD

      Dim addedCustomerFeed As CustomerFeed = customerFeedService.mutate( _
          New CustomerFeedOperation() {customerFeedOperation}).value(0)

      Console.WriteLine("Customer feed for feed ID {0} was added.", addedCustomerFeed.feedId)
    End Sub

    ''' <summary>
    ''' Creates text ads that use ad customizations for the specified ad group
    ''' IDs.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupIds">IDs of the ad groups to which customized ads
    ''' are added.</param>
    Private Shared Sub CreateAdsWithCustomizations(ByVal user As AdWordsUser, _
                                                   ByVal adGroupIds As Long())
      ' Get the AdGroupAdService.
      Dim adGroupAdService As AdGroupAdService = CType(user.GetService( _
          AdWordsService.v201406.AdGroupAdService), AdGroupAdService)

      Dim textAd As New TextAd
      textAd.headline = "Luxury Cruise to {=CustomizerFeed.Name}"
      textAd.description1 = "Only {=CustomizerFeed.Price}"
      textAd.description2 = "Offer ends in {=countdown(CustomizerFeed.Date)}!"
      textAd.url = "http://www.example.com"
      textAd.displayUrl = "www.example.com"

      ' We add the same ad to both ad groups. When they serve, they will show
      ' different values, since they match different feed items.
      Dim adGroupAdOperations As New List(Of AdGroupAdOperation)
      For Each adGroupId As Long In adGroupIds
        Dim adGroupAd As New AdGroupAd
        adGroupAd.adGroupId = adGroupId
        adGroupAd.ad = textAd

        Dim adGroupAdOperation As New AdGroupAdOperation
        adGroupAdOperation.operand = adGroupAd
        adGroupAdOperation.operator = [Operator].ADD

        adGroupAdOperations.Add(adGroupAdOperation)
      Next

      Dim adGroupAdReturnValue As AdGroupAdReturnValue = adGroupAdService.mutate( _
          adGroupAdOperations.ToArray)

      For Each addedAd As AdGroupAd In adGroupAdReturnValue.value
        Console.WriteLine("Created an ad with ID {0}, type '{1}' and status '{2}'.", _
            addedAd.ad.id, addedAd.ad.AdType, addedAd.status)
      Next
    End Sub
  End Class
End Namespace
