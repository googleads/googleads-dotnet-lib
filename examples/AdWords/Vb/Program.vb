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

Imports Google.Api.Ads.AdWords.Lib

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Reflection

Namespace Google.Api.Ads.AdWords.Examples.VB
    ''' <summary>
    ''' The Main class for this application.
    ''' </summary>
    Class Program
        ''' <summary>
        ''' A flag to keep track of whether help message was shown earlier.
        ''' </summary>
        Private Shared helpShown As Boolean = False

        ''' <summary>
        ''' A map to hold the code examples to be executed.
        ''' </summary>
        Private Shared codeExampleMap As List(Of KeyValuePair(Of String, ExampleBase)) =
            New List(Of KeyValuePair(Of String, ExampleBase))

        ''' <summary>
        ''' Registers the code example.
        ''' </summary>
        ''' <param name="key">The code example name.</param>
        ''' <param name="value">The code example instance.</param>
        Private Shared Sub RegisterCodeExample(ByVal key As String, ByVal value As ExampleBase)
            codeExampleMap.Add(New KeyValuePair(Of String, ExampleBase)(key, value))
        End Sub

        ''' <summary>
        ''' Static constructor to initialize the code example map.
        ''' </summary>
        Shared Sub New()
            Dim types As Type() = Assembly.GetExecutingAssembly.GetTypes
            For Each type As Type In types
                If (type.BaseType Is GetType(ExampleBase)) Then
                    RegisterCodeExample(
                        type.FullName.Replace((GetType(Program).Namespace & "."), ""),
                        TryCast(Activator.CreateInstance(type), ExampleBase))
                End If
            Next
        End Sub

        ''' <summary>
        ''' The main method.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            If (args.Length = 0) Then
                ShowUsage()
            Else
                Dim user As New AdWordsUser

                For Each cmdArgs As String In args
                    Dim found As Boolean = False
                    For Each pair As KeyValuePair(Of String, ExampleBase) In codeExampleMap
                        If String.Compare(pair.Key, cmdArgs, True) = 0 Then
                            found = True
                            RunACodeExample(user, pair.Value)
                            Exit For
                        End If
                    Next
                    If (Not found) Then
                        ShowUsage()
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Invokes the Run method of code example.
        ''' </summary>
        ''' <param name="codeExample">The code example.</param>
        ''' <param name="user">The AdWords user.</param>
        Private Shared Sub InvokeRun(ByVal codeExample As Object, ByVal user As AdWordsUser)
            codeExample.GetType.GetMethod("Run").Invoke(codeExample,
                                                        GetParameters(user, codeExample))
        End Sub

        ''' <summary>
        ''' Gets the description of the code example.
        ''' </summary>
        ''' <param name="codeExample">The code example.</param>
        ''' <returns>The description</returns>
        Private Shared Function GetDescription(ByVal codeExample As Object) As Object
            Return codeExample.GetType.GetProperty("Description").GetValue(codeExample, Nothing)
        End Function

        ''' <summary>
        ''' Gets the parameters for running a code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="codeExample">The code example.</param>
        ''' <returns>The list of parameters.</returns>
        Private Shared Function GetParameters(ByVal user As AdWordsUser, ByVal codeExample As Object) _
            As Object()
            Dim methodInfo As MethodInfo = codeExample.GetType.GetMethod("Run")
            Dim parameters As New List(Of Object)
            parameters.Add(user)
            parameters.AddRange(ExampleUtilities.GetParameters(methodInfo))
            Return parameters.ToArray
        End Function

        ''' <summary>
        ''' Runs a code example.
        ''' </summary>
        ''' <param name="user">The user whose credentials should be used for
        ''' running the code example.</param>
        ''' <param name="codeExample">The code example to run.</param>
        Private Shared Sub RunACodeExample(ByVal user As AdWordsUser, ByVal codeExample As Object)
            Try
                Console.WriteLine(GetDescription(codeExample))
                Program.InvokeRun(codeExample, user)
            Catch e As Exception
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
            Finally
                Console.WriteLine("Press [Enter] to continue")
                Console.ReadLine()
            End Try
        End Sub

        ''' <summary>
        ''' Prints program usage message.
        ''' </summary>
        Private Shared Sub ShowUsage()
            If Not helpShown Then
                helpShown = True
                Dim exeName As String = Path.GetFileName(Assembly.GetExecutingAssembly.Location)

                Console.WriteLine("Runs AdWords API code examples")
                Console.WriteLine("Usage : {0} [flags]\n", exeName)
                Console.WriteLine("Available flags\n")
                Console.WriteLine("--help\t\t : Prints this help message.", exeName)
                Console.WriteLine("--all\t\t : Run all code examples.", exeName)
                Console.WriteLine("--v13all: Runs all v13 code examples.")
                Console.WriteLine("--v201008all: Runs all v201008 code examples.")

                Console.WriteLine(
                    "\nexamplename1 [examplename1 ...] : Run specific code examples. " &
                    "Example name can be one of the following:", exeName)

                For Each pair As KeyValuePair(Of String, ExampleBase) In codeExampleMap
                    Console.WriteLine("{0} : {1}", pair.Key, pair.Value.Description)
                Next

                Console.WriteLine("Press [Enter] to continue")
                Console.ReadLine()
            End If
        End Sub
    End Class
End Namespace
