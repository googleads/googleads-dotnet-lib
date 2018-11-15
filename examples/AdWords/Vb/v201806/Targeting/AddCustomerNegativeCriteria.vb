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
    ''' This code example adds various types of negative criteria to a customer. These criteria
    ''' will be applied to all campaigns for the customer.
    ''' </summary>
    Public Class AddCustomerNegativeCriteria
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddCustomerNegativeCriteria
            Console.WriteLine(codeExample.Description)
            Try
                codeExample.Run(New AdWordsUser())
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
                Return "This code example adds various types of negative criteria to a customer. " +
                       "These criteria will be applied to all campaigns for the customer."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            ' [START addNegativeCriteria] MOE:strip_line
            Using customerNegativeCriterionService As CustomerNegativeCriterionService =
                CType(user.GetService(AdWordsService.v201806.CustomerNegativeCriterionService),
                      CustomerNegativeCriterionService)

                Dim criteria As New List(Of Criterion)

                ' Exclude tragedy & conflict content.
                Dim tragedyContentLabel As New ContentLabel()
                tragedyContentLabel.contentLabelType = ContentLabelType.TRAGEDY
                criteria.Add(tragedyContentLabel)

                ' Exclude a specific placement.
                Dim placement As New Placement()
                placement.url = "http://www.example.com"
                criteria.Add(placement)

                ' Additional criteria types are available for this service. See the types listed
                ' under Criterion here:
                ' https://developers.google.com/adwords/api/docs/reference/latest/CustomerNegativeCriterionService.Criterion

                ' Create operations to add each of the criteria above.
                Dim operations As New List(Of CustomerNegativeCriterionOperation)
                For Each criterion As Criterion In criteria
                    Dim negativeCriterion As New CustomerNegativeCriterion()
                    negativeCriterion.criterion = criterion
                    Dim operation As New CustomerNegativeCriterionOperation()
                    operation.operator = [Operator].ADD
                    operation.operand = negativeCriterion
                    operations.Add(operation)
                Next

                Try
                    ' Send the request to add the criteria.
                    Dim result As CustomerNegativeCriterionReturnValue =
                            customerNegativeCriterionService.mutate(operations.ToArray())

                    ' Display the results.
                    For Each negativeCriterion As CustomerNegativeCriterion In result.value
                        Console.WriteLine(
                            "Customer negative criterion with criterion ID {0} and type '{1}' " +
                            "was added.", negativeCriterion.criterion.id,
                            negativeCriterion.criterion.type)
                    Next
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to set customer negative criteria.",
                                                        e)
                End Try

            End Using
            ' [END addNegativeCriteria] MOE:strip_line
        End Sub
    End Class
End Namespace
