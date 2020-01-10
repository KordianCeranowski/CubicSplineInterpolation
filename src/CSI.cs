using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    class CSI
    {
        public class Point
        {
            readonly public double x;
            readonly public double y;

            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        readonly public static int VARIABLES_IN_POLYNOMIAL = 4;
        readonly int countOfPolynomials;

        public List<Point> points;
        public Matrix matrix;
        public double[] vector;

        int currentRow;

        public CSI(List<Point> points)
        {
            this.currentRow = 0;
            this.countOfPolynomials = points.Count - 1;

            this.points = points;

            this.matrix = new Matrix(VARIABLES_IN_POLYNOMIAL * countOfPolynomials);
            this.vector = new double[VARIABLES_IN_POLYNOMIAL * countOfPolynomials];
    }

        public void Run()
        {
            InsertFirstConditionEquasions();
            InsertSecondConditionEquasions();
            InsertThirdConditionEquasions();
            InsertFourthConditionEquasions();

            matrix.OrderRows();

            matrix.PerformGaussianElimination(vector);
        }

        //do zrefaktorowania
        private void InsertFirstConditionEquasions()
        {
            int currentPoint = 0;

            for (int currentPolynomial = 0; currentPolynomial < countOfPolynomials; currentPolynomial++)
            {
                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 0] = Math.Pow(points[currentPoint].x, 3);
                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 1] = Math.Pow(points[currentPoint].x, 2);
                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 2] = Math.Pow(points[currentPoint].x, 1);
                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 3] = Math.Pow(points[currentPoint].x, 0);

                vector[currentRow] = points[currentPoint].y;

                currentRow++;
                currentPoint++;

                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 0] = Math.Pow(points[currentPoint].x, 3);
                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 1] = Math.Pow(points[currentPoint].x, 2);
                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 2] = Math.Pow(points[currentPoint].x, 1);
                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 3] = Math.Pow(points[currentPoint].x, 0);

                vector[currentRow] = points[currentPoint].y;

                currentRow++;
            }
        }

        private void InsertSecondConditionEquasions()
        {
            int currentPoint = 1;

            for (int currentPolynomial = 0; currentPolynomial < countOfPolynomials - 1; currentPolynomial++)
            {
                int nextPolynomial = currentPolynomial + 1;

                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 0] = 3 * Math.Pow(points[currentPoint].x, 2);
                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 1] = 2 * Math.Pow(points[currentPoint].x, 1);
                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 2] = 1 * Math.Pow(points[currentPoint].x, 0);

                matrix[currentRow, nextPolynomial * VARIABLES_IN_POLYNOMIAL + 0] = -3 * Math.Pow(points[currentPoint].x, 2);
                matrix[currentRow, nextPolynomial * VARIABLES_IN_POLYNOMIAL + 1] = -2 * Math.Pow(points[currentPoint].x, 1);
                matrix[currentRow, nextPolynomial * VARIABLES_IN_POLYNOMIAL + 2] = -1 * Math.Pow(points[currentPoint].x, 0);

                vector[currentRow] = 0;

                currentPoint++;
                currentRow++;
            }
        }

        private void InsertThirdConditionEquasions()
        {
            int currentPoint = 1;

            for (int currentPolynomial = 0; currentPolynomial < countOfPolynomials - 1; currentPolynomial++)
            {
                int nextPolynomial = currentPolynomial + 1;

                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 0] = 6 * Math.Pow(points[currentPoint].x, 1);
                matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 1] = 2 * Math.Pow(points[currentPoint].x, 0);

                matrix[currentRow, nextPolynomial * VARIABLES_IN_POLYNOMIAL + 0] = -6 * Math.Pow(points[currentPoint].x, 1);
                matrix[currentRow, nextPolynomial * VARIABLES_IN_POLYNOMIAL + 1] = -2 * Math.Pow(points[currentPoint].x, 0);

                vector[currentRow] = 0;

                currentPoint++;
                currentRow++;
            }
        }

        private void InsertFourthConditionEquasions()
        {
            int currentPoint = 0;
            int currentPolynomial = 0;

            matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 0] = 6 * Math.Pow(points[currentPoint].x, 1);
            matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 1] = 2 * Math.Pow(points[currentPoint].x, 0);

            vector[currentRow] = 0;

            currentRow++;

            currentPoint = points.Count - 1;
            currentPolynomial = countOfPolynomials - 1;

            matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 0] = 6 * Math.Pow(points[currentPoint].x, 1);
            matrix[currentRow, currentPolynomial * VARIABLES_IN_POLYNOMIAL + 1] = 2 * Math.Pow(points[currentPoint].x, 0);

            vector[currentRow] = 0;
        }

        public void printVector()
        {
            string vectorString = "";
            foreach (var value in vector)
            {
                vectorString += value + " ";
            }
            Console.WriteLine(vectorString);
        }

        public void printMatrix()
        {
            Console.WriteLine(matrix);
        }

    }
}
