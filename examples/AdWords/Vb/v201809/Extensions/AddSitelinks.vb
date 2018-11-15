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
    ''' This code example adds sitelinks to a campaign. To create a campaign,
    ''' run AddCampaign.vb.
    ''' </summary>
    Public Class AddSitelinks
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddSitelinks
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
                    "This code example adds sitelinks to a campaign. To create a campaign, run " &
                    "AddCampaign.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignId">Id of the campaign with which sitelinks are associated.
        ''' </param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
            ' [START createExtSetting] MOE:strip_line
            Using campaignExtensionSettingService As CampaignExtensionSettingService =
                DirectCast(user.GetService(AdWordsService.v201809.CampaignExtensionSettingService),
                           CampaignExtensionSettingService)

                Dim customerService As CustomerService =
                        DirectCast(user.GetService(AdWordsService.v201809.CustomerService),
                                   CustomerService)

                ' Find the matching customer and its time zone. The getCustomers method
                ' will return a single Customer object corresponding to the session's
                ' clientCustomerId.
                Dim customer As Customer = customerService.getCustomers()(0)
                Console.WriteLine("Found customer ID {0:###-###-####} with time zone '{1}'.",
                                  customer.customerId, customer.dateTimeZone)

                Dim extensions As New List(Of ExtensionFeedItem)

                ' Create your sitelinks.
                Dim sitelink1 As New SitelinkFeedItem()
                sitelink1.sitelinkText = "Store Hours"
                sitelink1.sitelinkFinalUrls = New UrlList()
                sitelink1.sitelinkFinalUrls.urls = New String() _
                    {"http://www.example.com/storehours"}
                extensions.Add(sitelink1)

                Dim startOfThanksGiving As New DateTime(DateTime.Now.Year, 11, 20, 0, 0, 0)
                Dim endOfThanksGiving As New DateTime(DateTime.Now.Year, 11, 27, 23, 59, 59)

                ' Add check to make sure we don't create a sitelink with end date in the
                ' past.
                If DateTime.Now < endOfThanksGiving Then
                    ' Show the Thanksgiving specials link only from 20 - 27 Nov.
                    Dim sitelink2 As New SitelinkFeedItem()
                    sitelink2.sitelinkText = "Thanksgiving Specials"
                    sitelink2.sitelinkFinalUrls = New UrlList()
                    sitelink2.sitelinkFinalUrls.urls = New String() _
                        {"http://www.example.com/thanksgiving"}

                    sitelink2.startTime = String.Format("{0}1120 000000 {1}", DateTime.Now.Year,
                                                        customer.dateTimeZone)
                    sitelink2.endTime = String.Format("{0}1127 235959 {1}", DateTime.Now.Year,
                                                      customer.dateTimeZone)

                    ' Target this sitelink for United States only. See
                    ' https://developers.google.com/adwords/api/docs/appendix/geotargeting
                    ' for valid geolocation codes.
                    sitelink2.geoTargeting = New Location()
                    sitelink2.geoTargeting.id = 2840

                    ' Restrict targeting only to people physically within the United States.
                    ' Otherwise, this could also show to people interested in the United States
                    ' but not physically located there.
                    Dim geoTargetingRestriction As New FeedItemGeoRestriction()
                    geoTargetingRestriction.geoRestriction = GeoRestriction.LOCATION_OF_PRESENCE
                    sitelink2.geoTargetingRestriction = geoTargetingRestriction

                    extensions.Add(sitelink2)
                End If

                ' Show the wifi details primarily for high end mobile users.
                Dim sitelink3 As New SitelinkFeedItem()
                sitelink3.sitelinkText = "Wifi available"
                sitelink3.sitelinkFinalUrls = New UrlList()
                sitelink3.sitelinkFinalUrls.urls = New String() _
                    {"http://www.example.com/mobile/wifi"}
                sitelink3.devicePreference = New FeedItemDevicePreference()
                sitelink3.devicePreference.devicePreference = 30001
                extensions.Add(sitelink3)

                ' Show the happy hours link only during Mon - Fri 6PM to 9PM.
                Dim sitelink4 As New SitelinkFeedItem()
                sitelink4.sitelinkText = "Happy hours"
                sitelink4.sitelinkFinalUrls = New UrlList()
                sitelink4.sitelinkFinalUrls.urls = New String() _
                    {"http://www.example.com/happyhours"}
                extensions.Add(sitelink4)

                Dim schedule1 As New FeedItemSchedule()
                schedule1.dayOfWeek = DayOfWeek.MONDAY
                schedule1.startHour = 18
                schedule1.startMinute = MinuteOfHour.ZERO
                schedule1.endHour = 21
                schedule1.endMinute = MinuteOfHour.ZERO

                Dim schedule2 As New FeedItemSchedule()
                schedule2.dayOfWeek = DayOfWeek.TUESDAY
                schedule2.startHour = 18
                schedule2.startMinute = MinuteOfHour.ZERO
                schedule2.endHour = 21
                schedule2.endMinute = MinuteOfHour.ZERO

                Dim schedule3 As New FeedItemSchedule()
                schedule3.dayOfWeek = DayOfWeek.WEDNESDAY
                schedule3.startHour = 18
                schedule3.startMinute = MinuteOfHour.ZERO
                schedule3.endHour = 21
                schedule3.endMinute = MinuteOfHour.ZERO

                Dim schedule4 As New FeedItemSchedule()
                schedule4.dayOfWeek = DayOfWeek.THURSDAY
                schedule4.startHour = 18
                schedule4.startMinute = MinuteOfHour.ZERO
                schedule4.endHour = 21
                schedule4.endMinute = MinuteOfHour.ZERO

                Dim schedule5 As New FeedItemSchedule()
                schedule5.dayOfWeek = DayOfWeek.FRIDAY
                schedule5.startHour = 18
                schedule5.startMinute = MinuteOfHour.ZERO
                schedule5.endHour = 21
                schedule5.endMinute = MinuteOfHour.ZERO

                sitelink4.scheduling = New FeedItemSchedule() { _
                                                                  schedule1, schedule2, schedule3,
                                                                  schedule4, schedule5
                                                              }

                ' Create your campaign extension settings. This associates the sitelinks
                ' to your campaign.
                Dim campaignExtensionSetting As New CampaignExtensionSetting()
                campaignExtensionSetting.campaignId = campaignId
                campaignExtensionSetting.extensionType = FeedType.SITELINK
                campaignExtensionSetting.extensionSetting = New ExtensionSetting()
                campaignExtensionSetting.extensionSetting.extensions = extensions.ToArray

                Dim extensionOperation As New CampaignExtensionSettingOperation()
                extensionOperation.operand = campaignExtensionSetting
                extensionOperation.operator = [Operator].ADD

                Try
                    ' Add the extensions.
                    Dim retVal As CampaignExtensionSettingReturnValue =
                            campaignExtensionSettingService.mutate(
                                New CampaignExtensionSettingOperation() _
                                                                      {extensionOperation})

                    ' Display the results.
                    If Not (retVal.value Is Nothing) AndAlso retVal.value.Length > 0 Then
                        Dim newExtensionSetting As CampaignExtensionSetting = retVal.value(0)
                        Console.WriteLine(
                            "Extension setting with type = {0} was added to campaign ID {1}.",
                            newExtensionSetting.extensionType, newExtensionSetting.campaignId)
                    Else
                        Console.WriteLine("No extension settings were created.")
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to create extension settings.", e)
                End Try
            End Using
            ' [END createExtSetting] MOE:strip_line
        End Sub
    End Class
End Namespace
