using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Collections.ObjectModel;

namespace VisLab.Classes.Implementation.Entities
{
    public class Project
    {
        public class ModelsSection
        {
            [XmlElement]
            public ObservableCollection<Model> Model { get; set; }
        }

        public class ExpectedParametersSection
        {
            public class ParameterElement
            {
                [XmlAttribute]
                public string Name {get; set;}

                [XmlAttribute]
                public int No { get; set; }

                [XmlText]
                public string Value {get; set;}
            }

            [XmlElement]
            public ObservableCollection<ParameterElement> Parameter { get; set; }
        }

        private const string PROJECT_EXT = ".vislab";

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string CurrentModelName { get; set; }

        [XmlElement]
        public ModelsSection Models { get; set; }

        [XmlElement]
        public ExpectedParametersSection ExpectedParameters { get; set; }

        private Project() { }

        public Project(string projectName)
        {
            this.Name = projectName;

            ExpectedParameters = new ExpectedParametersSection()
            {
                Parameter = new ObservableCollection<ExpectedParametersSection.ParameterElement>()
            };

            Models = new ModelsSection()
            {
                Model = new ObservableCollection<Model>()
            };
        }

        public void Save(string directoryName)
        {
            var xs = new XmlSerializer(typeof(Project));

            using (var fs = File.Create(string.Format("{0}\\{1}{2}", directoryName, Name, PROJECT_EXT)))
            {
                xs.Serialize(fs, this);
            }
        }

        public static Project Load(string fileName)
        {
            var xs = new XmlSerializer(typeof(Project));

            using (var fs = File.OpenRead(fileName))
            {
                var pr = (Project)xs.Deserialize(fs);

                return pr;
            }
        }
    }
}
