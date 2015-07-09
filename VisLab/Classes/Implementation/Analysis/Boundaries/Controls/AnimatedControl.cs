using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows;

namespace VisLab.Classes.Implementation.Analysis.Boundaries.Controls
{
    public class AnimatedControl : UserControl
    {
        private ScaleTransform stBorder = new ScaleTransform(0, 0);

        public AnimatedControl() : base()
        {
            this.Background = Brushes.Transparent;
            this.LayoutTransform = this.stBorder;
            this.Loaded += new System.Windows.RoutedEventHandler(AnimatedControl_Loaded);                
        }

        void AnimatedControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var scaleYAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)), FillBehavior.Stop);
            scaleYAnimation.Completed += (sender__, e__) => this.stBorder.ScaleY = 1;

            this.stBorder.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);

            var scaleXAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)), FillBehavior.Stop);
            scaleXAnimation.Completed += (sender__, e__) => this.stBorder.ScaleX = 1;

            this.stBorder.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        }
    }
}
