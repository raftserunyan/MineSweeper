using System;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows.Forms;
                                //1388+lines
namespace MineSweeper
{
    public partial class Form1 : Form
    {
        RafButton[,] Board;
        GroupBox gb_Cells, gb_Info;
        Button btn_Face;
        Label lbl_Mines, lbl_Membrane, lbl_Time;
        public byte N;
        public int n
        {
            get { return N; }
            set
            {
                if (value > 24)
                    N = 24;
                else if (value < 9)
                    N = 9;
                else
                    N = (byte)value;
            }
        }
        public byte M;
        public int m
        {
            get { return M; }
            set
            {
                if (value > 30)
                    M = 30;
                else if (value < 9)
                    M = 9;
                else
                    M = (byte)value;
            }
        }
        public short Mines;
        public int mines
        {
            get { return Mines; }
            set
            {
                short tiv = (short)(((N * M) - (N + M)) + 1);
                if (value > tiv)
                    Mines = tiv;
                else if (value < 10)
                    Mines = 10;
                else
                    Mines = (short)value;
            }
        }
        private bool isLost = false;
        private bool isWon = false;
        private short pressedButtons = 0;
        public bool QuestionAllowed = true;
        public byte Language;
        public CustomSizeForm custom;
        public ShowBestTimesForm show;
        public SaveNewRecordForm save;
        public System.Timers.Timer tm_Time;// tm_WPP;
        private bool isFirstTime;// isFirstLoad = true;
        //private PictureBox pb_Raf;

        public Form1()
        {
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon ico = new Icon("Mine.ico");
            this.Icon = ico;
            this.Language = byte.Parse(File.ReadAllText("Language.txt"));

            InitializeComponent();
            switch (Language)
            {
                case 1:
                    mnuEnglish.Checked = true;
                    break;
                case 2:
                    mnuArmenian.Checked = true;
                    break;
            }
            StartPosition = FormStartPosition.CenterScreen;
            Load += Form1_Load;
            MouseDown += Form1_MouseDown;
            MouseUp += Form1_MouseUp;
            MaximizeBox = false;
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            pressedButtons = 0;
            isFirstTime = true;
            GetParameters();
            InitializeBoard();
            isLost = false;
            isWon = false;
            this.Width = gb_Cells.Right + 25;
            this.Height = gb_Cells.Bottom + 45;
            /*
            if (isFirstLoad)
            {
                CreatePb();
                tm_WPP.Enabled = true;
                isFirstLoad = false;
            }
            */
        }

