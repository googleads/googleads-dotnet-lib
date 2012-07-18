' Copyright 2012, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201206

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201206
  ''' <summary>
  ''' This code example shows how to delete site links from an existing
  ''' campaign. To add site links to an existing campaign, run AddSiteLinks.vb.
  ''' To get existing campaigns, run GetCampaigns.vb.
  '''
  ''' Tags: CampaignAdExtensionService.mutate
  ''' </summary>
  Public Class DeleteSitelinks
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New DeleteSitelinks
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        codeExample.Run(New AdWordsUser, campaignId)
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
        Return "This code example shows how to remove site links from an existing campaign. " & _
            "To add site links to an existing campaign, run AddSiteLinks.vb. To get existing " & _
            "campaigns, run GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign from which sitelinks are
    ''' deleted.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the CampaignAdExtensionService.
      Dim campaignExtensionService As CampaignAdExtensionService = user.GetService( _
          AdWordsService.v201206.CampaignAdExtensionService)

      Dim siteLinkExtensionId As Long = -1

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"AdExtensionId", "Status"}

      ' Filter the results for specified campaign id.
      Dim campaignPredicate As New Predicate
      campaignPredicate.operator = PredicateOperator.EQUALS
      campaignPredicate.field = "CampaignId"
      campaignPredicate.values = New String() {campaignId.ToString}

      ' Filter the results for active campaign ad extensions.
      Dim statusPredicate As New Predicate
      statusPredicate.operator = PredicateOperator.EQUALS
      statusPredicate.field = "Status"
      statusPredicate.values = New String() {CampaignAdExtensionStatus.ACTIVE.ToString}

      ' Filter for sitelinks ad extension type.
      Dim typePredicate As New Predicate
      typePredicate.operator = PredicateOperator.EQUALS
      typePredicate.field = "AdExtensionType"
      typePredicate.values = New String() {"SITELINKS_EXTENSION"}

      selector.predicates = New Predicate() {campaignPredicate, statusPredicate, typePredicate}

      ' Get the campaign ad extension containing sitelinks.
      Dim page As CampaignAdExtensionPage = campaignExtensionService.get(selector)
      If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing) AndAlso _
          (page.entries.Length > 0)) Then
        siteLinkExtensionId = page.entries(0).adExtension.id
      End If

      ' There are no site link extensions in this campaign.
      If (siteLinkExtensionId = -1) Then
        Return
      End If

      Dim campaignAdExtension As New CampaignAdExtension
      campaignAdExtension.campaignId = campaignId
      campaignAdExtension.adExtension = New AdExtension
      campaignAdExtension.adExtension.id = siteLinkExtensionId

      Dim operation As New CampaignAdExtensionOperation
      operation.operator = [Operator].REMOVE
      operation.operand = campaignAdExtension

      Try
        Dim retVal As CampaignAdExtensionReturnValue = campaignExtensionService.mutate( _
            New CampaignAdExtensionOperation() {operation})
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim campaignExtension As CampaignAdExtension = retVal.value(0)
          Console.WriteLine("Deleted a campaign ad extension with id = ""{0}"" and status " & _
              "= ""{1}""", campaignExtension.adExtension.id, campaignExtension.status)
          For Each siteLink As Sitelink In TryCast(campaignExtension.adExtension,  _
              SitelinksExtension).sitelinks
            Console.WriteLine("-- Site link text is ""{0}"" and destination url is {1}", _
                siteLink.displayText, siteLink.destinationUrl)
          Next
        Else
          Console.WriteLine("No site links were deleted.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to delete site links.", ex)
      End Try
    End Sub
  End Class
End Namespace
