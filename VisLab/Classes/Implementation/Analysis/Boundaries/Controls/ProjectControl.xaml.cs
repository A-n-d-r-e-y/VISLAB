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
using VisLab.Classes.Implementation.Entities;
using VisLab.Classes.Implementation.Analysis.Controllers;

namespace VisLab.Controls
{
    /// <summary>
    /// Interaction logic for ProjectControl.xaml
    /// </summary>
    public partial class ProjectControl : UserControl
    {
        private Point startPoint;

        public ProjectControl()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                startPoint = Mouse.GetPosition(this);
                this.Cursor = Cursors.SizeAll;
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Point curr = e.GetPosition(this);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var dif = curr - startPoint;

                ttModel.X += (dif.X * (1 / stModel.ScaleX));
                ttModel.Y += (dif.Y * (1 / stModel.ScaleY));

                startPoint = curr;
            }

            curr = e.GetPosition(this.canvas);
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            stModel.ScaleX += (e.Delta > 0) ? 0.1 * stModel.ScaleX : -0.1 * stModel.ScaleX;
            stModel.ScaleY += (e.Delta > 0) ? 0.1 * stModel.ScaleY : -0.1 * stModel.ScaleY;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ttModel.X += (e.NewSize.Width - e.PreviousSize.Width) / 2;
            ttModel.Y += (e.NewSize.Height - e.PreviousSize.Height) / 2;
        }

        private void grid_Initialized(object sender, EventArgs e)
        {
            Canvas.SetLeft(grid, -330);
            Canvas.SetTop(grid, -240);
        }

        private void ListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void ListBox_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }

        private void btnDeleteModel_Click(object sender, RoutedEventArgs e)
        {
#if (DEBUG)
            throw new EntryPointNotFoundException("Test exception");
#endif
            if (listbox.SelectedItem != null && listbox.SelectedItem is Model)
            {
                var modelName = (listbox.SelectedItem as Model).Name;
                var manager = (this.DataContext as ProjectManager);

                manager.DeleteModel(modelName);
            }
        }
    }
}
