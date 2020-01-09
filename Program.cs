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
        }
    }
}
