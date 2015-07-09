using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Web.UI.DataVisualization.Charting;

namespace VisLab.Classes.Integration.Utilities
{
    class Euclid
    {
        public static Point GetPolygonPoint(IEnumerable<Point> points, double position)
        {
            int count = points.Count();
            double length = 0.0, prev_length = 0.0;
            Point result = new Point(0, 0);

            if (count > 1)
            {
                Point
                    start = new Point(0, 0),
                    end = points.FirstOrDefault();

                int i = 1;
                while (position > length && i < count)
                {
                    start = end;
                    end = points.ElementAt(i++);
                    prev_length = length;
                    length += GetLineLength(start, end);
                }

                if (position <= length)
                {
                    //double
                        //dX = end.X - start.X,
                        //dY = end.Y - start.Y,
                        //sX = dY > 0 ? 1 : -1,
                        //sY = dY > 0 ? 1 : -1,
                        //angle = Math.Atan(dX / dY);

                    //////////////////////////////////

                    //move segment -> start to center
                    //start.X -= start.X;
                    //start.Y -= start.Y;
                    end.X -= start.X;
                    end.Y -= start.Y;

                    // now, coords of end describe angle (absolute positive -> first quadrant)
                    double
                        angle = Math.Atan(Math.Abs(end.X / end.Y)),
                        dX = Math.Sin(angle) * (position - prev_length),
                        dY = Math.Cos(angle) * (position - prev_length);

                    const double grad90 = 90 / (180 / Math.PI);

                    if (end.X <= 0 && end.Y > 0)
                    {
                        angle += grad90;
                        dX = Math.Cos(angle) * (position - prev_length);
                        dY = Math.Sin(angle) * (position - prev_length);
                    }
                    if (end.X < 0 && end.Y <= 0)
                    {
                        angle += (grad90 * 2);
                        dX = Math.Sin(angle) * (position - prev_length);
                        dY = Math.Cos(angle) * (position - prev_length);
                    }

                    if (end.X >= 0 && end.Y < 0)
                    {
                        angle += (grad90 * 3);
                        dX = Math.Cos(angle) * (position - prev_length);
                        dY = Math.Sin(angle) * (position - prev_length);
                    }

                    //double X = Math.Sin(angle) * (position - prev_length);
                    //double Y = Math.Cos(angle) * (position - prev_length);

                    //result = new Point(start.X + X * sX, start.Y + Y * sY);
                    result = new Point(start.X + dX, start.Y + dY);
                }

                return result;
            }

            return result;
        }

        private static double GetPolygonLength(IEnumerable<Point> points)
        {
            int count = points.Count();
            double sum = 0.0;

            if (count > 1)
            {
                Point start = points.FirstOrDefault();

                for (int i = 1; i < count; i++)
                {
                    Point end = points.ElementAt(i);
                    sum += GetLineLength(start, end);
                    start = end;
                }
            }

            return sum;
        }

        private static double GetLineLength(Point start, Point end)
        {
            var v1 = new Vector(start.X, start.Y);
            var v2 = new Vector(end.X, end.Y);

            return Vector.Subtract(v1, v2).Length;
        }

        private static Chart ch = new Chart();
        public static double TDistribution(double prob, int freedom)
        {
            return ch.DataManipulator.Statistics.InverseTDistribution(prob, freedom);
        }
    }
}
