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
Imports Google.Api.Ads.AdWords.v201101

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example gets a bid landscape for an ad group and a criterion.
  ''' To get ad groups, run GetAllAdGroups.vb. To get criteria, run
  ''' GetAllAdGroupCriteria.vb.
  '''
  ''' Tags: DataService.getCriterionBidLandscape
  ''' </summary>
  Class GetCriterionBidLandscape
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets a bid landscape for an ad group and a criterion. To " & _
            "get ad groups, run GetAllAdGroups.vb. To get criteria, run GetAllAdGroupCriteria.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetCriterionBidLandscape
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the DataService.
      Dim dataService As DataService = user.GetService(AdWordsService.v201101.DataService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))
      Dim criterionId As Long = Long.Parse(_T("INSERT_CRITERION_ID_HERE"))

      ' Create selector.
      Dim selector As New Selector
      selector.fields = New String() {"AdGroupId", "CriterionId", "StartDate", "EndDate", _
          "Bid", "LocalClicks", "LocalCost", "MarginalCpc", "LocalImpressions"}

      ' Set the filters.
      Dim adGroupPredicate As New Predicate
      adGroupPredicate.field = "AdGroupId"
      adGroupPredicate.operator = PredicateOperator.IN
      adGroupPredicate.values = New String() {adGroupId.ToString}

      Dim criterionPredicate As New Predicate
      criterionPredicate.field = "CriterionId"
      criterionPredicate.operator = PredicateOperator.IN
      adGroupPredicate.values = New String() {criterionId.ToString}

      selector.predicates = New Predicate() {adGroupPredicate, criterionPredicate}

      Try
        ' Get bid landscape for ad group criteria.
        Dim page As CriterionBidLandscapePage = dataService.getCriterionBidLandscape(selector)

        ' Display bid landscapes.
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          For Each bidLandscape As CriterionBidLandscape In page.entries
            Console.WriteLine("Found criterion bid landscape with ad group id ""{0}"", " & _
                  "criterion id ""{1}"", start date ""{2}"", end date ""{3}"", and " & _
                  "landscape points:", bidLandscape.adGroupId, _
                  bidLandscape.criterionId, bidLandscape.startDate, _
                  bidLandscape.endDate)
            For Each bidLandscapePoint As BidLandscapeLandscapePoint _
                In bidLandscape.landscapePoints
              Console.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, marginalCpc: {3}, " & _
                  "impressions: {4}", bidLandscapePoint.bid.microAmount, _
                  bidLandscapePoint.clicks, bidLandscapePoint.cost.microAmount, _
                  bidLandscapePoint.marginalCpc.microAmount, bidLandscapePoint.impressions)
            Next
          Next
        Else
          Console.WriteLine("No criterion bid landscapes were found.\n")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve criterion bid landscapes. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
