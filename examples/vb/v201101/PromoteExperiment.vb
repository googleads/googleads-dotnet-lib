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
  ''' This example promotes an experiment, which permanently applies all the
  ''' experiment changes made to its related ad groups, criteria and ads. To get
  ''' experiments, run GetAllExperiments.vb.
  '''
  ''' Tags: ExperimentService.mutate
  ''' </summary>
  Class PromoteExperiment
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This example promotes an experiment, which permanently applies all the " & _
            "experiment changes made to its related ad groups, criteria and ads. To get " & _
            "experiments, run GetAllExperiments.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New PromoteExperiment
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
          AdWordsService.v201101.ExperimentService)

      Dim experimentId As Long = Long.Parse(_T("INSERT_EXPERIMENT_ID_HERE"))

      ' Set experiment's status to PROMOTED.
      Dim experiment As New Experiment
      experiment.id = experimentId
      experiment.status = ExperimentStatus.PROMOTED

      ' Create operation.
      Dim operation As New ExperimentOperation
      operation.operator = [Operator].SET
      operation.operand = experiment

      Try
        ' Update experiment.
        Dim retVal As ExperimentReturnValue = experimentService.mutate( _
            New ExperimentOperation() {operation})

        ' Display results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          For Each tempExperiment As Experiment In retVal.value
            Console.WriteLine("Experiment with name = ""{0}"" and id = ""{1}"" was promoted.\n", _
                tempExperiment.name, tempExperiment.id)
          Next
        Else
          Console.WriteLine("No experiments were promoted.\n")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to promote experiment(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
