using System.Windows.Forms;

namespace MineSweeper
{
    partial class Form1
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
        private System.Windows.Forms.MenuStrip mnuMine;

        private System.Windows.Forms.ToolStripMenuItem mnuGame;
        public System.Windows.Forms.ToolStripMenuItem mnuNew;
        public System.Windows.Forms.ToolStripMenuItem mnuBeginner;
        public System.Windows.Forms.ToolStripMenuItem mnuIntermediate;
        public System.Windows.Forms.ToolStripMenuItem mnuExpert;
        public System.Windows.Forms.ToolStripMenuItem mnuCustom;
        public System.Windows.Forms.ToolStripMenuItem mnuQuestion;
        public System.Windows.Forms.ToolStripMenuItem mnuRecord;
        public System.Windows.Forms.ToolStripMenuItem mnuExit;

        public System.Windows.Forms.ToolStripMenuItem mnuLanguage;
        public System.Windows.Forms.ToolStripMenuItem mnuEnglish;
        public System.Windows.Forms.ToolStripMenuItem mnuArmenian;

        public System.Windows.Forms.ToolStripMenuItem mnuInfo;
        public System.Windows.Forms.ToolStripMenuItem mnuAboutUs;

        private void InitializeComponent()
        {
            this.mnuMine = new System.Windows.Forms.MenuStrip();

            this.mnuGame = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBeginner = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIntermediate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExpert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuestion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();

            this.mnuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArmenian = new System.Windows.Forms.ToolStripMenuItem();

            this.mnuInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAboutUs = new System.Windows.Forms.ToolStripMenuItem();

            this.mnuMine.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMine
            // 
            this.mnuMine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGame,
            this.mnuLanguage,
            this.mnuInfo
            });
            this.mnuMine.Location = new System.Drawing.Point(0, 0);
            this.mnuMine.Name = "mnuMine";
            this.mnuMine.Size = new System.Drawing.Size(198, 24);
            this.mnuMine.TabIndex = 0;
            this.mnuMine.Text = this.ChooseL("menuStrip1");
            // 
            // mnuGame
            // 
            this.mnuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            new ToolStripSeparator(),
            this.mnuBeginner,
            this.mnuIntermediate,
            this.mnuExpert,
            this.mnuCustom,
            new ToolStripSeparator(),
            this.mnuQuestion,
            new ToolStripSeparator(),
            this.mnuRecord,
            new ToolStripSeparator(),
            this.mnuExit
            });
            this.mnuGame.Name = "mnuGame";
            this.mnuGame.Size = new System.Drawing.Size(50, 20);
            this.mnuGame.Text = this.ChooseL("Game");
            // 
            // mnuNew
            // 
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(141, 22);
            this.mnuNew.Text = this.ChooseL("New");
            this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // mnuBeginner
            // 
            this.mnuBeginner.Name = "mnuBeginner";
            this.mnuBeginner.Size = new System.Drawing.Size(141, 22);
            this.mnuBeginner.Text = this.ChooseL("Beginner");
            this.mnuBeginner.Click += new System.EventHandler(this.mnuDifficulty_Click);
            // 
            // mnuIntermediate
            // 
            this.mnuIntermediate.Name = "mnuIntermediate";
            this.mnuIntermediate.Size = new System.Drawing.Size(141, 22);
            this.mnuIntermediate.Text = this.ChooseL("Intermediate");
            this.mnuIntermediate.Click += new System.EventHandler(this.mnuDifficulty_Click);
            // 
            // mnuExpert
            // 
            this.mnuExpert.Name = "mnuExpert";
            this.mnuExpert.Size = new System.Drawing.Size(141, 22);
            this.mnuExpert.Text = this.ChooseL("Expert");
            this.mnuExpert.Click += new System.EventHandler(this.mnuDifficulty_Click);
            // 
            // mnuCustom
            // 
            this.mnuCustom.Name = "mnuCustom";
            this.mnuCustom.Size = new System.Drawing.Size(141, 22);
            this.mnuCustom.Text = this.ChooseL("Custom...");
            this.mnuCustom.Click += new System.EventHandler(this.mnuDifficulty_Click);
            // 
            // mnuQuestion
            // 
            this.mnuQuestion.Name = "mnuQuestion";
            this.mnuQuestion.Size = new System.Drawing.Size(141, 22);
            this.mnuQuestion.Text = this.ChooseL("Question mark(?)");
            this.mnuQuestion.Click += new System.EventHandler(this.mnuQuestion_Click);
            // 
            // mnuRecord
            // 
            this.mnuRecord.Name = "mnuRecord";
            this.mnuRecord.Size = new System.Drawing.Size(141, 22);
            this.mnuRecord.Text = this.ChooseL("Best times...");
            this.mnuRecord.Click += new System.EventHandler(this.mnuRecord_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(141, 22);
            this.mnuExit.Text = this.ChooseL("Exit");
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuLanguage
            // 
            this.mnuLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuEnglish,
                this.mnuArmenian
            });
            this.mnuLanguage.Name = "mnuLanguage";
            this.mnuLanguage.Size = new System.Drawing.Size(50, 20);
            this.mnuLanguage.Text = this.ChooseL("Language");
            // 
            // mnuEnglish
            // 
            this.mnuEnglish.Name = "mnuEnglish";
            this.mnuEnglish.Size = new System.Drawing.Size(141, 22);
            this.mnuEnglish.Text = "English";
            this.mnuEnglish.Click += new System.EventHandler(this.mnuEnglish_Click);
            // 
            // mnuArmenian
            // 
            this.mnuArmenian.Name = "mnuArmenian";
            this.mnuArmenian.Size = new System.Drawing.Size(141, 22);
            this.mnuArmenian.Text = "Հայերեն";
            this.mnuArmenian.Click += new System.EventHandler(this.mnuArmenian_Click);
            // 
            // mnuInfo
            // 
            this.mnuInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuAboutUs
            });
            this.mnuInfo.Name = "mnuInfo";
            this.mnuInfo.Size = new System.Drawing.Size(50, 20);
            this.mnuInfo.Text = this.ChooseL("Info");
            // 
            // mnuAboutUs
            // 
            this.mnuAboutUs.Name = "mnuAboutUs";
            this.mnuAboutUs.Size = new System.Drawing.Size(141, 22);
            this.mnuAboutUs.Text = ChooseL("About Game");
            this.mnuAboutUs.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 248);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.mnuMine;
            this.Name = "Form1";
            this.Text = this.ChooseL("Mine Sweeper");
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.mnuMine.ResumeLayout(false);
            this.mnuMine.PerformLayout();
            this.ResumeLayout(false);

    }
    #endregion
}
}

