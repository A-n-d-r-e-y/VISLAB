using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using VisLab.Properties;
using VisLab.Classes;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using vissim = VisLab.Classes.VissimSingleton;
using Microsoft.SqlServer.Management.Common;
using System.Reflection;

namespace VisLab.Forms
{
    public partial class MainForm : Form
    {
        private static int instancesCounter = 0;
        private ProjectManager pm = new ProjectManager();

        // cached handle
        private IWin32Window vissimHandle;
        //public IWin32Window VissimHandle
        //{
        //    get { return vissimHandle; }
        //}

        public MainForm(IWin32Window vissimHandle)
        {
            InitializeComponent();

            ++instancesCounter;

            cboWindowMode.SelectedIndex = Settings.Default.windowModeIndex;

            if (Settings.Default.recentProjectsList == null) Settings.Default.recentProjectsList = new StringCollection();
            foreach (string item in Settings.Default.recentProjectsList)
            {
                AddValueToRecentProjectsList(item);
            }

            pm.LoadProjectFailed += (sender, e) =>
                {
                    if (Settings.Default.recentProjectsList.Contains(e.ProjectFileName))
                    {
                        if (MessageBox.Show(this,
                            string.Format("Project file {0}\ndoes not exist anymore.\nDelete it from recent projects list?", e.ProjectFileName),
                            "Project file missing",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            Settings.Default.recentProjectsList.Remove(e.ProjectFileName);

                            if (mntRecent.DropDownItems.ContainsKey(e.ProjectFileName))
                                mntRecent.DropDownItems.RemoveByKey(e.ProjectFileName);

                            if (Settings.Default.currentProject == e.ProjectFileName) Settings.Default.currentProject = string.Empty;
                        }
                    }
                };

            label1.DataBindings.Add("Enabled", pm, "HasProject");
            label1.DataBindings.Add("Text", pm, "ProjectName");
            //label2.DataBindings.Add("Text", pm, "LastSnapshotId");
        }

        public void AddValueToRecentProjectsList(string value)
        {
            var item = mntRecent.DropDownItems.Add(value);
            item.Name = value;
            item.Click += (sender, e) =>
            {
                string projectFileName = (sender as ToolStripItem).Text;

                pm.LoadProject(projectFileName);
            };
        }

        private void cboWindowMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Settings.Default.windowModeIndex != cboWindowMode.SelectedIndex)
            {
                Settings.Default.windowModeIndex = cboWindowMode.SelectedIndex;
                Settings.Default.Save();

                var f = new MainForm(vissimHandle);
                f.Location = this.Location;
                f.Size = this.Size;

                switch (cboWindowMode.SelectedIndex)
                {
                    case 0: // Standalone
                        f.ShowInTaskbar = true;
                        f.Show();
                        break;
                    case 1: // Modeless
                        f.ShowInTaskbar = false;
                        f.Show(vissimHandle);
                        break;
                    case 2: // Modal
                        f.ShowInTaskbar = false;
                        this.Hide();
                        f.ShowDialog(vissimHandle);
                        break;
                    case 3: // Topmost
                        f.ShowInTaskbar = true;
                        f.TopMost = true;
                        f.Show();
                        break;
                }

                this.Close();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            --instancesCounter;
            if (instancesCounter == 0) Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void mntNew_Click(object sender, EventArgs e)
        {
            var wnd = new CreateProjectForm();
            if (wnd.ShowDialog(this) == DialogResult.OK)
            {
                string projectName = wnd.tbxProjectName.Text;
                string projectDir = wnd.tbxProjectLocation.Text;
                string modelName = wnd.tbxNewModelName.Text;
                string modelFileName = wnd.tbxModelFile.Text;

                switch (wnd.checkedRadioButtonName)
                {
                    case "rbCreateNew":
                        pm.CreateNewProject(projectName, projectDir, modelName);
                        break;
                    case "rbSelectFromFile":
                        pm.CloneProject(projectName, projectDir, modelFileName);
                        break;
                    case "rbSelectCurrent":
                        pm.CloneProject(projectName, projectDir, string.Empty);
                        break;
                }
            }
        }

        private void mntOpen_Click(object sender, EventArgs e)
        {
            dlgOpenFile.Filter = "VISSIM Laboratory project file|*.vislab";
            if (dlgOpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pm.LoadProject(dlgOpenFile.FileName);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.OpenProjectOnStartup)
                if (!string.IsNullOrWhiteSpace(Settings.Default.currentProject))
                    pm.LoadProject(Settings.Default.currentProject);
                else
                {
                    mntOpen_Click(mntOpen, null);
                }
        }

        private void mntRunSimulation_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                var sim = vissim.Instance.Simulation;

                for (int i = 0; i < 1; i++)
                {
                    sim.RunIndex = i;
                    sim.RandomSeed = (int)DateTime.Now.Ticks;
                    sim.Comment = string.Format("Simulation random seed = {0}", sim.RandomSeed);

                    sim.RunContinuous();
                    vissim.Instance.DoEvents();
                }

                pm.MakeSnapshot();

                MessageBox.Show("Ok");
            });

            //Action func = (() =>
            //{
            //    var sim = vissim.Instance.Simulation;

            //    for (int i = 0; i < 1; i++)
            //    {
            //        sim.RunIndex = i;
            //        sim.RandomSeed = (int)DateTime.Now.Ticks;
            //        sim.Comment = string.Format("Simulation random seed = {0}", sim.RandomSeed);

            //        sim.RunContinuous();
            //        vissim.Instance.DoEvents();
            //    }
            //});
 
            //func.BeginInvoke(xxx, func);
        }

        //private void xxx(IAsyncResult cookie)
        //{
        //    var caller = (Action)cookie.AsyncState;
        //    caller.EndInvoke(cookie);


        //    //this.Invoke(() =>
        //    //    {
        //    //        pm.SnapshotManager.MakeSnapshot();
        //    //    });

        //    //this.Invoke(new Action(() =>
        //    //    {
        //    //        pm.SnapshotManager.MakeSnapshot();
        //    //    })); 
        //}

        private void mntStopSimulation_Click(object sender, EventArgs e)
        {
            vissim.Instance.Simulation.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TreeForm frm = new TreeForm(pm);
            frm.Show();
        }

        private void mntBrowseSnapshot_Click(object sender, EventArgs e)
        {
            var dlg = new BrowseDataForm();
            dlg.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLAdmin.MakeBackup(new ServerMessageEventHandler((o, args) =>
                {
                    MessageBox.Show(args.Error.Message);
                }), pm.LastSnapshotId, pm.Experiment.Number, "MATRIX", "Rainbow", @"\\tranquillity-pc\Temp\",
                "sa", "Macciato777");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SQLAdmin.MakeRestore(new ServerMessageEventHandler((o, args) =>
            {
                MessageBox.Show(args.Error.Message);
            }), pm.LastSnapshotId, "MATRIX", "Rainbow", @"\\tranquillity-pc\Temp\", "sa", "Macciato777");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var folderName = GetNetworkFolders(new FolderBrowserDialog());
        }

        private string GetNetworkFolders(FolderBrowserDialog oFolderBrowserDialog)
        {
            Type type = oFolderBrowserDialog.GetType();
            FieldInfo fieldInfo = type.GetField("rootFolder", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(oFolderBrowserDialog, 18);
            if (oFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                return oFolderBrowserDialog.SelectedPath.ToString();
            }
            else
            {
                return "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ModelForm form = new ModelForm(VissimSingleton.Instance.Net);
            form.Show();
        }

    }
}
