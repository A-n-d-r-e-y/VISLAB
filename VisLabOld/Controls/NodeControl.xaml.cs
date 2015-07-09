using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisLab.Classes;
using System.Windows.Media.Animation;
using System;

namespace VisLab.Controls
{
    /// <summary>
    /// Interaction logic for NodeControl.xaml
    /// </summary>
    public partial class NodeControl : UserControl
    {
        public static readonly RoutedEvent SelectEvent =
            EventManager.RegisterRoutedEvent("Select", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NodeControl));

        public static readonly RoutedEvent LoadEvent =
            EventManager.RegisterRoutedEvent("Load", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NodeControl));

        public static readonly RoutedEvent DeleteEvent =
            EventManager.RegisterRoutedEvent("Delete", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NodeControl));

        public event RoutedEventHandler Select
        {
            add { AddHandler(SelectEvent, value); }
            remove { RemoveHandler(SelectEvent, value); }
        }

        public event RoutedEventHandler Load
        {
            add { AddHandler(LoadEvent, value); }
            remove { RemoveHandler(LoadEvent, value); }
        }

        public event RoutedEventHandler Delete
        {
            add { AddHandler(DeleteEvent, value); }
            remove { RemoveHandler(DeleteEvent, value); }
        }

        public string Header
        {
            get { return expander.Header.ToString(); }
            set { expander.Header = value; }
        }

        //public bool IsExpanded
        //{
        //    get { return expander.IsExpanded; }
        //    set { expander.IsExpanded = value; }
        //}

        public bool IsSelected { get; set; }

        //public int Counter { get; set; }

        public static readonly DependencyProperty CounterProperty =
            DependencyProperty.Register(
            "Counter",
            typeof(int),
            typeof(NodeControl));

        public int Counter
        {
            get { return (int)GetValue(CounterProperty); }
            set { SetValue(CounterProperty, value); }
        }

        public NodeControl()
        {
            InitializeComponent();
        }

        private void ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(NodeControl.SelectEvent));
        }

        private int zIndexBufer = 1;

        private void expander_Expanded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation anime = new DoubleAnimation(0, this.MaxHeight, new Duration(TimeSpan.FromSeconds(0.3)), FillBehavior.HoldEnd);

            zIndexBufer = Canvas.GetZIndex(this);
            Canvas.SetZIndex(this, 99999);

            grid2.BeginAnimation(Grid.HeightProperty, anime);
        }

        private void expander_Collapsed(object sender, RoutedEventArgs e)
        {
            Canvas.SetZIndex(this, zIndexBufer);
        }

        private void border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (expander.IsExpanded) expander.IsExpanded = false;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(NodeControl.LoadEvent));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(NodeControl.DeleteEvent));
        }
    }
}