        private void InitializeBoard()
        {
            this.Controls.Add(this.mnuMine);

            #region gb_Cells
            gb_Cells = new GroupBox
            {
                Width = M * 20 + 10,
                Height = N * 20 + 15,
                Text = "",
                Left = 4,
                Top = 65
            };
            gb_Cells.MouseDown += Form1_MouseDown;
            gb_Cells.MouseUp += Form1_MouseUp;
            this.Controls.Add(gb_Cells);
            gb_Cells.BringToFront();

            Board = new RafButton[N, M];
            for (byte i = 0; i < N; i++)
            {
                for (byte j = 0; j < M; j++)
                {
                    Board[i, j] = new RafButton
                    {
                        Width = 20,
                        Height = 20,
                        Left = j * 20 + 5,
                        Top = i * 20 + 10,
                        FlatStyle = FlatStyle.Flat,
                        BackgroundImage = (Image)(Properties.Resources.tile),
                        BackgroundImageLayout = ImageLayout.Stretch,
                    };
                    Board[i, j].I = i;
                    Board[i, j].J = j;
                    Board[i, j].MouseDown += Button_Click;
                    Board[i, j].MouseDown += Form1_MouseDown;
                    Board[i, j].MouseUp += Form1_MouseUp;
                    gb_Cells.Controls.Add(Board[i, j]);
                    Board[i, j].FlatAppearance.BorderColor = Color.LightGray;
                }
            }
            #endregion

            #region gb_Info

            gb_Info = new GroupBox
            {
                Width = gb_Cells.Width,
                Height = 45,
                Left = 4,
                Top = 20,
            };
            gb_Info.MouseDown += Form1_MouseDown;
            gb_Info.MouseUp += Form1_MouseUp;
            this.Controls.Add(gb_Info);
            gb_Info.Select();
            btn_Face = new Button
            {
                Height = 30,
                Width = 30,
                Left = (gb_Info.Width - 30) / 2,
                Top = 10,
                FlatStyle = FlatStyle.Flat,
                BackgroundImage = Properties.Resources.Smile,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            btn_Face.Click += btn_Face_Click;
            gb_Info.Controls.Add(btn_Face);

            lbl_Mines = new Label
            {
                AutoSize = false,
                Font = new Font("Arial", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                BackColor = Color.Black,
                Height = 30,
                Width = 60,
                Top = 10,
                Left = btn_Face.Left / 2 - 30
            };
            gb_Info.Controls.Add(lbl_Mines);

            lbl_Time = new Label
            {
                AutoSize = false,
                Font = new Font("Arial", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                BackColor = Color.Black,
                Height = 30,
                Width = 60,
                Top = 10,
                Left = btn_Face.Right + (gb_Info.Right - btn_Face.Right) / 2 - 30,
                Text = "0"
            };
            gb_Info.Controls.Add(lbl_Time);

            #endregion

            #region Planting Mines

            Random rnd = new Random();
            byte rowss;
            byte columnss;
            short mine = 0;

            while (mine != Mines)
            {
                rowss = (byte)rnd.Next(0, N);
                columnss = (byte)rnd.Next(0, M);

                if (rowss + columnss == 0)
                    rowss++;

                if (!Board[rowss, columnss].IsBomb)
                {
                    Board[rowss, columnss].IsBomb = true;
                    mine++;
                }
            }

            lbl_Mines.Text = Mines.ToString();

            #endregion

            #region Timer

            tm_Time = new System.Timers.Timer(1000);
            tm_Time.Elapsed += tm_Time_Tick;

            #endregion

            #region Membrane

            lbl_Membrane = new Label
            {
                AutoSize = false,
                Left = 0,
                Top = 25,
                BackColor = Color.Transparent,
                Text = "",
                Width = gb_Cells.Width + 5,
                Height = gb_Cells.Bottom - 25,
            };
            this.Controls.Add(lbl_Membrane);
            lbl_Membrane.MouseDown += Form1_MouseDown;
            lbl_Membrane.MouseUp += Form1_MouseUp;
            #endregion  
        }

        private void tm_Time_Tick(object sender, ElapsedEventArgs e)
        {
            lbl_Time.Text = (int.Parse(lbl_Time.Text) + 1).ToString();
        }

        private void btn_Face_Click(object sender, EventArgs e)
        {
            tm_Time.Enabled = false;
            ClearBoard();
            Form1_Load(sender, e);
        }

        private void mnuDifficulty_Click(object sender, EventArgs e)
        {
            if (sender == mnuBeginner)
            {
                if (!mnuBeginner.Checked)
                {
                    tm_Time.Enabled = false;
                    mnuIntermediate.Checked = false;
                    mnuExpert.Checked = false;
                    mnuCustom.Checked = false;
                    mnuBeginner.Checked = true;
                    this.N = 9;
                    this.M = 9;
                    this.Mines = 10;
                    SaveParameters();
                    ClearBoard();

                    Form1_Load(this, e);
                }
            }
            else if (sender == mnuIntermediate)
            {
                if (!mnuIntermediate.Checked)
                {
                    tm_Time.Enabled = false;
                    mnuBeginner.Checked = false;
                    mnuExpert.Checked = false;
                    mnuCustom.Checked = false;
                    mnuIntermediate.Checked = true;
                    this.N = 16;
                    this.M = 16;
                    this.Mines = 40;
                    SaveParameters();
                    ClearBoard();

                    Form1_Load(this, e);
                }
            }
            else if (sender == mnuExpert)
            {
                if (!mnuExpert.Checked)
                {
                    tm_Time.Enabled = false;
                    mnuBeginner.Checked = false;
                    mnuIntermediate.Checked = false;
                    mnuCustom.Checked = false;
                    mnuExpert.Checked = true;
                    this.N = 16;
                    this.M = 30;
                    this.Mines = 99;
                    SaveParameters();
                    ClearBoard();

                    Form1_Load(this, e);
                }
            }
            else if (sender == mnuCustom)
            {
                custom = new CustomSizeForm();
                custom.ShowDialog();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isLost && !isWon)
                btn_Face.BackgroundImage = Properties.Resources.Oo;
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isLost)
                btn_Face.BackgroundImage = Properties.Resources.Smile;
            if (isWon)
                btn_Face.BackgroundImage = Properties.Resources.Glasses;
        }

        public void ClearBoard()
        {
            this.Controls.Clear();
            Array.Clear(Board, 0, Board.Length);
        }

        private void Button_Click(object sender, MouseEventArgs e)
        {
            RafButton btn = sender as RafButton;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (!btn.LeftClickDisabled)
                        {
                            if (isFirstTime)
                            {
                                tm_Time.Enabled = true;
                                isFirstTime = false;
                            }
                            //If lost
                            try
                            {
                                if (btn.IsBomb)
                                    YouLost(btn);
                            }
                            catch { return; }

                            if (!btn.IsVisited)
                            {
                                byte num = GetNumber(btn);
                                btn.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject($"_{num}");
                                pressedButtons++;
                                btn.IsVisited = true;

                                if (num == 0)
                                    OpenEmptySpace(btn, e);

                                //If Won
                                if (Board.Length - (int)pressedButtons <= (int)Mines)
                                    YouWon();
                            }
                        }
                        break;
                    }
                case MouseButtons.Right:
                    {
                        if (btn.IsFlag)
                        {
                            btn.LeftClickDisabled = false;
                            btn.IsFlag = false;
                            if (QuestionAllowed)
                            {
                                btn.IsQuestion = true;
                                btn.BackgroundImage = Properties.Resources.Question;
                            }
                            else
                                btn.BackgroundImage = Properties.Resources.tile;
                            lbl_Mines.Text = (int.Parse(lbl_Mines.Text) + 1).ToString();
                        }
                        else if (btn.IsQuestion)
                        {
                            btn.IsQuestion = false;
                            btn.BackgroundImage = Properties.Resources.tile;
                        }
                        else if (!btn.IsVisited)
                        {
                            btn.LeftClickDisabled = true;
                            btn.BackgroundImage = Properties.Resources.Flag2;
                            btn.IsFlag = true;
                            lbl_Mines.Text = (int.Parse(lbl_Mines.Text) - 1).ToString();
                        }
                        break;
                    }
                default: break;
            }
        }

