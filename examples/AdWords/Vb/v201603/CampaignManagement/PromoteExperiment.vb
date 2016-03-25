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
  ''' This example promotes an experiment, which permanently applies all the
  ''' experiment changes made to its related ad groups, criteria and ads. To
  ''' create an experiment, run AddExperiment.vb.
  ''' </summary>
  Public Class PromoteExperiment
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New PromoteExperiment
      Console.WriteLine(codeExample.Description)
      Try
        Dim experimentId As Long = Long.Parse("INSERT_EXPERIMENT_ID_HERE")
        codeExample.Run(New AdWordsUser, experimentId)
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
        Return "This example promotes an experiment, which permanently applies all the " & _
            "experiment changes made to its related ad groups, criteria and ads. To create an " & _
            "experiment, run AddExperiment.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="experimentId">Id of the experiment to be promoted.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal experimentId As Long)
      ' Get the ExperimentService.
      Dim experimentService As ExperimentService = CType(user.GetService( _
          AdWordsService.v201603.ExperimentService), ExperimentService)

      ' Set experiment's status to PROMOTED.
      Dim experiment As New Experiment
      experiment.id = experimentId
      experiment.status = ExperimentStatus.PROMOTED

      ' Create the operation.
      Dim operation As New ExperimentOperation
      operation.operator = [Operator].SET
      operation.operand = experiment

      Try
        ' Update the experiment.
        Dim retVal As ExperimentReturnValue = experimentService.mutate( _
            New ExperimentOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim promotedExperiment As Experiment = retVal.value(0)
          Console.WriteLine("Experiment with name = ""{0}"" and id = ""{1}"" was promoted.", _
                promotedExperiment.name, promotedExperiment.id)
        Else
          Console.WriteLine("No experiments were promoted.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to promote experiment(s).", e)
      End Try
    End Sub
  End Class
End Namespace
