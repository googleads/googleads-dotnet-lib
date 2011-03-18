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
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example shows how to add site links to an existing
  ''' campaign. To create a campaign, run AddCampaign.cs.
  '''
  ''' Tags: CampaignAdExtensionService.mutate
  ''' </summary>
  Class AddSiteLinks
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example shows how to add site links to an existing campaign. To " & _
            "create a campaign, run AddCampaign.cs."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddSiteLinks
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the CampaignAdExtensionService.
      Dim campaignExtensionService As CampaignAdExtensionService = user.GetService( _
          AdWordsService.v201008.CampaignAdExtensionService)

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))

      Dim siteLinkExtension As New SitelinksExtension

      Dim siteLink1 As New Sitelink
      siteLink1.displayText = "Music"
      siteLink1.destinationUrl = "http://www.example.com/music"

      Dim siteLink2 As New Sitelink
      siteLink2.displayText = "DVDs"
      siteLink2.destinationUrl = "http://www.example.com/dvds"

      Dim siteLink3 As New Sitelink
      siteLink3.displayText = "New albums"
      siteLink3.destinationUrl = "http://www.example.com/albums/new"

      siteLinkExtension.sitelinks = New Sitelink() {siteLink1, siteLink2, siteLink3}

      Dim campaignAdExtension As New CampaignAdExtension
      campaignAdExtension.adExtension = siteLinkExtension
      campaignAdExtension.campaignId = campaignId

      Dim operation As New CampaignAdExtensionOperation
      operation.operator = [Operator].ADD
      operation.operand = campaignAdExtension

      Try
        Dim retVal As CampaignAdExtensionReturnValue = campaignExtensionService.mutate( _
            New CampaignAdExtensionOperation() {operation})
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each campaignExtension As CampaignAdExtension In retVal.value
            Console.WriteLine("Created a campaign ad extension with id = ""{0}"" and status " & _
                "= ""{1}""", campaignExtension.adExtension.id, campaignExtension.status)
            For Each siteLink As Sitelink In _
                TryCast(campaignExtension.adExtension, SitelinksExtension).sitelinks
              Console.WriteLine("-- Site link text is ""{0}"" and destination url is {1}", _
                  siteLink.displayText, siteLink.destinationUrl)
            Next
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to add site links. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
