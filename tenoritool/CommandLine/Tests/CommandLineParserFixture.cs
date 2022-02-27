#region Copyright (C) 2005 - 2009 Giacomo Stelluti Scala
//
// Command Line Library: CommandLineParserFixture.cs
//
// Author:
//   Giacomo Stelluti Scala (gsscoder@ymail.com)
//
// Copyright (C) 2005 - 2009 Giacomo Stelluti Scala
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
#endregion

#if UNIT_TESTS
namespace CommandLine.Tests
{
    using System;
    using System.IO;
    using NUnit.Framework;

    [TestFixture]
    public sealed partial class CommandLineParserFixture
    {
        private static ICommandLineParser parser = new CommandLineParser();

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WillThrowExceptionIfArgumentsArrayIsNull()
        {
            parser.ParseArguments(null, new MockOptions());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WillThrowExceptionIfOptionsInstanceIsNull()
        {
            parser.ParseArguments(new string[] { }, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WillThrowExceptionIfTextWriterIsNull()
        {
            parser.ParseArguments(new string[] { }, new MockOptions(), null);
        }

        [Test]
        public void ParseStringOption()
        {
            MockOptions options = new MockOptions();
            bool success = parser.ParseArguments(
                    new string[] { "-s", "something" }, options);

            Assert.IsTrue(success);
            Assert.AreEqual("something", options.StringOption);
            Console.WriteLine(options);
        }

        [Test]
        public void ParseStringIntegerBoolOptions()
        {
            MockOptions options = new MockOptions();
            bool success = parser.ParseArguments(
                    new string[] { "-s", "another string", "-i100", "--switch" }, options);

            Assert.IsTrue(success);
            Assert.AreEqual("another string", options.StringOption);
            Assert.AreEqual(100, options.IntOption);
            Assert.AreEqual(true, options.BoolOption);
            Console.WriteLine(options);
        }

        [Test]
        public void ParseShortAdjacentOptions()
        {
            MockBoolPrevalentOptions options = new MockBoolPrevalentOptions();
            bool success = parser.ParseArguments(
                    new string[] { "-ca", "-d65" }, options);

            Assert.IsTrue(success);
            Assert.IsTrue(options.OptionC);
            Assert.IsTrue(options.OptionA);
            Assert.IsFalse(options.OptionB);
            Assert.AreEqual(65, options.DoubleOption);
            Console.WriteLine(options);
        }

        [Test]
        public void ParseShortLongOptions()
        {
            MockBoolPrevalentOptions options = new MockBoolPrevalentOptions();
            bool success = parser.ParseArguments(
                    new string[] { "-b", "--double=9" }, options);

            Assert.IsTrue(success);
            Assert.IsTrue(options.OptionB);
            Assert.IsFalse(options.OptionA);
            Assert.IsFalse(options.OptionC);
            Assert.AreEqual(9, options.DoubleOption);
            Console.WriteLine(options);
        }

        [Test]
        public void ValueListAttributeIsolatesNonOptionValues()
        {
            MockOptionsWithValueList options = new MockOptionsWithValueList();
            bool success = parser.ParseArguments(
                new string[] { "file1.ext", "file2.ext", "file3.ext", "-wo", "out.ext" }, options);

            Assert.IsTrue(success);
            Assert.AreEqual("file1.ext", options.InputFilenames[0]);
            Assert.AreEqual("file2.ext", options.InputFilenames[1]);
            Assert.AreEqual("file3.ext", options.InputFilenames[2]);
            Assert.AreEqual("out.ext", options.OutputFile);
            Assert.IsTrue(options.Overwrite);
            Console.WriteLine(options);
        }

        [Test]
        public void ValueListWithMaxElemInsideBounds()
        {
            MockOptionsWithValueListMaxElemDefined options = new MockOptionsWithValueListMaxElemDefined();
            bool success = parser.ParseArguments(
                    new string[] { "file.a", "file.b", "file.c" }, options);

            Assert.IsTrue(success);
            Assert.AreEqual("file.a", options.InputFilenames[0]);
            Assert.AreEqual("file.b", options.InputFilenames[1]);
            Assert.AreEqual("file.c", options.InputFilenames[2]);
            Assert.AreEqual(String.Empty, options.OutputFile);
            Assert.IsFalse(options.Overwrite);
            Console.WriteLine(options);
        }

        [Test]
        public void ValueListWithMaxElemOutsideBounds()
        {
            MockOptionsWithValueListMaxElemDefined options = new MockOptionsWithValueListMaxElemDefined();
            bool success = parser.ParseArguments(
                    new string[] { "file.a", "file.b", "file.c", "file.d" }, options);

            Assert.IsFalse(success);
        }

        [Test]
        public void ValueListWithMaxElemSetToZeroSucceeds()
        {
            MockOptionsWithValueListMaxElemEqZero options = new MockOptionsWithValueListMaxElemEqZero();
            bool success = parser.ParseArguments(new string[] { }, options);

            Assert.IsTrue(success);
            Assert.AreEqual(0, options.Junk.Count);
            Console.WriteLine(options);
        }

        [Test]
        public void ValueListWithMaxElemSetToZeroFailes()
        {
            MockOptionsWithValueListMaxElemEqZero options = new MockOptionsWithValueListMaxElemEqZero();
            Assert.IsFalse(parser.ParseArguments(new string[] { "some", "value" }, options));
        }

        [Test]
        public void ParseOptionList()
        {
            MockOptionsWithOptionList options = new MockOptionsWithOptionList();
            bool success = parser.ParseArguments(new string[] {
                                "-s", "string1:stringTwo:stringIII", "-f", "test-file.txt" }, options);

            Assert.IsTrue(success);
            Assert.AreEqual("string1", options.SearchKeywords[0]);
            Console.WriteLine(options.SearchKeywords[0]);
            Assert.AreEqual("stringTwo", options.SearchKeywords[1]);
            Console.WriteLine(options.SearchKeywords[1]);
            Assert.AreEqual("stringIII", options.SearchKeywords[2]);
            Console.WriteLine(options.SearchKeywords[2]);
        }

        /// <summary>
        /// Ref.: #BUG0000.
        /// </summary>
        [Test]
        public void ShortOptionRefusesEqualToken()
        {
            MockOptions options = new MockOptions();

            Assert.IsFalse(parser.ParseArguments(new string[] { "-i=10" }, options));
            Console.WriteLine(options);
        }

        ///// <summary>
        ///// Ref.: #BUG0001
        ///// </summary>
        //[Test]
        //[ExpectedException(typeof(MissingMethodException))]
        //public void CanNotCreateParserInstance()
        //{
        //    Activator.CreateInstance(typeof(Parser));
        //}

        [Test]
        public void ParseEnumOptions()
        {
            MockOptionsWithEnum options = new MockOptionsWithEnum();

            bool success = parser.ParseArguments(new string[] { "-f", "data.bin", "-a", "ReadWrite" }, options);

            Assert.IsTrue(success);
            Assert.AreEqual("data.bin", options.FileName);
            Assert.AreEqual(FileAccess.ReadWrite, options.FileAccess);
            Console.WriteLine(options);
        }

        #region #BUG0002
        [Test]
        public void ParsingNonExistentShortOptionFailsWithoutThrowingAnException()
        {
            MockOptions options = new MockOptions();
            bool success = parser.ParseArguments(new string[] { "-x" }, options);
            Assert.IsFalse(success);
        }

        [Test]
        public void ParsingNonExistentLongOptionFailsWithoutThrowingAnException()
        {
            MockOptions options = new MockOptions();
            bool success = parser.ParseArguments(new string[] { "--extend" }, options);
            Assert.IsFalse(success);
        }

        #endregion
    }
}
#endif
