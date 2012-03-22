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
  ''' This code example adds text ads to a given ad group. To list ad groups,
  ''' run GetAdGroups.vb. To learn how to handle policy violations and add
  ''' exemption requests, see HandlePolicyViolationError.vb.
  '''
  ''' Tags: AdGroupAdService.mutate
  ''' </summary>
  Public Class AddTextAds
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New AddTextAds
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, codeExample.GetParameters, Console.Out)
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
        Return "This code example adds text ads to a given ad group. To list ad groups, run " & _
            "GetAdGroups.vb. To learn how to handle policy violations and add exemption " & _
            "requests, see HandlePolicyViolationError.cs."
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

      ' Create the text ad.
      Dim textAd1 As New TextAd
      textAd1.headline = "Luxury Cruise to Mars"
      textAd1.description1 = "Visit the Red Planet in style."
      textAd1.description2 = "Low-gravity fun for everyone!"
      textAd1.displayUrl = "www.example.com"
      textAd1.url = "http://www.example.com"

      Dim textAdGroupAd1 As New AdGroupAd
      textAdGroupAd1.adGroupId = adGroupId
      textAdGroupAd1.ad = textAd1

      ' Optional: Set the status.
      textAdGroupAd1.status = AdGroupAdStatus.PAUSED

      ' Create the text ad.
      Dim textAd2 As New TextAd
      textAd2.headline = "Luxury Hotels in Mars"
      textAd2.description1 = "Enjoy your stay at Red Planet."
      textAd2.description2 = "Low-gravity fun for everyone!"
      textAd2.displayUrl = "www.example.com"
      textAd2.url = "http://www.example.com"

      Dim textAdGroupAd2 As New AdGroupAd
      textAdGroupAd2.adGroupId = adGroupId
      textAdGroupAd2.ad = textAd2

      ' Optional: Set the status.
      textAdGroupAd2.status = AdGroupAdStatus.PAUSED

      ' Create the operations.
      Dim textAdOperation1 As New AdGroupAdOperation
      textAdOperation1.operator = [Operator].ADD
      textAdOperation1.operand = textAdGroupAd1

      Dim textAdOperation2 As New AdGroupAdOperation
      textAdOperation2.operator = [Operator].ADD
      textAdOperation2.operand = textAdGroupAd2

      Dim retVal As AdGroupAdReturnValue = Nothing

      Try
        ' Create the ads.
        retVal = service.mutate(New AdGroupAdOperation() {textAdOperation1, textAdOperation2})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          ' If you are adding multiple type of Ads, then you may need to check
          ' for
          '
          ' if (adGroupAd.ad is TextAd) { ... }
          '
          ' to identify the ad type.
          For Each adGroupAd As AdGroupAd In retVal.value
            writer.WriteLine("New text ad with id = ""{0}"" and displayUrl = ""{1}"" was " & _
                "created.", adGroupAd.ad.id, adGroupAd.ad.displayUrl)
          Next
        Else
          writer.WriteLine("No text ads were created.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to create text ads.", ex)
      End Try
    End Sub
  End Class
End Namespace
