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
  ''' This code example gets bid landscapes for an ad group. To get ad groups,
  ''' run GetAdGroups.vb.
  ''' </summary>
  Public Class GetAdGroupBidSimulations
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetAdGroupBidSimulations
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
        Return "This code example gets bid landscapes for an ad group. To get ad groups, run " & _
            "GetAdGroups.vb"
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group for which bid simulations are
    ''' retrieved.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the DataService.
      Dim dataService As DataService = CType(user.GetService( _
          AdWordsService.v201603.DataService), DataService)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {
        AdGroupBidLandscape.Fields.AdGroupId, AdGroupBidLandscape.Fields.LandscapeType,
        AdGroupBidLandscape.Fields.LandscapeCurrent, AdGroupBidLandscape.Fields.StartDate,
        AdGroupBidLandscape.Fields.EndDate, BidLandscapeLandscapePoint.Fields.Bid,
        BidLandscapeLandscapePoint.Fields.LocalClicks,
        BidLandscapeLandscapePoint.Fields.LocalCost,
        BidLandscapeLandscapePoint.Fields.LocalImpressions
      }

      ' Set the filters.
      Dim adGroupPredicate As New Predicate
      adGroupPredicate.field = "AdGroupId"
      adGroupPredicate.operator = PredicateOperator.IN
      adGroupPredicate.values = New String() {adGroupId.ToString}

      selector.predicates = New Predicate() {
        Predicate.Equals(AdGroupBidLandscape.Fields.AdGroupId, adGroupId)
      }

      Try
        ' Get bid landscape for ad group.
        Dim page As AdGroupBidLandscapePage = dataService.getAdGroupBidLandscape(selector)
        If (((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) AndAlso _
            (page.entries.Length > 0)) Then
          For Each bidLandscape As AdGroupBidLandscape In page.entries
            Console.WriteLine("Found ad group bid landscape with ad group id '{0}', " & _
                "type '{1}', current: '{2}', start date '{3}', end date '{4}', and " & _
                "landscape points", bidLandscape.adGroupId, bidLandscape.type, _
                bidLandscape.landscapeCurrent, bidLandscape.startDate, bidLandscape.endDate)
            Dim point As BidLandscapeLandscapePoint
            For Each point In bidLandscape.landscapePoints
              Console.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, impressions: {3}", _
                  point.bid.microAmount, point.bid.microAmount, _
                  point.clicks, point.cost.microAmount, point.impressions)
            Next
          Next
        Else
          Console.WriteLine("No ad group bid landscapes were found.\n")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to get ad group bid landscapes.", e)
      End Try
    End Sub
  End Class
End Namespace
