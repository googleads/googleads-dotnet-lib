' Copyright 2016, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example adds two rule-based remarketing user lists: one with no
  ''' site visit date restrictions, and another that will only include users
  ''' who visit your site in the next six months.  See
  ''' https://developers.google.com/adwords/api/docs/guides/rule-based-remarketing
  ''' to learn more about rule based remarketing.
  ''' </summary>
  Public Class AddRuleBasedRemarketingList
    Inherits ExampleBase
    Private Const DATE_FORMAT_STRING As String = "yyyyMMdd"

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddRuleBasedRemarketingList
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
      Catch e As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(e))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds two rule-based remarketing user lists: one with no " & _
            "site visit date restrictions, and another that will only include users who " & _
            "visit your site in the next six months. See " & _
            "https://developers.google.com/adwords/api/docs/guides/rule-based-remarketing to " & _
            "learn more about rule based remarketing."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the AdwordsUserListService.
      Dim userListService As AdwordsUserListService = CType(user.GetService( _
          AdWordsService.v201603.AdwordsUserListService), AdwordsUserListService)

      ' First rule item group - users who visited the checkout page and had
      ' more than one item in their shopping cart.
      Dim checkoutStringRuleItem As New StringRuleItem()
      checkoutStringRuleItem.key = New StringKey()
      checkoutStringRuleItem.key.name = "ecomm_pagetype"
      checkoutStringRuleItem.op = StringRuleItemStringOperator.EQUALS
      checkoutStringRuleItem.value = "checkout"

      Dim checkoutRuleItem As New RuleItem()
      checkoutRuleItem.Item = checkoutStringRuleItem

      Dim cartSizeNumberRuleItem As New NumberRuleItem()
      cartSizeNumberRuleItem.key = New NumberKey()
      cartSizeNumberRuleItem.key.name = "cartsize"
      cartSizeNumberRuleItem.op = NumberRuleItemNumberOperator.GREATER_THAN
      cartSizeNumberRuleItem.value = 1

      Dim cartSizeRuleItem As New RuleItem()
      cartSizeRuleItem.Item = cartSizeNumberRuleItem

      ' Combine the two rule items into a RuleItemGroup so AdWords will AND
      ' their(rules) together.
      Dim checkoutMultipleItemGroup As New RuleItemGroup()
      checkoutMultipleItemGroup.items = New RuleItem() {checkoutRuleItem, cartSizeRuleItem}

      ' Second rule item group - users who check out within the next 3 months.
      Dim startDateDateRuleItem As New DateRuleItem()
      startDateDateRuleItem.key = New DateKey()
      startDateDateRuleItem.key.name = "checkoutdate"
      startDateDateRuleItem.op = DateRuleItemDateOperator.AFTER
      startDateDateRuleItem.value = DateTime.Now.ToString(DATE_FORMAT_STRING)
      Dim startDateRuleItem As New RuleItem()
      startDateRuleItem.Item = startDateDateRuleItem

      Dim endDateDateRuleItem As New DateRuleItem()
      endDateDateRuleItem.key = New DateKey()
      endDateDateRuleItem.key.name = "checkoutdate"
      endDateDateRuleItem.op = DateRuleItemDateOperator.BEFORE
      endDateDateRuleItem.value = DateTime.Now.AddMonths(3).ToString(DATE_FORMAT_STRING)
      Dim endDateRuleItem As New RuleItem()
      endDateRuleItem.Item = endDateDateRuleItem

      ' Combine the date rule items into a RuleItemGroup.
      Dim checkedOutNextThreeMonthsItemGroup As New RuleItemGroup()
      checkedOutNextThreeMonthsItemGroup.items = _
          New RuleItem() {startDateRuleItem, endDateRuleItem}

      ' Combine the rule item groups into a Rule so AdWords will OR the groups
      ' together.
      Dim rule As New Rule()
      rule.groups = New RuleItemGroup() {checkoutMultipleItemGroup, _
        checkedOutNextThreeMonthsItemGroup}

      ' Create the user list with no restrictions on site visit date.
      Dim expressionUserList As New ExpressionRuleUserList()
      expressionUserList.name = "Expression based user list created at " + DateTime.Now.ToString( _
          "yyyyMMdd_HHmmss")
      expressionUserList.description = "Users who checked out in three month window OR " & _
          "visited the checkout page with more than one item in their cart."
      expressionUserList.rule = rule

      ' Create the user list restricted to users who visit your site within
      ' the next six months.
      Dim startDate As DateTime = DateTime.Now
      Dim endDate As DateTime = startDate.AddMonths(6)

      Dim dateUserList As New DateSpecificRuleUserList()
      dateUserList.name = "Date rule user list created at " & _
          DateTime.Now.ToString("yyyyMMdd_HHmmss")
      dateUserList.description = String.Format("Users who visited the site between {0} and " & _
          "{1} and checked out in three month window OR visited the checkout page " & _
          "with more than one item in their cart.", startDate.ToString(DATE_FORMAT_STRING), _
          endDate.ToString(DATE_FORMAT_STRING))
      dateUserList.rule = rule

      ' Set the start and end dates of the user list.
      dateUserList.startDate = startDate.ToString(DATE_FORMAT_STRING)
      dateUserList.endDate = endDate.ToString(DATE_FORMAT_STRING)

      ' Create operations to add the user lists.
      Dim operations As New List(Of UserListOperation)
      For Each userList As UserList In New UserList() {expressionUserList, dateUserList}
        Dim operation As New UserListOperation()
        operation.operand = userList
        operation.operator = [Operator].ADD
        operations.Add(Operation)
      Next

      Try
        ' Submit the operations.
        Dim result As UserListReturnValue = userListService.mutate(operations.ToArray())

        ' Display the results.
        For Each userListResult As UserList In result.value
          Console.WriteLine("User list added with ID {0}, name '{1}', status '{2}', " +
              "list type '{3}', accountUserListStatus '{4}', description '{5}'.",
              userListResult.id, _
              userListResult.name, _
              userListResult.status, _
              userListResult.listType, _
              userListResult.accountUserListStatus,
              userListResult.description)
        Next
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add rule based user lists.", e)
      End Try
    End Sub
  End Class
End Namespace
