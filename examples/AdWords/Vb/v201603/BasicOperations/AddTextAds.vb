' Copyright 2016, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201603
Imports Google.Api.Ads.Common.Util

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example adds text ads to a given ad group. To list ad groups,
  ''' run GetAdGroups.vb. To learn how to handle policy violations and add
  ''' exemption requests, see HandlePolicyViolationError.vb.
  ''' </summary>
  Public Class AddTextAds
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
      Dim codeExample As New AddTextAds
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
        Return "This code example adds text ads to a given ad group. To list ad groups, run " & _
            "GetAdGroups.vb. To learn how to handle policy violations and add exemption " & _
            "requests, see HandlePolicyViolationError.cs."
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
      Dim service As AdGroupAdService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupAdService), AdGroupAdService)

      Dim operations As New List(Of AdGroupAdOperation)

      For i As Integer = 1 To NUM_ITEMS
        ' Create the text ad.
        Dim textAd As New TextAd
        textAd.headline = "Luxury Cruise to Mars"
        textAd.description1 = "Visit the Red Planet in style."
        textAd.description2 = "Low-gravity fun for everyone!"
        textAd.displayUrl = "www.example.com"
        textAd.finalUrls = New String() {"http://www.example.com/" & i}

        Dim textAdGroupAd As New AdGroupAd
        textAdGroupAd.adGroupId = adGroupId
        textAdGroupAd.ad = textAd

        ' Optional: Set the status.
        textAdGroupAd.status = AdGroupAdStatus.PAUSED

        ' Create the operations.
        Dim operation As New AdGroupAdOperation
        operation.operator = [Operator].ADD
        operation.operand = textAdGroupAd

        operations.Add(operation)
      Next

      Dim retVal As AdGroupAdReturnValue = Nothing

      Try
        ' Create the ads.
        retVal = service.mutate(operations.ToArray())

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          ' If you are adding multiple type of Ads, then you may need to check
          ' for
          '
          ' if (adGroupAd.ad is TextAd) { ... }
          '
          ' to identify the ad type.
          For Each adGroupAd As AdGroupAd In retVal.value
            Console.WriteLine("New text ad with id = ""{0}"" and displayUrl = ""{1}"" was " & _
                "created.", adGroupAd.ad.id, adGroupAd.ad.displayUrl)
          Next
        Else
          Console.WriteLine("No text ads were created.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create text ads.", e)
      End Try
    End Sub
  End Class
End Namespace
