' Copyright 2016, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License")
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http:'www.apache.org/licenses/LICENSE-2.0
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
  ''' This code example adds a text ad that uses advanced features of upgraded
  ''' URLs.
  ''' </summary>
  Public Class AddTextAdWithUpgradedUrls
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddTextAdWithUpgradedUrls
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
        Return "This code example adds a text ad that uses advanced features of upgraded URLs."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">ID of the adgroup to which ad is added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupAdService), AdGroupAdService)

      ' Create the text ad.
      Dim textAd As New TextAd
      textAd.headline = "Luxury Cruise to Mars"
      textAd.description1 = "Visit the Red Planet in style."
      textAd.description2 = "Low-gravity fun for everyone!"
      textAd.displayUrl = "www.example.com"

      ' Specify a tracking URL for 3rd party tracking provider. You may
      ' specify one at customer, campaign, ad group, ad, criterion or
      ' feed item levels.
      textAd.trackingUrlTemplate = _
          "http://tracker.example.com/?cid={_season}&promocode={_promocode}&u={lpurl}"

      ' Since your tracking URL has two custom parameters, provide their
      ' values too. This can be provided at campaign, ad group, ad, criterion
      ' or feed item levels.
      Dim seasonParameter As New CustomParameter
      seasonParameter.key = "season"
      seasonParameter.value = "christmas"

      Dim promoCodeParameter As New CustomParameter
      promoCodeParameter.key = "promocode"
      promoCodeParameter.value = "NYC123"

      textAd.urlCustomParameters = New CustomParameters
      textAd.urlCustomParameters.parameters = _
          New CustomParameter() {seasonParameter, promoCodeParameter}

      ' Specify a list of final URLs. This field cannot be set if URL field is
      ' set. This may be specified at ad, criterion and feed item levels.
      textAd.finalUrls = New String() { _
        "http://www.example.com/cruise/space/", _
        "http://www.example.com/locations/mars/" _
      }

      ' Specify a list of final mobile URLs. This field cannot be set if URL
      ' field is set, or finalUrls is unset. This may be specified at ad,
      ' criterion and feed item levels.
      textAd.finalMobileUrls = New String() { _
        "http://mobile.example.com/cruise/space/", _
        "http://mobile.example.com/locations/mars/" _
      }

      Dim textAdGroupAd As New AdGroupAd
      textAdGroupAd.adGroupId = adGroupId
      textAdGroupAd.ad = textAd

      ' Optional: Set the status.
      textAdGroupAd.status = AdGroupAdStatus.PAUSED

      ' Create the operation.
      Dim operation As New AdGroupAdOperation
      operation.operator = [Operator].ADD
      operation.operand = textAdGroupAd

      Dim retVal As AdGroupAdReturnValue = Nothing

      Try
        ' Create the ads.
        retVal = service.mutate(New AdGroupAdOperation() {operation})

        ' Display the results.
        If Not (retVal Is Nothing) AndAlso Not (retVal.value Is Nothing) Then
          Dim newAdGroupAd As AdGroupAd = retVal.value(0)
          Console.WriteLine("New text ad with ID = {0} and display URL = '{1}' was " & _
              "created.", newAdGroupAd.ad.id, newAdGroupAd.ad.displayUrl)
          Console.WriteLine("Upgraded URL properties:")
          Dim newTextAd As TextAd = CType(newAdGroupAd.ad, TextAd)

          Console.WriteLine("  Final URLs: {0}", String.Join(", ", newTextAd.finalUrls))
          Console.WriteLine("  Final Mobile URLS: {0}", _
                            String.Join(", ", newTextAd.finalMobileUrls))
          Console.WriteLine("  Tracking URL template: {0}", newTextAd.trackingUrlTemplate)

          Dim parameters As New List(Of String)
          For Each customParam As CustomParameter In newTextAd.urlCustomParameters.parameters
            parameters.Add(String.Format("{0}={1}", customParam.key, customParam.value))
          Next
          Console.WriteLine("  Custom parameters: {0}", String.Join(", ", parameters.ToArray()))
        Else
          Console.WriteLine("No text ads were created.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add ad to adgroup.", e)
      End Try
    End Sub
  End Class

End Namespace
