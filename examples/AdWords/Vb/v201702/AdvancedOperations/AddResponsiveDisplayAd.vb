' Copyright 2017, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201702
Imports Google.Api.Ads.Common.Util

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201702
  ''' <summary>
  ''' This code example adds an image representing the ad using the MediaService
  ''' and then adds a responsive display ad to an ad group. To get ad groups,
  ''' run GetAdGroups.vb.
  ''' </summary>
  Public Class AddResponsiveDisplayAd
    Inherits ExampleBase
    ''' <summary>
    ''' Number of items being added / updated in this code example.
    ''' </summary>
    Const NUM_ITEMS As Integer = 5

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddResponsiveDisplayAd
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
        Return "This code example adds an image representing the ad using the MediaService" & _
            "and then adds a responsive display ad to an ad group. To get ad groups, " & _
            "run GetAdGroups.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group to which ads are added.
    ''' </param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' [START addResponsiveDisplayAd] MOE:strip_line
      ' Get the AdGroupAdService.
      Dim adGroupAdService As AdGroupAdService = CType(user.GetService( _
          AdWordsService.v201702.AdGroupAdService), AdGroupAdService)

      ' Get the MediaService.
      Dim mediaService As MediaService = CType(user.GetService( _
          AdWordsService.v201702.MediaService), MediaService)

      Try
        ' Create the image.
        Dim image As New Image()
        image.data = MediaUtilities.GetAssetDataFromUrl("https://goo.gl/3b9Wfh", user.Config)
        image.type = MediaMediaType.IMAGE

        ' Upload the image.
        Dim newImage As Image = CType(mediaService.upload(New Media() {image})(0), Image)

        ' Create a responsive display ad.
        Dim responsiveDisplayAd As New ResponsiveDisplayAd()

        ' This ad format does not allow the creation of an image using the
        ' Image.data field. An image must first be created using the MediaService,
        ' and Image.mediaId must be populated when creating the ad.
        responsiveDisplayAd.marketingImage = New Image()
        responsiveDisplayAd.marketingImage.mediaId = newImage.mediaId

        responsiveDisplayAd.shortHeadline = "Travel"
        responsiveDisplayAd.longHeadline = "Travel the World"
        responsiveDisplayAd.description = "Take to the air!"
        responsiveDisplayAd.businessName = "Google"
        responsiveDisplayAd.finalUrls = New String() {"http://www.example.com"}

        ' Create ad group ad.
        Dim adGroupAd As New AdGroupAd()
        adGroupAd.adGroupId = adGroupId
        adGroupAd.ad = responsiveDisplayAd
        adGroupAd.status = AdGroupAdStatus.PAUSED

        ' Create operation.
        Dim operation As New AdGroupAdOperation()
        operation.operand = adGroupAd
        operation.operator = [Operator].ADD

        ' Make the mutate request.
        Dim result As AdGroupAdReturnValue = adGroupAdService.mutate( _
            New AdGroupAdOperation() {operation})

        ' Display results.
        If (Not result Is Nothing) AndAlso (Not result.value Is Nothing) Then
          For Each newAdGroupAd As AdGroupAd In result.value
            Dim newAd As ResponsiveDisplayAd = CType(newAdGroupAd.ad, ResponsiveDisplayAd)
            Console.WriteLine("Responsive display ad with ID '{0}' and short headline '{1}'" & _
                " was added.", newAd.id, newAd.shortHeadline)
          Next
        Else
          Console.WriteLine("No responsive display ads were created.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create responsive display ads.", e)
      End Try
      ' [END addResponsiveDisplayAd] MOE:strip_line
    End Sub
  End Class
End Namespace
