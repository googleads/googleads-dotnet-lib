' Copyright 2014, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.Common.Lib

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201406
  ''' <summary>
  ''' This code example illustrates how to handle 2 factor authorization errors.
  '''
  ''' Tags: CampaignService.get
  ''' </summary>
  Public Class HandleTwoFactorAuthorizationError
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New HandleTwoFactorAuthorizationError
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
    '''
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to handle 2 factor authorization errors."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Use a test account for which 2 factor authentication has been enabled.
      Dim loginEmail As String = "2steptester@gmail.com"
      Dim password As String = "testaccount"

      Dim config As New AdWordsAppConfig
      config.Email = loginEmail
      config.Password = password
      Dim authToken As New AuthToken(config, "adwords")

      Try
        ' Try to obtain an authToken.
        Dim token As String = authToken.GetToken
        Console.WriteLine("Retrieved an authToken = {0} for user {1}.", token, loginEmail)
      Catch ex As AuthTokenException
        ' Since the test account has 2 factor authentication enabled, this block
        ' of code will be executed.
        If (ex.ErrorCode = AuthTokenErrorCode.BadAuthentication) Then
          If (ex.Info = "InvalidSecondFactor") Then
            Console.WriteLine("The user has enabled two factor authentication in this " & _
                "account. Have the user generate an application-specific password to make " & _
                "calls against the AdWords API. See " & _
                "http://adwordsapi.blogspot.com/2011/02/authentication-changes-with-2-step.html" & _
                "for more details.")
          Else
            Console.WriteLine("Invalid credentials.")
          End If
        Else
          Throw New System.ApplicationException(String.Format("The server raised an {0} error.", _
              ex.ErrorCode), ex)
        End If
      End Try
    End Sub
  End Class
End Namespace
