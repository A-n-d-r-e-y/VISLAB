using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisLab.Classes;
using System.Windows.Shapes;
using System.Windows.Media;
using System;
using vissim = VisLab.Classes.VissimSingleton;
using System.Linq;

namespace VisLab.Controls
{
    /// <summary>
    /// Interaction logic for ModelControl.xaml
    /// </summary>
    public partial class ModelControl : UserControl
    {
        private Point startPoint;

        public ModelControl()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = Mouse.GetPosition(this);
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Point curr = e.GetPosition(this);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var dif = curr - startPoint;

                ttModel.X += (dif.X * (1/stModel.ScaleX));
                ttModel.Y -= (dif.Y * (1/stModel.ScaleX));

                startPoint = curr;
            }

            curr = e.GetPosition(this.cnvModel);
            tbkXCoordinate.Text = string.Format("x:{0}", curr.X);
            tbkYCoordinate.Text = string.Format("y:{0}", curr.Y);
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            stModel.ScaleX += (e.Delta > 0) ? 0.1 * stModel.ScaleX : -0.1 * stModel.ScaleX;
            stModel.ScaleY += (e.Delta > 0) ? -0.1 * -stModel.ScaleY : 0.1 * -stModel.ScaleY;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ttModel.X += (e.NewSize.Width - e.PreviousSize.Width) / 2;
            ttModel.Y += (e.NewSize.Height - e.PreviousSize.Height) / 2;
        }

        private void border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (expander.IsExpanded) expander.IsExpanded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cnvModel.Children.Clear();
            vissim.Instance.Net.Wrap().Draw(cnvModel, System.Windows.SystemColors.WindowTextBrush);

            foreach (var item in Analyst.GetReportData())
            {
                var tb = new TextBlock()
                {
                    Text = item.AvgSpeed.ToString(),
                    FontSize = 20,
                    Foreground = Brushes.Gray
                };

                tb.LayoutTransform = new ScaleTransform(1, -1);

                Canvas.SetLeft(tb, item.Center.X);
                Canvas.SetTop(tb, item.Center.Y);

                cnvModel.Children.Add(tb);

                vissim.Instance.Net.Links.Wrap().Where(link =>
                    {
                        return link.ID == item.Link;
                    }).First().GetPolyline(Brushes.Yellow);
            }

            //foreach (Ellipse item in Analyst.GetPoints(3))
            //{
            //    //item.LayoutTransform = new RotateTransform(90);
            //    cnvModel.Children.Add(item);

            //    Canvas.SetLeft(item, ((Point)item.Tag).X);
            //    Canvas.SetTop(item, ((Point)item.Tag).Y);
            //}
        }
    }
}
