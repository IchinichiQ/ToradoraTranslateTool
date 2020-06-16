﻿namespace ToradoraTranslateTool
{
    partial class FormTranslation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTranslation));
            this.dataGridViewFiles = new System.Windows.Forms.DataGridView();
            this.ColumnFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnProgress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewStrings = new System.Windows.Forms.DataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOriginal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTranslated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonFilesGridHelp = new System.Windows.Forms.Button();
            this.buttonTextGridHelp = new System.Windows.Forms.Button();
            this.contextMenuStripStrings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemExportStrings = new System.Windows.Forms.ToolStripMenuItem();
            this.itemImportStrings = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStrings)).BeginInit();
            this.contextMenuStripStrings.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewFiles
            // 
            this.dataGridViewFiles.AllowUserToAddRows = false;
            this.dataGridViewFiles.AllowUserToDeleteRows = false;
            this.dataGridViewFiles.AllowUserToResizeColumns = false;
            this.dataGridViewFiles.AllowUserToResizeRows = false;
            this.dataGridViewFiles.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFilename,
            this.ColumnProgress});
            this.dataGridViewFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridViewFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewFiles.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFiles.Name = "dataGridViewFiles";
            this.dataGridViewFiles.ReadOnly = true;
            this.dataGridViewFiles.RowHeadersVisible = false;
            this.dataGridViewFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFiles.Size = new System.Drawing.Size(179, 561);
            this.dataGridViewFiles.TabIndex = 0;
            this.dataGridViewFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFiles_CellDoubleClick);
            // 
            // ColumnFilename
            // 
            this.ColumnFilename.HeaderText = "Name";
            this.ColumnFilename.Name = "ColumnFilename";
            this.ColumnFilename.ReadOnly = true;
            this.ColumnFilename.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnProgress
            // 
            this.ColumnProgress.HeaderText = "Progress";
            this.ColumnProgress.Name = "ColumnProgress";
            this.ColumnProgress.ReadOnly = true;
            this.ColumnProgress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnProgress.Width = 60;
            // 
            // dataGridViewStrings
            // 
            this.dataGridViewStrings.AllowUserToAddRows = false;
            this.dataGridViewStrings.AllowUserToDeleteRows = false;
            this.dataGridViewStrings.AllowUserToResizeRows = false;
            this.dataGridViewStrings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStrings.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewStrings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewStrings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStrings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnOriginal,
            this.ColumnTranslated});
            this.dataGridViewStrings.ContextMenuStrip = this.contextMenuStripStrings;
            this.dataGridViewStrings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewStrings.Location = new System.Drawing.Point(179, 0);
            this.dataGridViewStrings.Name = "dataGridViewStrings";
            this.dataGridViewStrings.RowHeadersVisible = false;
            this.dataGridViewStrings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewStrings.Size = new System.Drawing.Size(757, 561);
            this.dataGridViewStrings.TabIndex = 1;
            this.dataGridViewStrings.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewStrings_CellMouseDown);
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ColumnName.FillWeight = 22.84264F;
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnName.Width = 41;
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
            // buttonFilesGridHelp
            // 
            this.buttonFilesGridHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFilesGridHelp.Location = new System.Drawing.Point(139, 539);
            this.buttonFilesGridHelp.Name = "buttonFilesGridHelp";
            this.buttonFilesGridHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonFilesGridHelp.TabIndex = 6;
            this.buttonFilesGridHelp.Text = "?";
            this.buttonFilesGridHelp.UseVisualStyleBackColor = true;
            // 
            // buttonTextGridHelp
            // 
            this.buttonTextGridHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTextGridHelp.Location = new System.Drawing.Point(896, 539);
            this.buttonTextGridHelp.Name = "buttonTextGridHelp";
            this.buttonTextGridHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonTextGridHelp.TabIndex = 7;
            this.buttonTextGridHelp.Text = "?";
            this.buttonTextGridHelp.UseVisualStyleBackColor = true;
            // 
            // contextMenuStripStrings
            // 
            this.contextMenuStripStrings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemExportStrings,
            this.itemImportStrings});
            this.contextMenuStripStrings.Name = "contextMenuStrip1";
            this.contextMenuStripStrings.Size = new System.Drawing.Size(181, 70);
            // 
            // itemExportStrings
            // 
            this.itemExportStrings.Name = "itemExportStrings";
            this.itemExportStrings.Size = new System.Drawing.Size(180, 22);
            this.itemExportStrings.Text = "Export all strings...";
            this.itemExportStrings.Click += new System.EventHandler(this.itemExportStrings_Click);
            // 
            // itemImportStrings
            // 
            this.itemImportStrings.Name = "itemImportStrings";
            this.itemImportStrings.Size = new System.Drawing.Size(180, 22);
            this.itemImportStrings.Text = "Import strings...";
            this.itemImportStrings.Click += new System.EventHandler(this.itemImportStrings_Click);
            // 
            // FormTranslation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 561);
            this.Controls.Add(this.buttonTextGridHelp);
            this.Controls.Add(this.buttonFilesGridHelp);
            this.Controls.Add(this.dataGridViewStrings);
            this.Controls.Add(this.dataGridViewFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 200);
            this.Name = "FormTranslation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Translation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTranslation_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStrings)).EndInit();
            this.contextMenuStripStrings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFiles;
        private System.Windows.Forms.DataGridView dataGridViewStrings;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnProgress;
        private System.Windows.Forms.Button buttonFilesGridHelp;
        private System.Windows.Forms.Button buttonTextGridHelp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOriginal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTranslated;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripStrings;
        private System.Windows.Forms.ToolStripMenuItem itemExportStrings;
        private System.Windows.Forms.ToolStripMenuItem itemImportStrings;
    }
}