using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisLab.Classes.Integration.Wrappers;
using VISSIM_COMSERVERLib;

namespace VisLab.Classes.Integration.Extensions
{
    public static class GraphicsExtensions
    {
        private static GraphicsWrapper gw = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eval"></param>
        /// <returns>Cached wrapper</returns>
        public static GraphicsWrapper Wrap(this Graphics gr)
        {
            if (gw == null) gw = new GraphicsWrapper(gr);
            return gw;
        }
    }
}
