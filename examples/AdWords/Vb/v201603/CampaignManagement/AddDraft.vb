' Copyright 2016, Google Inc. All Rights Reserved.
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

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example illustrates how to create a draft and access its
  ''' associated draft campaign. See the Campaign Drafts and Experiments guide
  ''' for more information:
  ''' https://developers.google.com/adwords/api/docs/guides/campaign-drafts-experiments
  ''' </summary>
  Public Class AddDraft
    Inherits ExampleBase
    
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddDraft
      Console.WriteLine(codeExample.Description)
      Try
        Dim baseCampaignId As Long = Long.Parse("INSERT_BASE_CAMPAIGN_ID_HERE")

        codeExample.Run(New AdWordsUser, baseCampaignId)
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
        Return "This code example adds a label to multiple campaigns."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="baseCampaignId">Id of the base campaign for creating draft.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal baseCampaignId As Long)
      ' Get the DraftService.
      Dim draftService As DraftService = CType(user.GetService( _
          AdWordsService.v201603.DraftService), DraftService)
      Dim draft As New Draft()
      draft.baseCampaignId = baseCampaignId
      draft.draftName = "Test Draft #" + ExampleUtilities.GetRandomString()

      Dim draftOperation As New DraftOperation()
      draftOperation.operator = [Operator].ADD
      draftOperation.operand = draft

      Try
        draft = draftService.mutate(New DraftOperation() {draftOperation}).value(0)

        Console.WriteLine("Draft with ID {0}, base campaign ID {1} and draft campaign ID " & _
            "{2} created.", draft.draftId, draft.baseCampaignId, draft.draftCampaignId)

        ' Once the draft is created, you can modify the draft campaign as if it
        ' were a real campaign. For example, you may add criteria, adjust bids,
        ' or even include additional ads. Adding a criterion is shown here.
        Dim campaignCriterionService As CampaignCriterionService =
            CType(user.GetService(AdWordsService.v201603.CampaignCriterionService),  _
                CampaignCriterionService)

        Dim language As New Language()
        language.id = 1003L ' Spanish

        ' Make sure to use the draftCampaignId when modifying the virtual draft
        ' campaign.
        Dim campaignCriterion As New CampaignCriterion()
        campaignCriterion.campaignId = draft.draftCampaignId
        campaignCriterion.criterion = language

        Dim criterionOperation As New CampaignCriterionOperation()
        criterionOperation.operator = [Operator].ADD
        criterionOperation.operand = campaignCriterion

        campaignCriterion = campaignCriterionService.mutate(
            New CampaignCriterionOperation() {criterionOperation}).value(0)

        Console.WriteLine("Draft updated to include criteria in draft campaign ID {0}.",
            draft.draftCampaignId)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create draft campaign and add " & _
            "criteria.", e)
      End Try
    End Sub
  End Class
End Namespace
