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
Imports Google.Api.Ads.AdWords.v201101
Imports Google.Api.Ads.Common.Util

Imports System
Imports System.IO
Imports System.Net

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example adds a text, image ad, and template (Click to Play
  ''' Video) ad to a given ad group. To get ad group, run GetAllAdGroups.vb.
  ''' To get all videos, run GetAllVideos.vb. To upload video, see
  ''' http://adwords.google.com/support/aw/bin/answer.py?hl=en&amp;answer=39454.
  '''
  ''' Tags: AdGroupAdService.mutate
  ''' </summary>
  Class AddAds
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds a text, image ad, and template (Click to Play Video) " & _
            "ad to a given ad group. To get ad group, run GetAllAdGroups.vb. To get all " & _
            "videos, run GetAllVideos.vb. To upload video, see " & _
            "http://adwords.google.com/support/aw/bin/answer.py?hl=en&answer=39454."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddAds
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = user.GetService( _
          AdWordsService.v201101.AdGroupAdService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))
      Dim videoMediaId As Long = Long.Parse(_T("INSERT_VIDEO_MEDIA_ID_HERE"))

      ' Create your text ad.
      Dim textAd As New TextAd
      textAd.headline = "Luxury Cruise to Mars"
      textAd.description1 = "Visit the Red Planet in style."
      textAd.description2 = "Low-gravity fun for everyone!"
      textAd.displayUrl = "www.example.com"
      textAd.url = "http://www.example.com"

      Dim textadGroupAd As New AdGroupAd
      textadGroupAd.adGroupId = adGroupId
      textadGroupAd.ad = textAd

      Dim textAdOperation As New AdGroupAdOperation
      textAdOperation.operator = [Operator].ADD
      textAdOperation.operand = textadGroupAd

      ' Create your image ad.
      Dim imageAd As New ImageAd
      imageAd.name = "My Image Ad"
      imageAd.displayUrl = "www.example.com"
      imageAd.url = "http://www.example.com"

      ' Load your image into data field.
      imageAd.image = New Image
      imageAd.image.data = MediaUtilities.GetAssetDataFromUrl( _
          "https://sandbox.google.com/sandboximages/image.jpg")

      ' Set the AdGroup Id.
      Dim imageAdGroupAd As New AdGroupAd
      imageAdGroupAd.adGroupId = adGroupId
      imageAdGroupAd.ad = imageAd

      ' Create the ADD Operation.
      Dim imageAdOperation As New AdGroupAdOperation
      imageAdOperation.operator = [Operator].ADD
      imageAdOperation.operand = imageAdGroupAd

      ' Create your video ad.
      Dim templateAd As New TemplateAd
      templateAd.templateId = 9

      Dim templateElement As New TemplateElement
      templateElement.uniqueName = "adData"
      templateAd.templateElements = New TemplateElement() {templateElement}

      ' Create the template field "startImage".
      Dim imageField As New TemplateElementField
      imageField.type = TemplateElementFieldType.IMAGE
      imageField.name = "startImage"

      Dim image As New Image

      image.type = MediaMediaType.IMAGE
      image.name = "Starting Image"

      ' Load your image into data field.
      image.data = MediaUtilities.GetAssetDataFromUrl( _
          "https://sandbox.google.com/sandboximages/image.jpg")

      imageField.fieldMedia = image

      ' Create the template field "displayUrlColor".
      Dim displayUrlColorField As New TemplateElementField
      displayUrlColorField.type = TemplateElementFieldType.ENUM
      displayUrlColorField.fieldText = "#ffffff"
      displayUrlColorField.name = "displayUrlColor"

      ' Create the template field "video".
      Dim videoField As New TemplateElementField
      videoField.type = TemplateElementFieldType.VIDEO
      videoField.name = "video"

      Dim video As New Video
      video.mediaId = videoMediaId
      video.type = MediaMediaType.VIDEO
      videoField.fieldMedia = video

      templateElement.fields = New TemplateElementField() {imageField, displayUrlColorField, _
          videoField}

      ' Set the dimension, name, url and displayurl for video ad.
      templateAd.dimensions = New Dimensions
      templateAd.dimensions.width = 300
      templateAd.dimensions.height = 250

      templateAd.name = "VideoAdTemplateExample"
      templateAd.url = "http://www.example.com"
      templateAd.displayUrl = "www.example.com"

      ' Set the AdGroup Id.
      Dim videoAdGroupAd As New AdGroupAd
      videoAdGroupAd.adGroupId = adGroupId
      videoAdGroupAd.ad = templateAd

      ' Create the ADD Operation.
      Dim videoAdOperation As New AdGroupAdOperation
      videoAdOperation.operator = [Operator].ADD
      videoAdOperation.operand = videoAdGroupAd

      Dim retVal As AdGroupAdReturnValue = Nothing
      Try
        retVal = service.mutate(New AdGroupAdOperation() {textAdOperation, imageAdOperation, _
            videoAdOperation})
        If (((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each tempAdGroupAd As AdGroupAd In retVal.value
            Console.WriteLine("New ad with id = ""{0}"" and displayUrl = ""{1}"" was created.", _
                tempAdGroupAd.ad.id, tempAdGroupAd.ad.displayUrl)
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to create Ad(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
