namespace ToradoraTranslateTool
{
    partial class FormNames
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNames));
            this.dataGridViewNames = new System.Windows.Forms.DataGridView();
            this.ColumnOriginal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTranslated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonNamesGridHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNames)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewNames
            // 
            this.dataGridViewNames.AllowUserToAddRows = false;
            this.dataGridViewNames.AllowUserToDeleteRows = false;
            this.dataGridViewNames.AllowUserToResizeRows = false;
            this.dataGridViewNames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewNames.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewNames.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnOriginal,
            this.ColumnTranslated});
            this.dataGridViewNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewNames.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewNames.Name = "dataGridViewNames";
            this.dataGridViewNames.RowHeadersVisible = false;
            this.dataGridViewNames.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewNames.Size = new System.Drawing.Size(800, 450);
            this.dataGridViewNames.TabIndex = 2;
            // 
            // ColumnOriginal
            // 
            this.ColumnOriginal.FillWeight = 138.5787F;
            this.ColumnOriginal.HeaderText = "Original";
            this.ColumnOriginal.Name = "ColumnOriginal";
            this.ColumnOriginal.ReadOnly = true;
            this.ColumnOriginal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnTranslated
            // 
            this.ColumnTranslated.FillWeight = 138.5787F;
            this.ColumnTranslated.HeaderText = "Translated";
            this.ColumnTranslated.Name = "ColumnTranslated";
            this.ColumnTranslated.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // buttonNamesGridHelp
            // 
            this.buttonNamesGridHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNamesGridHelp.Location = new System.Drawing.Point(760, 428);
            this.buttonNamesGridHelp.Name = "buttonNamesGridHelp";
            this.buttonNamesGridHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonNamesGridHelp.TabIndex = 8;
            this.buttonNamesGridHelp.Text = "?";
            this.buttonNamesGridHelp.UseVisualStyleBackColor = true;
            this.buttonNamesGridHelp.Click += new System.EventHandler(this.buttonNamesGridHelp_Click);
            // 
            // FormNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonNamesGridHelp);
            this.Controls.Add(this.dataGridViewNames);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(250, 150);
            this.Name = "FormNames";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Names translation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNames_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNames)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOriginal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTranslated;
        private System.Windows.Forms.Button buttonNamesGridHelp;
    }
}