# Budget Utilization Report

## Introduction

One of the important tasks when managing an AdWords account is to keep track of
 its budget utilization. Some campaigns may spend money faster than what you’d
 like, whereas others may have excess budget that never gets spent. Depending on
 your requirements, you may want to reallocate some unused budget from the
 campaigns that are underspending their budget, to ones that may need more
 budget. In other cases, you might want to investigate why the campaigns are
 not spending their allotted budgets efficiently.

Budget Utilization report analyzes your account’s performance over a specified
 period of time, and summarizes how campaigns utilized their budgets during
 that time frame.

## How it works

The Budget Utilization report is written as a C# code example, and uses the
 Ads API .NET library. The code example runs against a single Advertiser
 account. It accepts a start and an end date in ```YYYYMMDD``` format, and
 the path to which the output report should be written.

The application does the following steps when calculating the budget utilization:

- Run campaign performance report for the specified period.
- For each campaign that has <code>budgetLostImpressionShare != 0</code>
 the application calculates the extra budget required as follows:

    estimatedImpressions = impressions / impressionShare
    clickThroughRate = clicks / impressions
    averageCpc = clicks / cost
    lostImpressions = budgetLostImpressionShare * estimatedImpressions
    lostClicks = clickThroughRate * lostImpressions
    extraBudgetNeeded = lostClicks * averageCpc

For each campaign that has ```budgetLostmpressionShare = 0```, the excess budget
 is calculated as follows:

    extraBudget = budget - cost

The application generates a csv report of the form

    CampaignId, CampaignName, Budget, Cost, ExtraBudget, ExtraClicks, ExtraImpressions

## Taking corrective action

Once you have identified the campaigns that need to be fixed, you can take the
 following corrective action:

### 1. Adjust the budget

The budget utilization report shows how much budget is not being utilized in a
 campaign. You can choose to reduce its budget to a value that reflects its
 historic performance. The report also shows the excess budget requirements of
 campaigns that used up their allotted budget;
 you can increase their budgets if desired. You can use the get method of
 [CampaignService](//developers.google.com/adwords/api/docs/reference/latest/CampaignService)
 to retrieve a campaign’s ```budgetId```, and then use
 [BudgetService](//developers.google.com/adwords/api/docs/reference/latest/BudgetService)
 to retrieve and update the budget.

### 2. Adjust the bids

A campaign may have keywords or placements that command high bids and hence are
 draining the campaign budget very fast. Or it might have low bids that prevent
 utilizing the campaign budget appropriately.

You can adjust bids for your criteria using the mutate method of
 [AdGroupCriterionService](//developers.google.com/adwords/api/docs/reference/latest/AdGroupCriterionService).
 If the criterion is using a shared or flexible bidding strategy, you may have
 to use the [BiddingStrategyService](//developers.google.com/adwords/api/docs/reference/latest/BiddingStrategyService)
 instead. See our [guide](//developers.google.com/adwords/api/docs/guides/bidding)
 on bidding for more details.

### 3. Adjust your audience

Your campaign may be targeting regions that you don’t provide services, or may
 be targeting the wrong audience. To fix this, scan your list of keywords and
 placements that spend your budget without ROI, and pause them. Similarly, scan
 the locations your campaign is targeting, and add or remove location targets as
 required. In some cases, you may want to adjust the bids for locations instead.
