using System;
using System.Collections.Generic;
using static CubicSplineInterpolation.CSI;

namespace CubicSplineInterpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> list = new List<Point>
            {
                new Point(1,2),
                new Point(2,3),
                new Point(3,5),

                new Point(4, 1),
                new Point(5, 6),
                new Point(7, 4),
                new Point(8, 9)
            };



            //var generatedList = FunctionValuesGenerator.Generate(5, -5, 5);

            //CSI csi = new CSI(generatedList);

            CSI csi = new CSI(list);
            //csi.RunGaussSeidel();

            csi.RunGauss();

            csi.GetReport();

            
        }
    }
}
