' Copyright 2011, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201109
Imports Google.Api.Ads.Common.Util

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example adds a third party redirect ad to a given ad group. To
  ''' list ad groups, run GetAdGroups.vb.
  '''
  ''' Tags: AdGroupAdService.mutate
  ''' </summary>
  Class AddThirdPartyRedirectAd
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New AddThirdPartyRedirectAd
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds a third party redirect ad to a given ad group. To list " & _
            "ad groups, run GetAdGroups.vb."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"ADGROUP_ID"}
    End Function

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="parameters">The parameters for running the code
    ''' example.</param>
    ''' <param name="writer">The stream writer to which script output should be
    ''' written.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser, ByVal parameters As  _
        Dictionary(Of String, String), ByVal writer As TextWriter)
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = user.GetService( _
          AdWordsService.v201109.AdGroupAdService)

      Dim adGroupId As Long = Long.Parse(parameters("ADGROUP_ID"))

      ' Create the third party redirect ad.
      Dim redirectAd As New ThirdPartyRedirectAd
      redirectAd.name = String.Format("Example third party ad #{0}", ExampleUtilities.GetTimeStamp)
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

      ' Create the operations.
      Dim thirdPartyRedirectAdOperation As New AdGroupAdOperation
      thirdPartyRedirectAdOperation.operator = [Operator].ADD
      thirdPartyRedirectAdOperation.operand = thirdPartyRedirectAdGroupAd

      Dim retVal As AdGroupAdReturnValue = Nothing

      Try
        ' Create the ads.
        retVal = service.mutate(New AdGroupAdOperation() {thirdPartyRedirectAdOperation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          ' If you are adding multiple type of Ads, then you may need to check
          ' for
          '
          ' if (adGroupAd.ad is ThirdPartyRedirectAd) { ... }
          '
          ' to identify the ad type.
          For Each adGroupAd As AdGroupAd In retVal.value
            writer.WriteLine("New third party redirect ad with id = ""{0}"" and url = ""{1}"" " & _
                "was created.", adGroupAd.ad.id, DirectCast(adGroupAd.ad, ThirdPartyRedirectAd).url)
          Next
        Else
          writer.WriteLine("No third party redirect ad was created.")
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to create third party redirect ad. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
