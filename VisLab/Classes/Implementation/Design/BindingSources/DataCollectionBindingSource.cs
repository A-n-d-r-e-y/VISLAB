using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using VisLab.Classes.Integration.Entities;
using System.Data;

namespace VisLab.Classes.Implementation.Design
{
    class DataCollectionBindingSource
    {
        public int? MeasureId { get; set; }
        public Point Center { get; set; }
        public IEnumerable<Point> Points { get; set; }
        //public IEnumerable<dynamic> AggregatedData { get; set; }
        public DataTable AggregatedData { get; set; }
    }
}
