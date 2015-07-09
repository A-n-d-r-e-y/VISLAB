using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace VisLab.Classes
{
    [XmlRoot(Namespace = "http://www.morozov.lv/VissimPlugin/Classes/Project")]
    public class Project
    {
        public class ProjectFiles
        {
            public string ModelDirectory {get; set; }

            public string SnapshotDataFileName { get; set; }

            public string SnapshotTreeFileName { get; set; }

            public string ExperimentFileName { get; set; }
        }

        private const string ext = ".vislab";
        [XmlIgnore]
        public string FileExtension { get { return ext; } }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public Guid CurrentExperimentId { get; set; }

        /// <summary>
        /// XML section
        /// </summary>
        [XmlElement]
        public ProjectFiles Files = new ProjectFiles();

        public Project(string name)
        {
            Files.ModelDirectory = "Model";
            Files.SnapshotDataFileName = string.Format("{0}.snapshot.data", name);
            Files.SnapshotTreeFileName = string.Format("{0}.tree.data", name);
            Files.ExperimentFileName = string.Format("{0}.experiment.data", name);
            this.Name = name;
        }

        public Project() : this(Path.GetRandomFileName()) { }

        public void Save(string directoryName)
        {
            var xs = new XmlSerializer(typeof(Classes.Project));

            using (var fs = File.Create(string.Format("{0}\\{1}{2}", directoryName, Name, ext)))
            {
                xs.Serialize(fs, this);
            }
        }

        public static Project Load(string fileName)
        {
            var xs = new XmlSerializer(typeof(Classes.Project));

            using (var fs = File.OpenRead(fileName))
            {
                var pr = (Project)xs.Deserialize(fs);

                return pr;
            }
        }
    }
}
