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
Imports Google.Api.Ads.AdWords.Util
Imports Google.Api.Ads.AdWords.v201109

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example demonstrates how to handle partial failures.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Public Class HandlePartialFailures
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New HandlePartialFailures
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example demonstrates how to handle partial failures."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"ADGROUP_ID"}
    End Function

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="parameters">The parameters for running the code
    ''' example.</param>
    ''' <param name="writer">The stream writer to which script output should be
    ''' written.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser, ByVal parameters As  _
        Dictionary(Of String, String), ByVal writer As TextWriter)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201109.AdGroupCriterionService)

      ' Set partial failure mode for the service.
      adGroupCriterionService.RequestHeader.partialFailure = True

      Dim adGroupId As Long = Long.Parse(parameters("ADGROUP_ID"))

      Dim operations As New List(Of AdGroupCriterionOperation)

      ' Create the placements.
      Dim urls As String() = New String() {"http://mars.google.com", "http:/mars.google.com", _
                                           "mars.google.com"}
      For Each url As String In urls
        Dim placement As New Placement
        placement.url = url

        ' Create biddable ad group criterion.
        Dim placementBiddableAdGroupCriterion As New BiddableAdGroupCriterion
        placementBiddableAdGroupCriterion.adGroupId = adGroupId
        placementBiddableAdGroupCriterion.criterion = placement

        ' Create the operation.
        Dim placementAdGroupCriterionOperation As New AdGroupCriterionOperation
        placementAdGroupCriterionOperation.operand = placementBiddableAdGroupCriterion
        placementAdGroupCriterionOperation.operator = [Operator].ADD
        operations.Add(placementAdGroupCriterionOperation)
      Next

      Try
        ' Create the placements.
        Dim result As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            operations.ToArray)

        ' Display the results.
        If ((Not result Is Nothing) AndAlso (Not result.value Is Nothing)) Then
          For Each adGroupCriterionResult As AdGroupCriterion In result.value
            If (Not adGroupCriterionResult.criterion Is Nothing) Then
              writer.WriteLine("Placement with ad group id '{0}', criterion id " & _
                  "'{1}', and url '{2}' was added.\n", adGroupCriterionResult.adGroupId, _
                  adGroupCriterionResult.criterion.id, _
                  DirectCast(adGroupCriterionResult.criterion, Placement).url)
            End If
          Next
        Else
          writer.WriteLine("No placements were added.")
        End If

        ' Display the partial failure errors.
        If ((Not result Is Nothing) AndAlso (Not result.partialFailureErrors Is Nothing)) Then
          For Each apiError As ApiError In result.partialFailureErrors
            Dim operationIndex As Integer = ErrorUtilities.GetOperationIndex(apiError.fieldPath)
            If (operationIndex <> -1) Then
              Dim adGroupCriterion As AdGroupCriterion = operations(operationIndex).operand
              writer.WriteLine("Placement with ad group id '{0}' and url '{1}' " & _
                  "triggered a failure for the following reason: '{2}'.\n", _
                  adGroupCriterion.adGroupId, _
                  DirectCast(adGroupCriterion.criterion, Placement).url, _
                  apiError.errorString)
            Else
              writer.WriteLine("A failure for the following reason: '{0}' has occurred.\n", _
                  apiError.errorString)
            End If
          Next
        End If
      Catch e As Exception
        writer.WriteLine("Failed to add placement(s) in partial failure mode. Exception " & _
            "says '{0}'", e.Message)
      End Try
    End Sub
  End Class
End Namespace
