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

Imports DayOfWeek = Google.Api.Ads.AdWords.v201809.DayOfWeek

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example adds a price extension and associates it with an
    ''' account. Campaign targeting is also set using the specified campaign ID.
    ''' To get campaigns, run AddCampaigns.vb.
    ''' </summary>
    Public Class AddPrices
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddPrices
            Console.WriteLine(codeExample.Description)
            Try
                Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
                codeExample.Run(New AdWordsUser, campaignId)
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
                    "This code example adds a price extension and associates it with an account. " &
                    "Campaign targeting is also set using the specified campaign ID. To get " &
                    "campaigns, run AddCampaigns.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignId">Id of the campaign with which sitelinks are associated.
        ''' </param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
            Using customerExtensionSettingService As CustomerExtensionSettingService =
                DirectCast(user.GetService(AdWordsService.v201809.CustomerExtensionSettingService),
                           CustomerExtensionSettingService)

                ' [START createFeedItems] MOE:strip_line
                ' Create the price extension feed item.
                Dim priceFeedItem As New PriceFeedItem
                priceFeedItem.priceExtensionType = PriceExtensionType.SERVICES

                ' Price qualifier is optional.
                priceFeedItem.priceQualifier = PriceExtensionPriceQualifier.FROM
                priceFeedItem.trackingUrlTemplate = "http://tracker.example.com/?u={lpurl}"
                priceFeedItem.language = "en"

                priceFeedItem.campaignTargeting = New FeedItemCampaignTargeting()
                priceFeedItem.campaignTargeting.TargetingCampaignId = campaignId

                Dim saturdaySchedule As New FeedItemSchedule
                saturdaySchedule.dayOfWeek = DayOfWeek.SATURDAY
                saturdaySchedule.startHour = 10
                saturdaySchedule.startMinute = MinuteOfHour.ZERO
                saturdaySchedule.endHour = 22
                saturdaySchedule.endMinute = MinuteOfHour.ZERO

                Dim sundaySchedule As New FeedItemSchedule
                sundaySchedule.dayOfWeek = DayOfWeek.SUNDAY
                sundaySchedule.startHour = 10
                sundaySchedule.startMinute = MinuteOfHour.ZERO
                sundaySchedule.endHour = 18
                sundaySchedule.endMinute = MinuteOfHour.ZERO

                priceFeedItem.scheduling = New FeedItemSchedule() {saturdaySchedule, sundaySchedule}

                ' To create a price extension, at least three table rows are needed.
                Dim priceTableRows As New List(Of PriceTableRow)

                Dim currencyCode As String = "USD"

                priceTableRows.Add(
                    CreatePriceTableRow(
                        "Scrubs",
                        "Body Scrub, Salt Scrub",
                        "http://www.example.com/scrubs",
                        "http://m.example.com/scrubs",
                        60000000,
                        currencyCode,
                        PriceExtensionPriceUnit.PER_HOUR))
                priceTableRows.Add(
                    CreatePriceTableRow(
                        "Hair Cuts",
                        "Once a month",
                        "http://www.example.com/haircuts",
                        "http://m.example.com/haircuts",
                        75000000,
                        currencyCode,
                        PriceExtensionPriceUnit.PER_MONTH))
                priceTableRows.Add(
                    CreatePriceTableRow(
                        "Skin Care Package",
                        "Four times a month",
                        "http://www.example.com/skincarepackage",
                        Nothing,
                        250000000,
                        currencyCode,
                        PriceExtensionPriceUnit.PER_MONTH))

                priceFeedItem.tableRows = priceTableRows.ToArray()

                ' Create your campaign extension settings. This associates the sitelinks
                ' to your campaign.
                Dim customerExtensionSetting As New CustomerExtensionSetting
                customerExtensionSetting.extensionType = FeedType.PRICE
                customerExtensionSetting.extensionSetting = New ExtensionSetting
                customerExtensionSetting.extensionSetting.extensions =
                    New ExtensionFeedItem() {priceFeedItem}
                ' [END createFeedItems] MOE:strip_line

                Dim operation As New CustomerExtensionSettingOperation
                operation.operand = customerExtensionSetting
                operation.operator = [Operator].ADD

                Try
                    ' Add the extensions.
                    Dim retVal As CustomerExtensionSettingReturnValue =
                            customerExtensionSettingService.mutate(
                                New CustomerExtensionSettingOperation() {operation})
                    If Not (retVal.value Is Nothing) AndAlso retVal.value.Length > 0 Then
                        Dim newExtensionSetting As CustomerExtensionSetting = retVal.value(0)
                        Console.WriteLine("Extension setting with type '{0}' was added.",
                                          newExtensionSetting.extensionType)
                    Else
                        Console.WriteLine("No extension settings were created.")
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to create extension settings.", e)
                End Try
            End Using
        End Sub

        '  [START createPriceTableRow] MOE:strip_line
        ''' <summary>
        ''' Creates a price table row.
        ''' </summary>
        ''' <param name="header">The row header.</param>
        ''' <param name="description">The description text.</param>
        ''' <param name="finalUrl">The final URL.</param>
        ''' <param name="finalMobileUrl">The mobile final URL, or null if this field
        ''' should not be set.</param>
        ''' <param name="priceInMicros">The price in micros.</param>
        ''' <param name="currencyCode">The currency code.</param>
        ''' <param name="priceUnit">The price unit.</param>
        ''' <returns>A price table row for creating price extension.</returns>
        Private Shared Function CreatePriceTableRow(ByVal header As String,
                                                    ByVal description As String,
                                                    ByVal finalUrl As String,
                                                    ByVal finalMobileUrl As String,
                                                    ByVal priceInMicros As Long,
                                                    ByVal currencyCode As String,
                                                    ByVal priceUnit As PriceExtensionPriceUnit) _
            As PriceTableRow

            Dim retval As New PriceTableRow
            retval.header = header
            retval.description = description
            retval.finalUrls = New UrlList()
            retval.finalUrls.urls = New String() {finalUrl}

            Dim moneyWithCurrency As New MoneyWithCurrency
            moneyWithCurrency.currencyCode = currencyCode
            moneyWithCurrency.money = New Money
            moneyWithCurrency.money.microAmount = priceInMicros

            retval.price = moneyWithCurrency
            retval.priceUnit = priceUnit

            ' Optional: Set the mobile final URLs.
            If Not String.IsNullOrEmpty(finalMobileUrl) Then
                retval.finalMobileUrls = New UrlList()
                retval.finalMobileUrls.urls = New String() {finalMobileUrl}
            End If

            Return retval
        End Function
        ' [END createPriceTableRow] MOE:strip_line
    End Class
End Namespace
