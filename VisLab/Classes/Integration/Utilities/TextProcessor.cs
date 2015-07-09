using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using VisLab.Classes.Integration.Entities;
using System.Windows;
using System.Globalization;
using System.IO;

namespace VisLab.Classes.Integration.Utilities
{   
    [Flags]
    public enum VissimIniOptions : ulong
    {
        none = 0,
        opt1 = (1L << 0),
        opt2 = (1L << 1),
        opt3 = (1L << 2),
        opt4 = (1L << 3),
        opt5 = (1L << 4),
        opt6 = (1L << 5),
        opt7 = (1L << 6),
        opt8 = (1L << 7),
        opt9 = (1L << 8),
        opt10 = (1L << 9),
        opt11 = (1L << 10),
        opt12 = (1L << 11),
        opt13 = (1L << 12),
        opt14 = (1L << 13),
        opt15 = (1L << 14),
        opt16 = (1L << 15),
        opt17 = (1L << 16),
        opt18 = (1L << 17),
        opt19 = (1L << 18),
        opt20 = (1L << 19),
        opt21 = (1L << 20),
        opt22 = (1L << 21),
        opt23 = (1L << 22),
        opt24 = (1L << 23),
        opt25 = (1L << 24),
        opt26 = (1L << 25),
        opt27 = (1L << 26),
        opt28 = (1L << 27),
        opt29 = (1L << 28),
        opt30 = (1L << 29),
        opt31 = (1L << 30),
        opt32 = (1L << 31),
        opt33 = (1L << 32),
        opt34 = (1L << 33),
        opt35 = (1L << 34),
        opt36 = (1L << 35),
        DataCollectionEvaluation = (1L << 36),
        opt38 = (1L << 37),
        opt39 = (1L << 38),
        opt40 = (1L << 39),
        opt41 = (1L << 40),
        opt42 = (1L << 41),
        opt43 = (1L << 42),
        opt44 = (1L << 43),
        opt45 = (1L << 44),
        opt46 = (1L << 45),
        opt47 = (1L << 46),
        opt48 = (1L << 47),
        opt49 = (1L << 48),
        opt50 = (1L << 49),
        opt51 = (1L << 50),
        opt52 = (1L << 51),
        opt53 = (1L << 52),
        opt54 = (1L << 53),
        opt55 = (1L << 54),
        opt56 = (1L << 55),
        opt57 = (1L << 56),
        opt58 = (1L << 57),
        opt59 = (1L << 58),
        opt60 = (1L << 59),
        opt61 = (1L << 60),
        opt62 = (1L << 61),
        opt63 = (1L << 62),
    }

