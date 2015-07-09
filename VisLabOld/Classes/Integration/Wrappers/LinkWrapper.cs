using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace VisLab.Classes
{
    public class LinkWrapper
    {
        private readonly Link link;

        public string Name
        {
            get { return link.Name; }
            set { if (value.Length <= 255) link.Name = value; else link.Name = value.Substring(0, 255); }
        }

        public int ID
        {
            get { return link.ID; }
        }

        private Point[] linef;
        public Point[] LineF
        {
            get
            {
                if (linef == null)
                {
                    linef = GetPointsF().ToArray();
                }

                return linef;
            }
        }

        private System.Windows.Point[] line;
        public System.Windows.Point[] Line
        {
            get
            {
                if (line == null)
                {
                    line = GetPoints().ToArray();
                }

                return line;
            }
        }

        private double height = 0;
        public double Height
        {
            get
            {
                if (height == 0)
                    height = LanesCount * LaneWidth;

                return height;
            }
        }

        private Polyline pl;
        public Polyline GetPolyline(SolidColorBrush brush)
        {
            if (pl == null) pl = new Polyline()
                {
                    Stroke = brush,
                    StrokeThickness = Height,
                    Points = new PointCollection(Line)
                };
            else if (pl.Stroke != brush) pl.Stroke = brush;

            return pl;
        }

        /// <summary>
        /// Link behavior type
        /// </summary>
        public string BehaviorType
        {
            get { return link.get_AttValue("BEHAVIORTYPE").ToString(); }
        }

        /// <summary>
        /// True if the link is a connector
        /// </summary>
        public string IsConnector
        {
            get { return link.get_AttValue("CONNECTOR").ToString(); }
        }

        /// <summary>
        /// Link cost per km
        /// </summary>
        public string Cost
        {
            get { return link.get_AttValue("COST").ToString(); }
        }

        /// <summary>
        /// Link display type number
        /// </summary>
        public string DisplayType
        {
            get { return link.get_AttValue("DISPLAYTYPE").ToString(); }
        }

        /// <summary>
        /// Connector emergency stop distance in the current unit for small distances
        /// </summary>
        public string EmergencyStop
        {
            get { return link.get_AttValue("EMERGENCYSTOP").ToString(); }
        }

        /// <summary>
        /// If it is a connector: the smallest index of a connected lane of the origin link (rightmost = 1). Otherwise 0.
        /// </summary>
        public string FromLane
        {
            get { return link.get_AttValue("FROMLANE").ToString(); }
        }

        /// <summary>
        /// If it is a connector: the origin link coordinate where the connector starts (in the current unit for small distances). Otherwise 0.
        /// </summary>
        public string FromLinkCoord
        {
            get { return link.get_AttValue("FROMLINKCOORD").ToString(); }
        }

        /// <summary>
        /// Gradient in %
        /// </summary>
        public string Gradient
        {
            get { return link.get_AttValue("GRADIENT").ToString(); }
        }

        /// <summary>
        /// Connector lane change distance in the current unit for small distances
        /// </summary>
        public string LaneChangeDistance
        {
            get { return link.get_AttValue("LANECHANGEDISTANCE").ToString(); }
        }

        /// <summary>
        /// The width of the rightmost lane in the units of the current options
        /// </summary>
        public double LaneWidth
        {
            get { return (double)link.get_AttValue("LANEWIDTH"); }
        }

        /// <summary>
        /// Link length in the current unit for small distances
        /// </summary>
        public double Length
        {
            get { return (double)link.get_AttValue("LENGTH"); }
        }

        /// <summary>
        /// Number of lanes
        /// </summary>
        public int LanesCount
        {
            get { return (int)link.get_AttValue("NUMLANES"); }
        }

        public string AvgSpeed()
        {
            object o = link.GetSegmentResult("SPEED", 0, 10.0);
            return o.ToString();
        }

        public LinkWrapper(Link link)
        {
            this.link = link;
        }

        /// <summary>
        /// Reference line through the center of the link (array of VARIANTs)
        /// </summary>
        private IEnumerable<System.Windows.Point> GetPoints()
        {
            var enumerator = ((object[])link.get_AttValue("POINTS")).GetEnumerator();

            while (enumerator.MoveNext())
            {
                double
                    x = (double)(enumerator.Current as WorldPoint).X,
                    y = (double)(enumerator.Current as WorldPoint).Y;

                yield return new System.Windows.Point(x, y);
            }
        }

        /// <summary>
        /// Reference line through the center of the link (array of VARIANTs)
        /// </summary>
        private IEnumerable<Point> GetPointsF()
        {
            var enumerator = ((object[])link.get_AttValue("POINTS")).GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return new Point((float)(enumerator.Current as WorldPoint).X / 2 + 100, 605 - (float)(enumerator.Current as WorldPoint).Y / 2);
            }
        }
    }
}
