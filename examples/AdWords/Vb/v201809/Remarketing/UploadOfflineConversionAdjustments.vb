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
    ''' This code example demonstrates adjusting one conversion, but you can add more than one
    ''' operation in a single mutate request.
    ''' </summary>
    Public Class UploadOfflineConversionAdjustments
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New UploadOfflineConversionAdjustments
            Console.WriteLine(codeExample.Description)
            Try
                Dim conversionName As String = "INSERT_CONVERSION_NAME_HERE"
                Dim gclid As String = "INSERT_GOOGLE_CLICK_ID_HERE"
                Dim conversionTime As String = "INSERT_CONVERSION_TIME_HERE"
                Dim adjustmentType As OfflineConversionAdjustmentType =
                        DirectCast([Enum].Parse(GetType(OfflineConversionAdjustmentType),
                                                "INSERT_ADJUSTMENT_TYPE_HERE"),
                                   OfflineConversionAdjustmentType)
                Dim adjustmentTime As String = "INSERT_ADGROUP_ID_HERE"
                Dim adjustedValue As Double = Double.Parse("INSERT_ADGROUP_ID_HERE")

                codeExample.Run(New AdWordsUser, conversionName, gclid, conversionTime,
                                adjustmentType,
                                adjustmentTime, adjustedValue)
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
                    "This code example demonstrates adjusting one conversion, but you can add more " +
                    "than one operation in a single mutate request."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="conversionName">Name of the conversion to make adjustments.</param>
        ''' <param name="gclid">The google click ID for the adjustment.</param>
        ''' <param name="conversionTime">The conversion time.</param>
        ''' <param name="adjustmentType">The type of conversion adjustment.</param>
        ''' <param name="adjustmentTime">The conversion adjustment time.</param>
        ''' <param name="adjustedValue">The conversion adjustment value.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal conversionName As String,
                       ByVal gclid As String, ByVal conversionTime As String,
                       ByVal adjustmentType As OfflineConversionAdjustmentType,
                       ByVal adjustmentTime As String,
                       ByVal adjustedValue As Double)
            Using service As OfflineConversionAdjustmentFeedService = CType(
                user.GetService(
                    AdWordsService.v201809.OfflineConversionAdjustmentFeedService),
                OfflineConversionAdjustmentFeedService)

                ' Associate conversion adjustments with the existing named conversion
                ' tracker. The GCLID should have been uploaded before with a
                ' conversion.
                Dim feed As New GclidOfflineConversionAdjustmentFeed()
                feed.conversionName = conversionName
                feed.googleClickId = gclid
                feed.conversionTime = conversionTime
                feed.adjustmentType = adjustmentType
                feed.adjustmentTime = adjustmentTime
                feed.adjustedValue = adjustedValue

                ' Create the operation.
                Dim operation As New OfflineConversionAdjustmentFeedOperation()
                operation.operator = [Operator].ADD
                operation.operand = feed

                Try
                    ' Issue a request to the servers for adjustments of the conversion.
                    Dim retval As OfflineConversionAdjustmentFeedReturnValue = service.mutate(
                        New OfflineConversionAdjustmentFeedOperation() {operation})
                    Dim updatedFeed As GclidOfflineConversionAdjustmentFeed =
                            CType(retval.value(0), GclidOfflineConversionAdjustmentFeed)

                    Console.WriteLine("Uploaded conversion adjustment value of '{0}' for Google " +
                                      "Click ID '{1}'.", updatedFeed.conversionName,
                                      updatedFeed.googleClickId)
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to update conversion adjustment.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
