using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;

namespace VisLab.Classes.Integration.Wrappers
{
    public class VissimWrapper
    {
        private readonly Vissim vissim;

        public VissimWrapper(Vissim vissim)
        {
            this.vissim = vissim;
        }

        /// <summary>
        /// VISSIM revision number in text format.
        /// </summary>
        public string RevisionNumber
        {
            get { return vissim.get_AttValue("REVISION").ToString(); }
        }

        /// <summary>
        /// Enable the main menu.
        /// </summary>
        public void EnableMenu()
        {
            vissim.set_AttValue("MENU", 1);
        }

        /// <summary>
        /// Disable the main menu.
        /// </summary>
        public void DisableMenu()
        {
            vissim.set_AttValue("MENU", 0);
        }

        /// <summary>
        /// Enable all toolbars
        /// </summary>
        public void EnableToolbar()
        {
            vissim.set_AttValue("TOOLBAR", 1);
        }

        /// <summary>
        /// Disable all toolbars except Zoom (File, Selection, Run Control, Network Elements, Animation, Test, and Simulation).
        /// </summary>
        public void DisableToolbar()
        {
            vissim.set_AttValue("TOOLBAR", 0);
        }

        /// <summary>
        /// The name of the currently loaded input file
        /// </summary>
        public string InputFileName
        {
            get { return vissim.get_AttValue("INPUTFILE").ToString(); }
        }

        /// <summary>
        /// The current working directory
        /// </summary>
        public string WorkingDirectory
        {
            get {return vissim.get_AttValue("WORKINGFOLDER").ToString(); }
        }

        /// <summary>
        /// The exe folder where VISSIM is started from
        /// </summary>
        public string ExecutionDirectory
        {
            get { return vissim.get_AttValue("EXEFOLDER").ToString(); }
        }

        /// <summary>
        /// VISSIM version in text format.
        /// </summary>
        public string Version
        {
            get { return vissim.get_AttValue("VERSION").ToString(); }
        }
    }
}