    class TextProcessor
    {
        public static VissimIniOptions GetVissimOptions(string text)
        {
            var regex = new Regex(@"^Options=(?<mask>(0|1)*).*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var match = regex.Match(text);

            if (match.Success)
            {
                string bynaryString = match.Success ? match.Groups["mask"].Value : "0";

                UInt64 number = Convert.ToUInt64(bynaryString, 2);

                return (VissimIniOptions)number;
            }

            return 0;
        }

        public static void SetVissimOptions(StringBuilder sb, VissimIniOptions vissimIniOptions)
        {
            var regex = new Regex(@"^Options=(?<mask>(0|1)*).*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var match = regex.Match(sb.ToString());

            if (match.Success)
            {
                ulong number = Convert.ToUInt64(match.Groups["mask"].Value, 2);

                number |= (ulong)vissimIniOptions;

                StringBuilder strNumber = new StringBuilder();
                foreach (byte b in BitConverter.GetBytes(number))
                {
                    strNumber.Insert(0, Convert.ToString(b, 2).PadLeft(8, '0'));
                }

                sb.Replace(match.Groups["mask"].Value, strNumber.ToString());
            }
        }

        public static IEnumerable<DataCollectionPoint> GetCollectionPoints(string text)
        {
            var regex = new Regex(@"\r\n(?<sub>COLLECTION_POINT(\s+?(?<number>(\d+))\s*?)(.|\n)*?POSITION LINK(?<link>(.|\n)*?)LANE(?<lane>(.|\n)*?)AT (?<pos>\d+(\.\d*)?))\s*?", RegexOptions.IgnoreCase);         
            //var regex = new Regex(@"\r\n(?<sub>COLLECTION_POINT(?<number>(.|\n)*?)NAME(?<name>(.|\n)*?)LABEL(.|\n)*?LINK(?<link>(.|\n)*?)LANE(?<lane>(.|\n)*?)AT (?<pos>\d+(\.\d*)?))\s*?", RegexOptions.IgnoreCase);

            var matches = regex.Matches(text);

            var result = from match in matches.Cast<Match>()
                      select new DataCollectionPoint
                      {
                          TextFromFile = GroupToString(match.Groups["sub"]),
                          Name = GroupToInt(match.Groups["number"]).ToString(), //GroupToString(match.Groups["name"]),
                          Link = GroupToInt(match.Groups["link"]),
                          Lane = GroupToInt(match.Groups["lane"]),
                          Position = GroupToDouble(match.Groups["pos"]),
                          Id = GroupToInt(match.Groups["number"]),
                      };

            return result;
        }

        public static IEnumerable<PointsListItem> GetPoints(string text)
        {
            // counter -> { points }
            var counters = GetCounters(text);

            var links = GetLinks(text);
            var connectors = GetConnectors(text, links);
            var allLinks = links.Union(connectors); 

            var points = GetCollectionPoints(text);

            // cross join
            var join = from p in points
                       from m in counters.Where(d => d.Value.Contains(p.Id.Value))
                       let link = allLinks.Where(l => l.Id == p.Link).FirstOrDefault()
                       orderby m.Key, p.Id
                       select new PointsListItem()
                       {
                           MeasurId = m.Key,
                           PointId = p.Id,
                           Name = p.Name,
                           Link = p.Link,
                           Diameter = link.Width,
                           Coord = Euclid.GetPolygonPoint(link.Points, p.Position.Value)
                       };

            return join;
        }

        public static IEnumerable<LinkItem> GetLinks(string text)
        {
            var regex = new Regex(@"\r\n(?<link>LINK(?<id>(.|\n)*?)NAME(?<name>(.|\n)*?)LABEL(.|\n)*?LANES(?<lanes_count>(.|\n)*?)LANE_WIDTH(?<lanes_width>(.|\n)*?)GRADIENT(.|\n)*?FROM(?<from>(.|\n)*?)TO\s*(?<to>-?\d+(\.\d*)?\s*-?\d+(\.\d*)?))", RegexOptions.IgnoreCase);
            var matches = regex.Matches(text);

            var result = from match in matches.Cast<Match>()
                         let from_ = GroupToString(match.Groups["from"])
                         let to = GroupToString(match.Groups["to"])
                         let textDelimitedWithOVER = from_ + " OVER " + to
                         let lanes_width = GroupToString(match.Groups["lanes_width"])
                         select new LinkItem()
                         {
                                Id = GroupToInt(match.Groups["id"]),
                                Name = GroupToString(match.Groups["name"]),
                                Points = (!string.IsNullOrEmpty(from_) && !string.IsNullOrEmpty(to)) ? GetPointsFromText(textDelimitedWithOVER) : null,
                                Width = CalculateWidth(lanes_width),
                         };

            return result;
        }

        public static IEnumerable<LinkItem> GetConnectors(string text, IEnumerable<LinkItem> links)
        {
            var regex = new Regex(@"\r\n(?<connector>CONNECTOR(?<id>(.|\n)*?)NAME(?<name>(.|\n)*?)LABEL(.|\n)*?FROM LINK(?<from_link>(.|\n)*?)LANES(.|\n)*?OVER(?<points>(.|\n)*?)TO LINK(?<to_link>(.|\n)*?)LANES)", RegexOptions.IgnoreCase);
            var matches = regex.Matches(text);

            var result = from match in matches.Cast<Match>()
                         let points = GroupToString( match.Groups["points"])
                         let from_linkId = GroupToInt(match.Groups["from_link"])
                         let to_linkId = GroupToInt(match.Groups["to_link"])
                         select new LinkItem()
                         {
                             Id = GroupToInt(match.Groups["id"]),
                             Name = GroupToString( match.Groups["name"]),
                             Points = GetPointsFromText(points),
                             Width = (from link in links
                                     where link.Id == from_linkId || link.Id == to_linkId
                                     select link.Width).Min()
                         };

            return result;
        }

        public static IEnumerable<DataCollectionMeasurement> GetCountersData(string dir)
        {
            var regex = new Regex(@"^.*;.*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return
                    from fileName in Directory.GetFiles(dir, "*.mes", SearchOption.TopDirectoryOnly)
                    let text = File.ReadAllText(fileName)
                    let matches = regex.Matches(text)
                    where matches.Count > 3
                    let headers = matches[0].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let functions = matches[1].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let vehTypes = matches[2].Value.Split(';').Select(str => str.Trim()).ToArray()
                    from match in matches.Cast<Match>().Where((value, index) => index > 2)
                    let matchArr = match.Value.Split(';').Select((str, index) => new { Value = str.Trim(), Index = index }).ToArray()
                    select new DataCollectionMeasurement()
                    {
                        Id = int.Parse(matchArr[0].Value, System.Globalization.NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US")),
                        From = int.Parse(matchArr[1].Value, System.Globalization.NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US")),
                        To = int.Parse(matchArr[2].Value, System.Globalization.NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US")),
                        Cells = (from value in matchArr
                                 select new DataCollectionItem()
                                 {
                                     Header = headers[value.Index],
                                     Function = functions[value.Index],
                                     VehType = vehTypes[value.Index],
                                     Value = value.Value
                                 })
                    } into raw
                    group raw by new { Id = raw.Id, From = raw.From, To = raw.To } into gr
                    select new DataCollectionMeasurement()
                    {
                        Id = gr.Key.Id,
                        From = gr.Key.From,
                        To = gr.Key.To,
                        Cells = (from x in gr
                                 from c in x.Cells
                                 group c by new { H = c.Header, F = c.Function, V = c.VehType } into cx
                                 select new DataCollectionItem()
                                 {
                                     Header = cx.Key.H,
                                     Function = cx.Key.F,
                                     VehType = cx.Key.V,
                                     Value = (from v in cx
                                              select double.Parse(v.Value.Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US")))
                                              .Average()
                                              .ToString("0.##", CultureInfo.GetCultureInfo("en-US"))
                                 })
                    };
        }

        public class ThreeStrings
        {
            public string H { get; set; }
            public string F { get; set; }
            public string V { get; set; }
        }

        public static IEnumerable<DataCollectionMeasurement> GetCountersData2(string dir)
        {
            var regex = new Regex(@"^.*;.*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return
                    from fileName in Directory.GetFiles(dir, "*.mes", SearchOption.TopDirectoryOnly)
                    let text = File.ReadAllText(fileName)
                    let matches = regex.Matches(text)
                    where matches.Count > 3
                    let headers = matches[0].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let functions = matches[1].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let vehTypes = matches[2].Value.Split(';').Select(str => str.Trim()).ToArray()
                    from match in matches.Cast<Match>().Where((value, index) => index > 2)
                    let matchArr = match.Value.Split(';').Select((str, index) => new { Value = str.Trim(), Index = index }).ToArray()
                    from cell in matchArr
                    where cell.Index > 2
                    select new
                    {
                        FileName = fileName,
                        Id = int.Parse(matchArr[0].Value, System.Globalization.NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US")),
                        Header = headers[cell.Index],
                        Function = functions[cell.Index],
                        VehType = vehTypes[cell.Index],
                        Value = cell.Value,
                    } into heap
                    group heap by new { FileName = heap.FileName, Id = heap.Id, Header = heap.Header, Function = heap.Function, VehType = heap.VehType } into gr0
                    let empty = string.IsNullOrWhiteSpace(gr0.Key.Function)
                    let min = !string.IsNullOrWhiteSpace(gr0.Key.Function) && gr0.Key.Function == "Minimum"
                    let max = !string.IsNullOrWhiteSpace(gr0.Key.Function) && gr0.Key.Function == "Maximum"
                    let avg = !string.IsNullOrWhiteSpace(gr0.Key.Function) && gr0.Key.Function == "Mean"
                    let sequence = (from g in gr0 select ParseDouble(g.Value))
                    let sequenceWithoutZero = sequence.Where(v => v != 0)
                    select new
                    {
                        FileName = gr0.Key.FileName,
                        Id = gr0.Key.Id,
                        Header = gr0.Key.Header,
                        Function = gr0.Key.Function,
                        VehType = gr0.Key.VehType,
                        Value = empty ? sequence.Sum().ToString("0.##") :
                        (min ? (sequenceWithoutZero.Count() > 0 ? sequenceWithoutZero.Min().ToString("0.##") : "0") :
                        (max ? sequence.Max().ToString("0.##") : (sequenceWithoutZero.Count() > 0 ? sequenceWithoutZero.Average().ToString("0.##") : "0"))),
                    } into raw
                    group raw by raw.Id into gr
                    select new DataCollectionMeasurement()
                    {
                        Id = gr.Key,
                        From = 0,
                        To = 0,
                        Cells = (from c in gr
                                 group c by new { H = c.Header, F = c.Function, V = c.VehType } into cx
                                 let n = cx.Count() - 1
                                 let average = (from v in cx
                                             select double.Parse(v.Value.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"))).Average()
                                 let stDev = n > 0 ? Math.Sqrt((from v in cx
                                                        let value = double.Parse(v.Value.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"))
                                                        select Math.Pow(value - average, 2)).Sum() / n) : 0
                                 select new DataCollectionItem()
                                 {
                                     Header = cx.Key.H,
                                     Function = cx.Key.F,
                                     VehType = cx.Key.V,
                                     Value = average.ToString("0.##"),
                                     StandardDeviation = stDev.ToString("0.##"),
                                     Confidence90 = n > 0 ? string.Format("±{0:0.##}", Euclid.TDistribution(0.1, n) * (stDev / Math.Sqrt(n))) : "±0",
                                     Confidence95 = n > 0 ? string.Format("±{0:0.##}", Euclid.TDistribution(0.05, n) * (stDev / Math.Sqrt(n))) : "±0",
                                     Confidence99 = n > 0 ? string.Format("±{0:0.##}", Euclid.TDistribution(0.01, n) * (stDev / Math.Sqrt(n))) : "±0",
                                     NumberOfRuns = (n + 1).ToString()
                                 })
                    };
        }

        //private double GetStandardDeviation()
        //{

        //}

        private static string Aggregate(string function, List<DataCollectionItem> cx)
        {
            var sum = (from v in cx
                       select double.Parse(v.Value.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US")))
                                                .Sum()
                                                .ToString("0.##");
            var avg = (from v in cx
                       select double.Parse(v.Value.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US")))
                                                .Average()
                                                .ToString("0.##");

            var max = (from v in cx
                       select double.Parse(v.Value.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US")))
                                              .Max()
                                              .ToString("0.##");

            var min = (from v in cx
                       select double.Parse(v.Value.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US")))
                                              .Min()
                                              .ToString("0.##");

            return string.IsNullOrWhiteSpace(function) ? sum :
                (function == "Maximum") ? max :
                (function == "Minimum") ? min : avg;
        }

        public static IEnumerable<SectionListItem> GetSections(string text)
        {
            var raw = GetTravelTimeSections(text);

            var links = GetLinks(text);
            var connectors = GetConnectors(text, links);
            var allLinks = links.Union(connectors);

            var sections = from r in raw
                       let fromLink = allLinks.Where(l => l.Id == r.FromLink).FirstOrDefault()
                       let toLink = allLinks.Where(l => l.Id == r.ToLink).FirstOrDefault()
                       select new SectionListItem()
                       {
                           No = r.No,
                           FromLink = r.FromLink,
                           ToLink = r.ToLink,
                           Name = r.Name,
                           FromCoord = Euclid.GetPolygonPoint(fromLink.Points, r.FromPos.Value),
                           ToCoord = Euclid.GetPolygonPoint(toLink.Points, r.ToPos.Value),
                       };

            return sections;
        }

        public static IEnumerable<TravelTimeGroup> GetTrTimeData(string dir)
        {
            var regex = new Regex(@"^.*;.*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            int number;

            return
                    from fileName in Directory.GetFiles(dir, "*.rsz", SearchOption.TopDirectoryOnly)
                    let text = File.ReadAllText(fileName)
                    let matches = regex.Matches(text)
                    where matches.Count > 3
                    let headers = matches[0].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let vehTypes = matches[1].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let numbers = matches[2].Value.Split(';').Select(str => str.Trim()).ToArray()
                    from match in matches.Cast<Match>().Where((value, index) => index > 2)
                    let matchArr = match.Value.Split(';').Select((str, index) => new { Value = str.Trim(), Index = index }).ToArray()
                    where int.TryParse(matchArr[0].Value, out number) // workaround - removing Name string
                    from value in matchArr
                    where value.Index > 0 && !string.IsNullOrWhiteSpace(numbers[value.Index])
                    select new SectionItem()
                    {
                        Time = int.Parse(matchArr[0].Value, System.Globalization.NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US")),
                        Header = headers[value.Index],
                        VehType = vehTypes[value.Index],
                        Number = numbers[value.Index],
                        Value = double.Parse(value.Value.Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"))
                    } into raw
                    group raw by new { Time = raw.Time, Header = raw.Header, VehType = raw.VehType, Number = raw.Number } into gr1
                    select new SectionItem()
                    {
                        Time = gr1.Key.Time,
                        Header = gr1.Key.Header,
                        VehType = gr1.Key.VehType,
                        Number = gr1.Key.Number,
                        Value = (from g in gr1 select g.Value).Average()
                    } into rawByFile
                    group rawByFile by new { No = rawByFile.Number, Time = rawByFile.Time } into gr2
                    select new TravelTimePair()
                    {
                        No = int.Parse(gr2.Key.No, System.Globalization.NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US")),
                        Time = gr2.Key.Time,
                        TravelTime = (from g in gr2 where g.Header == "Trav" select g.Value).FirstOrDefault(),
                        VehCount = (from g in gr2 where g.Header == "#Veh" select g.Value).FirstOrDefault(),
                        VehType = (from g in gr2 where g.Header == "Trav" select g.VehType).FirstOrDefault(),
                    } into pairs
                    group pairs by new { No = pairs.No, VehType = pairs.VehType } into gr3
                    select new TravelTimeGroup()
                    {
                        No = gr3.Key.No,
                        VehType = gr3.Key.VehType,
                        Column = from g in gr3
                                 select new TravelTimeColumn()
                                 {
                                     Time = g.Time,
                                     TravelTime = g.TravelTime.Value.ToString("0.##"),
                                     VehCount = g.VehCount.Value.ToString("0.##"),
                                 },
                    };
        }

        public static IEnumerable<TravelTimeGroup> GetTrTimeData2(string dir)
        {
            var regex = new Regex(@"^.*;.*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            int number;

            return
                    from fileName in Directory.GetFiles(dir, "*.rsz", SearchOption.TopDirectoryOnly)
                    let text = File.ReadAllText(fileName)
                    let matches = regex.Matches(text)
                    where matches.Count > 3
                    let headers = matches[0].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let vehTypes = matches[1].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let numbers = matches[2].Value.Split(';').Select(str => str.Trim()).ToArray()
                    from match in matches.Cast<Match>().Where((value, index) => index > 2)
                    let matchArr = match.Value.Split(';').Select((str, index) => new { Value = str.Trim(), Index = index }).ToArray()
                    where int.TryParse(matchArr[0].Value, out number) // workaround - removing Name string
                    from value in matchArr
                    where value.Index > 0 && !string.IsNullOrWhiteSpace(numbers[value.Index])
                    select new SectionItem()
                    {
                        FileName = fileName,
                        //Time = int.Parse(matchArr[0].Value, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture),
                        Header = headers[value.Index],
                        VehType = vehTypes[value.Index],
                        Number = numbers[value.Index],
                        Value = double.Parse(value.Value.Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"))
                    } into heap
                    group heap by new { FileName = heap.FileName, Header = heap.Header, VehType = heap.VehType, Number = heap.Number } into gr0
                    select new SectionItem()
                    {
                        //FileName = gr0.Key.FileName,
                        //Time = gr0.Key.Time,
                        Header = gr0.Key.Header,
                        VehType = gr0.Key.VehType,
                        Number = gr0.Key.Number,
                        Value = gr0.Key.Header == "Trav" ? (from g in gr0 select g.Value).Average() : (from g in gr0 select g.Value).Sum()
                    } into raw
                    group raw by new { Header = raw.Header, VehType = raw.VehType, Number = raw.Number } into gr1
                    select new SectionItem()
                    {
                        //FileName = gr1.Key.FileName,
                        //Time = gr1.Key.Time,
                        Header = gr1.Key.Header,
                        VehType = gr1.Key.VehType,
                        Number = gr1.Key.Number,
                        Value = (from g in gr1 select g.Value.Value).Average(),
                        Sum = (from g in gr1 select g.Value.Value).Sum(),
                        RunsCount = gr1.Count(),
                    } into rawByFile
                    group rawByFile by rawByFile.Number into gr2
                    select new TravelTimePair()
                    {
                        No = int.Parse(gr2.Key, System.Globalization.NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US")),
                        RunsCount = (from g in gr2 select g.RunsCount).FirstOrDefault(),
                        //Time = gr2.Key.Time,
                        TravelTime = (from g in gr2 where g.Header == "Trav" select g.Value).FirstOrDefault(),
                        VehCount = (from g in gr2 where g.Header == "#Veh" select g.Value).FirstOrDefault(),
                        TravelTimeSum = (from g in gr2 where g.Header == "Trav" select g.Sum).FirstOrDefault(),
                        VehCountSum = (from g in gr2 where g.Header == "#Veh" select g.Sum).FirstOrDefault(),
                        VehType = (from g in gr2 where g.Header == "Trav" select g.VehType).FirstOrDefault(),
                    } into pairs
                    group pairs by new { No = pairs.No, VehType = pairs.VehType } into gr3
                    select new TravelTimeGroup()
                    {
                        No = gr3.Key.No,
                        VehType = gr3.Key.VehType,
                        RunsCount = (from g in gr3 select g.RunsCount).FirstOrDefault(),
                        Column = from g in gr3
                                 select new TravelTimeColumn()
                                 {
                                     //Time = g.Time,
                                     TravelTime = g.TravelTime.Value.ToString("0.##"),
                                     VehCount = g.VehCount.Value.ToString("0.##"),
                                     TravelTimeSum = g.TravelTimeSum,
                                     VehCountSum = g.VehCountSum,
                                 },
                    };
        }

        #region private methods

        private static double ParseDouble(string value)
        {
            return double.Parse(value.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"));
        }

        private static IEnumerable<TravelTimeSection> GetTravelTimeSections(string text)
        {
            var regex = new Regex(@"\r\n(?<text>TRAVEL_TIME\s+?(?<no>\S+?)\s+?NAME\s+?""(?<name>.*?)""\s+?DISPLAY(.|\n)*?LINK\s+?(?<from_link>\S+?)\s+?AT\s+?(?<from_pos>\S+?)\s+?TO(.|\n)*?LINK\s+?(?<to_link>\S+?)\s+?AT\s+?(?<to_pos>\S+?)\s+?SMOOTHING)", RegexOptions.IgnoreCase);
            var matches = regex.Matches(text);

            var result = from match in matches.Cast<Match>()
                         select new TravelTimeSection()
                         {
                             No = GroupToInt(match.Groups["no"]),
                             Name = GroupToString(match.Groups["name"]),
                             FromLink = GroupToInt(match.Groups["from_link"]),
                             FromPos = GroupToDouble(match.Groups["from_pos"]),
                             ToLink = GroupToInt(match.Groups["to_link"]),
                             ToPos = GroupToDouble(match.Groups["to_pos"]),
                             TextFromFile = GroupToString(match.Groups["text"]),
                         };

            return result;
        }

        private static IEnumerable<Point> GetPointsFromText(string text)
        {
            string[] dirtyPoints = text.Split(new string[] { "OVER" }, StringSplitOptions.RemoveEmptyEntries);

            return dirtyPoints.Select(point =>
            {
                var coords = point.Trim().Split(' ');

                return new Point(
                            double.Parse(coords[0].Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US")),
                            double.Parse(coords[1].Replace(',', '.'), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US")));
            });
        }

        private static double? CalculateWidth(string lanes_width)
        {
            double dOut = double.NaN;
            double? sum = null;

            if (lanes_width.Contains(' '))
            {
                sum = lanes_width.Split(' ').Select(num =>
                {
                    return double.TryParse(num.Trim().Replace(',', '.'), NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out dOut) ? dOut : new Nullable<double>(dOut);
                }).Sum();
            }
            else
            {
                sum = double.TryParse(lanes_width.Trim().Replace(',', '.'), NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out dOut) ? dOut : new Nullable<double>(dOut);
            }

            return sum;
        }

        private static Dictionary<int, int[]> GetCounters(string text)
        {
            var regex = new Regex(@"^CROSS_SEC_MEASUREMENT(?<m_id>.*?)COLLECTION_POINT(?<p_id>.*?)$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var matches = regex.Matches(text);

            var result = (from match in matches.Cast<Match>()
                          select new
                          {
                              key = GroupToInt(match.Groups["m_id"]).Value,
                              value = (from str in GroupToString(match.Groups["p_id"]).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                      select int.Parse(str)).ToArray()
                          }).ToDictionary(k => k.key, v => v.value);

            return result;
        }

        private static double? GetDoubleValue(string header, string[] headers, string[] matchArr)
        {
            double d;
            int index = Array.IndexOf(headers, header);

            return index >= 0 && double.TryParse(matchArr[index].Replace(',', '.'), NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out d) ? d : new Nullable<double>();
        }

        private static int? GetIntValue(string header, string[] headers, string[] matchArr)
        {
            int
                n,
                index = Array.IndexOf(headers, header);

            return index >= 0 && int.TryParse(matchArr[index], NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out n) ? n : new Nullable<int>();
        }

        private static string GroupToString(Group gr)
        {
            return gr.Success ? gr.Value.Trim() : string.Empty;
        }

        private static int? GroupToInt(Group gr)
        {
            int iVal;
            return gr.Success && int.TryParse(gr.Value, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out iVal) ? new Nullable<int>(iVal) : null;
        }

        private static double? GroupToDouble(Group gr)
        {
            double dVal;
            return gr.Success && double.TryParse(gr.Value.Replace(',', '.'), NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out dVal) ? new Nullable<double>(dVal) : null;
        }

        #endregion
    }
}
