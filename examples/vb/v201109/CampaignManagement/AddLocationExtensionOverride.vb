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

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example illustrates how to override a location extension.
  ''' To create an ad, run AddTextAds.vb. To create a location extension, run
  ''' AddLocationExtension.vb.
  '''
  ''' Tags: GeoLocationService.get, AdExtensionOverrideService.mutate
  ''' </summary>
  Class AddLocationExtensionOverride
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New AddLocationExtensionOverride
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to override a location extension. To " & _
            "create an ad, run AddTextAds.vb. To create a location extension, run " & _
            "AddLocationExtension.vb."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"AD_ID", "LOCATION_EXTENSION_ID"}
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
      ' Get the AdExtensionOverrideService.
      Dim adExtensionOverrideService As AdExtensionOverrideService = user.GetService( _
          AdWordsService.v201109.AdExtensionOverrideService)

      Dim adId As Long = Long.Parse(parameters("AD_ID"))
      Dim locationExtensionId As Long = Long.Parse(parameters("LOCATION_EXTENSION_ID"))

      ' Create the address.
      Dim address As New Address
      address.streetAddress = "1600 Amphitheatre Parkway"
      address.cityName = "Mountain View"
      address.provinceCode = "CA"
      address.postalCode = "94043"
      address.countryCode = "US"

      ' Get the GeoLocationService.
      Dim geoService As GeoLocationService = user.GetService( _
          AdWordsService.v201109.GeoLocationService)

      Dim selector As New GeoLocationSelector
      selector.addresses = New Address() {address}

      ' Get the geo location for the address.
      Dim location As GeoLocation = geoService.get(selector)(0)

      ' Create the location extension.
      Dim extension As New LocationExtension
      extension.id = locationExtensionId
      extension.address = location.address
      extension.geoPoint = location.geoPoint
      extension.encodedLocation = location.encodedLocation
      extension.source = LocationExtensionSource.ADWORDS_FRONTEND
      extension.phoneNumber = "1-800-555-5556"

      Dim locationExtensionOverride As New AdExtensionOverride
      locationExtensionOverride.adExtension = extension
      locationExtensionOverride.adId = adId

      ' Create the operation.
      Dim operation As New AdExtensionOverrideOperation
      operation.operator = [Operator].ADD
      operation.operand = locationExtensionOverride

      Try
        ' Create the location extension override.
        Dim retVal As AdExtensionOverrideReturnValue = adExtensionOverrideService.mutate( _
            New AdExtensionOverrideOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim adExtensionOverride As AdExtensionOverride = retVal.value(0)
          writer.WriteLine("Overrode location extension with id = ""{0}"" in ad id = ""{1}""", _
              adExtensionOverride.adExtension.id, adExtensionOverride.adId)
        Else
          writer.WriteLine("No location extensions were overridden.")
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to override location extension. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
