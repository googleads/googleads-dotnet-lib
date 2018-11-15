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
    ''' This code example demonstrates how to handle partial failures.
    ''' </summary>
    Public Class HandlePartialFailures
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New HandlePartialFailures
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
                Return "This code example demonstrates how to handle partial failures."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Id of the ad group to which keywords are added.
        ''' </param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            Using adGroupCriterionService As AdGroupCriterionService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupCriterionService),
                AdGroupCriterionService)

                ' [START enablePartialFailure] MOE:strip_line
                ' Set partial failure mode for the service.
                adGroupCriterionService.RequestHeader.partialFailure = True
                ' [END enablePartialFailure] MOE:strip_line

                Try
                    ' [START addKeywords] MOE:strip_line
                    Dim operations As New List(Of AdGroupCriterionOperation)

                    ' Create the keywords.
                    Dim keywords As String() = New String() { _
                                                                "mars cruise", "inv@lid cruise",
                                                                "venus cruise",
                                                                "b(a)d keyword cruise"}

                    For Each keywordText As String In keywords
                        Dim keyword As New Keyword
                        keyword.text = keywordText
                        keyword.matchType = KeywordMatchType.BROAD

                        ' Create biddable ad group criterion.
                        Dim keywordBiddableAdGroupCriterion As New BiddableAdGroupCriterion
                        keywordBiddableAdGroupCriterion.adGroupId = adGroupId
                        keywordBiddableAdGroupCriterion.criterion = keyword

                        ' Create the operation.
                        Dim keywordAdGroupCriterionOperation As New AdGroupCriterionOperation
                        keywordAdGroupCriterionOperation.operand = keywordBiddableAdGroupCriterion
                        keywordAdGroupCriterionOperation.operator = [Operator].ADD
                        operations.Add(keywordAdGroupCriterionOperation)
                    Next

                    ' Create the keywords.
                    Dim result As AdGroupCriterionReturnValue = adGroupCriterionService.mutate(
                        operations.ToArray)
                    ' [END addKeywords] MOE:strip_line

                    ' [START handleSuccessfulOperations] MOE:strip_line
                    ' Display the results.
                    If ((Not result Is Nothing) AndAlso (Not result.value Is Nothing)) Then
                        For Each adGroupCriterionResult As AdGroupCriterion In result.value
                            If (Not adGroupCriterionResult.criterion Is Nothing) Then
                                Console.WriteLine(
                                    "Keyword with ad group id '{0}', and criterion id " &
                                    "'{1}', and text '{2}' was added.\n",
                                    adGroupCriterionResult.adGroupId,
                                    adGroupCriterionResult.criterion.id,
                                    DirectCast(adGroupCriterionResult.criterion, Keyword).text)
                            End If
                        Next
                    Else
                        Console.WriteLine("No keywords were added.")
                    End If
                    ' [END handleSuccessfulOperations] MOE:strip_line

                    ' [START handleFailedOperations] MOE:strip_line
                    ' Display the partial failure errors.
                    If _
                        ((Not result Is Nothing) AndAlso
                         (Not result.partialFailureErrors Is Nothing)) Then
                        For Each apiError As ApiError In result.partialFailureErrors
                            Dim operationIndex As Integer = apiError.GetOperationIndex()
                            If (operationIndex <> - 1) Then
                                Dim adGroupCriterion As AdGroupCriterion =
                                        operations(operationIndex).operand
                                Console.WriteLine("Keyword with ad group id '{0}' and text '{1}' " &
                                                  "triggered a failure for the following reason: " &
                                                  "'{2}'.\n",
                                                  adGroupCriterion.adGroupId,
                                                  DirectCast(adGroupCriterion.criterion, Keyword).
                                                     text,
                                                  apiError.errorString)
                            Else
                                Console.WriteLine(
                                    "A failure for the following reason: '{0}' has occurred.\n",
                                    apiError.errorString)
                            End If
                        Next
                    End If
                    ' [END handleFailedOperations] MOE:strip_line
                Catch e As Exception
                    Throw _
                        New System.ApplicationException(
                            "Failed to add keyword(s) in partial failure " +
                            "mode.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
