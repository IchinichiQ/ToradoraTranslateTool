using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using OBJEditor;
using SevenZip;

namespace ToradoraTranslateTool
{
    class ObjTools
    {
        static string mainFilePath = Path.Combine(Application.StartupPath, "Translation.json");
        static string toolsDirectory = Path.Combine(Application.StartupPath, "Data", "DatWorker", "Workspace");

        public static void ProcessObjGz(string directoryPath)
        {
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Data", "Obj")))
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Data", "Obj"));

            string[] archives = Directory.GetFiles(directoryPath, "*.obj.gz", SearchOption.AllDirectories);

            foreach (string archive in archives)
            {
                if (Path.GetFileName(archive).EndsWith("AAA0.obj.gz") || Path.GetFileName(archive) == "STARTPOINT.obj.gz") // Such files can't be translated
                    continue;

                string newPath = Path.Combine(Application.StartupPath, "Data", "Obj", Path.GetFileNameWithoutExtension(archive), Path.GetFileName(archive)); // Data\Obj\%obj name%\%obj archive%
                Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                File.Copy(archive, newPath, true);
                
                Process myProc = new Process();
                myProc.StartInfo.FileName = Path.Combine(toolsDirectory, "gzip.exe");
                myProc.StartInfo.Arguments = "-d -f \"" + newPath + "\""; // -d for decompress, -f (force) for overwrite 
                myProc.StartInfo.WorkingDirectory = toolsDirectory;
                myProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myProc.Start();
                myProc.WaitForExit();
            }
        }

        public static void ProcessTxtGz(string directoryPath)
        {
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Data", "Txt")))
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Data", "Txt"));

            string archive = Path.Combine(directoryPath, "text", "utf16.txt.gz"); // We need only one file

            string newPath = Path.Combine(Application.StartupPath, "Data", "Txt", Path.GetFileNameWithoutExtension(archive), Path.GetFileName(archive)); // Data\Txt\%txt name%\%txt archive%
            Directory.CreateDirectory(Path.GetDirectoryName(newPath));
            File.Copy(archive, newPath, true);
 
            Process myProc = new Process();
            myProc.StartInfo.FileName = Path.Combine(toolsDirectory, "gzip.exe");
            myProc.StartInfo.Arguments = "-d -f \"" + newPath + "\""; // -d for decompress, -f (force) for overwrite 
            myProc.StartInfo.WorkingDirectory = toolsDirectory;
            myProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProc.Start();
            myProc.WaitForExit();
        }

        public static void ProcessSeekmap(string firstDirectory)
        {
            File.Copy(Path.Combine(firstDirectory, "seekmap.dat"), Path.Combine(toolsDirectory, "seekmap.txt.gz"), true);

            Process myProc = new Process();
            myProc.StartInfo.FileName = Path.Combine(toolsDirectory, "gzip.exe");
            myProc.StartInfo.Arguments = "-d -f seekmap.txt.gz"; // -d for decompress, -f (force) for overwrite 
            myProc.StartInfo.WorkingDirectory = toolsDirectory;
            myProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProc.Start();
            myProc.WaitForExit();

            File.Delete(Path.Combine(toolsDirectory, "seekmap.txt.gz"));
        }

