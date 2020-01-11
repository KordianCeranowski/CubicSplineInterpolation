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
/*                new Point(1,1),
                new Point(2,5),
                new Point(3,4)
*/
               new Point(1, 1),
                new Point(3, 6),
                new Point(5, 4),
                new Point(7, 9)
            };

            CSI csi = new CSI(list);

            Console.WriteLine(csi.matrix);
            Console.WriteLine(csi.vector);


            //csi.RunGaussSeidel();

            csi.RunGauss();


            //csi.printMatrix();
            Console.WriteLine(csi.vector.AsPolynomials());
        }
    }
}
