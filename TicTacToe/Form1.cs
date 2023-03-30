using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {

        private Graphics graphics = null;
        bool crossPlayer = false;
        //
        string[,] table = new string[3, 3];
        int xScore_ = 0;
        int bScore_ = 0;
        //
        bool gameOver = false;
        bool endGame = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int yValue = 500;
            label1.Location = new Point((this.Width / 2) - 120, (this.Height / 2) - yValue);
            label2.Location = new Point((this.Width / 2) - 160, (this.Height / 2) - (yValue - 50));
            pictureBox1.Location = new Point((this.Width / 2) - 58, (this.Height / 2) - (yValue - 100));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            //
            Draw();
        }

        private void Draw()
        {
            DrawGrid();

            //DrawCross(814, 370);//TOP LEFT CORNER
            //DrawCross(814, 490);//LEFT
            //DrawCross(814, 610);//BOTTOM LEFT CORNER
            //DrawCross(934, 370);//UP
            //DrawCross(934, 490);//CENTER
            //DrawCross(934, 610);//DOWN
            //DrawCross(1054, 370);//TOP RIGHT CORNER
            //DrawCross(1054, 490);//RIGHT
            //DrawCross(1054, 610);//BOTTOM RIGHT CORNER

            //DrawCircle(960, 491);//CENTER
            //DrawCircle(1080, 491);//RIGHT
        }

        private void DrawGrid()
        {
            //int ttt = ((this.Width / 2) - 8);//960 <<< VALOR CENTRAL
            //DrawVerticalLine(Color.Red, ((this.Width / 2) - 8), 0, this.Height);// -- MARCAÇÃO VERTICAL
            DrawVerticalLine(Color.White, ((this.Width / 2) - 8) - 60, 690, 330);
            DrawVerticalLine(Color.White, ((this.Width / 2) - 8) + 60, 690, 330);

            //DrawHorizontalLine(Color.Red, 0, this.Width, (this.Height / 2) - 7);//-7 --- MARCAÇÃO HORIZONTAL
            DrawHorizontalLine(Color.White, 800, 1100, ((this.Height / 2) - 7) - 60);//-7
            DrawHorizontalLine(Color.White, 800, 1100, ((this.Height / 2) - 7) + 60);//-7

            Turn();

            DrawCross(20, 350);
            DrawCircle(45, 450);
        }

        private void DrawVerticalLine(Color color, int x, int y1, int y2)
        {
            graphics.DrawLine(new Pen(new SolidBrush(color), 2), x, y1, x, y2);
        }

        private void DrawHorizontalLine(Color color, int x1, int x2, int y)
        {
            graphics.DrawLine(new Pen(new SolidBrush(color), 2), x1, y, x2, y);
        }

        private void DrawCross(int x = 0, int y = 0, bool erase = false)
        {
            graphics = this.CreateGraphics();
            Color color = erase ? Color.Black : Color.DeepSkyBlue;
            //x += 100;
            //y = 490;
            int size = 50;
            graphics.DrawLine(new Pen(new SolidBrush(color), 7), x, y, (x + size), (y + size));
            graphics.DrawLine(new Pen(new SolidBrush(color), 7), x, y + size, x + size, y);
        }

        private void DrawCircle(int x = 0, int y = 0, bool erase = false)
        {
            Color color = erase ? Color.Black : Color.Red;
            graphics = this.CreateGraphics();
            int size = 50;
            Rectangle rectangle = new Rectangle((x - (size / 2)), y, size, size);
            graphics.DrawEllipse(new Pen(new SolidBrush(color), 7), rectangle);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!endGame)
            {
                crossPlayer = !crossPlayer;
                bool validMoviment = false;
                if (crossPlayer)
                {
                    if (e.Y < 450)//TOP
                    {
                        if (e.X < 900)//LEFT
                        {
                            if (table[0, 0] == null)
                            {
                                DrawCross(814, 370);
                                table[0, 0] = "X";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 900 && e.X < 1015)//CENTER
                        {
                            if (table[0, 1] == null)
                            {
                                DrawCross(934, 370);
                                table[0, 1] = "X";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 1015 && e.X < 1115)//RIGHT
                        {
                            if (table[0, 2] == null)
                            {
                                DrawCross(1054, 370);
                                table[0, 2] = "X";
                                validMoviment = true;
                            }
                        }
                    }
                    if (e.Y > 450 && e.Y < 578)//CENTER
                    {
                        if (e.X < 900)//LEFT
                        {
                            if (table[1, 0] == null)
                            {
                                DrawCross(814, 490);
                                table[1, 0] = "X";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 900 && e.X < 1015)//CENTER
                        {
                            if (table[1, 1] == null)
                            {
                                DrawCross(934, 490);
                                table[1, 1] = "X";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 1015 && e.X < 1115)//RIGHT
                        {
                            if (table[1, 2] == null)
                            {
                                DrawCross(1054, 490);
                                table[1, 2] = "X";
                                validMoviment = true;
                            }
                        }
                    }
                    if (e.Y > 578)//BOTTOM
                    {
                        if (e.X < 900)//LEFT
                        {
                            if (table[2, 0] == null)
                            {
                                DrawCross(814, 610);
                                table[2, 0] = "X";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 900 && e.X < 1015)//CENTER
                        {
                            if (table[2, 1] == null)
                            {
                                DrawCross(934, 610);
                                table[2, 1] = "X";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 1015 && e.X < 1115)//RIGHT
                        {
                            if (table[2, 2] == null)
                            {
                                DrawCross(1054, 610);
                                table[2, 2] = "X";
                                validMoviment = true;
                            }
                        }
                    }
                    //
                    if (validMoviment)
                    {
                        Turn();
                    }
                    else
                    {
                        crossPlayer = !crossPlayer;
                    }
                }
                else
                {
                    if (e.Y < 450)//TOP
                    {
                        if (e.X < 900)//LEFT
                        {
                            if (table[0, 0] == null)
                            {
                                DrawCircle(839, 370);
                                table[0, 0] = "B";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 900 && e.X < 1015)//CENTER
                        {
                            if (table[0, 1] == null)
                            {
                                DrawCircle(959, 370);
                                table[0, 1] = "B";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 1015 && e.X < 1115)//RIGHT
                        {
                            if (table[0, 2] == null)
                            {
                                DrawCircle(1079, 370);
                                table[0, 2] = "B";
                                validMoviment = true;
                            }
                        }
                    }
                    if (e.Y > 450 && e.Y < 578)//CENTER
                    {
                        if (e.X < 900)//LEFT
                        {
                            if (table[1, 0] == null)
                            {
                                DrawCircle(840, 491);
                                table[1, 0] = "B";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 900 && e.X < 1015)//CENTER
                        {
                            if (table[1, 1] == null)
                            {
                                DrawCircle(960, 491);
                                table[1, 1] = "B";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 1015 && e.X < 1115)//RIGHT
                        {
                            if (table[1, 2] == null)
                            {
                                DrawCircle(1080, 491);
                                table[1, 2] = "B";
                                validMoviment = true;
                            }
                        }
                    }
                    if (e.Y > 578)//BOTTOM
                    {
                        if (e.X < 900)//LEFT
                        {
                            if (table[2, 0] == null)
                            {
                                DrawCircle(839, 610);
                                table[2, 0] = "B";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 900 && e.X < 1015)//CENTER
                        {
                            if (table[2, 1] == null)
                            {
                                DrawCircle(959, 610);
                                table[2, 1] = "B";
                                validMoviment = true;
                            }
                        }
                        if (e.X > 1015 && e.X < 1115)//RIGHT
                        {
                            if (table[2, 2] == null)
                            {
                                DrawCircle(1079, 610);
                                table[2, 2] = "B";
                                validMoviment = true;
                            }
                        }
                    }
                    //
                    if (validMoviment)
                    {
                        Turn();
                    }
                    else
                    {
                        crossPlayer = !crossPlayer;
                    }
                }
                GameOver();
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);
            DrawGrid();
            table = new string[3, 3];
            endGame = false;
        }

        private void Turn()
        {
            if (crossPlayer)
            {
                DrawCross(110, 100, true);
                DrawCircle(135, 100);
            }
            else
            {
                DrawCircle(135, 100, true);
                DrawCross(110, 100);
            }
        }

        private void GameOver()
        {
            int[] colorArray = new int[3];
            int count;
            for (int k = 0; k < 2; k++)//TESTE VERTICAL
            {
                for (int i = 0; i < 3; i++)
                {
                    count = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        if (table[j, i] != null)
                        {
                            count += table[j, i].Equals(k == 0 ? "X" : "B") ? 1 : 0;
                        }
                    }
                    gameOver = count == 3 ? true : false;
                    //
                    if (gameOver)
                    {
                        endGame = true;
                        if (k == 0)
                        {
                            //MessageBox.Show("Vencedor X");
                            xScore.Text = $"{++xScore_}";
                        }
                        else
                        {
                            //MessageBox.Show("Vencedor B");
                            bScore.Text = $"{++bScore_}";
                        }
                    }
                }
            }
            //
            for (int k = 0; k < 2; k++)//TESTE HORIZONTAL
            {
                for (int i = 0; i < 3; i++)
                {
                    count = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        if (table[i, j] != null)
                        {
                            count += table[i, j].Equals(k == 0 ? "X" : "B") ? 1 : 0;
                        }
                    }
                    gameOver = count == 3 ? true : false;
                    //
                    if (gameOver)
                    {
                        endGame = true;
                        if (k == 0)
                        {
                            //MessageBox.Show("Vencedor X");
                            xScore.Text = $"{++xScore_}";
                        }
                        else
                        {
                            //MessageBox.Show("Vencedor B");
                            bScore.Text = $"{++bScore_}";
                        }
                    }
                }
            }
            //
            for (int k = 0; k < 2; k++)
            {
                count = 0;
                for (int i = 0; i < 3; i++)//TESTE DIAGONAL 1
                {
                    if (table[i, i] != null)
                    {
                        if (table[i, i].Equals(k == 0 ? "X" : "B"))
                        {
                            count++;
                        }
                    }
                }
                gameOver = count == 3 ? true : false;
                if (gameOver)
                {
                    endGame = true;
                    if (k == 0)
                    {
                        //MessageBox.Show("Vencedor X");
                        xScore.Text = $"{++xScore_}";
                    }
                    else
                    {
                        //MessageBox.Show("Vencedor B");
                        bScore.Text = $"{++bScore_}";
                    }
                }
            }
            //
            for (int k = 0; k < 2; k++)
            {
                count = 0;
                for (int i = 2; i >= 0; i--)//TESTE DIAGONAL 2
                {
                    if (table[i, (2 - i)] != null)
                    {
                        if (table[i, (2 - i)].Equals(k == 0 ? "X" : "B"))
                        {
                            count++;
                        }
                    }
                }
                gameOver = count == 3 ? true : false;
                if (gameOver)
                {
                    endGame = true;
                    if (k == 0)
                    {
                        //MessageBox.Show("Vencedor X");
                        xScore.Text = $"{++xScore_}";
                    }
                    else
                    {
                        //MessageBox.Show("Vencedor B");
                        bScore.Text = $"{++bScore_}";
                    }
                }
            }
        }
    }
}
