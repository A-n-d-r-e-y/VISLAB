using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VISSIM_COMSERVERLib;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace TestApp
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
        opt37 = (1L << 36),
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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void fileSystemWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"D:\MOPO3OB\Desktop\Project2\Model\Olaine.INP";
            string text;

            using (var sr = new StreamReader(File.OpenRead(path)))
            {
                text = sr.ReadToEnd();
            }

            Regex regex = new Regex("EVALUATION\\s*DATABASE\\s*\"(?<conn>.*Data Source.*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var match = regex.Match(text);
            MessageBox.Show(match.Groups["conn"].Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        struct Setting { public string Name; public string Value; }
        struct Business { public string Name; public IEnumerable<Setting> Settings; }
        private void GroupingTest()
        {
            IEnumerable<Business> companies = new[]{
                new Business{ Name = "XYZ Inc.", Settings = new[]{ 
                    new Setting{ Name="Age", Value="27"},
                    new Setting{ Name="Industry", Value="IT"}
                }},
                new Business{ Name = "Programmers++", Settings = new[]{ 
                    new Setting{ Name="Age", Value="27"},
                    new Setting{ Name="Industry", Value="IT"}
                }},
                new Business{ Name = "Jeff's Luxury Salmon", Settings = new[]{ 
                    new Setting{ Name="Age", Value="63"},
                    new Setting{ Name="Industry", Value="Sports"}
                }},
                new Business{ Name = "Bank of Khazakstan", Settings = new[]{ 
                    new Setting{ Name="Age", Value="30"},
                    new Setting{ Name="Industry", Value="Finance"}
                }},
            };

            var groupSetting = "Industry";

            var grouped = companies
                .GroupBy(c => c.Settings.First(s => s.Name == groupSetting).Value);


            foreach (var group in grouped)
            {
                //Console.WriteLine(group.Key);
                foreach (var item in group.OrderBy(bus => bus.Name))
                    Console.WriteLine(" - " + item.Name);
            }
        }

        enum Gender { M, F }
        class Person { public string Name; public int Age; public Gender Gender; public IEnumerable<Book> Books; }
        private void SimpleTest()
        {
            IEnumerable<Person> family = new Person[]
            {
                new Person() { Age = 31, Gender = Gender.M, Name = "Andrey" },
                new Person() { Age = 6, Gender = Gender.M, Name = "Dima" },
                new Person() { Age = 30, Gender = Gender.F, Name = "Alena" },
                new Person() { Age = 67, Gender = Gender.F, Name = "Elena" },
                new Person() { Age = 70, Gender = Gender.M, Name = "Boris" }
            };

            var query = from person in family
                        group person by person.Gender
                            into gr
                            select new
                            {
                                Gender_ = gr.Key,
                                MaxAge = (from g in gr select g.Age).Average()
                            };

            foreach (var item in query)
            {
                MessageBox.Show(item.Gender_.ToString() + " - " + item.MaxAge.ToString());
            }
        }

        class Book { public string Title; public string Author; public double Price; }
        private void SimpleTest2()
        {
            IEnumerable<Person> family = new Person[]
            {
                new Person() { Age = 31, Gender = Gender.M, Name = "Andrey"
                    , Books = new Book[] {
                        new Book() { Title = "Alice", Author = "Caroll", Price = 9.99 },
                        new Book() { Title = "C#", Author = "Ben", Price = 19.99 } } },
                new Person() { Age = 6, Gender = Gender.M, Name = "Dima"
                    , Books = new Book[] {
                        new Book() { Title = "English", Author = "Teacher", Price = 7.00 } } },
                new Person() { Age = 30, Gender = Gender.F, Name = "Alena"
                    , Books = new Book[] {
                        new Book() { Title = "Sport", Author = "Arny", Price = 21.50 } } },
                new Person() { Age = 67, Gender = Gender.F, Name = "Elena" },
                new Person() { Age = 70, Gender = Gender.M, Name = "Boris" }
            };

            var query = from person in family
                        group person by person.Name
                            into gr
                            select new
                            {
                                Name = gr.Key,
                                AvgPrice = (from g in gr
                                            select g.Books == null ? 0 : g.Books.Average(b => b.Price)).Average()
                            };

            foreach (var item in query)
            {
                MessageBox.Show(item.Name.ToString() + " - " + item.AvgPrice.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string dir = @"D:\MOPO3OB\Desktop\Project3\Experiments\Rome_A_2D#0\#1\#2\Rome_A_2D.model";

            var regex = new Regex(@"^.*;.*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var result =
                    from fileName in Directory.GetFiles(dir, "*.rsz", SearchOption.TopDirectoryOnly)
                    let text = File.ReadAllText(fileName)
                    let matches = regex.Matches(text)
                    where matches.Count > 3
                    let headers = matches[0].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let vehTypes = matches[1].Value.Split(';').Select(str => str.Trim()).ToArray()
                    let numbers = matches[2].Value.Split(';').Select(str => str.Trim()).ToArray()
                    from match in matches.Cast<Match>().Where((value, index) => index > 2)
                    let matchArr = match.Value.Split(';').Select((str, index) => new { Value = str.Trim(), Index = index }).ToArray()
                    from value in matchArr
                    where value.Index > 0 && !string.IsNullOrWhiteSpace(numbers[value.Index])
                    select new SectionItem()
                    {
                        Time = int.Parse(matchArr[0].Value, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture),
                        Header = headers[value.Index],
                        VehType = vehTypes[value.Index],
                        Number = numbers[value.Index],
                        Value = double.Parse(value.Value, System.Globalization.NumberStyles.Number, CultureInfo.InvariantCulture)
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
                        No = int.Parse(gr2.Key.No, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture),
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
                                     TravelTime = g.TravelTime,
                                     VehCount = g.VehCount,
                                 },
                    };

            foreach (var res in result)
            {
                var arr = res.Column.ToArray();
                MessageBox.Show(res.No + ":" + res.VehType + "\n"
                    + ":1-" + arr[0].Time + ":1-" + arr[0].TravelTime + "\n"
                    + ":2-" + arr[1].Time + ":2-" + arr[1].TravelTime + "\n"
                    + ":3-" + arr[2].Time + ":3-" + arr[2].TravelTime + "\n"
                    + ":4-" + arr[3].Time + ":4-" + arr[3].TravelTime);
            }

            //var coll = GetCollectionPoints(text);
            //var x = coll.ToArray()[0];
            //MessageBox.Show(x.Id + ":" + x.Name + ":" + x.Position + ":" + x.Lane + ":" + x.Link);
        }

        public static IEnumerable<CollectionPointItem> GetCollectionPoints(string text)
        {
            var regex = new Regex(@"\r\n(?<sub>COLLECTION_POINT(\s+?(?<number>(\d+))\s*?)(.|\n)*?POSITION LINK(?<link>(.|\n)*?)LANE(?<lane>(.|\n)*?)AT (?<pos>\d+(\.\d*)?))\s*?", RegexOptions.IgnoreCase);
            
            var matches = regex.Matches(text);

            var result = from match in matches.Cast<Match>()
                         select new CollectionPointItem
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

        private static string GroupToString(Group gr)
        {
            return gr.Success ? gr.Value.Trim() : string.Empty;
        }

        private static int? GroupToInt(Group gr)
        {
            int iVal;
            return gr.Success && int.TryParse(gr.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out iVal) ? new Nullable<int>(iVal) : null;
        }

        private static double? GroupToDouble(Group gr)
        {
            double dVal;
            return gr.Success && double.TryParse(gr.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out dVal) ? new Nullable<double>(dVal) : null;
        }
    }

    public class CollectionPointItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Link { get; set; }
        public int? Lane { get; set; }
        public double? Position { get; set; }
        public string TextFromFile { get; set; }
    }

    class SectionItem
    {
        public int? Time { get; set; }
        public string Header { get; set; }
        public string VehType { get; set; }
        public string Number { get; set; }
        public double? Value { get; set; }
    }

    class TravelTimePair
    {
        public int? No { get; set; }
        public int? Time { get; set; }
        public double? TravelTime { get; set; }
        public double? VehCount { get; set; }
        public string VehType { get; set; }
    }

    class TravelTimeColumn
    {
        public int? Time { get; set; }
        public double? TravelTime { get; set; }
        public double? VehCount { get; set; }
    }

    class TravelTimeGroup
    {
        public int? No { get; set; }
        public string VehType { get; set; }
        public IEnumerable<TravelTimeColumn> Column { get; set; }
    }
}
