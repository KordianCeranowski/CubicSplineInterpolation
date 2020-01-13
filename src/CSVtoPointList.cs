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
            return Run(260);
        }
        public static List<Point> Run(int targetSize)
        {
            var points = new List<Point>();

            using (var reader = new StreamReader(@"..\..\..\google API\data.csv"))
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
            return points;
        }

        public static List<Point> StretchDiagonaly(List<Point> points, double factor)
        {
            var newPoints = new List<Point>();

            foreach (var point in points)
            {
                newPoints.Add(new Point(point.x * factor, point.y));
            }

            return newPoints;
        }

    }
}
