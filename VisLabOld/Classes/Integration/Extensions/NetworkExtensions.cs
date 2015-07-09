using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
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
            if (wrapperCache == null) wrapperCache = new NetworkWrapper(net);

            return wrapperCache;
        }
    }
}
