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
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelWork = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timerWork = new System.Windows.Forms.Timer(this.components);
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
            // 
            // buttonExtractIsoHelp
            // 
            this.buttonExtractIsoHelp.Location = new System.Drawing.Point(127, 47);
            this.buttonExtractIsoHelp.Name = "buttonExtractIsoHelp";
            this.buttonExtractIsoHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonExtractIsoHelp.TabIndex = 5;
            this.buttonExtractIsoHelp.Text = "?";
            this.buttonExtractIsoHelp.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(127, 85);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(20, 20);
            this.button7.TabIndex = 6;
            this.button7.Text = "?";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(127, 161);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(20, 20);
            this.button8.TabIndex = 8;
            this.button8.Text = "?";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(127, 123);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(20, 20);
            this.button9.TabIndex = 7;
            this.button9.Text = "?";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(127, 199);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(20, 20);
            this.button10.TabIndex = 9;
            this.button10.Text = "?";
            this.button10.UseVisualStyleBackColor = true;
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
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button7);
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 239);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToradoraTranslateTool";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExtractIso;
        private System.Windows.Forms.Button buttonExtractGame;
        private System.Windows.Forms.Button buttonTranslate;
        private System.Windows.Forms.Button buttonRepackGame;
        private System.Windows.Forms.Button buttonRepackIso;
        private System.Windows.Forms.Button buttonExtractIsoHelp;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelWork;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timerWork;
    }
}

