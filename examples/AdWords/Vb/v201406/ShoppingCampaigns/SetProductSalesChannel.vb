' Copyright 2014, Google Inc. All Rights Reserved.
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

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201406

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201406
  ''' <summary>
  ''' This code example sets the product sales channel.
  '''
  ''' Tags: CampaignCriterionService.mutate
  ''' </summary>
  Public Class SetProductSalesChannel
    Inherits ExampleBase

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example sets the product sales channel."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New SetProductSalesChannel
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
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign for which shopping channel
    ''' is set.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the CampaignCriterionService.
      Dim campaignCriterionService As CampaignCriterionService = CType(user.GetService( _
          AdWordsService.v201406.CampaignCriterionService), CampaignCriterionService)

      ' ProductSalesChannel is a fixed id criterion, with the possible values
      ' defined here.
      ' ONLINE: 200
      ' LOCAL: 201
      Dim productSalesChannel As New ProductSalesChannel()
      productSalesChannel.id = 200

      Dim campaignCriterion As New CampaignCriterion()
      campaignCriterion.campaignId = campaignId
      campaignCriterion.criterion = productSalesChannel

      ' Create operation.
      Dim operation As New CampaignCriterionOperation()
      operation.operand = campaignCriterion
      operation.operator = [Operator].ADD

      Try
        ' Make the mutate request.
        Dim retVal As CampaignCriterionReturnValue = campaignCriterionService.mutate( _
            New CampaignCriterionOperation() {operation})

        If ((Not (retVal Is Nothing)) AndAlso (Not (retVal.value Is Nothing))) Then
          ' Display campaign targets.
          For Each criterion As CampaignCriterion In retVal.value
            Console.WriteLine("Campaign criteria of type '{0}' was set to campaign with" & _
                " id = '{1}'.", criterion.criterion.CriterionType, criterion.campaignId)
          Next
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to create shopping campaign.", ex)
      End Try
    End Sub

  End Class

End Namespace
