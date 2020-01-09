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

        readonly int VARIABLES_IN_POLYNOMIAL = 4;
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
    }
}
