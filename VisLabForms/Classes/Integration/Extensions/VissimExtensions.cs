using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;

namespace VisLab.Classes
{
    public static class VissimExtensions
    {
        /// <summary>
        /// VISSIM revision number in text format.
        /// </summary>
        public static string GetRevisionNumber(this Vissim v)
        {
            return v.get_AttValue("REVISION").ToString();
        }

        /// <summary>
        /// Enable the main menu.
        /// </summary>
        public static void EnableMenu(this Vissim v)
        {
            v.set_AttValue("MENU", 1);
        }

        /// <summary>
        /// Disable the main menu.
        /// </summary>
        public static void DisableMenu(this Vissim v)
        {
            v.set_AttValue("MENU", 0);
        }

        /// <summary>
        /// Enable all toolbars
        /// </summary>
        public static void EnableToolbar(this Vissim v)
        {
            v.set_AttValue("TOOLBAR", 1);
        }

        /// <summary>
        /// Disable all toolbars except Zoom (File, Selection, Run Control, Network Elements, Animation, Test, and Simulation).
        /// </summary>
        public static void DisableToolbar(this Vissim v)
        {
            v.set_AttValue("TOOLBAR", 0);
        }

        /// <summary>
        /// The name of the currently loaded input file
        /// </summary>
        public static string GetInputFileName(this Vissim v)
        {
            return v.get_AttValue("INPUTFILE").ToString();
        }

        /// <summary>
        /// The current working directory
        /// </summary>
        public static string GetWorkingDirectory(this Vissim v)
        {
            return v.get_AttValue("WORKINGFOLDER").ToString();
        }

        /// <summary>
        /// The exe folder where VISSIM is started from
        /// </summary>
        public static string GetExecutionDirectory(this Vissim v)
        {
            return v.get_AttValue("EXEFOLDER").ToString();
        }

        /// <summary>
        /// VISSIM version in text format.
        /// </summary>
        public static string GetVersion(this Vissim v)
        {
            return v.get_AttValue("VERSION").ToString();
        }
    }
}
