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
  ''' This code example pauses a given ad. To list all ads, run GetTextAds.cs.
  '''
  ''' Tags: AdGroupAdService.mutate
  ''' </summary>
  Class UpdateAd
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New UpdateAd
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example pauses a given ad. To list all ads, run GetTextAds.cs."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"ADGROUP_ID", "AD_ID"}
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
      Dim service As AdGroupAdService = user.GetService(AdWordsService.v201109.AdGroupAdService)

      Dim adGroupId As Long = Long.Parse(parameters("ADGROUP_ID"))
      Dim adId As Long = Long.Parse(parameters("AD_ID"))
      Dim status As AdGroupAdStatus = AdGroupAdStatus.PAUSED

      ' Create the ad group ad.
      Dim adGroupAd As New AdGroupAd
      adGroupAd.status = status
      adGroupAd.adGroupId = adGroupId

      adGroupAd.ad = New Ad
      adGroupAd.ad.id = adId

      ' Create the operation.
      Dim adGroupAdOperation As New AdGroupAdOperation
      adGroupAdOperation.operator = [Operator].SET
      adGroupAdOperation.operand = adGroupAd

      Try
        ' Update the ad.
        Dim retVal As AdGroupAdReturnValue = service.mutate( _
            New AdGroupAdOperation() {adGroupAdOperation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim pausedAdGroupAd As AdGroupAd = retVal.value(0)
          writer.WriteLine("Ad with id ""{0}"" was paused.", pausedAdGroupAd.ad.id)
        Else
          writer.WriteLine("No ads were paused.")
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to pause ad. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
