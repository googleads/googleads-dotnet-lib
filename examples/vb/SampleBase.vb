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

#Const INTERACTIVE = 1

Imports Google.Api.Ads.AdWords.Lib

Imports System
Imports System.Threading

Namespace Google.Api.Ads.AdWords.Examples.VB
  ''' <summary>
  ''' This abstract class represents a code example.
  ''' </summary>
  MustInherit Class SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public MustOverride ReadOnly Property Description() As String

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public MustOverride Sub Run(ByVal user As AdWordsUser)

    ''' <summary>
    ''' Displays a prompt and reads an user input.
    ''' </summary>
    ''' <param name="prompt">The prompt.</param>
    ''' <returns>The user input.</returns>
    Protected Function _T(ByVal prompt As String) As String
#If INTERACTIVE Then
      Console.Write(prompt & " : ")
      Return Console.ReadLine
#Else
      Return prompt
#End If
    End Function

    ''' <summary>
    ''' Gets the current timestamp as a string.
    ''' </summary>
    ''' <returns>The current timestamp as a string.</returns>
    Protected Function GetTimeStamp() As String
      Thread.Sleep(100)
      Return DirectCast((DateTime.UtcNow - New DateTime(1970, 1, 1)), TimeSpan). _
          TotalMilliseconds.ToString
    End Function

    ''' <summary>
    ''' Gets the current user's home directory.
    ''' </summary>
    ''' <returns>The current user's home directory.</returns>
    Protected Function GetHomeDir() As String
      Return Environment.GetEnvironmentVariable("USERPROFILE")
    End Function
  End Class
End Namespace
