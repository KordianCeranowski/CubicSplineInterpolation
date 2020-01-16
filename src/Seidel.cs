using System;

namespace CubicSplineInterpolation
{
    class Seidel
    {
        private readonly double DIFFERENCE = 0.001;
        private readonly Matrix matrix;
        private readonly Vector vector;

        public Seidel(Matrix matrix, Vector vector)
        {
            this.matrix = matrix.Clone();
            this.vector = vector.Clone();
        }

        public Vector Run()
        {
            var lastVector = new Vector(vector.Length);
            var currentVector = new Vector(vector.Length);

            double difference;
            int temp = 0;

            do
            {
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
                temp++;
            }
            while (difference > DIFFERENCE);
            Console.WriteLine(temp);
            return currentVector;
        }
    }
}
