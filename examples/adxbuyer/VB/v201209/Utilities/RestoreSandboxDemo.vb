' Copyright 2012, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.Util.Data

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201209
  ''' <summary>
  ''' This code example shows how to restore an entire sandbox account.
  ''' You should run BackupSandboxDemo.vb first to obtain the input file
  ''' for this code example.
  ''' </summary>
  Public Class RestoreSandboxDemo
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New RestoreSandboxDemo
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
      Catch ex As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(ex))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example shows how to restore an entire sandbox account. You should " & _
            "run BackupSandboxDemo.vb first to obtain the input file for this code example."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      DataUtilities.RestoreSandboxContents(user, String.Format("{0}\SandboxBackup.xml", _
          ExampleUtilities.GetHomeDir))
    End Sub
  End Class
End Namespace
