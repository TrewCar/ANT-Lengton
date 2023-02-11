using System;
using System.Drawing;
using System.Windows.Forms;

namespace AntLengtonOptimozation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Location = new Point(this.Width / 2 - groupBox1.Width / 2, this.Height / 2 - groupBox1.Height / 2);
        }

        Graphics g;

        private int ColVoX; //колво клетов по ширине  по высоте
        private int ColVoY;

        private int[,] LocInf; // хранит 1 или 0 т.е. занчение цвета клетки

        private int SizeRect;

        private readonly int[,] Posin = new int[,] // шаги 
        {
            {0,-1},
            {-1,0},
            {0,1},
            {1,0},
        };

        InformationAnt[] Ants; // массив с маравьями, с значениями

        private int ants; // колво муравьёв

        private void ProvPos(ref Point Pos)
        {
            if (Pos.X == ColVoX)
                Pos.X = 0;
            else if (Pos.X < 0)
                Pos.X = ColVoX - 1;

            if (Pos.Y == ColVoY)
                Pos.Y = 0;
            else if (Pos.Y < 0)
                Pos.Y = ColVoY - 1;
        }

        private void ProvPixelColorAndMoveAnt(ref InformationAnt Ant)
        {
            Ant.Pos.X += Posin[Ant.MovePos, 0];

            Ant.Pos.Y += Posin[Ant.MovePos, 1];

            ProvPos(ref Ant.Pos);

            if (LocInf[Ant.Pos.X, Ant.Pos.Y] == 1)
            {
                LocInf[Ant.Pos.X, Ant.Pos.Y] = 0;
                Ant.MovePos -= 1;
                g.FillRectangle(Brushes.Black, Ant.Pos.X * SizeRect, Ant.Pos.Y * SizeRect, SizeRect, SizeRect);
            }
            else if (LocInf[Ant.Pos.X, Ant.Pos.Y] == 0)
            {
                LocInf[Ant.Pos.X, Ant.Pos.Y] = 1;
                Ant.MovePos += 1;
                g.FillRectangle(Ant.color, Ant.Pos.X * SizeRect, Ant.Pos.Y * SizeRect, SizeRect, SizeRect);
            }

            if (Ant.MovePos == 4)
                Ant.MovePos = 0;
            else if (Ant.MovePos == -1)
                Ant.MovePos = 3;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ants; i++)
                ProvPixelColorAndMoveAnt(ref Ants[i]); // Обработка каждого муравья 

            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;

            g = Graphics.FromImage(pictureBox1.Image);

            ants = int.Parse(textBox1.Text);

            SizeRect = int.Parse(textBox2.Text); // размер клетки

            ColVoX = pictureBox1.Width / SizeRect;
            ColVoY = pictureBox1.Height / SizeRect;

            LocInf = new int[ColVoX, ColVoY];
            Ants = new InformationAnt[ants];

            for (int i = 0; i < ants; i++)
            {
                Ants[i].MovePos = 0;

                Ants[i].Pos = new Point(rand.Next(0, ColVoX), rand.Next(0, ColVoY));

                Ants[i].color = new SolidBrush(Color.FromArgb(rand.Next(30, 230), rand.Next(30, 230), rand.Next(30, 230)));
            }
            timer1.Start();
            groupBox1.Visible = false;
        }
    }

    struct InformationAnt
    {
        public int MovePos;

        public Point Pos;

        public SolidBrush color;
    }
}