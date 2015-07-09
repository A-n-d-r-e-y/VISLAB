using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Threading;

namespace VisLab.Classes
{
    public static class DirectoryPacker
    {
        [Serializable]
        public struct SnapshotDirectory
        {
            [Serializable]
            public struct SnapshotFile
            {
                public string filename;
                public byte[] file;

                public SnapshotFile(string filename)
                {
                    this.filename = Path.GetFileName(filename);
                    using (var fs = File.OpenRead(filename))
                    {
                        file = new byte[fs.Length];
                        fs.Read(file, 0, file.Length);
                    }
                }
            }

            public Guid id;
            public string dirname;
            public List<SnapshotFile> dir;

            public SnapshotDirectory(Guid id, string dirname)
            {
                this.id = id;
                this.dirname = dirname;

                var files = Directory.GetFiles(dirname, "*.*", SearchOption.TopDirectoryOnly);
                dir = new List<SnapshotFile>(files.Length);

                foreach (string fName in files)
                {
                    dir.Add(new SnapshotFile(fName));
                }
            }
        }

        public static bool Pack(string dirName, string fileName, Guid id)
        {
            try
            {
                var dict = new Dictionary<Guid, SnapshotDirectory>();

                if (!File.Exists(fileName))
                {
                    using (var fs = File.Create(fileName))
                    {
                        using (var gz = new GZipStream(fs, CompressionMode.Compress))
                        {
                            var bf = new BinaryFormatter();
                            dict[id] = new SnapshotDirectory(id, dirName);
                            bf.Serialize(gz, dict);
                        }
                    }
                }
                else
                {
                    var bf = new BinaryFormatter();

                    using (var fs = File.OpenRead(fileName))
                    {
                        using (var gz = new GZipStream(fs, CompressionMode.Decompress))
                        {
                            dict = (Dictionary<Guid, SnapshotDirectory>)bf.Deserialize(gz);
                        }
                    }

                    dict[id] = new SnapshotDirectory(id, dirName);

                    using (var fs = File.Open(fileName, FileMode.Truncate, FileAccess.Write))
                    {
                        using (var gz = new GZipStream(fs, CompressionMode.Compress))
                        {
                            bf.Serialize(gz, dict);
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Pack(string modelDir, string currentDir, string newDirName)
        {
            //string rootDir = Path.GetPathRoot(modelDir);
            //string currentPath = Directory.GetDirectories(rootDir, currentId, SearchOption.AllDirectories).Where(path =>
            //    {
            //        return path != modelDir;
            //    }).FirstOrDefault();

            try
            {
                CopyAll(modelDir, Path.Combine(currentDir, newDirName));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void CopyAll(string SourcePath, string DestinationPath)
    {
        if (!Directory.Exists(DestinationPath)) Directory.CreateDirectory(DestinationPath);

        //foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
        //    SearchOption.AllDirectories))
        //    Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

        foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
            SearchOption.TopDirectoryOnly))
            File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath));
    }

        public static bool UnPack(string modelDir, string currentDir)
        {
            try
            {
                //Directory.Delete(modelDir, true);
                foreach (string fn in Directory.GetFiles(modelDir)) File.Delete(fn);

                CopyAll(currentDir, modelDir);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UnPack(string dirName, string fileName, Guid id)
        {
            try
            {
                var dict = new Dictionary<Guid, SnapshotDirectory>();

                using (var fs = File.OpenRead(fileName))
                {
                    var bf = new BinaryFormatter();

                    using (var gz = new GZipStream(fs, CompressionMode.Decompress))
                    {
                        dict = (Dictionary<Guid, SnapshotDirectory>)bf.Deserialize(gz);
                    }
                }

                foreach (string fn in Directory.GetFiles(dirName)) File.Delete(fn);

                foreach (var item in dict[id].dir)
                {
                    using (var fs = File.Create(dirName + "\\" + item.filename))
                    {
                        fs.Write(item.file, 0, item.file.Length);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static IEnumerable<string> Browse(string fileName)
        {
            var dict = new Dictionary<Guid, SnapshotDirectory>();

            using (var fs = File.OpenRead(fileName))
            {
                var bf = new BinaryFormatter();

                using (var gz = new GZipStream(fs, CompressionMode.Decompress))
                {
                    dict = (Dictionary<Guid, SnapshotDirectory>)bf.Deserialize(gz);
                }
            }

            foreach (var item in dict.Values)
            {
                yield return item.id + ": " + item.dirname;
            }
        }

        public static bool DeleteItem(string fileName, Guid id)
        {
            try
            {
                var dict = new Dictionary<Guid, SnapshotDirectory>();
                var bf = new BinaryFormatter();

                using (var fs = File.OpenRead(fileName))
                {
                    using (var gz = new GZipStream(fs, CompressionMode.Decompress))
                    {
                        dict = (Dictionary<Guid, SnapshotDirectory>)bf.Deserialize(gz);
                    }
                }

                dict.Remove(id);

                using (var fs = File.Open(fileName, FileMode.Truncate, FileAccess.Write))
                {
                    using (var gz = new GZipStream(fs, CompressionMode.Compress))
                    {
                        bf.Serialize(gz, dict);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
