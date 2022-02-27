#region Copyright (C) 2008 Giacomo Stelluti Scala
//
// Copy Directory Tree: DirectoryIteratorFixture.cs
//
// Author:
//   Giacomo Stelluti Scala (giacomo.stelluti@gmail.com)
//
// Copyright (C) 2008 Giacomo Stelluti Scala
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
namespace tenoritool.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public sealed class DirectoryIteratorFixture
    {
        private static string testPath;

        [SetUp]
        public void PrepareFileSystem()
        {
            testPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        [Test]
        public void IterateDirsToConsole()
        {
            DirectoryIterator dirIter = new DirectoryIterator(testPath);
            dirIter.Iterate(delegate(DirectoryIterator sender, DirectoryIterator.Args args) { Console.WriteLine(args.Path); });
        }

        [Test]
        public void IterateAllToConsole()
        {
            DirectoryIterator dirIter = new DirectoryIterator(testPath);
            dirIter.ProcessFiles = true;
            dirIter.Iterate(new DirectoryIterator.Delegate(PrintDirsAndFiles));
        }

        private static void PrintDirsAndFiles(DirectoryIterator sender, DirectoryIterator.Args args)
        {
            string text;
            if (args.IsDirectory)
            {
                text = "DIR: {0}";
            }
            else
            {
                text = "FILE: {0}";
            }
            Console.WriteLine(text, args.Path);
        }
    }
}
#endif
