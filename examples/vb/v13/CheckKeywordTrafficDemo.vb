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
Imports Google.Api.Ads.AdWords.v13

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v13
  ''' <summary>
  ''' This code example checks a batch of keywords to see whether they will
  ''' get any traffic.
  ''' </summary>
  Class CheckKeywordTrafficDemo
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example checks a batch of keywords to see whether they will get " & _
            "any traffic."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New CheckKeywordTrafficDemo
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      Dim service As TrafficEstimatorService = user.GetService( _
          AdWordsService.v13.TrafficEstimatorService)

      Dim trafficRequest1 As New KeywordTrafficRequest
      trafficRequest1.keywordText = "AdWords API"
      trafficRequest1.keywordType = KeywordType.Phrase
      trafficRequest1.language = "en_US"

      Dim trafficRequest2 As New KeywordTrafficRequest
      trafficRequest2.keywordText = "Google Desktop"
      trafficRequest2.keywordType = KeywordType.Broad
      trafficRequest2.language = "fr"

      Dim trafficRequests As KeywordTrafficRequest() = New KeywordTrafficRequest() _
          {trafficRequest1, trafficRequest2}

      Dim estimates As KeywordTraffic() = service.checkKeywordTraffic(trafficRequests)
      If (Not estimates Is Nothing) Then
        Console.WriteLine("{0, -20}{1, -10}", "Keyword", "Traffic")
        Console.WriteLine("---------------------------------")

        For i As Integer = 0 To estimates.Length - 1
          Console.WriteLine("{0, -20}{1, -10}", trafficRequests(i).keywordText, estimates(i))
        Next i
      Else
        Console.WriteLine("Given keyword(s) not expected to get any traffic.")
      End If
    End Sub
  End Class
End Namespace
