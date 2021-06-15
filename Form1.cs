using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoDeLaVida
{
    public partial class Form1 : Form
    {
        int mapSize = 50;
        int squareSize = 10;
        private readonly Size _size;

        public bool[,] rect2dFilledAux { get; set; }

        public MyMatrix matrix { get; set; }

        Pen pen = new Pen(Color.Black);
        Brush brushTrue = new SolidBrush(Color.Black);
        Brush brushFalse = new SolidBrush(Color.White);

        public Form1()
        {
            InitializeComponent();
            Width = mapSize * squareSize + 20;
            Height = mapSize * squareSize + 20;

            _size = new Size(squareSize, squareSize);
            rect2dFilledAux = new bool[mapSize, mapSize];
            matrix = new MyMatrix(mapSize, mapSize, _size);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            matrix.Initialize();
            //matrix.InitializeCustom();

            foreach (var p in matrix.graphicMatrixTrue)
            {
                g.FillRectangle(brushTrue, p);
            }

            while (true)
            {
                // Resalta la casilla 10x10 en rojo
                //g.DrawRectangle(new Pen(Color.Red), new Rectangle(new Point(20 * 10, 20 * 10), new Size(20, 20)));

                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        SetNextCellState(j, i, GiveNeighborsNum(j, i));
                    }
                }

                // PRUEBAS
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        matrix.stateMatrix[j, i] = rect2dFilledAux[j, i];
                    }
                }

                //matrix.stateMatrix = rect2dFilledAux;

                matrix.FillGraphicMatrix();

                foreach(var p in matrix.graphicMatrixTrue)
                {
                    g.FillRectangle(brushTrue, p);
                }

                foreach (var p in matrix.graphicMatrixFalse)
                {
                    g.FillRectangle(brushFalse, p);
                }

                Thread.Sleep(30);
            }
        }


        public void SetNextCellState(int rectPosX, int rectPosY, int neighbors)
        {
            if (matrix.stateMatrix[rectPosX, rectPosY] == true && (neighbors != 3 && neighbors != 2))
            {
                rect2dFilledAux[rectPosX, rectPosY] = false;
            }
            else if (matrix.stateMatrix[rectPosX, rectPosY] == true && (neighbors == 3 || neighbors == 2))
            {
                rect2dFilledAux[rectPosX, rectPosY] = true;
            }
            else if (matrix.stateMatrix[rectPosX, rectPosY] == false && neighbors == 3)
            {
                rect2dFilledAux[rectPosX, rectPosY] = true;
            }
            else if (matrix.stateMatrix[rectPosX, rectPosY] == false && neighbors != 3)
            {
                rect2dFilledAux[rectPosX, rectPosY] = false;
            }

            /*
            if(rectPosX == 10 && rectPosY == 10)
            {
                Console.WriteLine($"Posicion X del rectángulo: {rectPosX}");
                Console.WriteLine($"Posicion Y del rectángulo: {rectPosY}");
                Console.WriteLine($"Aux[10, 10]: {rect2dFilledAux[rectPosX, rectPosY]}");
            }
            */

            return;
        }

        public int GiveNeighborsNum(int rectPosX, int rectPosY)
        {
            /*
            if(rectPosX == 10 && rectPosY == 10)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Write((matrix.stateMatrix[9, 9] ? 1 : 0) + " ");
                Console.Write((matrix.stateMatrix[9, 10] ? 1 : 0) + " ");
                Console.WriteLine((matrix.stateMatrix[9, 11] ? 1 : 0) + " ");
                Console.Write((matrix.stateMatrix[10, 9] ? 1 : 0) + " ");
                Console.Write((matrix.stateMatrix[10, 10] ? 1 : 0) + " ");
                Console.WriteLine((matrix.stateMatrix[10, 11] ? 1 : 0) + " ");
                Console.Write((matrix.stateMatrix[11, 9] ? 1 : 0) + " ");
                Console.Write((matrix.stateMatrix[11, 10] ? 1 : 0) + " ");
                Console.WriteLine((matrix.stateMatrix[11, 11] ? 1 : 0) + " ");
                var resultTest = 0;

                int neighborXTest = rectPosX - 1;
                for (int i = 0; i < 3; i++)
                {
                    // Controla los límites del array
                    if (neighborXTest < 0 || neighborXTest >= mapSize)
                    {
                        neighborXTest++;
                        continue;
                    }

                    int neighborYTest = rectPosY - 1;
                    for (int j = 0; j < 3; j++)
                    {
                        // Controla los límites del array
                        if (neighborYTest < 0 || neighborYTest >= mapSize)
                        {
                            neighborYTest++;
                            continue;
                        }

                        // Cumprueba vecinos pero se excluye a si mismo
                        if (matrix.stateMatrix[neighborXTest, neighborYTest] == true && (neighborXTest != rectPosX || neighborYTest != rectPosY))
                        {
                            resultTest++;
                        }

                        neighborYTest++;
                    }

                    neighborXTest++;
                }
                Console.WriteLine($"Vecinos en el test: {resultTest}");
            }
            */

            var result = 0;

            int neighborX = rectPosX - 1;
            for (int i=0; i<3; i++)
            {
                // Controla los límites del array
                if (neighborX < 0)
                {
                    neighborX += mapSize;
                }
                else if (neighborX >= mapSize)
                {
                    neighborX -= mapSize;
                }

                int neighborY = rectPosY - 1;
                for (int j = 0; j < 3; j++)
                {
                    // Controla los límites del array
                    if (neighborY < 0)
                    {
                        neighborY += mapSize;
                    }
                    else if (neighborY >= mapSize)
                    {
                        neighborY -= mapSize;
                    }

                    // Cumprueba vecinos pero se excluye a si mismo
                    if (matrix.stateMatrix[neighborX, neighborY] == true && (neighborX != rectPosX || neighborY != rectPosY))
                    {
                        result++;
                    }

                    neighborY++;
                }

                neighborX++;
            }

            /*
            if (rectPosX == 10 && rectPosY == 10)
            {
                Console.WriteLine($"Vecinos calculados: {result}");
            }
            */

            return result;
        }

        public Rectangle[] ConvierteArrayRectagulos2dA1d(Rectangle[,] rects)
        {
            var result = new Rectangle[50*50];

            for(int i=0; i<50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    // Calcula la posición que le corresponde en el array de una dimensión
                    result[j + i * 50] = rects[i, j];
                }
            }

            return result;
        }
    }
}
