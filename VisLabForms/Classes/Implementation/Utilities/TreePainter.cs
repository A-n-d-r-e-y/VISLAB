using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using VisLab.WPF;

namespace VisLab.Classes
{
    public class TreePainter
    {
        private static int lastExperimentNumber = -1;
        private static Dictionary<Guid, Experiment> dict = new Dictionary<Guid, Experiment>();

        private static double h = 50.0;
        private static double l = 140.0;
        private static double w = 30.0;

        private static int maxDescendantsCount = 0;

        public static void DrawTopDown(Canvas canvas,
            ExperimentsTree.ExperimentsTreeNode node, 
            Point point, Experiment currentExp)
        {
            if (lastExperimentNumber != currentExp.Number)
            {
                dict = Experiment.GetDict();
                lastExperimentNumber = currentExp.Number;
            }

            if (node.DescendantsCount > maxDescendantsCount) maxDescendantsCount = node.DescendantsCount;

            var nodeCtrl = new NodeControl();
            nodeCtrl.Tag = node;
            nodeCtrl.Header = string.Format("Exp. Nr.{0}", dict[node.Id].Number);
            nodeCtrl.Counter = node.ChildsCount;
            nodeCtrl.lbl1.Content = "Id: " + node.Id.ToString();
            nodeCtrl.lbl2.Content = "Descendants Count: " + node.DescendantsCount.ToString();
            nodeCtrl.border.Background = GradientBraker.Brake(Colors.AntiqueWhite, Colors.Blue, maxDescendantsCount + 1).ToArray()[node.DescendantsCount];
            nodeCtrl.IsSelected = true;
            canvas.Children.Add(nodeCtrl);
            Canvas.SetTop(nodeCtrl, point.Y);
            Canvas.SetLeft(nodeCtrl, point.X);
            Canvas.SetZIndex(nodeCtrl, 100);

            // calculate coordinates
            double trainLength = (node.ChildsCount - 1) * l;
            double startX = point.X - trainLength / 2;
            double startY = h * (node.Level + 1);

            foreach (var chld in node.ChildNodes)
            {
                if (chld.IsOpen)
                {
                    DrawTopDown(canvas, chld, new Point(startX, startY), dict[chld.Id]);
                }
                else
                {
                    var nodeCtrl2 = new NodeControl();
                    nodeCtrl2.Header = string.Format("Exp. Nr.{0}", dict[chld.Id].Number);
                    nodeCtrl2.Tag = chld;
                    nodeCtrl2.Counter = chld.ChildsCount;
                    nodeCtrl2.lbl1.Content = "Id: " + chld.Id.ToString();
                    nodeCtrl2.lbl2.Content = "Descendants Count: " + chld.DescendantsCount.ToString();
                    nodeCtrl2.IsSelected = false;
                    nodeCtrl2.border.Background = GradientBraker.Brake(Colors.AntiqueWhite, Colors.Blue, maxDescendantsCount + 1).ToArray()[chld.DescendantsCount];
                    canvas.Children.Add(nodeCtrl2);
                    Canvas.SetTop(nodeCtrl2, startY);
                    Canvas.SetLeft(nodeCtrl2, startX);
                    Canvas.SetZIndex(nodeCtrl2, 100);
                }

                var line = new Line();
                if (!chld.IsOpen)
                {
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.1;
                    line.StrokeDashArray = new DoubleCollection(new double[] { 70, 35 });
                }
                else
                {
                    line.Stroke = Brushes.Gold;
                    line.StrokeThickness = 1;
                }
                
                line.X1 = point.X + l/1.39;
                line.Y1 = point.Y + w/2;
                line.X2 = startX + l/1.39;
                line.Y2 = startY + w / 2;
                canvas.Children.Add(line);
                Canvas.SetZIndex(line, 99);

                startX += l;
            }
        }

        public static void DrawDownTop(Canvas canvas, ExperimentsTree.ExperimentsTreeNode node, Point point)
        {
            if (node.ChildNodes.Count > 0)
                DrawChilds(canvas, node, point);

            if (node.ParentNode != null)
                DrawDownTop(canvas, node, point);
        }

        private static void DrawChilds(Canvas canvas, ExperimentsTree.ExperimentsTreeNode node, Point point)
        {
            // calculate coordinates
            double trainLength = (node.ChildsCount - 1) * l;
            double startX = point.X - trainLength / 2;
            double startY = h * (node.Level + 1);

            foreach (var chld in node.ChildNodes)
            {
                //var coord = new Point(startX, startY);

                var el2 = new Ellipse();
                el2.Tag = chld;
                el2.Fill = Brushes.Transparent;
                el2.StrokeThickness = 1;
                el2.Stroke = Brushes.Red;
                el2.Width = el2.Height = w;
                canvas.Children.Add(el2);
                Canvas.SetTop(el2, startY);
                Canvas.SetLeft(el2, startX);

                var tb2 = new TextBlock();
                tb2.Text = chld.ChildsCount.ToString();
                canvas.Children.Add(tb2);
                Canvas.SetTop(tb2, startY);
                Canvas.SetLeft(tb2, startX + el2.Width);
            }

            var line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 1;
            line.X1 = point.X + 20;
            line.Y1 = point.Y + w;
            line.X2 = startX + 20;
            line.Y2 = startY;
            canvas.Children.Add(line);

            startX += l;
        }
    }
}
