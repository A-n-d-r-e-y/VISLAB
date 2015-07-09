using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VISSIM_COMSERVERLib;
using VisLab.Classes;

namespace VisLab.Forms
{
    public partial class ModelForm : Form
    {
        private Net net;

        public ModelForm(Net net)
        {
            this.net = net;
            InitializeComponent();
            net.Wrap().Draw(modelCanvas1.cnvModel, System.Windows.SystemColors.WindowTextBrush);
        }
    }
}
