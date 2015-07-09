using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisLab.Classes.Integration.Generics
{
    public enum DataCollectionFunction { Minimum, Maximum, Mean }

    public class DataCollectionEntry<T>
    {
        public T Parameter { get; set; }
        public DataCollectionFunction Function { get; set; }
        public string VehicleClass { get; set; }
    }
}
