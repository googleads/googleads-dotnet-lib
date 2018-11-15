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
    ''' This code example adds expanded text ads to a given ad group. To list
    ''' ad groups, run GetAdGroups.vb.
    ''' </summary>
    Public Class AddExpandedTextAds
        Inherits ExampleBase

        ''' <summary>
        ''' Number of ads being added / updated in this code example.
        ''' </summary>
        Const NUMBER_OF_ADS As Integer = 5

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddExpandedTextAds
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
                Return "This code example adds expanded text ads to a given ad group. To list " &
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
            ' [START addExpandedTextAds] MOE:strip_line
            Using service As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupAdService),
                AdGroupAdService)

                Dim operations As New List(Of AdGroupAdOperation)

                For i As Integer = 1 To NUMBER_OF_ADS
                    ' [START addExpandedTextAd] MOE:strip_line
                    ' Create the expanded text ad.
                    Dim expandedTextAd As New ExpandedTextAd
                    expandedTextAd.headlinePart1 = "Cruise #" & i.ToString() & " to Mars"
                    expandedTextAd.headlinePart2 = "Best Space Cruise Line"
                    expandedTextAd.description = "Buy your tickets now!"
                    expandedTextAd.finalUrls = New String() {"http://www.example.com/" & i}

                    Dim expandedTextAdGroupAd As New AdGroupAd
                    expandedTextAdGroupAd.adGroupId = adGroupId
                    expandedTextAdGroupAd.ad = expandedTextAd

                    ' Optional: Set the status.
                    expandedTextAdGroupAd.status = AdGroupAdStatus.PAUSED
                    ' [END addExpandedTextAd] MOE:strip_line

                    ' Create the operations.
                    Dim operation As New AdGroupAdOperation
                    operation.operator = [Operator].ADD
                    operation.operand = expandedTextAdGroupAd

                    operations.Add(operation)
                Next

                Dim retVal As AdGroupAdReturnValue = Nothing

                Try
                    ' Create the ads.
                    retVal = service.mutate(operations.ToArray())

                    ' Display the results.
                    If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
                        For Each adGroupAd As AdGroupAd In retVal.value
                            Dim newAd As ExpandedTextAd = CType(adGroupAd.ad, ExpandedTextAd)
                            Console.WriteLine(
                                "Expanded text ad with ID '{0}' and headline '{1} - {2}' " +
                                "was added.", newAd.id, newAd.headlinePart1, newAd.headlinePart2)
                        Next
                    Else
                        Console.WriteLine("No expanded text ads were created.")
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to create expanded text ads.", e)
                End Try
                ' [END addExpandedTextAds] MOE:strip_line
            End Using
        End Sub
    End Class
End Namespace
