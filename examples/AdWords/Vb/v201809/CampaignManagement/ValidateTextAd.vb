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
    ''' This code example shows how to use the validateOnly header to validate
    ''' an expanded text ad. No objects will be created, but exceptions will
    ''' still be thrown.
    ''' </summary>
    Public Class ValidateTextAd
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New ValidateTextAd
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
        '''
        Public Overrides ReadOnly Property Description() As String
            Get
                Return _
                    "This code example shows how to use the validateOnly header to validate an " &
                    "expanded text ad. No objects will be created, but exceptions will still be " &
                    "thrown."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Id of the ad group to which text ads are
        ''' added.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            Using adGroupAdService As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupAdService),
                AdGroupAdService)

                ' Set the validateOnly headers.
                adGroupAdService.RequestHeader.validateOnly = True

                ' Create your expanded text ad.
                Dim expandedTextAd As New ExpandedTextAd()
                expandedTextAd.headlinePart1 = "Luxury Cruise to Mars"
                expandedTextAd.headlinePart2 = "Visit the Red Planet in style."
                expandedTextAd.description = "Low-gravity fun for everyone!!"
                expandedTextAd.finalUrls = New String() {"http://www.example.com"}

                Dim adGroupAd As New AdGroupAd
                adGroupAd.adGroupId = adGroupId
                adGroupAd.ad = expandedTextAd

                Dim operation As New AdGroupAdOperation
                operation.operator = [Operator].ADD
                operation.operand = adGroupAd
                Try
                    Dim retVal As AdGroupAdReturnValue = adGroupAdService.mutate(
                        New AdGroupAdOperation() {operation})
                    ' Since validation is ON, result will be null.
                    Console.WriteLine("Expanded text ad validated successfully.")
                Catch e As AdWordsApiException
                    ' This block will be hit if there is a validation error from the server.
                    Console.WriteLine(
                        "There were validation error(s) while adding expanded text ad.")

                    If (Not e.ApiException Is Nothing) Then
                        For Each apiError As ApiError In _
                            DirectCast(e.ApiException, ApiException).errors
                            Console.WriteLine("  Error type is '{0}' and fieldPath is '{1}'.",
                                              apiError.ApiErrorType, apiError.fieldPath)
                        Next
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to validate expanded text ad.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
