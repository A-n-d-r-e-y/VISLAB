using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
using System.Windows.Controls;
using System.Windows.Media;

namespace VisLab.Classes
{
    public class NetworkWrapper
    {
        private readonly Net net;
        //private static double
        //    minX = double.MaxValue, 
        //    minY = double.MaxValue, 
        //    maxX = double.MinValue, 
        //    maxY = double.MinValue;

        //public Double MinX
        //{
        //    get
        //    {
        //        if (minX == double.MaxValue) Initialize();
        //        return minX;
        //    }
        //}

        //public Double MinY
        //{
        //    get
        //    {
        //        if (minY == double.MaxValue) Initialize();
        //        return minY;
        //    }
        //}

        //public Double MaxX
        //{
        //    get
        //    {
        //        if (maxX == double.MinValue) Initialize();
        //        return maxX;
        //    }
        //}

        //public Double MaxY
        //{
        //    get
        //    {
        //        if (maxY == double.MinValue) Initialize();
        //        return maxY;
        //    }
        //}

        public NetworkWrapper(Net net)
        {
            this.net = net;
        }

        /// <summary>
        /// Fills all boundary coordinates //and link points at a time
        /// </summary>
        public void Initialize()
        {
            foreach (Link link in net.Links)
            {
                // link.Wrap().Line - caches link points
                //var x = link.Wrap().Line;
            }
        }

        private static Dictionary<double, System.Drawing.Pen> pencilBox = new Dictionary<double, System.Drawing.Pen>();

        public void Draw(System.Drawing.Graphics graphics, System.Drawing.Brush brush)
        {
            foreach (Link link in net.Links)
            {
                var w = link.Wrap();

                if (!pencilBox.Keys.Contains(w.Height))
                {
                    pencilBox[w.Height] = new System.Drawing.Pen(brush, (float)w.Height);
                }

                graphics.DrawLines(pencilBox[w.Height], w.LineF);
            }
        }

        public void Draw(Canvas canvas, SolidColorBrush brush)
        {
            foreach (Link link in net.Links)
            {
                var w = link.Wrap();
                canvas.Children.Add(w.GetPolyline(brush));
            }
        }
    }
}
