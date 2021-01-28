using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper
{
    public class ShowBestTimesForm : Form
    {
        public Label lbl_Beg, lbl_BegTime, lbl_BegName, lbl_Inter, lbl_InterTime, lbl_InterName, lbl_Exp, lbl_ExpTime, lbl_ExpName;
        public Button btn_Reset, btn_Ok;


        public ShowBestTimesForm()
        {
            Icon ico = new Icon("Mine.ico");
            this.Icon = ico;

            KeyPreview = true;
            KeyDown += ShowBestTimesForm_KeyDown;
            Font = new Font("Arial", 12, FontStyle.Bold);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = Program.form1.ChooseL("Fastest Mine Sweepers");
            Load += ShowBestTimesForm_Load;
            Width = 380;
            Height = 200;
            StartPosition = FormStartPosition.CenterParent;
            BackgroundImage = Properties.Resources.Medal;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void ShowBestTimesForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        btn_Ok_Click(this, null);
                        break;
                    }
                default:
                    break;
            }
        }

        private void ShowBestTimesForm_Load(object sender, EventArgs e)
        {
            List<string> lines = File.ReadAllLines("BestTimes.txt").ToList();
            string[] strBeg = lines[0].Split(' ');
            string[] strInter = lines[1].Split(' ');
            string[] strExp = lines[2].Split(' ');

            #region Initialize Board

            #region Labels
            lbl_Beg = new Label
            {
                Text = Program.form1.ChooseL("Beginner:"),
                AutoSize = true,
                Left = 15,
                Top = 20,
            };
            this.Controls.Add(lbl_Beg);

            lbl_BegTime = new Label
            {
                Text = Program.form1.ChooseL(int.Parse(strBeg[1]), "seconds"),
                AutoSize = true,
                Left = 125,
                Top = 20,
            };
            this.Controls.Add(lbl_BegTime);

            lbl_BegName = new Label
            {
                Text = Program.form1.ChooseL(strBeg[3]),
                AutoSize = true,
                Left = 245,
                Top = 20,
            };
            this.Controls.Add(lbl_BegName);

            lbl_Inter = new Label
            {
                Text = Program.form1.ChooseL("Intermediate:"),
                AutoSize = true,
                Left = 15,
                Top = 50,
            };
            this.Controls.Add(lbl_Inter);

            lbl_InterTime = new Label
            {
                Text = Program.form1.ChooseL(int.Parse(strInter[1]), "seconds"),
                AutoSize = true,
                Left = 125,
                Top = 50,
            };
            this.Controls.Add(lbl_InterTime);

            lbl_InterName = new Label
            {
                Text = Program.form1.ChooseL(strInter[3]),
                AutoSize = true,
                Left = 245,
                Top = 50,
            };
            this.Controls.Add(lbl_InterName);

            lbl_Exp = new Label
            {
                Text = Program.form1.ChooseL("Expert:"),
                AutoSize = true,
                Left = 15,
                Top = 80,
            };
            this.Controls.Add(lbl_Exp);

            lbl_ExpTime = new Label
            {
                Text = Program.form1.ChooseL(int.Parse(strExp[1]), "seconds"),
                AutoSize = true,
                Left = 125,
                Top = 80
            };
            this.Controls.Add(lbl_ExpTime);
            lbl_ExpName = new Label
            {
                Text = Program.form1.ChooseL(strExp[3]),
                AutoSize = true,
                Left = 245,
                Top = 80,
            };
            this.Controls.Add(lbl_ExpName);
            #endregion

            #region Buttons

            btn_Reset = new Button
            {
                Width = 200,
                Height = 30,
                Text = Program.form1.ChooseL("Reset Scores"),
                Top = lbl_Exp.Bottom + 25,
                Left = 30
            };
            btn_Reset.Click += btn_Reset_Click;
            this.Controls.Add(btn_Reset);

            btn_Ok = new Button
            {
                Width = 100,
                Height = 30,
                Text = Program.form1.ChooseL("OK"),
                Top = lbl_Exp.Bottom + 25,
                Left = 240
            };
            btn_Ok.Click += btn_Ok_Click;
            this.Controls.Add(btn_Ok);

            #endregion

            #endregion

            btn_Ok.Select();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            string[] lines = new string[]
            {
                "Beginner: 999 seconds Anonymous",
                "Intermediate: 999 seconds Anonymous",
                "Expert: 999 seconds Anonymous"
            };
            File.WriteAllLines("BestTimes.txt", lines);
            this.Controls.Clear();
            ShowBestTimesForm_Load(sender, e);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}