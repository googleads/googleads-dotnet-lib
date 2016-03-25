' Copyright 2016, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example illustrates how to add ad group level mobile bid
  ''' modifier override.
  ''' </summary>
  Public Class AddAdGroupBidModifier
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddAdGroupBidModifier
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        Dim bidModifier As Double = Double.Parse("INSERT_ADGROUP_BID_MODIFIER_HERE")
        codeExample.Run(New AdWordsUser, adGroupId, bidModifier)
      Catch e As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(e))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to add ad group level mobile bid" & _
            " modifier override."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the adgroup for which bid modifier is
    ''' set.</param>
    ''' <param name="bidModifier">The mobile bid modifier for adgroup</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long, ByVal bidModifier As Double)
      ' Get the AdGroupBidModifierService.
      Dim adGroupBidModifierService As AdGroupBidModifierService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupBidModifierService), 
              AdGroupBidModifierService)

      ' Mobile criterion ID.
      Dim criterionId As Long = 30001

      ' Create the adgroup bid modifier.
      Dim adGroupBidModifier As New AdGroupBidModifier()
      adGroupBidModifier.bidModifier = bidModifier
      adGroupBidModifier.adGroupId = adGroupId

      Dim platform As New Platform()
      platform.id = criterionId

      adGroupBidModifier.criterion = platform

      Dim operation As New AdGroupBidModifierOperation()
      operation.operator = [Operator].ADD
      operation.operand = adGroupBidModifier

      Try
        ' Add ad group level mobile bid modifier.
        Dim retval As AdGroupBidModifierReturnValue = adGroupBidModifierService.mutate( _
            New AdGroupBidModifierOperation() {operation})

        ' Display the results.
        If Not retval Is Nothing AndAlso Not retval.value Is Nothing AndAlso _
            retval.value.Length > 0 Then
          Dim newBidModifier As AdGroupBidModifier = retval.value(0)
          Console.WriteLine("AdGroup ID {0}, Criterion ID {1} was updated with ad group " & _
              "level modifier: {2}", newBidModifier.adGroupId, newBidModifier.criterion.id, _
              newBidModifier.bidModifier)
        Else
          Console.WriteLine("No bid modifiers were added to the adgroup.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add bid modifiers to adgroup.", e)
      End Try
    End Sub
  End Class

End Namespace
