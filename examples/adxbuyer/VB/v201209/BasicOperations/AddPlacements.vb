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
Imports Google.Api.Ads.AdWords.v201209

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201209
  ''' <summary>
  ''' This code example adds placements to an ad group. To get ad groups, run
  ''' GetAdGroups.vb.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Public Class AddPlacements
    Inherits ExampleBase
    ''' <summary>
    ''' Number of items being added / updated in this code example.
    ''' </summary>
    Const NUM_ITEMS As Integer = 5

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddPlacements
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId)
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
        Return "This code example adds placements to an ad group. To get ad groups, run " & _
            "GetAdGroups.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group to which keywords are added.
    ''' </param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201209.AdGroupCriterionService)

      Dim operations As New List(Of AdGroupCriterionOperation)

      For i As Integer = 1 To NUM_ITEMS

        ' Create the placement.
        Dim placement As New Placement
        placement.url = "http://mars.google.com/" + i.ToString()

        ' Create the biddable ad group criterion.
        Dim placementCriterion As AdGroupCriterion = New BiddableAdGroupCriterion
        placementCriterion.adGroupId = adGroupId
        placementCriterion.criterion = placement

        ' Create the operations.
        Dim operation As New AdGroupCriterionOperation
        operation.operator = [Operator].ADD
        operation.operand = placementCriterion

        operations.Add(operation)
      Next
      Try
        ' Create the placements.
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            operations.ToArray)

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          For Each adGroupCriterion As AdGroupCriterion In retVal.value
            ' If you are adding multiple type of criteria, then you may need to
            ' check for
            '
            ' if (adGroupCriterion is Placement) { ... }
            '
            ' to identify the criterion type.
            Console.WriteLine("Placement with ad group id = '{0}, placement id = '{1} and url " & _
                "= '{2}' was created.", adGroupCriterion.adGroupId, _
                adGroupCriterion.criterion.id, TryCast(adGroupCriterion.criterion, Placement).url)
          Next
        Else
          Console.WriteLine("No placements were added.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to create placements. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
