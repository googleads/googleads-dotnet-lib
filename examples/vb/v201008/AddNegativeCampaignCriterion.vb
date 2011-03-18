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
  ''' This code example creates a new negative campaign criterion. To create
  ''' campaign, run AddCampaign.vb.
  '''
  ''' Tags: CampaignCriterionService.mutate
  ''' </summary>
  Class AddNegativeCampaignCriterion
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example creates a new negative campaign criterion. To create " & _
            "campaign, run AddCampaign.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddNegativeCampaignCriterion
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the CampaignCriterionService.
      Dim campaignCriterionService As CampaignCriterionService = _
          user.GetService(AdWordsService.v201008.CampaignCriterionService)

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))

      Dim negativeCriterion As New NegativeCampaignCriterion
      negativeCriterion.campaignId = campaignId

      Dim keyword As New Keyword
      keyword.matchType = KeywordMatchType.BROAD
      keyword.text = "jupiter cruise"
      negativeCriterion.criterion = keyword

      Dim operation As New CampaignCriterionOperation
      operation.operator = [Operator].ADD
      operation.operand = negativeCriterion

      Try
        Dim retVal As CampaignCriterionReturnValue = campaignCriterionService.mutate( _
            New CampaignCriterionOperation() {operation})
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          For Each campaignCriterion As CampaignCriterion In retVal.value
            Dim tempKeyword As Keyword = campaignCriterion.criterion
            Console.WriteLine("New negative campaign criterion with id = '{0}' and text = " & _
                "'{1}' was added to campaign with id = '{2}'.", tempKeyword.id, tempKeyword.text, _
                campaignCriterion.campaignId)
          Next
        Else
          Console.WriteLine("No negative campaign criterion was added.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to add negative campaign criteria. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
