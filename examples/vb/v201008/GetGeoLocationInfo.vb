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
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example gets geo location information for addresses.
  '''
  ''' Tags: GeoLocationService.get
  ''' </summary>
  Class GetGeoLocationInfo
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets geo location information for addresses."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetGeoLocationInfo
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the GeoLocationService.
      Dim geoLocationService As GeoLocationService = user.GetService( _
          AdWordsService.v201008.GeoLocationService)

      Dim address1 As New Address
      address1.streetAddress = "1600 Amphitheatre Parkway"
      address1.cityName = "Mountain View"
      address1.provinceCode = "CA"
      address1.postalCode = "94043"
      address1.countryCode = "US"

      Dim address2 As New Address
      address2.streetAddress = "76 Ninth Avenue"
      address2.cityName = "New York"
      address2.provinceCode = "NY"
      address2.postalCode = "10011"
      address2.countryCode = "US"

      Dim address3 As New Address
      address3.streetAddress = "五四大街1号, Beijing东城区"
      address3.countryCode = "CN"

      ' Create selector.
      Dim selector As New GeoLocationSelector
      selector.addresses = New Address() {address1, address2, address3}

      Try
        ' Get geo locations.
        Dim geoLocations As GeoLocation() = geoLocationService.get(selector)

        If (Not geoLocations Is Nothing) Then
          ' Display geo locations.
          For Each geoLocation As GeoLocation In geoLocations
            If Not TypeOf geoLocation Is InvalidGeoLocation Then
              Console.WriteLine("Address {0} has latitude {1} and longitude {2}.", _
                  geoLocation.address.streetAddress, geoLocation.geoPoint.latitudeInMicroDegrees, _
                  geoLocation.geoPoint.longitudeInMicroDegrees)
            Else
              Console.WriteLine("Invalid geo location returned.\n")
            End If
          Next
        Else
          Console.WriteLine("No geo locations were returned.\n")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve geo location(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
