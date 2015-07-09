using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VisLab.Properties;
using System.Collections.Specialized;
using VisLab.Forms;
using System.Diagnostics;

namespace VisLab
{
    static class Program
    {
        private static MainForm frmMain;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IWin32Window vissimHandle = null;

            Process[] processes = Process.GetProcessesByName("vissim");
            if (processes.Length > 0) vissimHandle = new Classes.WindowWrapper(processes[0].MainWindowHandle);

            if (vissimHandle != null && vissimHandle.Handle != IntPtr.Zero)
            {
                Settings.Default.SettingChanging += new System.Configuration.SettingChangingEventHandler(Default_SettingChanging);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                frmMain = new MainForm(vissimHandle);

                switch (Settings.Default.windowModeIndex)
                {
                    case 0: // Standalone
                        frmMain.ShowInTaskbar = true;
                        frmMain.Show();
                        break;
                    case 1: // Modeless
                        frmMain.Show(vissimHandle);
                        break;
                    case 2: // Modal
                        frmMain.ShowDialog(vissimHandle);
                        break;
                    case 3: // Topmost
                        frmMain.ShowInTaskbar = true;
                        frmMain.TopMost = true;
                        frmMain.Show();
                        break;
                }

                Application.Run();
            }
            else
            {
                MessageBox.Show(
                    "Please, start VISSIM 5.3 with [/Automation] parameter first!\nVisLab application will be closed!",
                    "Warning!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        static void Default_SettingChanging(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            if (e.SettingName == "currentProject")
            {
                // by default list is emty and .NET do not create any instance
                if (Settings.Default.recentProjectsList == null) Settings.Default.recentProjectsList = new StringCollection();

                string newValue = e.NewValue.ToString();

                if (!Settings.Default.recentProjectsList.Contains(newValue))
                {
                    // delete first item in list if total length > max length
                    if (Settings.Default.recentProjectsList.Count >= Settings.Default.RecentProjectsListMaxLength)
                    {
                        Settings.Default.recentProjectsList.RemoveAt(0);
                        frmMain.mntRecent.DropDownItems.RemoveAt(0);
                    }

                    Settings.Default.recentProjectsList.Add(newValue);
                    frmMain.AddValueToRecentProjectsList(newValue);
                }
            }
        }
    }
}
