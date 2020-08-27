using System;
using System.Windows.Forms;

namespace ToradoraTranslateTool
{
    public partial class FormImport : Form
    {
        public FormImport()
        {
            InitializeComponent();
        }

        public int Column { get; set; }
        public int Cell { get; set; }
        public string Filename { get; set; }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Column = (int)inputRow.Value;
            Cell = (int)inputCell.Value;
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
