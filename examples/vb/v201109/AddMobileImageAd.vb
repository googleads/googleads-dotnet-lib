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
Imports System.IO
Imports System.Net

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example shows how to create a Mobile Image Ad.
  ''' </summary>
  Class AddMobileImageAd
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example shows how to create a Mobile Image Ad."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddMobileImageAd
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      Dim service As AdGroupAdService = user.GetService( _
          AdWordsService.v201109.AdGroupAdService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))

      Dim mobileImageId As New MobileImageAd
      mobileImageId.url = "http://www.example.com"

      ' Maximum length of display url is 20 characters.
      mobileImageId.displayUrl = "www.example.com"

      ' Ads should be displayed on carriers supporting HTML and XHTML browsers.
      mobileImageId.markupLanguages = New MarkupLanguageType() {MarkupLanguageType.HTML, _
          MarkupLanguageType.XHTML}

      ' Use all the available carriers. For possible values, see
      ' http://code.google.com/apis/adwords/docs/reference/latest/AdGroupAdService.MobileImageAd.html
      mobileImageId.mobileCarriers = New String() {"ALLCARRIERS"}

      mobileImageId.image = New Image
      mobileImageId.image.data = MediaUtilities.GetAssetDataFromUrl( _
          "http://adwords.google.com/select/images/samples/mobile300-50.gif")

      ' Set the AdGroup Id.
      Dim adGroupAd As New AdGroupAd
      adGroupAd.adGroupId = adGroupId
      adGroupAd.ad = mobileImageId

      ' Create the ADD Operation.
      Dim adGroupAdOperation As New AdGroupAdOperation
      adGroupAdOperation.operator = [Operator].ADD
      adGroupAdOperation.operand = adGroupAd

      Try
        Dim retVal As AdGroupAdReturnValue = service.mutate(New AdGroupAdOperation() _
            {adGroupAdOperation})
        If ((Not retVal.value Is Nothing) AndAlso (retVal.value.Length > 0)) Then
          For Each tempAdGroupAd As AdGroupAd In retVal.value
            Console.WriteLine("New mobile image ad with displayUrl = ""{0}"" and id = {1} was" & _
                " created.", DirectCast(tempAdGroupAd.ad, MobileImageAd).displayUrl, _
                tempAdGroupAd.ad.id)
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to create Ad(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
