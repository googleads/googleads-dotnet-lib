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
Imports Google.Api.Ads.AdWords.v201109

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example adds placements to an ad group. To get ad groups, run
  ''' GetAdGroups.vb.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Class AddPlacements
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New AddPlacements
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds placements to an ad group. To get ad groups, run " & _
            "GetAdGroups.vb."
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

      Dim adGroupId As Long = Long.Parse(parameters("ADGROUP_ID"))

      ' Create the placement.
      Dim placement1 As New Placement
      placement1.url = "http://mars.google.com"

      ' Create the biddable ad group criterion.
      Dim placementCriterion1 As AdGroupCriterion = New BiddableAdGroupCriterion
      placementCriterion1.adGroupId = adGroupId
      placementCriterion1.criterion = placement1

      ' Create the placement.
      Dim placement2 As New Placement
      placement2.url = "http://venus.google.com"

      ' Create the biddable ad group criterion.
      Dim placementCriterion2 As AdGroupCriterion = New BiddableAdGroupCriterion
      placementCriterion2.adGroupId = adGroupId
      placementCriterion2.criterion = placement2

      ' Create the operations.
      Dim placementOperation1 As New AdGroupCriterionOperation
      placementOperation1.operator = [Operator].ADD
      placementOperation1.operand = placementCriterion1

      Dim placementOperation2 As New AdGroupCriterionOperation
      placementOperation2.operator = [Operator].ADD
      placementOperation2.operand = placementCriterion2

      Try
        ' Create the placements.
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            New AdGroupCriterionOperation() {placementOperation1, placementOperation2})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          For Each adGroupCriterion As AdGroupCriterion In retVal.value
            ' If you are adding multiple type of criteria, then you may need to
            ' check for
            '
            ' if (adGroupCriterion is Placement) { ... }
            '
            ' to identify the criterion type.
            writer.WriteLine("Placement with ad group id = '{0}, placement id = '{1} and url = " & _
                "'{2}' was created.", adGroupCriterion.adGroupId, _
                adGroupCriterion.criterion.id, TryCast(adGroupCriterion.criterion, Placement).url)
          Next
        Else
          writer.WriteLine("No placements were added.")
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to create placements. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
