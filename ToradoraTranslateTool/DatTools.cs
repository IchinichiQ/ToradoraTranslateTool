using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToradoraTranslateTool
{
    class DatTools
    {
        public static void ExtractDat(string datPath)
        {
            string newPath = Path.Combine(Application.StartupPath, @"Data\DatWorker\", Path.GetFileName(datPath));
            File.Copy(datPath, newPath, true);
            Process myProc = new Process();
            myProc.StartInfo.FileName = Path.Combine(Application.StartupPath, @"Data\DatWorker\Dat Worker.exe");
            myProc.StartInfo.Arguments = '"' + newPath + '"'; // Add commas to ignore spaces in path
            myProc.StartInfo.WorkingDirectory = Path.Combine(Application.StartupPath, @"Data\DatWorker\");
            myProc.Start();

            myProc.WaitForExit();
        }

        public static void RepackDat(string lstPath)
        {
            Process myProc = new Process();
            myProc.StartInfo.FileName = Path.Combine(Application.StartupPath, @"Data\DatWorker\Dat Worker.exe");
            myProc.StartInfo.Arguments = '"' + lstPath + '"'; // Add commas to ignore spaces in path
            myProc.StartInfo.WorkingDirectory = Path.Combine(Application.StartupPath, @"Data\DatWorker\");
            myProc.Start();

            myProc.WaitForExit();
        }

    }
}
