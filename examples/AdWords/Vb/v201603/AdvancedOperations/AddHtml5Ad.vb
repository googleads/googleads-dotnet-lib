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
Imports Google.Api.Ads.Common.Util

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603

  ''' <summary>
  ''' This code example adds an HTML5 ad to a given ad group. To get ad
  ''' groups, run GetAdGroups.vb.
  ''' </summary>
  Public Class AddHtml5Ad
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddHtml5Ad
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        codeExample.Run(New AdWordsUser(), adGroupId)
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
        Return "This code example adds an HTML5 ad to a given ad group. To get ad" & _
            "groups, run GetAdGroups.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the first adgroup to which ad is added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupAdService), AdGroupAdService)

      ' Create the HTML5 template ad. See
      ' https://developers.google.com/adwords/api/docs/guides/template-ads#html5_ads
      ' for more details.
      Dim html5Ad As New TemplateAd()
      html5Ad.name = "Ad for HTML5"
      html5Ad.templateId = 419
      html5Ad.finalUrls = New String() {"http://example.com/html5"}
      html5Ad.displayUrl = "www.example.com/html5"

      html5Ad.dimensions = New Dimensions()
      html5Ad.dimensions.width = 300
      html5Ad.dimensions.height = 250

      ' The HTML5 zip file contains all the HTML, CSS, and images needed for the
      ' HTML5 ad. For help on creating an HTML5 zip file, check out Google Web
      ' Designer (https://www.google.com/webdesigner/).
      Dim html5Zip As Byte() = MediaUtilities.GetAssetDataFromUrl("https://goo.gl/9Y7qI2")

      ' Create a media bundle containing the zip file with all the HTML5 components.
      Dim mediaBundle As New MediaBundle()
      ' You may also upload an HTML5 zip using MediaService.upload() method
      ' set the mediaId field. See UploadMediaBundle.vb for an example on how to
      ' upload HTML5 zip files.
      mediaBundle.data = html5Zip
      mediaBundle.entryPoint = "carousel/index.html"
      mediaBundle.type = MediaMediaType.MEDIA_BUNDLE

      ' Create the template elements for the ad. You can refer to
      ' https://developers.google.com/adwords/api/docs/appendix/templateads
      ' for the list of available template fields.

      Dim adData As New TemplateElement
      adData.uniqueName = "adData"

      Dim customLayout As New TemplateElementField
      customLayout.name = "Custom_layout"
      customLayout.fieldMedia = mediaBundle
      customLayout.type = TemplateElementFieldType.MEDIA_BUNDLE

      Dim layout As New TemplateElementField
      layout.name = "layout"
      layout.fieldText = "Custom"
      layout.type = TemplateElementFieldType.ENUM

      adData.fields = New TemplateElementField() {customLayout, layout}

      html5Ad.templateElements = New TemplateElement() {adData}

      ' Create the AdGroupAd.
      Dim html5AdGroupAd As New AdGroupAd()
      html5AdGroupAd.adGroupId = adGroupId
      html5AdGroupAd.ad = html5Ad
      ' Additional properties (non-required).
      html5AdGroupAd.status = AdGroupAdStatus.PAUSED

      Dim adGroupAdOperation As New AdGroupAdOperation()
      adGroupAdOperation.operator = [Operator].ADD
      adGroupAdOperation.operand = html5AdGroupAd

      Try
        ' Add HTML5 ad.
        Dim result As AdGroupAdReturnValue = _
            service.mutate(New AdGroupAdOperation() {adGroupAdOperation})

        ' Display results.
        If (Not result Is Nothing) AndAlso (Not result.value Is Nothing) AndAlso _
            (result.value.Length > 0) Then
          For Each adGroupAd As AdGroupAd In result.value
            Console.WriteLine("New HTML5 ad with id '{0}' and display url '{1}' was added.", _
              adGroupAd.ad.id, adGroupAd.ad.displayUrl)
          Next
        Else
          Console.WriteLine("No HTML5 ads were added.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create HTML5 ad.", e)
      End Try
    End Sub
  End Class
End Namespace
