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
        private readonly int intervalsBetweenNewPoints;
        private readonly List<Point> newPoints;

        public GraphExporter(CSI csi, int intervalsBetweenNewPoints)
        {
            this.inputPoints = csi.points;
            this.intervalsBetweenNewPoints = intervalsBetweenNewPoints;
            this.polynomials = csi.vector.ToPolynomials();
            this.newPoints = new List<Point>();

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
            double interval = LengthOfInterval();
            double rangeStart = inputPoints[0].x;

            for (int intervalCount = 0; intervalCount < intervalsBetweenNewPoints; intervalCount++)
            {
                double x = rangeStart + interval * intervalCount;
                double y = CountYinPoint(x);
                Point point = new Point(x, y);
                newPoints.Add(point);
            }
        }

        private double LengthOfInterval()
        {
            double distanceBetweenPoints = inputPoints[inputPoints.Count - 1].x - inputPoints[0].x;
            return distanceBetweenPoints / intervalsBetweenNewPoints;
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
            string graphPointsCSV = PointsToCSV(newPoints);
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
