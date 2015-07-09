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
using VisLab.Classes.Integration.Entities;

namespace VisLab.Classes.Implementation.Analysis.Boundaries.Controls
{
    /// <summary>
    /// Interaction logic for ChartControl.xaml
    /// </summary>
    public partial class ChartControl : AnimatedControl
    {
        public ChartControl()
        {
            InitializeComponent();
        }

        private void cbxMeasures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var view = this.datagrid.ItemsSource as CollectionView;
            string parameter = (chart.Title as TextBlock).Text;
            string id = cbxMeasures.SelectedValue.ToString();

            view.Filter = (item) =>
                {
                    var desc = item as ItemDescriptor;
                    return desc.ColName == parameter && desc.CounterId == id;
                };
        }
    }
}
