using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using OBJEditor;
using SevenZip;

namespace ToradoraTranslateTool
{
    class ObjTools
    {

        public static void ProcessObjGz(string directoryPath)
        {
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Data", "Obj")))
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Data", "Obj"));

            SevenZipExtractor.SetLibraryPath(Path.Combine(Application.StartupPath, "7z.dll"));

            string[] archives = Directory.GetFiles(directoryPath, "*.obj.gz", SearchOption.AllDirectories);

            foreach (string archive in archives)
            {
                string newPath = Path.Combine(Application.StartupPath, "Data", "Obj", Path.GetFileNameWithoutExtension(archive), Path.GetFileName(archive)); // Data\Obj\%obj name%\%obj archive%
                Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                File.Copy(archive, newPath, true);
                File.WriteAllText(Path.Combine(Application.StartupPath, "Data", "Obj", Path.GetFileNameWithoutExtension(archive), Path.GetFileNameWithoutExtension(archive) + ".txt"), archive.Replace(Application.StartupPath, "")); // Write relative path to the original file in Data\Obj\%obj name%\%obj name%.txt

                SevenZipExtractor mySze = new SevenZipExtractor(newPath);
                mySze.ExtractArchive(Path.GetDirectoryName(newPath)); // Extract archive to Data\Obj\%obj name%\
                mySze.Dispose();

                try
                {
                    File.Move(newPath.Replace(".gz", ".tar"), newPath.Replace(".gz", "")); // SevenZip has bug, it is writes a file as "xxx.obj.tar", while it must be "xxx.obj"
                }
                catch { }
            }
        }

        public static void ProcessTxtGz(string directoryPath)
        {
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Data", "Txt")))
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Data", "Txt"));

            SevenZipExtractor.SetLibraryPath(Path.Combine(Application.StartupPath, "7z.dll"));

            string archive = Path.Combine(directoryPath, "Txt", "utf16.txt.gz"); // We need only one file

            string newPath = Path.Combine(Application.StartupPath, "Data", "Txt", Path.GetFileNameWithoutExtension(archive), Path.GetFileName(archive)); // Data\Txt\%txt name%\%txt archive%
            Directory.CreateDirectory(Path.GetDirectoryName(newPath));
            File.Copy(archive, newPath, true);
            File.WriteAllText(Path.Combine(Application.StartupPath, "Data", "Txt", Path.GetFileNameWithoutExtension(archive), Path.GetFileNameWithoutExtension(archive) + ".txt"), archive.Replace(Application.StartupPath, "")); // Write relative path to the original file in Data\Txt\%txt name%\%txt name%.txt

            SevenZipExtractor mySze = new SevenZipExtractor(newPath);
            mySze.ExtractArchive(Path.GetDirectoryName(newPath));
            mySze.Dispose();

            try
            {
                File.Move(newPath.Replace(".gz", ".tar"), newPath.Replace(".gz", "")); // SevenZip has bug, it is writes a file as "xxx.txt.tar", while it must be "xxx.txt"
            }
            catch { }
        }

        static string mainFilePath = Path.Combine(Application.StartupPath, "Translation.json");
        static string toolsDirectory = Path.Combine(Application.StartupPath, "Data", "DatWorker", "Workspace");

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
                    myProc.StartInfo.Arguments = "-n9 " + name; // Without -n9 the game will freeze
                    myProc.StartInfo.WorkingDirectory = toolsDirectory;
                    myProc.Start();
                    myProc.WaitForExit();

                    File.Delete(Path.Combine(toolsDirectory, name));
                    
                    File.Replace(Path.Combine(toolsDirectory, name + ".gz"), Application.StartupPath + File.ReadAllText(filepath + ".txt"), null);
                }
            }
        }

        public static void RepackSeekmap(string resourcePath, string firstDirectory)
        {
            File.Move(Path.Combine(firstDirectory, "seekmap.dat"), Path.Combine(toolsDirectory, "seekmap.txt.gz")); // seekmap.dat is a .gz archive with .txt file

            Process myProc = new Process();
            myProc.StartInfo.FileName = Path.Combine(toolsDirectory, "gzip.exe");
            myProc.StartInfo.Arguments = "-d seekmap.txt.gz"; // Without -n9 the game will freeze
            myProc.StartInfo.WorkingDirectory = toolsDirectory;
            myProc.Start();
            myProc.WaitForExit();

            File.Copy(resourcePath, Path.Combine(toolsDirectory, "RES.dat")); // RES.dat and seekmap.txt required for modseekmap.exe
            myProc = new Process();
            myProc.StartInfo.FileName = Path.Combine(toolsDirectory, "modseekmap.exe");
            myProc.StartInfo.WorkingDirectory = toolsDirectory;
            myProc.Start();
            myProc.WaitForExit();
            File.Move(Path.Combine(toolsDirectory, "seekmap.new"), Path.Combine(toolsDirectory, "seekmap.dat")); // modseekmap generates seekmap.new file

            myProc = new Process(); // compress seekmap.new to seekmap.dat
            myProc.StartInfo.FileName = Path.Combine(toolsDirectory, "gzip.exe");
            myProc.StartInfo.Arguments = "-n9 seekmap.dat"; // Without -n9 the game will freeze
            myProc.StartInfo.WorkingDirectory = toolsDirectory;
            myProc.Start();
            myProc.WaitForExit();
            File.Move(Path.Combine(toolsDirectory, "seekmap.dat.gz"), Path.Combine(firstDirectory, "seekmap.dat"));

            File.Delete(Path.Combine(toolsDirectory, "RES.dat")); // Remove temp files
            File.Delete(Path.Combine(toolsDirectory, "seekmap.txt"));
        }

    }
}
