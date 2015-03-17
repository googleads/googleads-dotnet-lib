' Copyright 2015, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201502

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201502

  ''' <summary>
  ''' This code example adds sitelinks to a campaign. To create a campaign,
  ''' run AddCampaign.vb.
  '''
  ''' Tags: CampaignExtensionSettingService.mutate
  ''' </summary>
  Public Class AddSitelinks
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
        Return "This code example adds sitelinks to a campaign. To create a campaign, run " & _
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
      ' Get the CampaignExtensionSettingService.
      Dim campaignExtensionSettingService As CampaignExtensionSettingService = _
          DirectCast(user.GetService(AdWordsService.v201502.CampaignExtensionSettingService),  _
                                    CampaignExtensionSettingService)

      ' Create your sitelinks.
      Dim sitelink1 As New SitelinkFeedItem()
      sitelink1.sitelinkText = "Store Hours"
      sitelink1.sitelinkFinalUrls = New String() {"http://www.example.com/storehours"}

      ' Show the Thanksgiving specials link only from 20 - 27 Nov.
      Dim sitelink2 As New SitelinkFeedItem()
      sitelink2.sitelinkText = "Thanksgiving Specials"
      sitelink2.sitelinkFinalUrls = New String() {"http://www.example.com/thanksgiving"}
      sitelink2.startTime = String.Format("{0}1120 000000 EST", DateTime.Now.Year)
      sitelink2.endTime = String.Format("{0}1127 235959 EST", DateTime.Now.Year)

      ' Show the wifi details primarily for high end mobile users.
      Dim sitelink3 As New SitelinkFeedItem()
      sitelink3.sitelinkText = "Wifi available"
      sitelink3.sitelinkFinalUrls = New String() {"http://www.example.com/mobile/wifi"}
      sitelink3.devicePreference = New FeedItemDevicePreference()
      sitelink3.devicePreference.devicePreference = 30001

      ' Show the happy hours link only during Mon - Fri 6PM to 9PM.
      Dim sitelink4 As New SitelinkFeedItem()
      sitelink4.sitelinkText = "Happy hours"
      sitelink4.sitelinkFinalUrls = New String() {"http://www.example.com/happyhours"}

      Dim schedule1 As New FeedItemSchedule()
      schedule1.dayOfWeek = AdWords.v201502.DayOfWeek.MONDAY
      schedule1.startHour = 18
      schedule1.startMinute = MinuteOfHour.ZERO
      schedule1.endHour = 21
      schedule1.endMinute = MinuteOfHour.ZERO

      Dim schedule2 As New FeedItemSchedule()
      schedule2.dayOfWeek = AdWords.v201502.DayOfWeek.TUESDAY
      schedule2.startHour = 18
      schedule2.startMinute = MinuteOfHour.ZERO
      schedule2.endHour = 21
      schedule2.endMinute = MinuteOfHour.ZERO

      Dim schedule3 As New FeedItemSchedule()
      schedule3.dayOfWeek = AdWords.v201502.DayOfWeek.WEDNESDAY
      schedule3.startHour = 18
      schedule3.startMinute = MinuteOfHour.ZERO
      schedule3.endHour = 21
      schedule3.endMinute = MinuteOfHour.ZERO

      Dim schedule4 As New FeedItemSchedule()
      schedule4.dayOfWeek = AdWords.v201502.DayOfWeek.THURSDAY
      schedule4.startHour = 18
      schedule4.startMinute = MinuteOfHour.ZERO
      schedule4.endHour = 21
      schedule4.endMinute = MinuteOfHour.ZERO

      Dim schedule5 As New FeedItemSchedule()
      schedule5.dayOfWeek = AdWords.v201502.DayOfWeek.FRIDAY
      schedule5.startHour = 18
      schedule5.startMinute = MinuteOfHour.ZERO
      schedule5.endHour = 21
      schedule5.endMinute = MinuteOfHour.ZERO

      sitelink4.scheduling = New FeedItemSchedule() { _
        schedule1, schedule2, schedule3, schedule4, schedule5 _
      }

      ' Create your campaign extension settings. This associates the sitelinks
      ' to your campaign.
      Dim campaignExtensionSetting As New CampaignExtensionSetting()
      campaignExtensionSetting.campaignId = campaignId
      campaignExtensionSetting.extensionType = FeedType.SITELINK
      campaignExtensionSetting.extensionSetting = New ExtensionSetting()
      campaignExtensionSetting.extensionSetting.extensions = New ExtensionFeedItem() { _
        sitelink1, sitelink2, sitelink3, sitelink4 _
      }

      Dim extensionOperation As New CampaignExtensionSettingOperation()
      extensionOperation.operand = campaignExtensionSetting
      extensionOperation.operator = [Operator].ADD

      Try
        ' Add the extensions.
        Dim retVal As CampaignExtensionSettingReturnValue = _
            campaignExtensionSettingService.mutate(New CampaignExtensionSettingOperation() _
                                                   {extensionOperation})

        ' Display the results.
        If Not (retVal.value Is Nothing) AndAlso retVal.value.Length > 0 Then
          Dim newExtensionSetting As CampaignExtensionSetting = retVal.value(0)
          Console.WriteLine("Extension setting with type = {0} was added to campaign ID {1}.", _
              newExtensionSetting.extensionType, newExtensionSetting.campaignId)
        Else
          Console.WriteLine("No extension settings were created.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to create extension settings.", ex)
      End Try
    End Sub
  End Class

End Namespace
