using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;

namespace VisLab.Classes
{
    public static class LinksExtensions
    {
        public static IEnumerable<LinkWrapper> Wrap(this Links links)
        {
            var enumerator = links.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var link = (Link)enumerator.Current;
                yield return new LinkWrapper(link);
            }
        }
    }
}