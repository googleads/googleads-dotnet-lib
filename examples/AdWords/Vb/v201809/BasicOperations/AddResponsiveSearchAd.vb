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
    ''' This code example adds a responsive search ad to a given ad group. To get ad groups,
    ''' run GetAdGroups.vb.
    ''' </summary>
    Public Class AddResponsiveSearchAd
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddResponsiveSearchAd
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
                    "This code example adds a responsive search ad to a given ad group. To get " +
                    "ad groups, run GetAdGroups.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Id of the ad group to which ads are added.
        ''' </param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            ' [START AddResponsiveSearchAd] MOE:strip_line
            Using service As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupAdService),
                AdGroupAdService)
                ' [START addResponsiveSearchAd] MOE:strip_line
                ' Create a responsive search ad.
                Dim responsiveSearchAd As New ResponsiveSearchAd()
                responsiveSearchAd.finalUrls = New String() {"http://www.example.com/cruise"}
                responsiveSearchAd.path1 = "all-inclusive"
                responsiveSearchAd.path2 = "deals"

                Dim textAsset1 As New TextAsset()
                textAsset1.assetText = "Cruise to Mars #" + ExampleUtilities.GetShortRandomString()

                Dim headline1 As New AssetLink()
                headline1.asset = textAsset1
                ' Set a pinning to always choose this asset for HEADLINE_1.
                ' Pinning Is optional; if no pinning is set, then headlines
                ' and descriptions will be rotated and the ones that perform
                ' best will be used more often.
                headline1.pinnedField = ServedAssetFieldType.HEADLINE_1

                Dim textAsset2 As New TextAsset()
                textAsset2.assetText = "Best Space Cruise Line"

                Dim headline2 As New AssetLink()
                headline2.asset = textAsset2

                Dim textAsset3 As New TextAsset()
                textAsset3.assetText = "Experience the Stars"

                Dim headline3 As New AssetLink()
                headline3.asset = textAsset3

                responsiveSearchAd.headlines = New AssetLink() {headline1, headline2, headline3}

                ' Create ad group ad.
                Dim adGroupAd As New AdGroupAd()
                adGroupAd.adGroupId = adGroupId
                adGroupAd.ad = responsiveSearchAd

                ' Optional: Set additional settings.
                adGroupAd.status = AdGroupAdStatus.PAUSED
                ' [END addResponsiveSearchAd] MOE:strip_line

                ' Create ad group ad operation and add it to the list.
                Dim operation As New AdGroupAdOperation()
                operation.operand = adGroupAd
                operation.operator = [Operator].ADD

                Try
                    ' Add the responsive search ad on the server.
                    Dim retval As AdGroupAdReturnValue = service.mutate(
                        New AdGroupAdOperation() _
                                                                           {operation})

                    ' Print out some information for the created ad.
                    For Each newAdGroupAd As AdGroupAd In retval.value
                        Dim newAd As ResponsiveSearchAd = CType(newAdGroupAd.ad, ResponsiveSearchAd)
                        Console.WriteLine("New responsive search ad with ID {0} was added.",
                                          newAd.id)
                        Console.WriteLine("Headlines:")

                        For Each headline As AssetLink In newAd.headlines
                            Dim textAsset As TextAsset = CType(headline.asset, TextAsset)
                            Console.WriteLine("    {0}", textAsset.assetText)
                            If headline.pinnedFieldSpecified Then
                                Console.WriteLine("      (pinned to {0})", headline.pinnedField)
                            End If
                        Next
                        Console.WriteLine("Descriptions:")
                        For Each description As AssetLink In newAd.descriptions
                            Dim textAsset As TextAsset = CType(description.asset, TextAsset)
                            Console.WriteLine("    {0}", textAsset.assetText)
                            If (description.pinnedFieldSpecified) Then
                                Console.WriteLine("      (pinned to {0})", description.pinnedField)
                            End If
                        Next
                    Next
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to create responsive search ad.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
