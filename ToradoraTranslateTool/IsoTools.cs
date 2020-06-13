using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DiscUtils.Iso9660;
using SevenZip;
using System.Windows.Forms;

namespace ToradoraTranslateTool
{
    class IsoTools
    {
        public static void Extract(string isoPath)
        {
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Data", "Iso")))
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Data", "Iso"));

            SevenZipExtractor.SetLibraryPath(Path.Combine(Application.StartupPath, "7z.dll"));

            SevenZipExtractor mySze = new SevenZipExtractor(isoPath);
            mySze.ExtractArchive(Path.Combine(Application.StartupPath, "Data", "Iso"));
            mySze.Dispose();
        }

        public static void Create(string isoPath)
        {
            // Too early
        }
    }
}
