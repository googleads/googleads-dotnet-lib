' Copyright 2016, Google Inc. All Rights Reserved.
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

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603

  ''' <summary>
  ''' This code example upgrades an ad to use upgraded URLs.
  ''' </summary>
  Public Class UpgradeAdUrl
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New UpgradeAdUrl
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        Dim adId As Long = Long.Parse("INSERT_AD_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId, adId)
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
        Return "This code example upgrades an ad to use upgraded URLs."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">ID of the ad group that contains the ad.
    ''' </param>
    ''' <param name="adId">ID of the ad to be upgraded.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long, ByVal adId As Long)
      ' Get the AdGroupAdService.
      Dim adGroupAdService As AdGroupAdService = DirectCast(user.GetService( _
          AdWordsService.v201603.AdGroupAdService), AdGroupAdService)

      Try
        ' Retrieve the Ad.
        Dim adGroupAd As AdGroupAd = GetAdGroupAd(adGroupAdService, adGroupId, adId)

        If adGroupAd Is Nothing Then
          Console.WriteLine("Ad not found.")
          Return
        End If

        ' Copy the destination url to the final url.
        Dim upgradeUrl As New AdUrlUpgrade()
        upgradeUrl.adId = adGroupAd.ad.id
        upgradeUrl.finalUrl = adGroupAd.ad.url

        ' Upgrade the ad.
        Dim upgradedAds As Ad() = adGroupAdService.upgradeUrl(New AdUrlUpgrade() {upgradeUrl})

        ' Display the results.
        If ((Not upgradedAds Is Nothing) AndAlso upgradedAds.Length > 0) Then
          For Each upgradedAd As Ad In upgradedAds
            Console.WriteLine("Ad with id = {0} and destination url = {1} was upgraded.", _
                upgradedAd.id, upgradedAd.finalUrls(0))
          Next
        Else
          Console.WriteLine("No ads were upgraded.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to upgrade ads.", e)
      End Try
    End Sub

    ''' <summary>
    ''' Gets the ad group ad by ID.
    ''' </summary>
    ''' <param name="adGroupAdService">The AdGroupAdService instance.</param>
    ''' <param name="adGroupId">ID of the ad group.</param>
    ''' <param name="adId">ID of the ad to be retrieved.</param>
    ''' <returns>The AdGroupAd if the item could be retrieved, null otherwise.
    ''' </returns>
    Private Function GetAdGroupAd(ByVal adGroupAdService As AdGroupAdService, _
                                  ByVal adGroupId As Long, ByVal adId As Long) As AdGroupAd
      ' Create a selector.
      Dim selector As New Selector()
      selector.fields = New String() {Ad.Fields.Id, Ad.Fields.Url}

      ' Restrict the fetch to only the selected ad group ID and ad ID.
      selector.predicates = New Predicate() {
        Predicate.Equals(AdGroupAd.Fields.AdGroupId, adGroupId),
        Predicate.Equals(Ad.Fields.Id, adId)
      }

      ' Get the ad.
      Dim page As AdGroupAdPage = adGroupAdService.get(selector)

      If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing) AndAlso _
          (page.entries.Length > 0)) Then
        Return page.entries(0)
      Else
        Return Nothing
      End If
    End Function
  End Class

End Namespace
