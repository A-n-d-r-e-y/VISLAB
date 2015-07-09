using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
using VisLab.Classes.Integration.Wrappers;

namespace VisLab.Classes.Integration.Extensions
{
    public enum WrapMode { CachedWrap, DirectWrap, CommonWrap }

    public static class LinkExtensions
    {
        /// <summary>
        /// No need to drop this cache if no one new link created. Instead, need to reset LinkWrapper lines cache.
        /// If new link was created - reset this cache
        /// </summary>
        private static Dictionary<int, LinkWrapper> wrappersCache = new Dictionary<int, LinkWrapper>();

        public static LinkWrapper Wrap(this Link link, WrapMode mode)
        {
            switch (mode)
            {
                case WrapMode.CachedWrap:
                    if (!wrappersCache.ContainsKey(link.ID))
                    {
                        var w = new LinkWrapper(link);
                        wrappersCache[link.ID] = w;
                        //return w;
                    }

                    return wrappersCache[link.ID];

                case WrapMode.DirectWrap:
                    var x = new LinkWrapper(link);
                    wrappersCache[link.ID] = x;
                    return x;

                case WrapMode.CommonWrap:
                default:
                    return new LinkWrapper(link);
            }
        }

        public static LinkWrapper Wrap(this Link link)
        {
            return Wrap(link, WrapMode.CachedWrap);
        }

        public static void DropCache()
        {
            wrappersCache = null;
        }
    }
}
