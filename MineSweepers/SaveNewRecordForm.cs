using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper
{
    public class SaveNewRecordForm : Form
    {
        public Label lbl_Text;
        public TextBox tb_Name;
        public Button btn_Ok;
        public string level = "0";
        private byte lvl
        {
            get { return 0; }
            set 
            {
                switch (value)
                {
                    case 0:
                        {
                            level = "beginner";
                            break;
                        }
                    case 1:
                        {
                            level = "intermediate";
                            break;
                        }
                    case 2:
                        {
                            level = "expert";
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        short time;
        private int tm
        {
            get { return time; }
            set
            {
                if (value > 999)
                    time = 999;
                else if (value < 0)
                    time = 0;
                else
                    time = (short)value;
            }
        }

        public SaveNewRecordForm(byte _lvl, short _time)
        {
            KeyPreview = true;
            KeyDown += SaveNewRecordForm_KeyDown;
            this.lvl = _lvl;
            this.tm = _time;
            FormBorderStyle = FormBorderStyle.None;
            Width = 230;
            Height = 230;
            StartPosition = FormStartPosition.CenterParent;
            this.Load += SaveNewRecordForm_Load;
            BackgroundImage = Properties.Resources.Congrats;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void SaveNewRecordForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
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

        private void SaveNewRecordForm_Load(object sender, EventArgs e)
        {
            #region Initialize Board

            lbl_Text = new Label
            {
                AutoSize = false,
                Width = 230,
                Height = 50,
                Left = 0,
                Top = 110,
                Text = Program.form1.ChooseL($"You have the fastest time\nfor {level} level.\nPlease enter your name."),
                Font = new Font("Arial", 8, FontStyle.Bold),
                BackColor = Color.Transparent
            };
            lbl_Text.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lbl_Text);

            tb_Name = new TextBox
            {
                Width = 150,
                Left = 40,
                Top = lbl_Text.Bottom + 8,
                Text = Program.form1.ChooseL("Anonymous")
            };
            tb_Name.Select();
            tb_Name.Select(0, tb_Name.Text.Length);
            this.Controls.Add(tb_Name);

            btn_Ok = new Button
            { 
                Text = Program.form1.ChooseL("OK"),
                Width = 70,
                Height = 30,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Left = 80,
                Top = tb_Name.Bottom + 5
            };
            btn_Ok.Click += btn_Ok_Click;
            this.Controls.Add(btn_Ok);

            #endregion
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            List<string> lines = File.ReadAllLines("BestTimes.txt").ToList();
            if (level == "beginner")
            {
                lines[0] = $"Beginner: {time} seconds {tb_Name.Text}";
            }
            else if (level == "intermediate")
            {
                lines[1] = $"Intermediate: {time} seconds {tb_Name.Text}";
            }
            else if (level == "expert")
            {
                lines[2] = $"Expert: {time} seconds {tb_Name.Text}";
            }
            File.WriteAllLines("BestTimes.txt", lines);
            ShowBestTimesForm show = new ShowBestTimesForm();
            show.ShowDialog();
            this.Close();
        }
    }
}