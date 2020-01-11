using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    class Point
    {
        readonly public double x;
        readonly public double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}
