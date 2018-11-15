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
Imports Google.Api.Ads.AdWords.v201809

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example adds an ad customizer feed. Then it adds an ad in two
    ''' different ad groups that uses the feed to populate dynamic data.
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
                Dim feedName As String = "INSERT_FEED_NAME_HERE"
                codeExample.Run(New AdWordsUser(), adGroupId1, adGroupId2, feedName)
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
                Return "This code example adds an ad customizer feed. Then it adds an ad in two " &
                       "different ad groups that uses the feed to populate dynamic data."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId1">Id of the first adgroup to which ads with ad
        ''' customizers are added.</param>
        ''' <param name="adGroupId2">Id of the second adgroup to which ads with ad
        ''' customizers are added.</param>
        ''' <param name="feedName">Name of the feed to be created.</param>
        Public Sub Run(ByVal user As AdWordsUser,
                       ByVal adGroupId1 As Long,
                       ByVal adGroupId2 As Long,
                       ByVal feedName As String)
            Try
                ' Create a customizer feed. One feed per account can be used for all ads.
                Dim adCustomizerFeed As AdCustomizerFeed = CreateCustomizerFeed(user, feedName)

                ' Add feed items containing the values we'd like to place in ads.
                CreateCustomizerFeedItems(user, New Long() {adGroupId1, adGroupId2},
                                          adCustomizerFeed)

                ' All set! We can now create ads with customizations.
                CreateAdsWithCustomizations(user, New Long() {adGroupId1, adGroupId2}, feedName)
            Catch e As Exception
                Throw New System.ApplicationException("Failed to add ad customizers modifiers to " +
                                                      "ad group.", e)
            End Try
        End Sub

        ''' <summary>
        ''' Creates a new Feed for ad customizers.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="feedName">Name of the feed to be created.</param>
        ''' <returns>An ad customizer feed.</returns>
        Private Shared Function CreateCustomizerFeed(ByVal user As AdWordsUser,
                                                     ByVal feedName As String) As AdCustomizerFeed
            ' [START createFeed] MOE:strip_line
            Using adCustomizerFeedService As AdCustomizerFeedService = DirectCast(
                user.GetService(
                    AdWordsService.v201809.AdCustomizerFeedService),
                AdCustomizerFeedService)

                Dim feed As New AdCustomizerFeed()
                feed.feedName = feedName

                Dim attribute1 As New AdCustomizerFeedAttribute
                attribute1.name = "Name"
                attribute1.type = AdCustomizerFeedAttributeType.STRING

                Dim attribute2 As New AdCustomizerFeedAttribute
                attribute2.name = "Price"
                attribute2.type = AdCustomizerFeedAttributeType.PRICE

                Dim attribute3 As New AdCustomizerFeedAttribute
                attribute3.name = "Date"
                attribute3.type = AdCustomizerFeedAttributeType.DATE_TIME

                feed.feedAttributes = New AdCustomizerFeedAttribute() { _
                                                                          attribute1, attribute2,
                                                                          attribute3
                                                                      }
                ' [END createFeed] MOE:strip_line

                ' [START mutateFeed] MOE:strip_line
                Dim feedOperation As New AdCustomizerFeedOperation()
                feedOperation.operand = feed
                feedOperation.operator = [Operator].ADD

                Dim addedFeed As AdCustomizerFeed = adCustomizerFeedService.mutate(
                    New AdCustomizerFeedOperation() {feedOperation}).value(0)

                Console.WriteLine("Created ad customizer feed with ID = {0} and name = '{1}'.",
                                  addedFeed.feedId, addedFeed.feedName)
                ' [END mutateFeed] MOE:strip_line
                Return addedFeed
            End Using
        End Function

        ''' <summary>
        ''' Creates FeedItems with the values to use in ad customizations for each
        ''' ad group in <code>adGroupIds</code>.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupIds">IDs of adgroups to which ad customizations are
        ''' made.</param>
        ''' <param name="adCustomizerFeed">The ad customizer feed.</param>
        Private Shared Sub CreateCustomizerFeedItems(ByVal user As AdWordsUser,
                                                     ByVal adGroupIds As Long(),
                                                     ByVal adCustomizerFeed As AdCustomizerFeed)
            Using feedItemService As FeedItemService = CType(
                user.GetService(
                    AdWordsService.v201809.FeedItemService),
                FeedItemService)

                Dim feedItemOperations As New List(Of FeedItemOperation)

                ' [START specifyFeedItemValues] MOE:strip_line
                Dim marsDate As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
                feedItemOperations.Add(CreateFeedItemAddOperation(adCustomizerFeed, "Mars",
                                                                  "$1234.56",
                                                                  marsDate.ToString(
                                                                      "yyyyMMdd HHmmss")))

                Dim venusDate As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 15)
                feedItemOperations.Add(CreateFeedItemAddOperation(adCustomizerFeed, "Venus",
                                                                  "$1450.00",
                                                                  venusDate.ToString(
                                                                      "yyyyMMdd HHmmss")))
                ' [END specifyFeedItemValues] MOE:strip_line
                Dim feedItemReturnValue As FeedItemReturnValue = feedItemService.mutate(
                    feedItemOperations.ToArray)

                For Each addedFeedItem As FeedItem In feedItemReturnValue.value
                    Console.WriteLine("Added feed item with ID {0}", addedFeedItem.feedItemId)
                Next

                ' Add feed item targeting to restrict the feed item to specific ad groups.
                RestrictFeedItemToAdGroup(user, feedItemReturnValue.value(0), adGroupIds(0))
                RestrictFeedItemToAdGroup(user, feedItemReturnValue.value(1), adGroupIds(1))
            End Using
        End Sub

        ''' <summary>
        ''' Restricts the feed item to an ad group.
        ''' </summary>
        ''' <param name="user">The user.</param>
        ''' <param name="feedItem">The feed item.</param>
        ''' <param name="adGroupId">The ad group ID.</param>
        Private Shared Sub RestrictFeedItemToAdGroup(user As AdWordsUser,
                                                     feedItem As FeedItem, adGroupId As Long?)
            Dim adGroupTarget As New FeedItemAdGroupTarget()
            adGroupTarget.feedId = feedItem.feedId
            adGroupTarget.feedItemId = feedItem.feedItemId
            adGroupTarget.adGroupId = adGroupId.Value

            Using feedItemTargetService As FeedItemTargetService = CType(
                user.GetService(
                    AdWordsService.v201809.FeedItemTargetService),
                FeedItemTargetService)
                Dim operation As New FeedItemTargetOperation()
                operation.operator = [Operator].ADD
                operation.operand = adGroupTarget

                Dim retval As FeedItemTargetReturnValue = feedItemTargetService.mutate(
                    New FeedItemTargetOperation() {operation})
                Dim newAdGroupTarget As FeedItemAdGroupTarget =
                        CType(retval.value(0), FeedItemAdGroupTarget)
                Console.WriteLine("Feed item target for feed ID {0} and feed item ID {1}" +
                                  " was created to restrict serving to ad group ID {2}",
                                  newAdGroupTarget.feedId, newAdGroupTarget.feedItemId,
                                  newAdGroupTarget.adGroupId)
            End Using
        End Sub

        ''' <summary>
        ''' Creates a FeedItemOperation that will create a FeedItem with the
        ''' specified values when sent to FeedItemService.mutate.
        ''' </summary>
        ''' <param name="adCustomizerFeed">The ad customizer feed.</param>
        ''' <param name="nameValue">The value for the name attribute of the
        ''' FeedItem.</param>
        ''' <param name="priceValue">The value for the price attribute of the
        ''' FeedItem.</param>
        ''' <param name="dateValue">The value for the date attribute of the
        ''' FeedItem.</param>
        ''' <returns>A new FeedItemOperation for adding a FeedItem.</returns>
        Private Shared Function CreateFeedItemAddOperation(ByVal adCustomizerFeed As _
                                                              AdCustomizerFeed,
                                                           ByVal nameValue As String,
                                                           ByVal priceValue As String,
                                                           ByVal dateValue As String) _
            As FeedItemOperation
            ' [START createFeedItem] MOE:strip_line
            Dim feedItem As New FeedItem
            feedItem.feedId = adCustomizerFeed.feedId
            Dim attributeValues As New List(Of FeedItemAttributeValue)

            ' FeedAttributes appear in the same order as they were created
            ' - Name, Price, Date. See CreateCustomizerFeed method for details.
            Dim nameAttributeValue As New FeedItemAttributeValue
            nameAttributeValue.feedAttributeId = adCustomizerFeed.feedAttributes(0).id
            nameAttributeValue.stringValue = nameValue
            attributeValues.Add(nameAttributeValue)

            Dim priceAttributeValue As New FeedItemAttributeValue
            priceAttributeValue.feedAttributeId = adCustomizerFeed.feedAttributes(1).id
            priceAttributeValue.stringValue = priceValue
            attributeValues.Add(priceAttributeValue)

            Dim dateAttributeValue As New FeedItemAttributeValue
            dateAttributeValue.feedAttributeId = adCustomizerFeed.feedAttributes(2).id
            dateAttributeValue.stringValue = dateValue
            attributeValues.Add(dateAttributeValue)

            feedItem.attributeValues = attributeValues.ToArray

            ' [END createFeedItem] MOE:strip_line

            Dim feedItemOperation As New FeedItemOperation
            feedItemOperation.operand = feedItem
            feedItemOperation.operator = [Operator].ADD

            Return feedItemOperation
        End Function

        ''' <summary>
        ''' Creates text ads that use ad customizations for the specified ad group
        ''' IDs.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupIds">IDs of the ad groups to which customized ads
        ''' are added.</param>
        ''' <param name="feedName">Name of the feed to use.</param>
        Private Shared Sub CreateAdsWithCustomizations(ByVal user As AdWordsUser,
                                                       ByVal adGroupIds As Long(),
                                                       ByVal feedName As String)
            Using adGroupAdService As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupAdService),
                AdGroupAdService)

                ' [START createAd] MOE:strip_line
                Dim expandedTextAd As New ExpandedTextAd
                expandedTextAd.headlinePart1 = String.Format("Luxury Cruise to {{={0}.Name}}",
                                                             feedName)
                expandedTextAd.headlinePart2 = String.Format("Only {{={0}.Price}}", feedName)
                expandedTextAd.description =
                    String.Format("Offer ends in {{=countdown({0}.Date)}}!",
                                  feedName)
                expandedTextAd.finalUrls = New String() {"http://www.example.com"}
                ' [END createAd] MOE:strip_line

                ' We add the same ad to both ad groups. When they serve, they will show
                ' different values, since they match different feed items.
                Dim adGroupAdOperations As New List(Of AdGroupAdOperation)
                For Each adGroupId As Long In adGroupIds
                    Dim adGroupAd As New AdGroupAd
                    adGroupAd.adGroupId = adGroupId
                    adGroupAd.ad = expandedTextAd

                    Dim adGroupAdOperation As New AdGroupAdOperation
                    adGroupAdOperation.operand = adGroupAd
                    adGroupAdOperation.operator = [Operator].ADD

                    adGroupAdOperations.Add(adGroupAdOperation)
                Next

                Dim adGroupAdReturnValue As AdGroupAdReturnValue = adGroupAdService.mutate(
                    adGroupAdOperations.ToArray)

                For Each addedAd As AdGroupAd In adGroupAdReturnValue.value
                    Console.WriteLine("Created an ad with ID {0}, type '{1}' and status '{2}'.",
                                      addedAd.ad.id, addedAd.ad.AdType, addedAd.status)
                Next
            End Using
        End Sub
    End Class
End Namespace
