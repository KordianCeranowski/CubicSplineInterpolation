using System;
using System.Collections.Generic;

namespace CubicSplineInterpolation
{
    class CSI
    {
        readonly public static int VARIABLES_IN_POLYNOMIAL = 4;

        readonly int countOfPolynomials;
        readonly public List<Polynomial> polynomials;

        public List<Point> points;
        public Matrix matrix;
        public Vector vector;
        public Vector mVector;

        public CSI(List<Point> points)
        {
            int n = points.Count;

            this.countOfPolynomials = n - 1;
            this.polynomials = new List<Polynomial>();

            this.points = points;

            this.matrix = new Matrix(n);
            this.vector = new Vector(n);
            

            FillSystemOfEquasions();

            this.mVector = generateMfromGauss();

            FillPolynomialsList();
        }


        //sposób z wykładu
        private void FillSystemOfEquasions()
        {
            matrix[0, 0] = 2;
            for (int row = 1; row < matrix.size - 1; row++)
            {
                FillRow(row);
            }
            matrix[matrix.size - 1, matrix.size - 1] = 2;
        }

        private void FillRow(int row)
        {
            matrix[row, row - 1] = Mi(row);
            matrix[row, row] = 2;
            matrix[row, row + 1] = Lambda(row);
            vector[row] = Delta(row);
        }

        private double Delta(int j)
        {
            double partOne = 6 / (H(j) + H(j + 1));
            double partTwo = (Fx(j + 1) - Fx(j)) / H(j + 1);
            double partThree = (Fx(j) - Fx(j - 1)) / H(j);

            return partOne * (partTwo - partThree);
        }

        private double Fx(int j)
        {
            return points[j].y;
        }

        private double Lambda(int j)
        {
            return H(j + 1) / (H(j) + H(j + 1));
        }

        private double Mi(int j)
        {
            return H(j) / (H(j) + H(j + 1));
        }

        private double H(int j)
        {
            return points[j].x - points[j - 1].x;
        }





        private void FillPolynomialsList()
        {
            for (int i = 0; i < countOfPolynomials; i++)
            {
                Polynomial polynomial = GeneratePolynomial(i);
                polynomials.Add(polynomial);
            }
        }

        private Polynomial GeneratePolynomial(int j)
        {
            return new Polynomial(A(j), B(j), C(j), D(j));
        }

        private double A(int j)
        {
            return Fx(j);
        }

        private double B(int j)
        {
            double partOne = (Fx(j + 1) - Fx(j)) / H(j + 1);
            double partTwo = (2 * M(j) + M(j + 1)) / 6;

            return partOne - partTwo * H(j + 1);
        }

        private double C(int j)
        {
            return M(j) / 2;
        }

        private double D(int j)
        {
            double upperPart = M(j + 1) - M(j);
            double lowerPart = 6 * H(j + 1);
            return upperPart / lowerPart;
        }

        private double M(int j)
        {
            return mVector[j];
        }


        

        public void Print(int howMany)
        {
            var start = points[0].x;
            var stop = points[points.Count - 1].x;
            var jump = (stop - start) / howMany;
            for (double x = start; x < stop; x += jump)
            {
                Console.WriteLine(new Point(x, S(x)));
            }
        }

        private double S(double x)
        {
            int i = GetIndexX(x);

            double diff = x - points[i].x;

            double value =  A(i) +
                            B(i) * diff +
                            C(i) * diff * diff +
                            D(i) * diff * diff * diff;

            return value;

        }

        private int GetIndexX(double x)
        {
            for (int i = 1; i < points.Count; i++)
            {
                if (x <= points[i].x)
                {
                    return i - 1;
                }
            }
            throw new Exception("Podano wartość spoza oszacowanych wielomianów");
        }


        // Paste output into https://www.desmos.com/calculator
        public void GetReport()
        {
            foreach (var point in points)
            {
                Console.WriteLine(point);
            }
            Console.WriteLine(vector.AsPolynomials());
        }

        public void GenerateCSV(int intervals)
        {
            new GraphExporter(this, intervals);
        }

        private Vector generateMfromGauss()
        {
            return new Gauss(matrix, vector).Run();
        }

    }
}
