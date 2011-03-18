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
Imports Google.Api.Ads.AdWords.Util
Imports Google.Api.Ads.AdWords.Util.Units

Imports System
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example displays API method usage for this month for all methods
  ''' provided by the AdWords API. Note that this data is not in real time and
  ''' is refreshed every few hours.
  ''' </summary>
  Class MethodApiUnitsUsageDemo
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example displays API method usage for this month for all methods " & _
            "provided by the AdWords API. Note that this data is not in real time and is " & _
            "refreshed every few hours."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New MethodApiUnitsUsageDemo
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      user.ResetUnits()
      Dim methodQuotaUsage As List(Of MethodQuotaUsage) = UnitsUtilities.GetMethodQuotaUsage( _
          user, DateTime.Now.AddMonths(-1), DateTime.Now)

      For Each usage As MethodQuotaUsage In methodQuotaUsage
        Console.WriteLine("{0,-50} - {1}", (usage.ServiceName & "." & usage.MethodName), _
            usage.Units)
      Next
      Console.WriteLine("\nTotal Quota unit cost for this run: {0}.\n", user.GetUnits)
    End Sub
  End Class
End Namespace
