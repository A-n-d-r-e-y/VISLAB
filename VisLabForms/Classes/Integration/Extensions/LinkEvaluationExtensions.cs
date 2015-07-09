using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;

namespace VisLab.Classes
{
    public static class LinkEvaluationExtensions
    {
        private static LinkEvaluationWrapper wrapper = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eval"></param>
        /// <returns>Cached wrapper</returns>
        public static LinkEvaluationWrapper Wrap(this LinkEvaluation eval)
        {
            if (wrapper == null) wrapper = new LinkEvaluationWrapper(eval);
            return wrapper;
        }
    }
}
