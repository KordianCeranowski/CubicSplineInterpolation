using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    class Test
    {
        Matrix matrix;
        Vector vector;

        public Test()
        {
            this.matrix = new Matrix(3);
            this.vector = new Vector(new double[3] { 23, 134, 19 });
            FillMatrix();
        }

        private void FillMatrix()
        {
            var table = new double[9] 
            { 
              1, -3, 4,
             16,  6, 7,
             -5, 10, 4
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = table[i * 3 + j];
                }
            }
        }

        public void TestGauss()
        {
            Console.WriteLine(Gauss.GaussianElimination(matrix, vector));
        }

        public void TestSiedel()
        {
            //Console.WriteLine(GaussSeidel.GaussSeidelProcedure(matrix, vector));
            Console.WriteLine(GaussSeidel.Run(matrix, vector));
        }

        public void TestJacobi()
        {
            Console.WriteLine(Jacobi.JacobiProcedure(matrix, vector));
        }

    }
}
