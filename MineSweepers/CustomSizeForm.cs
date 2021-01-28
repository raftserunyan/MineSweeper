using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MineSweeper
{
    public class CustomSizeForm : Form
    {
        public Label lbl_Rows, lbl_Columns, lbl_Mines;
        public TextBox tb_Rows, tb_Columns, tb_Mines;
        public Button btn_Select, btn_Cancel;


        public CustomSizeForm()
        {
            Icon ico = new Icon("Mine.ico");
            this.Icon = ico;

            StartPosition = FormStartPosition.CenterParent;
            this.Text = Program.form1.ChooseL("Custom Board");
            this.Load += CustomSize_Load;
            this.KeyDown += CustomSizeForm_KeyDown;
            this.Size = new Size(245, 200);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            BackgroundImage = Properties.Resources.Bashnya;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void CustomSizeForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btn_Select_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void CustomSize_Load(object sender, EventArgs e)
        {
            #region InitializeForm

            #region TextBoxes
            tb_Rows = new TextBox()
            {
                Font = new Font("Arial", 10, FontStyle.Bold),
                Width = 100,
                Height = 20,
                Left = 115,
                Top = 14
            };
            this.Controls.Add(tb_Rows);

            tb_Columns = new TextBox()
            {
                Font = new Font("Arial", 10, FontStyle.Bold),
                Width = 100,
                Height = 20,
                Left = 115,
                Top = 45
            };
            this.Controls.Add(tb_Columns);

            tb_Mines = new TextBox()
            {
                Font = new Font("Arial", 10, FontStyle.Bold),
                Width = 100,
                Height = 20,
                Left = 115,
                Top = 76
            };
            this.Controls.Add(tb_Mines);
            #endregion

            #region Labels
            lbl_Rows = new Label()
            {
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.Transparent,
                Text = Program.form1.ChooseL("Height:"),
                Top = 17,
                Left = 0,
                AutoSize = false,
                Width = tb_Rows.Left,
                Height = 20,
            };
            lbl_Rows.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lbl_Rows);

            lbl_Columns = new Label()
            {
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.Transparent,
                Top = 48,
                Left = 0,
                Text = Program.form1.ChooseL("Width:"),
                AutoSize = false,
                Width = tb_Columns.Left,
                Height = 20,
            };
            lbl_Columns.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lbl_Columns);

            lbl_Mines = new Label()
            {
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.Transparent,
                Text = Program.form1.ChooseL("Mines:"),
                Left = 0,
                Top = 76,
                AutoSize = false,
                Width = tb_Mines.Left,
                Height = 20,
            };
            lbl_Mines.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lbl_Mines);

            #endregion

            #region Buttons

            btn_Select = new Button()
            {
                Text = Program.form1.ChooseL("OK"),
                Width = 80,
                Height = 30
            };
            btn_Select.Top = tb_Mines.Bottom + 15;
            btn_Select.Click += btn_Select_Click;
            this.Controls.Add(btn_Select);

            btn_Cancel = new Button()
            {
                Text = Program.form1.ChooseL("Cancel"),
                Width = 80,
                Height = 30
            };
            //-------A line from select button---------
            btn_Select.Left = (this.Width - (btn_Select.Width + btn_Cancel.Width + 6)) / 2 - 10;
            //-----------------------------------------
            btn_Cancel.Left = btn_Select.Right + 5;
            btn_Cancel.Top = tb_Mines.Bottom + 15;
            btn_Cancel.Click += btn_Cancel_Click;
            this.Controls.Add(btn_Cancel);
            #endregion

            #endregion

            GetParameters();
        }

        private int nn;
        private int mm;
        private int mines;

        private void btn_Select_Click(object sender, EventArgs e)
        {
            try
            {
                nn = int.Parse(tb_Rows.Text);
                mm = int.Parse(tb_Columns.Text);
                mines = int.Parse(tb_Mines.Text);
            }
            catch (Exception)
            {
                if (tb_Rows.Text == "")
                {
                    EmptyTB(ref tb_Rows);
                    return;
                }
                else if (tb_Columns.Text == "")
                {
                    EmptyTB(ref tb_Columns);
                    return;
                }
                else if (!int.TryParse(tb_Rows.Text, out nn))
                {
                    NotNumber(ref tb_Rows);
                    return;
                }
                else if (!int.TryParse(tb_Columns.Text, out mm))
                {
                    NotNumber(ref tb_Columns);
                    return;
                }
                else if (tb_Mines.Text == "" || !int.TryParse(tb_Mines.Text, out mines))
                    mines = 10;
            }
            Program.form1.tm_Time.Enabled = false;

            Program.form1.mnuBeginner.Checked = false;
            Program.form1.mnuIntermediate.Checked = false;
            Program.form1.mnuExpert.Checked = false;
            Program.form1.mnuCustom.Checked = true;

            Program.form1.n = nn;
            Program.form1.m = mm;
            Program.form1.mines = mines;

            SaveParameters();

            Program.form1.ClearBoard();
            Program.form1.Form1_Load(this, e);

            this.Close();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmptyTB(ref TextBox tb)
        {
            MessageBox.Show(Program.form1.ChooseL("Please fill at least the first two fields."), Program.form1.ChooseL("Something's wrong..."), MessageBoxButtons.OK, MessageBoxIcon.Error);
            tb.Select();
        }
        private void NotNumber(ref TextBox tb)
        {
            MessageBox.Show(Program.form1.ChooseL("       You must input a number, not a text!\n \t          Try again..."), Program.form1.ChooseL("Something's wrong..."), MessageBoxButtons.OK, MessageBoxIcon.Error);
            tb.Clear();
            tb.Select();
        }

        private void GetParameters()
        {
            string[] str = File.ReadAllText("Parameters.txt").Split(' ');
            tb_Rows.Text = str[0];
            tb_Columns.Text = str[1];
            tb_Mines.Text = str[2];
        }
        private void SaveParameters()
        {
            string s = $"{Program.form1.N} {Program.form1.M} {Program.form1.Mines} {Convert.ToByte(Program.form1.QuestionAllowed)}";
            File.WriteAllText("Parameters.txt", s);
        }
    }
}