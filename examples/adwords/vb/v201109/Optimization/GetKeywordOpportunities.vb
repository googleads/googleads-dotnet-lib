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
  ''' This code example reads all the keyword opportunities for the account.
  '''
  ''' Tags: BulkOpportunityService.get
  ''' </summary>
  Friend Class GetKeywordOpportunities
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetKeywordOpportunities
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example reads all the keyword opportunities for the account."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {}
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
      ' Get the BulkOpportunityService.
      Dim bulkOpportunityService As BulkOpportunityService = user.GetService( _
          AdWordsService.v201109.BulkOpportunityService)

      ' Create the selector.
      Dim selector As New BulkOpportunitySelector
      selector.requestedAttributeTypes = New OpportunityAttributeType() { _
          OpportunityAttributeType.ADGROUP_ID, OpportunityAttributeType.AVERAGE_MONTHLY_SEARCHES, _
          OpportunityAttributeType.CAMPAIGN_ID, OpportunityAttributeType.IDEA_TYPE, _
          OpportunityAttributeType.KEYWORD}
      selector.ideaTypes = New OpportunityIdeaType() {OpportunityIdeaType.KEYWORD}

      ' Select selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New BulkOpportunityPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get keyword opportunities.
          page = bulkOpportunityService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset
            For Each opportunity As Opportunity In page.entries
              For Each idea As OpportunityIdea In opportunity.opportunityIdeas
                Dim data As New Dictionary(Of OpportunityAttributeType,  _
                    Google.Api.Ads.AdWords.v201109.Attribute)
                For Each dataItem As OpportunityAttribute_AttributeMapEntry In idea.data
                  data.Item(dataItem.key) = dataItem.value
                Next
                Dim campaignId As Long = TryCast(data(OpportunityAttributeType.CAMPAIGN_ID),  _
                    LongAttribute).value
                Dim adGroupId As Long = TryCast(data(OpportunityAttributeType.CAMPAIGN_ID),  _
                    LongAttribute).value
                writer.WriteLine("{0}) Opportunities found for campaign id '{1}' and " & _
                    "ad group id '{2}':", i, campaignId, adGroupId)
                Dim keyword As Keyword = TryCast(data(OpportunityAttributeType.KEYWORD),  _
                    KeywordAttribute).value
                Dim averageMonthlySearches As Long = _
                    TryCast(data(OpportunityAttributeType.AVERAGE_MONTHLY_SEARCHES), IntegerAttribute).value
                writer.WriteLine("\tKeyword opportunity with text '{0}', match type '{1}', " & _
                    "and average monthly search volume '{2}' was found.", keyword.text, _
                    keyword.matchType, averageMonthlySearches)
              Next
            Next
            i += 1
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        writer.WriteLine("Number of keyword opportunities found: {0}", page.totalNumEntries)
      Catch ex As Exception
        writer.WriteLine("Failed to get keyword opportunities. Exception says '{0}'", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
