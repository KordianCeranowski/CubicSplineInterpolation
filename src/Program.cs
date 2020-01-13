using System;
using System.Collections.Generic;
using System.Diagnostics;
using static CubicSplineInterpolation.CSI;

namespace CubicSplineInterpolation
{
    class Program
    {
        static void Main(string[] args)
        {

            //new Test().TestSiedel();

            Stopwatch timer = new Stopwatch();

            List<Point> list = new List<Point>
            {
                new Point(1, 2),
                new Point(2, 3),
                new Point(3, 5),
                new Point(4, 1),
                new Point(5, 6),
                new Point(7, 4),
                new Point(8, 9)
            };

            var test = CSVtoPointList.Run();
            test = CSVtoPointList.StretchDiagonaly(test, 1000);

            timer.Start();
            CSI csi = new CSI(list);

            Console.WriteLine(csi.matrix);

            //csi.RunGaussSeidel();
            csi.RunGauss();

            timer.Stop();

            csi.GetReport();


            var seconds = timer.ElapsedMilliseconds / 1000;

            Console.WriteLine("// " + test.Count + " -> " + seconds + "." + (timer.ElapsedMilliseconds - seconds * 1000) + " s");
        }
    }
}

// 10 -> 0.113 s
// 20 -> 0.747 s
// 40 -> 4.429 s
// 80 -> 33.924 s

// RELEASE
// 10 -> 0.112 s
// 20 -> 0.427 s
// 40 -> 3.550 s
// 80 -> 29.448 s

// I BEZ AVASTA
// 10 -> 0.103 s
// 20 -> 0.402 s
// 40 -> 3.126 s
// 80 -> 27.468 s
// 160 -> 354.786 s


// ale z printem
// 80 -> 27.977 s
// 256 -> 2368.679 s