namespace ToradoraTranslateTool
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonExtractIso = new System.Windows.Forms.Button();
            this.buttonExtractGame = new System.Windows.Forms.Button();
            this.buttonTranslate = new System.Windows.Forms.Button();
            this.buttonRepackGame = new System.Windows.Forms.Button();
            this.buttonRepackIso = new System.Windows.Forms.Button();
            this.buttonExtractIsoHelp = new System.Windows.Forms.Button();
            this.buttonExtractGameHelp = new System.Windows.Forms.Button();
            this.buttonRepackGameHelp = new System.Windows.Forms.Button();
            this.buttonTranslateHelp = new System.Windows.Forms.Button();
            this.buttonRepackIsoHelp = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelWork = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timerWork = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExtractIso
            // 
            this.buttonExtractIso.Location = new System.Drawing.Point(8, 35);
            this.buttonExtractIso.Name = "buttonExtractIso";
            this.buttonExtractIso.Size = new System.Drawing.Size(115, 32);
            this.buttonExtractIso.TabIndex = 0;
            this.buttonExtractIso.Text = "Extract ISO";
            this.buttonExtractIso.UseVisualStyleBackColor = true;
            this.buttonExtractIso.Click += new System.EventHandler(this.buttonExtractIso_Click);
            // 
            // buttonExtractGame
            // 
            this.buttonExtractGame.Enabled = false;
            this.buttonExtractGame.Location = new System.Drawing.Point(8, 73);
            this.buttonExtractGame.Name = "buttonExtractGame";
            this.buttonExtractGame.Size = new System.Drawing.Size(115, 32);
            this.buttonExtractGame.TabIndex = 1;
            this.buttonExtractGame.Text = "Extract game files";
            this.buttonExtractGame.UseVisualStyleBackColor = true;
            this.buttonExtractGame.Click += new System.EventHandler(this.buttonExtractGame_Click);
            // 
            // buttonTranslate
            // 
            this.buttonTranslate.Enabled = false;
            this.buttonTranslate.Location = new System.Drawing.Point(8, 111);
            this.buttonTranslate.Name = "buttonTranslate";
            this.buttonTranslate.Size = new System.Drawing.Size(115, 32);
            this.buttonTranslate.TabIndex = 2;
            this.buttonTranslate.Text = "Translate strings";
            this.buttonTranslate.UseVisualStyleBackColor = true;
            this.buttonTranslate.Click += new System.EventHandler(this.buttonTranslate_Click);
            // 
            // buttonRepackGame
            // 
            this.buttonRepackGame.Enabled = false;
            this.buttonRepackGame.Location = new System.Drawing.Point(8, 149);
            this.buttonRepackGame.Name = "buttonRepackGame";
            this.buttonRepackGame.Size = new System.Drawing.Size(115, 32);
            this.buttonRepackGame.TabIndex = 3;
            this.buttonRepackGame.Text = "Repack game files";
            this.buttonRepackGame.UseVisualStyleBackColor = true;
            this.buttonRepackGame.Click += new System.EventHandler(this.buttonRepackGame_Click);
            // 
            // buttonRepackIso
            // 
            this.buttonRepackIso.Enabled = false;
            this.buttonRepackIso.Location = new System.Drawing.Point(8, 187);
            this.buttonRepackIso.Name = "buttonRepackIso";
            this.buttonRepackIso.Size = new System.Drawing.Size(115, 32);
            this.buttonRepackIso.TabIndex = 4;
            this.buttonRepackIso.Text = "Repack ISO";
            this.buttonRepackIso.UseVisualStyleBackColor = true;
            this.buttonRepackIso.Click += new System.EventHandler(this.buttonRepackIso_Click);
            // 
            // buttonExtractIsoHelp
            // 
            this.buttonExtractIsoHelp.Location = new System.Drawing.Point(127, 47);
            this.buttonExtractIsoHelp.Name = "buttonExtractIsoHelp";
            this.buttonExtractIsoHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonExtractIsoHelp.TabIndex = 5;
            this.buttonExtractIsoHelp.Text = "?";
            this.buttonExtractIsoHelp.UseVisualStyleBackColor = true;
            this.buttonExtractIsoHelp.Click += new System.EventHandler(this.buttonExtractIsoHelp_Click);
            // 
            // buttonExtractGameHelp
            // 
            this.buttonExtractGameHelp.Location = new System.Drawing.Point(127, 85);
            this.buttonExtractGameHelp.Name = "buttonExtractGameHelp";
            this.buttonExtractGameHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonExtractGameHelp.TabIndex = 6;
            this.buttonExtractGameHelp.Text = "?";
            this.buttonExtractGameHelp.UseVisualStyleBackColor = true;
            this.buttonExtractGameHelp.Click += new System.EventHandler(this.buttonExtractGameHelp_Click);
            // 
            // buttonRepackGameHelp
            // 
            this.buttonRepackGameHelp.Location = new System.Drawing.Point(127, 161);
            this.buttonRepackGameHelp.Name = "buttonRepackGameHelp";
            this.buttonRepackGameHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonRepackGameHelp.TabIndex = 8;
            this.buttonRepackGameHelp.Text = "?";
            this.buttonRepackGameHelp.UseVisualStyleBackColor = true;
            this.buttonRepackGameHelp.Click += new System.EventHandler(this.buttonRepackGameHelp_Click);
            // 
            // buttonTranslateHelp
            // 
            this.buttonTranslateHelp.Location = new System.Drawing.Point(127, 123);
            this.buttonTranslateHelp.Name = "buttonTranslateHelp";
            this.buttonTranslateHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonTranslateHelp.TabIndex = 7;
            this.buttonTranslateHelp.Text = "?";
            this.buttonTranslateHelp.UseVisualStyleBackColor = true;
            this.buttonTranslateHelp.Click += new System.EventHandler(this.buttonTranslateHelp_Click);
            // 
            // buttonRepackIsoHelp
            // 
            this.buttonRepackIsoHelp.Location = new System.Drawing.Point(127, 199);
            this.buttonRepackIsoHelp.Name = "buttonRepackIsoHelp";
            this.buttonRepackIsoHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonRepackIsoHelp.TabIndex = 9;
            this.buttonRepackIsoHelp.Text = "?";
            this.buttonRepackIsoHelp.UseVisualStyleBackColor = true;
            this.buttonRepackIsoHelp.Click += new System.EventHandler(this.buttonRepackIsoHelp_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(9, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(264, 227);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // labelWork
            // 
            this.labelWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWork.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWork.Location = new System.Drawing.Point(8, 2);
            this.labelWork.Name = "labelWork";
            this.labelWork.Size = new System.Drawing.Size(139, 28);
            this.labelWork.TabIndex = 11;
            this.labelWork.Text = "Ready";
            this.labelWork.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelWork);
            this.panel1.Controls.Add(this.buttonRepackIsoHelp);
            this.panel1.Controls.Add(this.buttonRepackGameHelp);
            this.panel1.Controls.Add(this.buttonTranslateHelp);
            this.panel1.Controls.Add(this.buttonExtractGameHelp);
            this.panel1.Controls.Add(this.buttonExtractIsoHelp);
            this.panel1.Controls.Add(this.buttonRepackIso);
            this.panel1.Controls.Add(this.buttonRepackGame);
            this.panel1.Controls.Add(this.buttonTranslate);
            this.panel1.Controls.Add(this.buttonExtractGame);
            this.panel1.Controls.Add(this.buttonExtractIso);
            this.panel1.Location = new System.Drawing.Point(279, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 228);
            this.panel1.TabIndex = 12;
            // 
            // timerWork
            // 
            this.timerWork.Interval = 500;
            this.timerWork.Tick += new System.EventHandler(this.timerWork_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.label1.Location = new System.Drawing.Point(10, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "1.1.0";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 239);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToradoraTranslateTool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonExtractIso;
        private System.Windows.Forms.Button buttonExtractGame;
        private System.Windows.Forms.Button buttonTranslate;
        private System.Windows.Forms.Button buttonRepackGame;
        private System.Windows.Forms.Button buttonRepackIso;
        private System.Windows.Forms.Button buttonExtractIsoHelp;
        private System.Windows.Forms.Button buttonExtractGameHelp;
        private System.Windows.Forms.Button buttonRepackGameHelp;
        private System.Windows.Forms.Button buttonTranslateHelp;
        private System.Windows.Forms.Button buttonRepackIsoHelp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelWork;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timerWork;
        private System.Windows.Forms.Label label1;
    }
}

