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
    ''' This code example removes an ad group by setting the status to 'REMOVED'.
    ''' To get ad groups, run GetAdGroups.vb.
    ''' </summary>
    Public Class RemoveAdGroup
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New RemoveAdGroup
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
                    "This code example removes an ad group by setting the status to 'REMOVED'. " &
                    "To get ad groups, run GetAdGroups.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Id of the ad group to be removed.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            Using adGroupService As AdGroupService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupService),
                AdGroupService)

                ' Create ad group with REMOVED status.
                Dim adGroup As New AdGroup
                adGroup.id = adGroupId
                adGroup.status = AdGroupStatus.REMOVED

                ' Create the operation.
                Dim operation As New AdGroupOperation
                operation.operand = adGroup
                operation.operator = [Operator].SET

                Try
                    ' Remove the ad group.
                    Dim retVal As AdGroupReturnValue = adGroupService.mutate(
                        New AdGroupOperation() {operation})

                    ' Display the results.
                    If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso
                        (retVal.value.Length > 0)) Then
                        Dim removedAdGroup As AdGroup = retVal.value(0)
                        Console.WriteLine(
                            "Ad group with id = ""{0}"" and name = ""{1}"" was removed.",
                            removedAdGroup.id, removedAdGroup.name)
                    Else
                        Console.WriteLine("No ad groups were removed.")
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to remove ad groups.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
