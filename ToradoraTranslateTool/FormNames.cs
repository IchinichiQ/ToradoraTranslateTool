using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ToradoraTranslateTool
{
    public partial class FormNames : Form
    {
        string mainFilePath = Path.Combine(Application.StartupPath, "Translation.json");

        public FormNames(List<string> originalNames)
        {
            InitializeComponent();

            JObject mainFile = JObject.Parse(File.ReadAllText(mainFilePath)); // Get a saved names translation
            foreach (string name in originalNames)
            {
                string translatedName = "";

                if (mainFile["names"] != null && mainFile["names"][name] != null) 
                    translatedName = mainFile["names"][name].ToString();

                dataGridViewNames.Rows.Add(name, translatedName);
            }

            MessageBox.Show("Do not translate the names unless you have translated the charaname.txt file, otherwise they will not appear in dialogs!", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void SaveProgress()
        {
            JObject mainFile = JObject.Parse(File.ReadAllText(mainFilePath));

            if (mainFile["names"] != null)
            {
                for (int i = 0; i < dataGridViewNames.Rows.Count; i++) // Updating translation in json
                {
                    string originalName = dataGridViewNames.Rows[i].Cells[0].Value?.ToString();
                    string translatedName = dataGridViewNames.Rows[i].Cells[1].Value?.ToString();
                    mainFile["names"][originalName] = translatedName;
                }
            }
            else
            {
                JObject translatedNames = new JObject(); // Creating json with all strings
                for (int i = 0; i < dataGridViewNames.Rows.Count; i++)
                {
                    string originalName = dataGridViewNames.Rows[i].Cells[0].Value?.ToString();
                    string translatedName = dataGridViewNames.Rows[i].Cells[1].Value?.ToString();
                    translatedNames.Add(originalName, translatedName);
                }
                mainFile.Add(new JProperty("names", translatedNames));
            }

            File.WriteAllText(mainFilePath, mainFile.ToString());
        }

        private void buttonNamesGridHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Once you translate the names here, they will be automatically replaced in each translated .obj file at the repacking stage." + Environment.NewLine +
                "Be sure to translate the names in the charaname.txt file.", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormNames_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveProgress();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
