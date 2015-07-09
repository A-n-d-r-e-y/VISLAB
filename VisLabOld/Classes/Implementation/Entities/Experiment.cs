using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace VisLab.Classes
{
    /// <summary>
    /// Experiment life cycle: first Experiment is created with the Project,
    /// each following Experiment is created with the destruction of the previous - after the simulation ends.
    /// Experiment is saved every time when closing the Experiment editing interface
    /// </summary>
    [Serializable]
    public class Experiment : INotifyPropertyChanged, IBindingEntity
    {
        #region metadata

        private static Dictionary<Guid, Experiment> dict = new Dictionary<Guid, Experiment>();

        private int number;
        public int Number
        {
            get { return number; }
            private set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        private DateTime date;
        public DateTime Date {
            get { return date; }
            private set { date = value; }
        }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            set
            {
                if (dict.Keys.Contains(value))
                {
                    var clone = dict[value];

                    this.Date = clone.date;
                    this.id = clone.id;
                    this.Number = clone.number;
                }
                else id = value;
            }
        }

        public bool HasBackup { get; set; }

        public bool HasSnapshot { get; set; }

        #endregion

        public Experiment(Guid id)
        {
            if (dict.Count == 0) this.number = 0;
            else this.number = dict.Values.Max(exp => exp.Number) + 1;
            
            this.date = DateTime.Now;
            this.id = id;
        }

        public static Dictionary<Guid, Experiment> GetDict()
        {
            return dict;
        }

        public static void Create(Guid id, string fileName)
        {
            Create(id, fileName, false, false);
        }

        public static void Create(Guid id, string fileName, bool hasBackup, bool hasSnapshot)
        {
            if (!File.Exists(fileName))
            {
                using (var fs = File.Create(fileName))
                {
                    var bf = new BinaryFormatter();
                    dict[id] = new Experiment(id)
                    {
                        HasBackup = hasBackup,
                        HasSnapshot = hasSnapshot
                    };

                    bf.Serialize(fs, dict);
                }
            }
            else
            {
                var bf = new BinaryFormatter();

                using (var fs = File.OpenRead(fileName))
                {
                    dict = (Dictionary<Guid, Experiment>)bf.Deserialize(fs);
                }

                dict[id] = new Experiment(id)
                {
                    HasBackup = hasBackup,
                    HasSnapshot = hasSnapshot
                };

                using (var fs = File.Open(fileName, FileMode.Truncate, FileAccess.Write))
                {
                    bf.Serialize(fs, dict);
                }
            }
        }

        public static Experiment Load(string fileName, Guid id)
        {
            //var dict = new Dictionary<Guid, Experiment>();

            using (var fs = File.OpenRead(fileName))
            {
                var bf = new BinaryFormatter();

                dict = (Dictionary<Guid, Experiment>)bf.Deserialize(fs);
            }

            return dict[id];
        }

        public static void Delete(string fileName, Guid id)
        {
            //var dict = new Dictionary<Guid, Experiment>();
            var bf = new BinaryFormatter();

            using (var fs = File.OpenRead(fileName))
            {
                dict = (Dictionary<Guid, Experiment>)bf.Deserialize(fs);
            }

            dict.Remove(id);

            using (var fs = File.Open(fileName, FileMode.Truncate, FileAccess.Write))
            {
                bf.Serialize(fs, dict);
            }
        }

        public static Experiment GetInstance(Guid id)
        {
            return dict[id];
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}