using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
using System.Drawing;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace VisLab.Classes
{
    public static class NetworkExtensions
    {
        //private static Dictionary<string, NetworkWrapper> wrappersCache = new Dictionary<string, NetworkWrapper>();
        public static NetworkWrapper wrapperCache;

        public static NetworkWrapper Wrap(this Net net)
        {
            if (wrapperCache == null)
            {
                var w = new NetworkWrapper(net);
                wrapperCache = w;
                return w;
            }

            return wrapperCache;
        }
    }
}
