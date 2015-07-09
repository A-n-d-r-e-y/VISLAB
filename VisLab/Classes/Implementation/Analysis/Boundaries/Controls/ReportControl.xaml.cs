using System.Windows.Controls;
using System.Collections.Specialized;
using VisLab.Classes.Implementation.Analysis.Controllers;
using VisLab.Controls;
using VisLab.Classes.Integration.Entities;
using System.Windows.Media.Animation;
using System.Windows;
using System;
using System.Windows.Media;
using VisLab.Classes.Implementation.Design;
using System.Windows.Input;
using System.Linq;
using System.Data;

namespace VisLab.Classes.Implementation.Analysis.Boundaries.Controls
{
    /// <summary>
    /// Interaction logic for ReportControl.xaml
    /// </summary>
    public partial class ReportControl : UserControl
    {
        public ReportControl()
        {
            InitializeComponent();

            ((INotifyCollectionChanged)listbox.Items).CollectionChanged += listbox_CollectionChanged; 
        }

        private void listbox_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    listbox.ScrollIntoView(e.NewItems[0]);
                    listbox.SelectedItem = e.NewItems[0];
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    int n = listbox.Items.Count - 1;
                    if (n >= 0)
                    {
                        var item = listbox.Items[n];
                        listbox.ScrollIntoView(item);
                        listbox.SelectedItem = item;
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                default:
                    break;
            }
        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rbs = (this.DataContext as ReportBindingSource);

            if (e.AddedItems.Count > 0)
            {
                var modelControl = (e.AddedItems[0] as ModelControl);
                rbs.CopyToMaster(modelControl);
            }

            if (e.RemovedItems.Count > 0 && (sender as ListBox).Items.Count == 0) rbs.ClearMaster();
        }

        private void btnCountersReport_Click(object sender, RoutedEventArgs e)
        {
            var rbs = (this.DataContext as ReportBindingSource);
            rbs.ShowCountersReport();
        }

        private void btnTrTimesReport_Click(object sender, RoutedEventArgs e)
        {
            var rbs = (this.DataContext as ReportBindingSource);
            rbs.ShowTrTimesReport();
        }
    }
}
