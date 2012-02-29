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
Imports Google.Api.Ads.AdWords.Util.Units

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example displays API units usage attributed to each client and
  ''' sub-MCC this month. It should be noted that this data is not in
  ''' real time and is refreshed every few hours.
  ''' </summary>
  Class ClientApiUnitsUsageDemo
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New ClientApiUnitsUsageDemo
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example displays API units usage attributed to each client and " & _
            "sub-MCC this month. It should be noted that this data is not in real time and is " & _
            "refreshed every few hours."
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
      user.ResetUnits()
      Dim clientUsage As SortedList(Of String, Long) = Nothing
      Dim startDate As New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
      Dim endDate As DateTime = DateTime.Today
      Dim clientQuotaUsage As ClientQuotaUsage = UnitsUtilities.GetClientQuotaUsage(user, _
          startDate, endDate)

      writer.WriteLine("Total units consumed between {0} and {1}: {2}", _
          startDate.ToString("d MMM yyyy"), endDate.ToString("d MMM yyyy"), _
          clientQuotaUsage.TotalUnits)
      writer.WriteLine("Breakup of unit consumption by account (rolled up to MCCs)")
      writer.WriteLine("==========================================================")

      For Each email As String In clientUsage.Keys
        writer.WriteLine("{0,-40} : {1,10}", email, clientUsage.Item(email))
      Next
      writer.WriteLine("Difference between units consumed and rolledup values : {0}", _
          clientQuotaUsage.DiffUnits)
      writer.WriteLine("Total units consumed for this run : {0}", user.GetUnits)
    End Sub
  End Class
End Namespace
