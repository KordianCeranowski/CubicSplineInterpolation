﻿using System;
using System.Collections.Generic;

namespace CubicSplineInterpolation
{
    public class Matrix
    {
        private Dictionary<Tuple<int, int>, double> fields;
        public readonly int size;

        public override string ToString()
        {
            string matrixString = "";
            int MAX_SPACES = 4;

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    double val = this[row, col];
                    String valString = val.ToString();
                    matrixString += valString;
                    int spaces = MAX_SPACES - valString.Length;
                    for (int i = 0; i < spaces; i++)
                    {
                        matrixString += " ";
                    }
                }
                matrixString += "\n";
            }

            return matrixString;
        }

        public Matrix(int size)
        {
            fields = new Dictionary<Tuple<int, int>, double>();
            this.size = size;
        }

        public double this[int row, int col]
        {
            get => GetValue(row, col);
            set => SetValue(row, col, value);
        }

        private bool ContainsField(int row, int col)
        {
            return fields.ContainsKey(Tuple.Create(row, col));
        }

        private bool IsOutOfBounds(int row, int col)
        {
            return row < 0 || row >= size || col < 0 || col >= size;
        }

        private double GetValue(int row, int col)
        {
            var coordinates = Tuple.Create(row, col);
            if (!fields.ContainsKey(coordinates))
            {
                if (IsOutOfBounds(row, col))
                {
                    throw new System.IndexOutOfRangeException();
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return fields[coordinates];
            }
        }

        private void SetValue(int row, int col, double newValue)
        {
            if (IsOutOfBounds(row, col))
            {
                throw new System.IndexOutOfRangeException();
            }

            if (!ContainsField(row, col))
            {
                fields.Add(Tuple.Create(row, col), newValue);
                return;
            }

            if (newValue == 0)
            {
                fields.Remove(Tuple.Create(row, col));
                return;
            }
            else
            {
                fields[Tuple.Create(row, col)] = newValue;
                return;
            }
        }

        // Porządkowanie rzędów aby na przekątnych nie było zer
        public void OrderRows(ref Vector vector)
        {
            var newFields = new Dictionary<Tuple<int, int>, double>();
            var newVector = new Vector(vector.Length);

            int equasionsFromFirstCondition = size / 2;
            int equasionsFromSecondCondition = size / CSI.VARIABLES_IN_POLYNOMIAL - 1;
            int equasionsFromThirdCondition = equasionsFromSecondCondition;

            foreach (var field in fields)
            {
                int newRow = 0;
                int currentRow = field.Key.Item1;

                // Zapamiętać: Dictionary zapamiętuje kolejność wpisów. 

                // First condition
                if (currentRow < equasionsFromFirstCondition)
                {
                    if (currentRow % 2 == 0)
                    {
                        newRow = currentRow * 2 + 2;
                    }
                    else
                    {
                        newRow = currentRow * 2 + 1;
                    }
                }
                // Second condition
                else if (currentRow < equasionsFromFirstCondition + equasionsFromSecondCondition)
                {
                    newRow = (currentRow - equasionsFromFirstCondition + 1) * CSI.VARIABLES_IN_POLYNOMIAL;
                }
                // Third condition
                else if (currentRow < equasionsFromFirstCondition + equasionsFromSecondCondition + equasionsFromThirdCondition)
                {
                    newRow = (currentRow - equasionsFromFirstCondition - equasionsFromSecondCondition) * CSI.VARIABLES_IN_POLYNOMIAL + 1;
                }
                // Fourth condition
                else if (currentRow == size - 2)
                {
                    newRow = 0;
                }
                else if (currentRow == size - 1)
                {
                    newRow = size - 3;
                }
                else
                {
                    throw new System.IndexOutOfRangeException();
                }

                newFields.Add(Tuple.Create(newRow, field.Key.Item2), field.Value);
                newVector[newRow] = vector[currentRow];

            }

            this.fields = newFields;
            vector = newVector;

        }

        // does not swap zeroes
        private void SwapRows(int rowOne, int rowTwo)
        {
            for (int col = 0; col < size; col++)
            {
                bool rowOneExists = this.ContainsField(rowOne, col);
                bool rowTwoExists = this.ContainsField(rowTwo, col);

                if (!rowOneExists && !rowTwoExists)
                {
                    continue;
                }

                if (rowOneExists && rowTwoExists)
                {
                    double temp = this[rowOne, col];
                    this[rowOne, col] = this[rowTwo, col];
                    this[rowTwo, col] = temp;
                    continue;
                }

                if (rowOneExists) // and rowTwo does not
                {
                    this[rowTwo, col] = this[rowOne, col];
                    this[rowOne, col] = 0;
                    continue;
                }
                else // rowTwo exists and rowOne does not exist
                {
                    this[rowOne, col] = this[rowTwo, col];
                    this[rowTwo, col] = 0;
                    continue;
                }
            }
        }

        public void GaussianElimination(Vector vector)
        {
            if (vector.Length != this.size)
            {
                throw new System.Exception("Wprowadzono wektor o złym rozmiarze");
            }

            for (int i = 0; i < size; i++)
            {
                PartialChoice(i, i, vector);

                for (int j = i + 1; j < size; j++)
                {
                    double zerowany = this[j, i];
                    double zerujacy = this[i, i];
                    double multiplier = zerowany / zerujacy;
                    for (int col = i; col < size; col++)
                    {
                        zerujacy = this[i, col];
                        this[j, col] -= zerujacy * multiplier;
                    }

                    vector[j] -= vector[i] * multiplier;

                }
            }

            BackwardsOperation(vector);

        }

        private void PartialChoice(int row, int column, Vector vector)
        {
            int rowToSwap = FindMaxInRows(row, column);
            SwapRows(row, rowToSwap);

            //swaping rows in vector;
            double temp = vector[row];
            vector[row] = vector[rowToSwap];
            vector[rowToSwap] = temp;

        }

        private int FindMaxInRows(int startRow, int column)
        {
            double maxValue = this[startRow, column];
            maxValue = Math.Abs(maxValue);
            int rowOfMaxValue = startRow;

            for (int row = startRow + 1; row < size; row++)
            {
                double var = this[row, column];
                var = Math.Abs(var);
                if (var > maxValue)
                {
                    maxValue = var;
                    rowOfMaxValue = row;
                }
            }

            return rowOfMaxValue;
        }

        private void BackwardsOperation(Vector vector)
        {
            double q; // mnożnik dla danego miejsca w wektorze, tj dla 3 pozycji w wektorze to będzie punkt [3,3] w macierzy
            double x; // wartość w danym miejscu w wektorze
            double d; // mnożnik dla aktualnie wyliczanego miejsca w wektorze

            for (int row = size - 1; row >= 0; row--)
            {
                for (int i = row + 1; i < size; i++)
                {
                    q = this[row, i];
                    x = vector[i];
                    vector[row] -= q * x;
                }

                d = this[row, row];
                vector[row] /= d;
            }
        }


        public void GaussSeidel(ref Vector vector)
        {
            var lastVector = new Vector(vector.Length);
            var currentVector = new Vector(vector.Length);

            double difference = 1;

            while (difference > 0.001)
            {
                for (int i = 0; i < currentVector.Length; i++)
                {
                    double sum = 0;

                    sum += vector[i];

                    for (int j = 0; j < size; j++)
                    {
                        if (i != j)
                        {
                            sum -= this[i, j] * lastVector[j];
                        }
                    }

                    var dividor = this[i, i];
                    currentVector[i] = sum / dividor;

                }

                difference = CountDifference(currentVector, lastVector);

                Console.WriteLine(currentVector); ;
                Console.WriteLine(lastVector);
                Console.WriteLine("");

                for (int i = 0; i < currentVector.Length; i++)
                {
                    lastVector[i] = currentVector[i];
                }

            }

            vector = currentVector;

        }

        private double CountDifference(Vector current, Vector last)
        {
            double sum = 0;

            for (int i = 0; i < current.Length; i++)
            {
                sum += Math.Abs(current[i] - last[i]);
            }

            Console.WriteLine("XD: " + sum);

            return sum;
        }

    }
}
