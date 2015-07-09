using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisLab.Classes.Integration.Entities
{
    //No.    1 (dff                 ): from link     7 at   16.1 m to link     6 at   68.8 m, Distance  104.3 m
    public class TravelTimeSection
    {
        public int? No { get; set; }
        public string Name { get; set; }
        public int? FromLink { get; set; }
        public double? FromPos { get; set; }
        public int? ToLink { get; set; }
        public double? ToPos { get; set; }
        public string TextFromFile { get; set; }
    }
}
