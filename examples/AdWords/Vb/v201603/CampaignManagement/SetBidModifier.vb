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
  ''' This code example sets a bid modifier for the mobile platform on given
  ''' campaign. The campaign must be an enhanced type of campaign. To get
  ''' campaigns, run GetCampaigns.vb. To enhance a campaign, run
  ''' SetCampaignEnhanced.vb.
  ''' </summary>
  Public Class SetBidModifier
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New SetBidModifier
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        Dim bidModifier As Double = Double.Parse("INSERT_BID_MODIFIER_HERE")
        codeExample.Run(New AdWordsUser, campaignId, bidModifier)
      Catch e As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(e))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    '''
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example sets a bid modifier for the mobile platform on given " & _
            "campaign. The campaign must be an enhanced type of campaign. To get campaigns, " & _
            "run GetCampaigns.vb. To enhance a campaign, run SetCampaignEnhanced.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the specified user.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign whose bid should be modified.
    ''' </param>
    ''' <param name="bidModifier">The bid modifier.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long, ByVal bidModifier As Double)
      ' Get the CampaignCriterionService.
      Dim campaignCriterionService As CampaignCriterionService = CType(user.GetService( _
          AdWordsService.v201603.CampaignCriterionService),  _
          CampaignCriterionService)

      ' Create mobile platform. The ID can be found in the documentation.
      ' https://developers.google.com/adwords/api/docs/appendix/platforms
      Dim mobile As New Platform()
      mobile.id = 30001

      ' Create criterion with modified bid.
      Dim criterion As New CampaignCriterion()
      criterion.campaignId = campaignId
      criterion.criterion = mobile
      criterion.bidModifier = bidModifier

      ' Create SET operation.
      Dim operation As New CampaignCriterionOperation()
      operation.operator = [Operator].SET
      operation.operand = criterion

      Try
        ' Update campaign criteria.
        Dim result As CampaignCriterionReturnValue = campaignCriterionService.mutate( _
            New CampaignCriterionOperation() {operation})

        ' Display campaign criteria.
        If Not result.value Is Nothing Then
          For Each newCriterion As CampaignCriterion In result.value
            Console.WriteLine("Campaign criterion with campaign id '{0}', criterion id '{1}' " & _
                "and type '{2}' was modified with bid {3:F2}.", newCriterion.campaignId, _
                newCriterion.criterion.id, newCriterion.criterion.type, newCriterion.bidModifier)
          Next
        Else
          Console.WriteLine("No campaigns were modified.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to set bid modifier.", e)
      End Try
    End Sub
  End Class
End Namespace