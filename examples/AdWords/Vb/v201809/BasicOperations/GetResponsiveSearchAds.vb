' Copyright 2018 Google LLC
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
Imports Google.Api.Ads.AdWords.v201809

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example gets non-removed responsive search ads in an ad group. To add
    ''' responsive search ads, run AddResponsiveSearchAd.vb. To get ad groups, run
    ''' GetAdGroups.vb.
    ''' </summary>
    Public Class GetResponsiveSearchAds
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New GetResponsiveSearchAds
            Console.WriteLine(codeExample.Description)
            Try
                Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
                codeExample.Run(New AdWordsUser, adGroupId)
            Catch e As Exception
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
            End Try
        End Sub

        ''' <summary>
        ''' Returns a description about the code example.
        ''' </summary>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return _
                    "This code example gets non-removed responsive search ads in an ad group. To " +
                    "add responsive search ads, run AddResponsiveSearchAd.vb. To get ad groups, " +
                    "run GetAdGroups.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Id of the ad group from which expanded text ads
        ''' are retrieved.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            ' [START getResponsiveSearchAd] MOE:strip_line
            Using service As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupAdService),
                AdGroupAdService)

                ' Create a selector.
                Dim selector As New Selector

                selector.fields = New String() { _
                                                   ResponsiveSearchAd.Fields.Id,
                                                   AdGroupAd.Fields.Status,
                                                   ResponsiveSearchAd.Fields.
                                                       ResponsiveSearchAdHeadlines,
                                                   ResponsiveSearchAd.Fields.
                                                       ResponsiveSearchAdDescriptions
                                               }

                selector.ordering = New OrderBy() {OrderBy.Asc(ResponsiveSearchAd.Fields.Id)}

                selector.predicates =
                    New Predicate() {Predicate.Equals(AdGroupAd.Fields.AdGroupId, adGroupId),
                                     Predicate.Equals("AdType",
                                                      AdType.RESPONSIVE_SEARCH_AD.ToString())}

                ' Select the selector paging.
                selector.paging = Paging.Default

                Dim page As New AdGroupAdPage

                Try
                    Do
                        ' Get the responsive search ads.
                        page = service.get(selector)

                        ' Display the results.
                        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
                            Dim i As Integer = selector.paging.startIndex

                            For Each adGroupAd As AdGroupAd In page.entries
                                Dim ad As ResponsiveSearchAd = CType(adGroupAd.ad,
                                                                     ResponsiveSearchAd)
                                Console.WriteLine(
                                    "{0} New responsive search ad with ID {1} and status " +
                                    "{2} was found.", i + 1, ad.id, adGroupAd.status)

                                Console.WriteLine("Headlines:")

                                For Each headline As AssetLink In ad.headlines
                                    Dim textAsset As TextAsset = CType(headline.asset, TextAsset)
                                    Console.WriteLine("    {0}", textAsset.assetText)
                                    If headline.pinnedFieldSpecified Then
                                        Console.WriteLine("      (pinned to {0})",
                                                          headline.pinnedField)
                                    End If
                                Next
                                Console.WriteLine("Descriptions:")
                                For Each description As AssetLink In ad.descriptions
                                    Dim textAsset As TextAsset = CType(description.asset, TextAsset)
                                    Console.WriteLine("    {0}", textAsset.assetText)
                                    If (description.pinnedFieldSpecified) Then
                                        Console.WriteLine("      (pinned to {0})",
                                                          description.pinnedField)
                                    End If
                                Next
                            Next
                            i += 1
                        End If
                        selector.paging.IncreaseOffset()
                    Loop While (selector.paging.startIndex < page.totalNumEntries)
                    Console.WriteLine("Number of responsive search ads found: {0}",
                                      page.totalNumEntries)
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to get responsive search ads.", e)
                End Try
            End Using
            ' [END getResponsiveSearchAd] MOE:strip_line
        End Sub
    End Class
End Namespace
