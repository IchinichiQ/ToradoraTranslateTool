using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace DatWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Toradora DAT Automation Tool");
                Console.WriteLine("Drop Dat files");
            }

            foreach (string arg in args)
            {
                if (arg.ToLower().EndsWith("-LstOrder.lst".ToLower()))
                {
                    string[] LSTS = File.ReadAllLines(arg);
                    SaveDat(LSTS);
                }
                else
                {
                    LSTOrder = new List<string>();
                    ResetWorkspace();
                    OpenDat(arg);
                    LSTOrder.Reverse();
                    File.WriteAllLines(arg + "-LstOrder.lst", LSTOrder.ToArray());
                }
            }
            //Console.ReadKey();
        }

        private static void ResetWorkspace()
        {
            Console.WriteLine("Starting...");
            string[] Files = Directory.GetFiles(".\\Workspace", "*.dat", SearchOption.TopDirectoryOnly);
            foreach (string DAT in Files)
            {
                if (File.Exists(".\\" + Path.GetFileName(DAT)))
                    File.Delete(DAT);
                else
                    File.Move(DAT, ".\\" + Path.GetFileName(DAT));
            }
        }

        static List<string> LSTOrder = new List<string>();
        const string TMP = ".\\Workspace\\{0}";

        #region EXTRACT
        static bool OpenDat(string DAT)
        {
            try
            {
                Console.WriteLine("Extracting: {0}", Path.GetFileName(DAT));
                string[] Files = ExtractDatContent(DAT);
                foreach (string File in Files)
                {
                    if (Path.GetExtension(File).ToLower().Trim(' ', '.') == "dat")
                    {
                        OpenDat(File);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        static string[] ExtractDatContent(string DAT)
        {
            string TMPPath = string.Format(TMP, Path.GetFileName(DAT));
            if (File.Exists(TMPPath))
                File.Delete(TMPPath);

            File.Move(DAT, TMPPath);

            string NewDir = Path.GetDirectoryName(DAT) + "\\" + Path.GetFileNameWithoutExtension(DAT) + "\\";
            if (NewDir.StartsWith("\\"))
                NewDir = "." + NewDir;

            var startinfo = new ProcessStartInfo("cmd.exe", "/C \".\\Workspace\\!XP.BAT\"")
            {
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
            List<string> FLIST = new List<string>();
            string isPadded = "N";
            var process = new Process();
            process.StartInfo = startinfo;
            process.ErrorDataReceived += (sender, args) => {
                if (args.Data == null)
                    return;

                string Line = args.Data.Trim();

                if (Line.StartsWith("Is padded with space: "))
                {
                    isPadded = Line.Replace("Is padded with space: ", "").Substring(0, 1);
                }

                if (!Line.StartsWith("^"))
                {
                    if (!Line.Contains("#") || !Line.Contains("@"))
                    {
                        return;
                    }
                    string DFN = GetDatFN(Line.Split('#')[1].Split('@')[0].Trim('\t', ' ', ','));
                    FLIST.Add(DFN);
                    Console.Title = "Processing: " + DFN;
                    return;
                }

                FLIST[FLIST.Count - 1] = ".\\" + Line.Trim('^', ' ', '\t') + '\t' + FLIST.Last();
            };
            process.Start();
            process.BeginErrorReadLine();
            process.WaitForExit();


            File.Move(TMPPath, DAT);

            if (Directory.Exists(".\\Workspace\\" + Path.GetFileNameWithoutExtension(DAT)))
            {

                if (Directory.Exists(NewDir))
                    Directory.Delete(NewDir, true);
                Directory.Move(".\\Workspace\\" + Path.GetFileNameWithoutExtension(DAT), NewDir);

                string TXT = isPadded;
                foreach (string File in FLIST.ToArray())
                    TXT += "\r\n" + File;

                string lst = GetDatLFN(DAT);
                if (lst.StartsWith("\\"))
                    lst = "." + lst;

                if (File.Exists(lst))
                    File.Delete(lst);

                File.WriteAllText(lst, TXT);
                LSTOrder.Add(lst);

                return Directory.GetFiles(NewDir, "*", SearchOption.AllDirectories);
            }

            return new string[0];
        }

        private static string GetDatFN(string file)
        {
            string[] Splits = Path.GetFileName(file).Split('.');
            if (Splits.Length >= 3 && int.TryParse(Splits[Splits.Length - 2], out int tmp))
            {
                string FN = string.Empty;
                for (int i = 0; i < Splits.Length; i++)
                {
                    if (int.TryParse(Splits[i], out int TMP) && Splits[i].StartsWith("0"))
                        continue;
                    FN += Splits[i] + ".";
                }
                return FN.TrimEnd('.');
            }

            return Path.GetFileName(file);
        }
        private static string GetDatLFN(string file)
        {
            return Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file) + ".lst";
        }
        #endregion

        #region REPACK
        public static bool SaveDat(string[] Files)
        {
            foreach (string File in Files)
            {
                if (!System.IO.File.Exists(File))
                    continue;

                Console.WriteLine("Repacking: {0}", Path.GetFileName(File));
                RepackDat(File);
            }

            return true;
        }
        public static bool RepackDat(string DAT)
        {
            try
            {
                string DatDir = Path.GetDirectoryName(DAT) + "\\";
                if (DatDir.StartsWith("\\"))
                    DatDir = '.' + DatDir;

                if (DatDir.StartsWith(".\\"))
                {
                    DatDir = AppDomain.CurrentDomain.BaseDirectory + DatDir.Substring(2, DatDir.Length - 2);
                }

                Process Proc = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = AppDomain.CurrentDomain.BaseDirectory + "Workspace\\makeGDP.exe",
                        Arguments = "\"" + DatDir + Path.GetFileNameWithoutExtension(DAT) + "\"",
                        WorkingDirectory = DatDir,
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };

                Proc.Start();
                Proc.WaitForExit();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}
