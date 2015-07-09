using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;
using VisLab.Classes.Integration.Extensions;

namespace VisLab.Classes.Integration
{
    public class VissimSingleton
    {
        private static Vissim instance;

        private VissimSingleton() { }

        private static void CreateNewInstance()
        {
            instance = new Vissim();
            VissimExtensions.DropCache();
        }

        public static Vissim Instance
        {
            get
            {
                if (instance == null) CreateNewInstance();
                else
                {
                    try
                    {
                        instance.DoEvents();
                    }
                    catch
                    {
                        CreateNewInstance();
                    }
                }
                return instance;
            }
        }

        public static bool IsInstanciated
        {
            get
            {
                try
                {
                    if (instance != null) instance.DoEvents();
                }
                catch
                {
                    instance = null;
                }

                return instance != null;
            }
        }
    }
}
