using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vissim = VisLab.Classes.VissimSingleton;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace VisLab.Classes
{
    public class AvgSpeedReportItem
    {
        public int Link { get; set; }
        public int Lane { get; set; }
        public Point Center { get; set; }
        public double AvgSpeed { get; set; }
    }

    public class Analyst
    {
        public static List<AvgSpeedReportItem> GetReportData()
        {
            string query =
@"WITH RankedLink AS (
		SELECT ROW_NUMBER() OVER (ORDER BY Link, SegEndC) [Number], *
		FROM dbo.Olaine_LINK_EVAL),
	MiddleLink AS (
		SELECT AVG(Number) [MiddleLink]
		FROM RankedLink
		GROUP BY Link),
	AvgSpeed AS (
		SELECT AVG(v__0_) [speed], Link
		FROM dbo.Olaine_LINK_EVAL
		GROUP BY Link)

SELECT RankedLink.Link, 0 [Lane], (SegStX + SegEndX)/2 [center.x], (SegStY + SegEndY)/2 [center.y], AvgSpeed.speed
FROM RankedLink
	JOIN MiddleLink ON RankedLink.Number=MiddleLink.MiddleLink
	JOIN AvgSpeed ON RankedLink.Link=AvgSpeed.Link";

            var list = new List<AvgSpeedReportItem>();

            try
            {
                var sb = new OleDbConnectionStringBuilder(vissim.Instance.Evaluation.Wrap().GetConnectionString());
                if (sb.ContainsKey("Password"))
                {
                    using (var conn = new OleDbConnection(sb.ConnectionString))
                    {
                        conn.Open();
                        try
                        {
                            using (var reader = new OleDbCommand(query, conn).ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    list.Add(new AvgSpeedReportItem()
                                    {
                                        Link = reader.GetInt32(0),
                                        Lane = reader.GetInt32(1),
                                        Center = new Point(reader.GetDouble(2), reader.GetDouble(3)),
                                        AvgSpeed = reader.GetDouble(4)
                                    });
                                }
                            }
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            catch { }

            return list;
        }

        public static List<Line> GetSegments()
        {
            string query = @"select Link, Lane, SegStX, SegStY, SegEndX, SegEndY from dbo.Olaine_LINK_EVAL";

            var list = new List<Line>();

            var sb = new OleDbConnectionStringBuilder(vissim.Instance.Evaluation.Wrap().GetConnectionString());
            if (sb.ContainsKey("Password"))
            {
                using (var conn = new OleDbConnection(sb.ConnectionString))
                {
                    conn.Open();
                    using (var reader = new OleDbCommand(query, conn).ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Line()
                            {
                                X1 = reader.GetDouble(2),
                                Y1 = reader.GetDouble(3),
                                X2 = reader.GetDouble(4),
                                Y2 = reader.GetDouble(5),
                                Stroke = Brushes.Red
                            });
                        }
                    }
                }
            }

            return list;
        }

        public static List<Ellipse> GetPoints(int size)
        {
            string query = @"select Link, Lane, SegStX, SegStY, SegEndX, SegEndY from dbo.Olaine_LINK_EVAL";

            var list = new List<Ellipse>();

            var sb = new OleDbConnectionStringBuilder(vissim.Instance.Evaluation.Wrap().GetConnectionString());
            if (sb.ContainsKey("Password"))
            {
                using (var conn = new OleDbConnection(sb.ConnectionString))
                {
                    conn.Open();
                    using (var reader = new OleDbCommand(query, conn).ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Ellipse()
                            {
                                Height = size,
                                Width = size,
                                Fill = Brushes.Red,
                                Stroke = Brushes.Red,
                                Tag = new Point(reader.GetDouble(2), reader.GetDouble(3))
                            });
                        }
                    }
                }
            }

            return list;
        }
    }
}
