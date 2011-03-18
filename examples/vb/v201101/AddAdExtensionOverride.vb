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
Imports Google.Api.Ads.AdWords.v201101

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example illustrates how to override a campaign ad extension.
  ''' To create an ad, run AddAds.vb. To create a campaign ad extension, run
  ''' AddCampaignAdExtension.vb.
  '''
  ''' Tags: GeoLocationService.get, AdExtensionOverrideService.mutate
  ''' </summary>
  Class AddAdExtensionOverride
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to override a campaign ad extension. To " & _
            "create an ad, run AddAds.vb. To create a campaign ad extension, run " & _
            "AddCampaignAdExtension.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddAdExtensionOverride
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdExtensionOverrideService.
      Dim adExtensionOverrideService As AdExtensionOverrideService = user.GetService( _
          AdWordsService.v201101.AdExtensionOverrideService)

      Dim adId As Long = Long.Parse(_T("INSERT_AD_ID_HERE"))
      Dim campaignAdExtensionId As Long = Long.Parse(_T("INSERT_CAMPAIGN_AD_EXTENSION_ID_HERE"))

      Dim address As New Address
      address.streetAddress = "1600 Amphitheatre Parkway"
      address.cityName = "Mountain View"
      address.provinceCode = "CA"
      address.postalCode = "94043"
      address.countryCode = "US"

      Dim geoService As GeoLocationService = user.GetService( _
          AdWordsService.v201101.GeoLocationService)

      Dim selector As New GeoLocationSelector
      selector.addresses = New Address() {address}
      Dim location As GeoLocation = geoService.get(selector)(0)

      Dim extension As New LocationExtension
      extension.id = campaignAdExtensionId
      extension.address = location.address
      extension.geoPoint = location.geoPoint
      extension.encodedLocation = location.encodedLocation
      extension.source = LocationExtensionSource.ADWORDS_FRONTEND
      extension.phoneNumber = "1-800-555-5556"

      Dim adOverride As New AdExtensionOverride
      adOverride.adExtension = extension
      adOverride.adId = adId

      Dim operation As New AdExtensionOverrideOperation
      operation.operator = [Operator].ADD
      operation.operand = adOverride

      Try
        Dim retVal As AdExtensionOverrideReturnValue = adExtensionOverrideService.mutate( _
            New AdExtensionOverrideOperation() {operation})
        If (((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim adExtensionOverride As AdExtensionOverride = retVal.value(0)
          Console.WriteLine("Overrode Ad Extension with id = ""{0}"" in Ad id = ""{1}""", _
              adExtensionOverride.adExtension.id, adExtensionOverride.adId)
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to override AdExtension. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
