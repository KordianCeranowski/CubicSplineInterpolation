using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CubicSplineInterpolation
{
    class Program
    {
        public static List<Point> generateFakePoints(int howMany)
        {
            var points = new List<Point>();
            for (int i = 0; i < howMany; i++)
            {
                var x = i * 0.118;
                var y = 3 * x * x * x * x - x*x*x -x*x + x + 3;
                points.Add(new Point(x, y));
            }
            return points;
        }

        static List<Point> googleMapsData = CSVtoPointList.Run(8);
        static List<Point> fake = generateFakePoints(50);

        static void Main(string[] args)
        {

            var s = Stopwatch.StartNew();
            CSI csi = new CSI(fake);
            csi.GenerateMfromSeidel();
            csi.GenerateMfromJacobi();
            Console.WriteLine(s.Elapsed);
        }
    }
}
