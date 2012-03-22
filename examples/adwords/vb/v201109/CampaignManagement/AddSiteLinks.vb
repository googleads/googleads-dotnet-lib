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
  ''' This code example shows how to add site links to an existing
  ''' campaign. To create a campaign, run AddCampaign.vb.
  '''
  ''' Tags: CampaignAdExtensionService.mutate
  ''' </summary>
  Public Class AddSiteLinks
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New AddSiteLinks
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
        Return "This code example shows how to add site links to an existing campaign. To " & _
            "create a campaign, run AddCampaign.vb."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"CAMPAIGN_ID"}
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
      ' Get the CampaignAdExtensionService.
      Dim campaignExtensionService As CampaignAdExtensionService = user.GetService( _
          AdWordsService.v201109.CampaignAdExtensionService)

      Dim campaignId As Long = Long.Parse(parameters("CAMPAIGN_ID"))

      ' Create the sitelinks.
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

      ' Create the operation.
      Dim operation As New CampaignAdExtensionOperation
      operation.operator = [Operator].ADD
      operation.operand = campaignAdExtension

      Try
        ' Create the sitelinks.
        Dim retVal As CampaignAdExtensionReturnValue = campaignExtensionService.mutate( _
            New CampaignAdExtensionOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim campaignExtension As CampaignAdExtension = retVal.value(0)
          writer.WriteLine("Created a campaign ad extension with id = ""{0}"" and status " & _
                "= ""{1}""", campaignExtension.adExtension.id, campaignExtension.status)
          For Each siteLink As Sitelink In _
              TryCast(campaignExtension.adExtension, SitelinksExtension).sitelinks
            writer.WriteLine("-- Site link text is ""{0}"" and destination url is {1}", _
                siteLink.displayText, siteLink.destinationUrl)
          Next
        Else
          writer.WriteLine("No sitelinks were created.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to add site links.", ex)
      End Try
    End Sub
  End Class
End Namespace
