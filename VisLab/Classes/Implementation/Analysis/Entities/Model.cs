using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace VisLab.Classes.Implementation.Entities
{
    public class Model : INotifyPropertyChanged
    {
        public static readonly string MODEL_DIR_POSTFIX = ".model";
        public static readonly string EXPERIMENTS_DIR = "Experiments";

        [XmlAttribute]
        public string Name { get; set; }

        private string selectedExperimentPath;
        [XmlElement]
        public string SelectedExperimentPath
        {
            get { return selectedExperimentPath; }
            set
            {
                selectedExperimentPath = value;
                OnPropertyChanged("SelectedExperimentPath");
            }
        }


        private string loadedExperimentPath;
        [XmlElement]
        public string LoadedExperimentPath
        {
            get { return loadedExperimentPath; }
            set
            {
                loadedExperimentPath = value;
                OnPropertyChanged("LoadedExperimentPath");
            }
        }

        public Model() { }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
