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
  ''' This code example estimates traffic for a given keyword.
  ''' </summary>
  Class KeywordEstimateDemo
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example estimates traffic for a given keyword."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New KeywordEstimateDemo
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the service.
      Dim service As TrafficEstimatorService = user.GetService( _
          AdWordsService.v13.TrafficEstimatorService)

      ' Set the attributes of the keywords to be estimated.
      Dim myKeyword As New KeywordRequest
      myKeyword.text = "flowers"
      myKeyword.maxCpc = 50000
      myKeyword.type = KeywordType.Broad

      ' To estimate more keywords, create more KeywordRequest objects
      ' and add them to the list of keyword to estimate.
      Dim keyReqs As KeywordRequest() = New KeywordRequest() {myKeyword}

      ' Estimate traffic for given keywords.
      Dim estimates As KeywordEstimate() = service.estimateKeywordList(keyReqs)

      If (Not estimates Is Nothing) Then
        For i As Integer = 0 To estimates.Length - 1
          Dim estimate As KeywordEstimate = estimates(i)
          Console.WriteLine(("Keyword estimates for: " & keyReqs(i).text & _
              "\nClicks per day between {0} and {1}" & _
              "\nCost per click between {0} and {1}" & _
              "\nAverage position between {0} and {1}"), _
              estimate.lowerClicksPerDay, estimate.upperClicksPerDay, estimate.lowerCpc, _
              estimate.upperCpc, estimate.lowerAvgPosition, estimate.upperAvgPosition)
        Next i
      Else
        Console.WriteLine("No traffic estimates are available for given keyword(s)")
      End If
    End Sub
  End Class
End Namespace
