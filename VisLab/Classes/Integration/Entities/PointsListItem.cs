using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace VisLab.Classes.Integration.Entities
{
    public class PointsListItem
    {
        public int? MeasurId { get; set; }
        public int? PointId { get; set; }
        public string Name { get; set; }
        public int? Link { get; set; }
        public Point Coord { get; set; }
        public double? Diameter { get; set; }
    }
}
