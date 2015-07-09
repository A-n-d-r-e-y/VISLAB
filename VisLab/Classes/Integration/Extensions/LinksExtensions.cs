using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
using VisLab.Classes.Integration.Wrappers;

namespace VisLab.Classes.Integration.Extensions
{
    public static class LinksExtensions
    {
        public static IEnumerable<LinkWrapper> Wrap(this Links links)
        {
            var enumerator = links.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var link = (Link)enumerator.Current;
                yield return link.Wrap();
            }
        }
    }
}