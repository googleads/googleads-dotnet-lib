// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.Common.Tests.Config {

  /// <summary>
  /// Tests for <see cref="XmlDictionarySectionConfigurationSource"/> class.
  /// </summary>
  public class XmlDictionarySectionConfigurationSourceTests {
    
    /// <summary>
    /// The section name.
    /// </summary>
    private const string SECTION_NAME = "TestConfig";

    /// <summary>
    /// A mock <see cref="IFileInfo"/> class for testing purposes.
    /// </summary>
    private class MockFileInfo : IFileInfo {
      private string fileName;

      public MockFileInfo(string fileName) {
        this.fileName = fileName;
      }

      public bool Exists => true;

      public long Length => 0;

      public string PhysicalPath => fileName;

      public string Name => fileName;

      public DateTimeOffset LastModified => new DateTimeOffset(
          DateTime.Now.Subtract(new TimeSpan(2)));

      public bool IsDirectory => false;

      public Stream CreateReadStream() {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    /// A mock <see cref="IDirectoryContents"/> class for testing purposes.
    /// </summary>
    private class MockDirectoryContents : IDirectoryContents {
      private List<MockFileInfo> files = new List<MockFileInfo>();

      public MockDirectoryContents(string[] fileNames) {
        foreach (string fileName in fileNames) {
          files.Add(new MockFileInfo(fileName));
        }
      }

      public bool Exists => true;

      public IEnumerator<IFileInfo> GetEnumerator() => files.GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => files.GetEnumerator();

      internal IFileInfo GetFileInfo(string subpath) {
        foreach (IFileInfo file in files) {
          if (file.Name == subpath) {
            return file;
          }
        }
        return new NullFileProvider().GetFileInfo(subpath);
      }
    }

    /// <summary>
    /// A mock <see cref="IFileProvider"/> for testing purposes.
    /// </summary>
    private class MockFileProvider : IFileProvider {
      private MockDirectoryContents contents;

      public MockFileProvider(MockDirectoryContents contents) {
        this.contents = contents;
      }

      public IDirectoryContents GetDirectoryContents(string subpath) {
        return contents;
      }

      public IFileInfo GetFileInfo(string subpath) {
        return contents.GetFileInfo(subpath);
      }

      public IChangeToken Watch(string filter) {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    /// Tests the <see cref="XmlDictionarySectionConfigurationSource.Build(IConfigurationBuilder)"/>
    /// method.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestTryParse() {
      XmlDictionarySectionConfigurationSource source;

      // Ensure that an exe.config file is picked up.
      string[] fileSet1 = new string[] { "Test.exe", "Test.exe.config" };
      source = BuildConfigSource(fileSet1);
      Assert.AreEqual("Test.exe.config", source.Path);

      // Ensure that a web.config file is picked up.
      string[] fileSet2 = new string[] { "Test.aspx", "web.config" };
      source = BuildConfigSource(fileSet2);
      Assert.AreEqual("web.config", source.Path);

      // Ensure that any .config file is picked up.
      string[] fileSet3 = new string[] { "Test.aspx", "Foo.config" };
      source = BuildConfigSource(fileSet3);
      Assert.AreEqual("Foo.config", source.Path);

      // When Exe.config and web.config is provided, exe.config takes precedence.
      string[] fileSet4 = new string[] { "web.config", "Test.exe", "Test.exe.config" };
      source = BuildConfigSource(fileSet4);
      Assert.AreEqual("Test.exe.config", source.Path);

      // When web.config and random .config is provided, web.config takes precedence.
      string[] fileSet5 = new string[] { "foo.config", "Test.exe", "web.config", "bar.config" };
      source = BuildConfigSource(fileSet5);
      Assert.AreEqual("web.config", source.Path);

      // When no files are provided, path is set to empty, and Optional is set to true.
      string[] fileSet6 = new string[] {  };
      source = BuildConfigSource(fileSet6);
      Assert.That(source.Path, Is.Null.Or.Empty);
      Assert.IsTrue(source.Optional);
    }

    /// <summary>
    /// Builds the configuration source from a list of file names.
    /// </summary>
    /// <param name="fileNames">The list of file names.</param>
    /// <returns>A configuration source for testing purposes.</returns>
    private static XmlDictionarySectionConfigurationSource BuildConfigSource(string[] fileNames) {
      MockFileProvider fileProvider = new MockFileProvider(new MockDirectoryContents(fileNames));
      ConfigurationBuilder configBuilder = new ConfigurationBuilder();
      configBuilder.SetFileProvider(fileProvider);
      XmlDictionarySectionConfigurationSource source =
          new XmlDictionarySectionConfigurationSource(SECTION_NAME);
      source.Build(configBuilder);
      return source;
    }
  }
}