        private byte GetNumber(RafButton btn)
        {
            byte num = 0;
            for (int i = btn.I - 1; i < btn.I + 2; i++)
            {
                if (i < 0)
                    continue;
                else if (i == this.N)
                    break;

                for (int j = btn.J - 1; j < btn.J + 2; j++)
                {
                    if (j < 0)
                        continue;
                    else if (j == this.M)
                        break;

                    if (Board[i, j].IsBomb)
                        num++;
                }
            }
            return num;
        }

        private void YouLost(RafButton btn)
        {
            tm_Time.Enabled = false;
            isLost = true;
            for (byte i = 0; i < this.N; i++)
            {
                for (byte j = 0; j < this.M; j++)
                {
                    if (Board[i, j].IsBomb && !Board[i, j].IsFlag)
                        Board[i, j].BackgroundImage = Properties.Resources.Bomb;
                    else if (Board[i, j].IsFlag && !Board[i, j].IsBomb)
                        Board[i, j].BackgroundImage = Properties.Resources.FakeFlag;
                    Board[i, j].Enabled = false;
                }
            }
            btn.BackgroundImage = Properties.Resources.RedBomb;
            btn_Face.BackgroundImage = Properties.Resources.Lost;
            throw new Exception();
        }
        private void YouWon()
        {
            tm_Time.Enabled = false;
            isWon = true;
            for (byte i = 0; i < this.N; i++)
            {
                for (byte j = 0; j < this.M; j++)
                {
                    if (Board[i, j].IsBomb)
                        Board[i, j].BackgroundImage = Properties.Resources.Flag2;
                    Board[i, j].Enabled = false;
                }
            }
            btn_Face.BackgroundImage = Properties.Resources.Glasses;

            byte difficulty = (byte)(mnuBeginner.Checked ? 0 : (mnuIntermediate.Checked ? 1 : (mnuExpert.Checked ? 2 : 3)));
            if (difficulty != 3)
            {
                short bestTime = short.Parse((File.ReadAllLines("BestTimes.txt"))[difficulty].Split(' ')[1]);
                short thisTime = short.Parse(lbl_Time.Text);
                if (thisTime < bestTime)
                {
                    save = new SaveNewRecordForm(difficulty, thisTime);
                    save.ShowDialog();
                }
            }
        }

        private void OpenEmptySpace(RafButton btn, MouseEventArgs e)
        {
            for (int i = btn.I - 1; i < btn.I + 2; i++)
            {
                if (i < 0)
                    continue;
                else if (i == this.N)
                    break;

                for (int j = btn.J - 1; j < btn.J + 2; j++)
                {
                    if (j < 0)
                        continue;
                    else if (j == this.M)
                        break;

                    Button_Click(Board[i, j], e);
                }
            }
        }

