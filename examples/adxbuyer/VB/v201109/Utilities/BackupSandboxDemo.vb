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
Imports Google.Api.Ads.AdWords.Util.Data

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example shows how to backup a sandbox account.
  ''' </summary>
  Public Class BackupSandboxDemo
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New BackupSandboxDemo
      Console.WriteLine(codeExample.Description)
      Try
        Dim fileName As String = "INSERT_OUTPUT_FILENAME"
        codeExample.Run(New AdWordsUser, fileName)
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
        Return "This code example shows how to backup a sandbox account."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="fileName">The file to which sandbox contents are backed
    ''' up.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal fileName As String)
      ' The following set of fields are not exhaustive, they are only for
      ' illustration. If you need to backup more object fields, you need to
      ' lookup the corresponding selector names and add them below.
      DataUtilities.DownloadSandboxContents(user, fileName, _
          New String() {"Id", "Name", "Status"}, _
          New String() {"Id", "Name", "Status"}, _
          New String() {"Id", "Status", "Headline", "Description1", "Description2", "DisplayUrl"}, _
          New String() {"Id", "KeywordText"}, _
          New String() {"Id", "CriteriaType"})
    End Sub
  End Class
End Namespace
