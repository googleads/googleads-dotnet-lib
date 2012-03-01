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
  ''' This code example gets all targeting criteria for a campaign.  To set
  ''' campaign targeting criteria, run AddCampaignTargetingCriteria.vb. To get
  ''' campaigns, run GetCampaigns.vb.
  '''
  ''' Tags: CampaignCriterionService.get
  ''' </summary>
  Class GetCampaignTargetingCriteria
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetCampaignTargetingCriteria
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all targeting criteria for a campaign.  To set campaign " & _
            "targeting criteria, run AddCampaignTargetingCriteria.vb. To get campaigns, run " & _
            "GetCampaigns.vb."
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
      ' Get the CampaignCriterionService.
      Dim campaignCriterionService As CampaignCriterionService = user.GetService( _
          AdWordsService.v201109.CampaignCriterionService)
      Dim campaignId As Long = Long.Parse(parameters("CAMPAIGN_ID"))

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "CriteriaType", "CampaignId"}

      ' Set the filters.
      Dim predicate As New Predicate
      predicate.field = "CampaignId"
      predicate.operator = PredicateOperator.EQUALS
      predicate.values = New String() {campaignId.ToString}

      selector.predicates = New Predicate() {predicate}

      ' Set the selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New CampaignCriterionPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get all campaign targets.
          page = campaignCriterionService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset
            For Each campaignCriterion As CampaignCriterion In page.entries
              Dim negative As String = ""
              If (TypeOf campaignCriterion Is NegativeCampaignCriterion) Then
                negative = "Negative "
              End If
              writer.WriteLine("{0}) {1}Campaign targeting criterion with id = '{2}' and " & _
                  "Type = {3} was found for campaign id '{4}'", i, negative, _
                  campaignCriterion.criterion.id, campaignCriterion.criterion.type, _
                  campaignCriterion.campaignId)
              i += 1
            Next
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        writer.WriteLine("Number of campaign targeting criteria found: {0}", page.totalNumEntries)
      Catch ex As Exception
        writer.WriteLine("Failed to get campaign targeting criteria. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
