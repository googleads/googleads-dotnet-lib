' Copyright 2015, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201509

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201509
  ''' <summary>
  ''' This code example adds various types of targeting criteria to a campaign.
  ''' To get a list of campaigns, run GetCampaigns.vb.
  ''' </summary>
  Public Class AddCampaignTargetingCriteria
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddCampaignTargetingCriteria
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        codeExample.Run(New AdWordsUser, campaignId)
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
        Return "This code example adds various types of targeting criteria to a campaign. To " & _
            "get a list of campaigns, run GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign to which targeting criteria
    ''' are added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the CampaignCriterionService.
      Dim campaignCriterionService As CampaignCriterionService = user.GetService( _
          AdWordsService.v201509.CampaignCriterionService)

      ' Create language criteria.
      ' See http://code.google.com/apis/adwords/docs/appendix/languagecodes.html
      ' for a detailed list of language codes.
      Dim language1 As New Language
      language1.id = 1002 ' French
      Dim languageCriterion1 As New CampaignCriterion
      languageCriterion1.campaignId = campaignId
      languageCriterion1.criterion = language1

      Dim language2 As New Language
      language2.id = 1005  ' Japanese
      Dim languageCriterion2 As New CampaignCriterion
      languageCriterion2.campaignId = campaignId
      languageCriterion2.criterion = language2

      ' Create location criteria.
      ' See http://code.google.com/apis/adwords/docs/appendix/countrycodes.html
      ' for a detailed list of country codes.
      Dim location1 As New Location
      location1.id = 2840  ' USA
      Dim locationCriterion1 As New CampaignCriterion
      locationCriterion1.campaignId = campaignId
      locationCriterion1.criterion = location1

      Dim location2 As New Location
      location2.id = 2392 ' Japan
      Dim locationCriterion2 As New CampaignCriterion
      locationCriterion2.campaignId = campaignId
      locationCriterion2.criterion = location2

      ' Add a negative campaign placement.
      Dim negativeCriterion As New NegativeCampaignCriterion()
      negativeCriterion.campaignId = campaignId

      Dim placement As New Placement()
      placement.url = "http://mars.google.com"

      negativeCriterion.criterion = placement

      Dim criteria() As CampaignCriterion = {languageCriterion1, languageCriterion2, _
          locationCriterion1, locationCriterion2, negativeCriterion}

      Dim operations As New List(Of CampaignCriterionOperation)

      For Each criterion As CampaignCriterion In criteria
        Dim operation As New CampaignCriterionOperation
        operation.operator = [Operator].ADD
        operation.operand = criterion
        operations.Add(operation)
      Next

      Try
        ' Set the campaign targets.
        Dim retVal As CampaignCriterionReturnValue = campaignCriterionService.mutate( _
            operations.ToArray())

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          For Each criterion As CampaignCriterion In retVal.value
            Console.WriteLine("Campaign criterion of type '{0}' was set to campaign with id " & _
                "= '{1}'.", criterion.criterion.CriterionType, criterion.campaignId)
          Next
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to set campaign criteria.", e)
      End Try
    End Sub
  End Class
End Namespace
