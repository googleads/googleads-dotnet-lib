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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example deletes an ad using the 'REMOVE' operator. To get ads,
  ''' run GetAllAds.vb.
  '''
  ''' Tags: AdGroupAdService.mutate
  ''' </summary>
  Class DeleteAd
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example deletes an ad using the 'REMOVE' operator. To get ads, " & _
            "run GetAllAds.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New DeleteAd
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdGroupAdService.
      Dim adGroupAdService As AdGroupAdService = user.GetService( _
          AdWordsService.v201109.AdGroupAdService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_AD_GROUP_ID_HERE"))
      Dim adId As Long = Long.Parse(_T("INSERT_AD_ID_HERE"))

      ' Create base class ad to avoid setting type specific fields.
      Dim ad As New Ad
      ad.id = adId

      ' Create ad group ad.
      Dim adGroupAd As New AdGroupAd
      adGroupAd.adGroupId = adGroupId

      adGroupAd.ad = ad

      ' Create operations.
      Dim operation As New AdGroupAdOperation
      operation.operand = adGroupAd
      operation.operator = [Operator].REMOVE

      Try
        ' Delete ad.
        Dim retVal As AdGroupAdReturnValue = adGroupAdService.mutate( _
            New AdGroupAdOperation() {operation})
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each temp As AdGroupAd In retVal.value
            Console.WriteLine("Ad with id = ""{0}"" and type = ""{1}"" was deleted.", _
                temp.ad.id, temp.ad.AdType)
          Next
        Else
          Console.WriteLine("No ads were deleted.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to delete ad. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
