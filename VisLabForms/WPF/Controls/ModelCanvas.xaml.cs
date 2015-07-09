using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VisLab.WPF
{
    /// <summary>
    /// Interaction logic for ModelCanvas.xaml
    /// </summary>
    public partial class ModelCanvas : UserControl
    {
        private Point startPoint;

        public ModelCanvas()
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
    }
}
