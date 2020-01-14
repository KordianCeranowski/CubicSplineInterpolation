using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    class Jacobi
    {
        Matrix A;
        Vector B;
        Vector X;
        Vector oldX;

        Matrix LplusU;
        Matrix DtoMinus1;
        Matrix Tj;
        Vector Fj;

        public Jacobi(Matrix A, Vector B)
        {
            this.A = A;
            this.B = B;

            this.X = new Vector(this.B.Length);
            this.oldX = new Vector(this.B.Length);

            this.DtoMinus1 = new Matrix(A.size);
            FillDtoMinus1();
            this.LplusU = new Matrix(A.size);
            FillLplusU();

            this.Tj = DtoMinus1 * LplusU;

            this.Fj = DtoMinus1 * B;

            Run();
        }

        private void FillDtoMinus1()
        {
            for (int i = 0; i < A.size; i++)
            {
                DtoMinus1[i, i] = 1d / A[i, i];
            }
        }

        private void FillLplusU()
        {
            foreach (var kvp in A.fields)
            {
                if (kvp.Key.Item1 != kvp.Key.Item2)
                {
                    LplusU.fields.Add(kvp.Key, kvp.Value);
                }
            }
        }

        private void Run()
        {
            int iterations = 10;
            for (int i = 0; i < iterations; i++)
            {
                X = Tj * oldX + Fj;
                oldX = X;
                Console.WriteLine(X);
            }
        }



        public static Vector JacobiProcedure(Matrix matrix, Vector vector)
        {
            var lastVector = new Vector(vector.Length);
            var currentVector = new Vector(vector.Length);

            double difference;

            do
            {
                Console.WriteLine(currentVector);
                for (int row = 0; row < matrix.size; row++)
                {
                    double sum = 0;
                    sum += vector[row];

                    for (int col = 0; col < matrix.size; col++)
                    {
                        if (row != col)
                        {
                            sum -= matrix[row, col] * lastVector[col];
                        }
                    }

                    currentVector[row] = sum / matrix[row, row];
                }

                difference = (currentVector - lastVector).GetNorm();
                lastVector.CopyValuesFrom(currentVector);
            }
            while (difference > 0.001);

            return currentVector;

        }
    }
}
