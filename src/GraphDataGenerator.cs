using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation.src
{
    class GraphDataGenerator
    {

        List<Point> points;


        public List<Point> Generate(List<Point> points, Vector polynomials, int intervalsBetweenPoints)
        {
            this.points = points;
            List<Point> graph = new List<Point>();



            return graph;
        }

        private int WhichPolynomialDescribes(double x)
        {
            for (int i = 1; i < points.Count; i++)
            {
                if(x < points[i].x)
                {
                    return i - 1;
                }
            }
            throw new IndexOutOfRangeException();
        }



    }
}
