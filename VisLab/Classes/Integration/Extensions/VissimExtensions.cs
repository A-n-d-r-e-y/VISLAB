using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
using VisLab.Classes.Integration.Wrappers;

namespace VisLab.Classes.Integration.Extensions
{
    public static class VissimExtensions
    {
        private static VissimWrapper vissimWrapper = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eval"></param>
        /// <returns>Cached wrapper</returns>
        public static VissimWrapper Wrap(this Vissim vissim)
        {
            if (vissimWrapper == null) vissimWrapper = new VissimWrapper(vissim);
            return vissimWrapper;
        }

        public static void DropCache()
        {
            vissimWrapper = null;
        }
    }
}
