' Copyright 2016, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603

  ''' <summary>
  ''' This code example creates a shared keyword list, adds keywords to the list
  ''' and attaches it to an existing campaign. To get the list of campaigns,
  ''' run GetCampaigns.vb.
  ''' </summary>
  Public Class CreateAndAttachSharedKeywordSet
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New CreateAndAttachSharedKeywordSet
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
        Return "This code example creates a shared keyword list, adds keywords to the list" & _
            "and attaches it to an existing campaign. To get the list of campaigns, run " & _
            "GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign to which keywords are added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      Try
        ' Create a shared set.
        Dim sharedSet As SharedSet = CreateSharedKeywordSet(user)

        Console.WriteLine("Shared set with id = {0}, name = {1}, type = {2}, status = {3} " & _
            "was created.", sharedSet.sharedSetId, sharedSet.name, sharedSet.type, _
            sharedSet.status)

        ' Add new keywords to the shared set.
        Dim keywordTexts As String() = New String() {"mars cruise", "mars hotels"}
        Dim sharedCriteria As SharedCriterion() = AddKeywordsToSharedSet(user, _
            sharedSet.sharedSetId, keywordTexts)
        For Each sharedCriterion As SharedCriterion In sharedCriteria
          Dim keyword As Keyword = DirectCast(sharedCriterion.criterion, Keyword)
          Console.WriteLine("Added keyword with id = {0}, text = {1}, matchtype = {2} to " & _
              "shared set with id = {3}.", keyword.id, keyword.text, keyword.matchType, _
              sharedSet.sharedSetId)
        Next

        ' Attach the shared set to the campaign.
        Dim attachedSharedSet As CampaignSharedSet = AttachSharedSetToCampaign(user, campaignId, _
            sharedSet.sharedSetId)

        Console.WriteLine("Attached shared set with id = {0} to campaign id {1}.", _
            attachedSharedSet.sharedSetId, attachedSharedSet.campaignId)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create shared keyword set and attach " & _
            "it to a campaign.", e)
      End Try
    End Sub

    ''' <summary>
    ''' Create a shared keyword set.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <returns>The shared set.</returns>
    Public Function CreateSharedKeywordSet(ByVal user As AdWordsUser) As SharedSet
      ' Get the SharedSetService.
      Dim sharedSetService As SharedSetService = DirectCast(user.GetService( _
          AdWordsService.v201603.SharedSetService), SharedSetService)

      Dim operation As New SharedSetOperation()
      operation.operator = [Operator].ADD
      Dim sharedSet As New SharedSet()
      sharedSet.name = "API Negative keyword list - " & ExampleUtilities.GetRandomString()
      sharedSet.type = SharedSetType.NEGATIVE_KEYWORDS
      operation.operand = sharedSet

      Dim retval As SharedSetReturnValue = sharedSetService.mutate( _
          New SharedSetOperation() {operation})
      Return retval.value(0)
    End Function

    ''' <summary>
    ''' Adds a set of keywords to a shared set.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="sharedSetId">The shared set id.</param>
    ''' <param name="keywordTexts">The keywords to be added to the shared set.</param>
    ''' <returns>The newly added set of shared criteria.</returns>
    Public Function AddKeywordsToSharedSet(ByVal user As AdWordsUser, ByVal sharedSetId As Long, _
        ByVal keywordTexts As String()) As SharedCriterion()
      ' Get the SharedCriterionService.
      Dim sharedSetService As SharedCriterionService = DirectCast(user.GetService( _
          AdWordsService.v201603.SharedCriterionService), SharedCriterionService)

      Dim operations As New List(Of SharedCriterionOperation)
      For Each keywordText As String In keywordTexts
        Dim keyword As New Keyword()
        keyword.text = keywordText
        keyword.matchType = KeywordMatchType.BROAD

        Dim sharedCriterion As New SharedCriterion()
        sharedCriterion.criterion = keyword
        sharedCriterion.negative = True
        sharedCriterion.sharedSetId = sharedSetId

        Dim operation As New SharedCriterionOperation()
        operation.operator = [Operator].ADD
        operation.operand = sharedCriterion
        operations.Add(operation)
      Next

      Dim retval As SharedCriterionReturnValue = sharedSetService.mutate(operations.ToArray())
      Return retval.value
    End Function

    ''' <summary>
    ''' Attaches a shared set to a campaign.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">The campaign id.</param>
    ''' <param name="sharedSetId">The shared set id.</param>
    ''' <returns>A CampaignSharedSet object that represents a binding between
    ''' the specified campaign and the shared set.</returns>
    Public Function AttachSharedSetToCampaign(ByVal user As AdWordsUser, _
        ByVal campaignId As Long, ByVal sharedSetId As Long) As CampaignSharedSet
      ' Get the CampaignSharedSetService.
      Dim campaignSharedSetService As CampaignSharedSetService = DirectCast(user.GetService( _
          AdWordsService.v201603.CampaignSharedSetService), CampaignSharedSetService)

      Dim campaignSharedSet As New CampaignSharedSet()
      campaignSharedSet.campaignId = campaignId
      campaignSharedSet.sharedSetId = sharedSetId

      Dim operation As New CampaignSharedSetOperation()
      operation.operator = [Operator].ADD
      operation.operand = campaignSharedSet

      Dim retval As CampaignSharedSetReturnValue = campaignSharedSetService.mutate( _
          New CampaignSharedSetOperation() {operation})
      Return retval.value(0)
    End Function

  End Class
End Namespace
