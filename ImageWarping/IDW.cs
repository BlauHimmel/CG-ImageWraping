using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageWarping
{
    class IDW
    {
        private List<Point> startPoints;
        private List<Point> endPoints;
        private double u;

        public IDW(List<Point> startPoints, List<Point> endPoints)
        {
            this.startPoints = startPoints;
            this.endPoints = endPoints;
            this.u = 2.0;
        }

        public Point F(Point p)
        {
            Point result = new Point(0, 0);
            for (int i = 0; i < startPoints.Count; i++)
            {
                result.X += Convert.ToInt32(Wi(p, i) * Fi(p, i).X);
                result.Y += Convert.ToInt32(Wi(p, i) * Fi(p, i).Y);
            }
            return result;
        }

        private Point Fi(Point p, int i)
        {
            Point endPoint = endPoints[i];
            endPoint.X += p.X - startPoints[i].X;
            endPoint.Y += p.Y - startPoints[i].Y;
            return endPoint;
        }

        private double Wi(Point p, int i)
        {
            double tmp = 0;
            //Wi(Pi) = 1
            if (p == startPoints[i])
            {
                return 1;
            }
            foreach (Point startPoint in startPoints)
            {
                tmp += GetDelta(p, startPoint);
            }
            return GetDelta(p, startPoints[i]) / tmp;
        }

        private double GetDistanceSquare(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        private double GetDelta(Point p, Point pi)
        {
            return Math.Pow(1.0 / GetDistanceSquare(p, pi), u);
        }
    }
}
