using System.IO;
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
    }
}
