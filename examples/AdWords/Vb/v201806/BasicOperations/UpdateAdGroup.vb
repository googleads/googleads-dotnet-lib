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
    ''' This code example illustrates how to update an ad group, setting its
    ''' status to 'PAUSED', and its CPC bid to a new value if specified.
    ''' To create an ad group, run AddAdGroup.vb.
    ''' </summary>
    Public Class UpdateAdGroup
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New UpdateAdGroup
            Console.WriteLine(codeExample.Description)
            Try
                Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
                Dim bidMicroAmount As Long? = Nothing

                ' Optional: Provide a cpc bid for the ad group, in micro amounts.
                Dim tempVal As Long = 0
                If Long.TryParse("INSERT_CPC_BID_IN_MICROS_HERE", tempVal) Then
                    bidMicroAmount = New Nullable(Of Long)(tempVal)
                End If

                codeExample.Run(New AdWordsUser, adGroupId, bidMicroAmount)
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
                    "This code example illustrates how to update an ad group, setting its status " &
                    "to 'PAUSED', and its CPC bid to a new value if specified. To create an ad " &
                    "group, run AddAdGroup.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Id of the ad group to be updated.</param>
        ''' <param name="bidMicroAmount">The CPC bid amount in micros.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long,
                       ByVal bidMicroAmount As Long?)
            Using adGroupService As AdGroupService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupService),
                AdGroupService)

                ' [START updateAdGroup] MOE:strip_line
                ' Create an ad group with the specified ID.
                Dim adGroup As New AdGroup
                adGroup.id = adGroupId

                ' Pause the ad group.
                adGroup.status = AdGroupStatus.PAUSED

                ' Update the CPC bid if specified.
                If bidMicroAmount.HasValue() Then
                    Dim biddingStrategyConfiguration As New BiddingStrategyConfiguration()
                    Dim cpcBidMoney = New Money()
                    cpcBidMoney.microAmount = bidMicroAmount.Value
                    Dim cpcBid As New CpcBid()
                    cpcBid.bid = cpcBidMoney
                    biddingStrategyConfiguration.bids = New Bids() {cpcBid}
                    adGroup.biddingStrategyConfiguration = biddingStrategyConfiguration
                End If

                ' Create the operation.
                Dim operation As New AdGroupOperation
                operation.operator = [Operator].SET
                operation.operand = adGroup

                Try
                    ' Update the ad group.
                    Dim retVal As AdGroupReturnValue = adGroupService.mutate(
                        New AdGroupOperation() {operation})
                    ' [END updateAdGroup] MOE:strip_line

                    ' Display the results.
                    If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso
                        (retVal.value.Length > 0)) Then
                        Dim adGroupResult As AdGroup = retVal.value(0)
                        Dim bsConfig As BiddingStrategyConfiguration =
                                adGroupResult.biddingStrategyConfiguration

                        ' Find the CpcBid in the bidding strategy configuration's bids collection.
                        Dim cpcBidMicros As Long = 0L
                        If (Not bsConfig Is Nothing) AndAlso (Not bsConfig.bids Is Nothing) Then
                            For Each Bid As Bids In bsConfig.bids
                                If TypeOf Bid Is CpcBid Then
                                    cpcBidMicros = DirectCast(Bid, CpcBid).bid.microAmount
                                    Exit For
                                End If
                            Next
                        End If
                        Console.WriteLine(
                            "Ad group with ID {0} and name '{1}' updated to have status '{2}'" &
                            " and CPC bid {3}", adGroupResult.id, adGroupResult.name,
                            adGroupResult.status, cpcBidMicros)
                    Else
                        Console.WriteLine("No ad groups were updated.")
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to update ad groups.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
