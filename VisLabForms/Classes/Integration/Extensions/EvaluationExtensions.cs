using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;

namespace VisLab.Classes
{
    public static class EvaluationExtensions
    {
        private static EvaluationWrapper wrapper = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eval"></param>
        /// <returns>Cached wrapper</returns>
        public static EvaluationWrapper Wrap(this Evaluation eval)
        {
            if (wrapper == null) wrapper = new EvaluationWrapper(eval);       
            return wrapper;
        }
    }
}
