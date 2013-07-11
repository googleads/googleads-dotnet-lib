' Copyright 2013, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201306

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201306
  ''' <summary>
  ''' This code example shows how to add a location extension to an existing
  ''' campaign. To create a campaign, run AddCampaign.vb.
  '''
  ''' Tags: GeoLocationService.get, CampaignAdExtensionService.mutate
  ''' </summary>
  Public Class AddLocationExtension
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddLocationExtension
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
        Return "This code example shows how to add a location extension to an existing" & _
            " campaign. To create a campaign, run AddCampaign.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign to which location
    ''' extensions are added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the CampaignAdExtensionService.
      Dim campaignExtensionService As CampaignAdExtensionService = user.GetService( _
          AdWordsService.v201306.CampaignAdExtensionService)

      ' Add location 1: 1600 Amphitheatre Pkwy, Mountain View, US.
      Dim address1 As New Address
      address1.streetAddress = "1600 Amphitheatre Parkway"
      address1.cityName = "Mountain View"
      address1.provinceCode = "CA"
      address1.postalCode = "94043"
      address1.countryCode = "US"

      ' Add location 2: 38 avenue de l'Opéra, 75002 Paris, FR.
      Dim address2 As New Address
      address2.streetAddress = "38 avenue de l'Opéra"
      address2.cityName = "Paris"
      address2.postalCode = "75002"
      address2.countryCode = "FR"

      ' Get the GeoLocationService.
      Dim geoService As GeoLocationService = user.GetService( _
          AdWordsService.v201306.GeoLocationService)

      ' Create the selector.
      Dim selector As New GeoLocationSelector
      selector.addresses = New Address() {address1, address2}

      ' Retrieve the locations.
      Dim locations As GeoLocation() = geoService.get(selector)

      Dim operations As New List(Of CampaignAdExtensionOperation)

      ' Phone numbers for US and FR offices.
      Dim phoneNumbers As String() = New String() {"(650) 253-0000", "(0)1 42 68 53 00"}
      Dim index As Integer = 0

      ' Create a location extension for each geo location returned by the
      ' server.
      For Each location As GeoLocation In locations
        Dim locationExtension As New LocationExtension
        locationExtension.address = location.address
        locationExtension.geoPoint = location.geoPoint
        locationExtension.encodedLocation = location.encodedLocation
        locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND

        ' Optional: Set the company name.
        locationExtension.companyName = "ACME Inc."

        ' Optional: Set the phone number.
        locationExtension.phoneNumber = phoneNumbers(index)
        index = index + 1

        Dim extension As New CampaignAdExtension
        extension.campaignId = campaignId
        extension.status = CampaignAdExtensionStatus.ACTIVE
        extension.adExtension = locationExtension

        Dim operation As New CampaignAdExtensionOperation
        operation.operator = [Operator].ADD
        operation.operand = extension

        operations.Add(operation)
      Next

      Try
        ' Create the location extensions.
        Dim retVal As CampaignAdExtensionReturnValue = campaignExtensionService.mutate( _
            operations.ToArray)

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each campaignExtension As CampaignAdExtension In retVal.value
            Console.WriteLine("Created a location extension with id = ""{0}"" and status " & _
                "= ""{1}""", campaignExtension.adExtension.id, campaignExtension.status)
          Next
        Else
          Console.WriteLine("No location extensions were created.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to add location extensions.", ex)
      End Try
    End Sub
  End Class
End Namespace
