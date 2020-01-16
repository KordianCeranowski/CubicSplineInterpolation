using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    class HidingTest
    {
        public CSI csi;

        Point[] allPoints;
        List<Point> showingPoints;
        List<Point> hiddenPoints;
        double percentToShow;

        public HidingTest(List<Point> points, double percentToShow)
        {
            this.allPoints = new Point[points.Count];
            points.CopyTo(allPoints);
            this.showingPoints = new List<Point>();
            this.hiddenPoints = new List<Point>();
            this.percentToShow = percentToShow;

            RemovePartOfPoints();

            this.csi = new CSI(showingPoints);
        }

        public double GetRealError()
        {
            double sumOfErrors = 0;
            foreach (var point in hiddenPoints)
            {
                var realValue = point.y;
                var approximatedValue = csi.S(point.x);

                var error = Math.Abs((realValue - approximatedValue));
                sumOfErrors += error;
            }

            return sumOfErrors / hiddenPoints.Count;
        }

        public double GetAverageError()
        {
            double sumOfErrors = 0;
            foreach (var point in hiddenPoints)
            {
                var realValue = point.y;
                var approximatedValue = csi.S(point.x);

                var error = Math.Abs((realValue - approximatedValue) / realValue);
                sumOfErrors += error;
            }

            return sumOfErrors / hiddenPoints.Count;
        }

        public void RunGauss()
        {
            this.csi.GenerateMfromGauss();
        }

        public void RunSeidel()
        {
            this.csi.GenerateMfromSeidel();
        }

        public void RunJacobi()
        {
            this.csi.GenerateMfromJacobi();
        }

        private void RemovePartOfPoints()
        {
            double percent = 0;

            if (!showingPoints.Contains(allPoints[0]))
            {
                showingPoints.Add(allPoints[0]);
            }

            foreach (Point point in allPoints)
            {
                percent += percentToShow;
                if (percent < 100)
                {
                    hiddenPoints.Add(point);
                }
                else
                {
                    showingPoints.Add(point);
                    percent -= 100;
                }
            }
            
            if (!showingPoints.Contains(allPoints[allPoints.Length - 1]))
            {
                showingPoints.Add(allPoints[allPoints.Length - 1]);
            }
        }
        
        public void PrintReal()
        {
            foreach (var item in allPoints)
            {
                Console.WriteLine(item);
            }
        }

        public void PrintFound()
        {
            csi.Print(allPoints.Length);
        }
    }
}
