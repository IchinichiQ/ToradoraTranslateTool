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
using OBJEditor;
using Newtonsoft.Json.Linq;

namespace ToradoraTranslateTool
{
    public partial class FormTranslation : Form
    {
        string currentFile;
        public string mainFilePath = Path.Combine(Application.StartupPath, "Translation.json");

        public FormTranslation()
        {
            InitializeComponent();

            if (!File.Exists(mainFilePath))
                File.WriteAllText(mainFilePath, "{ }");

            List<String> directories = new List<string>();
            directories.AddRange(Directory.GetDirectories(Path.Combine(Application.StartupPath, "Data", "Txt")).Select(Path.GetFileName)); // Get all directories with .obj and .txt files
            directories.AddRange(Directory.GetDirectories(Path.Combine(Application.StartupPath, "Data", "Obj")).Select(Path.GetFileName));

            dataGridViewFiles.Rows.Add("Total: ", "0%");

            JObject mainFile = JObject.Parse(File.ReadAllText(mainFilePath));
            foreach (string name in directories) // Adding files to the table
            {
                int translationPercent = 0;
                if (mainFile[name] != null)  // If json have saved translation
                {
                    int stringCount = mainFile[name].Children().Children().Count(); // scary
                    int translatedCount = 0;
                    for (int i = 0; i < stringCount; i++)
                    {
                        if (mainFile[name][i.ToString()].ToString() != "")
                            translatedCount++;
                    }
                    translationPercent = (int)Math.Round((double)(translatedCount * 100) / stringCount);
                }
                
                dataGridViewFiles.Rows.Add(name, translationPercent + "%");
            }
            updateTotalPercent();
        }

        private void dataGridViewFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0) // Ignore row with total translation percent
                LoadFile(dataGridViewFiles[0, e.RowIndex].Value.ToString());
        }

        private void LoadFile(string filename)
        {
            if (currentFile != null)
                SaveProgress();

            currentFile = filename;
            string[] myStrings;
            Dictionary<int, string> myNames = new Dictionary<int, string>();
            if (Path.GetExtension(currentFile) == ".obj")
            {
                string filepath = Path.Combine(Application.StartupPath, "Data", "Obj", currentFile, currentFile);
                OBJHelper myHelper = new OBJHelper(File.ReadAllBytes(filepath));
                myStrings = myHelper.Import();
                myNames = myHelper.Actors;
            }
            else // Else it is .txt file
            {
                string filepath = Path.Combine(Application.StartupPath, "Data", "Txt", currentFile, currentFile);
                myStrings = File.ReadAllLines(filepath, new UnicodeEncoding(false, false)); // Txt file has encoding UTF-16 LE (Unicode without BOM)
            }

            JObject mainFile = JObject.Parse(File.ReadAllText(mainFilePath));
            bool haveTranslation = false;
            if (mainFile[currentFile] != null)
                haveTranslation = true;

            dataGridViewStrings.Rows.Clear();
            for (int i = 0; i < myStrings.Length; i++)
            {
                string name = "";
                string sentence = "";
                string translated = "";
                if (myStrings[i].StartsWith("「") && myStrings[i].EndsWith("」"))
                {
                    name = myNames[i];
                    sentence = myStrings[i].Replace("「", "").Replace("」", ""); // Remove brackets from original sentence
                }
                else
                    sentence = myStrings[i];

                if (haveTranslation)
                    translated = mainFile[currentFile][i.ToString()].ToString().Replace("「", "").Replace("」", ""); // Remove brackets from translated sentence

                dataGridViewStrings.Rows.Add(name, sentence, translated);
            }
        }

        private void SaveProgress()
        {
            JObject mainFile = JObject.Parse(File.ReadAllText(mainFilePath));

            int translatedCount = 0;
            if (mainFile[currentFile] != null)
            {
                for (int i = 0; i < dataGridViewStrings.Rows.Count; i++) // Updating translation in json
                {
                    string translatedString = dataGridViewStrings.Rows[i].Cells[2].Value?.ToString();
                    if (translatedString != "" && translatedString != null)
                    {
                        translatedCount++;
                        if (dataGridViewStrings.Rows[i].Cells[0].Value?.ToString() != "") // If have a name, then add the necessary brackets
                            translatedString = "「" + translatedString + "」";
                    }
                    mainFile[currentFile][i.ToString()] = translatedString;
                }
            }
            else
            {
                JObject translatedStrings = new JObject(); // Creating json with all strings
                for (int i = 0; i < dataGridViewStrings.Rows.Count; i++)
                {
                    string translatedString = dataGridViewStrings.Rows[i].Cells[2].Value?.ToString();
                    if (translatedString != "")
                    {
                        translatedCount++;
                        if (dataGridViewStrings.Rows[i].Cells[1].Value?.ToString() != "")
                            translatedString = "「" + translatedString + "」";
                    }
                    translatedStrings.Add(i.ToString(), translatedString);
                }
                mainFile.Add(new JProperty(currentFile, translatedStrings));
            }

            int translationPercent = (int)Math.Round((double)(translatedCount * 100) / dataGridViewStrings.Rows.Count);
            DataGridViewRow myRow = dataGridViewFiles.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString().Equals(currentFile)).First(); // Find row by filename
            myRow.Cells[1].Value = translationPercent.ToString() + "%";
            updateTotalPercent();

            File.WriteAllText(mainFilePath, mainFile.ToString());
        }

        private void FormTranslation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentFile != null)
                SaveProgress();
        }

        private void dataGridViewStrings_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                c.DataGridView.ClearSelection();
                c.DataGridView.CurrentCell = c;
                c.Selected = true;
            }
        }

        private void itemExportStrings_Click(object sender, EventArgs e)
        {
            if (currentFile == null)
            {
                MessageBox.Show("First select the file!", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog mySaveFileDialog = new SaveFileDialog())
            {
                mySaveFileDialog.Filter = "Text file (*.txt) | *.txt";

                if (mySaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] myStrings = new string[dataGridViewStrings.Rows.Count];
                    for (int i = 0; i < myStrings.Length; i++)
                    {
                        myStrings[i] = dataGridViewStrings.Rows[i].Cells[0].Value?.ToString() + ";" + dataGridViewStrings.Rows[i].Cells[1].Value?.ToString() + ";" + dataGridViewStrings.Rows[i].Cells[2].Value?.ToString();
                    }
                    File.WriteAllLines(mySaveFileDialog.FileName, myStrings);
                }
            }
        }

        private void itemImportStrings_Click(object sender, EventArgs e)
        {
            if (currentFile == null)
            {
                MessageBox.Show("First select the file!", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (OpenFileDialog myOpenFileDialog = new OpenFileDialog())
            {
                myOpenFileDialog.Filter = "Text file (*.txt) | *.txt";

                if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] myStrings = File.ReadAllLines(myOpenFileDialog.FileName);
                    for (int i = 0; i < myStrings.Length; i++)
                    {
                        dataGridViewStrings.Rows[i].Cells[2].Value = myStrings[i];
                    }
                }
            }
        }

        private void updateTotalPercent()
        {
            int currentPercent = 0;
            for (int i = 1; i < dataGridViewFiles.Rows.Count; i++)
            {
                currentPercent += Int32.Parse(dataGridViewFiles.Rows[i].Cells[1].Value?.ToString().Replace("%",""));
            }
            int currentTotalPercent = (int)Math.Round((double)(currentPercent * 100) / ((dataGridViewFiles.Rows.Count - 1) * 100));
            dataGridViewFiles.Rows[0].Cells[1].Value = currentTotalPercent.ToString() + "%";
        }


    }
}
