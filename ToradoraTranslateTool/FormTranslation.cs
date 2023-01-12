using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OBJEditor;
using Newtonsoft.Json.Linq;
using System.Reflection;
using NanoXLSX;
using System.Text.RegularExpressions;

namespace ToradoraTranslateTool
{
    public partial class FormTranslation : Form
    {
        string currentFile;
        string mainFilePath = Path.Combine(Application.StartupPath, "Translation.json");

        public FormTranslation()
        {
            InitializeComponent();

            //Enable double buffering for the tables to remove lags
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, dataGridViewStrings, new object[] { true });

            typeof(DataGridView).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, dataGridViewFiles, new object[] { true });

            try
            {
                if (!File.Exists(mainFilePath))
                    File.WriteAllText(mainFilePath, "{ }");

                List<String> directories = new List<string>();
                directories.AddRange(Directory.GetDirectories(Path.Combine(Application.StartupPath, "Data", "Txt")).Select(Path.GetFileName)); // Get all directories with .obj and .txt files
                directories.AddRange(Directory.GetDirectories(Path.Combine(Application.StartupPath, "Data", "Obj")).Select(Path.GetFileName));

                dataGridViewFiles.Rows.Add("Total: ", "0%");

                JObject mainFile = JObject.Parse(File.ReadAllText(mainFilePath));
                foreach (string name in directories) // Adding files to the table
                {
                    string translationPercent = "0";
                    if (mainFile[name] != null)  // If json have saved translation
                    {
                        int stringCount = mainFile[name].Children().Children().Count(); // scary
                        int translatedCount = 0;
                        for (int i = 0; i < stringCount; i++)
                        {
                            if (mainFile[name][i.ToString()].ToString() != "")
                                translatedCount++;
                        }
                        translationPercent = Math.Round((double)(translatedCount * 100) / stringCount, 1).ToString();
                    }

                    dataGridViewFiles.Rows.Add(name, translationPercent + "%");
                }
                updateTotalPercent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > 0) // Ignore row with total translation percent
                    LoadFile(dataGridViewFiles[0, e.RowIndex].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                myNames = myHelper.actors;
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
                    sentence = myStrings[i].TrimStart('「').TrimEnd('」'); // Remove brackets from the beginning and end of the original sentence
                }
                else
                    sentence = myStrings[i];
                sentence = sentence.Replace("＿", " ");

