﻿using System;

namespace CubicSplineInterpolation
{
    class Seidel
    {
        private readonly double DIFFERENCE = 0.001;
        private readonly Matrix matrix;
        private readonly Vector vector;

        public Seidel(Matrix matrix, Vector vector)
        {
            this.matrix = matrix;
            this.vector = vector;
        }

        public Vector Run()
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
            while (difference > DIFFERENCE);

            return currentVector;

        }
    }
}