using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace VisLab.Classes.Integration.Entities
{
    public class LinkItem
    {
        public string Name { get; set; }
        public IEnumerable<Point> Points { get; set; }
        public double? Width { get; set; }
        public int? Id { get; set; }

        public Polyline GetPolyLine(SolidColorBrush brush)
        {
            return new Polyline()
                {
                    Stroke = brush,
                    StrokeThickness = Width.HasValue ? Width.Value : 1,
                    Points = new PointCollection(Points)
                };
        }
    }
}
