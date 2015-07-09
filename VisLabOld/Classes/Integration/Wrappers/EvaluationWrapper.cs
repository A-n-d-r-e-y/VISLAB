using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
using vissim = VisLab.Classes.VissimSingleton;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace VisLab.Classes
{
    public class EvaluationWrapper
    {
        private readonly Evaluation eval;

        public EvaluationWrapper(Evaluation eval)
        {
            this.eval = eval;
        }

        public string GetConnectionString()
        {
            string path = System.IO.Path.Combine(vissim.Instance.GetWorkingDirectory(), vissim.Instance.GetInputFileName());
            string text;

            using (var sr = new StreamReader(File.OpenRead(path)))
            {
                text = sr.ReadToEnd();
            }

            Regex regex = new Regex("EVALUATION\\s*DATABASE\\s*\"(?<conn>.*Data Source.*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var match = regex.Match(text);
            var group = match.Groups["conn"];
            if (group != null) return group.Value;
            else return string.Empty;
        }

        /// <summary>
        /// Analyzer database (True/False)
        /// </summary>
        public bool IsAnalyzerEnabled
        {
            get { return (short)eval.get_AttValue("ANALYZER") != 0; }
            set { eval.set_AttValue("ANALYZER", value ? -1 : 0); }
        }

        /// <summary>
        /// Convergence evaluations (True/False)
        /// </summary>
        public bool IsConvergenceEnabled
        {
            get { return (short)eval.get_AttValue("CONVERGENCE") != 0; }
            set { eval.set_AttValue("CONVERGENCE", value ? -1 : 0); }
        }

        /// <summary>
        /// Data collections (True/False)
        /// </summary>
        public bool IsDataCollectionsEnabled
        {
            get { return (short)eval.get_AttValue("DATACOLLECTION") != 0; }
            set { eval.set_AttValue("DATACOLLECTION", value ? -1 : 0); }
        }

        /// <summary>
        /// Delays (True/False)
        /// </summary>
        public bool IsDelaysEnabled
        {
            get { return (short)eval.get_AttValue("DELAY") != 0; }
            set { eval.set_AttValue("DELAY", value ? -1 : 0); }
        }

        /// <summary>
        /// Export evaluations (True/False)
        /// </summary>
        public bool IsExportEnabled
        {
            get { return (short)eval.get_AttValue("EXPORT") != 0; }
            set { eval.set_AttValue("EXPORT", value ? -1 : 0); }
        }

        /// <summary>
        /// Link segment evaluations (True/False)
        /// </summary>
        public bool IsLinksEnabled
        {
            get { return (short)eval.get_AttValue("LINK") != 0; }
            set { eval.set_AttValue("LINK", value ? -1 : 0); }
        }

        /// <summary>
        /// Node evaluations (True/False)
        /// </summary>
        public bool IsNodesEnabled
        {
            get { return (short)eval.get_AttValue("NODE") != 0; }
            set { eval.set_AttValue("NODE", value ? -1 : 0); }
        }

        /// <summary>
        /// Dynamic assignment path evaluations (True/False)
        /// </summary>
        public bool IsPathsEnabled
        {
            get { return (short)eval.get_AttValue("PATHS") != 0; }
            set { eval.set_AttValue("PATHS", value ? -1 : 0); }
        }
    }
}
