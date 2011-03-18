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
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example gets a bid landscape for an ad group and a criterion.
  ''' To get ad groups, run GetAllAdGroups.vb. To get criteria, run
  ''' GetAllAdGroupCriteria.vb.
  '''
  ''' Tags: BidLandscapeService.getBidLandscape
  ''' </summary>
  Class GetCriterionBidLandscape
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets a bid landscape for an ad group and a criterion. To get " & _
            "ad groups, run GetAllAdGroups.vb. To get criteria, run GetAllAdGroupCriteria.vb."
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
      ' Get the BidLandscapeService.
      Dim bidLandscapeService As BidLandscapeService = user.GetService( _
          AdWordsService.v201008.BidLandscapeService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))
      Dim criterionId As Long = Long.Parse(_T("INSERT_CRITERION_ID_HERE"))

      ' Create selector.
      Dim selector As New CriterionBidLandscapeSelector

      ' Create id filter.
      Dim idFilter As New BidLandscapeIdFilter
      idFilter.adGroupId = adGroupId
      idFilter.criterionId = criterionId
      selector.idFilters = New BidLandscapeIdFilter() {idFilter}

      Try
        ' Get bid landscape for ad group criteria.
        Dim bidLandscapes As BidLandscape() = bidLandscapeService.getBidLandscape(selector)

        ' Display bid landscapes.
        If ((Not bidLandscapes Is Nothing) AndAlso (bidLandscapes.Length > 0)) Then
          For Each bidLandscape As BidLandscape In bidLandscapes
            If TypeOf bidLandscape Is CriterionBidLandscape Then
              Dim criterionBidLandscape As CriterionBidLandscape = bidLandscape
              Console.WriteLine("Found criterion bid landscape with ad group id ""{0}"", " & _
                  "criterion id ""{1}"", start date ""{2}"", end date ""{3}"", and " & _
                  "landscape points:", criterionBidLandscape.adGroupId, _
                  criterionBidLandscape.criterionId, criterionBidLandscape.startDate, _
                  criterionBidLandscape.endDate)
              For Each bidLandscapePoint As BidLandscapeLandscapePoint _
                  In bidLandscape.landscapePoints
                Console.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, marginalCpc: {3}, " & _
                    "impressions: {4}", bidLandscapePoint.bid.microAmount, _
                    bidLandscapePoint.clicks, bidLandscapePoint.cost.microAmount, _
                    bidLandscapePoint.marginalCpc.microAmount, bidLandscapePoint.impressions)
              Next
            End If
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
