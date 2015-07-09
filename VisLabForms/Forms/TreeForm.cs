using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisLab.WPF;
using VisLab.Classes;

namespace VisLab.Forms
{
    public partial class TreeForm : Form
    {
        private TreeCanvas treeCanvas1;

        public TreeForm(ProjectManager pm)
        {
            InitializeComponent();

            treeCanvas1 = new TreeCanvas(pm);
            elementHost1.Child = treeCanvas1;
        }
    }
}
