// Copyright 2009, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace com.google.api.adwords.lib.util {
  /// <summary>
  /// Wraps the functionalities to read and write from a CSV file.
  /// </summary>
  public class CsvFile {
    /// <summary>
    /// Headers in the csv file.
    /// </summary>
    List<string> headers;

    /// <summary>
    /// Records in the csv file.
    /// </summary>
    List<string[]> records;

    /// <summary>
    /// public constructor.
    /// </summary>
    public CsvFile() {
      headers = new List<string>();
      records = new List<string[]>();
    }

    /// <summary>
    /// CSV file headers, that represents the column names.
    /// </summary>
    public List<string> Headers {
      get {return headers;}
      set {headers = value;}
    }

    /// <summary>
    /// Collection of records in the CSV file.
    /// </summary>
    public List<string[]> Records {
      get {return records;}
      set {records = value;}
    }

    /// <summary>
    /// Reads the contents of the CSV file into memory.
    /// </summary>
    /// <param name="fileName">Full path to the csv file.</param>
    /// <param name="hasHeaders">True, if the first line of the csv file is a header.</param>
    public void Read(string fileName, bool hasHeaders) {
      Load(fileName, hasHeaders);
    }

    /// <summary>
    /// Reads the contents of the CSV string into memory.
    /// </summary>
    /// <param name="fileName">Text to be parsed as csv file contents.</param>
    /// <param name="hasHeaders">True, if the first line of the csv file contents
    /// is a header.</param>
    public void ReadFromString(string contents, bool hasHeaders) {
      Parse(contents, hasHeaders);
    }

    /// <summary>
    /// Writes the contents of the CsvFile object into a file.
    /// </summary>
    /// <param name="fileName">The full path of the file to which
    /// the contents are to be written.</param>
    /// <remarks>The file will have headers only if <see cref="Headers"/>
    /// are set for this object.</remarks>
    public void Write(string fileName) {
      StreamWriter writer = null;
      try {
        writer = new StreamWriter(fileName);
        if (Headers != null) {
          StringBuilder builder = ConvertRowToCsvString(Headers.ToArray());
          writer.WriteLine(builder.ToString().TrimEnd(','));
        }
        foreach (string[] row in Records) {
          StringBuilder builder = ConvertRowToCsvString(row);
          writer.WriteLine(builder.ToString().TrimEnd(','));
        }
      } finally {
        if (writer != null) {
          writer.Close();
        }
      }
    }

    /// <summary>
    /// Converts a csv row item collection into a csv string.
    /// </summary>
    /// <param name="rowItems">An array of string items which represents
    /// one row in CSV file.</param>
    /// <returns>A StringBuilder object representing the stringized row.
    /// You can call a ToString() to get the stringized representation
    /// for this row.</returns>
    private StringBuilder ConvertRowToCsvString(string[] rowItems) {
      StringBuilder builder = new StringBuilder();
      for (int i = 0; i < rowItems.Length; i++) {
        string temp = rowItems[i];
        temp = temp.Replace("\"", "\"\"");
        if (temp.Contains(",") || temp.Contains("\"")) {
          temp = "\"" + temp + "\"";
        }
        builder.Append(temp + ",");
      }
      return builder;
    }

    /// <summary>
    /// Parses a csv file and loads it into memory.
    /// </summary>
    /// <param name="filepath">Full path to the csv file.</param>
    /// <param name="hasHeaders">True, if the first line of the csv file is a header.</param>
    private void Load(string filePath, bool hasHeaders) {
      StreamReader reader = null;
      try {
        reader = new StreamReader(filePath);
        string contents = reader.ReadToEnd();
        Parse(contents, hasHeaders);
      } finally {
        if (reader != null) {
          reader.Close();
        }
      }
    }

    /// <summary>
    /// Parses a csv file's contents and loads it into memory.
    /// </summary>
    /// <param name="contents">File contents that should be parsed into memory.</param>
    /// <param name="hasHeaders">True, if the first line of the csv file is a header.</param>
    private void Parse(string contents, bool hasHeaders) {
      string[] lines = contents.Split(new char[] {'\n', '\r'},
          StringSplitOptions.RemoveEmptyEntries);

      if (lines.Length == 0) {
        return;
      }
      int startIndex = 0;

      if (hasHeaders) {
        headers = new List<string>(SplitCsvLine(lines[0], StringSplitOptions.None));
        startIndex = 1;
      } else {
        headers = null;
      }
      records = new List<string[]>();
      for (int i = startIndex; i < lines.Length; i++) {
        string[] splits = SplitCsvLine(lines[i], StringSplitOptions.None);
        records.Add(splits);
      }
    }

    /// <summary>
    /// Splits a csv line into its components.
    /// </summary>
    /// <param name="text">The comma separated line to be split into
    /// components.</param>
    /// <param name="options">The string splitting options.</param>
    /// <returns>The items, broken down as an array of strings.</returns>
    private static string[] SplitCsvLine(string text, StringSplitOptions options) {
      if (text == null) {
        throw new ArgumentException("text cannot be null");
      }
      int start = 0;
      List<string> tempItems = new List<string>();
      bool quotes = false;
      for (int i = 0; i < text.Length; i++) {
        switch (text[i]) {
          case '"':
            quotes = !quotes;
            break;

          case ',':
            if (!quotes) {
              ExtractAndAddItem(text, start, i, tempItems, options);
              start = i + 1;
            }
            break;
        }
      }
      ExtractAndAddItem(text, start, text.Length, tempItems, options);
      // Quotes opened, not closed.
      if (quotes) {
        throw new ApplicationException("quotes not closed.");
      }
      return tempItems.ToArray();
    }

    /// <summary>
    /// Extracts one token identified by SplitCsvLine.
    /// </summary>
    /// <param name="text">The original comma separated line.</param>
    /// <param name="start">Start index for the item just identified.</param>
    /// <param name="end">Stop index for the item just identified.</param>
    /// <param name="tempItems">The array to which the item should be
    /// added.</param>
    /// <param name="options">The string split options to be used while extracting the
    /// token.</param>
    private static void ExtractAndAddItem(string text, int start, int end,
        List<string> tempItems, StringSplitOptions options) {
      string item = "";

      item = text.Substring(start, end - start);
      item = item.Replace("\"\"", "\"");
      if (item.Length >= 2 && item[0] == '"' && item[item.Length - 1] == '"') {
        item = item.Substring(1, item.Length - 2);
      }
      if (options == StringSplitOptions.None || (options == StringSplitOptions.RemoveEmptyEntries
          && !string.IsNullOrEmpty(item))) {
        tempItems.Add(item);
      }
      return;
    }
  }
}
