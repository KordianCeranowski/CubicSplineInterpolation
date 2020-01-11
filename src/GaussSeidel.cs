using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    class GaussSeidel
    {
        public static void GaussSeidelProcedure(Matrix matrix, Vector vector)
        {
            var lastVector = new Vector(vector.Length);
            var currentVector = new Vector(vector.Length);

            double difference = 1;

            while (difference > 0.001)
            {
                Console.WriteLine(currentVector);
                for (int i = 0; i < matrix.size; i++)
                {
                    double sum = 0;
                    sum += vector[i];

                    for (int j = 0; j < matrix.size; j++)
                    {
                        if (i != j)
                        {
                            sum -= matrix[i, j] * currentVector[j];
                        }
                    }

                    var dividor = matrix[i, i];
                    currentVector[i] = sum / dividor;
                }

                difference = CountDifference(currentVector, lastVector);
                lastVector.CopyValuesFrom(currentVector);
            }

            vector.CopyValuesFrom(currentVector);

        }

        private static double CountDifference(Vector current, Vector last)
        {
            double sum = 0;

            for (int i = 0; i < current.Length; i++)
            {
                sum += Math.Abs(current[i] - last[i]);
            }

            Console.WriteLine("Difference: " + sum);

            return sum;
        }
    }
}
