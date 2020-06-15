using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

            string archive = Path.Combine(directoryPath, "text", "utf16.txt.gz"); // We need only one files

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

    }
}
