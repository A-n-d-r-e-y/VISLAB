using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisLab.Classes;
using System.Windows.Shapes;
using System.Windows.Media;
using System;
using System.Linq;
using VisLab.Classes.Implementation.Analysis.Controllers;
using VisLab.Classes.Implementation.Analysis.Boundaries.Controls;
using VisLab.Classes.Implementation.Design;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using VisLab.Classes.Implementation.Design.Generics;
using VisLab.Classes.Integration.Entities;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace VisLab.Controls
{
    /// <summary>
    /// Interaction logic for ModelControl.xaml
    /// </summary>
    public partial class ModelControl : UserControl
    {
        private Point startPoint;

        private Point countersGridStartPoint;
        private bool isCountersGridFlying;

        private Point trTimesGridStartPoint;
        private bool isTrTimesGridFlying;

        private Point dccStartPoint;
        private List<Line> lines = new List<Line>();

        public bool HasNetwork;

        public bool HasPoints;
        public bool HasCounters;

        public bool HasSections;
        public bool HasTravelTimes;

        public bool ArePointsVisible;
        public bool AreCountersVisible;
        public bool AreSectionsVisible;
        public bool AreTrTimesVisible;

        public ScaleTransform GlobalFixedScale = new ScaleTransform(1, 1);

        public bool IsMaster;
        public ModelControl Clone;

        private const string DEFAULT_GROUPING_FIELD_NAME = "MeasurId";

        private ObservableCollection<PointsListItem> collectionPoints;
        public ObservableCollection<PointsListItem> CollectionPoints
        {
            get { return collectionPoints; }
            set
            {
                collectionPoints = value;

                var view = CollectionViewSource.GetDefaultView(CollectionPoints);
                var distinct = (from d in
                                    (from p in CollectionPoints select p.PointId).Distinct()
                                select new
                                {
                                    pointId = d,
                                    measureId = (from p in CollectionPoints
                                                 where p.PointId == d
                                                 orderby p.MeasurId
                                                 select p.MeasurId).FirstOrDefault()
                                }).ToArray();

                view.Filter = (param) =>
                {
                    var counter = (param as PointsListItem);
                    int count = (from d in distinct
                                 where d.measureId == counter.MeasurId && d.pointId == counter.PointId
                                 select d.GetHashCode()).Count();

                    return count > 0;
                };

                if (checkGrouping.IsChecked.Value)
                {
                    var g = new PropertyGroupDescription(DEFAULT_GROUPING_FIELD_NAME);
                    view.GroupDescriptions.Add(g);
                }
                dgCounters.ItemsSource = view;
            }
        }

        private ObservableCollection<SectionListItem> trTimeSections;
        public ObservableCollection<SectionListItem> TrTimeSections
        {
            get { return trTimeSections; }
            set
            {
                trTimeSections = value;

                var view = CollectionViewSource.GetDefaultView(TrTimeSections);
                dgTrTimes.ItemsSource = view;
            }
        }

        public static readonly DependencyProperty ModelNameProperty =
            DependencyProperty.Register(
            "ModelName",
            typeof(string),
            typeof(ModelControl));

        public static readonly DependencyProperty ExperimentNumberProperty =
            DependencyProperty.Register(
            "ExperimentNumber",
            typeof(int),
            typeof(ModelControl));

        public static readonly RoutedEvent ModelDoubleClickEvent =
            EventManager.RegisterRoutedEvent("ModelDoubleClick", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ModelControl));

        public event RoutedEventHandler ModelDoubleClick
        {
            add { AddHandler(ModelDoubleClickEvent, value); }
            remove { RemoveHandler(ModelDoubleClickEvent, value); }
        }

        public string ModelName
        {
            get { return (string)GetValue(ModelNameProperty); }
            set { SetValue(ModelNameProperty, value); }
        }

        public int ExperimentNumber
        {
            get { return (int)GetValue(ExperimentNumberProperty); }
            set { SetValue(ExperimentNumberProperty, value); }
        }

        public ModelControl(string modelName)
        {
            InitializeComponent();
            ellipse.Style = null;

            this.ModelName = modelName;

            // dgCounters collapsed by default
            stBorder.ScaleY = stBorder.ScaleY = 0;
            stTrTimesGrid.ScaleY = stTrTimesGrid.ScaleY = 0;
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
            if (isTrTimesGridFlying)
            {
                isTrTimesGridFlying = false;
                cnvModel.IsEnabled = true;
                dgTrTimes.IsEnabled = true;
            }
            else if (isCountersGridFlying)
            {
                isCountersGridFlying = false;
                cnvModel.IsEnabled = true;
                dgCounters.IsEnabled = true;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isTrTimesGridFlying)
            {
                Point curr = e.GetPosition(this);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var dif = curr - trTimesGridStartPoint;

                    ttTrTimesGrid.X += dif.X;
                    ttTrTimesGrid.Y += dif.Y;

                    trTimesGridStartPoint = curr;

                    e.Handled = true;
                }
            }
            else if (isCountersGridFlying)
            {
                Point curr = e.GetPosition(this);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var dif = curr - countersGridStartPoint;

                    ttDataGrid.X += dif.X;
                    ttDataGrid.Y += dif.Y;

                    countersGridStartPoint = curr;

                    e.Handled = true;
                }
            }
            else
            {
                Point curr = e.GetPosition(this);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var dif = curr - startPoint;

                    ttModel.X += (dif.X * (1 / stModel.ScaleX));
                    ttModel.Y -= (dif.Y * (1 / stModel.ScaleY));

                    startPoint = curr;
                }

                curr = e.GetPosition(this.cnvModel);
                tbkXCoordinate.Text = string.Format("x:{0}", curr.X);
                tbkYCoordinate.Text = string.Format("y:{0}", curr.Y);
            }
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double
                dx = (e.Delta > 0) ? 0.1 * stModel.ScaleX : -0.1 * stModel.ScaleX,
                dy = (e.Delta > 0) ? -0.1 * -stModel.ScaleY : 0.1 * -stModel.ScaleY;

            stModel.ScaleX += dx;
            stModel.ScaleY += dy;

            e.Handled = true;

            // -> slider_ValueChanged
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize.Width > 0 && e.PreviousSize.Height > 0)
            {
                ttModel.X += (e.NewSize.Width - e.PreviousSize.Width) / 2;
                ttModel.Y += (e.NewSize.Height - e.PreviousSize.Height) / 2;
            }
            //else
            //{
            //    ttModel.X = e.NewSize.Width / 3;
            //    ttModel.Y = -e.NewSize.Height / 3;
            //}
        }

        public void OptimizeForList()
        {
            slider.Minimum = 0.05;
            slider.Value = 0.2;
            slider.Visibility = System.Windows.Visibility.Collapsed;
            panel.Visibility = System.Windows.Visibility.Collapsed;
            spModelDescription.Visibility = System.Windows.Visibility.Collapsed;
            countersGrid.Visibility = Visibility.Collapsed;
            trTimesGrid.Visibility = Visibility.Collapsed;
        }

        public void MoveToPoint(Point point)
        {
            double
                fromX = ttModel.X,
                toX = ActualWidth / 2 - point.X,
                fromY = ttModel.Y,
                toY = ActualHeight / 2 - point.Y;

            var horAnimation = new DoubleAnimation(fromX, toX, new Duration(TimeSpan.FromSeconds(0.2)), FillBehavior.Stop);
            horAnimation.Completed += (sender_, e_) =>
            {
                ttModel.X = toX;
            };
            ttModel.BeginAnimation(TranslateTransform.XProperty, horAnimation);

            var vertAnimation = new DoubleAnimation(fromY, toY, new Duration(TimeSpan.FromSeconds(0.2)), FillBehavior.Stop);
            vertAnimation.Completed += (sender_, e_) =>
            {
                ttModel.Y = toY;
            };
            ttModel.BeginAnimation(TranslateTransform.YProperty, vertAnimation);
        }

        public void ChangePointsVisibility(Visibility visibility)
        {
            var query = from ui in cnvModel.Children.Cast<UIElement>()
                        where ui is Ellipse && (ui as Ellipse).Tag != null && (ui as Ellipse).Tag.ToString() == "point"
                        select ui as Ellipse;

            foreach (var ui in query)
            {
                ui.Visibility = visibility;
            }

            ArePointsVisible = (visibility == Visibility.Visible);

            if (!IsMaster && Clone != null) Clone.ChangePointsVisibility(visibility);
        }

        public void ChangeCountersVisibility(Visibility visibility)
        {
            var query = from ui in cnvModel.Children.Cast<UIElement>()
                        where ui is DataCollectionControl && (ui as DataCollectionControl).Tag != null && (ui as DataCollectionControl).Tag.ToString() == "counter"
                        select ui as DataCollectionControl;

            foreach (var ui in query)
            {
                ui.Visibility = visibility;
            }

            AreCountersVisible = (visibility == Visibility.Visible);

            if (!IsMaster && Clone != null) Clone.ChangeCountersVisibility(visibility);
        }

        #region DataCollectionControl

        private void DataCollectionControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.ClickCount == 1)
                {
                    var dc = (sender as DataCollectionControl);
                    //var dcbs = dc.DataContext as DataCollectionBindingSource;

                    dccStartPoint = Mouse.GetPosition(this);

                    dc.datagrid.IsEnabled = false;
                    e.Handled = true;
                }
            }
        }

        private void DataCollectionControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var dc = (sender as DataCollectionControl);
            dc.datagrid.IsEnabled = true;
        }

        private void DataCollectionControl_MouseMove(object sender, MouseEventArgs e)
        {
            var dc = (sender as DataCollectionControl);
            var dcbs = dc.DataContext as DataCollectionBindingSource;

            Point endPoint = e.GetPosition(this);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var dif = endPoint - dccStartPoint;

                dcbs.Center = new Point(
                    dcbs.Center.X + (dif.X * (1 / stModel.ScaleX)),
                    dcbs.Center.Y - (dif.Y * (1 / stModel.ScaleY)));

                Canvas.SetLeft(dc, dcbs.Center.X);
                Canvas.SetTop(dc, dcbs.Center.Y);
            }

            dccStartPoint = endPoint;

            ClearLines();
            DrawLines(dcbs, dc);

            e.Handled = true;
        }

        private void DataCollectionControl_MouseEnter(object sender, MouseEventArgs e)
        {
            var dc = (sender as DataCollectionControl);
            var dcbs = dc.DataContext as DataCollectionBindingSource;

            var bc = new BrushConverter();
            dc.OpacityMask = (Brush)bc.ConvertFrom("#73000000");

            DrawLines(dcbs, dc);
        }

        private void DataCollectionControl_MouseLeave(object sender, MouseEventArgs e)
        {
            var dc = (sender as DataCollectionControl);
            var dcbs = dc.DataContext as DataCollectionBindingSource;

            dc.OpacityMask = Brushes.Black;

            ClearLines();
        }

        private void DrawLines(DataCollectionBindingSource dcbs, DataCollectionControl dc)
        {
            var group = (TransformGroup)(dc.RenderTransform);
            var trans = (ScaleTransform)group.Children[0];

            int i = 0;
            foreach (var p in dcbs.Points)
            {
                ++i;
                Line l = new Line()
                {
                    X1 = (dcbs.Center.X),
                    Y1 = dcbs.Center.Y - (dc.ActualHeight / 2) * trans.ScaleX,
                    X2 = p.X,
                    Y2 = p.Y,
                    Stroke = dc.Tag.ToString() == "counter" ? Brushes.Gold : i == 1 ? Brushes.Red : Brushes.Green,
                    StrokeThickness = 0.5 * trans.ScaleX,
                };

                Panel.SetZIndex(l, 150);

                lines.Add(l);
                cnvModel.Children.Add(l);
            }
        }

        private void ClearLines()
        {
            foreach (var l in lines)
            {
                cnvModel.Children.Remove(l);
            }
        }

        #endregion

        #region Ellipse

        double
            ellipseNormalSize = 20,
            ellipseActiveSize = 26;

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((sender as Ellipse).Tag != null && (sender as Ellipse).Tag.ToString() == "point") ResizeEllipse((sender as Ellipse), ellipseActiveSize);
        }

        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((sender as Ellipse).Tag != null && (sender as Ellipse).Tag.ToString() == "point") ResizeEllipse((sender as Ellipse), ellipseNormalSize);
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Ellipse).Tag != null && (sender as Ellipse).Tag.ToString() == "point")
            {
                var collectionPoint = (sender as Ellipse).DataContext as PointsListItem;

                var point = (from item in dgCounters.Items.Cast<PointsListItem>()
                             where item.Coord.X == collectionPoint.Coord.X && item.Coord.Y == collectionPoint.Coord.Y
                             select item).FirstOrDefault();

                if (point != null)
                {
                    dgCounters.ScrollIntoView(point);
                    dgCounters.SelectedValue = point;
                }
            }
        }

        private void ResizeEllipse(Ellipse ellipse, double size)
        {
            double
                dX = ellipse.Width - size,
                dY = ellipse.Height - size;

            ellipse.Width = size;
            ellipse.Height = size;

            Canvas.SetLeft(ellipse, Canvas.GetLeft(ellipse) + dX / 2);
            Canvas.SetTop(ellipse, Canvas.GetTop(ellipse) + dY / 2);
        }

        #endregion

        private void Polyline_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                Point point = e.GetPosition(cnvModel);
                MoveToPoint(point);

                // move all models in the report control list
                RaiseEvent(new RoutedEventArgs<Point>(point, ModelControl.ModelDoubleClickEvent));
            }
        }

        double xFactor = 1, yFactor = 1;
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (btnFixedPoints.IsChecked.Value)
            {
                GlobalFixedScale.ScaleX = xFactor / stModel.ScaleX;
                GlobalFixedScale.ScaleY = yFactor / stModel.ScaleY;
            }
        }

        private void btnFixedPoints_Checked(object sender, RoutedEventArgs e)
        {
            xFactor = GlobalFixedScale.ScaleX * stModel.ScaleX;
            yFactor = GlobalFixedScale.ScaleY * stModel.ScaleY;
        }

        private void btnFixedPoints_Unchecked(object sender, RoutedEventArgs e)
        {
            xFactor = yFactor = 1;
        }

        private void flyingGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            countersGridStartPoint = Mouse.GetPosition(this);
            isCountersGridFlying = true;

            cnvModel.IsEnabled = false;
            dgCounters.IsEnabled = false;

            e.Handled = true;
        }

        private void datagrid_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var point = (e.AddedItems[0] as PointsListItem).Coord;
                MoveToPoint(point);

                if (IsMaster && Clone != null) Clone.MoveToPoint(point);
            }
        }

        public void CloseCountersGrid()
        {
            if (countersGrid.Visibility == Visibility.Visible) ChangeDataGridVisibility(1, 0, stBorder);
            else
            {
                if (!IsMaster && Clone != null) Clone.CloseCountersGrid();
            }
        }

        public void OpenCountersGrid()
        {
            if (countersGrid.Visibility == Visibility.Visible) ChangeDataGridVisibility(0, 1, stBorder);
            else
            {
                if (!IsMaster && Clone != null) Clone.OpenCountersGrid();
            }
        }

        private void ChangeDataGridVisibility(int from, int to, ScaleTransform st)
        {
            var scaleYAnimation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(0.4)), FillBehavior.Stop);
            scaleYAnimation.Completed += (sender__, e__) => st.ScaleY = to;

            st.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);

            var scaleXAnimation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(0.4)), FillBehavior.Stop);
            scaleXAnimation.Completed += (sender__, e__) => st.ScaleX = to;

            st.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        }

        private void checkGrouping_Checked(object sender, RoutedEventArgs e)
        {
            var view = dgCounters.ItemsSource as ICollectionView;
            view.GroupDescriptions.Clear();
            var g = new PropertyGroupDescription(DEFAULT_GROUPING_FIELD_NAME);
            view.GroupDescriptions.Add(g);
        }

        private void checkGrouping_Unchecked(object sender, RoutedEventArgs e)
        {
            var view = dgCounters.ItemsSource as ICollectionView;
            view.GroupDescriptions.Clear();
        }

        #region trTimesGrid

        public void ChangeSectionsVisibility(Visibility visibility)
        {
            var query = from ui in cnvModel.Children.Cast<UIElement>()
                        where ui is Ellipse && (ui as Ellipse).Tag != null && (ui as Ellipse).Tag.ToString() == "section"
                        select ui as Ellipse;

            foreach (var ui in query)
            {
                ui.Visibility = visibility;
            }

            AreSectionsVisible = (visibility == Visibility.Visible);

            if (!IsMaster && Clone != null) Clone.ChangeSectionsVisibility(visibility);
        }

        public void ChangeTrTimesVisibility(Visibility visibility)
        {
            var query = from ui in cnvModel.Children.Cast<UIElement>()
                        where ui is DataCollectionControl
                        && (ui as DataCollectionControl).Tag != null
                        && (ui as DataCollectionControl).Tag.ToString() == "tr.times"
                        select ui as DataCollectionControl;

            foreach (var ui in query)
            {
                ui.Visibility = visibility;
            }

            AreTrTimesVisible = (visibility == Visibility.Visible);

            if (!IsMaster && Clone != null) Clone.ChangeTrTimesVisibility(visibility);
        }

        public void CloseTrTimesGrid()
        {
            if (trTimesGrid.Visibility == Visibility.Visible) ChangeDataGridVisibility(1, 0, stTrTimesGrid);
            else
            {
                if (!IsMaster && Clone != null) Clone.CloseTrTimesGrid();
            }
        }

        public void OpenTrTimesGrid()
        {
            if (trTimesGrid.Visibility == Visibility.Visible) ChangeDataGridVisibility(0, 1, stTrTimesGrid);
            else
            {
                if (!IsMaster && Clone != null) Clone.OpenTrTimesGrid();
            }
        }

        private void trTimesGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            trTimesGridStartPoint = Mouse.GetPosition(this);
            isTrTimesGridFlying = true;

            cnvModel.IsEnabled = false;
            dgTrTimes.IsEnabled = false;

            e.Handled = true;
        }

        private void dgTrTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var point = (e.AddedItems[0] as SectionListItem).FromCoord;
                MoveToPoint(point);

                if (IsMaster && Clone != null) Clone.MoveToPoint(point);
            }
        }

        private void dgTrTimes_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }

        #endregion


        double saveddgCountersHeight;
        private void checkMini_Checked(object sender, RoutedEventArgs e)
        {
            saveddgCountersHeight = dgCounters.Height;
            dgCounters.Height = countersGrid.MinHeight;
        }

        private void checkMini_Unchecked(object sender, RoutedEventArgs e)
        {
            dgCounters.Height = saveddgCountersHeight;
        }
    }
}
