using System;
using System.Drawing;
using System.Windows.Forms;

namespace AntLengtonOptimozation
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bmp = new Bitmap(500, 500);

        static int ColVo = 100;

        static readonly int rect = 500 / ColVo;

        int xPos = 50;
        int yPos = 50;

        int[,] LocInf = new int[ColVo, ColVo];

        readonly int[,] Posin = new int[,]
        {
            {0,-1},
            {-1,0},
            {0,1},
            {1,0},
        };

        static int MovePos = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = bmp;

            g = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < 500; i += rect)
            {
                g.DrawLine(new Pen(Color.Black, 1), new Point(i, 0), new Point(i, pictureBox1.Height));
            }
            g.DrawLine(new Pen(Color.Black, 1), new Point(pictureBox1.Width - 1, 0), new Point(pictureBox1.Width - 1, pictureBox1.Height));
            for (int i = 0; i < 500; i += rect)
            {
                g.DrawLine(new Pen(Color.Black, 1), new Point(0, i), new Point(pictureBox1.Width, i));
            }
            g.DrawLine(new Pen(Color.Black, 1), new Point(0, pictureBox1.Height - 1), new Point(pictureBox1.Width, pictureBox1.Height - 1));



        }

        private void ProvPos()
        {

            if (xPos == ColVo)
                xPos = 0;

            else if (xPos < 0)
                xPos = ColVo - 1;

            if (yPos == ColVo)
                yPos = 0;

            else if (yPos < 0)
                yPos = ColVo - 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            xPos += Posin[MovePos,0];
            yPos += Posin[MovePos, 1];

            ProvPos();

            if (LocInf[xPos, yPos] == 1)
            {
                LocInf[xPos, yPos] = 0;
                MovePos -= 1;
                g.FillRectangle(Brushes.White, xPos * rect, yPos * rect, rect, rect);
            }
            else if (LocInf[xPos, yPos] == 0)
            {
                LocInf[xPos, yPos] = 1;
                MovePos += 1;
                g.FillRectangle(Brushes.Blue, xPos * rect, yPos * rect, rect, rect);
            }

            if (MovePos == 4)
                MovePos = 0;
            else if (MovePos == -1)
                MovePos = 3;

            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
