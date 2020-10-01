using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace OBJEditor
{
    public class OBJ
    {
        const short DIALOGUE = 0x64;
        const short DIALOGUE2 = 0x68;

        const short CHOICE = 0x69;
        const short CHOICE2 = 0x67;

        const short QUESTION = 0x0323;

        const short CHAPTER = 0x2BC;

        byte[] script;

        public OBJ(byte[] script)
        {
            this.script = script;
        }

        public string[] Import()
        {
            List<string> strings = new List<string>();
            int blockCount = script.GetInt32(0x00);
            int blockLen = script.GetInt32(0x04);
            for (int i = blockLen, x = 0; x < blockCount; x++, i += blockLen)
            {
                blockLen = script.GetInt32(i);
                int index = i + 6;
                int entries = 0;
                switch (script.GetInt16(i + 4))
                {
                    case DIALOGUE2:
                    case DIALOGUE:
                        strings.Add(script.GetString(i + 10));
                        break;

                    case CHOICE:
                        entries = script.GetInt32(index);
                        for (int y = 0; y < entries; y++)
                        {
                            index += 0x8;
                            strings.Add(script.GetString(index));
                            index += (script.GetInt32(index) * 2) + 4;
                        }
                        break;

                    case CHOICE2:
                        entries = script.GetInt32(index);
                        index += 0x8;

                        for (int y = 0; y < entries; y++)
                        {

                            strings.Add(script.GetString(index));
                            index += (script.GetInt32(index) * 2) + 4;

                            if (script.GetInt32(index) == 0x00)
                            {
                                index += 8;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Assert(script.GetInt32(index) == 0x01);

                                index += 4;
                                index += (script.GetInt32(index) * 2) + 4;
                                index += 4;
                            }
                        }
                        break;

                    case QUESTION:
                        index += 4;
                        entries = script.GetInt32(index);
                        index += 4;

                        strings.Add(script.GetString(index));
                        index += 0x4 + (script.GetInt32(index) * 2);

                        for (int y = 0; y < entries; y++)
                        {
                            strings.Add(script.GetString(index));
                            index += 0x4 + (script.GetInt32(index) * 2) + 0x24;
                        }
                        break;
                    case CHAPTER:
                        strings.Add(script.GetString(index));
                        break;
                }
            }

            return strings.ToArray();
        }

        public byte[] Export(string[] Strings)
        {
            int blockCount = script.GetInt32(0x00);
            int blockLen = script.GetInt32(0x04);
            List<List<int>> jumpUpdates = new List<List<int>>();

            MemoryStream output = new MemoryStream();
            script.CopyTo(output, 0, blockLen);

            for (int i = blockLen, x = 0, ID = 0; x < blockCount; x++, i += blockLen)
            {
                blockLen = script.GetInt32(i);
                MemoryStream newBlock;
                int index = i;
                int count;

                switch (script.GetInt16(i + 4))
                {
                    case DIALOGUE2:
                    case DIALOGUE:
                        newBlock = new MemoryStream();
                        script.CopyTo(newBlock, i + 4, 0x6);

                        string phrase = Strings[ID++];
                        string secondPhrase = null;
                        if (phrase.Contains("[DEL]"))
                        {
                            List<int> jumpInfo = new List<int> { x, -1 };
                            jumpUpdates.Add(jumpInfo);
                            x--;
                            blockCount--;
                            break;
                        }
                        if (phrase.Contains("[") && phrase.Contains("]"))
                        {
                            secondPhrase = Regex.Match(phrase, @"\[(.*?)\]").Groups[1].Value;
                            phrase = phrase.Replace("[" + secondPhrase + "]", "");

                            if (phrase.Contains("（") && phrase.Contains("）"))
                                secondPhrase = "（" + secondPhrase + "）";
                            if (phrase.EndsWith("」"))
                                secondPhrase = phrase.Substring(0, phrase.IndexOf("「") + 1) + secondPhrase + "」";
                        }
                        phrase.WriteTo(newBlock);

                        WriteBlock(newBlock, output);
                        if (secondPhrase != null)
                        {
                            newBlock = new MemoryStream();
                            script.CopyTo(newBlock, i + 4, 0x2);
                            newBlock.Write(new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, 4);

                            secondPhrase.WriteTo(newBlock);
                            WriteBlock(newBlock, output);

                            List<int> jumpInfo = new List<int> { x, 1 };
                            jumpUpdates.Add(jumpInfo);
                            x++;
                            blockCount++;
                        }

                        break;

                    case CHOICE:
                        newBlock = new MemoryStream();
                        count = script.GetInt32(index + 0x6);
                        script.CopyTo(newBlock, index + 4, 0x2);
                        index += 0x06;

                        for (int y = 0; y < count; y++)
                        {
                            script.CopyTo(newBlock, index, 0x8);
                            LimitString(Strings[ID++]).WriteTo(newBlock);

                            index += 0x8;
                            index += (script.GetInt32(index) * 2) + 4;
                        }

                        WriteBlock(newBlock, output);
                        break;
                    case CHOICE2:
                        newBlock = new MemoryStream();
                        index += 4;

                        count = script.GetInt32(index + 0x2);

                        script.CopyTo(newBlock, index, 0xA);
                        index += 0xA;


                        for (int y = 0; y < count; y++)
                        {
                            LimitString(Strings[ID++]).WriteTo(newBlock);
                            index += (script.GetInt32(index) * 2) + 4;


                            script.CopyTo(newBlock, index, 0x4);
                            index += 4;

                            if (script.GetInt32(index - 4) == 0x00)
                            {
                                script.CopyTo(newBlock, index, 0x4);
                                index += 4;
                            }
                            else
                            {
                                int LabelLen = (script.GetInt32(index) * 2) + 4;
                                script.CopyTo(newBlock, index, LabelLen);
                                index += LabelLen;

                                script.CopyTo(newBlock, index, 0x4);
                                index += 4;
                            }
                        }

                        WriteBlock(newBlock, output);
                        break;

                    case QUESTION:
                        newBlock = new MemoryStream();

                        count = script.GetInt32(index + 0xA);
                        script.CopyTo(newBlock, index + 4, 0xA);
                        index += 0xE;

                        index += (script.GetInt32(index) * 2) + 4;
                        LimitString(Strings[ID++]).WriteTo(newBlock);

                        for (int y = 0; y < count; y++)
                        {
                            index += (script.GetInt32(index) * 2) + 4;

                            LimitString(Strings[ID++]).WriteTo(newBlock);


                            script.CopyTo(newBlock, index, 0x24);
                            index += 0x24;
                        }

                        WriteBlock(newBlock, output);
                        break;

                    case CHAPTER:
                        newBlock = new MemoryStream();
                        script.CopyTo(newBlock, index + 4, 0x2);

                        index += 0x6;
                        index += (script.GetInt32(index) * 2) + 4;

                        Strings[ID++].WriteTo(newBlock);
                        script.CopyTo(newBlock, index, 0x6);

                        WriteBlock(newBlock, output);
                        break;

                    default:
                        script.CopyTo(output, i, blockLen);
                        break;
                }
            }

            byte[] blockCountBytes = BitConverter.GetBytes(blockCount);
            output.Position = 0;
            output.Write(blockCountBytes, 0, blockCountBytes.Length);

            for (int i = 0; i < jumpUpdates.Count; i++)
                UpdateJumps(output, jumpUpdates[i][0], jumpUpdates[i][1]);

            return output.ToArray();
        }

        private void UpdateJumps(MemoryStream stream, int minBlock, int change)
        {
            stream.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < stream.Length; i += 16)
            {
                byte[] line = new byte[16];
                stream.Seek(i, SeekOrigin.Begin);
                stream.Read(line, 0, 15);
                // jump is 10 00 00 00 BE 02 xx xx xx xx 00 00 00 00 00 00, where "xx xx xx xx" is block number
                if (line[0] == 0x10 && line[1] == 0x00 && line[2] == 0x00 && line[3] == 0x00 && line[4] == 0xBE && line[5] == 0x02)
                {
                    int blockNumber = line.GetInt32(0x06);
                    if (blockNumber < minBlock) continue;
                    blockNumber += change;
                    byte[] blockIdBytes = BitConverter.GetBytes(blockNumber);
                    line[0x06] = blockIdBytes[0];
                    line[0x07] = blockIdBytes[1];
                    stream.Seek(i, SeekOrigin.Begin);
                    stream.Write(line, 0, 15);
                }
            }
        }

        private string LimitString(string Input)
        {
            string Result = Input.Replace("＿", @" ");
            return Result;
        }
        public void WriteBlock(Stream Content, Stream Output)
        {
            int NewLen = (int)Content.Length + 4;
            int Blank = 0;

            while ((NewLen + Blank) % 0x10 != 0x00)
                Blank++;

            if (Blank <= 0x8)
                Blank += 0x10;

            NewLen += Blank;
            BitConverter.GetBytes(NewLen).CopyTo(Output, 0, 4);
            Content.Seek(0, 0);
            Content.CopyTo(Output);
            (new byte[Blank]).CopyTo(Output, 0, Blank);
        }

    }
}