        private void GetParameters()
        {
            string[] str = File.ReadAllText("Parameters.txt").Split(' ');
            this.n = int.Parse(str[0]);
            this.m = int.Parse(str[1]);
            this.mines = int.Parse(str[2]);
            byte q = byte.Parse(str[3]);

            if (this.N == 9 && this.M == 9 && this.Mines == 10)
            {
                mnuBeginner.Checked = true;
                mnuCustom.Checked = false;
            }
            else if (this.N == 16 && this.M == 16 && this.Mines == 40)
            {
                mnuIntermediate.Checked = true;
                mnuCustom.Checked = false;
            }
            else if (this.N == 16 && this.M == 30 && this.Mines == 99)
            {
                mnuExpert.Checked = true;
                mnuCustom.Checked = false;
            }
            else
                mnuCustom.Checked = true;

            if (q == 1)
            {
                QuestionAllowed = true;
                mnuQuestion.Checked = true;
            }
            else
            {
                QuestionAllowed = false;
                mnuQuestion.Checked = false; ;
            }
        }
        private void SaveParameters()
        {
            string s = $"{this.N} {this.M} {this.Mines} {Convert.ToByte(this.QuestionAllowed)}";
            File.WriteAllText("Parameters.txt", s);
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            ClearBoard();
            Form1_Load(this, e);
        }
        private void mnuQuestion_Click(object sender, EventArgs e)
        {
            string line = File.ReadAllText("Parameters.txt");
            string[] str = line.Split(' ');

            if (!QuestionAllowed)
            {
                mnuQuestion.Checked = true;
                QuestionAllowed = true;
                str[3] = "1";
            }
            else
            {
                mnuQuestion.Checked = false;
                QuestionAllowed = false;
                str[3] = "0";
            }
            line = $"{str[0]} {str[1]} {str[2]} {str[3]}";
            File.WriteAllText("Parameters.txt", line);
        }
        private void mnuRecord_Click(object sender, EventArgs e)
        {
            show = new ShowBestTimesForm();
            show.ShowDialog();
        }
        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                ChooseL("Creator: Raf Tserunyan\nInstagram: @raftserunyan\nIf you have any questions or suggestions - text me!\nBut please don't ask me about how to play this game :D"),
                ChooseL("About Game"),
                MessageBoxButtons.OK
                );
        }

        private void mnuEnglish_Click(object sender, EventArgs e)
        {
            if (!mnuEnglish.Checked)
            {
                mnuArmenian.Checked = false;
                mnuEnglish.Checked = true;
                Language = 1;
                File.WriteAllText("Language.txt", "1");

                this.Text = "Mine Sweeper";

                mnuGame.Text = "Game";
                mnuLanguage.Text = "Language";
                mnuNew.Text = "New";
                mnuBeginner.Text = "Beginner";
                mnuIntermediate.Text = "Intermediate";
                mnuExpert.Text = "Expert";
                mnuCustom.Text = "Custom...";
                mnuQuestion.Text = "Question mark(?)";
                mnuRecord.Text = "Best times...";
                mnuExit.Text = "Exit";
                mnuInfo.Text = "Info";
                mnuAboutUs.Text = "About Game";
            }
        }
        private void mnuArmenian_Click(object sender, EventArgs e)
        {
            if (!mnuArmenian.Checked)
            {
                mnuArmenian.Checked = true;
                mnuEnglish.Checked = false;
                Language = 2;
                File.WriteAllText("Language.txt", "2");

                this.Text = "Mine Sweeper";

                mnuGame.Text = "Խաղ";
                mnuLanguage.Text = "Լեզու";
                mnuNew.Text = "Նոր";
                mnuBeginner.Text = "Սկսնակ";
                mnuIntermediate.Text = "Միջակ";
                mnuExpert.Text = "Փորձագետ";
                mnuCustom.Text = "Անհատական...";
                mnuQuestion.Text = "Հարցական նշան(?)";
                mnuRecord.Text = "Լավագույն արդյունքները";
                mnuExit.Text = "Դուրս գալ";
                mnuInfo.Text = "Ինֆո";
                mnuAboutUs.Text = "Խաղի մասին";
            }
        }

        public string ChooseL(string str)
        {
            if (this.Language == 2)
            {
                switch (str)
                {
                    case "Mine Sweeper":
                        return "Սակրավոր";
                    case "Game":
                        return "Խաղ";
                    case "Language":
                        return "Լեզու";
                    case "New":
                        return "Նոր";
                    case "Beginner":
                        return "Սկսնակ";
                    case "Intermediate":
                        return "Միջակ";
                    case "Expert":
                        return "Փորձագետ";
                    case "Custom...":
                        return "Անհատական...";
                    case "Question mark(?)":
                        return "Հարցական նշան(?)";
                    case "Best times...":
                        return "Լավագույն արդյունքները";
                    case "Exit":
                        return "Դուրս գալ";
                    case "Custom Board":
                        return "Անհատական խաղադաշտ";
                    case "Height:":
                        return "Բարձրություն";
                    case "Width:":
                        return "Լայնություն";
                    case "Mines:":
                        return "Ականներ";
                    case "OK":
                        return "Ընդունել";
                    case "Cancel":
                        return "Չեղարկել";
                    case "Fastest Mine Sweepers":
                        return "Լավագույն սակրավորները";
                    case "Beginner:":
                        return "Սկսնակ՝";
                    case "Intermediate:":
                        return "Միջակ՝";
                    case "Expert:":
                        return "Փորձագետ՝";
                    case "Reset Scores":
                        return "Ջնջել արդյունքները";
                    case "Anonymous":
                        return "Անանուն";
                    case "You have the fastest time\nfor beginner level.\nPlease enter your name.":
                        return "Դուք գրանցեցիք լավագույն արդյունք\nսկսնակ մակարդակում։\nԽնդրում ենք ներմուծել ձեր անունը։";
                    case "You have the fastest time\nfor intermediate level.\nPlease enter your name.":
                        return "Դուք գրանցեցիք լավագույն արդյունք\nմիջակ մակարդակում։\nԽնդրում ենք ներմուծել ձեր անունը։";
                    case "You have the fastest time\nfor expert level.\nPlease enter your name.":
                        return "Դուք գրանցեցիք լավագույն արդյունք\nփորձագետ մակարդակում։\nԽնդրում ենք ներմուծել ձեր անունը։";
                    case "Creator: Raf Tserunyan\nInstagram: @raftserunyan\nIf you have any questions or suggestions - text me!\nBut please don't ask me about how to play this game :D":
                        return "Ստեղծող: Ռաֆ Ծերունյան\nInstagram: @raftserunyan\nԵթե ունեք հարցեր կամ առաջարկներ՝ գրեք ինձ։\nԽնդրում եմ չհարցնել թե ինչպես պետք է խաղալ այս խաղը :D";
                    case "About Game":
                        return "Խաղի մասին";
                    case "Info":
                        return "Ինֆո";
                    case "Please fill at least the first two fields.":
                        return "Խնդրում ենք լրացնել առնվազն առաջին երկու դաշտերը։";
                    case "Something's wrong...":
                        return "Ինչ որ բան այն չէ․․․";
                    case "       You must input a number, not a text!\n \t          Try again...":
                        return "Պետք է ներմուծել թիվ, այլ ոչ թե տեքստ!\n \tՓորձեք կրկին․․․";
                    default:
                        return str;
                }
            }
            else return str;
        }
        public string ChooseL(int i, string str)
        {
            if (this.Language == 2)
                return $"{i} վայրկյան";
            else return $"{i} {str}";
        }

        #region Chstacvac animacia
        /*
        private short chap;
        private void CreatePb()
        {
            pb_Raf = new PictureBox
            {
                Width = this.Width,
                Height = this.Height - 42,
                Left = 0,
                Top = 0,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            if (mnuBeginner.Checked)
                pb_Raf.BackgroundImage = Properties.Resources.WPB;
            else if (mnuIntermediate.Checked)
                pb_Raf.BackgroundImage = Properties.Resources.WPBI;
            else
                pb_Raf.BackgroundImage = Properties.Resources.WPE;

            chap = (short)pb_Raf.Width;

            //this.Controls.Add(pb_Raf);
            pb_Raf.BringToFront();

            tm_WPP = new System.Timers.Timer(5);
            tm_WPP.Elapsed += Tm_WPP_Elapsed;
        }

        private short angam = 0;
        private void Tm_WPP_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (angam >= 150)
            {
                if (chap >= 0)
                {
                    pb_Raf.Left += 7;
                    chap -= 7;
                }
                else
                {
                    this.Controls.Remove(pb_Raf);
                    tm_WPP.Enabled = false;
                }
            }
            angam++;
        }
        */

        #endregion
    }
}