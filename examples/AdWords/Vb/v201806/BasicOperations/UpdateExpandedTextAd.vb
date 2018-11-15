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
    ''' This code example updates an expanded text ad. To get expanded text ads,
    ''' run GetExpandedTextAds.vb.
    ''' </summary>
    Public Class UpdateExpandedTextAd
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New UpdateExpandedTextAd
            Console.WriteLine(codeExample.Description)
            Try
                Dim adId As Long = Long.Parse("INSERT_AD_ID_HERE")
                codeExample.Run(New AdWordsUser, adId)

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
                    "This code example updates an expanded text ad. To get expanded text ads, run" +
                    " GetExpandedTextAds.cs."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adId">Id of the ad to be updated.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adId As Long)
            Using service As AdService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupAdService),
                AdService)

                ' Create an expanded text ad using the provided ad ID.
                Dim expandedTextAd As New ExpandedTextAd()
                expandedTextAd.id = adId

                ' Update some properties of the expanded text ad.
                expandedTextAd.headlinePart1 = "Cruise to Pluto #" +
                                               ExampleUtilities.GetShortRandomString()
                expandedTextAd.headlinePart2 = "Tickets on sale now"
                expandedTextAd.description = "Best space cruise ever."
                expandedTextAd.finalUrls = New String() {"http://www.example.com/"}
                expandedTextAd.finalMobileUrls = New String() {"http://www.example.com/mobile"}

                ' Create ad group ad operation And add it to the list.
                Dim operation As New AdOperation
                operation.operator = [Operator].SET
                operation.operand = expandedTextAd

                Try
                    ' Update the ad on the server.
                    Dim result As AdReturnValue = service.mutate(New AdOperation() {operation})
                    Dim updatedAd As ExpandedTextAd = CType(result.value(0), ExpandedTextAd)

                    ' Print out some information.
                    Console.WriteLine("Expanded text ad with ID {0} was updated.", updatedAd.id)
                    Console.WriteLine(
                        "Headline part 1: {0}\nHeadline part 2: {1}\nDescription: {2}" +
                        "\nFinal URL: {3}\nFinal mobile URL: {4}",
                        updatedAd.headlinePart1, updatedAd.headlinePart2, updatedAd.description,
                        updatedAd.finalUrls(0), updatedAd.finalMobileUrls(0))
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to update expanded text ad.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
