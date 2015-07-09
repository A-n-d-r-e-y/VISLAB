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
using System.ComponentModel;
using System.Data;
using VisLab.Classes.Implementation.Design;
using VisLab.Controls;
using VisLab.Classes.Implementation.Design.BindingSources;
using System.IO;
using VisLab.Classes.Implementation.Wrappers;
using System.Windows.Interop;
using VisLab.Windows;
using System.Diagnostics;
using VisLab.Classes.Implementation.Design.Utilities;
using System.Globalization;
using System.Windows.Controls.DataVisualization.Charting;
using VisLab.Classes.Implementation.Entities;
using VisLab.Classes.Implementation.Analysis.Controllers;
using Microsoft.Win32;
using VisLab.Classes.Integration.Entities;
using VisLab.Properties;

namespace VisLab.Classes.Implementation.Analysis.Boundaries.Controls
{
    /// <summary>
    /// Interaction logic for CountersReportControl.xaml
    /// </summary>
    public partial class CountersReportControl : UserControl
    {
        ShadowMaker shadowMaker;
        List<ColumnSelectionBindingSource> columnsList;
        ProjectManager manager;
        IEnumerable<ItemDescriptor> items;

        public CountersReportControl(string header, ProjectManager manager, IEnumerable<ItemDescriptor> items)
        {
            InitializeComponent();

            if (header == "Counters Report")
            {
                var binding = new Binding
                {
                    Source = Settings.Default,
                    Path = new PropertyPath("CountersReportControl_cbxColumnsList_SelectedIndex1"),
                    Mode = BindingMode.TwoWay,
                };

                cbxColumnsList.SetBinding(ComboBox.SelectedIndexProperty, binding);

                binding = new Binding
                {
                    Source = Settings.Default,
                    Path = new PropertyPath("CountersReportControl_tbxFilter1"),
                    Mode = BindingMode.TwoWay,
                };

                tbxFilter.SetBinding(TextBox.TextProperty, binding);
            }
            else
            {
                var binding = new Binding
                {
                    Source = Settings.Default,
                    Path = new PropertyPath("CountersReportControl_cbxColumnsList_SelectedIndex2"),
                    Mode = BindingMode.TwoWay,
                };

                cbxColumnsList.SetBinding(ComboBox.SelectedIndexProperty, binding);

                binding = new Binding
                {
                    Source = Settings.Default,
                    Path = new PropertyPath("CountersReportControl_tbxFilter2"),
                    Mode = BindingMode.TwoWay,
                };

                tbxFilter.SetBinding(TextBox.TextProperty, binding);
            }

            this.items = items;
            this.manager = manager;
            tbkHeader.Text = header;
            shadowMaker = new ShadowMaker();
            ccTopLayerHost.DataContext = shadowMaker;
        }

        private void rbOnlyWithData_Checked(object sender, RoutedEventArgs e)
        {
            if (datagrid.Columns.Count > 0)
            {
                var view = datagrid.ItemsSource as ICollectionView;
                var dt = (view.SourceCollection as DataView).Table;

                foreach (DataColumn col in dt.Columns)
                {
                    var row = (from r in dt.Rows.Cast<DataRow>()
                               where string.IsNullOrWhiteSpace(r[col.ColumnName].ToString())
                               select r).FirstOrDefault();

                    if (row != null) datagrid.Columns[col.Ordinal].Visibility = Visibility.Collapsed;
                    else datagrid.Columns[col.Ordinal].Visibility = Visibility.Visible;
                }

                datagrid.Items.Refresh();
            }
        }

        private void rbSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            if (datagrid != null && datagrid.HasItems && datagrid.Columns.Count > 0)
            {
                foreach (var item in datagrid.Columns)
                {
                    if (item.Visibility != Visibility.Visible) item.Visibility = Visibility.Visible;
                }
            }
        }

        private void rbCustom_Checked(object sender, RoutedEventArgs e)
        {
            var colSelectionCtrl = new ColumnSelectionControl();

            colSelectionCtrl.DataContext = columnsList;
            colSelectionCtrl.btnClose.Click += (s_, e_) =>
                {
                    foreach (var item in columnsList)
                    {
                        datagrid.Columns[item.Ordinal].Visibility = item.IsVisible ? Visibility.Visible : Visibility.Collapsed;
                    }

                    shadowMaker.ShadowDown();
                    datagrid.Items.Refresh();
                };

            shadowMaker.ShowThis(colSelectionCtrl);

            shadowMaker.ShadowUp();
        }

