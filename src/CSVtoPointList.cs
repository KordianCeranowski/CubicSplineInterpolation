using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CubicSplineInterpolation
{
    class CSVtoPointList
    {
        public static List<Point> Run()
        {
            return Run(21143478);
        }
        public static List<Point> Run(int targetSize)
        {
            var points = new List<Point>();

            using (var reader = new StreamReader(@"..\..\..\google API\512.txt"))
            {
                int counter = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    points.Add(new Point(Double.Parse(values[0]), Double.Parse(values[1])));

                    counter++;
                    if (counter >= targetSize)
                    {
                        break;
                    }
                }
            }
            return ToMeters(points);
        }

        private static List<Point> ToMeters(List<Point> points)
        {
            var newPoints = new List<Point>();

            foreach (var point in points)
            {
                newPoints.Add(new Point(point.x * 1000, point.y));
            }

            return newPoints;
        }

        private static List<Point> ToKilometers(List<Point> points)
        {
            var newPoints = new List<Point>();

            foreach (var point in points)
            {
                newPoints.Add(new Point(point.x, point.y / 1000));
            }

            return newPoints;
        }
    }
}
