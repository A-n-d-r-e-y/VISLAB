using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.IO;
using System.Data;
using System.Threading;
using System.Windows.Threading;

using VisLab.Classes.Implementation.Analysis.Boundaries.Controls;
using VisLab.Classes.Implementation.Design.Generics;
using VisLab.Classes.Integration.Utilities;
using VisLab.Classes.Implementation.Entities;
using VisLab.Classes.Implementation.Design;
using VisLab.Classes.Implementation.Design.Utilities;
using VisLab.Controls;
using VisLab.Classes.Integration.Entities;
using VisLab.Windows;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;

namespace VisLab.Classes.Implementation.Analysis.Controllers
{
    public class Analyst
    {
        public ReportBindingSource Host { get; private set; }

        ProjectManager clerk;
        ShadowMaker shadowMaker;
        AsyncExceptionHandler aex;

        #region Cache
        //private Dictionary<string, IEnumerable<LinkItem>> linksCache = new Dictionary<string, IEnumerable<LinkItem>>();
        //private Dictionary<string, List<PointsListItem>> pointsCache = new Dictionary<string, List<PointsListItem>>();
        //private Dictionary<string, IEnumerable<DataCollectionMeasurement>> countersCache = new Dictionary<string, IEnumerable<DataCollectionMeasurement>>();
        private Dictionary<string, ModelControl> experimentsCache = new Dictionary<string, ModelControl>();
        #endregion

        public Analyst(ProjectManager clerk, ShadowMaker shadowMaker, AsyncExceptionHandler exceptionHandler)
        {
            this.aex = exceptionHandler;

            this.shadowMaker = shadowMaker;

            this.clerk = clerk;
            this.Host = new ReportBindingSource(shadowMaker, exceptionHandler, clerk);

            //this.Host.DrawModel += new EventHandler<EventArgs<UserControl, bool>>(Host_DrawModel);
            this.Host.DrawPoints += new EventHandler<EventArgs<UserControl>>(Host_DrawPoints);
            this.Host.DrawCounters += new EventHandler<EventArgs<UserControl>>(Host_DrawCounters);
            this.Host.GenerateCountersReport += new EventHandler<EventArgs<DataTableEx, Dispatcher>>(Host_GenerateCountersReport);
            this.Host.GenerateTrTimesReport += new EventHandler<EventArgs<DataTableEx, Dispatcher>>(Host_GenerateTrTimesReport);

            this.Host.DrawSections += new EventHandler<EventArgs<UserControl>>(Host_DrawSections);
            this.Host.DrawTrTimes += new EventHandler<EventArgs<UserControl>>(Host_DrawTrTimes);
        }

        #region host events

        private void Host_DrawCounters(object sender, EventArgs<UserControl> e)
        {
            var control = e.Value as ModelControl;
            drCounters(control);
        }

        private void Host_DrawPoints(object sender, EventArgs<UserControl> e)
        {
            var control = e.Value as ModelControl;
            drPoints(control);
        }

        private class TwoStrings
        {
            public string ModelName { get; set; }
            public int ExperimentNumber { get; set; }
        }

