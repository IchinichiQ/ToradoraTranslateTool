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
using System.Threading;
using System.Diagnostics;

namespace ToradoraTranslateTool
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            EnableButtons();
        }

        // TODO:
        // 1. Extract ISO            ✓
        // 2. Extract .dat           ✓
        // 3. Edit .obj              ✓
        // 4. Repack .dat            ✓
        // 5. Create new seekmap     ✓
        // 6. Create new ISO         ✘ (Using UMDGen)

        private void EnableButtons()
        {
            buttonExtractIso.Enabled = true;
            if (File.Exists(Path.Combine(Application.StartupPath, "Data", "Iso", "PSP_GAME", "USRDIR", "resource.dat"))) // If iso already extracted, enable available steps
            {
                buttonExtractGame.Enabled = true;
            }
            if (File.Exists(Path.Combine(Application.StartupPath, "Data", "Txt", "utf16.txt", "utf16.txt")))
            {
                buttonTranslate.Enabled = true;
                buttonRepackGame.Enabled = true;
                buttonRepackIso.Enabled = true;
            }
        }

        private void DisableButtons()
        {
            buttonExtractIso.Enabled = false;
            buttonExtractGame.Enabled = false;
            buttonExtractGame.Enabled = false;
            buttonTranslate.Enabled = false;
            buttonRepackGame.Enabled = false;
            buttonRepackIso.Enabled = false;
        }

        private async void buttonExtractIso_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Toradora ISO (*.iso) | *.iso";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ChangeStatus(true);
                    DisableButtons();

                    await Task.Run(() => IsoTools.Extract(openFileDialog.FileName));

                    ChangeStatus(false);
                    EnableButtons();
                }
            }
        }

        private async void buttonExtractGame_Click(object sender, EventArgs e)
        {
            ChangeStatus(true);
            DisableButtons();

            await Task.Run(() => DatTools.ExtractDat(Path.Combine(Application.StartupPath, "Data", "Iso", "PSP_GAME", "USRDIR", "resource.dat")));
            await Task.Run(() => DatTools.ExtractDat(Path.Combine(Application.StartupPath, "Data", "Iso", "PSP_GAME", "USRDIR", "first.dat")));
            await Task.Run(() => ObjTools.ProcessObjGz(Path.Combine(Application.StartupPath, "Data", "DatWorker", "resource")));
            await Task.Run(() => ObjTools.ProcessTxtGz(Path.Combine(Application.StartupPath, "Data", "DatWorker", "first")));
            await Task.Run(() => ObjTools.ProcessSeekmap(Path.Combine(Application.StartupPath, "Data", "DatWorker", "first")));

            ChangeStatus(false);
            EnableButtons();
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            FormTranslation myForm = new FormTranslation();
            myForm.Show();        
        }

        private async void buttonRepackGame_Click(object sender, EventArgs e)
        {
            ChangeStatus(true);
            DisableButtons();

            await Task.Run(() => ObjTools.RepackObj());
            await Task.Run(() => ObjTools.RepackTxt());
            await Task.Run(() => DatTools.RepackDat(Path.Combine(Application.StartupPath, "Data", "DatWorker", "resource.dat-LstOrder.lst")));
            await Task.Run(() => ObjTools.RepackSeekmap(Path.Combine(Application.StartupPath, "Data", "DatWorker", "resource.dat"), Path.Combine(Application.StartupPath, "Data", "DatWorker", "first")));
            await Task.Run(() => DatTools.RepackDat(Path.Combine(Application.StartupPath, "Data", "DatWorker", "first.dat-LstOrder.lst")));
            await Task.Run(() => File.Copy(Path.Combine(Application.StartupPath, "Data", "DatWorker", "resource.dat"), Path.Combine(Application.StartupPath, "Data", "Iso", "PSP_GAME", "USRDIR", "resource.dat"), true));
            await Task.Run(() => File.Copy(Path.Combine(Application.StartupPath, "Data", "DatWorker", "first.dat"), Path.Combine(Application.StartupPath, "Data", "Iso", "PSP_GAME", "USRDIR", "first.dat"), true));

            ChangeStatus(false);
            EnableButtons();
        }

        private void buttonRepackIso_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(Application.StartupPath, "UMDGen.exe"));
        }

        private void ChangeStatus(bool isWorking)
        {
            if (isWorking)
            {
                labelWork.Text = "Working";
                timerWork.Enabled = true;
            }
            else
            {
                timerWork.Enabled = false;
                labelWork.Text = "Ready";
            }
        }

        private void timerWork_Tick(object sender, EventArgs e)
        {
            if (labelWork.Text != "Working...")
                labelWork.Text += ".";
            else
                labelWork.Text = "Working";        
        }

    }
}
