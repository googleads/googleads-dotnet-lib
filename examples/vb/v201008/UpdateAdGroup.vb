' Copyright 2011, Google Inc. All Rights Reserved.
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

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example illustrates how to update an ad group, setting its
  ''' status to 'PAUSED'. To create an ad group, run AddAdGroup.vb.
  '''
  ''' Tags: AdGroupService.mutate
  ''' </summary>
  Class UpdateAdGroup
    Inherits SampleBase
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
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New UpdateAdGroup
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = user.GetService(AdWordsService.v201008.AdGroupService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_AD_GROUP_ID_HERE"))

      Dim adGroup As New AdGroup
      adGroup.status = AdGroupStatus.PAUSED
      adGroup.id = adGroupId

      Dim operation As New AdGroupOperation
      operation.operator = [Operator].SET
      operation.operand = adGroup

      Try
        Dim retVal As AdGroupReturnValue = adGroupService.mutate(New AdGroupOperation() {operation})
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          For Each adGroupValue As AdGroup In retVal.value
            Console.WriteLine("Ad group with id = '{0}' was successfully updated.", adGroupValue.id)
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to update ad group(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
