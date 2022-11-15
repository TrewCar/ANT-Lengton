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

        static int ColVoX; //колво клетов по ширине  по высоте
        static int ColVoY;

        int[,] LocInf; // хранит 1 или 0 т.е. занчение цвета клетки

        readonly int[,] Posin = new int[,] // шаги 
        {
            {0,-1},
            {-1,0},
            {0,1},
            {1,0},
        };

        InformationAnt[] Ants; // массив с маравьями, с значениями

        int ants; // колво муравьёв

        private void ProvPos(ref int xPos, ref int yPos)
        {
            if (xPos == ColVoX)
                xPos = 0;

            else if (xPos < 0)
                xPos = ColVoX - 1;

            if (yPos == ColVoY)
                yPos = 0;

            else if (yPos < 0)
                yPos = ColVoY - 1;
        }

        private void ProvPixelColorAndMoveAnt(ref int MovePos, ref int xPos, ref int yPos, SolidBrush ColorPixel)
        {
            xPos += Posin[MovePos, 0];

            yPos += Posin[MovePos, 1];

            ProvPos(ref xPos, ref yPos);

            if (LocInf[xPos, yPos] == 1)
            {
                LocInf[xPos, yPos] = 0;
                MovePos -= 1;
                g.FillRectangle(Brushes.Black, xPos * SizeRect, yPos * SizeRect, SizeRect, SizeRect);
            }
            else if (LocInf[xPos, yPos] == 0)
            {
                LocInf[xPos, yPos] = 1;
                MovePos += 1;
                g.FillRectangle(ColorPixel, xPos * SizeRect, yPos * SizeRect, SizeRect, SizeRect);
            }

            if (MovePos == 4)
                MovePos = 0;
            else if (MovePos == -1)
                MovePos = 3;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ants; i++)
                ProvPixelColorAndMoveAnt(ref Ants[i].MovePos, ref Ants[i].xPos, ref Ants[i].yPos, new SolidBrush(Color.FromArgb(Ants[i].R, Ants[i].G, Ants[i].B))); // Обработка каждого муравья


            pictureBox1.Refresh();
        }
        int SizeRect;
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

                Ants[i].xPos = rand.Next(0, ColVoX); //местоположение по оси X
                Ants[i].yPos = rand.Next(0, ColVoY); //местоположение по оси Y

                Ants[i].R = rand.Next(30, 230);  //
                Ants[i].G = rand.Next(30, 230);  //Индексы цветов по RGB
                Ants[i].B = rand.Next(30, 230);  //
            }
            timer1.Start();
            groupBox1.Visible = false;
        }
    }

    struct InformationAnt
    {
        public int MovePos;

        public int xPos;
        public int yPos;

        public int R;
        public int G;
        public int B;
    }
}