        private void btnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            //ClipboardToExcel();

            //var view = datagrid.ItemsSource as ICollectionView;
            //var dt = (view.SourceCollection as DataView).Table;

            //foreach (DataColumn col in dt.Columns)
            //{
            //    var row = (from r in dt.Rows.Cast<DataRow>()
            //               where string.IsNullOrWhiteSpace(r[col.ColumnName].ToString())
            //               select r).FirstOrDefault();

            //    if (row != null) datagrid.Columns[col.Ordinal].Visibility = Visibility.Collapsed;
            //    else datagrid.Columns[col.Ordinal].Visibility = Visibility.Visible;
            //}

            var view = (datagrid.ItemsSource as BindingListCollectionView).SourceCollection as DataView;

            var columns = from col in view.Table.Columns.Cast<DataColumn>()
                          select col.ColumnName;

            var sb = new StringBuilder();

            foreach (var col in columns)
            {
                sb.AppendFormat("{0};", col);
            }
            sb.Append('\n');


            var rows = from rowView in view.Cast<DataRowView>()
                       select rowView.Row;

            foreach (var row in rows)
            {
                foreach (object obj in row.ItemArray)
                {
                    sb.AppendFormat("{0};", obj);
                }
                sb.Append('\n');
            }

            var dlg = new SaveFileDialog();
            dlg.FileName = string.Format("VisLab_{0}", tbkHeader.Text);
            dlg.DefaultExt = "CSV";
            dlg.Filter = "CSV files|*.csv";

