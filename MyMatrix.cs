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

        // TODO: Convertir a estáticos
        public static bool[,] stateMatrix { get; set; }
        public static Rectangle[,] graphicMatrixTrue { get; set; }
        public static Rectangle[,] graphicMatrixFalse { get; set; }

        public delegate void Figure();

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

            for (int i = 0; i < _dimX; i++)
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

        public void FillRandomState()
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

        public static void Plane()
        {
            stateMatrix[0, 1] = true;
            stateMatrix[1, 2] = true;
            stateMatrix[2, 0] = true;
            stateMatrix[2, 1] = true;
            stateMatrix[2, 2] = true;
        }

        public static void Plane2()
        {
            stateMatrix[10, 11] = true;
            stateMatrix[11, 12] = true;
            stateMatrix[12, 10] = true;
            stateMatrix[12, 11] = true;
            stateMatrix[12, 12] = true;
        }


        public List<Figure> figuresList = new List<Figure>();

        /*
        public Figure fPlane = delegate (int dimX, int dimY)
        {
            stateMatrix[9, 10] = true;
            stateMatrix[10, 11] = true;
            stateMatrix[11, 9] = true;
            stateMatrix[11, 10] = true;
            stateMatrix[11, 11] = true;
        };

        public Figure fPlane2 = delegate (int dimX, int dimY)
        {
            stateMatrix[0, 1] = true;
            stateMatrix[1, 2] = true;
            stateMatrix[2, 0] = true;
            stateMatrix[2, 1] = true;
            stateMatrix[2, 2] = true;
        };
        */

        public void Initialize()
        {
            FillRandomState();
            FillGraphicMatrix();
        }

        public void InitializeCustom(List<Figure> fs)
        {
            stateMatrix = new bool[_dimX, _dimY];

            figuresList = fs;

            foreach(var f in fs)
            {
                f();
            }

            FillGraphicMatrix();
        }
    }
}
