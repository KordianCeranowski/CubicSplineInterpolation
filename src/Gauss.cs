using System;

namespace CubicSplineInterpolation
{
    class Gauss
    {
        private static bool OUTPUT_ON = false;

        private readonly Matrix matrix;
        private readonly Vector vector;

        public Gauss(Matrix matrix, Vector vector)
        {
            if (vector.Length != matrix.size)
            {
                throw new System.Exception("Wprowadzono wektor o złym rozmiarze");
            }

            this.matrix = matrix.Clone();
            this.vector = vector.Clone();
        }

        public Vector RunWithPercentMeter()
        {
            OUTPUT_ON = true;
            return Run();
        }

        public Vector Run()
        {
            for (int i = 0; i < matrix.size; i++)
            {
                PrintProgress(i);
                PartialChoice(i, i);

                for (int j = i + 1; j < matrix.size; j++)
                {
                    double zerowany = matrix[j, i];
                    double zerujacy = matrix[i, i];
                    double multiplier = zerowany / zerujacy;
                    for (int col = i; col < matrix.size; col++)
                    {
                        zerujacy = matrix[i, col];
                        matrix[j, col] -= zerujacy * multiplier;
                    }

                    vector[j] -= vector[i] * multiplier;

                }
            }

            return BackwardsOperation();
        }

        private void PartialChoice(int row, int column)
        {
            int rowToSwapWith = FindMaxInRows(row, column);

            matrix.SwapRows(row, rowToSwapWith);
            vector.SwapRows(row, rowToSwapWith);
        }

        private int FindMaxInRows(int startRow, int column)
        {
            double maxValue = matrix[startRow, column];
            maxValue = Math.Abs(maxValue);
            int rowOfMaxValue = startRow;

            for (int row = startRow + 1; row < matrix.size; row++)
            {
                double var = matrix[row, column];
                var = Math.Abs(var);
                if (var > maxValue)
                {
                    maxValue = var;
                    rowOfMaxValue = row;
                }
            }

            return rowOfMaxValue;
        }

        private Vector BackwardsOperation()
        {
            double q; // mnożnik dla danego miejsca w wektorze, tj dla 3 pozycji w wektorze to będzie punkt [3,3] w macierzy
            double x; // wartość w danym miejscu w wektorze
            double d; // mnożnik dla aktualnie wyliczanego miejsca w wektorze

            for (int row = matrix.size - 1; row >= 0; row--)
            {
                for (int i = row + 1; i < matrix.size; i++)
                {
                    q = matrix[row, i];
                    x = vector[i];
                    vector[row] -= q * x;
                }

                d = matrix[row, row];
                vector[row] /= d;
            }

            return vector;
        }

        private void PrintProgress(int iteration)
        {
            if (OUTPUT_ON)
            {
                Console.Write($"\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\bPostęp Gaussa: {Math.Round(iteration * 100d / matrix.size, 2)}%");
            }
        }
    }
}