        private void Host_GenerateCountersReport(object sender, EventArgs<DataTableEx, Dispatcher> e)
        {
            var list = new List<TwoStrings>();

            var dt = e.Value.dt = new DataTable();

            dt.Columns.Add(new DataColumn("Experiment"));
            dt.Columns.Add(new DataColumn("Measur\x2024"));

            e.Param.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                list = (from m in Host.List
                        select new TwoStrings()
                        {
                            ModelName = m.ModelName,
                            ExperimentNumber = m.ExperimentNumber,
                        }).ToList();
            }));

            var dirs = from m in list
                       select new
                       {
                           ExpName = string.Format("{0}#{1}", m.ModelName, m.ExperimentNumber),
                           Path = clerk.GetExperimentModelDirName(m.ModelName, m.ExperimentNumber)
                       };

            var experiments = from d in dirs
                              select new DataCollection()
                              {
                                  ExperimentName = d.ExpName,
                                  AggregatedData = TextProcessor.GetCountersData2(d.Path)
                              };

            // columns
            var parameters = from exp in experiments
                             from data in exp.AggregatedData
                             from cell in data.Cells
                             group cell by new { F = cell.Function, H = cell.Header, V = cell.VehType } into gr
                             select BuildHeader(new DataCollectionItem()
                             {
                                 Function = gr.Key.F,
                                 Header = gr.Key.H,
                                 VehType = gr.Key.V,
                             }); ;

            // creating columns
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
                          ExpName = exp.ExperimentName,
                      };

            foreach (var item in xxx)
            {
                var row = dt.NewRow();
                row[0] = item.ExpName;
                row[1] = item.CounterId;
                aex.Protect(() => dt.Rows.Add(row));
            }

            e.Value.Items = from exp in experiments
                         from data in exp.AggregatedData
                         from cell in data.Cells
                         select new ItemDescriptor()
                         {
                             CounterId = data.Id.ToString(),
                             ColName = BuildHeader(cell),
                             RowName = exp.ExperimentName,
                             Value = cell,
                         };

            foreach (var item in e.Value.Items)
            {
                var row = dt.Select(string.Format("[Experiment] = '{0}' AND [Measur\x2024] = '{1}'"
                    , item.RowName
                    , item.CounterId)).FirstOrDefault();

                row.SetField<DataCollectionItem>(item.ColName, item.Value);
            }
        }

        private void Host_GenerateTrTimesReport(object sender, EventArgs<DataTableEx, Dispatcher> e)
        {
            var list = new List<TwoStrings>();

            var dt = e.Value.dt = new DataTable();

            e.Param.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                list = (from m in Host.List
                        select new TwoStrings()
                        {
                            ModelName = m.ModelName,
                            ExperimentNumber = m.ExperimentNumber,
                        }).ToList();
            }));

            var dirs = from m in list
                       select new
                       {
                           ExpName = string.Format("{0}#{1}", m.ModelName, m.ExperimentNumber),
                           Path = clerk.GetExperimentModelDirName(m.ModelName, m.ExperimentNumber)
                       };

            var experiments = from d in dirs
                              let raw = TextProcessor.GetTrTimeData2(d.Path)
                              from r in raw
                              from c in r.Column                             
                              select new
                              {
                                  ExperimentName = d.ExpName,
                                  No = r.No,
                                  VehType = r.VehType,
                                  TravelTime = c.TravelTime,
                                  VehCount = c.VehCount,
                                  TravelTimeSum = c.TravelTimeSum,
                                  VehCountSum = c.VehCountSum,
                                  NumberOfRuns = r.RunsCount,
                              };

            // Travel Time (___) columns
            var trTimeColumns = (from exp in experiments
                                select exp.VehType).Distinct();

            // creating columns
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Experiment"),
                new DataColumn("Measur\x2024"),
                new DataColumn("Nr\x2024_of_Vehicles"),
            });

            foreach (string colName in trTimeColumns) dt.Columns.Add(new DataColumn(string.Format("Travel_Time_({0})", colName)));

            var items = new List<ItemDescriptor>(experiments.Count() * 2);

            // creating rows
            foreach (var exp in experiments)
            {
                var dr = dt.NewRow();
                dr["Experiment"] = exp.ExperimentName;
                dr["Measur\x2024"] = exp.No;
                dr[string.Format("Travel_Time_({0})", exp.VehType)] = exp.TravelTime;
                dr["Nr\x2024_of_Vehicles"] = exp.VehCount;

                dt.Rows.Add(dr);


                int n = exp.NumberOfRuns.Value - 1;
                double trTimeAverage = double.Parse(exp.TravelTime);
                double trTimeStDev = n > 0 ? Math.Sqrt(exp.TravelTimeSum.Value / n) : 0;

                double vehCountAverage = double.Parse(exp.VehCount);
                double vehCountStDev = n > 0 ? Math.Sqrt(exp.VehCountSum.Value / n) : 0;

                items.AddRange(new ItemDescriptor[]
                {
                    new ItemDescriptor()
                    {
                        CounterId = exp.No.ToString(),
                        ColName = string.Format("Travel_Time_({0})", exp.VehType),
                        RowName = exp.ExperimentName,
                        Value = new DataCollectionItem()
                        {
                            Value = exp.TravelTime,
                            StandardDeviation = trTimeStDev.ToString("0.##"),
                            Confidence90 = (n > 0 ? Euclid.TDistribution(0.1, n) * (trTimeStDev / Math.Sqrt(n)) : 0).ToString("0.##"),
                            Confidence95 = (n > 0 ? Euclid.TDistribution(0.05, n) * (trTimeStDev / Math.Sqrt(n)) : 0).ToString("0.##"),
                            Confidence99 = (n > 0 ? Euclid.TDistribution(0.01, n) * (trTimeStDev / Math.Sqrt(n)) : 0).ToString("0.##"),
                            NumberOfRuns = exp.NumberOfRuns.ToString(),
                        }
                    },
                    new ItemDescriptor()
                    {
                        CounterId = exp.No.ToString(),
                        ColName = "Nr\x2024_of_Vehicles",
                        RowName = exp.ExperimentName,
                        Value = new DataCollectionItem()
                        {
                            Value = exp.VehCount,
                            StandardDeviation = vehCountStDev.ToString("0.##"),
                            Confidence90 = string.Format("±{0:0.##}", (n > 0 ? Euclid.TDistribution(0.1, n) * (vehCountStDev / Math.Sqrt(n)) : 0)),
                            Confidence95 = string.Format("±{0:0.##}", (n > 0 ? Euclid.TDistribution(0.05, n) * (vehCountStDev / Math.Sqrt(n)) : 0)),
                            Confidence99 = string.Format("±{0:0.##}", (n > 0 ? Euclid.TDistribution(0.01, n) * (vehCountStDev / Math.Sqrt(n)) : 0)),
                            NumberOfRuns = exp.NumberOfRuns.ToString(),
                        }
                    },
                });

                e.Value.Items = items;
            }
        }

        void Host_DrawTrTimes(object sender, EventArgs<UserControl> e)
        {
            var control = e.Value as ModelControl;
            drTrTimes(control);
        }

        void Host_DrawSections(object sender, EventArgs<UserControl> e)
        {
            var control = e.Value as ModelControl;
            drSections(control);
        }

        #endregion

        private static DataTable CreateTableFromMeasurement(string masterModelDir, int? id)
        {
            var rows = TextProcessor.GetCountersData(masterModelDir);

            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("Parameter"));

            var distinctTo = from r in rows
                             where r.Id == id.Value
                             from c in r.Cells
                             where c.Header == "to"
                             group c by c.Value into gr
                             orderby int.Parse(gr.Key)
                             select gr.Key;

            foreach (var to in distinctTo)
            {
                var col = new DataColumn(to);
                dt.Columns.Add(col);
            }

            var distinctHeaders = from r in rows
                                  where r.Id == id.Value
                                  from c in r.Cells
                                  group c by BuildHeader(c) into gr
                                  where gr.Key != "from" && gr.Key != "to" && gr.Key != "Measur\x2024"
                                  orderby gr.Key
                                  select gr.Key;

            foreach (var item in distinctHeaders)
            {
                var row = dt.NewRow();
                row[0] = item;
                dt.Rows.Add(row);
            }

            var data = from r in rows
                       where r.Id == id.Value
                       from c in r.Cells
                       where c.Header != "from" && c.Header != "to" && c.Header != "Measur."
                       orderby r.To
                       select new
                       {
                           Parameter = BuildHeader(c),
                           To = r.To,
                           Value = c.Value,
                       };


            foreach (var item in data)
            {
                var row = dt.Select(string.Format("[Parameter] = '{0}'", item.Parameter)).FirstOrDefault();
                row[item.To.ToString()] = item.Value;

            }

            return dt;
        }

        private static DataSet CreateDataSetFromGroups(string masterModelDir)
        {
            var tables = TextProcessor.GetTrTimeData(masterModelDir);

            var ds = new DataSet();

            foreach (var table in tables)
            {
                var dt = new DataTable(table.No.Value.ToString());
                dt.Columns.Add(new DataColumn("Parameter"));

                foreach (var data in table.Column)
                    dt.Columns.Add(new DataColumn(data.Time.Value.ToString(), typeof(double)));

                DataRow row1 = dt.NewRow();
                row1["Parameter"] = string.Format("Travel Time ({0})", table.VehType);

                DataRow row2 = dt.NewRow();
                row2["Parameter"] = "Nr. of Vehicles";

                foreach (var data in table.Column)
                {
                    row1[data.Time.Value.ToString()] = data.TravelTime;
                    row2[data.Time.Value.ToString()] = data.VehCount;
                }

                dt.Rows.Add(row1);
                dt.Rows.Add(row2);

                ds.Tables.Add(dt);
            }

            return ds;
        }
        
        private static string BuildHeader(DataCollectionItem item)
        {
            string header = item.Header;
            string function = item.Function;
            string vehType = item.VehType;

            return string.Format(string.IsNullOrWhiteSpace(function)
               ? string.IsNullOrWhiteSpace(vehType) ? header : string.Format("{0}-{1}", header, vehType)
               : string.Format("{0}({1}){2}", header, function, vehType)).Replace(" ", "_").Replace(".", "\x2024");
        }

        private ModelControl CreateModelControl(string modelName, Experiment exp, bool notMaster)
        {
            var ctrl = new ModelControl(modelName)
            {
                DataContext = exp,
                //TODO [redundancy] exp.Number
                ExperimentNumber = exp.Number,
                IsMaster = !notMaster,
            };

            if (notMaster) ctrl.OptimizeForList();

            ctrl.ModelDoubleClick += (sender_, e_) => Host.MoveAllModels((e_ as RoutedEventArgs<Point>).Value);

            return ctrl;
        }

        public void AnalyzeExperiment(string modelName, Experiment exp)
        {
            if (Host.List.Count(ctrl => ctrl.ModelName == modelName && ctrl.ExperimentNumber == exp.Number) == 0)
            {
                string key = modelName + exp.Number.ToString();

                if (!experimentsCache.ContainsKey(key))
                {
                    var ctrl = CreateModelControl(modelName, exp, true);
                    var master = CreateModelControl(modelName, exp, false);

                    ctrl.Clone = master;
                    master.Clone = ctrl;

                    experimentsCache[key] = ctrl;
                }

                //TODO [redundancy] replace isShadowUp with shadowMaker.isShadowUp
                bool isShadowUp = false;
                if (shadowMaker.Shadow == null)
                {
                    shadowMaker.ShowThis(new LoadingControl("Loading..."));
                    shadowMaker.ShadowUp();
                    isShadowUp = true;
                }

                var control = experimentsCache[key];

                ThreadPool.QueueUserWorkItem(o =>
                {
                    aex.Protect(() =>
                        {
                            // resource-intensive
                            if (!control.HasNetwork) drNetwork(control);

                            if (Host.AreCountersGlobalVisible)
                            {
                                if (!control.HasPoints) drPoints(control);
                                if (!control.HasCounters) drCounters(control);
                            }

                            if (Host.AreTrTimesGlobalVisible)
                            {
                                if (!control.HasSections) drSections(control);
                                if (!control.HasTravelTimes) drTrTimes(control);
                            }

                            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
                            {
                                if (Host.AreCountersGlobalVisible)
                                {
                                    if (!control.IsMaster) control.Clone.OpenCountersGrid();
                                    else control.OpenCountersGrid();
                                }

                                if (Host.AreTrTimesGlobalVisible)
                                {
                                    if (!control.IsMaster) control.Clone.OpenTrTimesGrid();
                                    else control.OpenTrTimesGrid();
                                }

                                Host.List.Add(control);
                                if (isShadowUp) shadowMaker.ShadowDown();
                            }));
                        });
                });
            }
        }

        public void RemoveExperiment(string modelName, int experimentNumber)
        {
            var expToRemove = Host.List.Where(control =>
                {
                    return control.ModelName == modelName && control.ExperimentNumber == experimentNumber;
                }).FirstOrDefault();

            if (expToRemove != null)
                Host.List.Remove(expToRemove); // -> listbox_CollectionChanged -> listbox.SelectionChanged -> CopyModelToMaster
        }

        #region private methods

        private static Point FindCenter(IEnumerable<Point> points)
        {
            var queryX = (from p in points select p.X);
            var queryY = (from p in points select p.Y);

            var pn = new Point(
                new[] { queryX.Min(), queryX.Max() }.Average(),
                new[] { queryY.Min(), queryY.Max() }.Average());

            return pn;
        }

        private void drNetwork(ModelControl control)
        {
            string modelName = "";
            int expNumber = -1;

            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                modelName = control.ModelName;
                expNumber = control.ExperimentNumber;
            }));

            string inputFileName = clerk.GetExperimentInputFileName(modelName, expNumber);
            string text = File.ReadAllText(inputFileName);

            var links = TextProcessor.GetLinks(text);
            var connectors = TextProcessor.GetConnectors(text, links.ToArray());

            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                ModelPlotter.DrawLinks(control, links.Union(connectors));
            }));
        }

        private void drPoints(ModelControl control)
        {
            string modelName = "";
            int expNumber = -1;

            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                modelName = control.ModelName;
                expNumber = control.ExperimentNumber;
            }));

            string inputFileName = clerk.GetExperimentInputFileName(modelName, expNumber);

            string text = File.ReadAllText(inputFileName);

            var points = TextProcessor.GetPoints(text); //.ToList();

            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                //var pts = points;
                ModelPlotter.DrawPoints(control, points);
            }));
        }

        private void drCounters(ModelControl control)
        {
            string modelName = "";
            int expNumber = -1;
            IEnumerable<PointsListItem> points = null;

            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                modelName = control.ModelName;
                expNumber = control.ExperimentNumber;
                points = control.Clone.CollectionPoints;
            }));

            string masterModelDir = clerk.GetExperimentModelDirName(modelName, expNumber);

            var bindings = from p in points
                           group p by p.MeasurId
                               into gr
                               let coords = (from p in gr select p.Coord)
                               select new DataCollectionBindingSource
                               {
                                   MeasureId = gr.Key,
                                   Center = FindCenter(coords),
                                   Points = coords,
                                   AggregatedData = CreateTableFromMeasurement(masterModelDir, gr.Key),
                               };

            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                ModelPlotter.DrawCounters(control, bindings);
            }));
        }

        private void drSections(ModelControl control)
        {
            string modelName = "";
            int expNumber = -1;

            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                modelName = control.ModelName;
                expNumber = control.ExperimentNumber;
            }));

            string inputFileName = clerk.GetExperimentInputFileName(modelName, expNumber);

            string text = File.ReadAllText(inputFileName);

            var sections = TextProcessor.GetSections(text); //.ToList();

            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                ModelPlotter.DrawSections(control, sections);
            }));
        }

        private void drTrTimes(ModelControl control)
        {
            string modelName = "";
            int expNumber = -1;
            IEnumerable<SectionListItem> sections = null;

            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                modelName = control.ModelName;
                expNumber = control.ExperimentNumber;
                sections = control.Clone.TrTimeSections;
            }));

            string masterModelDir = clerk.GetExperimentModelDirName(modelName, expNumber);
            var ds = CreateDataSetFromGroups(masterModelDir);

            var bindings = from s in sections
                           group s by s.No
                               into gr
                               let coords = (from g in gr select new Point[] { g.FromCoord, g.ToCoord }).FirstOrDefault()
                               select new DataCollectionBindingSource
                               {
                                   MeasureId = gr.Key,
                                   Center = FindCenter(coords),
                                   Points = coords,
                                   AggregatedData = ds.Tables[gr.Key.Value.ToString()],
                               };

            control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
            {
                ModelPlotter.DrawTrTimes(control, bindings);
            }));
        }

        #endregion
    }
}
