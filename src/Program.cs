using System;
using System.Collections.Generic;
using static CubicSplineInterpolation.CSI;

namespace CubicSplineInterpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(1, 1));
            list.Add(new Point(2, 5));
            list.Add(new Point(3, 4));
            /*list.Add(new Point(4, 8));
            list.Add(new Point(5, 5));
            list.Add(new Point(43, 8));
            list.Add(new Point(433, 5));*/


            CSI csi = new CSI(list);

            csi.Run();

            csi.printMatrix();

            csi.printVector();


        }
    }
}
