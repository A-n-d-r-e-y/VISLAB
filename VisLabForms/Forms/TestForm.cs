using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using VisLab.Properties;
using VISSIM_COMSERVERLib;
using VisLab.Classes;
using System.Collections;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace VisLab.Forms
{
    //public enum WindowMode { wmStandalone, wmModeless, wmModal, wmTopmost } -> Settings

    /// <summary>
    /// jjj
    /// </summary>
    public partial class TestForm : Form
    {
        private static int instancesCounter = 0;
        
        // cached handle
        private IWin32Window vissimHandle;
        public IWin32Window VissimHandle
        {
            get
            {
                if (vissimHandle == null || vissimHandle.Handle == IntPtr.Zero)
                {
                    Process[] processes = Process.GetProcessesByName("vissim");
                    if (processes.Length > 0) vissimHandle = new Classes.WindowWrapper(processes[0].MainWindowHandle);
                }

                return vissimHandle;
            }
        }

        public TestForm()
        {
            ++instancesCounter;
            InitializeComponent();

            //cboWindowStyle.DataBindings.Add("SelectedIndex", Settings.Default, "windowModeIndex");
            cboWindowMode.SelectedIndex = Settings.Default.windowModeIndex;

            //ThreadPool.QueueUserWorkItem((o) => VissimSingleton.Instance.Net.Wrap().Initialize());
            //VissimSingleton.Instance.Net.Wrap().Initialize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ThreadPool.QueueUserWorkItem((o) => sim.RunContinuous());

            ThreadPool.QueueUserWorkItem((o) =>
                {
                    var sim = VissimSingleton.Instance.Simulation;
                    for (int i = 0; i < 5; i++)
                    {
                        sim.RunIndex = i;
                        sim.RandomSeed = (int)DateTime.Now.Ticks;
                        sim.Comment = string.Format("Simulation random seed = {0}", sim.RandomSeed);

                        sim.RunContinuous();
                        VissimSingleton.Instance.DoEvents();
                    }
                });
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var sim = VissimSingleton.Instance.Simulation;
            //sim.RandomSeed = (int)DateTime.Now.Ticks;
            //sim.Comment = string.Format("Simulation random seed = {0}", sim.RandomSeed);

            sim.AttValue["RANDOMSEEDINC"] = 3;
            sim.AttValue["DIRECTORY"] = @"D:\MOPO3OB\Desktop\BW\Olaine (Dymanic)\New folder";

            ThreadPool.QueueUserWorkItem((o) => sim.RunMulti(5));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VissimSingleton.Instance.Simulation.Stop();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            --instancesCounter;
            if (instancesCounter == 0) Application.Exit();
        }

        private void cboWindowMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Settings.Default.windowModeIndex != cboWindowMode.SelectedIndex)
            {
                Settings.Default.windowModeIndex = cboWindowMode.SelectedIndex;
                Settings.Default.Save();

                var f = new TestForm();
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
                        f.Show(VissimHandle);
                        break;
                    case 2: // Modal
                        f.ShowInTaskbar = false;
                        this.Hide();
                        f.ShowDialog(VissimHandle);
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VissimSingleton.Instance.LoadNet();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VissimSingleton.Instance.SaveNet();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VissimSingleton.Instance.LoadLayout();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VissimSingleton.Instance.SaveLayout();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var btn = (sender as Button);
            btn.Enabled = false;
            var text = btn.Text;
            btn.Text = "Running...";

            var query = from n in VissimSingleton.Instance.Net.Links.Wrap()
                        select new
                        {
                            name = n.Name,
                            id = n.ID,
                            length = n.Length,
                            a = n.Gradient,
                            b = n.BehaviorType,
                            c = n.Cost,
                            d = n.DisplayType,
                            e = n.EmergencyStop,
                            f = n.FromLane,
                            g = n.FromLinkCoord,
                            h = n.IsConnector,
                            i = n.LaneChangeDistance,
                            j = n.LanesCount,
                            k = n.LaneWidth
                        };

            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bs = new BindingSource()
                {
                    DataSource = query
                };

                Action act = () =>
                    {
                        dataGridView1.DataSource = bs;
                        btn.Text = text;
                        btn.Enabled = true;
                    };

                this.BeginInvoke(act);
            });
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                var link = VissimSingleton.Instance.Net.Links.GetLinkByNumber(1).Wrap();
                MessageBox.Show(string.Format("link name: {0}, avgSpeed: {1}", link.Name, link.AvgSpeed()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public struct pp
        {
            public int x;
            public int y;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            StringBuilder list = new StringBuilder();

            //Link link = VissimSingleton.Instance.Net.Links[1];

            //list.AppendFormat("{1}: {0}\n", link.Name, link.ID);
            //foreach (var item in link.GetPoints())
            //{
            //    list.AppendFormat("{0}.{1}\n", item.X, item.Y);
            //}

            //MessageBox.Show(list.ToString());

            ModelForm form = new ModelForm(VissimSingleton.Instance.Net);
            form.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string workingDir = VissimSingleton.Instance.GetWorkingDirectory();
                string exeDir = VissimSingleton.Instance.GetExecutionDirectory();

                if (workingDir == exeDir) return;

                //VissimSingleton.Instance.SaveNet();
                VissimSingleton.Instance.SaveLayout(workingDir + "vissim.ini");

                string query = BuildQuery(workingDir);
                if (!string.IsNullOrEmpty(query))
                {
                    var sb = new SqlConnectionStringBuilder();
                    sb.DataSource = "TOSHIBA";
                    sb.InitialCatalog = "VISSIM";
                    sb.IntegratedSecurity = true;

                    using (var conn = new SqlConnection(sb.ConnectionString))
                    {
                        conn.Open();
                        using (var command = new SqlCommand(query, conn))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string BuildQuery(string path)
        {
            var files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            var sb = new StringBuilder();

            if (files.Length > 0)
            {
                sb.AppendLine("INSERT INTO Files");

                for (int i = 0; i < files.Length; i++)
                {
                    string fileName = files[i];

                    sb.AppendFormat("SELECT '{2}', '{1}', BulkColumn FROM OPENROWSET (BULK '{0}', SINGLE_BLOB) rowset\n",
                        fileName, System.IO.Path.GetExtension(fileName).Substring(1, 3).ToUpper(), System.IO.Path.GetFileName(fileName));

                    if (i < files.Length - 1) sb.AppendLine("UNION");
                }
            }

            return sb.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string netFileName = VissimSingleton.Instance.GetInputFileName();
                string workingDir = VissimSingleton.Instance.GetWorkingDirectory();
                string exeDir = VissimSingleton.Instance.GetExecutionDirectory();

                if (workingDir == exeDir) throw new Exception("ex");

                var files = Directory.GetFiles(workingDir, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string fileName in files)
                {
                    File.Delete(fileName);
                }

                var sb = new SqlConnectionStringBuilder();
                sb.DataSource = "TOSHIBA";
                sb.InitialCatalog = "VISSIM";
                sb.IntegratedSecurity = true;

                using (var conn = new SqlConnection(sb.ConnectionString))
                {
                    conn.Open();
                    using (var reader = new SqlCommand("SELECT * FROM Files;", conn).ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                using (var fs = new FileStream(workingDir + reader.GetString(1), FileMode.Create, FileAccess.Write))
                                {
                                    var bytes = reader.GetSqlBytes(3);
                                    fs.Write(bytes.Buffer, 0, (int)bytes.Length);
                                }
                            }
                        }
                    }
                }
                
                VissimSingleton.Instance.LoadNet(workingDir + netFileName);
                VissimSingleton.Instance.LoadLayout(workingDir + "vissim.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            VissimSingleton.Instance.DisableMenu();
            VissimSingleton.Instance.DisableToolbar();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            VissimSingleton.Instance.EnableMenu();
            VissimSingleton.Instance.EnableToolbar();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //VissimSingleton.Instance.Evaluation.Wrap().IsAnalyzerEnabled = true;
            //VissimSingleton.Instance.Evaluation.LinkEvaluation.Wrap().IsFileEnabled = true;
            //VissimSingleton.Instance.Evaluation.LinkEvaluation.Wrap().TableName = "xxx";
            //MessageBox.Show(string.Format("LinksEvaluation = {0}\nDatabase = {1}\nTablename={2}\nFilename={3}\nFileEnabled={4}",
            //    VissimSingleton.Instance.Evaluation.Wrap().IsLinksEnabled,
            //    VissimSingleton.Instance.Evaluation.LinkEvaluation.Wrap().IsDatabaseEnabled,
            //    VissimSingleton.Instance.Evaluation.LinkEvaluation.Wrap().TableName,
            //    VissimSingleton.Instance.Evaluation.LinkEvaluation.Wrap().FileName,
            //    VissimSingleton.Instance.Evaluation.LinkEvaluation.Wrap().IsFileEnabled));
            //MessageBox.Show(((short)VissimSingleton.Instance.Evaluation.get_AttValue("ANALYZER")).ToString());
            
            //MessageBox.Show((string)VissimSingleton.Instance.Evaluation.LinkEvaluation.get_AttValue("TABLENAME"));
            //get { return (string)eval.AttValue["TABLENAME"]; }
            //set { eval.set_AttValue("TABLENAME", value); }

            //MessageBox.Show(VissimSingleton.Instance.Evaluation.QueueCounterEvaluation.Wrap().FileName);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //var ex = new Experiment();
            //var ex2 = new Experiment();
            //ex2.ChildExperiments.Add(new Experiment());
            //ex2.ChildExperiments.Add(new Experiment());
            //ex.ChildExperiments.Add(ex2);
            //ex.ChildExperiments.Add(new Experiment());

            //ex.Save("tree.xml");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Experiment.FindParentId("tree.xml", "37488e1b-110a-4752-a0c8-7c8095601602").ToString());

            var ex = ExperimentsTree.ExperimentsTreeNode.Load("tree2.dat");
            MessageBox.Show(ex.Id.ToString());
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //FileContainer.Write(@"D:\BDE", "data.tree");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //FileContainer.Read("data", "data.tree");
            DirectoryPacker.UnPack("data", "data.tree", Guid.NewGuid());
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //FileContainer.Append(@"D:\Temp\2\2.PNG", "data.tree");
            DirectoryPacker.Pack(@"D:\bin\ILSpy", "data.tree", Guid.NewGuid());
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //Project p = new Project();
            //p.Files.ModelDirectory = @"Model";
            //p.Name = "Test Project";
            //p.Files.SnapshotDataFileName = "data.tree";
            //p.Files.SnapshotTreeFileName = "tree.xml";
            ////p.SetNotSaved();

            //p.Save("Test Project.xml");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //openFileDialog.Filter = "VISSIM Laboratory project file|*.vislab";
            //if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    ProjectManager.LoadProject(openFileDialog.FileName);
            //}
        }

        private void button23_Click(object sender, EventArgs e)
        {
            CreateProjectForm wnd = new CreateProjectForm();
            wnd.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //TreeForm frm = new TreeForm();
            //frm.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
    }
}
