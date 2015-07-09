using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace VisLab.Classes.Integration.Entities
{
    //COLLECTION_POINT    1  NAME "collector" LABEL  0.00 0.00 POSITION LINK 1 LANE 2 AT 253.982 
    public class DataCollectionPoint
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Link { get; set; }
        public int? Lane { get; set; }
        public double? Position { get; set; }
        public string TextFromFile { get; set; }
    }
}
