using System;
using System.IO;
using System.Text;

namespace OBJEditor {
    internal static class Extensions {
        static Encoding encoding = Encoding.GetEncoding("UTF-16LE");
        internal static int GetInt32(this byte[] arr, int at) => BitConverter.ToInt32(arr, at);
        internal static int GetInt16(this byte[] arr, int at) => BitConverter.ToInt16(arr, at);

        internal static string GetString(this byte[] arr, int at) {
            int strLen = arr.GetInt32(at);
            at += 4;

            byte[] buffer = new byte[strLen * 2];
            for (int i = 0; i < strLen; i++) {
                buffer[(i * 2)] = arr[(i * 2) + at];
                buffer[(i * 2) + 1] = arr[(i * 2) + at + 1];
            }
            return encoding.GetString(buffer);
        }

        internal static void WriteTo(this string String, Stream Output) {
            byte[] data = encoding.GetBytes(String);
            BitConverter.GetBytes(data.Length / 2).CopyTo(Output, 0, 4);
            Output.Write(data, 0, data.Length);
        }

        internal static void CopyTo(this byte[] arr, Stream buffer, int readIndex, int length) => buffer.Write(arr, readIndex, length);
    }
}
