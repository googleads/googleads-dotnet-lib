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
    ''' This code example imports offline conversion values for specific clicks to
    ''' your account. To get Google Click ID for a click, run
    ''' CLICK_PERFORMANCE_REPORT. To set up a conversion tracker, run the
    ''' AddConversionTrackers.vb example.
    ''' </summary>
    Public Class UploadOfflineConversions
        Inherits ExampleBase

        ''' <summary>
        ''' Returns a description about the code example.
        ''' </summary>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return _
                    "This code example imports offline conversion values for specific clicks to " &
                    "your account. To get Google Click ID for a click, run " &
                    "CLICK_PERFORMANCE_REPORT. To set up a conversion tracker, run the " & 
                    "AddConversionTrackers.vb example."
            End Get
        End Property

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim conversionName As String = "INSERT_CONVERSION_NAME_HERE"
            ' GCLID needs to be newer than 30 days.
            Dim gClId As String = "INSERT_GOOGLE_CLICK_ID_HERE"
            '  The conversion time should be higher than the click time.
            Dim conversionTime As String = "INSERT_CONVERSION_TIME_HERE"
            Dim conversionValue As Double = Double.Parse("INSERT_CONVERSION_VALUE_HERE")

            Dim codeExample As New UploadOfflineConversions
            Console.WriteLine(codeExample.Description)
            Try
                codeExample.Run(New AdWordsUser, conversionName, gClId, conversionTime,
                                conversionValue)
            Catch e As Exception
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
            End Try
        End Sub

        ' [START uploadOfflineConversions] MOE:strip_line
        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="conversionName">The name of the upload conversion to be
        ''' created.</param>
        ''' <param name="gClid">The Google Click ID of the click for which offline
        ''' conversions are uploaded.</param>
        ''' <param name="conversionValue">The conversion value to be uploaded.
        ''' </param>
        ''' <param name="conversionTime">The conversion time, in yyyymmdd hhmmss
        ''' format.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal conversionName As String,
                       ByVal gClid As String, ByVal conversionTime As String,
                       ByVal conversionValue As Double)
            Using _
                offlineConversionFeedService As OfflineConversionFeedService =
                    CType(
                        user.GetService(
                            AdWordsService.v201806.OfflineConversionFeedService),
                        OfflineConversionFeedService)

                Try
                    ' Associate offline conversions with the existing named conversion tracker. If
                    ' this tracker was newly created, it may be a few hours before it can accept
                    ' conversions.
                    Dim feed As New OfflineConversionFeed()
                    feed.conversionName = conversionName
                    feed.conversionTime = conversionTime
                    feed.conversionValue = conversionValue
                    feed.googleClickId = gClid

                    ' Optional: To upload fractional conversion credits, set the external 
                    ' attribution model and credit. To use this feature, your conversion tracker 
                    ' should be marked as externally attributed. See
                    ' https://developers.google.com/adwords/api/docs/guides/conversion-tracking#importing_externally_attributed_conversions
                    ' to learn more about importing externally attributed conversions.

                    ' feed.externalAttributionModel = "Linear"
                    ' feed.externalAttributionCredit = 0.3

                    Dim offlineConversionOperation As New OfflineConversionFeedOperation()
                    offlineConversionOperation.operator = [Operator].ADD
                    offlineConversionOperation.operand = feed

                    Dim offlineConversionRetval As OfflineConversionFeedReturnValue =
                            offlineConversionFeedService.mutate(
                                New OfflineConversionFeedOperation() {offlineConversionOperation})

                    Dim newFeed As OfflineConversionFeed = offlineConversionRetval.value(0)

                    Console.WriteLine(
                        "Uploaded offline conversion value of {0} for Google Click ID = " &
                        "'{1}' to '{2}'.", newFeed.conversionValue, newFeed.googleClickId,
                        newFeed.conversionName)
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to upload offline conversions.", e)
                End Try
            End Using
        End Sub
        ' [END uploadOfflineConversions] MOE:strip_line
    End Class
End Namespace
