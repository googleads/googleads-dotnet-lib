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

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example removes an ad using the 'REMOVE' operator. To list ads,
  ''' run GetTextAds.vb.
  ''' </summary>
  Public Class RemoveAd
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New RemoveAd
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        Dim adId As Long = Long.Parse("INSERT_AD_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId, adId)
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
        Return "This code example removes an ad using the 'REMOVE' operator. To list ads, " & _
            "run GetTextAds.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group that contains the ad.</param>
    ''' <param name="adId">Id of the ad being removed.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long, ByVal adId As Long)
      ' Get the AdGroupAdService.
      Dim adGroupAdService As AdGroupAdService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupAdService), AdGroupAdService)

      ' Since we do not need to update any ad-specific fields, it is enough to
      ' create the base type.
      Dim ad As New Ad
      ad.id = adId

      ' Create the ad group ad.
      Dim adGroupAd As New AdGroupAd
      adGroupAd.adGroupId = adGroupId

      adGroupAd.ad = ad

      ' Create the operation.
      Dim operation As New AdGroupAdOperation
      operation.operand = adGroupAd
      operation.operator = [Operator].REMOVE

      Try
        ' Remove the ad.
        Dim retVal As AdGroupAdReturnValue = adGroupAdService.mutate( _
            New AdGroupAdOperation() {operation})

        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim removedAdGroupAd As AdGroupAd = retVal.value(0)
          Console.WriteLine("Ad with id = ""{0}"" and type = ""{1}"" was removed.", _
              removedAdGroupAd.ad.id, removedAdGroupAd.ad.AdType)
        Else
          Console.WriteLine("No ads were removed.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to remove ad.", e)
      End Try
    End Sub
  End Class
End Namespace
