using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    class FunctionValuesGenerator
    {
        private static double F(double x)
        {
            return x;
        }

        public static List<Point> Generate(int howMany, double rangeStart, double rangeStop)
        {
            if (rangeStart >= rangeStop)
                throw new ArgumentException();

            var points = new List<Point>();
            Random random = new Random();

            double range = rangeStop - rangeStart;

            for (int i = 0; i < howMany; i++)
            {
                double x = rangeStart + range * i / howMany;
                double y = F(x) + random.NextDouble() * 2 - 1;
                points.Add(new Point(x, y));
            }

            return points;
        }
    }
}
