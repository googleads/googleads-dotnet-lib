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
  ''' This code example illustrates how to create a text ad with ad parameters.
  ''' To add an ad group, run AddAdGroup.vb. To add an ad group criterion,
  ''' run AddAdGroupCriterion.vb.
  '''
  ''' Tags: AdGroupAdService.mutate, AdParamService.mutate
  ''' </summary>
  Class SetAdParams
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to create a text ad with ad parameters. To " & _
            "add an ad group, run AddAdGroup.vb. To add an ad group criterion, run " & _
            "AddAdGroupCriterion.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New SetAdParams
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      Dim adGroupAdService As AdGroupAdService = user.GetService( _
          AdWordsService.v201008.AdGroupAdService)

      Dim adParamService As AdParamService = user.GetService( _
          AdWordsService.v201008.AdParamService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_AD_GROUP_ID_HERE"))
      Dim criterionId As Long = Long.Parse(_T("INSERT_CRITERION_ID_HERE"))

      Dim textAd As New TextAd
      textAd.url = "http://www.example.com"
      textAd.displayUrl = "example.com"
      textAd.headline = " Mars Cruises"
      textAd.description1 = "Low-gravity fun for {param1:cheap}."
      textAd.description2 = "Only {param2:a few} seats left!"

      Dim adOperand As New AdGroupAd
      adOperand.adGroupId = adGroupId
      adOperand.status = AdGroupAdStatus.ENABLED
      adOperand.ad = textAd

      Dim adOperation As New AdGroupAdOperation
      adOperation.operand = adOperand
      adOperation.operator = [Operator].ADD

      Try
        Dim retVal As AdGroupAdReturnValue = adGroupAdService.mutate( _
            New AdGroupAdOperation() {adOperation})
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) _
            AndAlso (retVal.value.Length > 0)) Then
          Console.WriteLine("Text ad id {0} was successfully added.", retVal.value(0).ad.id)
        Else
          Console.WriteLine("No ads were created.")
          Return
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to create ad(s). Exception says ""{0}""", ex.Message)
        Return
      End Try

      ' Prepare for setting ad parameters.
      Dim priceParam As New AdParam
      priceParam.adGroupId = adGroupId
      priceParam.criterionId = criterionId
      priceParam.paramIndex = 1
      priceParam.insertionText = "$100"

      Dim priceOperation As New AdParamOperation
      priceOperation.operator = [Operator].SET
      priceOperation.operand = priceParam

      Dim seatParam As New AdParam
      seatParam.adGroupId = adGroupId
      seatParam.criterionId = criterionId
      seatParam.paramIndex = 2
      seatParam.insertionText = "50"

      Dim seatOperation As New AdParamOperation
      seatOperation.operator = [Operator].SET
      seatOperation.operand = seatParam

      Try
        ' Set ad parameters.
        Dim newAdParams As AdParam() = adParamService.mutate(New AdParamOperation() _
            {priceOperation, seatOperation})

        If (Not newAdParams Is Nothing) Then
          Console.WriteLine("Ad parameters were successfully updated.")
        Else
          Console.WriteLine("No ad parameters were set.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to set ad ad parameter(s). Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