        public static void RepackObj()
        {
            List<String> directories = new List<string>();       
            directories.AddRange(Directory.GetDirectories(Path.Combine(Application.StartupPath, "Data", "Obj")).Select(Path.GetFileName));

            JObject mainFile = JObject.Parse(File.ReadAllText(mainFilePath));
            foreach (string name in directories)
            {
                if (mainFile[name] != null)  // If json have translation for that file
                {
                    string filepath = Path.Combine(Application.StartupPath, "Data", "Obj", name, name);
                    OBJHelper myHelper = new OBJHelper(File.ReadAllBytes(filepath));
                    string[] scriptStrings = myHelper.Import();

                    for(int i = 0; i < scriptStrings.Length; i++)
                    {
                        string translatedString = mainFile[name][i.ToString()].ToString();
                        if (translatedString != "")
                            scriptStrings[i] = translatedString;                    
                    }

                    File.WriteAllBytes(Path.Combine(toolsDirectory, name), myHelper.Export(scriptStrings));

                    Process myProc = new Process();
                    myProc.StartInfo.FileName = Path.Combine(toolsDirectory, "gzip.exe");
                    myProc.StartInfo.Arguments = "-n9 -f " + name; // Without -n9 the game will freeze
                    myProc.StartInfo.WorkingDirectory = toolsDirectory;
                    myProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    myProc.Start();
                    myProc.WaitForExit();

                    File.Delete(Path.Combine(toolsDirectory, name));
                    
                    File.Replace(Path.Combine(toolsDirectory, name + ".gz"), Application.StartupPath + File.ReadAllText(filepath + ".txt"), null);
                }
            }
        }

        public static void RepackTxt()
        {
            List<String> directories = new List<string>();
            directories.AddRange(Directory.GetDirectories(Path.Combine(Application.StartupPath, "Data", "Txt")).Select(Path.GetFileName));

            JObject mainFile = JObject.Parse(File.ReadAllText(mainFilePath));
            foreach (string name in directories)
            {
                if (mainFile[name] != null)  // If json have translation for that file
                {
                    string filepath = Path.Combine(Application.StartupPath, "Data", "Txt", name, name);
                    string[] fileLines = File.ReadAllLines(filepath, new UnicodeEncoding(false, false)); ;

                    for (int i = 0; i < fileLines.Length; i++)
                    {
                        string translatedString = mainFile[name][i.ToString()].ToString();
                        if (translatedString != "")
                            fileLines[i] = translatedString;
                    }

                    File.WriteAllLines(Path.Combine(toolsDirectory, name), fileLines, new UnicodeEncoding(false, false));

                    Process myProc = new Process();
                    myProc.StartInfo.FileName = Path.Combine(toolsDirectory, "gzip.exe");
                    myProc.StartInfo.Arguments = "-n9 -f " + name; // Without -n9 the game will freeze
                    myProc.StartInfo.WorkingDirectory = toolsDirectory;
                    myProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    myProc.Start();
                    myProc.WaitForExit();

                    File.Delete(Path.Combine(toolsDirectory, name));

                    File.Replace(Path.Combine(toolsDirectory, name + ".gz"), Application.StartupPath + File.ReadAllText(filepath + ".txt"), null);
                }
            }
        }

        public static void RepackSeekmap(string resourcePath, string firstDirectory)
        {
            File.Copy(resourcePath, Path.Combine(toolsDirectory, "RES.dat"), true); // RES.dat and seekmap.txt required for modseekmap.exe
            Process myProc = new Process();
            myProc.StartInfo.FileName = Path.Combine(toolsDirectory, "modseekmap.exe"); // modseekmap generates seekmap.new file
            myProc.StartInfo.WorkingDirectory = toolsDirectory;
            myProc.Start();
            myProc.WaitForExit();
            File.Move(Path.Combine(toolsDirectory, "seekmap.new"), Path.Combine(toolsDirectory, "seekmap.dat")); // rename seekmap.new to seekmap.dat

            myProc = new Process(); // compress seekmap.dat to seekmap.dat.gz
            myProc.StartInfo.FileName = Path.Combine(toolsDirectory, "gzip.exe");
            myProc.StartInfo.Arguments = "-n9 -f seekmap.dat"; // Without -n9 the game will freeze
            myProc.StartInfo.WorkingDirectory = toolsDirectory;
            myProc.Start();
            myProc.WaitForExit();
            File.Copy(Path.Combine(toolsDirectory, "seekmap.dat.gz"), Path.Combine(firstDirectory, "seekmap.dat"), true); // rename seekmap.dat.gz to seekmap.dat and move it to the directory where first.dat was unpacked

            File.Delete(Path.Combine(toolsDirectory, "RES.dat")); // Remove temp files
            File.Delete(Path.Combine(toolsDirectory, "seekmap.dat.gz"));
        }

    }
}
