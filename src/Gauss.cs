using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    class Gauss
    {
        private static readonly bool OUTPUT_ON = false;

        public static void GaussianElimination(Matrix matrix, Vector vector)
        {
            if (vector.Length != matrix.size)
            {
                throw new System.Exception("Wprowadzono wektor o złym rozmiarze");
            }

            for (int i = 0; i < matrix.size; i++)
            {
                if (OUTPUT_ON)
                {
                    Console.Write($"\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\bPostęp Gaussa: {Math.Round(i * 100d / matrix.size, 2)}%");
                }
                    
                PartialChoice(matrix, i, i, vector);

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

            Console.Write("\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b");
            BackwardsOperation(matrix, vector);

        }

        private static void PartialChoice(Matrix matrix, int row, int column, Vector vector)
        {
            int rowToSwapWith = FindMaxInRows(matrix, row, column);

            matrix.SwapRows(row, rowToSwapWith);
            vector.SwapRows(row, rowToSwapWith);
        }

        private static int FindMaxInRows(Matrix matrix, int startRow, int column)
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

        private static void BackwardsOperation(Matrix matrix, Vector vector)
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
        }
    }
}
