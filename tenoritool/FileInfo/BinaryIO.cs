#region Copyright (C) 2008 Giacomo Stelluti Scala
//
// Copy Directory Tree: BinaryIO.cs
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

namespace tenoritool
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    class BinaryIO
    {
        public static UInt32 ReadUInt32(byte[] data, int offset)
        {
            return Convert.ToUInt32(
                    data[offset++]
                 | (data[offset++] << 8)
                 | (data[offset++] << 16)
                 | (data[offset++] << 24)
                 );
        }

        public static UInt32 ReadUInt32(byte[] data)
        {
            return ReadUInt32(data, 0);
        }


        public static UInt64 ReadUInt64(byte[] data, int offset)
        {
            return Convert.ToUInt64(ReadUInt32(data, offset)) + (Convert.ToUInt64(ReadUInt32(data, offset + 4)) << 32);
        }

        public static UInt64 ReadUInt64(byte[] data)
        {
            return ReadUInt64(data, 0);
        }
    
    }
}