' Copyright 2012, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201109_1
Imports Google.Api.Ads.Common.Util

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109_1
  ''' <summary>
  ''' This code example adds third party redirect ads to a given ad group. To
  ''' list ad groups, run GetAdGroups.vb.
  '''
  ''' Tags: AdGroupAdService.mutate
  ''' </summary>
  Public Class AddThirdPartyRedirectAds
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
          AdWordsService.v201109_1.AdGroupAdService)

      Dim operations As New List(Of AdGroupAdOperation)

      For i As Integer = 1 To NUM_ITEMS
        ' Create the third party redirect ad.
        Dim redirectAd As New ThirdPartyRedirectAd
        redirectAd.name = String.Format("Example third party ad #{0}", ExampleUtilities.GetRandomString)
        redirectAd.url = "http://www.example.com"

        redirectAd.dimensions = New Dimensions
        redirectAd.dimensions.height = 250
        redirectAd.dimensions.width = 300

        redirectAd.snippet = "<img src=""https://sandbox.google.com/sandboximages/image.jpg""/>"
        redirectAd.impressionBeaconUrl = "http://www.examples.com/beacon"
        redirectAd.certifiedVendorFormatId = 119
        redirectAd.isCookieTargeted = False
        redirectAd.isUserInterestTargeted = False
        redirectAd.isTagged = False

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
                "was created.", adGroupAd.ad.id, DirectCast(adGroupAd.ad, ThirdPartyRedirectAd).url)
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
