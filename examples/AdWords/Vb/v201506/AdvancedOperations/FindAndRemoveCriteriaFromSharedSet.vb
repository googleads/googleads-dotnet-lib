' Copyright 2015, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License")
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http:'www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201506

Imports System
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.v201506

  ''' <summary>
  ''' This code example demonstrates how to find and remove shared sets and
  ''' shared set criteria.
  ''' 
  ''' </summary>
  Public Class FindAndRemoveCriteriaFromSharedSet
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New FindAndRemoveCriteriaFromSharedSet
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        codeExample.Run(New AdWordsUser(), campaignId)
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
        Return "This code example demonstrates how to find and remove shared sets and shared " & _
            "set criteria."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign to which keywords are removed.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the list of shared sets that are attached to the campaign.
      Dim sharedSetIds As List(Of String) = GetSharedSetIds(user, campaignId)

      ' Get the shared criteria in those shared sets.
      Dim sharedCriteria As List(Of SharedCriterion) = GetSharedCriteria(user, sharedSetIds)

      ' Remove the shared criteria from the shared sets.
      RemoveSharedCriteria(user, sharedCriteria)
    End Sub

    ''' <summary>
    ''' Gets the shared set IDs associated with a campaign.
    ''' </summary>
    ''' <param name="user">The user that owns the campaign.</param>
    ''' <param name="campaignId">The campaign identifier.</param>
    ''' <returns>The list of shared set IDs associated with the campaign.</returns>
    Private Function GetSharedSetIds(ByVal user As AdWordsUser, ByVal campaignId As Long) _
        As List(Of String)
      Dim campaignSharedSetService As CampaignSharedSetService = DirectCast(user.GetService( _
              AdWordsService.v201506.CampaignSharedSetService), CampaignSharedSetService)

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim selector As New Selector()
      selector.fields = New String() {"SharedSetId", "CampaignId", "SharedSetName", "SharedSetType"}

      Dim predicate1 As New Predicate()
      predicate1.field = "CampaignId"
      predicate1.operator = PredicateOperator.EQUALS
      predicate1.values = New String() {campaignId.ToString()}

      Dim predicate2 As New Predicate()
      predicate2.field = "SharedSetType"
      predicate2.operator = PredicateOperator.IN
      predicate2.values = New String() {SharedSetType.NEGATIVE_KEYWORDS.ToString()}

      selector.predicates = New Predicate() {predicate1, predicate2}
      selector.paging = New Paging()
      selector.paging.numberResults = pageSize

      Dim sharedSetIds As New List(Of String)

      Dim page As New CampaignSharedSetPage()

      Try
        Do
          selector.paging.startIndex = offset

          ' Get the campaigns.
          page = campaignSharedSetService.get(selector)

          ' Display the results.
          If (Not page Is Nothing) AndAlso (Not page.entries Is Nothing) Then
            Dim i As Integer = offset
            For Each campaignSharedSet As CampaignSharedSet In page.entries
              sharedSetIds.Add(campaignSharedSet.sharedSetId.ToString())
              Console.WriteLine("Campaign shared set ID {0} and name '{1}' found for campaign " & _
                  "ID {2}.\n", campaignSharedSet.sharedSetId, campaignSharedSet.sharedSetName, _
                  campaignSharedSet.campaignId)
            Next
          End If
          offset += pageSize
        Loop While (offset < page.totalNumEntries)
        Return sharedSetIds
      Catch e As Exception
        Throw New Exception("Failed to get shared set ids for campaign.", e)
      End Try
    End Function

    ''' <summary>
    ''' Gets the shared criteria in a shared set.
    ''' </summary>
    ''' <param name="user">The user that owns the shared set.</param>
    ''' <param name="sharedSetIds">The shared criteria IDs.</param>
    ''' <returns>The list of shared criteria.</returns>
    Private Function GetSharedCriteria(ByVal user As AdWordsUser, ByVal sharedSetIds As  _
                                       List(Of String)) As List(Of SharedCriterion)
      Dim sharedCriterionService As SharedCriterionService = DirectCast(user.GetService( _
          AdWordsService.v201506.SharedCriterionService), SharedCriterionService)

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim selector As New Selector()
      selector.fields = New String() {"SharedSetId", "Id", "KeywordText", _
          "KeywordMatchType", "PlacementUrl"}

      Dim predicate As New Predicate()
      predicate.field = "SharedSetId"
      predicate.operator = PredicateOperator.IN
      predicate.values = sharedSetIds.ToArray()

      selector.predicates = New Predicate() {predicate}
      selector.paging = New Paging()
      selector.paging.numberResults = pageSize

      Dim sharedCriteria As New List(Of SharedCriterion)

      Dim page As New SharedCriterionPage()

      Try
        Do
          selector.paging.startIndex = offset

          ' Get the campaigns.
          page = sharedCriterionService.get(selector)

          ' Display the results.
          If (Not page Is Nothing) AndAlso (Not page.entries Is Nothing) Then
            Dim i As Integer = offset
            For Each sharedCriterion As SharedCriterion In page.entries
              Select sharedCriterion.criterion.type
                Case CriterionType.KEYWORD
                  Dim keyword As Keyword = DirectCast(sharedCriterion.criterion, Keyword)
                  Console.WriteLine("Shared negative keyword with ID {0} and text '{1}' was " & _
                      "found.", keyword.id, keyword.text)
                  Exit Select
                  
                Case CriterionType.PLACEMENT
                  Dim placement As Placement = DirectCast(sharedCriterion.criterion, Placement)
                  Console.WriteLine("Shared negative placement with ID {0} and URL '{1}' " & _
                      "was found.", placement.id, placement.url)
                  Exit Select

                Case Else
                  Console.WriteLine("Shared criteria with ID {0} was found.", _
                      sharedCriterion.criterion.id)
              End Select

              sharedCriteria.Add(sharedCriterion)
            Next
          End If
          offset += pageSize
        Loop While (offset < page.totalNumEntries)

        Return sharedCriteria
      Catch e As Exception
        Throw New Exception("Failed to get shared criteria.", e)
      End Try
    End Function

    ''' <summary>
    ''' Removes a list of shared criteria.
    ''' </summary>
    ''' <param name="user">The user that owns the shared criteria.</param>
    ''' <param name="sharedCriteria">The list shared criteria to be removed.</param>
    Private Sub RemoveSharedCriteria(ByVal user As AdWordsUser, ByVal sharedCriteria _
        As List(Of SharedCriterion))
      If sharedCriteria.Count = 0 Then
        Console.WriteLine("No shared criteria to remove.")
        Return
      End If

      Dim sharedCriterionService As SharedCriterionService = DirectCast(user.GetService( _
          AdWordsService.v201506.SharedCriterionService), SharedCriterionService)

      Dim operations As New List(Of SharedCriterionOperation)

      For Each sharedCriterion As SharedCriterion In sharedCriteria

        Dim operation As New SharedCriterionOperation()
        operation.operator = [Operator].REMOVE

        Dim tempSharedCriterion As New SharedCriterion()
        tempSharedCriterion.sharedSetId = sharedCriterion.sharedSetId
        tempSharedCriterion.criterion = New Criterion()
        tempSharedCriterion.criterion.id = sharedCriterion.criterion.id

        operation.operand = tempSharedCriterion
        operations.Add(operation)
      Next
      Try
        Dim sharedCriterionReturnValue As SharedCriterionReturnValue = _
            sharedCriterionService.mutate(operations.ToArray())

        For Each removedCriterion As SharedCriterion In sharedCriterionReturnValue.value
          Console.WriteLine("Shared criterion ID {0} was successfully removed from shared " & _
              "set ID {1}.", removedCriterion.criterion.id, removedCriterion.sharedSetId)
        Next
      Catch e As Exception
        Throw New Exception("Failed to remove shared criteria.", e)
      End Try
    End Sub
  End Class
End Namespace
