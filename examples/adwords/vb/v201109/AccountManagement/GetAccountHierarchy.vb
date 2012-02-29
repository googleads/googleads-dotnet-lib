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
  ''' This code example illustrates how to retrieve the account hierarchy under
  ''' an account.
  '''
  ''' Tags: ServicedAccountService.get
  ''' </summary>
  Class GetAccountHierarchy
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetAccountHierarchy
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to retrieve the account hierarchy under " & _
            "an account."
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
      ' Get the ServicedAccountService.
      Dim servicedAccountService As ServicedAccountService = user.GetService( _
          AdWordsService.v201109.ServicedAccountService)

      ' Create the selector.
      Dim selector As New ServicedAccountSelector

      ' Disable selector paging to retrive links.
      selector.enablePaging = False

      Try
        ' Retrieve the accounts.
        Dim graph As ServicedAccountGraph = servicedAccountService.get(selector)

        If ((Not graph Is Nothing) AndAlso (Not graph.accounts Is Nothing)) Then
          ' Display the accounts.
          writer.WriteLine("There are {0} customers under this account hierarchy.", _
              graph.accounts.Length)

          For i As Integer = 0 To graph.accounts.Length - 1
            writer.WriteLine("{0}) Customer id: {1:###-###-####}\nLogin email: {2}\n" & _
                "Company name: {3}\nIsMCC: {4}", (i + 1), graph.accounts(i).customerId, _
                graph.accounts(i).login, graph.accounts(i).companyName, _
                graph.accounts(i).canManageClients)
          Next i

          ' Display the links.
          For Each link As Link In graph.links
            writer.WriteLine("There is a {0} link from {1:###-###-####} to " & _
                "{2:###-###-####}", link.typeOfLink, link.managerId.id, link.clientId.id)
          Next
        Else
          writer.WriteLine("No accounts were retrieved.")
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to retrieve accounts. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
