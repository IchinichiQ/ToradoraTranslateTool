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

namespace ToradoraTranslateTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            EnableButtons();
        }

        // TODO:
        // 1. Extract ISO            ✓
        // 2. Extract .dat           ✓
        // 3. Edit .obj
        // 4. Repack .dat            ✓
        // 5. Create new seekmap
        // 6. Create ISO

        private void EnableButtons()
        {
            buttonExtractIso.Enabled = true;
            if (File.Exists(Path.Combine(Application.StartupPath, "Data", "Iso", "PSP_GAME", "USRDIR", "resource.dat"))) // If iso already extracted, enable next step button
                buttonExtractGame.Enabled = true;
        }

        private void DisableButtons()
        {
            buttonExtractIso.Enabled = false;
            buttonExtractGame.Enabled = false;
            buttonExtractGame.Enabled = false;
            buttonTranslate.Enabled = false;
            buttonRepackgame.Enabled = false;
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
