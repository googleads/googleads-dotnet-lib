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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
    ''' <summary>
    ''' This code example gets keyword traffic estimates.
    ''' </summary>
    Public Class EstimateKeywordTraffic
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New EstimateKeywordTraffic
            Console.WriteLine(codeExample.Description)
            Try
                codeExample.Run(New AdWordsUser)
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
                Return "This code example gets keyword traffic estimates."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            Using trafficEstimatorService As TrafficEstimatorService = CType(
                user.GetService(
                    AdWordsService.v201806.TrafficEstimatorService),
                TrafficEstimatorService)

                ' [START createKeywordEstimateRequest] MOE:strip_line
                ' Create keywords. Up to 2000 keywords can be passed in a single request.
                Dim keyword1 As New Keyword
                keyword1.text = "mars cruise"
                keyword1.matchType = KeywordMatchType.BROAD

                Dim keyword2 As New Keyword
                keyword2.text = "cheap cruise"
                keyword2.matchType = KeywordMatchType.PHRASE

                Dim keyword3 As New Keyword
                keyword3.text = "cruise"
                keyword3.matchType = KeywordMatchType.EXACT

                Dim keywords As Keyword() = New Keyword() {keyword1, keyword2, keyword3}

                ' Create a keyword estimate request for each keyword.
                Dim keywordEstimateRequests As New List(Of KeywordEstimateRequest)

                For Each keyword As Keyword In keywords
                    Dim keywordEstimateRequest As New KeywordEstimateRequest
                    keywordEstimateRequest.keyword = keyword
                    keywordEstimateRequests.Add(keywordEstimateRequest)
                Next

                ' Create negative keywords.
                Dim negativeKeyword1 As New Keyword
                negativeKeyword1.text = "moon walk"
                negativeKeyword1.matchType = KeywordMatchType.BROAD

                Dim negativeKeywordEstimateRequest As New KeywordEstimateRequest
                negativeKeywordEstimateRequest.keyword = negativeKeyword1
                negativeKeywordEstimateRequest.isNegative = True
                keywordEstimateRequests.Add(negativeKeywordEstimateRequest)
                ' [END createKeywordEstimateRequest] MOE:strip_line

                ' [START createAdGroupEstimateRequest] MOE:strip_line
                ' Create ad group estimate requests.
                Dim adGroupEstimateRequest As New AdGroupEstimateRequest
                adGroupEstimateRequest.keywordEstimateRequests = keywordEstimateRequests.ToArray
                adGroupEstimateRequest.maxCpc = New Money
                adGroupEstimateRequest.maxCpc.microAmount = 1000000
                ' [END createAdGroupEstimateRequest] MOE:strip_line

                ' [START createCampaignEstimateRequest] MOE:strip_line
                ' Create campaign estimate requests.
                Dim campaignEstimateRequest As New CampaignEstimateRequest
                campaignEstimateRequest.adGroupEstimateRequests = New AdGroupEstimateRequest() _
                    {adGroupEstimateRequest}

                ' See http://code.google.com/apis/adwords/docs/appendix/countrycodes.html
                ' for a detailed list of country codes.
                Dim countryCriterion As New Location
                countryCriterion.id = 2840 'US

                ' See http://code.google.com/apis/adwords/docs/appendix/languagecodes.html
                ' for a detailed list of language codes.
                Dim languageCriterion As New Language
                languageCriterion.id = 1000 'en

                campaignEstimateRequest.criteria = New Criterion() _
                    {countryCriterion, languageCriterion}
                ' [END createCampaignEstimateRequest] MOE:strip_line

                Try
                    ' [START makeRequest] MOE:strip_line
                    ' Create the selector.
                    Dim selector As New TrafficEstimatorSelector
                    selector.campaignEstimateRequests =
                        New CampaignEstimateRequest() {campaignEstimateRequest}

                    ' Optional: Request a list of campaign level estimates segmented by platform.
                    selector.platformEstimateRequested = True

                    ' Get traffic estimates.
                    Dim result As TrafficEstimatorResult = trafficEstimatorService.get(selector)
                    ' [END makeRequest] MOE:strip_line

                    ' [START displayEstimates] MOE:strip_line
                    ' Display the results.
                    If ((Not result Is Nothing) AndAlso (Not result.campaignEstimates Is Nothing) _
                        AndAlso (result.campaignEstimates.Length > 0)) Then
                        Dim campaignEstimate As CampaignEstimate = result.campaignEstimates(0)

                        ' Display the campaign level estimates segmented by platform.
                        If Not campaignEstimate.platformEstimates Is Nothing Then
                            For Each platformEstimate As PlatformCampaignEstimate In _
                                campaignEstimate.platformEstimates
                                Dim platformMessage As String =
                                        String.Format("Results for the platform with ID:" &
                                                      " {0} and name : {1}.",
                                                      platformEstimate.platform.id,
                                                      platformEstimate.platform.platformName)

                                DisplayMeanEstimates(platformMessage, platformEstimate.minEstimate,
                                                     platformEstimate.maxEstimate)
                            Next
                        End If

                        If ((Not campaignEstimate.adGroupEstimates Is Nothing) AndAlso
                            (campaignEstimate.adGroupEstimates.Length > 0)) Then
                            Dim adGroupEstimate As AdGroupEstimate =
                                    campaignEstimate.adGroupEstimates(0)

                            If (Not adGroupEstimate.keywordEstimates Is Nothing) Then
                                For i As Integer = 0 To adGroupEstimate.keywordEstimates.Length - 1
                                    Dim keyword As Keyword = keywordEstimateRequests.Item(i).keyword
                                    Dim keywordEstimate As KeywordEstimate =
                                            adGroupEstimate.keywordEstimates(i)

                                    If keywordEstimateRequests.Item(i).isNegative Then
                                        Continue For
                                    End If

                                    Dim kwdMessage As String =
                                            String.Format("Results for the keyword with" &
                                                          " text = '{0}' and match type = '{1}':",
                                                          keyword.text, keyword.matchType)
                                    DisplayMeanEstimates(kwdMessage, keywordEstimate.min,
                                                         keywordEstimate.max)
                                Next i
                            End If
                        End If
                    Else
                        Console.WriteLine("No traffic estimates were returned.")
                    End If
                    ' [END displayEstimates] MOE:strip_line
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to retrieve traffic estimates.", e)
                End Try
            End Using
        End Sub

        ''' <summary>
        ''' Displays the mean estimates.
        ''' </summary>
        ''' <param name="message">The message to display.</param>
        ''' <param name="minEstimate">The minimum stats estimate.</param>
        ''' <param name="maxEstimate">The maximum stats estimate.</param>
        Private Sub DisplayMeanEstimates(ByVal message As String,
                                         ByVal minEstimate As StatsEstimate,
                                         ByVal maxEstimate As StatsEstimate)
            ' Find the mean of the min and max values.
            Dim meanAverageCpc As Long = 0L
            Dim meanAveragePosition As Double = 0
            Dim meanClicks As Single = 0
            Dim meanTotalCost As Single = 0

            If (Not (minEstimate Is Nothing) AndAlso Not (maxEstimate Is Nothing)) Then
                If (Not (minEstimate.averageCpc Is Nothing) AndAlso
                    Not (maxEstimate.averageCpc Is Nothing)) Then
                    meanAverageCpc = CLng((minEstimate.averageCpc.microAmount +
                                           maxEstimate.averageCpc.microAmount)/2)
                End If

                If minEstimate.averagePositionSpecified AndAlso
                   maxEstimate.averagePositionSpecified Then
                    meanAveragePosition =
                        (minEstimate.averagePosition + maxEstimate.averagePosition)/2
                End If

                If minEstimate.clicksPerDaySpecified AndAlso maxEstimate.clicksPerDaySpecified Then
                    meanClicks = (minEstimate.clicksPerDay + maxEstimate.clicksPerDay)/2
                End If
                If (Not (minEstimate.totalCost Is Nothing) AndAlso
                    Not (maxEstimate.totalCost Is Nothing)) Then
                    meanTotalCost = CLng((minEstimate.totalCost.microAmount +
                                          maxEstimate.totalCost.microAmount)/2)
                End If
            End If

            Console.WriteLine(message)
            Console.WriteLine("  Estimated average CPC: {0}", meanAverageCpc)
            Console.WriteLine("  Estimated ad position: {0:0.00}", meanAveragePosition)
            Console.WriteLine("  Estimated daily clicks: {0}", meanClicks)
            Console.WriteLine("  Estimated daily cost: {0}", meanTotalCost)
        End Sub
    End Class
End Namespace
