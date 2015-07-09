using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using VisLab.Classes.Implementation.Entities;
using VisLab.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VisLab.Classes.Implementation.Utilities
{
    public class TreePlotter
    {
        private static double verticalGap = 50.0;
        private static double horizontalGap = 140.0;
        private static double linesOffset = 30.0;

        private static int maxDescendantsCount;

        public static void DrawTree(Canvas canvas, string treeRootPath, string selectedExpPath, string loadedExpPath)
        {
            Point rootPoint = new Point(0, 0);
            var exp = Experiment.Load(treeRootPath, selectedExpPath, loadedExpPath);

            DrawTopDown(exp, canvas, rootPoint, 0);
        }

        private static void DrawTopDown(Experiment exp, Canvas canvas, Point point, int lvl)
        {
            if (exp.DescendantsCount > maxDescendantsCount) maxDescendantsCount = exp.DescendantsCount;

            var nodeCtrl = new NodeControl();
            nodeCtrl.DataContext = exp;
            nodeCtrl.Counter = exp.ChildsCount;
            //if (!dict[node.Id].HasBackup || !dict[node.Id].HasSnapshot) nodeCtrl.Background = Brushes.LightPink;
            nodeCtrl.border.Background = GradientBraker.Brake(Colors.White, Colors.LightBlue, maxDescendantsCount + 1).ToArray()[exp.DescendantsCount];
            nodeCtrl.IsSelected = exp.IsLoaded;
            canvas.Children.Add(nodeCtrl);
            Canvas.SetTop(nodeCtrl, point.Y);
            Canvas.SetLeft(nodeCtrl, point.X);
            Canvas.SetZIndex(nodeCtrl, 100);

            nodeCtrl.cbxAnalyze.IsChecked = exp.Data == null ? false : exp.Data.Alanyze;

            // calculate coordinates
            double trainLength = (exp.ChildsCount - 1) * horizontalGap;
            double startX = point.X - trainLength / 2;
            double startY = verticalGap * (lvl + 1);

            foreach (var chld in exp.ChildNodes)
            {
                if (chld.IsOpen)
                {
                    DrawTopDown(chld, canvas, new Point(startX, startY), lvl + 1);
                }
                else
                {
                    var nodeCtrl2 = new NodeControl();
                    nodeCtrl2.DataContext = chld;
                    nodeCtrl2.Counter = chld.ChildsCount;
                    //if (!dict[chld.Id].HasBackup || !dict[chld.Id].HasSnapshot) nodeCtrl2.Background = Brushes.LightPink;
                    nodeCtrl2.IsSelected = chld.IsLoaded;
                    nodeCtrl2.border.Background = GradientBraker.Brake(Colors.White, Colors.LightBlue, maxDescendantsCount + 1).ToArray()[chld.DescendantsCount];
                    canvas.Children.Add(nodeCtrl2);
                    Canvas.SetTop(nodeCtrl2, startY);
                    Canvas.SetLeft(nodeCtrl2, startX);
                    Canvas.SetZIndex(nodeCtrl2, 100);

                    nodeCtrl2.cbxAnalyze.IsChecked = chld.Data == null ? false : chld.Data.Alanyze;
                }

                var line = new Line();

                if (!chld.IsLoaded)
                {
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.3;
                    line.StrokeDashArray = new DoubleCollection(new double[] { 35, 35 });
                }
                else
                {
                    line.Stroke = Brushes.Gold;
                    line.StrokeThickness = 1.5;
                }

                //if (chld.IsOpen) line.StrokeThickness = 2;

                line.X1 = point.X + horizontalGap / 1.39;
                line.Y1 = point.Y + linesOffset / 2;
                line.X2 = startX + horizontalGap / 1.39;
                line.Y2 = startY + linesOffset / 2;
                canvas.Children.Add(line);
                Canvas.SetZIndex(line, 99);

                startX += horizontalGap;
            }
        }
    }
}
