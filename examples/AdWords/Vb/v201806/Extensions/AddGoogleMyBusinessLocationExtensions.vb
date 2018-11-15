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
Imports Google.Api.Ads.Common.Lib

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
    ''' <summary>
    ''' This code example adds a feed that syncs feed items from a Google
    ''' My Business (GMB) account and associates the feed with a customer.
    ''' </summary>
    Public Class AddGoogleMyBusinessLocationExtensions
        Inherits ExampleBase

        ''' <summary>
        ''' The placeholder type for location extensions. See the Placeholder
        ''' reference page for a list of all the placeholder types and fields.
        '''
        ''' https://developers.google.com/adwords/api/docs/appendix/placeholders
        ''' </summary>
        Private Const PLACEHOLDER_LOCATION As Integer = 7

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddGoogleMyBusinessLocationExtensions
            Console.WriteLine(codeExample.Description)

            Dim user As New AdWordsUser

            Try
                ' The email address of either an owner or a manager of the GMB account.
                Dim gmbEmailAddress As String = "INSERT_GMB_EMAIL_ADDRESS_HERE"

                ' Refresh the access token so that there's a valid access token.
                user.OAuthProvider.RefreshAccessToken()

                ' If the gmbEmailAddress above is the same user you used to generate
                ' your AdWords API refresh token, leave the assignment below unchanged.
                ' Otherwise, to obtain an access token for your GMB account, run the
                ' OAuth Token generator utility while logged in as the same user as
                ' gmbEmailAddress. Copy and paste the AccessToken value into the
                ' assignment below.
                Dim gmbAccessToken As String = user.Config.OAuth2AccessToken

                ' If the gmbEmailAddress above is for a GMB manager instead of the GMB
                ' account owner, then set businessAccountIdentifier to the +Page ID of
                ' a location for which the manager has access. See the location
                ' extensions guide at
                ' https://developers.google.com/adwords/api/docs/guides/feed-services-locations
                ' for details.
                Dim businessAccountIdentifier As String = Nothing
                codeExample.Run(user, gmbEmailAddress, gmbAccessToken,
                                businessAccountIdentifier)
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
                    "This code example adds a feed that syncs feed items from a Google my Business " &
                    "(GMB) account and associates the feed with a customer."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="gmbEmailAddress">The email address for Google My Business
        ''' account.</param>
        ''' <param name="gmbAccessToken">The OAuth2 access token for Google
        ''' My Business account.</param>
        ''' <param name="businessAccountIdentifier">The account identifier for
        ''' Google My Business account.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal gmbEmailAddress As String,
                       ByVal gmbAccessToken As String, ByVal businessAccountIdentifier As String)
            Dim gmbFeed As Feed = CreateGmbFeed(user, gmbEmailAddress, gmbAccessToken,
                                                businessAccountIdentifier)
            AddCustomerFeed(user, gmbFeed)
        End Sub

        ''' <summary>
        ''' Create a feed that will sync to the Google My Business account
        ''' specified by gmbEmailAddress.
        ''' </summary>
        ''' <param name="user">The user.</param>
        ''' <param name="gmbEmailAddress">The GMB email address.</param>
        ''' <param name="gmbAccessToken">The GMB access token.</param>
        ''' <param name="businessAccountIdentifier">The GMB account identifier.</param>
        ''' <returns>The newly created GMB feed.</returns>
        Private Shared Function CreateGmbFeed(ByVal user As AdWordsUser,
                                              ByVal gmbEmailAddress As String,
                                              ByVal gmbAccessToken As String,
                                              ByVal businessAccountIdentifier As String) As Feed
            Using feedService As FeedService = CType(
                user.GetService(
                    AdWordsService.v201806.FeedService),
                FeedService)

                ' Create a feed that will sync to the Google My Business account
                ' specified by gmbEmailAddress. Do not add FeedAttributes to this object,
                ' as AdWords will add them automatically because this will be a
                ' system generated feed.
                Dim gmbFeed As New Feed()
                gmbFeed.name = String.Format("Google My Business feed #{0}",
                                             ExampleUtilities.GetRandomString())

                Dim feedData As New PlacesLocationFeedData()
                feedData.emailAddress = gmbEmailAddress
                feedData.businessAccountIdentifier = businessAccountIdentifier

                ' Optional: specify labels to filter Google My Business listings. If
                ' specified, only listings that have any of the labels set are
                ' synchronized into FeedItems.
                feedData.labelFilters = New String() {"Stores in New York City"}

                Dim oAuthInfo As New OAuthInfo()
                oAuthInfo.httpMethod = "GET"

                ' Permissions for the AdWords API scope will also cover GMB.
                oAuthInfo.httpRequestUrl = user.Config.GetDefaultOAuth2Scope()
                oAuthInfo.httpAuthorizationHeader = String.Format("Bearer {0}", gmbAccessToken)
                feedData.oAuthInfo = oAuthInfo

                gmbFeed.systemFeedGenerationData = feedData

                ' Since this feed's feed items will be managed by AdWords,
                ' you must set its origin to ADWORDS.
                gmbFeed.origin = FeedOrigin.ADWORDS

                ' Create an operation to add the feed.
                Dim feedOperation As New FeedOperation()
                feedOperation.operand = gmbFeed
                feedOperation.operator = [Operator].ADD

                Try
                    ' Add the feed. Since it is a system generated feed, AdWords will
                    ' automatically:
                    ' 1. Set up the FeedAttributes on the feed.
                    ' 2. Set up a FeedMapping that associates the FeedAttributes of the
                    ' Feed with the placeholder fields of the LOCATION placeholder type.
                    Dim addFeedResult As FeedReturnValue = feedService.mutate(
                        New FeedOperation() {feedOperation})
                    Dim addedFeed As Feed = addFeedResult.value(0)
                    Console.WriteLine("Added GMB feed with ID {0}", addedFeed.id)
                    Return addedFeed
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to create GMB feed.", e)
                End Try
            End Using
        End Function

        ''' <summary>
        ''' Add a CustomerFeed that associates the feed with this customer for
        ''' the LOCATION placeholder type.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="feed">The GMB feed.</param>
        Sub AddCustomerFeed(ByVal user As AdWordsUser, ByVal feed As Feed)
            Using customerFeedService As CustomerFeedService = CType(
                user.GetService(
                    AdWordsService.v201806.CustomerFeedService),
                CustomerFeedService)

                ' Add a CustomerFeed that associates the feed with this customer for
                ' the LOCATION placeholder type.
                Dim customerFeed As New CustomerFeed()
                customerFeed.feedId = feed.id
                customerFeed.placeholderTypes = New Integer() {PLACEHOLDER_LOCATION}

                ' Create a matching function that will always evaluate to true.
                Dim customerMatchingFunction As New [Function]()
                Dim constOperand As New ConstantOperand()
                constOperand.type = ConstantOperandConstantType.BOOLEAN
                constOperand.booleanValue = True
                customerMatchingFunction.lhsOperand = New FunctionArgumentOperand() {constOperand}
                customerMatchingFunction.operator = FunctionOperator.IDENTITY
                customerFeed.matchingFunction = customerMatchingFunction

                ' Create an operation to add the customer feed.
                Dim customerFeedOperation As New CustomerFeedOperation()
                customerFeedOperation.operand = customerFeed
                customerFeedOperation.operator = [Operator].ADD

                ' After the completion of the Feed ADD operation above the added feed
                ' will not be available for usage in a CustomerFeed until the sync
                ' between the AdWords and GMB accounts completes.  The loop below
                ' will retry adding the CustomerFeed up to ten times with an
                ' exponential back-off policy.
                Dim addedCustomerFeed As CustomerFeed = Nothing

                Dim config As New AdWordsAppConfig()
                config.RetryCount = 10

                Dim errorHandler As New ErrorHandler(config)
                Try
                    Do
                        Try
                            Dim customerFeedResult As CustomerFeedReturnValue =
                                    customerFeedService.mutate(
                                        New CustomerFeedOperation() _
                                                                  {customerFeedOperation})
                            addedCustomerFeed = customerFeedResult.value(0)

                            Console.WriteLine(
                                "Added CustomerFeed for feed ID {0} and placeholder type {1}",
                                addedCustomerFeed.feedId, addedCustomerFeed.placeholderTypes(0))
                            Exit Do
                        Catch e As AdWordsApiException
                            Dim apiException As ApiException = CType(e.ApiException, ApiException)
                            For Each apiError As ApiError In apiException.errors
                                If TypeOf apiError Is CustomerFeedError Then
                                    If (DirectCast(apiError, CustomerFeedError).reason =
                                        CustomerFeedErrorReason.
                                            MISSING_FEEDMAPPING_FOR_PLACEHOLDER_TYPE) Then
                                        errorHandler.DoExponentialBackoff()
                                        errorHandler.IncrementRetriedAttempts()
                                    Else
                                        Throw
                                    End If
                                End If
                            Next

                        End Try
                    Loop While (errorHandler.HaveMoreRetryAttemptsLeft())

                    ' OPTIONAL: Create a CampaignFeed to specify which FeedItems to use at
                    ' the Campaign level.  This will be similar to the CampaignFeed in the
                    ' AddSiteLinks example, except you can also filter based on the
                    ' business name and category of each FeedItem by using a
                    ' FeedAttributeOperand in your matching function.

                    ' OPTIONAL: Create an AdGroupFeed for even more fine grained control
                    ' over which feed items are used at the AdGroup level.
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to create customer feed.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
