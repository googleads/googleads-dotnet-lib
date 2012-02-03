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
Imports Google.Api.Ads.AdWords.v201109

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example illustrates how to create a text ad with ad parameters.
  ''' To add an ad group, run AddAdGroup.vb. To add a keyword, run
  ''' AddKeyword.vb.
  '''
  ''' Tags: AdGroupAdService.mutate, AdParamService.mutate
  ''' </summary>
  Class SetAdParameters
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New SetAdParameters
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to create a text ad with ad parameters. To " & _
            "add an ad group, run AddAdGroup.vb. To add a keyword, run AddKeyword.vb."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"ADGROUP_ID", "CRITERION_ID"}
    End Function

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="parameters">The parameters for running the code
    ''' example.</param>
    ''' <param name="writer">The stream writer to which script output should be
    ''' written.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser, ByVal parameters As  _
        Dictionary(Of String, String), ByVal writer As TextWriter)
      ' Get the AdGroupAdService.
      Dim adGroupAdService As AdGroupAdService = user.GetService( _
          AdWordsService.v201109.AdGroupAdService)

      ' Get the AdParamService.
      Dim adParamService As AdParamService = user.GetService( _
          AdWordsService.v201109.AdParamService)

      Dim adGroupId As Long = Long.Parse(parameters("ADGROUP_ID"))
      Dim criterionId As Long = Long.Parse(parameters("CRITERION_ID"))

      ' Create the text ad.
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

      ' Create the operation.
      Dim adOperation As New AdGroupAdOperation
      adOperation.operand = adOperand
      adOperation.operator = [Operator].ADD

      Try
        ' Create the text ad.
        Dim retVal As AdGroupAdReturnValue = adGroupAdService.mutate( _
            New AdGroupAdOperation() {adOperation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) _
            AndAlso (retVal.value.Length > 0)) Then
          writer.WriteLine("Text ad with id = ""{0}"" was successfully added.", _
              retVal.value(0).ad.id)
        Else
          writer.WriteLine("No text ads were created.")
          Return
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to create text ads. Exception says ""{0}""", ex.Message)
        Return
      End Try

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
        Dim newAdParams As AdParam() = adParamService.mutate(New AdParamOperation() _
            {priceOperation, seatOperation})

        'Display the results.
        If (Not newAdParams Is Nothing) Then
          writer.WriteLine("Ad parameters were successfully updated.")
        Else
          writer.WriteLine("No ad parameters were set.")
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to set ad parameter(s). Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
