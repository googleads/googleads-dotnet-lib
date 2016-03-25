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
  ''' This code example creates an experiment using a query percentage of 10,
  ''' which defines what fraction of auctions should go to the control split
  ''' (90%) vs. the experiment split (10%), then adds experimental bid changes
  ''' for criteria and ad groups. To get campaigns, run GetCampaigns.vb.
  ''' To get ad groups, run GetAdGroups.vb. To get criteria, run
  ''' GetKeywords.vb.
  ''' </summary>
  Public Class AddExperiment
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddExperiment
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        Dim criterionId As Long = Long.Parse("INSERT_CRITERION_ID_HERE")

        codeExample.Run(New AdWordsUser, campaignId, adGroupId, criterionId)
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
        Return "This code example creates an experiment using a query percentage of 10, which " & _
            "defines what fraction of auctions should go to the control split (90%) vs. the " & _
            "experiment split (10%), then adds experimental bid changes for criteria and ad " & _
            "groups. To get campaigns, run GetCampaigns.vb. To get ad groups, run " & _
            "GetAdGroups.vb. To get criteria, run GetKeywords.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign to which experiments are
    ''' added.</param>
    ''' <param name="adGroupId">Id of the ad group to which experiments are
    ''' added.</param>
    ''' <param name="criterionId">Id of the criterion for which experiments
    ''' are added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long, ByVal adGroupId As Long, _
        ByVal criterionId As Long)
      ' Get the ExperimentService.
      Dim experimentService As ExperimentService = CType(user.GetService( _
          AdWordsService.v201603.ExperimentService), ExperimentService)

      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupService), AdGroupService)

      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupCriterionService), AdGroupCriterionService)

      ' Create the experiment.
      Dim experiment As New Experiment
      experiment.campaignId = campaignId
      experiment.name = ("Interplanetary Cruise #" & ExampleUtilities.GetRandomString)
      experiment.queryPercentage = 10
      experiment.startDateTime = DateTime.Now.AddDays(1).ToString("yyyyMMdd HHmmss")

      ' Optional: Set the end date.
      experiment.endDateTime = DateTime.Now.AddDays(30).ToString("yyyyMMdd HHmmss")

      ' Optional: Set the status.
      experiment.status = ExperimentStatus.ENABLED

      ' Create the operation.
      Dim experimentOperation As New ExperimentOperation
      experimentOperation.operator = [Operator].ADD
      experimentOperation.operand = experiment

      Try
        ' Add the experiment.
        Dim experimentRetVal As ExperimentReturnValue = experimentService.mutate( _
            New ExperimentOperation() {experimentOperation})

        'Display the results.
        If ((Not experimentRetVal Is Nothing) AndAlso (Not experimentRetVal.value Is Nothing) _
            AndAlso (experimentRetVal.value.Length > 0)) Then
          Dim experimentId As Long = 0

          Dim newExperiment As Experiment = experimentRetVal.value(0)

          Console.WriteLine("Experiment with name = ""{0}"" and id = ""{1}"" was added.", _
              newExperiment.name, newExperiment.id)
          experimentId = newExperiment.id

          ' Set ad group for the experiment.
          Dim adGroup As New AdGroup
          adGroup.id = adGroupId

          ' Create experiment bid multiplier rule that will modify ad group bid
          ' for the experiment.
          Dim adGroupBidMultiplier As New ManualCPCAdGroupExperimentBidMultipliers
          adGroupBidMultiplier.maxCpcMultiplier = New BidMultiplier
          adGroupBidMultiplier.maxCpcMultiplier.multiplier = 1.5

          ' Set experiment data to the ad group.
          Dim adGroupExperimentData As New AdGroupExperimentData
          adGroupExperimentData.experimentId = experimentId
          adGroupExperimentData.experimentDeltaStatus = ExperimentDeltaStatus.MODIFIED
          adGroupExperimentData.experimentBidMultipliers = adGroupBidMultiplier

          adGroup.experimentData = adGroupExperimentData

          ' Create the operation.
          Dim adGroupOperation As New AdGroupOperation
          adGroupOperation.operand = adGroup
          adGroupOperation.operator = [Operator].SET

          ' Update the ad group.
          Dim adGroupRetVal As AdGroupReturnValue = adGroupService.mutate( _
              New AdGroupOperation() {adGroupOperation})

          ' Display the results.
          If ((Not adGroupRetVal Is Nothing) AndAlso (Not adGroupRetVal.value Is Nothing) _
              AndAlso (adGroupRetVal.value.Length > 0)) Then
            Dim updatedAdGroup As AdGroup = adGroupRetVal.value(0)
            Console.WriteLine("Ad group with name = ""{0}"", id = ""{1}"" and status = " & _
                  """{2}"" was updated for the experiment.", updatedAdGroup.name, _
                  updatedAdGroup.id, updatedAdGroup.status)
          Else
            Console.WriteLine("No ad groups were updated.")
          End If

          ' Set ad group criteria for the experiment.
          Dim criterion As New Criterion
          criterion.id = criterionId

          Dim adGroupCriterion As New BiddableAdGroupCriterion
          adGroupCriterion.adGroupId = adGroupId
          adGroupCriterion.criterion = criterion

          ' Create experiment bid multiplier rule that will modify criterion bid
          ' for the experiment.
          Dim bidMultiplier As New ManualCPCAdGroupCriterionExperimentBidMultiplier
          bidMultiplier.maxCpcMultiplier = New BidMultiplier
          bidMultiplier.maxCpcMultiplier.multiplier = 1.5

          ' Set experiment data to the criterion.
          Dim adGroupCriterionExperimentData As New BiddableAdGroupCriterionExperimentData
          adGroupCriterionExperimentData.experimentId = experimentId
          adGroupCriterionExperimentData.experimentDeltaStatus = ExperimentDeltaStatus.MODIFIED
          adGroupCriterionExperimentData.experimentBidMultiplier = bidMultiplier

          adGroupCriterion.experimentData = adGroupCriterionExperimentData

          ' Create the operation.
          Dim adGroupCriterionOperation As New AdGroupCriterionOperation
          adGroupCriterionOperation.operand = adGroupCriterion
          adGroupCriterionOperation.operator = [Operator].SET

          ' Update the ad group criteria.
          Dim adGroupCriterionRetVal As AdGroupCriterionReturnValue = _
              adGroupCriterionService.mutate(New AdGroupCriterionOperation() _
                  {adGroupCriterionOperation})

          ' Display the results.
          If ((Not adGroupCriterionRetVal Is Nothing) AndAlso _
              (Not adGroupCriterionRetVal.value Is Nothing) AndAlso _
              (adGroupCriterionRetVal.value.Length > 0)) Then
            Dim updatedAdGroupCriterion As AdGroupCriterion = adGroupCriterionRetVal.value(0)
            Console.WriteLine("Ad group criterion with ad group id = ""{0}"", criterion id =" & _
                  " ""{1}"" and type = ""{2}"" was updated for the experiment.", _
                  updatedAdGroupCriterion.adGroupId, updatedAdGroupCriterion.criterion.id, _
                  updatedAdGroupCriterion.criterion.CriterionType)
          Else
            Console.WriteLine("No ad group criteria were updated.")
          End If
        Else
          Console.WriteLine("No experiments were added.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add experiment(s).", e)
      End Try
    End Sub
  End Class
End Namespace