                if (haveTranslation)
                {
                    translated = mainFile[currentFile][i.ToString()].ToString();

                    if (translated.StartsWith("「") && translated.EndsWith("」"))
                        translated = translated.TrimStart('「').TrimEnd('」'); // Remove brackets from the beginning and end of the original sentence

                    if (translated.StartsWith("（") && translated.EndsWith("）"))
                        translated = translated.TrimStart('（').TrimEnd('）'); // Remove brackets from the beginning and end of the original sentence
                }

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
                        if (dataGridViewStrings.Rows[i].Cells[1].Value.ToString().StartsWith("（") && dataGridViewStrings.Rows[i].Cells[1].Value.ToString().EndsWith("）"))
                            translatedString = "（" + translatedString + "）";
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
                        if (dataGridViewStrings.Rows[i].Cells[0].Value?.ToString() != "" && dataGridViewStrings.Rows[i].Cells[0].Value?.ToString() != null)
                            translatedString = "「" + translatedString + "」";
                    }
                    translatedStrings.Add(i.ToString(), translatedString);
                }
                mainFile.Add(new JProperty(currentFile, translatedStrings));
            }

            string translationPercent = Math.Round((double)(translatedCount * 100.0) / dataGridViewStrings.Rows.Count, 1).ToString();
            DataGridViewRow myRow = dataGridViewFiles.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString().Equals(currentFile)).First(); // Find row by filename
            myRow.Cells[1].Value = translationPercent.ToString() + "%";
            updateTotalPercent();

            File.WriteAllText(mainFilePath, mainFile.ToString());
        }

        private void FormTranslation_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (currentFile != null)
                    SaveProgress();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewStrings_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == MouseButtons.Right)
                {
                    DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                    c.DataGridView.ClearSelection();
                    c.DataGridView.CurrentCell = c;
                    c.Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Text export
        private void ExportText(string filename)
        {
            Workbook workbook = new Workbook(filename, "Sheet1");
            for (int i = 0; i < dataGridViewStrings.RowCount; i++)
            {
                workbook.CurrentWorksheet.AddNextCell(dataGridViewStrings.Rows[i].Cells[0].Value?.ToString());
                workbook.CurrentWorksheet.AddNextCell(dataGridViewStrings.Rows[i].Cells[1].Value?.ToString());
                workbook.CurrentWorksheet.AddNextCell(dataGridViewStrings.Rows[i].Cells[2].Value?.ToString());
                workbook.CurrentWorksheet.GoToNextRow();
            }
            workbook.Save();
        }

        private void itemExportStrings_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFile == null)
                {
                    MessageBox.Show("First select the file!", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SaveFileDialog mySaveFileDialog = new SaveFileDialog())
                {
                    mySaveFileDialog.Filter = "Excel sheet (*.xlsx) | *.xlsx";
                    mySaveFileDialog.FileName = Path.GetFileNameWithoutExtension(currentFile);

                    if (mySaveFileDialog.ShowDialog() == DialogResult.OK)
                        ExportText(mySaveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItemExportAll_Click(object sender, EventArgs e)
        {
            try
            {
                using (var myFolderDialog = new FolderBrowserDialog())
                {
                    if (myFolderDialog.ShowDialog() == DialogResult.OK)
                    {
                        for (int i = 1; i < dataGridViewFiles.RowCount; i++)
                        {
                            dataGridViewFiles.ClearSelection(); // Select and scroll to the current file to show progress
                            dataGridViewFiles.Rows[i].Selected = true;
                            dataGridViewFiles.CurrentCell = dataGridViewFiles.Rows[i].Cells[0];

                            LoadFile(dataGridViewFiles[0, i].Value.ToString());
                            ExportText(Path.Combine(myFolderDialog.SelectedPath, Path.GetFileNameWithoutExtension(dataGridViewFiles[0, i].Value.ToString()) + ".xlsx"));
                            Application.DoEvents();
                        }

                        MessageBox.Show("Done!", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Text import
        private void ImportText(string filename, int column, int cell)
        {
            Workbook myWorkbook = Workbook.Load(filename);
            for (int i = cell; i <= dataGridViewStrings.RowCount; i++)
            {
                string cellKey = GetColumnName(column - 1) + i.ToString();
                try
                {
                    dataGridViewStrings.Rows[i - 1].Cells[2].Value = myWorkbook.CurrentWorksheet.Cells[cellKey].Value;
                }
                catch (KeyNotFoundException ex) when (ex.HResult == -2146232969) // NanoXLSX returns KeyNotFoundException if cell is empty
                {
                    dataGridViewStrings.Rows[i - 1].Cells[2].Value = "";
                }
            }
        }

        static string GetColumnName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];
            value += letters[index % letters.Length];

            return value;
        }

        private void itemImportStrings_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFile == null)
                {
                    MessageBox.Show("First select the file!", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (OpenFileDialog myOpenFileDialog = new OpenFileDialog())
                {
                    myOpenFileDialog.Filter = "Xlsx table (*.xlsx) | *.xlsx";
                    if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FormImport myForm = new FormImport();
                        if (myForm.ShowDialog() == DialogResult.OK)
                            ImportText(myOpenFileDialog.FileName, myForm.Column, myForm.Cell);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itemImportAll_Click(object sender, EventArgs e)
        {
            try
            {
                using (var myFolderDialog = new FolderBrowserDialog())
                {
                    if (myFolderDialog.ShowDialog() == DialogResult.OK)
                    {
                        FormImport myForm = new FormImport();
                        if (myForm.ShowDialog() == DialogResult.OK)
                        {
                            for (int i = 1; i < dataGridViewFiles.RowCount; i++)
                            {
                                string objName = dataGridViewFiles[0, i].Value.ToString();
                                string xlsxFilename = Path.Combine(myFolderDialog.SelectedPath, Path.GetFileNameWithoutExtension(objName) + ".xlsx");
                                if (File.Exists(xlsxFilename))
                                {
                                    dataGridViewFiles.ClearSelection(); // Select and scroll to the current file to show progress
                                    dataGridViewFiles.Rows[i].Selected = true;
                                    dataGridViewFiles.CurrentCell = dataGridViewFiles.Rows[i].Cells[0];

                                    LoadFile(objName);
                                    ImportText(xlsxFilename, myForm.Column, myForm.Cell);
                                    Application.DoEvents();
                                }
                            }
                        }
                        myForm.Dispose();

                        MessageBox.Show("Done!", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void updateTotalPercent()
        {
            double currentPercent = 0;
            for (int i = 1; i < dataGridViewFiles.Rows.Count; i++)
            {
                currentPercent += Double.Parse(dataGridViewFiles.Rows[i].Cells[1].Value?.ToString().Replace("%", ""));
            }
            string currentTotalPercent = Math.Round((double)(currentPercent * 100.0) / ((dataGridViewFiles.Rows.Count - 1) * 100.0), 1).ToString();
            dataGridViewFiles.Rows[0].Cells[1].Value = currentTotalPercent + "%";
        }

        private List<string> GetAllNames()
        {
            List<string> uniqueNames = new List<string>();

            for (int i = 1; i < dataGridViewFiles.Rows.Count; i++)
            {
                string filename = dataGridViewFiles.Rows[i].Cells[0].Value?.ToString();
                if (Path.GetExtension(filename) != ".obj")
                    continue;

                string filepath = Path.Combine(Application.StartupPath, "Data", "Obj", filename, filename);
                OBJHelper myHelper = new OBJHelper(File.ReadAllBytes(filepath));
                Dictionary<int, string> myNames = new Dictionary<int, string>();
                myHelper.Import();
                myNames = myHelper.actors;

                for (int ii = 0; ii < myNames.Count; ii++)
                {
                    if (uniqueNames.Contains(myNames[ii]) == false)
                        uniqueNames.Add(myNames[ii]);
                }
            }

            uniqueNames.Remove(null);
            return uniqueNames;
        }

        private void itemTranslateNames_Click(object sender, EventArgs e)
        {
            FormNames myForm = new FormNames(GetAllNames());
            myForm.Show();
        }

        #region Line breaks inserting
        private void InsertLineBreaks(LineBreaksInserter inserter)
        {
            for (int i = 0; i < dataGridViewStrings.RowCount; i++)
            {
                string currentString = dataGridViewStrings.Rows[i].Cells[2].Value?.ToString();
                bool isSpeech = dataGridViewStrings.Rows[i].Cells[0].Value?.ToString() != "";

                string newString = inserter.InsertLineBreaks(currentString, isSpeech);

                dataGridViewStrings.Rows[i].Cells[2].Value = newString;
            }
        }

        private string SelectDumpedFontFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Dumped font file (*.txt) | *.txt";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileName;
                else
                    return null;
            }
        }

        private void itemLineBreaks_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFile == null)
                {
                    MessageBox.Show("First select the file!", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string dumpedFontFile = SelectDumpedFontFile();
                LineBreaksInserter inserter = new LineBreaksInserter(dumpedFontFile, 455);

                InsertLineBreaks(inserter);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itemLineBreaksAll_Click(object sender, EventArgs e)
        {
            try
            {
                string dumpedFontFile = SelectDumpedFontFile();
                LineBreaksInserter inserter = new LineBreaksInserter(dumpedFontFile, 455);

                for (int i = 1; i < dataGridViewFiles.RowCount; i++)
                {
                    string objName = dataGridViewFiles[0, i].Value.ToString();

                    dataGridViewFiles.ClearSelection(); // Select and scroll to the current file to show progress
                    dataGridViewFiles.Rows[i].Selected = true;
                    dataGridViewFiles.CurrentCell = dataGridViewFiles.Rows[i].Cells[0];

                    LoadFile(objName);

                    InsertLineBreaks(inserter);

                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Line breaks removing
        private void RemoveLineBreaks()
        {
            for (int i = 0; i < dataGridViewStrings.RowCount; i++)
                dataGridViewStrings.Rows[i].Cells[2].Value = dataGridViewStrings.Rows[i].Cells[2].Value?.ToString().Replace('＿', ' ');
        }

        private void itemRemoveLineBreaks_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFile == null)
                {
                    MessageBox.Show("First select the file!", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RemoveLineBreaks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itemRemoveLineBreaksAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < dataGridViewFiles.RowCount; i++)
                {
                    string objName = dataGridViewFiles[0, i].Value.ToString();

                    dataGridViewFiles.ClearSelection(); // Select and scroll to the current file to show progress
                    dataGridViewFiles.Rows[i].Selected = true;
                    dataGridViewFiles.CurrentCell = dataGridViewFiles.Rows[i].Cells[0];

                    LoadFile(objName);
                    RemoveLineBreaks();
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + Environment.NewLine + ex.ToString(), "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void buttonFilesGridHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This table contains 363 files with 26508 lines to be translated." + Environment.NewLine +
                "Double-click a file to load it." + Environment.NewLine +
                "You can export and import text from all files from the context menu." + Environment.NewLine +
                "To import the translated text, you need to rename your .xlsx files according to the name of the .obj file, for example, \"_0000ESS1.xlsx\".", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonTextGridHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This table contains all the sentences stored in the selected file." + Environment.NewLine +
                "All entered data will be automatically saved for later use." + Environment.NewLine +
                "The brackets \"（\" and \"）\" will be added to the translated text automatically" + Environment.NewLine +
                "You can export all rows to a .xlsx file from the context menu." + Environment.NewLine +
                "You can also import the finished translation from .xlsx tables into the program." + Environment.NewLine +
                "Learn more at: https://github.com/12135555/ToradoraTranslateTool", "ToradoraTranslateTool", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
