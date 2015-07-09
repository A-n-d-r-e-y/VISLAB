using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;
using VisLab.Controls;

namespace VisLab.Classes.Implementation.Design
{
    public class ShadowMaker : INotifyPropertyChanged
    {
        private readonly ShadingControl shadowControl;

        public ShadingControl Shadow { get; private set; }

        //private UserControl bufer;

        public event EventHandler<EventArgs> Up;

        private void OnShadowUp()
        {
            if (Up != null) Up(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Down;

        private void OnShadowDown()
        {
            if (Down != null) Down(this, EventArgs.Empty);
        }

        public ShadowMaker()
        {
            this.shadowControl = new ShadingControl();
        }

        public void ShadowUp()
        {
            Shadow = shadowControl;
            OnPropertyChanged("Shadow");

            OnShadowUp();
        }

        public void ShadowDown()
        {
            //if (bufer != null)
            //{
            //    shadowControl.Content = bufer;
            //    bufer = null;
            //}
            //else
            {
                Shadow = null;
                shadowControl.Content = null;
                OnPropertyChanged("Shadow");

                OnShadowDown();
            }
        }

        public void ShowThis(UserControl uc)
        {
            //if (shadowControl.Content != null) bufer = (UserControl)shadowControl.Content;
            shadowControl.Content = uc;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
