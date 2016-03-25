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
  ''' This code example illustrates how to retrieve all carriers and languages
  ''' available for targeting.
  ''' </summary>
  Public Class GetTargetableLanguagesAndCarriers
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetTargetableLanguagesAndCarriers
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
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
        Return "This code example illustrates how to retrieve all carriers and languages" & _
            "available for targeting."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the ConstantDataService.
      Dim constantDataService As ConstantDataService = CType(user.GetService( _
          AdWordsService.v201603.ConstantDataService), ConstantDataService)

      Try
        ' Get all carriers.
        Dim carriers As Carrier() = constantDataService.getCarrierCriterion

        ' Display the results.
        If (Not carriers Is Nothing) Then
          For Each carrier As Carrier In carriers
            Console.WriteLine("Carrier name is '{0}', ID is {1} and country code is '{2}'.", _
                carrier.name, carrier.id, carrier.countryCode)
          Next
        Else
          Console.WriteLine("No carriers were retrieved.")
        End If

        ' Get all languages.
        Dim languages As Language() = constantDataService.getLanguageCriterion

        ' Display the results.
        If (Not languages Is Nothing) Then
          For Each language As Language In languages
            Console.WriteLine("Language name is '{0}', ID is {1} and code is '{2}'.", _
                language.name, language.id, language.code)
          Next
        Else
          Console.WriteLine("No languages were found.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to get targetable carriers and languages.", _
            e)
      End Try
    End Sub
  End Class
End Namespace
