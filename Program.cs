using System;

namespace CubicSplineInterpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Matrix matrix = new Matrix(3);
            Console.WriteLine(matrix);
            matrix[0, 0] = 1;
            matrix[1, 2] = 2;
            matrix[2, 2] = 3;
            Console.WriteLine(matrix);
            matrix.SwapRows(0, 1);
            Console.WriteLine(matrix);
            matrix[2, 2]++;
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

            Console.WriteLine(matrix);

            

        }
    }
}
