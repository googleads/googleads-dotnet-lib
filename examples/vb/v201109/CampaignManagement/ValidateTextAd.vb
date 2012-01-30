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
  ''' This code example shows how to use the validateOnly header to validate
  ''' a text ad. No objects will be created, but exceptions will still be
  ''' thrown.
  '''
  ''' Tags: AdGroupAdService.mutate
  ''' </summary>
  Class ValidateTextAd
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New ValidateTextAd
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters, Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    '''
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example shows how to use the validateOnly header to validate a text " & _
            "ad. No objects will be created, but exceptions will still be thrown."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary><returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"ADGROUP_ID"}
    End Function

    ''' <summary>
    ''' Runs the specified user.
    ''' </summary>
    ''' <param name="user">The user.</param>
    ''' <param name="parameters">The parameters.</param>
    ''' <param name="writer">The writer.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser, ByVal parameters As  _
        Dictionary(Of String, String), ByVal writer As TextWriter)
      ' Get the AdGroupAdService.
      Dim adGroupAdService As AdGroupAdService = user.GetService( _
          AdWordsService.v201109.AdGroupAdService)

      ' Set the validateOnly headers.
      adGroupAdService.RequestHeader.validateOnly = True

      Dim adGroupId As Long = Long.Parse(parameters.Item("ADGROUP_ID"))

      ' Create your text ad.
      Dim textAd As New TextAd
      textAd.headline = "Luxury Cruise to Mars"
      textAd.description1 = "Visit the Red Planet in style."
      textAd.description2 = "Low-gravity fun for everyone!!"
      textAd.displayUrl = "www.example.com"
      textAd.url = "http://www.example.com"

      Dim textAdGroupAd As New AdGroupAd
      textAdGroupAd.adGroupId = adGroupId
      textAdGroupAd.ad = textAd

      Dim textAdOperation As New AdGroupAdOperation
      textAdOperation.operator = [Operator].ADD
      textAdOperation.operand = textAdGroupAd
      Try
        Dim retVal As AdGroupAdReturnValue = adGroupAdService.mutate( _
            New AdGroupAdOperation() {textAdOperation})
        ' Since validation is ON, result will be null.
        writer.WriteLine("text ad validated successfully.")
      Catch ex As AdWordsApiException
        ' This block will be hit if there is a validation error from the server.
        writer.WriteLine("There were validation error(s) while adding text ad.")

        If (Not ex.ApiException Is Nothing) Then
          For Each apiError As ApiError In DirectCast(ex.ApiException, ApiException).errors
            writer.WriteLine("  Error type is '{0}' and fieldPath is '{1}'.", _
                apiError.ApiErrorType, apiError.fieldPath)
          Next
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to validate text ad. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace