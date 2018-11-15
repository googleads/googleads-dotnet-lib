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
    ''' This code example illustrates how to create a text ad with ad parameters.
    ''' To add an ad group, run AddAdGroup.vb. To add a keyword, run
    ''' AddKeyword.vb.
    ''' </summary>
    Public Class SetAdParameters
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New SetAdParameters
            Console.WriteLine(codeExample.Description)
            Try
                Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
                Dim criterionId As Long = Long.Parse("INSERT_CRITERION_ID_HERE")

                codeExample.Run(New AdWordsUser, adGroupId, criterionId)
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
                    "This code example illustrates how to create a text ad with ad parameters. To" &
                    " add an ad group, run AddAdGroup.vb. To add a keyword, run AddKeyword.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Id of the ad group that contains the criterion.
        ''' </param>
        ''' <param name="criterionId">Id of the keyword for which the ad
        ''' parameters are set.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long,
                       ByVal criterionId As Long)
            Using adGroupAdService As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupAdService),
                AdGroupAdService)
                Using adParamService As AdParamService = CType(
                    user.GetService(
                        AdWordsService.v201806.AdParamService),
                    AdParamService)

                    ' Create the expanded text ad.
                    Dim expandedTextAd As New ExpandedTextAd()
                    expandedTextAd.headlinePart1 = "Mars Cruises"
                    expandedTextAd.headlinePart2 = "Low-gravity fun for {param1:cheap}."
                    expandedTextAd.description = "Only {param2:a few} seats left!"
                    expandedTextAd.finalUrls = New String() {"http://www.example.com"}

                    Dim adOperand As New AdGroupAd
                    adOperand.adGroupId = adGroupId
                    adOperand.status = AdGroupAdStatus.ENABLED
                    adOperand.ad = expandedTextAd

                    ' Create the operation.
                    Dim adOperation As New AdGroupAdOperation
                    adOperation.operand = adOperand
                    adOperation.operator = [Operator].ADD

                    ' Create the expanded text ad.
                    Dim retVal As AdGroupAdReturnValue = adGroupAdService.mutate(
                        New AdGroupAdOperation() {adOperation})

                    ' Display the results.
                    If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) _
                        AndAlso (retVal.value.Length > 0)) Then
                        Console.WriteLine(
                            "Expanded text ad with id = ""{0}"" was successfully added.",
                            retVal.value(0).ad.id)
                    Else
                        Throw New System.ApplicationException("Failed to create expanded text ads.")
                        Return
                    End If

                    ' Create the ad param for price.
                    Dim priceParam As New AdParam
                    priceParam.adGroupId = adGroupId
                    priceParam.criterionId = criterionId
                    priceParam.paramIndex = 1
                    priceParam.insertionText = "$100"

                    ' Create the ad param for seats.
                    Dim seatParam As New AdParam
                    seatParam.adGroupId = adGroupId
                    seatParam.criterionId = criterionId
                    seatParam.paramIndex = 2
                    seatParam.insertionText = "50"

                    ' Create the operations.
                    Dim priceOperation As New AdParamOperation
                    priceOperation.operator = [Operator].SET
                    priceOperation.operand = priceParam

                    Dim seatOperation As New AdParamOperation
                    seatOperation.operator = [Operator].SET
                    seatOperation.operand = seatParam

                    Try
                        ' Set the ad parameters.
                        Dim newAdParams As AdParam() = adParamService.mutate(
                            New AdParamOperation() _
                                                                                {priceOperation,
                                                                                 seatOperation})

                        'Display the results.
                        If (Not newAdParams Is Nothing) Then
                            Console.WriteLine("Ad parameters were successfully updated.")
                        Else
                            Console.WriteLine("No ad parameters were set.")
                        End If
                    Catch e As Exception
                        Throw New System.ApplicationException("Failed to set ad parameter(s).", e)
                    End Try
                End Using
            End Using
        End Sub
    End Class
End Namespace
