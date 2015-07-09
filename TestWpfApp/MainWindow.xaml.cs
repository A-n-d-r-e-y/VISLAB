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
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data;
using System.ComponentModel;
using TestWpfApp.vislab;

namespace TestWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckQuery()
        {
            var dirs = new[]
            {
                new { ExpName = "Jelgava#1", Path = @"D:\Desktop\Project1\Experiments\Jelgava#0\#1\Jelgava.model"},
                new { ExpName = "Jelgava#2", Path = @"D:\Desktop\Project1\Experiments\Jelgava#0\#1\#2\Jelgava.model"},
            };

            var experiments = from d in dirs
                              select new DataCollection()
                              {
                                  ExperimentName = d.ExpName,
                                  AggregatedData = GetList(d.Path)
                              };

            StringBuilder sb = new StringBuilder();

            foreach (var exp in experiments)
            {
                foreach (var aggr in exp.AggregatedData)
                {
                    foreach (var cell in aggr.Cells)
                    {
                        sb.AppendFormat("{5}: id={0}, {1} ({2}) {3} = {4}\n"
                            , aggr.Id
                            , cell.Header
                            , cell.Function
                            , cell.VehType
                            , cell.Value
                            , exp.ExperimentName);
                    }
                }
            }

            MessageBox.Show(sb.ToString());
            
            this.Close();
        }

        private void ShowReport()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("Experiment"));

            var dirs = new[]
            { 
                new { ExpName = "KA_3D#2", Path = @"D:\MOPO3OB\Desktop\Project2\Experiments\KA_3D#0\#2\KA_3D.model"},
                new { ExpName = "beijing#1", Path = @"D:\MOPO3OB\Desktop\Project2\Experiments\beijing#0\#1\beijing.model"},
            };

            var experiments = from d in dirs
                              select new DataCollection()
                              {
                                  ExperimentName = d.ExpName,
                                  AggregatedData = GetList(d.Path)
                              };

            // columns
            var parameters = from exp in experiments
                             from data in exp.AggregatedData
                             from cell in data.Cells
                             group cell by new { F = cell.Function, H = cell.Header, V = cell.VehType } into gr
                             select string.Format("{0}({1}){2}", gr.Key.H, gr.Key.F, gr.Key.V).Replace(" ", "_").Replace(".", "\x2024");

            // creation columns
            foreach (var param in parameters)
            {
                var col = new DataColumn(param);
                dt.Columns.Add(col);
            }

            var xxx = from exp in experiments
                      from data in exp.AggregatedData
                      //group data by new { data.Id, exp.ExperimentName } into gr
                      select new
                      {
                          CounterId = data.Id.ToString(),
                          ExpName = exp.ExperimentName
                      };

            foreach (var item in xxx)
            {
                var row = dt.NewRow();
                row[0] = item.ExpName;
                row[1] = item.CounterId;
                dt.Rows.Add(row);
            }

            var query2 = from exp in experiments
                         from data in exp.AggregatedData
                         from cell in data.Cells
                         select new
                         {
                             CounterId = data.Id.ToString(),
                             ColName = string.Format("{0}({1}){2}", cell.Header, cell.Function, cell.VehType).Replace(" ", "_").Replace(".", "\x2024"),
                             RowName = exp.ExperimentName,
                             Value = cell.Value
                         };

            foreach (var item in query2)
            {
                var row = dt.Select(string.Format("[Experiment] = '{0}' AND [Measur\x2024()] = '{1}'", item.RowName, item.CounterId)).FirstOrDefault();
                row[item.ColName] = item.Value;
            }

            ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dt);
            if (cvTasks != null && cvTasks.CanGroup == true)
            {
                cvTasks.GroupDescriptions.Clear();
                cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("Measur\x2024()"));
            }

            this.DataContext = cvTasks;
        }

        //public MainWindow()
        //{
        //    InitializeComponent();

        //    var dirs = new[]
        //    {
        //        new { ExpName = "Olaine#1", Path = @"D:\Desktop\Project1\Experiments\Jelgava#0\#1\Jelgava.model"},
        //        //new { ExpName = "Olaine#2", Path = @"D:\MOPO3OB\Desktop\Project1\Experiments\Olaine#0\#1\#2\Olaine.model"},
        //        //new { ExpName = "Olaine#3", Path = @"D:\MOPO3OB\Desktop\Project1\Experiments\Olaine#0\#1\#2\#3\Olaine.model"},
        //        //new { ExpName = "Olaine#4", Path = @"D:\MOPO3OB\Desktop\Project1\Experiments\Olaine#0\#1\#2\#3\#4\Olaine.model"},
        //    };

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add(new DataColumn("Description"));

        //    var list = from d in dirs
        //               select new DataCollection()
        //               {
        //                   ExperimentName = d.ExpName,
        //                   AggregatedData = GetList(d.Path)
        //               };

        //    foreach (var item in list)
        //    {
        //        var col = new DataColumn(item.ExperimentName);
        //        dt.Columns.Add(col);
        //    }

        //    var query = from l in list
        //                from a in l.AggregatedData
        //                from c in a.Cells
        //                group c by new { F = c.Function, H = c.Header, V = c.VehType } into gr
        //                select gr.Key;

        //    foreach (var item in query)
        //    {
        //        var row = dt.NewRow();
        //        row[0] = string.Format("{0} ({1}) {2}", item.H, item.F, item.V);
        //        dt.Rows.Add(row);
        //    }

        //    var query2 = from l in list
        //                 from a in l.AggregatedData
        //                 where a.Id == 1
        //                 from c in a.Cells
        //                 select new
        //                 {
        //                     ColName = l.ExperimentName,
        //                     RowName = string.Format("{0} ({1}) {2}", c.Header, c.Function, c.VehType),
        //                     Value = c.Value
        //                 };

        //    foreach (var item in query2)
        //    {
        //        var row = dt.Select(string.Format("Description = '{0}'", item.RowName)).FirstOrDefault();
        //        row[item.ColName] = item.Value;
        //    }

        //    this.DataContext = new[]
        //        {
        //            new { Id = 1, Data = dt.DefaultView },
        //            new { Id = 2, Data = dt.DefaultView },
        //        };
        //}

        private IEnumerable<DataCollection> GetFakeList()
        {
            return new DataCollection[]
            {
                new DataCollection() { ExperimentName = "Olaine#1",
                    AggregatedData = new DataCollectionMeasurement[]
                    {
                        new DataCollectionMeasurement() { Id = 1,
                            Cells = new DataCollectionItem[]
                            {
                                new DataCollectionItem() { Function = "Mean", Header = "Speed", VehType = "all veh. types", Value = "78",},
                                new DataCollectionItem() { Function = "Max", Header = "Speed", VehType = "all veh. types", Value = "100",},
                            } },
                        new DataCollectionMeasurement() { Id = 2,
                            Cells = new DataCollectionItem[]
                            {
                                new DataCollectionItem() { Function = "Mean", Header = "Speed", VehType = "all veh. types", Value = "39",},
                                new DataCollectionItem() { Function = "Max", Header = "Speed", VehType = "all veh. types", Value = "71",},
                            } }
                    } },
                new DataCollection() { ExperimentName = "Olaine#2",
                    AggregatedData = new DataCollectionMeasurement[]
                    {
                        new DataCollectionMeasurement() { Id = 1,
                            Cells = new DataCollectionItem[]
                            {
                                new DataCollectionItem() { Function = "Mean", Header = "Speed", VehType = "all veh. types", Value = "78",},
                                new DataCollectionItem() { Function = "Max", Header = "Speed", VehType = "all veh. types", Value = "100",},
                            } },
                        new DataCollectionMeasurement() { Id = 2,
                            Cells = new DataCollectionItem[]
                            {
                                new DataCollectionItem() { Function = "Mean", Header = "Speed", VehType = "all veh. types", Value = "39",},
                                new DataCollectionItem() { Function = "Max", Header = "Speed", VehType = "all veh. types", Value = "71",},
                            } }
                    } }
            };
        }

        private IEnumerable<DataCollectionMeasurement> GetList(string dir)
        {
            var regex = new Regex(@"^.*;.*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return
                    from fileName in Directory.GetFiles(dir, "*.mes", SearchOption.TopDirectoryOnly)
                    let text = File.ReadAllText(fileName)
                    let matches = regex.Matches(text)
                    where matches.Count > 3
                    let headers = matches[0].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let functions = matches[1].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let vehTypes = matches[2].Value.Split(';').Select(str => str.Trim()).ToArray()
                    from match in matches.Cast<Match>().Where((value, index) => index > 2)
                    let matchArr = match.Value.Split(';').Select((str, index) => new { Value = str.Trim(), Index = index }).ToArray()
                    select new DataCollectionMeasurement()
                    {
                        Id = int.Parse(matchArr[0].Value, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture),
                        Cells = (from value in matchArr
                                 select new DataCollectionItem()
                                 {
                                     Header = headers[value.Index],
                                     Function = functions[value.Index],
                                     VehType = vehTypes[value.Index],
                                     Value = value.Value
                                 })
                    } into raw
                    group raw by raw.Id into gr
                    select new DataCollectionMeasurement()
                    {
                        Id = gr.Key,
                        Cells = (from x in gr
                                 from c in x.Cells
                                 group c by new { H = c.Header, F = c.Function, V = c.VehType } into cx
                                 select new DataCollectionItem()
                                 {
                                     Header = cx.Key.H,
                                     Function = cx.Key.F,
                                     VehType = cx.Key.V,
                                     Value = (from v in cx
                                              select double.Parse(v.Value.Trim(), System.Globalization.NumberStyles.Number, CultureInfo.InvariantCulture))
                                              .Average()
                                              .ToString()
                                 })
                    };

                //return
                //    from fileName in Directory.GetFiles(path, "*.mes", SearchOption.TopDirectoryOnly)
                //    let text = File.ReadAllText(fileName)
                //    let matches = regex.Matches(text)
                //    where matches.Count > 3
                //    let headers = matches[0].Value.Split(';').Select(str => str.Trim()).ToArray()
                //    let functions = matches[1].Value.Split(';').Select(str => str.Trim()).ToArray()
                //    let vehTypes = matches[2].Value.Split(';').Select(str => str.Trim()).ToArray()
                //    from match in matches.Cast<Match>().Where((value, index) => index > 2)
                //    let matchArr = match.Value.Split(';').Select(str => str.Trim()).ToArray()
                //    from value in matchArr
                //    let index = Array.IndexOf(matchArr, value)
                //    select new DataCollectionItem()
                //    {
                //        Header = headers[index],
                //        Function = functions[index],
                //        VehType = vehTypes[index],
                //        Value = value
                //    } into raw
                //    group raw by new { H = raw.Header, F = raw.Function, V = raw.VehType } into gr
                //    select new DataCollectionItem()
                //    {
                //        Header = gr.Key.H,
                //        Function = gr.Key.F,
                //        VehType = gr.Key.V,
                //        Value = (from v in gr
                //                 select double.Parse(v.Value, System.Globalization.NumberStyles.Number, CultureInfo.InvariantCulture))
                //                 .Average()
                //                 .ToString()
                //    };
        }

        private void BrakeSentence(string sentence, string sentence2)
        {
            var query = from word in sentence.Split(' ')
                   from ch in word.ToArray()
                   group ch by ch into gr
                   select new
                   {
                       Ch = gr.Key,
                       Count = gr.Count()
                   };

            var query2 = from word in sentence2.Split(' ')
                        from ch in word.ToArray()
                        group ch by ch into gr
                        select new
                        {
                            Ch = gr.Key,
                            Count = gr.Count()
                        };

            var result = from q in query
                         from q2 in query2
                         select new
                         {
                             Ch = q.Ch,
                             Count1 = q.Count,
                             Count2 = q2.Count,
                         };

            StringBuilder sb = new StringBuilder();

            foreach (var item in result)
            {
                sb.AppendFormat("{0}={1};{2}\n", item.Ch, item.Count1, item.Count2);
            }

            MessageBox.Show(sb.ToString());
        }

        double
            x = 1,
            y = 1;
        private void Button_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var trans = new ScaleTransform();
            (sender as Button).RenderTransform = trans;


            x += (e.Delta > 0) ? 0.1 * x : -0.1 * x;
            y += (e.Delta > 0) ? -0.1 * -y : 0.1 * -y;

            trans.ScaleX = x;
            trans.ScaleY = y;

            e.Handled = true;
        }

        private void Border_Initialized(object sender, EventArgs e)
        {
            //ShowReport();
            PerfTest();
        }

        private void PerfTest()
        {
            string[] arr = new string[5000000];

            for (int i = 0; i < 5000000; i++)
            {
                arr[i] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            }

            var query = from s in arr
                        where s.Length > 20
                        select s.Substring(0, 100);

            StringBuilder sb = new StringBuilder();

            var list = query.ToList();

            foreach (var item in list)
            {
                sb.AppendLine(item);
            }

            MessageBox.Show(sb.ToString());
        }
    }

    /// <summary>
    /// Represents Data Collection Row Cells
    /// </summary>
    class DataCollectionItem
    {
        public string Header { get; set; }
        public string Function { get; set; }
        public string VehType { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// Represents Data Collection Row
    /// </summary>
    class DataCollectionMeasurement
    {
        public int Id { get; set; }
        public IEnumerable<DataCollectionItem> Cells { get; set; }
    }

    /// <summary>
    /// Represents aggregated data from the experiment folder .mes files
    /// </summary>
    class DataCollection
    {
        /// <summary>
        /// [Model name]#[Exp.Number] e.g. Olaine#3
        /// </summary>
        public string ExperimentName { get; set; }
        public IEnumerable<DataCollectionMeasurement> AggregatedData { get; set; }
    }
}
