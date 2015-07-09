using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;

namespace VisLab.Classes
{
    public class VissimSingleton
    {
        private static Vissim instance;

        private VissimSingleton() { }

        public static Vissim Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Vissim();
                }
                return instance;
            }
        }
    }
}
