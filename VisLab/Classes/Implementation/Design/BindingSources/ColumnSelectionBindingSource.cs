using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisLab.Classes.Implementation.Design.BindingSources
{
    public class ColumnSelectionBindingSource
    {
        public int Ordinal { get; set; }
        public string Header { get; set; }
        public bool IsVisible { get; set; }
    }
}
