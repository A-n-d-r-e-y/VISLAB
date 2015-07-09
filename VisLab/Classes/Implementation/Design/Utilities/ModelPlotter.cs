using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

using VisLab.Classes.Implementation.Analysis.Boundaries.Controls;
using VisLab.Classes.Implementation.Design.Generics;
using VisLab.Classes.Integration.Utilities;
using VisLab.Classes.Integration.Entities;
using System.Threading;
using System.Windows.Threading;
using System.Data;
using VisLab.Controls;
using System.Collections.ObjectModel;

namespace VisLab.Classes.Implementation.Design.Utilities
{
    class ModelPlotter
    {
        private static int
            modelLevel = 100,
            counterLevel = 200,
            measurementLevel = 300;

        public static void DrawLinks(ModelControl control, IEnumerable<LinkItem> links)
        {
            control.HasNetwork = control.Clone.HasNetwork = true;

            var canvas1 = control.cnvModel;
            var canvas2 = control.Clone.cnvModel;

            foreach (var link in links)
            {
                AddLinkToCanvas(canvas1, link);
                AddLinkToCanvas(canvas2, link);
            }
        }

        private static void AddLinkToCanvas(Canvas canvas, LinkItem link)
        {
            var poly = link.GetPolyLine(Brushes.Gray);
            poly.ToolTip = "Double click to move here";
            Panel.SetZIndex(poly, modelLevel);
            canvas.Children.Add(poly);
        }

        public static void DrawPoints(ModelControl control, IEnumerable<PointsListItem> points)
        {
            double ellipseNormalSize = 20;

            control.HasPoints = control.Clone.HasPoints = true;
            control.Clone.CollectionPoints = new ObservableCollection<PointsListItem>(points);

            var canvas1 = control.cnvModel;
            var canvas2 = control.Clone.cnvModel;

            var trans1 = control.GlobalFixedScale; //new ScaleTransform(1, 1);
            var trans2 = control.Clone.GlobalFixedScale;  //new ScaleTransform(1, 1);

            foreach (var point in points)
            {
                AddPointToCanvas(ellipseNormalSize, canvas1, trans1, point);
                AddPointToCanvas(ellipseNormalSize, canvas2, trans2, point);
            }
        }

        private static void AddPointToCanvas(double ellipseNormalSize, Canvas canvas, ScaleTransform trans, PointsListItem point)
        {
            var el = new Ellipse()
            {
                Width = ellipseNormalSize,
                Height = ellipseNormalSize,
                //MinWidth = point.Diameter.Value,
                //MaxWidth = point.Diameter.Value,
                Fill = Brushes.Gold,
                Stroke = Brushes.Gray,
                Tag = "point",
                RenderTransform = trans,
                RenderTransformOrigin = new Point(0.5, 0.5),
                DataContext = point,
            };

            Panel.SetZIndex(el, counterLevel);

            canvas.Children.Add(el);
            Canvas.SetLeft(el, point.Coord.X - el.Width / 2);
            Canvas.SetTop(el, point.Coord.Y - el.Height / 2);
        }

        public static void DrawCounters(ModelControl control, IEnumerable<DataCollectionBindingSource> bindings)
        {
            control.HasCounters = control.Clone.HasCounters = true;

            //var group1 = new TransformGroup();
            //group1.Children.Add(new ScaleTransform(1, 1));
            //group1.Children.Add(new ScaleTransform(1, -1));

            var group2 = new TransformGroup();
            group2.Children.Add(control.Clone.GlobalFixedScale);
            group2.Children.Add(new ScaleTransform(1, -1));

            //var canvas1 = control.cnvModel;
            var canvas2 = control.Clone.cnvModel;

            foreach (var item in bindings)
            {
                //AddCounterToCanvas(group1, canvas1, item);
                AddCounterToCanvas(group2, canvas2, item);
            }
        }

        private static void AddCounterToCanvas(TransformGroup group, Canvas canvas, DataCollectionBindingSource item)
        {
            var dc = new DataCollectionControl()
            {
                DataContext = item,
                RenderTransform = group,
                Tag = "counter",
            };

            Panel.SetZIndex(dc, measurementLevel);

            canvas.Children.Add(dc);
            Canvas.SetLeft(dc, item.Center.X);
            Canvas.SetTop(dc, item.Center.Y);
        }

        public static void DrawSections(ModelControl control, IEnumerable<SectionListItem> sections)
        {
            double ellipseNormalSize = 15;

            control.HasSections = control.Clone.HasSections = true;
            control.Clone.TrTimeSections = new ObservableCollection<SectionListItem>(sections);

            var canvas1 = control.cnvModel;
            var canvas2 = control.Clone.cnvModel;

            var trans1 = control.GlobalFixedScale; //new ScaleTransform(1, 1);
            var trans2 = control.Clone.GlobalFixedScale; //new ScaleTransform(1, 1);

            foreach (var section in sections)
            {
                AddSectionsToCanvas(ellipseNormalSize, canvas1, trans1, section);
                AddSectionsToCanvas(ellipseNormalSize, canvas2, trans2, section);
            }
        }

        private static void AddSectionsToCanvas(double ellipseNormalSize, Canvas canvas, ScaleTransform trans, SectionListItem section)
        {
            var el = new Ellipse()
            {
                Width = ellipseNormalSize,
                Height = ellipseNormalSize,
                Fill = Brushes.Red,
                Stroke = Brushes.Gray,
                Tag = "section",
                RenderTransform = trans,
                RenderTransformOrigin = new Point(0.5, 0.5),
                DataContext = section,
            };

            Panel.SetZIndex(el, counterLevel);

            canvas.Children.Add(el);
            Canvas.SetLeft(el, section.FromCoord.X - el.Width / 2);
            Canvas.SetTop(el, section.FromCoord.Y - el.Height / 2);

            //---------------------------------------------------------------

            el = new Ellipse()
            {
                Width = ellipseNormalSize,
                Height = ellipseNormalSize,
                Fill = Brushes.Green,
                Stroke = Brushes.Gray,
                Tag = "section",
                RenderTransform = trans,
                RenderTransformOrigin = new Point(0.5, 0.5),
                DataContext = section,
            };

            Panel.SetZIndex(el, counterLevel);

            canvas.Children.Add(el);
            Canvas.SetLeft(el, section.ToCoord.X - el.Width / 2);
            Canvas.SetTop(el, section.ToCoord.Y - el.Height / 2);
        }

        public static void DrawTrTimes(ModelControl control, IEnumerable<DataCollectionBindingSource> bindings)
        {
            control.HasTravelTimes = control.Clone.HasTravelTimes = true;

            var group2 = new TransformGroup();
            group2.Children.Add(control.Clone.GlobalFixedScale);
            group2.Children.Add(new ScaleTransform(1, -1));

            var canvas2 = control.Clone.cnvModel;

            foreach (var item in bindings)
            {
                AddTrTimesToCanvas(group2, canvas2, item);
            }
        }

        private static void AddTrTimesToCanvas(TransformGroup group, Canvas canvas, DataCollectionBindingSource item)
        {
            var dc = new DataCollectionControl()
            {
                DataContext = item,
                RenderTransform = group,
                Tag = "tr.times"
            };

            Panel.SetZIndex(dc, measurementLevel);

            canvas.Children.Add(dc);
            Canvas.SetLeft(dc, item.Center.X);
            Canvas.SetTop(dc, item.Center.Y);
        }
    }
}
