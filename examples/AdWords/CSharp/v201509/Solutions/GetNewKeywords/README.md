# Get New Keywords

## Introduction

When you initially create a campaign in AdWords, you might start with a few ads,
 and a small set of keywords. However, as time proceeds, you would want to
 manually research and add new keywords. This code example shows the concepts
 explained in optimization guides for
  [TargetingIdeaService](//developers.google.com/adwords/api/docs/guides/targeting-idea-service) and
 [TrafficEstimatorService](//developers.google.com/adwords/api/docs/guides/traffic-estimator-service)
 in action.

## How does the example work?

The code example generates a list of expansion keywords for a given campaign.
 It performs the following steps as part of this process:

- Identify seed keywords from multiple data sources.
- Generate keyword ideas using the seed keywords, while excluding negative terms.
- Generate traffic estimates for the keywords.
- Check if generated keywords have policy violations.
- Save the keyword list to a local csv file for further processing.

## Identifying seed keywords

The code example picks seed keywords from three different sources:

### Search Query report

Search Query report (SQR) shows how your ads performed when triggered by actual
 searches within the Search Network. Search Query report can be used to identify
 new search terms with high potential, then add them to your keyword list. The
 code example runs SQR for the previous month and picks the top N search queries
 as seed keywords.

Note: Search Query report may also reveal search terms that are not relevant for
 your business. In such cases, you can add those search terms as negative
 keywords at the campaign level to avoid spending money showing your ad to
 people who aren't interested in it.

### Keywords Performance report

Keywords Performance report helps you identify the top performing keywords in
 your account during a given period. The script runs the keywords performance
 report for the previous month and picks the top N keywords as seed keywords.

### User input

You may identify new keywords that are relevant to your campaign from other
 sources. These keywords may also be used as seed keywords when researching new
 keywords. The code example accepts a list of user-provided keywords from a CSV
 file of the format

    keyword, matchtype

The following values are accepted for matchtype: ```EXACT, PHRASE and BROAD```.

## Generating keyword ideas

Once you identify the seed keywords, you can use [TargetingIdeaService]
(//developers.google.com/adwords/api/docs/reference/v201502/TargetingIdeaService)
 to generate keyword ideas. The code example allows you to provide language and
 location search criteria to control how the keyword ideas are generated.

The code example also allows you to provide a list of negative terms that you’d
 like to exclude in the generated keyword ideas. This list is provided through
 a CSV file of the format

    keyword, matchtype

The following values are accepted for matchtype: ```EXACT, PHRASE and BROAD```.

## Generating traffic estimates

Once you have the seed keywords, you can use TrafficEstimatorService to generate
 traffic estimates for the keywords. The script accepts a maxCPC value that
 specifies how much you are willing to pay for each click when estimating
 traffic for the new keywords.

## Performing policy checks

The code example allows you to check the newly generated keywords for policy
 violations. This is done by retrieving an ad group from the target campaign,
 and trying to add the keywords to that ad group after setting the validateOnly
 header to true. Any policy violations found are saved to the output.

## Configuring the code example

The following parameters in the Settings class may be used to configure the code
 example.

<table>
  <tr>
    <th>Setting name</th>
    <th>Details</th>
  </tr>
  <tr>
    <td>SQR_MAX_RESULTS</td>
    <td>Specifies how many items to pick from Search Query performance report
     when picking seed keywords.</td>
  </tr>
  <tr>
    <td>KPR_MAX_RESULTS</td>
    <td>Specifies how many items to pick from Keywords Performance report when
     picking seed keywords.</td>
  </tr>
  <tr>
    <td>LOCATIONS</td>
    <td>Specifies what locations should be used when generating keyword ideas
     and estimates. If this setting is empty, then the keyword ideas and
     estimates are generated for all locations. If you include multiple
     locations, make sure the locations don’t overlap, to avoid double-counting
     traffic estimates.</td>
  </tr>
  <tr>
    <td>LANGUAGES</td>
    <td>Specifies what languages should be used when generating keyword ideas
     and estimates. If this setting is empty, then the keyword ideas and
     estimates are generated for all languages.
    </td>
  </tr>
  <tr>
    <td>USER_KEYWORDS_FILE_PATH</td>
    <td>Full path of the file from which user input seed keywords are picked.
     The CSV file should be of the format:
     <pre>    keyword, matchtype</pre>
     The following values are accepted for matchtype column: <code>EXACT,
     PHRASE and BROAD</code>.
    </td>
  </tr>
  <tr>
    <td>USER_NEGATIVE_TERMS_FILE_PATH</td>
    <td>Full path of the file from which user negative terms are picked.
     The CSV file should be of the format:
     <pre>keyword, matchtype</pre>
     The following values are accepted for matchtype column:
      <code>EXACT, PHRASE and BROAD</code>.
    </td>
  </tr>
</table>

The script also accepts the following parameters at runtime:

<table>
  <tr>
    <th>Parameter name</th>
    <th>Details</th>
  </tr>
  <tr>
    <td>campaignId</td>
    <td>ID of the campaign for which keyword expansion is done.</td>
  </tr>
  <tr>
    <td>maxCpcInMicros</td>
    <td>The maximum CPC in micros, when performing traffic estimates.</td>
  </tr>
  <tr>
    <td>outputPath</td>
    <td>Path to which output file is generated.</td>
  </tr>
</table>
