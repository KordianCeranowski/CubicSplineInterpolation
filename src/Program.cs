using System;
using System.Collections.Generic;
using static CubicSplineInterpolation.CSI;

namespace CubicSplineInterpolation
{
    class Program
    {
        static void Main(string[] args)
        {
/*            Matrix matrix = new Matrix(3);
            Console.WriteLine(matrix);
            matrix[0, 0] = 1;
            matrix[1, 2] = 2;
            matrix[2, 2] = 3;
            Console.WriteLine(matrix);
            matrix.SwapRows(0, 1);
            Console.WriteLine(matrix);
            matrix[2, 2] = 0 ;
            Console.WriteLine(matrix);


            matrix[0, 0] = 1;
            matrix[0, 1] = -3;
            matrix[0, 2] = 4;
            matrix[1, 0] = 16;
            matrix[1, 1] = 6;
            matrix[1, 2] = 7;
            matrix[2, 0] = -5;
            matrix[2, 1] = 10;
            matrix[2, 2] = 4;

            double[] vector = { 23, 134, 19 };

            matrix.PerformGaussianElimination(vector);

            Console.WriteLine(vector[0] + " " + vector[1] + " " + vector[2]);*/


            List<Point> list = new List<Point>();
            list.Add(new Point(1, 1));
            list.Add(new Point(2, 5));
            list.Add(new Point(3, 4));
            list.Add(new Point(4, 8));
            list.Add(new Point(5, 5));
            list.Add(new Point(43, 8));
            list.Add(new Point(433, 5));


            CSI csi = new CSI(list);

            csi.Run();

            csi.printMatrix();

            csi.printVector();


        }
    }
}
