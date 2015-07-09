using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace VisLab.Classes.Integration.Entities
{
    public class SectionListItem
    {
        public int? No { get; set; }
        public string Name { get; set; }
        public int? FromLink { get; set; }
        public int? ToLink { get; set; }
        public Point FromCoord { get; set; }
        public Point ToCoord { get; set; }
    }
}
