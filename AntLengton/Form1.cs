using System;
using System.Drawing;
using System.Windows.Forms;

namespace AntLengton
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        static int MovePos = 0;

        CheckBox[,] s = new CheckBox[80, 80];
        private void Form1_Load(object sender, EventArgs e)
        {
            int sz = 10;

            for (int i = 0; i < 80; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    s[i, j] = new CheckBox { Size = new Size(sz, sz), Location = new Point(i * sz, j * sz), Enabled = true };
                }
            }
            foreach (CheckBox item in s)
            {
                this.Controls.Add(item);
            }
        }
        int xPos = 50;
        int yPos = 50;

        readonly int[,] Posin = new int[,]
        {
            {0,-1},
            {-1,0},
            {0,1},
            {1,0},
        };
        private void timer1_Tick(object sender, EventArgs e)
        {
            xPos += Posin[MovePos, 0];
            yPos += Posin[MovePos, 1];

            if (xPos == 80 )
            {
                xPos = 0;
            }
            else if(xPos == -1)
            {
                xPos = 79;
            }

            if(yPos == 80)
            {
                yPos = 0;
            }
            else if(yPos == -1)
            {
                yPos = 79;
            }

            
            if (s[xPos, yPos].CheckState == CheckState.Unchecked)
            {
                s[xPos, yPos].CheckState = CheckState.Indeterminate;
     
                MovePos += 1;
                if (MovePos == 4)
                {
                    MovePos = 0;
                }

            }
            else if (s[xPos, yPos].CheckState == CheckState.Indeterminate)
            {
                s[xPos, yPos].CheckState = CheckState.Unchecked;

                MovePos -= 1;
                if (MovePos == -1)
                {
                    MovePos = 3;
                }
            }
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
