using System;
using System.Collections.Generic;

namespace CubicSplineInterpolation
{
    public class Matrix
    {
        public Dictionary<Tuple<int, int>, double> fields;
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


        // Mnożenie macierzy
        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left.size != right.size)
                throw new ArgumentException("Matrixes need to have same number of rows.");

            Matrix matrix = new Matrix(left.size);

            int multiplications = left.size;
            double val;

            for (int row = 0; row < matrix.size; row++)
            {
                for (int col = 0; col < matrix.size; col++)
                {
                    for (int i = 0; i < multiplications; i++)
                    {
                        val = left[row, i];
                        val *= right[i, col];
                        matrix[row, col] += val;
                    }
                }
            }

            return matrix;
        }

        public static Vector operator *(Matrix matrix, Vector vector)
        {
            Vector resultVector = new Vector(vector.Length);

            for (int row = 0; row < vector.Length; row++)
            {
                resultVector[row] = 0;

                for (int col = 0; col < matrix.size; col++)
                {
                    resultVector[row] += matrix[row, col] * vector[col];
                }

            }

            return resultVector;
        }


        // Does not swap zeroes
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

        public Matrix Clone()
        {
            Matrix clone = new Matrix(size);
            foreach (var kvp in this.fields)
            {
                clone.fields.Add(kvp.Key, kvp.Value);
            }
            return clone;
        }

    }
}
