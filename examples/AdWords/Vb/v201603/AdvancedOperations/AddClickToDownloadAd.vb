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
  ''' This code example creates a click-to-download ad, also known as an
  ''' app promotion ad to a given ad group. To list ad groups, run
  ''' GetAdGroups.vb.
  ''' </summary>
  Public Class AddClickToDownloadAd
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddClickToDownloadAd
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
        Return "This code example creates a click-to-download ad, also known as an app " & _
            "promotion ad to a given ad group. To list ad groups, run GetAdGroups.vb."
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
      Dim adGroupAdService As AdGroupAdService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupAdService), AdGroupAdService)

      ' Create the template ad.
      Dim clickToDownloadAppAd As New TemplateAd()

      clickToDownloadAppAd.name = "Ad for demo game"
      clickToDownloadAppAd.templateId = 353
      clickToDownloadAppAd.finalUrls = New String() { _
          "http://play.google.com/store/apps/details?id=com.example.demogame" _
      }
      clickToDownloadAppAd.displayUrl = "play.google.com"

      ' Create the template elements for the ad. You can refer to
      ' https://developers.google.com/adwords/api/docs/appendix/templateads
      ' for the list of avaliable template fields.
      Dim headline As New TemplateElementField()
      headline.name = "headline"
      headline.fieldText = "Enjoy your drive in Mars"
      headline.type = TemplateElementFieldType.TEXT

      Dim description1 As New TemplateElementField()
      description1.name = "description1"
      description1.fieldText = "Realistic physics simulation"
      description1.type = TemplateElementFieldType.TEXT

      Dim description2 As New TemplateElementField()
      description2.name = "description2"
      description2.fieldText = "Race against players online"
      description2.type = TemplateElementFieldType.TEXT

      Dim appId As New TemplateElementField()
      appId.name = "appId"
      appId.fieldText = "com.example.demogame"
      appId.type = TemplateElementFieldType.TEXT

      Dim appStore As New TemplateElementField()
      appStore.name = "appStore"
      appStore.fieldText = "2"
      appStore.type = TemplateElementFieldType.ENUM

      Dim adData As New TemplateElement()
      adData.uniqueName = "adData"
      adData.fields = New TemplateElementField() {headline, description1, description2, appId, _
          appStore}

      clickToDownloadAppAd.templateElements = New TemplateElement() {adData}

      ' Create the adgroupad.
      Dim clickToDownloadAppAdGroupAd As New AdGroupAd()
      clickToDownloadAppAdGroupAd.adGroupId = adGroupId
      clickToDownloadAppAdGroupAd.ad = clickToDownloadAppAd

      ' Optional: Set the status.
      clickToDownloadAppAdGroupAd.status = AdGroupAdStatus.PAUSED

      ' Create the operation.
      Dim operation As New AdGroupAdOperation()
      operation.operator = [Operator].ADD
      operation.operand = clickToDownloadAppAdGroupAd

      Try
        ' Create the ads.
        Dim retval As AdGroupAdReturnValue = adGroupAdService.mutate(New AdGroupAdOperation() _
                                                                     {operation})

        ' Display the results.
        If Not retval Is Nothing AndAlso Not retval.value Is Nothing Then
          For Each adGroupAd As AdGroupAd In retval.value
            Console.WriteLine("New click-to-download ad with id = '{0}' and url = '{1}' " & _
                "was created.", adGroupAd.ad.id, adGroupAd.ad.finalUrls(0))
          Next
        Else
          Console.WriteLine("No click-to-download ads were created.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create click-to-download ad.", e)
      End Try
    End Sub
  End Class

End Namespace
