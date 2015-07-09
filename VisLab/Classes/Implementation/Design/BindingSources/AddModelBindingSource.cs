using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VisLab.Classes.Implementation.Design
{
    public class AddModelBindingSource : INotifyPropertyChanged
    {
        private const string DEFAULT_MODEL_NAME = "<Model Name>";
        private const string DEFAULT_MODEL_FILE = "<Model File>";

        private string newModelName = DEFAULT_MODEL_NAME;
        public string NewModelName {
            get { return newModelName; }
            set
            {
                newModelName = value;
                OnPropertyChanged("NewModelName");
                OnPropertyChanged("IsValidated");
            }
        }

        private bool isNewModelNameActive = true;
        public bool IsNewModelNameActive
        {
            get { return isNewModelNameActive; }
            set
            {
                isNewModelNameActive = value;

                if (value == false)
                {
                    newModelName = DEFAULT_MODEL_NAME;
                    OnPropertyChanged("NewModelName");
                }

                OnPropertyChanged("IsNewModelNameActive");
                OnPropertyChanged("IsValidated");
            }
        }

        private string loadModelFromFile = DEFAULT_MODEL_FILE;
        public string LoadModelFromFile
        {
            get { return loadModelFromFile; }
            set
            {
                loadModelFromFile = value;
                OnPropertyChanged("LoadModelFromFile");
                OnPropertyChanged("IsValidated");
            }
        }

        private bool isLoadModelFileActive = false;
        public bool IsLoadModelFileActive
        {
            get { return isLoadModelFileActive; }
            set
            {
                isLoadModelFileActive = value;

                if (value == false)
                {
                    loadModelFromFile = DEFAULT_MODEL_FILE;
                    OnPropertyChanged("LoadModelFromFile");
                }

                OnPropertyChanged("IsLoadModelFileActive");
                OnPropertyChanged("IsValidated");
            }
        }

        private string altModelName = DEFAULT_MODEL_NAME;
        public string AltModelName
        {
            get { return altModelName; }
            set
            {
                altModelName = value;
                OnPropertyChanged("AltModelName");
                OnPropertyChanged("IsValidated");
            }
        }

        private bool isAltModelNameActive = false;
        public bool IsAltModelNameActive
        {
            get { return isAltModelNameActive; }
            set
            {
                isAltModelNameActive = value;

                if (value == false)
                {
                    altModelName = DEFAULT_MODEL_NAME;
                    OnPropertyChanged("AltModelName");
                }

                OnPropertyChanged("IsAltModelNameActive");
                OnPropertyChanged("IsValidated");
            }
        }

        public bool IsValidated
        {
            get { return Validate(); }
        }

        private bool Validate()
        {
            return (isNewModelNameActive && !(newModelName.StartsWith("<") || newModelName.EndsWith(">")) && !string.IsNullOrWhiteSpace(newModelName))
                || ((isLoadModelFileActive && !(loadModelFromFile.StartsWith("<") || loadModelFromFile.EndsWith(">")) && !string.IsNullOrWhiteSpace(loadModelFromFile))
                    && ((isAltModelNameActive && !(altModelName.StartsWith("<") || altModelName.EndsWith(">")) && !string.IsNullOrWhiteSpace(altModelName)) || !isAltModelNameActive));
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
