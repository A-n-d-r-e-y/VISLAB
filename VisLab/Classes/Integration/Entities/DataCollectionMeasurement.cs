using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisLab.Classes.Integration.Generics;
using System.ComponentModel;
using System.Data;

namespace VisLab.Classes.Integration.Entities
{
    /// <summary>
    /// Represents Data Collection Row Cells
    /// </summary>
    public class DataCollectionItem
    {
        public string Header { get; set; }
        public string Function { get; set; }
        public string VehType { get; set; }
        public string Value { get; set; }
        public string StandardDeviation { get; set; }
        public string Confidence90 { get; set; }
        public string Confidence95 { get; set; }
        public string Confidence99 { get; set; }
        public string NumberOfRuns { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public string ToFullString()
        {
            return Value + ":" + StandardDeviation + ":" + Confidence90;
        }
    }

    public class DataTableEx
    {
        public DataTable dt { get; set; }
        public IEnumerable<ItemDescriptor> Items { get; set; }
    }

    public class ItemDescriptor
    {
        public string ColName { get; set; }
        public string RowName { get; set; }
        public string CounterId { get; set; }
        public DataCollectionItem Value { get; set; }
    }

    /// <summary>
    /// Represents Data Collection Row
    /// </summary>
    class DataCollectionMeasurement
    {
        public string FileName { get; set; }
        public int Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public IEnumerable<DataCollectionItem> Cells { get; set; }
    }

    /// <summary>
    /// Represents aggregated data from the experiment folder .mes files
    /// </summary>
    class DataCollection
    {
        /// <summary>
        /// [Model name]#[Exp.Number] e.g. Olaine#3
        /// </summary>
        public string ExperimentName { get; set; }
        public IEnumerable<DataCollectionMeasurement> AggregatedData { get; set; }
    }
}
