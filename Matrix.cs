﻿using System;
using System.Collections.Generic;

namespace CubicSplineInterpolation
{
    internal class Matrix
    {
        public Dictionary<Tuple<int, int>, double> fields;
        public int size;

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

        public bool ContainsField(int row, int col)
        {
            return !fields.ContainsKey(Tuple.Create(row, col));
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
                // if argument is out of bounds
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

        // longer but more effective approach
        // does not swap zeroes
        public void SwapRows(int rowOne, int rowTwo)
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
                }
                else // rowTwo exists and rowOne does not exist
                {
                    this[rowOne, col] = this[rowTwo, col];
                    this[rowTwo, col] = 0;
                }
            }
        }

    }
}
