' Copyright 2016, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example illustrates how to update an ad group, setting its
  ''' status to 'PAUSED'. To create an ad group, run AddAdGroup.vb.
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
        codeExample.Run(New AdWordsUser, adGroupId)
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
        Return "This code example illustrates how to update an ad group, setting its status " & _
            "to 'PAUSED'. To create an ad group, run AddAdGroup.vb"
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group to be updated.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupService), AdGroupService)

      ' Create the ad group.
      Dim adGroup As New AdGroup
      adGroup.status = AdGroupStatus.PAUSED
      adGroup.id = adGroupId

      ' Create the operation.
      Dim operation As New AdGroupOperation
      operation.operator = [Operator].SET
      operation.operand = adGroup

      Try
        ' Update the ad group.
        Dim retVal As AdGroupReturnValue = adGroupService.mutate(New AdGroupOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim pausedAdGroup As AdGroup = retVal.value(0)
          Console.WriteLine("Ad group with id = '{0}' was successfully updated.", _
              pausedAdGroup.id)
        Else
          Console.WriteLine("No ad groups were updated.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to update ad groups.", e)
      End Try
    End Sub
  End Class
End Namespace
