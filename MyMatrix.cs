using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeLaVida
{
    public class MyMatrix
    {
        private readonly int _dimX;
        private readonly int _dimY;
        private readonly Size _rectSize;

        public bool[,] stateMatrix { get; set; }
        public Rectangle[,] graphicMatrixTrue { get; set; }
        public Rectangle[,] graphicMatrixFalse { get; set; }

        public MyMatrix(int dimX, int dimY, Size rectSize)
        {
            _dimX = dimX;
            _dimY = dimY;
            _rectSize = rectSize;

            stateMatrix = new bool[dimX, dimY];
            graphicMatrixTrue = new Rectangle[dimX, dimY];
            graphicMatrixFalse = new Rectangle[dimX, dimY];
        }

        public void FillGraphicMatrix()
        {
            int pointX;
            int pointY;

            graphicMatrixTrue = new Rectangle[_dimX, _dimY];
            graphicMatrixFalse = new Rectangle[_dimX, _dimY];

            for (int i=0; i<_dimX; i++)
            {
                for (int j = 0; j < _dimY; j++)
                {
                    pointX = i * _rectSize.Width;
                    pointY = j * _rectSize.Height;

                    var point = new Point(pointX, pointY);
                    var rect = new Rectangle(point, _rectSize);

                    if (stateMatrix[j, i]) graphicMatrixTrue[j, i] = rect;
                    if (!stateMatrix[j, i]) graphicMatrixFalse[j, i] = rect;

                    /*
                    if(i == 10 && j == 10)
                    {
                        Console.WriteLine("ESTADO DE LA MATRIZ EN EL METODO QUE ACTUALIZA GRAFICOS");
                        Console.WriteLine();
                        Console.Write((stateMatrix[9, 9] ? 1 : 0) + " ");
                        Console.Write((stateMatrix[9, 10] ? 1 : 0) + " ");
                        Console.WriteLine((stateMatrix[9, 11] ? 1 : 0) + " ");
                        Console.Write((stateMatrix[10, 9] ? 1 : 0) + " ");
                        Console.Write((stateMatrix[10, 10] ? 1 : 0) + " ");
                        Console.WriteLine((stateMatrix[10, 11] ? 1 : 0) + " ");
                        Console.Write((stateMatrix[11, 9] ? 1 : 0) + " ");
                        Console.Write((stateMatrix[11, 10] ? 1 : 0) + " ");
                        Console.WriteLine((stateMatrix[11, 11] ? 1 : 0) + " ");

                        var rectTest = new Rectangle(new Point(0, 0), _rectSize);

                        Console.WriteLine();
                        Console.Write((graphicMatrixTrue[9, 9].Location != rectTest.Location ? 1 : 0) + " ");
                        Console.Write((graphicMatrixTrue[9, 10].Location != rectTest.Location ? 1 : 0) + " ");
                        Console.WriteLine((graphicMatrixTrue[9, 11].Location != rectTest.Location ? 1 : 0) + " ");
                        Console.Write((graphicMatrixTrue[10, 9].Location != rectTest.Location ? 1 : 0) + " ");
                        Console.Write((graphicMatrixTrue[10, 10].Location != rectTest.Location ? 1 : 0) + " ");
                        Console.WriteLine((graphicMatrixTrue[10, 11].Location != rectTest.Location ? 1 : 0) + " ");
                        Console.Write((graphicMatrixTrue[11, 9].Location != rectTest.Location ? 1 : 0) + " ");
                        Console.Write((graphicMatrixTrue[11, 10].Location != rectTest.Location ? 1 : 0) + " ");
                        Console.WriteLine((graphicMatrixTrue[11, 11].Location != rectTest.Location ? 1 : 0) + " ");
                    }
                    */
                }
            }
        }

        public void RandomFillState()
        {
            Random r = new Random();
            for (int i = 0; i < _dimX; i++)
            {
                for (int j = 0; j < _dimX; j++)
                {
                    int num = r.Next(0, 2);
                    stateMatrix[i, j] = Convert.ToBoolean(num);
                }
            }
        }

        public void CreatePlane()
        {
            stateMatrix = new bool[_dimX, _dimY];
            stateMatrix[9, 10] = true;
            stateMatrix[10, 11] = true;
            stateMatrix[11, 9] = true;
            stateMatrix[11, 10] = true;
            stateMatrix[11, 11] = true;
        }

        public void Initialize()
        {
            RandomFillState();
            FillGraphicMatrix();
        }

        public void InitializeCustom()
        {
            CreatePlane();
            FillGraphicMatrix();
        }
    }
}
