' Copyright 2013, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201309

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201309
  ''' <summary>
  ''' This code example deletes a placement using the 'REMOVE' operator. To get
  ''' placements, run GetPlacements.vb.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Public Class DeletePlacement
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New DeletePlacement
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        Dim placementId As Long = Long.Parse("INSERT_PLACEMENT_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId, placementId)
      Catch ex As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(ex))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example deletes a placement using the 'REMOVE' operator. To get " & _
            "placements, run GetPlacements.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group that contains the keyword.
    ''' </param>
    ''' <param name="placementId">Id of the placement to be deleted.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long, ByVal placementId As Long)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201309.AdGroupCriterionService)

      ' Create base class criterion to avoid setting placement-specific
      ' fields.
      Dim criterion As New Criterion
      criterion.id = placementId

      ' Create the ad group criterion.
      Dim adGroupCriterion As New BiddableAdGroupCriterion
      adGroupCriterion.adGroupId = adGroupId
      adGroupCriterion.criterion = criterion

      ' Create the operation.
      Dim operation As New AdGroupCriterionOperation
      operation.operand = adGroupCriterion
      operation.operator = [Operator].REMOVE

      Try
        ' Delete the placement.
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            New AdGroupCriterionOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim deletedPlacement As AdGroupCriterion = retVal.value(0)
          Console.WriteLine("Placement with ad group id = ""{0}"" and id = ""{1}"" was " & _
                "deleted.", deletedPlacement.adGroupId, deletedPlacement.criterion.id)
        Else
          Console.WriteLine("No placement was deleted.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to delete placement. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
