using System.Collections.Generic;
namespace CubicSplineInterpolation
{
    class Program
    {
        static List<Point> samplePoints = new List<Point>
        {
            new Point(1, 2),
            new Point(2, 3),
            new Point(3, 5),
            new Point(4, 1),
            new Point(5, 6),
            new Point(7, 4),
            new Point(8, 9)
        };

        static List<Point> pawla = new List<Point>
        {
            new Point(0, 0),
            new Point(1, 0.5),
            new Point(2, 2),
            new Point(3, 1.5)
        };

        static List<Point> criticalPoints = new List<Point>
        {
            new Point(2, 75),
            new Point(11, 9),
            new Point(54, -15),
            new Point(98, 272)
        };

        static List<Point> googleMapsData = CSVtoPointList.Run(0);

        static void Main(string[] args)
        {
            //new Test().TestGauss();

            CSI csi = new CSI(samplePoints);
            csi.GenerateMFromSiedel();
            csi.Print(100);


            //System.Console.WriteLine(csi.matrix);
            //System.Console.WriteLine(csi.vector);

            //csi.Print(1000);

            //csi.RunGauss();
        }
    }
}
