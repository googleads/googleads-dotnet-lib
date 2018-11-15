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
    ''' This code example imports offline call conversion values for calls related
    ''' to the ads in your account.
    ''' </summary>
    Public Class UploadOfflineCallConversions
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim conversionName As String = "INSERT_CONVERSION_NAME_HERE"

            ' For times use the format yyyyMMdd HHmmss tz. For more details on formats, see:
            ' https://developers.google.com/adwords/api/docs/appendix/codes-formats#date-and-time-formats
            ' For time zones, see:
            ' https://developers.google.com/adwords/api/docs/appendix/codes-formats#timezone-ids

            '  The conversion time should be after the call start time.
            Dim conversionTime As String = "INSERT_CONVERSION_TIME_HERE"
            Dim callStartTime As String = "INSERT_CALL_START_TIME_HERE"

            Dim conversionValue As Double = Double.Parse("INSERT_CONVERSION_VALUE_HERE")
            Dim callerId As String = "INSERT_CALLER_ID_HERE"

            Dim codeExample As New UploadOfflineCallConversions
            Console.WriteLine(codeExample.Description)
            Try
                codeExample.Run(New AdWordsUser, conversionName, callStartTime, callerId,
                                conversionTime, conversionValue)
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
                    "This code example imports offline call conversion values for calls related " &
                    " to the ads in your account."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="conversionName">The name of the call conversion to be updated.</param>
        ''' <param name="callStartTime">The call start time.</param>
        ''' <param name="conversionValue">The conversion value to be uploaded.</param>
        ''' <param name="callerId">The caller ID to be uploaded.</param>
        ''' <param name="conversionTime">The conversion time, in yyyymmdd hhmmss format.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal conversionName As String,
                       ByVal callStartTime As String, ByVal callerId As String,
                       ByVal conversionTime As String, ByVal conversionValue As Double)
            ' [START uploadOfflineCallConversions] MOE:strip_line
            Using offlineCallConversionFeedService As OfflineCallConversionFeedService =
                CType(user.GetService(AdWordsService.v201806.OfflineCallConversionFeedService),
                      OfflineCallConversionFeedService)

                ' Associate offline call conversions with the existing named conversion tracker. If 
                ' this tracker was newly created, it may be a few hours before it can accept 
                ' conversions.
                Dim feed As New OfflineCallConversionFeed()
                feed.callerId = callerId
                feed.callStartTime = callStartTime
                feed.conversionName = conversionName
                feed.conversionTime = conversionTime
                feed.conversionValue = conversionValue

                Dim feedOperation As New OfflineCallConversionFeedOperation()
                feedOperation.operator = [Operator].ADD
                feedOperation.operand = feed

                Try
                    ' This example uploads only one call conversion, but you can upload
                    ' multiple call conversions by passing additional operations.
                    Dim offlineCallConversionReturnValue As OfflineCallConversionFeedReturnValue =
                            offlineCallConversionFeedService.mutate(
                                New OfflineCallConversionFeedOperation() _
                                                                       {feedOperation})

                    ' Display results.
                    For Each feedResult As OfflineCallConversionFeed In _
                        offlineCallConversionReturnValue.value
                        Console.WriteLine(
                            "Uploaded offline call conversion value of {0} for caller ID '{1}'.",
                            feedResult.conversionValue, feedResult.callerId)
                    Next
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to upload offline call conversions.",
                                                        e)
                End Try
            End Using
            ' [END uploadOfflineCallConversions] MOE:strip_line
        End Sub
    End Class
End Namespace
