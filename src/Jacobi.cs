using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    class Jacobi
    {
        public static void JacobiProcedure(Matrix matrix, Vector vector)
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

            vector.CopyValuesFrom(currentVector);

        }
    }
}
