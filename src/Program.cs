using System;
using System.Collections.Generic;
using static CubicSplineInterpolation.CSI;

namespace CubicSplineInterpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix = new Matrix(3);
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

            Console.WriteLine(vector[0] + " " + vector[1] + " " + vector[2]);


            List<Point> list = new List<Point>();
            list.Add(new Point(1, 1));
            list.Add(new Point(2, 5));
            list.Add(new Point(3, 4));


            CSI csi = new CSI(list);

            csi.Run();

            Console.WriteLine(csi.matrix);

            Console.WriteLine(
                csi.vector[0] + " " +
                csi.vector[1] + " " +
                csi.vector[2] + " " +
                csi.vector[3] + " " +
                csi.vector[4] + " " +
                csi.vector[5] + " " +
                csi.vector[6] + " " +
                csi.vector[7] + " "
                );


        }
    }
}
