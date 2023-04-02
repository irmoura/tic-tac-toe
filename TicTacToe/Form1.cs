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
        bool crossPlayer = true;
        //
        //string[,] table = new string[3, 3];
        Coordinate[,] coordinates = new Coordinate[3, 3];
        int xScore_ = 0;
        int bScore_ = 0;
        int size = 50;
        //
        bool gameOver = false;
        bool endGame = false;
        //
        int centralVerticalLine = 0;
        int centralHorizontalLine = 0;
        //
        bool empate = false;

        public Form1()
        {
            InitializeComponent();
            // Obtém a segunda tela do sistema
            Screen secondScreen = Screen.AllScreens[0];

            // Define a posição da janela na segunda tela
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = secondScreen.Bounds.Location;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphics = CreateGraphics();
            //reiniciarToolStripMenuItem.PerformClick();
        }

        private void DrawGrid()
        {
            graphics = this.CreateGraphics();
            graphics.Clear(Color.Black);
            centralHorizontalLine = (this.Width / 2) - 8;
            centralVerticalLine = (this.Height / 2) - 7;
            //DrawVerticalLine(Color.Red, centralHorizontalLine, 0, this.Height);// -- MARCAÇÃO VERTICAL VERTICAL USO NOS TESTES
            //DrawVerticalLine(Color.Red, centralHorizontalLine + 120, 0, this.Height);// -- MARCAÇÃO VERTICAL USO NOS TESTES
            //DrawVerticalLine(Color.Red, centralHorizontalLine - 120, 0, this.Height);// -- MARCAÇÃO VERTICAL USO NOS TESTES
            DrawVerticalLine(Color.White, centralHorizontalLine - 60, centralVerticalLine + 180, centralVerticalLine - 180);
            DrawVerticalLine(Color.White, centralHorizontalLine + 60, centralVerticalLine + 180, centralVerticalLine - 180);

            //DrawHorizontalLine(Color.Red, 0, this.Width, centralVerticalLine);//-7 --- MARCAÇÃO HORIZONTAL VERTICAL USO NOS TESTES
            //DrawHorizontalLine(Color.Red, 0, this.Width, centralVerticalLine + 120);//-7 --- MARCAÇÃO HORIZONTAL VERTICAL USO NOS TESTES
            //DrawHorizontalLine(Color.Red, 0, this.Width, centralVerticalLine - 120);//-7 --- MARCAÇÃO HORIZONTAL VERTICAL USO NOS TESTES
            DrawHorizontalLine(Color.White, centralHorizontalLine - 180, centralHorizontalLine + 180, centralVerticalLine - 60);
            DrawHorizontalLine(Color.White, centralHorizontalLine - 180, centralHorizontalLine + 180, centralVerticalLine + 60);

            Turn();

            DrawCross(Color.DeepSkyBlue, 20, 350);
            DrawCircle(Color.Red, 45, 450);
        }

        private void DrawVerticalLine(Color color, int x, int y1, int y2)
        {
            graphics = this.CreateGraphics();
            graphics.DrawLine(new Pen(new SolidBrush(color), 2), x, y1, x, y2);
        }

        private void DrawHorizontalLine(Color color, int x1, int x2, int y)
        {
            graphics = this.CreateGraphics();
            graphics.DrawLine(new Pen(new SolidBrush(color), 2), x1, y, x2, y);
        }

        private void DrawCross(Color color, int x, int y, bool erase = false)
        {
            graphics = this.CreateGraphics();
            //Color color = erase ? Color.Black : Color.DeepSkyBlue;
            //color = destacar ? Color.Yellow : color;
            //color = empate ? Color.Red : color;
            graphics.DrawLine(new Pen(new SolidBrush(color), 7), x, y, (x + size), (y + size));
            graphics.DrawLine(new Pen(new SolidBrush(color), 7), x, y + size, x + size, y);
        }

        private void DrawCircle(Color color, int x, int y, bool erase = false)
        {
            graphics = this.CreateGraphics();
            //Color color = erase ? Color.Black : Color.Red;
            //color = destacar ? Color.Yellow : color;
            Rectangle rectangle = new Rectangle((x - (size / 2)), y, size, size);
            graphics.DrawEllipse(new Pen(new SolidBrush(color), 7), rectangle);
        }

        private void GameDraw(int x, int y, int line, int column)
        {
            crossPlayer = !crossPlayer;//COMENTAR NOS TESTES
            bool validMoviment = false;//INICIAR COM TRUE NOS TESTES
            if (coordinates[line, column] == null)
            {
                switch (crossPlayer)
                {
                    case true:
                        DrawCross(Color.DeepSkyBlue, x, y);
                        coordinates[line, column] = new Coordinate() { X = x, Y = y, player = "X" };
                        break;
                    case false:
                        DrawCircle(Color.Red, (x + 25), y);
                        coordinates[line, column] = new Coordinate() { X = (x + 25), Y = y, player = "B" };
                        break;
                }
                validMoviment = true;
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
            GameOver();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!endGame)
            {
                int x = 0;
                int column = 0;
                if (e.X < (centralHorizontalLine - 60))//LEFT
                {
                    x = (centralHorizontalLine - 145);
                }
                if (e.X > (centralHorizontalLine - 60) && e.X < (centralHorizontalLine + 60))//CENTER
                {
                    x = (centralHorizontalLine - 25);
                    column = 1;
                }
                if (e.X > (centralHorizontalLine + 60))//RIGHT
                {
                    x = (centralHorizontalLine + 95);
                    column = 2;
                }
                //
                if (e.Y < (centralVerticalLine - 60))//TOP
                {
                    GameDraw(x, (centralVerticalLine - 145), 0, column);
                }
                if (e.Y > (centralVerticalLine - 60) && e.Y < (centralVerticalLine + 60))//CENTER
                {
                    GameDraw(x, (centralVerticalLine - 25), 1, column);
                }
                if (e.Y > (centralVerticalLine + 60))//BOTTOM
                {
                    GameDraw(x, (centralVerticalLine + 95), 2, column);
                }
            }
        }

        private void Turn()
        {
            if (crossPlayer)
            {
                DrawCross(Color.Black, 110, 100, true);
                Color color = empate ? Color.Black : Color.Red;
                DrawCircle(color, 135, 100);
            }
            else
            {
                DrawCircle(Color.Black, 135, 100, true);
                Color color = empate ? Color.Black : Color.DeepSkyBlue;
                DrawCross(color, 110, 100);
            }
        }

        private void GameOver()
        {
            int count;
            string winner = string.Empty;
            for (int k = 0; k < 2; k++)//TESTE VERTICAL
            {
                for (int i = 0; i < 3; i++)
                {
                    count = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        if (coordinates[j, i] != null)
                        {
                            count += coordinates[j, i].player.Equals(k == 0 ? "X" : "B") ? 1 : 0;
                        }
                    }
                    gameOver = count == 3 ? true : false;
                    //
                    if (gameOver)
                    {
                        endGame = true;
                        if (k == 0)
                        {
                            xScore.Text = $"{++xScore_}";
                            winner = "X";
                        }
                        else
                        {
                            bScore.Text = $"{++bScore_}";
                            winner = "B";
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
                        if (coordinates[i, j] != null)
                        {
                            count += coordinates[i, j].player.Equals(k == 0 ? "X" : "B") ? 1 : 0;
                        }
                    }
                    gameOver = count == 3 ? true : false;
                    //
                    if (gameOver)
                    {
                        endGame = true;
                        if (k == 0)
                        {
                            xScore.Text = $"{++xScore_}";
                            winner = "X";
                        }
                        else
                        {
                            bScore.Text = $"{++bScore_}";
                            winner = "B";
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
                    if (coordinates[i, i] != null)
                    {
                        if (coordinates[i, i].player.Equals(k == 0 ? "X" : "B"))
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
                        xScore.Text = $"{++xScore_}";
                        winner = "X";
                    }
                    else
                    {
                        bScore.Text = $"{++bScore_}";
                        winner = "B";
                    }
                }
            }
            //
            for (int k = 0; k < 2; k++)
            {
                count = 0;
                for (int i = 2; i >= 0; i--)//TESTE DIAGONAL 2
                {
                    if (coordinates[i, (2 - i)] != null)
                    {
                        if (coordinates[i, (2 - i)].player.Equals(k == 0 ? "X" : "B"))
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
                        xScore.Text = $"{++xScore_}";
                        winner = "X";
                    }
                    else
                    {
                        bScore.Text = $"{++bScore_}";
                        winner = "B";
                    }
                }
            }
            //
            count = 0;
            if (!endGame)
            {
                for (int i = 0; i < 3; i++)//CHECAR EMPATE
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (coordinates[i, j] != null)
                        {
                            count++;
                        }
                    }
                }
                gameOver = count == 9 ? true : false;
                endGame = count == 9 ? true : false;
            }
            //
            if (endGame)
            {
                label3.Text = count < 9 ? $"Fim de Jogo! Campeão:" : "Empate!";
                empate = count < 9 ? false : true;
                crossPlayer = !crossPlayer;
                Turn();
                //
                for (int i = 0; i < 3; i++)//DESTACAR VENCEDOR
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (coordinates[i, j] != null)
                        {
                            if (coordinates[i, j].player.Equals("X") && winner.Equals("X"))
                            {
                                DrawCross(Color.Yellow, coordinates[i, j].X, coordinates[i, j].Y);
                            }
                            if (coordinates[i, j].player.Equals("B") && winner.Equals("B"))
                            {
                                DrawCircle(Color.Yellow, coordinates[i, j].X, coordinates[i, j].Y);
                            }
                            if (empate && coordinates[i, j].player.Equals("X"))
                            {
                                DrawCross(Color.Red, coordinates[i, j].X, coordinates[i, j].Y);
                            }
                        }
                    }
                }
                //
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //int yValue = 0;
            //label1.Location = new Point((this.Width / 2) - 120, 0);
            //label2.Location = new Point((this.Width / 2) - 160, yValue + 50);
            //pictureBox1.Location = new Point((this.Width / 2) - 58, yValue + 100);
            ////
            //DrawGrid();
        }

        private void reiniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);
            empate = false;
            DrawGrid();
            coordinates = new Coordinate[3, 3];
            endGame = false;
            label3.Text = "Próximo a Jogar:";
            reiniciarToolStripMenuItem.Text = "Reiniciar";
        }
    }
}
