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

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB
  ''' <summary>
  ''' This abstract class represents a code example.
  ''' </summary>
  Public MustInherit Class ExampleBase
    ''' <summary>
    ''' Delegate for accepting user inputs for this code example.
    ''' </summary>
    ''' <param name="parameterNames">The list of parameter param names.</param>
    ''' <returns>A dictionary, with key as parameter name and value as parameter
    ''' value.</returns>
    Public Delegate Function GetUserInputsMethod(ByVal parameterNames As String()) _
        As Dictionary(Of String, String)

    ''' <summary>
    ''' Callback for getting user inputs.
    ''' </summary>
    Private userInputMethodField As GetUserInputsMethod = New GetUserInputsMethod( _
        AddressOf ExampleUtilities.GetUserInputs)

    ''' <summary>
    ''' Gets or sets the callback for getting user inputs.
    ''' </summary>
    Public Property UserInputMethod() As GetUserInputsMethod
      Get
        Return Me.userInputMethodField
      End Get
      Set(ByVal value As GetUserInputsMethod)
        Me.userInputMethodField = value
      End Set
    End Property

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public MustOverride ReadOnly Property Description() As String

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>A list of parameter names for this code example.</returns>
    Public MustOverride Function GetParameterNames() As String()

    ''' <summary>
    ''' Gets the parameters required to run this code example.
    ''' </summary>
    ''' <returns>A dictionary, with key as parameter name and value as parameter
    ''' value.</returns>
    Public Overridable Function GetParameters() As Dictionary(Of String, String)
      Return Me.UserInputMethod.Invoke(GetParameterNames)
    End Function

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="parameters">The parameters for running the code
    ''' example.</param>
    ''' <param name="writer">The stream writer to which script output should be
    ''' written.</param>
    Public MustOverride Sub Run(ByVal user As AdWordsUser, ByVal parameters As  _
        Dictionary(Of String, String), ByVal writer As TextWriter)
  End Class
End Namespace