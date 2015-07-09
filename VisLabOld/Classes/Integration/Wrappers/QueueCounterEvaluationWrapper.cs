using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;

namespace VisLab.Classes
{
    public class QueueCounterEvaluationWrapper
    {
        private readonly QueueCounterEvaluation eval;

        public QueueCounterEvaluationWrapper(QueueCounterEvaluation eval)
        {
            this.eval = eval;
        }

        /// <summary>
        /// Write evaluation file flag (true/false)
        /// </summary>
        public bool IsFileEnabled
        {
            get { return (short)eval.get_AttValue("FILE") != 0; }
            set { eval.set_AttValue("FILE", value ? -1 : 0); }
        }
    }
}
