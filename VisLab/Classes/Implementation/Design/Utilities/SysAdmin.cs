using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace VisLab.Classes.Implementation.Utilities
{
    public class SysAdmin
    {
        private static string[] evalFileExtensions = new string[]
        {
            ".BEO",
            ".FZP",
            ".KNR",
            ".KNA",
            ".MER",
            ".MES",
            ".NPE",
            ".OVW",
            ".RSR",
            ".RSZ",
            ".SPW",
            ".STZ",
            ".STR",
            ".VLR",
            ".VLZ",
            ".FHZ",
            ".WGA",
            ".CVA",
            ".MLE",
            ".LZV",
            ".LDP",
            ".LSA",
            ".MERP",
            ".MESP",
            ".PP",
            ".RSRP",
            ".RSZP",
            ".PQE",
        };

        public static void CopyDirectory(string SourcePath, string DestinationPath)
        {
            if (!Directory.Exists(DestinationPath)) Directory.CreateDirectory(DestinationPath);
            if (Directory.GetFiles(DestinationPath).Count() > 0 || Directory.GetDirectories(DestinationPath).Count() > 0) SysAdmin.EmptyDir(DestinationPath);

            //Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath));

            string modelPath = SourcePath.Contains('#')
                ? DestinationPath.Contains('#') ? null : DestinationPath
                : SourcePath;

            if (!string.IsNullOrWhiteSpace(modelPath)) DeleteEvaluationFiles(modelPath);
        }

        public static void CopyDirectory(string SourcePath, string DestinationPath, string modelName)
        {
            if (!Directory.Exists(DestinationPath)) Directory.CreateDirectory(DestinationPath);
            //if (Directory.GetFiles(DestinationPath).Count() > 0 || Directory.GetDirectories(DestinationPath).Count() > 0) SysAdmin.EmptyDir(DestinationPath);

            //Create all of directories
            //foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            //    Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all files

            var query = from path in Directory.GetFiles(SourcePath, "*.*", SearchOption.TopDirectoryOnly)
                        let file = Path.GetFileName(path)
                        where file.ToUpper().StartsWith(modelName.ToUpper()) || file == "vissim.ini" || file.EndsWith(".sig")
                        select path;

            foreach (string newPath in query)
                File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath));

            string modelPath = SourcePath.Contains('#')
                ? (DestinationPath.Contains('#') ? null : DestinationPath)
                : (DestinationPath.Contains('#') ? SourcePath : null);

            if (!string.IsNullOrWhiteSpace(modelPath)) DeleteEvaluationFiles(modelPath);
        }

        public static int ExtractExperimentNumberFromPath(string path)
        {
            int
                n,
                result = -1;

            var regex = new Regex("^.*#(?<number>\\d+).*$", RegexOptions.IgnoreCase);
            var match = regex.Match(path);
            var group = match.Groups["number"];

            if (group != null && int.TryParse(group.Value, out n)) result = n;

            return result;
        }

        public static string ExtractQueryStringFromText(string text)
        {
            string result = string.Empty;

            var regex = new Regex("EVALUATION\\s*DATABASE\\s*\"(?<conn>.*Data Source.*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var match = regex.Match(text);
            var group = match.Groups["conn"];
            if (group != null) result = group.Value;

            return result;
        }

        public static string GetRootDirectory(string path)
        {
            string result = string.Empty;

            var regex = new Regex(@"^(?<root>[^\\]*\\).*$", RegexOptions.IgnoreCase);
            var match = regex.Match(path);
            var group = match.Groups["root"];

            if (group != null) result = group.Value;

            return result;
        }

        public static void EmptyDir(string dir)
        {
            foreach (var fileName in Directory.GetFiles(dir)) File.Delete(fileName);
            foreach (var dirName in Directory.GetDirectories(dir)) Directory.Delete(dirName, true);
        }

        public static int GetMaxDirNumber(string rootPath)
        {
            if (!Directory.Exists(rootPath)) return -1;
            var dirs =  Directory.EnumerateDirectories(rootPath, "#*", SearchOption.AllDirectories);

            if (dirs.Count() > 0) return dirs.Select(path => int.Parse(Path.GetFileName(path).Remove(0, 1))).Max();
            else return ExtractExperimentNumberFromPath(rootPath);
        }

        public static void ReplaceFileNamesInDirectory(string modelDir, string oldModelName, string newModelName)
        {
            var files = Directory.GetFiles(modelDir);

            foreach (var oldFileName in files)
            {
                string newFileName = Path.Combine(
                    modelDir,
                    Path.GetFileName(oldFileName).ToLower().Replace(oldModelName.ToLower(), newModelName.ToLower()));

                if (!newFileName.Equals(oldFileName, StringComparison.InvariantCultureIgnoreCase))
                {
                    File.Copy(oldFileName, newFileName);
                    File.Delete(oldFileName);
                }
            }
        }

        private static void DeleteEvaluationFiles(string modelDir)
        {
            var files = from f in Directory.GetFiles(modelDir, "*.*", SearchOption.TopDirectoryOnly)
                        where evalFileExtensions.Contains(Path.GetExtension(f).ToUpper())
                        select f;

            foreach (string f in files) File.Delete(f);
        }
    }
}
