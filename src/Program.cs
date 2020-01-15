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

        static List<Point> criticalPoints = new List<Point>
        {
            new Point(2, 75),
            new Point(11, 9),
            new Point(54, -15),
            new Point(98, 272)
        };

        static List<Point> googleMapsData = CSVtoPointList.Run();

        static void Main(string[] args)
        {
            CSI csi = new CSI(samplePoints);
            csi.GenerateMFromSeidel();
            csi.Print(100);
        }
    }
}
