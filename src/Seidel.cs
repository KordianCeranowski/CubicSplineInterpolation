using System;

namespace CubicSplineInterpolation
{
    class Seidel
    {
        public static Vector GaussSeidelProcedure(Matrix matrix, Vector vector)
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
                            sum -= matrix[row, col] * currentVector[col];
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

        //wersja robertowa
        public static Vector Run(Matrix inputMatrix, Vector expectedOutcome)
        {
            int iterations = 0;
            var size = expectedOutcome.Length;
            var solvedVector = new Vector(size);
            double normValue;
            var tolerance = 0.001;

            do
            {
                var oldVector = new Vector(size);
                oldVector.CopyValuesFrom(solvedVector);

                for (int i = 0; i < size; i++)
                {
                    double sigma = 0;

                    for (int j = 0; j < i - 1; j++)
                    {
                        sigma += inputMatrix[i, j] * solvedVector[j];
                    }

                    for (int j = i; j < size; j++)
                    {
                        sigma += inputMatrix[i, j] * oldVector[j];
                    }

                    solvedVector[i] = (1 / inputMatrix[i, i] * expectedOutcome[i] - sigma);
                }
                iterations++;

                normValue = (oldVector - solvedVector).GetNorm();
            }
            while (normValue > tolerance);

            return solvedVector;
        }
    }
}
