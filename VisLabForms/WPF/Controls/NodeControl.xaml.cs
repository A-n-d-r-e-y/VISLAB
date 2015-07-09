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
    /// Interaction logic for NodeControl.xaml
    /// </summary>
    public partial class NodeControl : UserControl
    {
        public static readonly RoutedEvent SelectEvent =
            EventManager.RegisterRoutedEvent("Select", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NodeControl));

        public event RoutedEventHandler Select
        {
            add { AddHandler(SelectEvent, value); }
            remove { RemoveHandler(SelectEvent, value); }
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
            zIndexBufer = Canvas.GetZIndex(this);
            Canvas.SetZIndex(this, 99999);
        }

        private void expander_Collapsed(object sender, RoutedEventArgs e)
        {
            Canvas.SetZIndex(this, zIndexBufer);
        }

        private void border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (expander.IsExpanded) expander.IsExpanded = false;
        }
    }
}
