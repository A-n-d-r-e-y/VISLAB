using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisLab.Classes.Integration.Entities
{
    class SectionItem
    {
        public string FileName { get; set; }
        public int? Time { get; set; }
        public string Header { get; set; }
        public string VehType { get; set; }
        public string Number { get; set; }
        public double? Value { get; set; }
        public double? Sum { get; set; }
        public int? RunsCount { get; set; }
    }

    class TravelTimePair
    {
        public int? No { get; set; }
        public int? Time { get; set; }
        public double? TravelTime { get; set; }
        public double? VehCount { get; set; }
        public double? TravelTimeSum { get; set; }
        public double? VehCountSum { get; set; }
        public string VehType { get; set; }
        public int? RunsCount { get; set; }
    }

    class TravelTimeColumn
    {
        public int? Time { get; set; }
        public string TravelTime { get; set; }
        public string VehCount { get; set; }
        public double? TravelTimeSum { get; set; }
        public double? VehCountSum { get; set; }
    }

    class TravelTimeGroup
    {
        public int? No { get; set; }
        public string VehType { get; set; }
        public int? RunsCount { get; set; }
        public IEnumerable<TravelTimeColumn> Column { get; set; }
    }
}
