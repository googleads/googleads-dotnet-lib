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
Imports Google.Api.Ads.AdWords.v201806
Imports Attribute = Google.Api.Ads.AdWords.v201806.Attribute

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
    ''' <summary>
    ''' This code example retrieves keywords that are related to a given keyword.
    ''' </summary>
    Public Class GetKeywordIdeas
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New GetKeywordIdeas
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
                Return "This code example retrieves keywords that are related to a given keyword."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">ID of the ad group to use for generating ideas.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long?)
            Using targetingIdeaService As TargetingIdeaService = CType(
                user.GetService(
                    AdWordsService.v201806.TargetingIdeaService),
                TargetingIdeaService)

                ' Create selector.
                ' [START prepareRequestTypes] MOE:strip_line
                Dim selector As New TargetingIdeaSelector()
                selector.requestType = RequestType.IDEAS
                selector.ideaType = IdeaType.KEYWORD
                ' [END prepareRequestTypes] MOE:strip_line
                ' [START prepareRequestAttributeTypes] MOE:strip_line
                selector.requestedAttributeTypes =
                    New AttributeType() { _
                                            AttributeType.KEYWORD_TEXT,
                                            AttributeType.SEARCH_VOLUME,
                                            AttributeType.CATEGORY_PRODUCTS_AND_SERVICES
                                        }
                ' [END prepareRequestAttributeTypes] MOE:strip_line

                Dim searchParameters As New List(Of SearchParameter)

                ' [START prepareRequestQueryParameter] MOE:strip_line
                ' Create related to query search parameter.
                Dim relatedToQuerySearchParameter As New RelatedToQuerySearchParameter()
                relatedToQuerySearchParameter.queries = New String() { _
                                                                         "bakery", "pastries",
                                                                         "birthday cake"
                                                                     }
                searchParameters.Add(relatedToQuerySearchParameter)
                ' [END prepareRequestQueryParameter] MOE:strip_line

                ' Add a language search parameter (optional).
                ' The ID can be found in the documentation:
                '   https://developers.google.com/adwords/api/docs/appendix/languagecodes
                Dim languageParameter As New LanguageSearchParameter()
                Dim english As New Language()
                english.id = 1000
                languageParameter.languages = New Language() {english}
                searchParameters.Add(languageParameter)

                ' [START prepareRequestNetworkSetting] MOE:strip_line
                ' Add network search parameter (optional).
                Dim networkSetting As New NetworkSetting()
                networkSetting.targetGoogleSearch = True
                networkSetting.targetSearchNetwork = False
                networkSetting.targetContentNetwork = False
                networkSetting.targetPartnerSearchNetwork = False

                Dim networkSearchParameter As New NetworkSearchParameter()
                networkSearchParameter.networkSetting = networkSetting
                searchParameters.Add(networkSearchParameter)
                ' [END prepareRequestNetworkSetting] MOE:strip_line

                ' Optional: Use an existing ad group to generate ideas.
                If adGroupId.HasValue() Then
                    ' [START setSeedAdGroupId] MOE:strip_line
                    Dim seedAdGroupIdSearchParameter As New SeedAdGroupIdSearchParameter()
                    seedAdGroupIdSearchParameter.adGroupId = adGroupId.Value
                    searchParameters.Add(seedAdGroupIdSearchParameter)
                    ' [END setSeedAdGroupId] MOE:strip_line
                End If

                ' Set the search parameters.
                selector.searchParameters = searchParameters.ToArray()

                ' [START preparePaging] MOE:strip_line

                ' Set selector paging (required for targeting idea service).
                selector.paging = Paging.Default
                ' [END preparePaging] MOE:strip_line

                Dim page As New TargetingIdeaPage()

                Try
                    Dim i As Integer = 0
                    Do
                        ' [START getKeywordIdeas] MOE:strip_line
                        ' Get related keywords.
                        page = targetingIdeaService.get(selector)
                        ' [END getKeywordIdeas] MOE:strip_line

                        ' [START displayKeywordIdeas] MOE:strip_line
                        'Display the results.
                        If Not page.entries Is Nothing AndAlso page.entries.Length > 0 Then
                            For Each targetingIdea As TargetingIdea In page.entries
                                For Each entry As Type_AttributeMapEntry In targetingIdea.data
                                    ' Preferred: Use targetingIdea.data.ToDict() if you are not on
                                    ' Mono.
                                    Dim ideas As Dictionary(Of AttributeType, Attribute) =
                                            MapEntryExtensions.ToDict (Of AttributeType, Attribute)(
                                                targetingIdea.data)

                                    Dim keyword As String =
                                            DirectCast(ideas(AttributeType.KEYWORD_TEXT),
                                                       StringAttribute).value
                                    Dim categorySet As IntegerSetAttribute =
                                            DirectCast(
                                                ideas(AttributeType.CATEGORY_PRODUCTS_AND_SERVICES),
                                                IntegerSetAttribute)

                                    Dim categories As String = ""

                                    If _
                                        (Not categorySet Is Nothing) AndAlso
                                        (Not categorySet.value Is Nothing) Then
                                        categories = String.Join(", ", categorySet.value)
                                    End If

                                    Dim averageMonthlySearches As Long =
                                            DirectCast(ideas(AttributeType.SEARCH_VOLUME),
                                                       LongAttribute).value


                                    Dim averageCpcMoney As Money =
                                            DirectCast(ideas(AttributeType.AVERAGE_CPC),
                                                       MoneyAttribute).value
                                    Dim averageCpc As Long
                                    If (Not averageCpcMoney Is Nothing) Then
                                        averageCpc = averageCpcMoney.microAmount
                                    End If

                                    Dim competition As Double =
                                            DirectCast(ideas(AttributeType.COMPETITION),
                                                       DoubleAttribute).value
                                    Console.WriteLine(
                                        "Keyword with text '{0}', average monthly search " +
                                        "volume {1}, average CPC {2}, and competition {3:F2} was " &
                                        "found with categories: {4}", keyword,
                                        averageMonthlySearches, averageCpc,
                                        competition, categories)
                                Next
                                i = i + 1
                            Next
                        End If
                        selector.paging.IncreaseOffset()
                    Loop While (selector.paging.startIndex < page.totalNumEntries)
                    Console.WriteLine("Number of related keywords found: {0}", page.totalNumEntries)
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to retrieve related keywords.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
