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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example reads all the keyword opportunities for the account.
  '''
  ''' Tags: BulkOpportunityService.get
  ''' </summary>
  Friend Class GetKeywordOpportunities
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example reads all the keyword opportunities for the account."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetKeywordOpportunities
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the BulkOpportunityService.
      Dim bulkOpportunityService As BulkOpportunityService = user.GetService( _
          AdWordsService.v201109.BulkOpportunityService)

      ' Create selector.
      Dim selector As New BulkOpportunitySelector
      selector.requestedAttributeTypes = New OpportunityAttributeType() { _
          OpportunityAttributeType.ADGROUP_ID, _
          OpportunityAttributeType.AVERAGE_MONTHLY_SEARCHES, _
          OpportunityAttributeType.CAMPAIGN_ID, OpportunityAttributeType.IDEA_TYPE, _
          OpportunityAttributeType.KEYWORD}
      selector.ideaTypes = New OpportunityIdeaType() {OpportunityIdeaType.KEYWORD}

      Dim paging As New Paging
      paging.startIndex = 0
      paging.numberResults = 10
      selector.paging = paging

      Try
        Dim page As BulkOpportunityPage = bulkOpportunityService.get(selector)
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing) AndAlso _
            page.entries.Length > 0) Then
          For Each opportunity As Opportunity In page.entries
            For Each idea As OpportunityIdea In opportunity.opportunityIdeas
              Dim data As New Dictionary(Of OpportunityAttributeType,  _
                  Google.Api.Ads.AdWords.v201109.Attribute)
              For Each dataItem As OpportunityAttribute_AttributeMapEntry In idea.data
                data.Item(dataItem.key) = dataItem.value
              Next
              Dim campaignId As Long = TryCast(data.Item(OpportunityAttributeType.CAMPAIGN_ID),  _
                  LongAttribute).value
              Dim adGroupId As Long = TryCast(data.Item(OpportunityAttributeType.CAMPAIGN_ID),  _
                  LongAttribute).value
              Console.WriteLine("Opportunities found for campaign id '{0}' and ad group id " & _
                  "'{1}':", campaignId, adGroupId)
              Dim keyword As Keyword = TryCast(data.Item(OpportunityAttributeType.KEYWORD),  _
                  KeywordAttribute).value
              Dim averageMonthlySearches As Long = _
                  TryCast(data.Item(OpportunityAttributeType.KEYWORD), IntegerAttribute).value
              Console.WriteLine("\tKeyword opportunity with text '{0}', match type '{1}', " & _
                  "and average monthly search volume '{2}' was found.", keyword.text, _
                  keyword.matchType, averageMonthlySearches)
            Next
          Next
        Else
          Console.WriteLine("No keyword opportunities were found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to get keyword opportunities. Exception says '{0}'", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
