#region Copyright (C) 2008 Giacomo Stelluti Scala
//
// Copy Directory Tree: Program.cs
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
    using System.IO;

    using NUnit.Framework;

    [TestFixture]
    public sealed class PathUtilityFixture
    {
        [Test]
        public void GetBaseNameOfAPath()
        {
            string path = Environment.CurrentDirectory;
            Console.WriteLine("Path: {0}", path);

            string itemOfPath = Path.Combine(path, "FakeDirectory");
            Console.WriteLine("Path's Item: {0}", itemOfPath);

            string baseName = PathUtility.GetBaseName(path, itemOfPath);
            Console.WriteLine("Base Name: {0}", baseName);
        }
    }
}
#endif