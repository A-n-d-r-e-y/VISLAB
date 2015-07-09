using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VisLab.Classes
{
    /// <summary>
    /// Experiment life cycle: first Experiment is created with the Project,
    /// each following Experiment is created with the destruction of the previous - after the simulation ends.
    /// Experiment is saved every time when closing the Experiment editing interface
    /// </summary>
    [Serializable]
    public class Experiment
    {
        #region Metadata

        private static Dictionary<Guid, Experiment> dict;

        private int number;
        public int Number
        {
            get { return number; }
        }

        private DateTime date;
        public DateTime Date { get { return date; } }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        private static readonly int startNumber;
        //public static int StartNumber { get { return startNumber; } }

        #endregion

        #region Data
        #endregion

        public Experiment(Guid id)
        {
            this.number = -1;
            this.date = DateTime.Now;
            this.id = id;
        }

        public void Reset()
        {
            // reset data
        }

        public static Dictionary<Guid, Experiment> GetDict()
        {
            return dict;
        }

        public void Save(string fileName)
        {
            if (!File.Exists(fileName))
            {
                using (var fs = File.Create(fileName))
                {
                    var bf = new BinaryFormatter();
                    if (this.number < 0) this.number = Experiment.startNumber;
                    dict = new Dictionary<Guid, Experiment>();
                    dict[id] = this;
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

                if (this.number < 0 || !dict.ContainsKey(id)) this.number = dict.Values.Max(exp => exp.number) + 1;

                dict[id] = this;

                using (var fs = File.Open(fileName, FileMode.Truncate, FileAccess.Write))
                {
                    bf.Serialize(fs, dict);
                }
            }

            this.date = DateTime.Now;
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
    }
}