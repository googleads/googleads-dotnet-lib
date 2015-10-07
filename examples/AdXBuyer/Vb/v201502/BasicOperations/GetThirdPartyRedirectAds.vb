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

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201502

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201502
  ''' <summary>
  ''' This code example retrieves all third party redirect ads given an existing
  ''' ad group. To add ads to an existing ad group, run
  ''' AddThirdPartyRedirectAd.vb.
  ''' </summary>
  Public Class GetThirdPartyRedirectAds
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetThirdPartyRedirectAds
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId)
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
        Return "This code example retrieves all third party redirect ads given an existing ad " & _
            "group. To add third party redirect ads to an existing ad group, run " & _
            "AddThirdPartyRedirectAd.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group from which text ads are
    ''' retrieved.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = user.GetService(AdWordsService.v201502.AdGroupAdService)

      ' Create a selector.
      Dim selector As New Selector
      selector.fields = New String() {
        Ad.Fields.Id, AdGroupAd.Fields.Status, ThirdPartyRedirectAd.Fields.Url,
        ThirdPartyRedirectAd.Fields.DisplayUrl, ThirdPartyRedirectAd.Fields.RichMediaAdSnippet
      }

      selector.ordering = New OrderBy() {OrderBy.Asc(Ad.Fields.Id)}
      selector.predicates = New Predicate() {
        Predicate.Equals(AdGroupAd.Fields.AdGroupId, adGroupId.ToString()),
        Predicate.Equals("AdType", "THIRD_PARTY_REDIRECT_AD"),
        Predicate.In(AdGroupAd.Fields.Status, New String() {
            AdGroupAdStatus.ENABLED.ToString(),
            AdGroupAdStatus.PAUSED.ToString(),
            AdGroupAdStatus.DISABLED.ToString()
        })
      }
      selector.paging = Paging.Default

      Dim page As New AdGroupAdPage

      Try
        Do
          ' Get the third party redirect ads.
          page = service.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = selector.paging.startIndex

            For Each adGroupAd As AdGroupAd In page.entries
              Dim thirdPartyRedirectAd As ThirdPartyRedirectAd = adGroupAd.ad
              Console.WriteLine("{0}) Ad id is {1} and status is {2}", i + 1, _
                                thirdPartyRedirectAd.id, adGroupAd.status)
              Console.WriteLine("  Url: {0}\n  Display Url: {1}\n  Snippet:{2}", _
                  String.Join(",", thirdPartyRedirectAd.finalUrls), _
                  thirdPartyRedirectAd.displayUrl, thirdPartyRedirectAd.snippet)
            Next
            i += 1
          End If
          selector.paging.IncreaseOffset()
        Loop While (selector.paging.startIndex < page.totalNumEntries)
        Console.WriteLine("Number of third party redirect ads found: {0}", page.totalNumEntries)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to get third party redirect ads.", e)
      End Try
    End Sub
  End Class
End Namespace