            if (dlg.ShowDialog().Value)
            {
                File.WriteAllText(dlg.FileName, sb.ToString().Replace('\x2024', '.'));

                Process.Start(dlg.FileName);
            }
        }

        private void ClipboardToExcel()
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            try
            {
                datagrid.SelectAllCells();
                try
                {
                    ApplicationCommands.Copy.Execute(null, datagrid);
                }
                catch (Exception ex)
                {
                    RemoteLogger.ReportIssueAsync(ex);
                    MessageBox.Show(App.Current.MainWindow, ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    datagrid.UnselectAllCells();
                }

                var dlg = new System.Windows.Forms.FolderBrowserDialog();
                var ww = new WindowWrapper(new WindowInteropHelper(App.Current.MainWindow).Handle);
                if (dlg.ShowDialog(ww) == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = System.IO.Path.Combine(dlg.SelectedPath, string.Format("VisLab_{0}.csv", tbkHeader.Text));
                    string data = (string)Clipboard.GetData(DataFormats.Text);

                    Clipboard.Clear();

                    if (data != null) data = data.Replace('\t', ';').Replace('?', '.');

                    File.WriteAllText(fileName, data);

                    Process.Start(fileName);
                }
            }
            finally
            {
                datagrid.SelectionMode = DataGridSelectionMode.Single;
            }
        }

        private void tbxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.datagrid != null && datagrid.ItemsSource != null) Filtering();
        }

        private void Filtering()
        {
            var view = datagrid.ItemsSource as ICollectionView;
            var dt = (view.SourceCollection as DataView).Table;

            if (string.IsNullOrWhiteSpace(tbxFilter.Text)) dt.DefaultView.RowFilter = null;
            else
            {
                dt.DefaultView.RowFilter = string.Format("[{1}] LIKE '*{0}*'"
                    , tbxFilter.Text
                    , (cbxColumnsList.SelectedItem as ColumnSelectionBindingSource).Header);
            }
        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            columnsList = (from c in datagrid.Columns.Cast<DataGridColumn>()
                           select new ColumnSelectionBindingSource()
                           {
                               Ordinal = c.DisplayIndex,
                               Header = ((c.HeaderTemplate.LoadContent() as StackPanel).Children[1] as TextBlock).Text,
                               IsVisible = c.Visibility == Visibility.Visible
                           }).ToList();

            //var view = new CollectionViewSource();
            //view.Source = columnsList;
            //view.View.Filter = (item) => (item as ColumnSelectionBindingSource).IsVisible;

            if (Settings.Default.CountersReportControl_cbxColumnsList_SelectedIndex1 >= columnsList.Count)
            {
                Settings.Default.CountersReportControl_cbxColumnsList_SelectedIndex1 = 0;
            }

            cbxColumnsList.ItemsSource = columnsList;
            //cbxColumnsList.SelectedIndex = 0;
        }

        private void cbxColumnsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtering();
        }

        private void datagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = (string)e.Column.Header;

            //var stack = new StackPanel();
            //stack.Orientation = Orientation.Horizontal;

            //var btn = new Button()
            //{
            //    Content = new Image()
            //    {
            //        Source = new BitmapImage(new Uri("/Styles/chart_line.png", UriKind.Relative))
            //    },
            //    Margin = new Thickness(1, 1, 5, 1),
            //};

            //if (header == "Experiment" || header == "Measur\x2024") btn.Visibility = Visibility.Collapsed;

            //btn.Click += (s_, e_) => GenerateSeries(header);

            //stack.Children.Add(btn);

            //stack.Children.Add(new TextBlock()
            //{
            //    Text = header,
            //    Background = Brushes.Transparent,
            //    //FontSize = 8,
            //});



            var template = new DataTemplate();

            var stackFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackFactory.Name = "stack";
            stackFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            var img = new FrameworkElementFactory(typeof(Image));
            Binding binding = new Binding();
            binding.Source = new Uri("/Styles/chart_line.png", UriKind.Relative);
            img.SetBinding(Image.SourceProperty, binding);
            img.SetValue(Image.VisibilityProperty, Visibility.Visible);
            img.AddHandler(Image.MouseDownEvent, new MouseButtonEventHandler((s_, e_) =>
                {
                    GenerateSeries(header);
                    e_.Handled = true;
                }));
            img.SetValue(Image.MarginProperty, new Thickness(1, 1, 5, 1));
            img.SetValue(Image.CursorProperty, Cursors.Hand);
            img.SetValue(Image.ToolTipProperty, "Click to view Detailed Report");
            if (header == "Experiment" || header == "Measur\x2024") img.SetValue(Image.VisibilityProperty, Visibility.Collapsed);
            stackFactory.AppendChild(img);

            var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockFactory.SetValue(TextBlock.TextProperty, header);
            textBlockFactory.SetValue(TextBlock.BackgroundProperty, Brushes.Transparent);
            stackFactory.AppendChild(textBlockFactory);

            template.VisualTree = stackFactory;


            e.Column.HeaderTemplate = template;
        }

        private string GetValueFromProject(string parameter, int measurement)
        {
            string value = (from p in manager.Project.ExpectedParameters.Parameter
                            where p.Name == parameter && p.No == measurement
                            select p.Value).FirstOrDefault();

            double parsedValue = 0;

            if (value != null)
            {
                if (!double.TryParse(value.Replace(',', '.')
                    , System.Globalization.NumberStyles.Number
                    , CultureInfo.GetCultureInfo("en-US")
                    , out parsedValue))
                {
                    parsedValue = 0;
                }
            }

            return parsedValue.ToString();
        }

        private void GenerateSeries(string header)
        {
            var chart = new ChartControl();
            chart.datagrid.ItemsSource = CollectionViewSource.GetDefaultView(items);

            var view = (datagrid.ItemsSource as BindingListCollectionView).SourceCollection as DataView;

            var measurements = (from row in view.Cast<DataRowView>()
                                where !string.IsNullOrWhiteSpace(row.Row.Field<string>(header))
                                orderby int.Parse(row.Row.Field<string>("Measur\x2024"))
                                select row.Row.Field<string>("Measur\x2024")).Distinct();

            chart.tbxExpectedValue.Text = GetValueFromProject(header, (from m in measurements select int.Parse(m)).Min());

            chart.grid.MaxWidth = this.ActualWidth - 50;
            chart.grid.MinWidth = 500;
            chart.grid.MinHeight = 300;
            chart.grid.MaxHeight = App.Current.MainWindow.ActualHeight - 150;

            chart.chart.Title = new TextBlock()
            {
                Text = header,
                FontSize = 24,
                FontWeight = FontWeights.Bold,
            };

            var data = from r in
                           (
                               from row in view.Cast<DataRowView>()
                               where !string.IsNullOrWhiteSpace(row.Row.Field<string>(header))
                               select new
                               {
                                   Experiment = row.Row.Field<string>("Experiment"),
                                   Measure = row.Row.Field<string>("Measur\x2024"),
                                   Column = row.Row.Field<string>(header),
                               })
                       group r by r.Measure into gr
                       select new
                       {
                           Measure = gr.Key,
                           Data = from g in gr
                                  orderby g.Experiment
                                  select new
                                  {
                                      Experiment = g.Experiment,
                                      Value = double.Parse(g.Column.Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US")),
                                  }
                       };

            foreach (var item in data)
            {
                var series = new LineSeries()
                {
                    Title = item.Measure,
                    ItemsSource = item.Data,
                    IndependentValueBinding = new Binding("Experiment"),
                    DependentValueBinding = new Binding("Value"),
                };

                chart.chart.Series.Add(series);
            }

            double baseWidth = chart.grid.MinWidth;
            double baseHeight = chart.grid.MinHeight;
            chart.grid.Height = baseHeight + 180 * data.Count();
            chart.grid.Width = baseWidth + 180 * data.Count();

            double expectedValue;
            if (!double.TryParse(chart.tbxExpectedValue.Text.Replace(',', '.')
                , System.Globalization.NumberStyles.Number
                , CultureInfo.GetCultureInfo("en-US")
                , out expectedValue))
            {
                expectedValue = 0;
            }

            Style dataPointStyle = new Style(typeof(LineDataPoint));
            dataPointStyle.Setters.Add(new Setter(LineDataPoint.TemplateProperty, null));

            Style polyLineStyle = new Style(typeof(Polyline));
            polyLineStyle.Setters.Add(new Setter(Polyline.StrokeProperty, Brushes.Gray));
            polyLineStyle.Setters.Add(new Setter(Polyline.StrokeThicknessProperty, 5.0));
            polyLineStyle.Setters.Add(new Setter(Polyline.StrokeDashArrayProperty, new DoubleCollection(new double[] { 1, 2 })));

            var expected = new LineSeries()
            {
                Title = "Expected value",
                ItemsSource = from exp in
                                  (from d in data
                                   from x in d.Data
                                   select x.Experiment).Distinct()
                              select new
                              {
                                  Experiment = exp,
                                  Value = expectedValue,
                              },
                IndependentValueBinding = new Binding("Experiment"),
                DependentValueBinding = new Binding("Value"),
                DataPointStyle = dataPointStyle,
                PolylineStyle = polyLineStyle,
            };

            chart.chart.Series.Add(expected);

            chart.cbxMeasures.ItemsSource = measurements;

            chart.cbxMeasures.SelectionChanged += (s_, e_) =>
                {
                    chart.tbxExpectedValue.Text = GetValueFromProject(header, int.Parse(chart.cbxMeasures.SelectedValue.ToString()));

                    double value;

                    if (!double.TryParse(chart.tbxExpectedValue.Text.Replace(',', '.')
                        , System.Globalization.NumberStyles.Number
                        , CultureInfo.GetCultureInfo("en-US")
                        , out value)) value = 0;

                    expected.ItemsSource = from exp in
                                               (from d in data
                                                from x in d.Data
                                                select x.Experiment).Distinct()
                                           select new
                                           {
                                               Experiment = exp,
                                               Value = value,
                                           };
                };

            chart.btnAddExpectedValue.Click += (s_, e_) =>
            {
                double value;
                if (double.TryParse(chart.tbxExpectedValue.Text.Replace(',', '.')
                    , System.Globalization.NumberStyles.Number
                    , CultureInfo.GetCultureInfo("en-US")
                    , out value))
                {
                    expected.ItemsSource = from exp in
                                               (from d in data
                                                from x in d.Data
                                                select x.Experiment).Distinct()
                                           select new
                                           {
                                               Experiment = exp,
                                               Value = value,
                                           };

                    if (value != 0)
                    {
                        var param = (from p in manager.Project.ExpectedParameters.Parameter
                                     where p.Name == header && p.No == int.Parse(chart.cbxMeasures.Text)
                                     select p).FirstOrDefault();

                        if (param == null)
                        {
                            manager.Project.ExpectedParameters.Parameter.Add(new Project.ExpectedParametersSection.ParameterElement()
                            {
                                Name = header,
                                No = int.Parse(chart.cbxMeasures.Text),
                                Value = chart.tbxExpectedValue.Text,
                            });
                        }
                        else
                        {
                            param.Value = chart.tbxExpectedValue.Text;
                        }

                        manager.SaveProject();
                    }
                }
                else chart.tbxExpectedValue.Text = "0";
            };

            chart.btnClose.Click += (s_, e_) =>
            {
                shadowMaker.ShadowDown();
            };

            shadowMaker.ShowThis(chart);

            shadowMaker.ShadowUp();
        }
    }
}
