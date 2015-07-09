using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
using System.Windows.Controls;
using System.Windows.Media;
using VisLab.Classes.Integration.Extensions;

namespace VisLab.Classes.Integration.Wrappers
{
    public class NetworkWrapper
    {
        private readonly Net net;

        public NetworkWrapper(Net net)
        {
            this.net = net;
        }

        private static Dictionary<double, Pen> pencilBox = new Dictionary<double, Pen>();

        public void Draw(Graphics graphics, Brush brush)
        {
            foreach (Link link in net.Links)
            {
                var w = link.Wrap();

                if (!pencilBox.Keys.Contains(w.Height))
                {
                    pencilBox[w.Height] = new Pen(brush, (float)w.Height);
                }

                //TODO error
                //graphics.DrawLines(pencilBox[w.Height], w.LineF);
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
