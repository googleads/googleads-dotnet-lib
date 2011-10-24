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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example gets all active ad group criteria in an ad group. To
  ''' add ad group criteria, run AddAdGroupCriteria.vb. To get ad groups in an
  ''' account, run GetAllAdGroups.vb.
  '''
  ''' Tags: AdGroupCriterionService.get
  ''' </summary>
  Class GetAllActiveAdGroupCriteria
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all active ad group criteria in an ad group. To add " & _
            "ad group criteria, run AddAdGroupCriteria.vb. To get ad groups in an account, " & _
            "run GetAllAdGroups.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllActiveAdGroupCriteria
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201109.AdGroupCriterionService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "AdGroupId", "KeywordText", "Status", "PlacementUrl"}

      ' Create the filters.
      Dim adGroupPredicate As New Predicate
      adGroupPredicate.field = "AdGroupId"
      adGroupPredicate.operator = PredicateOperator.EQUALS
      adGroupPredicate.values = New String() {adGroupId.ToString}

      Dim statusPredicate As New Predicate
      statusPredicate.field = "Status"
      statusPredicate.operator = PredicateOperator.EQUALS
      statusPredicate.values = New String() {UserStatus.ACTIVE.ToString}

      selector.predicates = New Predicate() {adGroupPredicate, statusPredicate}

      Try
        ' Get all ad group criteria.
        Dim adGroupCriterionPage As AdGroupCriterionPage = adGroupCriterionService.get(selector)
        If ((Not adGroupCriterionPage Is Nothing) AndAlso _
            (Not adGroupCriterionPage.entries Is Nothing)) Then
          ' Display ad group criteria.
          For Each adGroupCriterion As AdGroupCriterion In adGroupCriterionPage.entries
            If TypeOf adGroupCriterion.criterion Is Keyword Then
              Dim keyword As Keyword = adGroupCriterion.criterion
              Console.WriteLine("Keyword ad group criterion with criterion ID = '{0}', " & _
                  "text = '{1}' and matchType = '{2} was found.", keyword.id, keyword.text, _
                  keyword.matchType)
            ElseIf TypeOf adGroupCriterion.criterion Is Placement Then
              Dim placement As Placement = adGroupCriterion.criterion
              Console.WriteLine("Placement ad group criterion criterion ID = '{0}' and url " & _
                  "= '{1}' was found.", placement.id, placement.url)
            End If
          Next
        Else
          Console.WriteLine("No ad group criteria found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve criteria. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
