using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageWarping
{
    class RBF
    {
        private List<Point> startPoints;
        private List<Point> endPoints;
        private List<double> ri;
        private List<double> x;
        private List<double> y;
        private List<List<double>> dipj;
        private double u;

        public RBF(List<Point> startPoints, List<Point> endPoints)
        {
            this.startPoints = startPoints;
            this.endPoints = endPoints;
            this.u = -1.0;
            GetDiPj();
            GetRi();
            GetXY();
        }

        public Point F(Point p)
        {
            List<double> dp = new List<double>(startPoints.Count);
            for (int k = 0; k < startPoints.Count; k++)
            {
                dp.Add(Math.Pow((p.X - startPoints[k].X), 2) + Math.Pow((p.Y - startPoints[k].Y), 2));
            }
            double mx1 = 0;
            double my1 = 0;
            for (int i = 0; i < startPoints.Count; i++)
            {
                mx1 += x[i] * Math.Pow((dp[i] * dp[i] + ri[i] * ri[i]), u / 2.0);
                my1 += y[i] * Math.Pow((dp[i] * dp[i] + ri[i] * ri[i]), u / 2.0);
            }
            mx1 = mx1 + p.X;
            my1 = my1 + p.Y;
            long x1 = Convert.ToInt64(mx1 + 0.5); ;
            long y1 = Convert.ToInt64(my1 + 0.5);
            Point result = new Point();
            result.X = x1 > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(x1);
            result.Y = y1 > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(y1);
            return result;
        }

        private void GetDiPj()
        {
            dipj = new List<List<double>>();
            for (int i = 0; i < startPoints.Count; i++)
            {
                List<double> dp = new List<double>();
                for (int j = 0; j < startPoints.Count; j++)
                {
                    dp.Add((startPoints[i].X - startPoints[j].X) * (startPoints[i].X - startPoints[j].X)
                         + (startPoints[i].Y - startPoints[j].Y) * (startPoints[i].Y - startPoints[j].Y));
                }
                dipj.Add(dp);
            }
        }

        private void GetRi()
        {
            ri = new List<double>(startPoints.Count);
            ri.Add(dipj[0][1]); 
            for (int j = 1; j < startPoints.Count; j++)
            {
                if (dipj[0][j] < ri[0])
                {
                    ri[0] = dipj[0][j]; 
                }
            }
            for (int i = 1; i < startPoints.Count; i++)
            {
                ri.Add(dipj[i][0]); 
                for (int j = 1; j < startPoints.Count; j++)
                {
                    if (dipj[i][j] < ri[i] && i != j)
                    {
                        ri[i] = dipj[i][j];
                    }
                }
            }
            for (int i = 0; i < startPoints.Count; i++)
            {
                for (int j = 0; j < startPoints.Count; j++)
                {
                    dipj[i][j] = Math.Pow((dipj[i][j] + ri[j]), u);
                }
            }
        }

        private void GetXY()
        {
            x = new List<double>(startPoints.Count);
            y = new List<double>(startPoints.Count);
            for (int i = 0; i < startPoints.Count; i++)
            {
                x.Add(endPoints[i].X - startPoints[i].X);
                y.Add(endPoints[i].Y - startPoints[i].Y); 
            }
            x = Gauss(dipj, x);
            y = Gauss(dipj, y);
        }

        private List<double> Gauss(List<List<double>> A, List<double> d)
        {
            List<List<double>> B = new List<List<double>>();

            for (int i = 0; i < A.Count; i++)
            {
                List<double> tmp = new List<double>();
                for (int j = 0; j < A[i].Count; j++)
                {
                    tmp.Add(A[i][j]);
                }
                B.Add(tmp);
            }

            for (int k = 0; k < d.Count - 1; k++)
            {
                if (B[k][k] == 0)
                {
                    return d;
                }
                else
                {
                    for (int i = k + 1; i < d.Count; i++)
                    {
                        B[i][k] = B[i][k] / B[k][k];
                        for (int j = k + 1; j < d.Count; j++)
                        {
                            d[i] = d[i] - d[k] * B[i][k];
                            B[i][j] = B[i][j] - B[k][j] * B[i][k];
                        }
                    }
                }
            }

            if (B[d.Count - 1][d.Count - 1] == 0)
            {
                return d;
            }

            d[d.Count - 1] = d[d.Count - 1] / B[d.Count - 1][d.Count - 1];
            double sum = 0;
            for (int k = d.Count - 2; k >= 0; k--)
            {
                sum = 0;
                for (int i = k + 1; i < d.Count; i++)
                {
                    sum += B[k][i] * d[i];
                }
                d[k] = (d[k] - sum) / B[k][k];
            }
            return d;
        }
    }
}
