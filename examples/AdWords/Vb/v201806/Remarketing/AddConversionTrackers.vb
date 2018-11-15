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
    ''' This code example adds an AdWords conversion tracker and an upload conversion tracker.
    ''' </summary>
    Public Class AddConversionTrackers
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddConversionTrackers
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
        '''
        Public Overrides ReadOnly Property Description() As String
            Get
                Return _
                    "This code example adds an AdWords conversion tracker and an upload " &
                    "conversion tracker."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            ' Get the ConversionTrackerService.
            Using conversionTrackerService As ConversionTrackerService = CType(
                user.GetService(
                    AdWordsService.v201806.ConversionTrackerService),
                ConversionTrackerService)

                Dim conversionTrackers As New List(Of ConversionTracker)

                ' [START createAdWordsConversion] MOE:strip_line
                ' Create an Adwords conversion tracker.
                Dim adWordsConversionTracker As New AdWordsConversionTracker()
                adWordsConversionTracker.name = "Earth to Mars Cruises Conversion #" &
                                                ExampleUtilities.GetRandomString()
                adWordsConversionTracker.category = ConversionTrackerCategory.DEFAULT

                ' Set optional fields.
                adWordsConversionTracker.status = ConversionTrackerStatus.ENABLED
                adWordsConversionTracker.viewthroughLookbackWindow = 15
                adWordsConversionTracker.defaultRevenueValue = 23.41
                adWordsConversionTracker.alwaysUseDefaultRevenueValue = True
                conversionTrackers.Add(adWordsConversionTracker)
                ' [END createAdWordsConversion] MOE:strip_line

                ' [START createUploadConversion] MOE:strip_line
                ' Create an upload conversion for offline conversion imports.
                Dim uploadConversion As New UploadConversion()
                ' Set an appropriate category. This field is optional, and will be set to
                ' DEFAULT if not mentioned.
                uploadConversion.category = ConversionTrackerCategory.LEAD
                uploadConversion.name = "Upload Conversion #" + ExampleUtilities.GetRandomString()
                uploadConversion.viewthroughLookbackWindow = 30
                uploadConversion.ctcLookbackWindow = 90

                ' Optional: Set the default currency code to use for conversions
                ' that do not specify a conversion currency. This must be an ISO 4217
                ' 3-character currency code such as "EUR" or "USD".
                ' If this field is not set on this UploadConversion, AdWords will use
                ' the account's currency.
                uploadConversion.defaultRevenueCurrencyCode = "EUR"

                ' Optional: Set the default revenue value to use for conversions
                ' that do not specify a conversion value. Note that this value
                ' should NOT be in micros.
                uploadConversion.defaultRevenueValue = 2.5

                ' Optional: To upload fractional conversion credits, mark the upload conversion
                ' as externally attributed. See
                ' https://developers.google.com/adwords/api/docs/guides/conversion-tracking#importing_externally_attributed_conversions
                ' to learn more about importing externally attributed conversions.

                ' uploadConversion.isExternallyAttributed = True

                conversionTrackers.Add(uploadConversion)
                ' [END createUploadConversion] MOE:strip_line

                Try
                    ' [START mutateRequest] MOE:strip_line
                    ' Create operations.
                    Dim operations As New List(Of ConversionTrackerOperation)
                    For Each conversionTracker As ConversionTracker In conversionTrackers
                        Dim operation As New ConversionTrackerOperation()
                        operation.operator = [Operator].ADD
                        operation.operand = conversionTracker
                        operations.Add(operation)
                    Next

                    ' Add conversion tracker.
                    Dim retval As ConversionTrackerReturnValue = conversionTrackerService.mutate(
                        operations.ToArray())
                    ' [END mutateRequest] MOE:strip_line

                    ' Display the results.
                    If (Not retval Is Nothing) AndAlso (Not retval.value Is Nothing) AndAlso
                       retval.value.Length > 0 Then
                        For Each conversionTracker As ConversionTracker In retval.value
                            Console.WriteLine(
                                "Conversion with ID {0}, name '{1}', status '{2}' and " &
                                "category '{3}' was added.", conversionTracker.id,
                                conversionTracker.name,
                                conversionTracker.status, conversionTracker.category)

                            If TypeOf conversionTracker Is AdWordsConversionTracker Then
                                Dim newAdWordsConversionTracker As AdWordsConversionTracker =
                                        CType(conversionTracker, AdWordsConversionTracker)

                                Console.WriteLine(
                                    "Google global site tag:\n{0}\nGoogle event snippet:\n{1}",
                                    newAdWordsConversionTracker.googleGlobalSiteTag,
                                    newAdWordsConversionTracker.googleEventSnippet
                                    )
                            Else
                            End If
                        Next
                    Else
                        Console.WriteLine("No conversion trackers were added.")
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to add conversion trackers.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
