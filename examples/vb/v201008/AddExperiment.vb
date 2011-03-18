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
  ''' This code example creates an experiment using a query percentage of 10,
  ''' which defines what fraction of auctions should go to the control split
  ''' (90%) vs. the experiment split (10%), then adds experimental bid changes
  ''' for criteria and ad groups. To get campaigns, run GetAllCampaigns.vb.
  ''' To get ad groups, run GetAllAdGroups.vb. To get criteria, run
  ''' GetAllAdGroupCriteria.vb.
  '''
  ''' Tags: ExperimentService.mutate
  ''' </summary>
  Class AddExperiment
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example creates an experiment using a query percentage of 10, which " & _
            "defines what fraction of auctions should go to the control split (90%) vs. the " & _
            "experiment split (10%), then adds experimental bid changes for criteria and ad " & _
            "groups. To get campaigns, run GetAllCampaigns.vb. To get ad groups, run " & _
            "GetAllAdGroups.vb. To get criteria, run GetAllAdGroupCriteria.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddExperiment
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the ExperimentService.
      Dim experimentService As ExperimentService = user.GetService( _
          AdWordsService.v201008.ExperimentService)

      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = user.GetService(AdWordsService.v201008.AdGroupService)

      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201008.AdGroupCriterionService)

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))
      Dim adGroupId As Long = Long.Parse(_T("INSERT_AD_GROUP_ID_HERE"))
      Dim criterionId As Long = Long.Parse(_T("INSERT_CRITERION_ID_HERE"))

      ' Create experiment.
      Dim experiment As New Experiment
      experiment.campaignId = campaignId
      experiment.name = ("Interplanetary Cruise #" & GetTimeStamp)
      experiment.queryPercentage = 10
      experiment.startDateTime = DateTime.Now.ToString("yyyyMMdd HHmmss")

      ' Create operation.
      Dim experimentOperation As New ExperimentOperation
      experimentOperation.operator = [Operator].ADD
      experimentOperation.operand = experiment

      Try
        ' Add experiment.
        Dim experimentRetVal As ExperimentReturnValue = experimentService.mutate( _
            New ExperimentOperation() {experimentOperation})
        If (((Not experimentRetVal Is Nothing) AndAlso (Not experimentRetVal.value Is Nothing)) _
            AndAlso (experimentRetVal.value.Length > 0)) Then
          Dim experimentId As Long = 0

          For Each tempExperiment As Experiment In experimentRetVal.value
            Console.WriteLine("Experiment with name = ""{0}"" and id = ""{1}"" was added.", _
                tempExperiment.name, tempExperiment.id)
            experimentId = tempExperiment.id
          Next

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

          ' Create operation.
          Dim adGroupOperation As New AdGroupOperation
          adGroupOperation.operand = adGroup
          adGroupOperation.operator = [Operator].SET

          ' Update ad group.
          Dim adGroupRetVal As AdGroupReturnValue = adGroupService.mutate( _
              New AdGroupOperation() {adGroupOperation})

          ' Display results.
          If ((Not adGroupRetVal Is Nothing) AndAlso (Not adGroupRetVal.value Is Nothing)) Then
            For Each tempAdGroup As AdGroup In adGroupRetVal.value
              Console.WriteLine("Ad group with name = ""{0}"", id = ""{1}"" and status = " & _
                  """{2}"" was updated for the experiment.", tempAdGroup.name, _
                  tempAdGroup.id, tempAdGroup.status)
            Next
          Else
            Console.WriteLine("No ad groups were updated." & ChrW(10))
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

          ' Create operation.
          Dim adGroupCriterionOperation As New AdGroupCriterionOperation
          adGroupCriterionOperation.operand = adGroupCriterion
          adGroupCriterionOperation.operator = [Operator].SET

          ' Update ad group criteria.
          Dim adGroupCriterionRetVal As AdGroupCriterionReturnValue = _
              adGroupCriterionService.mutate(New AdGroupCriterionOperation() _
                  {adGroupCriterionOperation})

          ' Display results.
          If ((Not adGroupCriterionRetVal Is Nothing) AndAlso _
              (Not adGroupCriterionRetVal.value Is Nothing)) Then
            For Each tempAdGroupCriterion As AdGroupCriterion In adGroupCriterionRetVal.value
              Console.WriteLine("Ad group criterion with ad group id = ""{0}"", criterion id =" & _
                  " ""{1}"" and type = ""{2}"" was updated for the experiment.", _
                  tempAdGroupCriterion.adGroupId, tempAdGroupCriterion.criterion.id, _
                  tempAdGroupCriterion.criterion.CriterionType)
            Next
          Else
            Console.WriteLine("No ad group criteria were updated.\n")
          End If
        Else
          Console.WriteLine("No experiments were added.\n")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to add experiment(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
