using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    class GraphExporter
    {
        public static char SEPARATOR = ';';
        private static readonly string path = @"..\..\..\output\";
        private readonly List<Point> inputPoints;
        private readonly List<Polynomial> polynomials;
        private readonly int intervalsBetweenInputPoints;
        private readonly List<Point> graphPoints;

        public GraphExporter(CSI csi, int intervalsBetweenInputPoints)
        {
            this.inputPoints = csi.points;
            this.intervalsBetweenInputPoints = intervalsBetweenInputPoints;
            this.polynomials = csi.vector.ToPolynomials();
            this.graphPoints = new List<Point>();

            if (polynomials.Count != inputPoints.Count - 1)
            {
                throw new Exception("Zła liczba punktów lub wielomianów");
            }

            CreateGraph();
            GenerateCSVFiles();
        }

        #region Creating points

        private void CreateGraph()
        {
            for (int i = 0; i < polynomials.Count; i++)
            {
                graphPoints.Add(inputPoints[i]);
                AddPointsFromPolynomial(i);
            }
            int lastPointIndex = inputPoints.Count - 1;
            graphPoints.Add(inputPoints[lastPointIndex]);
        }

        private void AddPointsFromPolynomial(int numberOfPolynomial)
        {
            double interval = LengthOfIntervalOnPolynomial(numberOfPolynomial);
            double rangeStart = inputPoints[numberOfPolynomial].x;

            for (int intervalCount = 1; intervalCount < intervalsBetweenInputPoints; intervalCount++)
            {
                double x = rangeStart + interval * intervalCount;
                double y = CountYinPoint(x);
                Point point = new Point(x, y);
                graphPoints.Add(point);
            }
        }

        private double LengthOfIntervalOnPolynomial(int numberOfPolynomial)
        {
            double distanceBetweenPoints = inputPoints[numberOfPolynomial + 1].x - inputPoints[numberOfPolynomial].x;
            return distanceBetweenPoints / intervalsBetweenInputPoints;
        }

        private double CountYinPoint(double x)
        {
            int indexOfPolynomial = WhichPolynomialDescribes(x);
            double Y = 0;
            Y += polynomials[indexOfPolynomial].A * Math.Pow(x, 3);
            Y += polynomials[indexOfPolynomial].B * Math.Pow(x, 2);
            Y += polynomials[indexOfPolynomial].C * Math.Pow(x, 1);
            Y += polynomials[indexOfPolynomial].D * Math.Pow(x, 0);
            return Y;
        }

        private int WhichPolynomialDescribes(double x)
        {
            for (int i = 1; i < inputPoints.Count; i++)
            {
                if (x < inputPoints[i].x)
                {
                    return i - 1;
                }
            }
            throw new IndexOutOfRangeException();
        }

        #endregion

        #region Saving to files

        private void GenerateCSVFiles()
        {
            WriteGraphToFile();
            WriteInputPointsToFile();
        }

        private void WriteGraphToFile()
        {
            string graphPointsCSV = PointsToCSV(graphPoints);
            System.IO.File.WriteAllText(path + "GraphPoints.csv", graphPointsCSV);
        }

        private void WriteInputPointsToFile()
        {
            string inputPointsCSV = PointsToCSV(inputPoints);
            System.IO.File.WriteAllText(path + "InputPoints.csv", inputPointsCSV);
        }

        private string PointsToCSV(List<Point> points)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("x; y\n");

            foreach (Point point in points)
            {
                builder.Append(point.ToStringForCSV()).Append("\n");
            }
            return builder.ToString().TrimEnd(new char[] { SEPARATOR });
        }

        #endregion
    }
}
