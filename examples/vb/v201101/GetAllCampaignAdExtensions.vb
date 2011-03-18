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

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example shows how to retrieve all Ad Extensions in a Campaign.
  ''' To create a Campaign Ad Extension, run AddCampaignAdExtensionOverride.vb.
  '''
  ''' Tags: CampaignAdExtensionService.get
  ''' </summary>
  Class GetAllCampaignAdExtensions
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example shows how to retrieve all Ad Extensions in a campaign. " & _
            "To create a Campaign Ad Extension, run AddCampaignAdExtensionOverride.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllCampaignAdExtensions
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the CampaignAdExtensionService.
      Dim campaignExtensionService As CampaignAdExtensionService = _
          user.GetService(AdWordsService.v201101.CampaignAdExtensionService)

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))

      ' Create a selector.
      Dim selector As New Selector
      selector.fields = New String() {"AdExtensionId", "Status"}

      ' Set the filters.
      Dim campaignPredicate As New Predicate
      campaignPredicate.operator = PredicateOperator.EQUALS
      campaignPredicate.field = "CampaignId"
      campaignPredicate.values = New String() {campaignId.ToString}

      selector.predicates = new Predicate() {campaignPredicate}

      ' Specify a sort order by AdExtensionId.
      Dim orderBy As New OrderBy
      orderBy.field = "AdExtensionId"
      orderBy.sortOrder = SortOrder.ASCENDING
      selector.ordering = New OrderBy() {orderBy}

      Try
        Dim page As CampaignAdExtensionPage = campaignExtensionService.get(selector)
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          Console.WriteLine("Retrieved {0} out of {1} entries.", page.entries.Length, _
              page.totalNumEntries)
          For Each campaignExtension As CampaignAdExtension In page.entries
            Console.WriteLine("Campaign ad extension id is ""{0}"" and status is  ""{1}""", _
                campaignExtension.adExtension.id, campaignExtension.status)
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve campaign ad extensions. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
