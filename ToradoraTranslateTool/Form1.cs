using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ToradoraTranslateTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Data", "Iso")))
               Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Data", "Iso"));
          
        }

        // TODO:
        // 1. Extract ISO
        // 2. Extract .dat
        // 3. Edit .obj
        // 4. Create new seekmap
        // 5. Create new .dat
        // 6. Create ISO

        private void button1_Click(object sender, EventArgs e)
        {
            IsoTools.Extract(@"C:\Users\Павел\source\repos\ToradoraTranslateTool\ToradoraTranslateTool\bin\Debug\Toradora Portable! (English v1.10).iso");
            MessageBox.Show("Done!");
        }
    }
}
