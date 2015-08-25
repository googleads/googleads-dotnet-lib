' Copyright 2015, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201506

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201506
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
          AdWordsService.v201506.AdGroupCriterionService), AdWords.v201506.AdGroupCriterionService)

      ' Create a selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "KeywordMatchType", "KeywordText", "CriteriaType"}

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

      selector.predicates = New Predicate() {adGroupPredicate, criteriaPredicate}

      ' Set the selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New AdGroupCriterionPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get the keywords.
          page = adGroupCriterionService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset

            For Each adGroupCriterion As AdGroupCriterion In page.entries
              Dim isNegative As Boolean = TypeOf adGroupCriterion Is NegativeAdGroupCriterion

              ' If you are retrieving multiple type of criteria, then you may
              ' need to check for
              '
              ' if (adGroupCriterion is Keyword) { ... }
              '
              ' to identify the criterion type.
              Dim keyword As Keyword = CType(adGroupCriterion.criterion, AdWords.v201506.Keyword)
              Dim keywordType As String = "Keyword"
              If isNegative Then
                keywordType = "Negative keyword"
              End If

              Console.WriteLine("{0}) {1} with text = '{2}', matchtype = '{3}', ID = '{4}' " & _
                                "and criteria type = '{5}' was found.", i + 1, keywordType, _
                                keyword.text, keyword.matchType, keyword.id, keyword.CriterionType)
              i += 1
            Next
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        Console.WriteLine("Number of keywords found: {0}", page.totalNumEntries)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to retrieve keywords.", e)
      End Try
    End Sub
  End Class
End Namespace