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

namespace VisLab.Controls
{
    /// <summary>
    /// Interaction logic for ShadingControl.xaml
    /// </summary>
    public partial class ShadingControl : UserControl
    {
        public ShadingControl()
        {
            InitializeComponent();
        }

        public void ReleaseShadow()
        {
            (this.Content as Control).OpacityMask = Brushes.Black;
            var anime = new DoubleAnimation(1, 0.3, new Duration(TimeSpan.FromSeconds(5)), FillBehavior.HoldEnd);
            this.BeginAnimation(Grid.OpacityProperty, anime);
        }
    }
}
