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
    ''' This code example accepts a pending invitation to link your AdWords
    ''' account to a Google Merchant Center account.
    ''' </summary>
    Public Class AcceptServiceLink
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AcceptServiceLink
            Console.WriteLine(codeExample.Description)
            Try
                Dim serviceLinkId As Long = Long.Parse("INSERT_SERVICE_LINK_ID_HERE")
                codeExample.Run(New AdWordsUser, serviceLinkId)
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
                Return "This code example accepts a pending invitation to link your AdWords " &
                       "account to a Google Merchant Center account."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="serviceLinkId">The service link ID to accept.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal serviceLinkId As Long)
            ' [START acceptServiceLink] MOE:strip_line
            Using customerService As CustomerService = CType(
                user.GetService(
                    AdWordsService.v201806.CustomerService),
                CustomerService)

                ' Create the operation to set the status to ACTIVE.
                Dim op As New ServiceLinkOperation()
                op.operator = [Operator].SET
                Dim serviceLink As New ServiceLink()
                serviceLink.serviceLinkId = serviceLinkId
                serviceLink.serviceType = ServiceType.MERCHANT_CENTER
                serviceLink.linkStatus = ServiceLinkLinkStatus.ACTIVE
                op.operand = serviceLink

                Try
                    ' Update the service link.
                    Dim mutatedServiceLinks As ServiceLink() =
                            customerService.mutateServiceLinks(New ServiceLinkOperation() {op})

                    ' Display the results.
                    For Each mutatedServiceLink As ServiceLink In mutatedServiceLinks
                        Console.WriteLine(
                            "Service link with service link ID {0}, type '{1}' updated to " &
                            "status: {2}.", mutatedServiceLink.serviceLinkId,
                            mutatedServiceLink.serviceType,
                            mutatedServiceLink.linkStatus)
                    Next
                    ' [END acceptServiceLink] MOE:strip_line
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to update service link.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
