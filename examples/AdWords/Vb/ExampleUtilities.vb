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

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Reflection

Namespace Google.Api.Ads.AdWords.Examples.VB
    ''' <summary>
    ''' Utility class for assisting in running code examples.
    ''' </summary>
    Public Class ExampleUtilities
        ''' <summary>
        ''' Gets the current user's home directory.
        ''' </summary>
        ''' <returns>The current user's home directory.</returns>
        Public Shared Function GetHomeDir() As String
            Return Environment.GetEnvironmentVariable("USERPROFILE")
        End Function

        ''' <summary>
        ''' Gets a random string. Used for generating unique strings for use with
        ''' AdGroups, Campaigns, etc.
        ''' </summary>
        ''' <returns>The random string.</returns>
        Public Shared Function GetRandomString() As String
            Return String.Format("{0} - {1}", Guid.NewGuid,
                                 DateTime.Now.ToString("yyyy-M-d H-m-s.ffffff"))
        End Function

        ''' <summary>
        ''' Gets a short random string. Useful for generating unique names for
        ''' campaigns, ad groups, etc.
        ''' </summary>
        ''' <returns>The random string.</returns>
        Public Shared Function GetShortRandomString() As String
            Return Guid.NewGuid().ToString().Substring(0, 8)
        End Function

        ''' <summary>
        ''' Gets the user inputs for running a code example in command line mode.
        ''' </summary>
        ''' <param name="paramNames">The parameter names.</param>
        ''' <returns>A dictionary, with key as parameter name and value as
        ''' parameter value.</returns>
        Public Shared Function GetUserInputs(ByVal paramNames As String()) As _
            Dictionary(Of String, String)
            Dim parameters As New Dictionary(Of String, String)
            Dim paramName As String
            For Each paramName In paramNames
                Console.Write("Enter {0}: ", paramName)
                parameters.Item(paramName) = Console.ReadLine
            Next
            Return parameters
        End Function

        ''' <summary>
        ''' Formats the exception as a printable message.
        ''' </summary>
        ''' <param name="ex">The exception.</param>
        ''' <returns>The formatted exception string.</returns>
        Public Shared Function FormatException(ByVal ex As Exception) As String
            Dim messages As New List(Of String)
            Dim rootEx As Exception = ex
            Do While (Not rootEx Is Nothing)
                messages.Add(String.Format("{0} ({1})\n\n{2}\n", rootEx.GetType.ToString,
                                           rootEx.Message, rootEx.StackTrace))
                rootEx = rootEx.InnerException
            Loop
            Return String.Join("\nCaused by\n\n", messages.ToArray)
        End Function

        ''' <summary>
        ''' Determines whether the specified type is Nullable.
        ''' </summary>
        ''' <param name="type">The type.</param>
        ''' <returns>True, if the type is nullable, false otherwise.</returns>
        Public Shared Function IsNullable(ByVal type As Type) As Boolean
            If type.IsGenericType AndAlso
               type.GetGenericTypeDefinition() = GetType(Nullable(Of )) Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Gets the underlying type of a given type.
        ''' </summary>
        ''' <param name="type">The type.</param>
        ''' <returns>The type, if the given type is not nullable, the underlying
        ''' type if it is nullable.</returns>
        Public Shared Function GetUnderlyingType(ByVal type As Type) As Type
            If IsNullable(type) Then
                Return type.GetGenericArguments()(0)
            Else
                Return type
            End If
        End Function

        ''' <summary>
        ''' Gets the parameters for running the code example.
        ''' </summary>
        ''' <param name="methodInfo">The method info for Run method in code
        ''' example.</param>
        ''' <returns>An array of parameters for running the code example.</returns>
        Public Shared Function GetParameters(ByVal methodInfo As MethodInfo) As List(Of Object)
            Dim retval As New List(Of Object)()
            Dim paramInfos As ParameterInfo() = methodInfo.GetParameters()

            ' The first argument is AdWordsUser, skip it.
            For i As Integer = 1 To paramInfos.Length - 1
                Dim paramInfo As ParameterInfo = paramInfos(i)

                Dim underlyingType As Type = GetUnderlyingType(paramInfo.ParameterType)
                Dim isNullable As Boolean = ExampleUtilities.IsNullable(underlyingType)
                If isNullable Then
                    Console.Write("[Optional] ")
                End If

                If underlyingType.IsArray Then
                    Console.Write("[Comma Separated] ")
                End If

                Console.Write("Enter {0}: ", paramInfo.Name)
                Dim value As String = Console.ReadLine()
                Dim objValue As Object = Nothing

                If underlyingType.IsArray Then
                    Dim items As String() = value.Split(","c)
                    For j As Integer = 0 To items.Length
                        items(j) = items(j).Trim()
                    Next
                    If underlyingType.GetElementType() <> GetType(String) Then
                        Throw New Exception("Only string[] array parameters are supported.")
                    Else
                        objValue = items
                    End If
                Else
                    Dim typeConverter As TypeConverter = TypeDescriptor.GetConverter(underlyingType)
                    Try
                        objValue = typeConverter.ConvertFromString(value)
                    Catch
                        If Not isNullable Then
                            Throw
                        End If
                    End Try
                End If

                retval.Add(objValue)
            Next

            Return retval
        End Function
    End Class
End Namespace
