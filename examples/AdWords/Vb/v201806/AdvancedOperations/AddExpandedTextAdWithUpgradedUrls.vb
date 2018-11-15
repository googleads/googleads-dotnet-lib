' Copyright 2018 Google LLC
'
' Licensed under the Apache License, Version 2.0 (the "License")
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http:'www.apache.org/licenses/LICENSE-2.0
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
    ''' This code example adds an expanded text ad that uses advanced features of upgraded
    ''' URLs.
    ''' </summary>
    Public Class AddExpandedTextAdWithUpgradedUrls
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddExpandedTextAdWithUpgradedUrls
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
                    "This code example adds an expanded text ad that uses advanced features of " &
                    "upgraded URLs."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">ID of the adgroup to which ad is added.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            Using adGroupAdService As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupAdService),
                AdGroupAdService)

                ' Create the expanded text ad.
                Dim expandedTextAd As New ExpandedTextAd
                expandedTextAd.headlinePart1 = "Luxury Cruise to Mars"
                expandedTextAd.headlinePart2 = "Visit the Red Planet in style."
                expandedTextAd.description = "Low-gravity fun for everyone!"

                ' [START setTrackingUrlTemplate] MOE:strip_line
                ' Specify a tracking URL for 3rd party tracking provider. You may
                ' specify one at customer, campaign, ad group, ad, criterion or
                ' feed item levels.
                expandedTextAd.trackingUrlTemplate =
                    "http://tracker.example.com/?cid={_season}&promocode={_promocode}&u={lpurl}"
                ' [END setTrackingUrlTemplate] MOE:strip_line

                ' [START setCustomParameters] MOE:strip_line
                ' Since your tracking URL has two custom parameters, provide their
                ' values too. This can be provided at campaign, ad group, ad, criterion
                ' or feed item levels.
                Dim seasonParameter As New CustomParameter
                seasonParameter.key = "season"
                seasonParameter.value = "christmas"

                Dim promoCodeParameter As New CustomParameter
                promoCodeParameter.key = "promocode"
                promoCodeParameter.value = "NYC123"

                expandedTextAd.urlCustomParameters = New CustomParameters
                expandedTextAd.urlCustomParameters.parameters =
                    New CustomParameter() {seasonParameter, promoCodeParameter}
                ' [END setCustomParameters] MOE:strip_line

                ' Specify a list of final URLs. This field cannot be set if URL field is
                ' set. This may be specified at ad, criterion and feed item levels.
                expandedTextAd.finalUrls = New String() { _
                                                            "http://www.example.com/cruise/space/",
                                                            "http://www.example.com/locations/mars/"
                                                        }

                ' Specify a list of final mobile URLs. This field cannot be set if URL
                ' field is set, or finalUrls is unset. This may be specified at ad,
                ' criterion and feed item levels.
                expandedTextAd.finalMobileUrls =
                    New String() { _
                                     "http://mobile.example.com/cruise/space/",
                                     "http://mobile.example.com/locations/mars/"
                                 }

                Dim adGroupAd As New AdGroupAd
                adGroupAd.adGroupId = adGroupId
                adGroupAd.ad = expandedTextAd

                ' Optional: Set the status.
                adGroupAd.status = AdGroupAdStatus.PAUSED

                ' Create the operation.
                Dim operation As New AdGroupAdOperation
                operation.operator = [Operator].ADD
                operation.operand = adGroupAd

                Dim retVal As AdGroupAdReturnValue = Nothing

                Try
                    ' Create the ads.
                    retVal = adGroupAdService.mutate(New AdGroupAdOperation() {operation})

                    ' Display the results.
                    If Not (retVal Is Nothing) AndAlso Not (retVal.value Is Nothing) Then
                        Dim newExpandedTextAd As ExpandedTextAd = CType(retVal.value(0).ad,
                                                                        ExpandedTextAd)

                        Console.WriteLine(
                            "Expanded text ad with ID '{0}' and headline '{1} - {2}' was added.",
                            newExpandedTextAd.id, newExpandedTextAd.headlinePart1,
                            newExpandedTextAd.headlinePart2)

                        Console.WriteLine("Upgraded URL properties:")

                        Console.WriteLine("  Final URLs: {0}",
                                          String.Join(", ", newExpandedTextAd.finalUrls))
                        Console.WriteLine("  Final Mobile URLS: {0}",
                                          String.Join(", ", newExpandedTextAd.finalMobileUrls))
                        Console.WriteLine("  Tracking URL template: {0}",
                                          newExpandedTextAd.trackingUrlTemplate)

                        Dim parameters As New List(Of String)
                        For Each customParam As CustomParameter In _
                            newExpandedTextAd.urlCustomParameters.parameters
                            parameters.Add(String.Format("{0}={1}", customParam.key,
                                                         customParam.value))
                        Next
                        Console.WriteLine("  Custom parameters: {0}",
                                          String.Join(", ", parameters.ToArray()))
                    Else
                        Console.WriteLine("No expanded text ads were created.")
                    End If
                Catch e As Exception
                    Throw _
                        New System.ApplicationException(
                            "Failed to add expanded text ad to adgroup.",
                            e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
