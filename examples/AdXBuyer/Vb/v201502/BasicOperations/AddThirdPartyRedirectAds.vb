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
Imports Google.Api.Ads.Common.Util

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201502
  ''' <summary>
  ''' This code example adds third party redirect ads to a given ad group. To
  ''' list ad groups, run GetAdGroups.vb.
  '''
  ''' Tags: AdGroupAdService.mutate
  ''' </summary>
  Public Class AddThirdPartyRedirectAds
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddThirdPartyRedirectAds
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId)
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
        Return "This code example adds third party redirect ads to a given ad group. To list " & _
            "ad groups, run GetAdGroups.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group to which ads are added.
    ''' </param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = user.GetService( _
          AdWordsService.v201502.AdGroupAdService)

      ' Create standard third party redirect ad.
      Dim standardAd As New ThirdPartyRedirectAd
      standardAd.name = String.Format("Example third party ad #{0}", ExampleUtilities.GetRandomString)
      standardAd.finalUrls = New String() {"http://www.example.com"}

      standardAd.dimensions = New Dimensions
      standardAd.dimensions.height = 250
      standardAd.dimensions.width = 300

      standardAd.snippet = "<img src='http://goo.gl/HJM3L'/>"

      ' DoubleClick Rich Media Expandable format ID.
      standardAd.certifiedVendorFormatId = 232
      standardAd.isCookieTargeted = False
      standardAd.isUserInterestTargeted = False
      standardAd.isTagged = False
      standardAd.richMediaAdType = RichMediaAdRichMediaAdType.STANDARD

      ' Expandable Ad properties.
      standardAd.expandingDirections = New ThirdPartyRedirectAdExpandingDirection() { _
      ThirdPartyRedirectAdExpandingDirection.EXPANDING_UP, _
      ThirdPartyRedirectAdExpandingDirection.EXPANDING_DOWN _
      }

      standardAd.adAttributes = New RichMediaAdAdAttribute() { _
          RichMediaAdAdAttribute.ROLL_OVER_TO_EXPAND}

      ' Create in-stream third party redirect ad.
      Dim inStreamAd As New ThirdPartyRedirectAd()
      inStreamAd.name = String.Format("Example third party ad #{0}", ExampleUtilities.GetRandomString)
      inStreamAd.finalUrls = New String() {"http://www.example.com"}
      ' Set the duration to 15 secs.
      inStreamAd.adDuration = 15000
      inStreamAd.sourceUrl = "http://ad.doubleclick.net/pfadx/N270.126913.6102203221521/B3876671.21;dcadv=2215309;sz=0x0;ord=%5btimestamp%5d;dcmt=text/xml"
      inStreamAd.certifiedVendorFormatId = 303
      inStreamAd.richMediaAdType = RichMediaAdRichMediaAdType.IN_STREAM_VIDEO

      Dim operations As New List(Of AdGroupAdOperation)
      Dim ads As ThirdPartyRedirectAd() = New ThirdPartyRedirectAd() {standardAd, inStreamAd}
      For Each redirectAd As ThirdPartyRedirectAd In ads
        Dim thirdPartyRedirectAdGroupAd As New AdGroupAd
        thirdPartyRedirectAdGroupAd.adGroupId = adGroupId
        thirdPartyRedirectAdGroupAd.ad = redirectAd

        ' Optional: Set the status.
        thirdPartyRedirectAdGroupAd.status = AdGroupAdStatus.PAUSED

        ' Create the operations.
        Dim operation As New AdGroupAdOperation
        operation.operator = [Operator].ADD
        operation.operand = thirdPartyRedirectAdGroupAd

        operations.Add(operation)
      Next

      Try
        ' Create the ads.
        Dim retVal As AdGroupAdReturnValue = service.mutate(operations.ToArray())

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          ' If you are adding multiple type of Ads, then you may need to check
          ' for
          '
          ' if (adGroupAd.ad is ThirdPartyRedirectAd) { ... }
          '
          ' to identify the ad type.
          For Each adGroupAd As AdGroupAd In retVal.value
            Console.WriteLine("New third party redirect ad with id = ""{0}"" and url = ""{1}"" " & _
                "was created.", adGroupAd.ad.id, _
                DirectCast(adGroupAd.ad, ThirdPartyRedirectAd).finalUrls(0))
          Next
        Else
          Console.WriteLine("No third party redirect ads were created.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to create third party redirect ad. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
