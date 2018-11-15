' Copyright 2018 Google LLC
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
Imports Google.Api.Ads.AdWords.v201809

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
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
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
            End Try
        End Sub

        ''' <summary>
        ''' Returns a description about the code example.
        ''' </summary>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return _
                    "This code example adds two rule-based remarketing user lists: one with no " &
                    "site visit date restrictions, and another that will only include users who " &
                    "visit your site in the next six months. See " &
                    "https://developers.google.com/adwords/api/docs/guides/rule-based-remarketing" &
                    " to learn more about rule based remarketing."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            Using userListService As AdwordsUserListService = CType(
                user.GetService(
                    AdWordsService.v201809.AdwordsUserListService),
                AdwordsUserListService)

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
                checkoutMultipleItemGroup.items = New RuleItem() _
                    {checkoutRuleItem, cartSizeRuleItem}

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
                checkedOutNextThreeMonthsItemGroup.items =
                    New RuleItem() {startDateRuleItem, endDateRuleItem}

                ' Combine the rule item groups into a Rule so AdWords knows how to apply the rules.
                Dim rule As New Rule()
                rule.groups = New RuleItemGroup() { _
                                                      checkoutMultipleItemGroup,
                                                      checkedOutNextThreeMonthsItemGroup
                                                  }

                ' ExpressionRuleUserLists can use either CNF Or DNF For matching. CNF means
                ' 'at least one item in each rule item group must match', and DNF means 'at
                ' least one entire rule item group must match'.
                ' DateSpecificRuleUserList only supports DNF. You can also omit the rule
                ' type altogether To Default To DNF.
                rule.ruleType = UserListRuleTypeEnumsEnum.DNF


                ' Third And fourth rule item groups.
                ' [START createRules] MOE:strip_line
                ' Visitors of a page who visited another page. See
                ' https//developers.google.com/adwords/api/docs/reference/latest/AdwordsUserListService.StringKey
                ' for more details.
                Dim urlStringKey As New StringKey()
                urlStringKey.name = "url__"

                Dim site1StringRuleItem As New StringRuleItem()
                site1StringRuleItem.key = urlStringKey
                site1StringRuleItem.op = StringRuleItemStringOperator.EQUALS
                site1StringRuleItem.value = "example.com/example1"
                Dim site1RuleItem As New RuleItem()
                site1RuleItem.Item = site1StringRuleItem

                Dim site2StringRuleItem As New StringRuleItem()
                site2StringRuleItem.key = urlStringKey
                site2StringRuleItem.op = StringRuleItemStringOperator.EQUALS
                site2StringRuleItem.value = "example.com/example2"
                Dim site2RuleItem As New RuleItem()
                site2RuleItem.Item = site2StringRuleItem

                ' Create two RuleItemGroups to show that a visitor browsed two sites.
                Dim site1RuleItemGroup As New RuleItemGroup()
                site1RuleItemGroup.items = New RuleItem() {site1RuleItem}
                Dim site2RuleItemGroup As New RuleItemGroup()
                site2RuleItemGroup.items = New RuleItem() {site2RuleItem}

                ' Create two rules to show that a visitor browsed two sites.
                Dim userVisitedSite1Rule As New Rule()
                userVisitedSite1Rule.groups = New RuleItemGroup() {site1RuleItemGroup}

                Dim userVisitedSite2Rule As New Rule()
                userVisitedSite2Rule.groups = New RuleItemGroup() {site2RuleItemGroup}
                ' [END createRules] MOE:strip_line

                ' Create the user list with no restrictions on site visit date.
                Dim expressionUserList As New ExpressionRuleUserList()
                Dim creationTimeString As String = DateTime.Now.ToString("yyyyMMdd_HHmmss")

                expressionUserList.name = "Expression based user list created at " +
                                          creationTimeString
                expressionUserList.description = "Users who checked out in three month window OR " &
                                                 "visited the checkout page with more than one " &
                                                 "item in their cart."
                expressionUserList.rule = rule

                ' Optional: Set the prepopulationStatus to REQUESTED to include past users
                ' in the user list.
                expressionUserList.prepopulationStatus =
                    RuleBasedUserListPrepopulationStatus.REQUESTED

                ' Create the user list restricted to users who visit your site within
                ' the next six months.
                Dim startDate As DateTime = DateTime.Now
                Dim endDate As DateTime = startDate.AddMonths(6)

                Dim dateUserList As New DateSpecificRuleUserList()
                dateUserList.name = "Date rule user list created at " + creationTimeString
                dateUserList.description =
                    String.Format("Users who visited the site between {0} and " &
                                  "{1} and checked out in three month window OR visited the " &
                                  "checkout page with more than one item in their cart.",
                                  startDate.ToString(DATE_FORMAT_STRING),
                                  endDate.ToString(DATE_FORMAT_STRING))
                dateUserList.rule = rule

                ' Set the start and end dates of the user list.
                dateUserList.startDate = startDate.ToString(DATE_FORMAT_STRING)
                dateUserList.endDate = endDate.ToString(DATE_FORMAT_STRING)

                ' [START createCombinedRuleUserList] MOE:strip_line
                ' Create the user list where "Visitors of a page who did visit another page".
                ' To create a user list where "Visitors of a page who did not visit another
                ' page", change the ruleOperator from And to AND_NOT.
                Dim CombinedRuleUserList As New CombinedRuleUserList()
                CombinedRuleUserList.name = "Combined rule user list created at " +
                                            creationTimeString
                CombinedRuleUserList.description = "Users who visited two sites."
                CombinedRuleUserList.leftOperand = userVisitedSite1Rule
                CombinedRuleUserList.rightOperand = userVisitedSite2Rule
                CombinedRuleUserList.ruleOperator = CombinedRuleUserListRuleOperator.AND
                ' [END createCombinedRuleUserList] MOE:strip_line

                ' Create operations to add the user lists.
                Dim operations As New List(Of UserListOperation)
                For Each userList As UserList In New UserList() {expressionUserList, dateUserList,
                                                                 CombinedRuleUserList}
                    Dim operation As New UserListOperation()
                    operation.operand = userList
                    operation.operator = [Operator].ADD
                    operations.Add(operation)
                Next

                Try
                    ' Submit the operations.
                    Dim result As UserListReturnValue = userListService.mutate(operations.ToArray())

                    ' Display the results.
                    For Each userListResult As UserList In result.value
                        Console.WriteLine(
                            "User list added with ID {0}, name '{1}', status '{2}', " +
                            "list type '{3}', accountUserListStatus '{4}', description '{5}'.",
                            userListResult.id,
                            userListResult.name,
                            userListResult.status,
                            userListResult.listType,
                            userListResult.accountUserListStatus,
                            userListResult.description)
                    Next
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to add rule based user lists.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
