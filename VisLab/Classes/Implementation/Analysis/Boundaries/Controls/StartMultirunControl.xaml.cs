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
using System.Windows.Media.Animation;

namespace VisLab.Classes.Implementation.Analysis.Boundaries.Controls
{
    /// <summary>
    /// Interaction logic for StartMultirunControl.xaml
    /// </summary>
    public partial class StartMultirunControl : UserControl
    {
        public StartMultirunControl()
        {
            InitializeComponent();

            stBorder.ScaleX = stBorder.ScaleY = 0;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var scaleYAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)), FillBehavior.Stop);
            scaleYAnimation.Completed += (sender__, e__) => this.stBorder.ScaleY = 1;

            this.stBorder.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);

            var scaleXAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)), FillBehavior.Stop);
            scaleXAnimation.Completed += (sender__, e__) => this.stBorder.ScaleX = 1;

            this.stBorder.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        }
    }
}
