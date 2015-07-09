using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;

namespace VisLab.Classes
{
    public class LinkEvaluationWrapper
    {
        private readonly LinkEvaluation eval;

        public LinkEvaluationWrapper(LinkEvaluation eval)
        {
            this.eval = eval;
        }

        /// <summary>
        /// Database flag
        /// </summary>
        public bool IsDatabaseEnabled
        {
            get { return (short)eval.get_AttValue("DATABASE") != 0; }
            set { eval.set_AttValue("DATABASE", value ? -1 : 0); }
        }

        /// <summary>
        /// Write evaluation file flag (true/false)
        /// </summary>
        public bool IsFileEnabled
        {
            get { return (short)eval.get_AttValue("FILE") != 0; }
            set { eval.set_AttValue("FILE", value ? -1 : 0); }
        }

        /// <summary>
        /// Database table name
        /// </summary>
        public string TableName
        {
            get { return (string)eval.get_AttValue("TABLENAME"); }
            //get { return (string)eval.AttValue["TABLENAME"]; }
            set { eval.set_AttValue("TABLENAME", value); }
        }

        /// <summary>
        /// Path and filename for the configuration file
        /// </summary>
        public string FileName
        {
            get { return (string)eval.get_AttValue("FILENAME"); }
            set { eval.set_AttValue("FILENAME", value); }
        }
    }
}
