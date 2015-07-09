using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisLab.Classes;
using System.Threading;
using VisLab.Windows;
using System;

namespace VisLab.Controls
{
    /// <summary>
    /// Interaction logic for TreeCanvas.xaml
    /// </summary>
    public partial class TreeControl : UserControl
    {
        private ProjectManager pm;
        private Point startPoint;

        public TreeControl(ProjectManager pm)
        {
            InitializeComponent();

            this.pm = pm;

            Refresh();
        }

        public void Refresh()
        {
            cnvTree.Children.Clear();
            pm.DrawTree(cnvTree, new Point(0, 0));
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
            pm.SaveTree();
            pm.SaveProject();
        }

        private void NodeControl_Select(object sender, RoutedEventArgs e)
        {
            var nodeCtrl = (sender as NodeControl);
            var node = (nodeCtrl.Tag as ExperimentsTree.ExperimentsTreeNode);
            node.Open();

            //pm.LastSnapshotId = node.Id;
            //pm.LoadExperiment(node.Id);

            Refresh();
        }

        private void NodeControl_Load(object sender, RoutedEventArgs e)
        {
            var nodeCtrl = (sender as NodeControl);
            var node = (nodeCtrl.Tag as ExperimentsTree.ExperimentsTreeNode);

            (App.Current.MainWindow as MainWindow).LoadingAnimationStart();

            ThreadPool.QueueUserWorkItem(o =>
                {
                    pm.TakeSnapshot(node);

                    Dispatcher.Invoke((Action)(() =>
                    {
                        (App.Current.MainWindow as MainWindow).LoadingAnimationEnd();
                        Refresh();
                    }));
                });

            //Refresh();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ttModel.X += (e.NewSize.Width - e.PreviousSize.Width) / 2;
            ttModel.Y += (e.NewSize.Height - e.PreviousSize.Height) / 2;
        }
    }
}
