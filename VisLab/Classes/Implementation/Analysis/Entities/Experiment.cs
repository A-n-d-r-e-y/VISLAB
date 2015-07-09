using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisLab.Classes.Implementation.Utilities;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;

namespace VisLab.Classes.Implementation.Entities
{
    public class Experiment
    {
        public class ExperimentData : INotifyPropertyChanged
        {
            public static readonly string EXPERIMENT_DATA_POSTFIX = ".experiment";

            [XmlIgnore]
            public TimeSpan Duration { get; set; }

            ///// <summary>
            ///// Period in simulation seconds
            ///// </summary>
            //[XmlIgnore]
            //public TimeSpan Period { get; set; }

            [XmlElement("Duration")]
            public long DurationTicks
            {
                get { return Duration.Ticks; }
                set { Duration = new TimeSpan(value); }
            }

            public DateTime CreatedOn { get; set; }
            public string Description { get; set; }
            public int NumberOfRuns { get; set; }

            //[XmlElement("Period")]
            //public double PeriodSeconds
            //{
            //    get { return Period.Seconds; }
            //    set { Period = new TimeSpan(0, 0, (int)value); }
            //}

            public bool IsCountersEvaluationEnabled { get; set; }

            public bool IsTravelTimesEvaluationEnabled { get; set; }

            [XmlIgnore]
            public bool CanAnalyze
            {
                get { return IsCountersEvaluationEnabled || IsTravelTimesEvaluationEnabled; }
            }

            private bool analyze;
            public bool Alanyze
            {
                get { return analyze; }
                set
                {
                    analyze = value;
                    OnPropertyChanged("Alanyze");
                }
            }

            public void Save(string fileName)
            {
                var xs = new XmlSerializer(typeof(ExperimentData));

                using (var fs = File.Create(fileName))
                {
                    xs.Serialize(fs, this);
                }
            }

            public static ExperimentData Load(string fileName)
            {
                var xs = new XmlSerializer(typeof(ExperimentData));

                using (var fs = File.OpenRead(fileName))
                {
                    var pr = (ExperimentData)xs.Deserialize(fs);

                    return pr;
                }
            }

            [field: NonSerialized]
            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(string property)
            {
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public List<Experiment> ChildNodes = new List<Experiment>();

        public int Number { get; private set; }

        public bool IsOpen { get; private set; }

        public bool IsLoaded { get; private set; }

        public string Path { get; private set; }

        public ExperimentData Data { get; private set; }

        public int ChildsCount
        {
            get { return ChildNodes.Count; }
        }

        public bool IsRoot
        {
            get { return Number == 0; }
        }

        public int DescendantsCount
        {
            get
            {
                return Directory.EnumerateDirectories(Path, "#*", SearchOption.AllDirectories).Count();
            }
        }

        private Experiment() { }

        /// <summary>
        /// Creating experiment tree from experiment directory :)
        /// </summary>
        /// <param name="rootPath">Absolute path to the root experiment folder</param>
        /// <param name="selectedPath">Relative (including root) path to the selected experiment</param>
        /// <param name="loadedPath">Relative (including root) path to the loaded experiment</param>
        public static Experiment Load(string rootPath, string selectedPath, string loadedPath)
        {
            var exp = new Experiment();

            exp.Path = rootPath;

            var expDataFileName = Directory.GetFiles(rootPath, "*" + ExperimentData.EXPERIMENT_DATA_POSTFIX).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(expDataFileName)) exp.Data = ExperimentData.Load(expDataFileName);

            exp.Number = SysAdmin.ExtractExperimentNumberFromPath(rootPath);

            string selectedRoot = SysAdmin.GetRootDirectory(selectedPath);
            exp.IsOpen = selectedRoot.EndsWith(string.Format(@"#{0}\", exp.Number));

            string loadedRoot = SysAdmin.GetRootDirectory(loadedPath);
            exp.IsLoaded = loadedRoot.EndsWith(string.Format(@"#{0}\", exp.Number));

            string s1 = (selectedPath.Contains(selectedRoot) && !string.IsNullOrWhiteSpace(selectedRoot)) ? selectedPath.Replace(selectedRoot, "") : string.Empty;
            string s2 = (loadedPath.Contains(loadedRoot) && !string.IsNullOrWhiteSpace(loadedRoot)) ? loadedPath.Replace(loadedRoot, "") : string.Empty;

            exp.BuildTree(rootPath, s1, s2);

            return exp;
        }

        private void BuildTree(string rootPath, string selectedPath, string loadedPath)
        {
            var subDirs = Directory.GetDirectories(rootPath, "#*");

            foreach (string subDir in subDirs)
            {
                int n = SysAdmin.ExtractExperimentNumberFromPath(subDir);
                var exp = Experiment.Load(subDir, selectedPath+'\\', loadedPath+'\\');
                this.ChildNodes.Add(exp);

                //exp.BuildTree(subDir);
            }
        }

        public void DeAnalyze(bool recursive)
        {
            this.Data.Alanyze = false;

            if (recursive)
                foreach (var child in ChildNodes)
                {
                    child.DeAnalyze(recursive);
                }
        }
    }
}
