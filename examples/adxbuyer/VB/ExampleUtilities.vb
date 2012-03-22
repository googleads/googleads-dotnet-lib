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

Imports System
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB
  ''' <summary>
  ''' Utility class for assisting in running code examples.
  ''' </summary>
  Public Class ExampleUtilities
    ''' <summary>
    ''' Gets the current user's home directory.
    ''' </summary>
    ''' <returns>The current user's home directory.</returns>
    Public Shared Function GetHomeDir() As String
      Return Environment.GetEnvironmentVariable("USERPROFILE")
    End Function

    ''' <summary>
    ''' Gets the current timestamp as a string.
    ''' </summary>
    ''' <returns>The current timestamp as a string.</returns>
    Public Shared Function GetTimeStamp() As String
      Return DateTime.Now.ToString("yyyy-M-d H-m-s.ffffff")
    End Function

    ''' <summary>
    ''' Gets the user inputs for running a code example in command line mode.
    ''' </summary>
    ''' <param name="paramNames">The parameter names.</param>
    ''' <returns>A dictionary, with key as parameter name and value as
    ''' parameter value.</returns>
    Public Shared Function GetUserInputs(ByVal paramNames As String()) As  _
        Dictionary(Of String, String)
      Dim parameters As New Dictionary(Of String, String)
      Dim paramName As String
      For Each paramName In paramNames
        Console.Write("Enter {0}: ", paramName)
        parameters.Item(paramName) = Console.ReadLine
      Next
      Return parameters
    End Function

    ''' <summary>
    ''' Formats the exception as a printable message.
    ''' </summary>
    ''' <param name="ex">The exception.</param>
    ''' <returns>The formatted exception string.</returns>
    Public Shared Function FormatException(ByVal ex As Exception) As String
      Dim messages As New List(Of String)
      Dim rootEx As Exception = ex
      Do While (Not rootEx Is Nothing)
        messages.Add(String.Format("{0} ({1})\n\n{2}\n", rootEx.GetType.ToString, _
            rootEx.Message, rootEx.StackTrace))
        rootEx = rootEx.InnerException
      Loop
      Return String.Join("\nCaused by\n\n", messages.ToArray)
    End Function
  End Class
End Namespace
