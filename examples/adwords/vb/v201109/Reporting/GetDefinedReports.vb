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
  ''' This code example gets all report definitions.
  '''
  ''' Tags: ReportDefinitionService.get
  ''' </summary>
  Public Class GetDefinedReports
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetDefinedReports
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, codeExample.GetParameters, Console.Out)
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
        Return "This code example gets all report definitions."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {}
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
      ' Get the ReportDefinitionService.
      Dim reportDefinitionService As ReportDefinitionService = user.GetService( _
          AdWordsService.v201109.ReportDefinitionService)

      ' Create the selector.
      Dim selector As New ReportDefinitionSelector

      Try
        ' Get all report definitions.
        Dim page As ReportDefinitionPage = reportDefinitionService.get(selector)

        ' Display report definitions.
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing) _
            AndAlso (page.entries.Length > 0)) Then
          For Each reportDefinition As ReportDefinition In page.entries
            writer.WriteLine("ReportDefinition with name ""{0}"" and id ""{1}"" was found.", _
                reportDefinition.reportName, reportDefinition.id)
          Next
        Else
          writer.WriteLine("No report definitions were found.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve report definitions.", ex)
      End Try
    End Sub
  End Class
End Namespace
