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
using VisLab.Classes;

namespace VisLab.WPF
{
    /// <summary>
    /// Interaction logic for TreeCanvas.xaml
    /// </summary>
    public partial class TreeCanvas : UserControl
    {
        private ProjectManager pm;
        private Point startPoint;
        private ExperimentsTree tree;

        public TreeCanvas(ProjectManager pm)
        {
            InitializeComponent();

            this.pm = pm;
            tree = ExperimentsTree.Load(pm.SnapshotTreeFileName);

            Refresh();
        }

        void Refresh()
        {
            cnvTree.Children.Clear();
            TreePainter.DrawTopDown(cnvTree, tree.root, new Point(0, 0), pm.Experiment);
        }

        void tree_NodeMouseUp(object sender, MouseButtonEventArgs e)
        {
            var nodeCtrl = (sender as NodeControl);
            var node = (nodeCtrl.Tag as ExperimentsTree.ExperimentsTreeNode);
            node.Open();

            pm.LastSnapshotId = node.Id;
            pm.LoadExperiment(node.Id);

            Refresh();
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

                ttModel.X += (dif.X * (1 / stModel.ScaleX));
                ttModel.Y += (dif.Y * (1 / stModel.ScaleX));

                startPoint = curr;
            }

            curr = e.GetPosition(this.cnvTree);
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            stModel.ScaleX += (e.Delta > 0) ? 0.1 * stModel.ScaleX : -0.1 * stModel.ScaleX;
            stModel.ScaleY += (e.Delta > 0) ? 0.1 * stModel.ScaleY : -0.1 * stModel.ScaleY;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            tree.Save(pm.SnapshotTreeFileName);
            pm.SaveProject();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void NodeControl_Select(object sender, RoutedEventArgs e)
        {
            var nodeCtrl = (sender as NodeControl);
            var node = (nodeCtrl.Tag as ExperimentsTree.ExperimentsTreeNode);
            node.Open();

            pm.LastSnapshotId = node.Id;
            pm.LoadExperiment(node.Id);

            Refresh();
        }
    }
}
