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
        }

        public void SetUp()
        {
            this.matrix = new Matrix(3);
            FillMatrix();
            this.vector = new Vector(new double[3] { 23, 134, 19 });
        }

        private void FillMatrix()
        {
            var table = new double[9] 
            { 
              1, -3,  4,
             16,  6,  7,
             -5, 10,  4
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = table[i * 3 + j];
                }
            }
        }

        public void multiplicationTest()
        {
            Matrix m1 = new Matrix(3);
            m1[0, 0] = 2;
            m1[1, 1] = 3;
            m1[2, 2] = 4;
            Matrix m2 = new Matrix(3);
            m2[0, 0] = 1 / 2d;
            m2[1, 1] = 1 / 3d;
            m2[2, 2] = 1 / 4d;

            var m3 = m1 * m2;

            Console.WriteLine(m3);
        }

        public void TestGauss()
        {
            SetUp();
            Gauss gauss = new Gauss(matrix, vector);
            var newVector = gauss.Run();
            Console.WriteLine(newVector);
        }

        public void TestSiedel()
        {
        }

        public void TestJacobi()
        {
        }

    }
}
