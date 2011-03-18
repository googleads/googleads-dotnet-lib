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
  ''' This example gets all experiments in a campaign. To add an experiment, run
  ''' AddExperiment.vb. To get campaigns, run GetAllCampaigns.vb.
  '''
  ''' Tags: ExperimentService.mutate
  ''' </summary>
  Class GetAllExperiments
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This example gets all experiments in a campaign. To add an experiment, run " & _
            "AddExperiment.vb. To get campaigns, run GetAllCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllExperiments
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

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))

      ' Create selector.
      Dim selector As New ExperimentSelector
      selector.campaignIds = New Long() {campaignId}
      selector.includeStats = True

      Try
        ' Get all experiments.
        Dim page As ExperimentPage = experimentService.get(selector)

        ' Display results.
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          For Each experiment As Experiment In page.entries
            Dim stats As ExperimentSummaryStats = experiment.experimentSummaryStats
            Console.WriteLine("Experiment with name = ""{0}"", id = ""{1}"" and control " & _
                "id = ""{2}"" was found and it includes {3} ad group(s) and {4} criteria.\n", _
                experiment.name, experiment.id, experiment.controlId, stats.adGroupsCount, _
                stats.adGroupCriteriaCount)
          Next
        Else
          Console.WriteLine("No experiments were found." & ChrW(10))
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to get experiment(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
