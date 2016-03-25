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
  ''' This code example gets all keywords in an ad group. To add keywords, run
  ''' AddKeywords.vb.
  ''' </summary>
  Public Class GetKeywords
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetKeywords
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId)
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
        Return "This code example gets all keywords in an ad group. To add keywords, run " & _
            "AddKeywords.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">ID of the ad group from which keywords are
    ''' retrieved.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupCriterionService), AdGroupCriterionService)

      ' Create a selector.
      Dim selector As New Selector
      selector.fields = New String() {
        Keyword.Fields.Id, Keyword.Fields.KeywordMatchType,
        Keyword.Fields.KeywordText, Keyword.Fields.CriteriaType
      }

      ' Select only keywords.
      Dim criteriaPredicate As New Predicate
      criteriaPredicate.field = "CriteriaType"
      criteriaPredicate.operator = PredicateOperator.IN
      criteriaPredicate.values = New String() {"KEYWORD"}

      ' Restrict search to an ad group.
      Dim adGroupPredicate As New Predicate
      adGroupPredicate.field = "AdGroupId"
      adGroupPredicate.operator = PredicateOperator.EQUALS
      adGroupPredicate.values = New String() {adGroupId.ToString()}

      selector.predicates = New Predicate() {
        Predicate.In(Keyword.Fields.CriteriaType, New String() {"KEYWORD"}),
        Predicate.Equals(AdGroupCriterion.Fields.AdGroupId, adGroupId)
      }
      selector.ordering = New OrderBy() {OrderBy.Asc(Keyword.Fields.KeywordText)}
      selector.paging = Paging.Default

      Dim page As New AdGroupCriterionPage

      Try
        Do
          ' Get the keywords.
          page = adGroupCriterionService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = selector.paging.startIndex

            For Each adGroupCriterion As AdGroupCriterion In page.entries
              Dim keyword As Keyword = CType(adGroupCriterion.criterion, Keyword)

              Console.WriteLine("{0}) Keyword with text '{1}', match type '{2}', criteria " & _
                  "type '{3}', and ID {4} was found.", i + 1, keyword.text, keyword.matchType,
                  keyword.type, keyword.id)
              i += 1
            Next
          End If
          selector.paging.IncreaseOffset()
        Loop While (selector.paging.startIndex < page.totalNumEntries)
        Console.WriteLine("Number of keywords found: {0}", page.totalNumEntries)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to retrieve keywords.", e)
      End Try
    End Sub
  End Class
End Namespace