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
using VisLab.Classes.Implementation.Design;
using VisLab.Classes.Implementation.Wrappers;
using System.Windows.Interop;
using VisLab.Windows;
using Microsoft.Win32;

namespace VisLab.Controls
{
    /// <summary>
    /// Interaction logic for AddModelControl.xaml
    /// </summary>
    public partial class AddModelControl : UserControl
    {
        public AddModelControl()
        {
            InitializeComponent();
            stBorder.ScaleX = stBorder.ScaleY = 0;

            this.DataContext = new AddModelBindingSource();
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

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            if (!rbLoadFromFile.IsChecked.Value) rbLoadFromFile.IsChecked = true;

            var dlgOpenFile = new OpenFileDialog();
            dlgOpenFile.Filter = "VISSIM input file|*.inp";
            dlgOpenFile.FileName = "";

            if (dlgOpenFile.ShowDialog(App.Current.MainWindow).Value)
            {
                tbxFileName.Text = dlgOpenFile.FileName;
                tbxFileName.ToolTip = dlgOpenFile.FileName;
            }
        }
    }
}
