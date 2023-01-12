using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToradoraTranslateTool
{
    class LineBreaksInserter
    {
        private Dictionary<char, int> glyphsWidth;
        private readonly int maxLineLength;

        public LineBreaksInserter(string pathToDumpedFont, int maxLineLength)
        {
            glyphsWidth = new Dictionary<char, int>();

            LoadDumpedFont(pathToDumpedFont);
            this.maxLineLength = maxLineLength;
        }

        private void LoadDumpedFont(string pathToDumpedFont)
        {
            string[] dumpedFontLines = File.ReadAllLines(pathToDumpedFont);

            for (int i = 0; i < dumpedFontLines.Length; i+=2)
            {
                int charCode = int.Parse(dumpedFontLines[i], NumberStyles.HexNumber);
                char curChar = Char.ConvertFromUtf32(charCode).ToCharArray()[0];

                double charWidthInaccurate = double.Parse(dumpedFontLines[i + 1], CultureInfo.InvariantCulture);
                int charWidthAccurate = (int)Math.Ceiling(charWidthInaccurate);

                glyphsWidth.Add(curChar, charWidthAccurate);
            }
        }

        private int GetStringLength(string str, bool isSpeech)
        {
            char[] strChars = str.ToCharArray();
            int length = 0;

            for (int i = 0; i < strChars.Length; i++)
                length += glyphsWidth[strChars[i]];           
            
            if (isSpeech)
                length += glyphsWidth['「'];

            return length;
        }

        public string InsertLineBreaks(string insertTo, bool isSpeech)
        {
            string newString = "";
            string secondString = null;
            if (insertTo.Contains("[") && insertTo.Contains("]"))
            {
                secondString = Regex.Match(insertTo, @"\[(.*?)\]").Groups[1].Value;
                insertTo = insertTo.Replace("[" + secondString + "]", "");
            }

            if (GetStringLength(insertTo, isSpeech) > maxLineLength && !insertTo.Contains('＿'))
            {
                string[] words = insertTo.Split();

                for (int j = 0; j < words.Length; j++)
                {
                    string tempString;
                    if (newString.Contains('＿'))
                        // Symbol '＿' should be included in new line, because it affects the length of the line, although it is not visible
                        tempString = newString.Substring(newString.LastIndexOf('＿')) + " " + words[j];
                    else
                        tempString = newString + " " + words[j];

                    if (GetStringLength(tempString, isSpeech) > maxLineLength)
                        newString += "＿" + words[j];
                    else
                        newString += " " + words[j];
                }
                newString = newString.Trim();
            }
            else
                newString = insertTo;

            if (secondString != null)
                newString += "[" + InsertLineBreaks(secondString, isSpeech) + "]";

            return newString;
        }
    }
}
