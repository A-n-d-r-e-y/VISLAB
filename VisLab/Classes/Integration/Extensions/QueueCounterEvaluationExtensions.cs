using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
using VisLab.Classes.Integration.Wrappers;

namespace VisLab.Classes.Integration.Extensions
{
    public static class QueueCounterEvaluationExtensions
    {
        private static QueueCounterEvaluationWrapper wrapper = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="link"></param>
        /// <returns>Cached wrapper</returns>
        public static QueueCounterEvaluationWrapper Wrap(this QueueCounterEvaluation queue)
        {
            if (wrapper == null) wrapper = new QueueCounterEvaluationWrapper(queue);
            return wrapper;
        }
    }
}
