using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using vissim = VisLab.Classes.VissimSingleton;
using VisLab.Properties;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;
using System.Threading;
using System.Windows.Threading;
using System.Data.OleDb;

namespace VisLab.Classes
{

    public enum ModelCreationMode { cmCreateNew, cmClone }

    public class ProjectEventArgs : EventArgs
    {
        public string ProjectFileName;

        public ProjectEventArgs(string projectFileName)
        {
            this.ProjectFileName = projectFileName;
        }
    }

    /// <summary>
    /// Функции:
    /// 1. делать пути полными
    /// </summary>
    public class ProjectManager : INotifyPropertyChanged
    {
        #region windows
        private readonly Dispatcher gUI;
        #endregion

        #region project

        private string projectDir;

        private Project project;
        private Project Project
        {
            get { return project; }
            set
            {
                project = value;

                // clear Cache;
                modelFileName = string.Empty;


                //OnPropertyChanged("HasProject");
                //OnPropertyChanged("ProjectName");
                //OnPropertyChanged("LastSnapshotId");
            }
        }

        public bool HasProject
        {
            get { return project != null; }
        }

        public string ProjectName
        {
            get
            {
                string name = string.Empty;
                if (HasProject) name = project.Name;

                return name;
            }
        }

        public string SnapshotDataFileName
        {
            get
            {
                string fileName = string.Empty;
                if (HasProject) fileName = Path.Combine(projectDir, project.Files.SnapshotDataFileName);

                return fileName;
            }
        }

        public string SnapshotTreeFileName
        {
            get
            {
                string fileName = string.Empty;
                if (HasProject) fileName = Path.Combine(projectDir, project.Files.SnapshotTreeFileName);

                return fileName;
            }
        }

        public string ExperimentFileName
        {
            get
            {
                string fileName = string.Empty;
                if (HasProject) fileName = Path.Combine(projectDir, project.Files.ExperimentFileName);

                return fileName;
            }
        }

        public string ModelDir
        {
            get
            {
                string dirName = string.Empty;
                if (HasProject) dirName = Path.Combine(projectDir, project.Files.ModelDirectory);

                if (!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);

                return dirName;
            }
        }

        public Guid LastSnapshotId
        {
            get
            {
                Guid guid = Guid.Empty;
                if (HasProject) guid = project.CurrentExperimentId;

                return guid;
            }
            set
            {
                if (HasProject)
                {
                    project.CurrentExperimentId = value;

                    OnPropertyChanged("ExperimentNumber");
                }
            }
        }

        private string ProjectFileName
        {
            get
            {
                string fileName = string.Empty;
                if (HasProject) fileName = string.Format("{0}\\{1}{2}", projectDir, Project.Name, Project.FileExtension);

                return fileName;
            }
        }

        private void SaveProjectToSettings()
        {
            string fn = ProjectFileName;

            if (!string.IsNullOrWhiteSpace(fn))
            {
                Settings.Default.currentProject = fn;
                Settings.Default.Save();
            }
        }

        public void SaveProject()
        {
            project.Save(projectDir);
        }

        #endregion

        #region tree

        public string TreeFileName
        {
            get
            {
                string fileName = string.Empty;
                if (HasProject) fileName = Path.Combine(projectDir, project.Files.SnapshotTreeFileName);

                return fileName;
            }
        }

        private ExperimentsTree tree;
        private ExperimentsTree Tree
        {
            get
            {
                if (tree == null)
                {
                    tree = ExperimentsTree.Load(TreeFileName);
                }

                return tree; 
            }
        }

        public void DrawTree(Canvas canvas, Point point)
        {
            TreePainter.DrawTopDown(canvas, Tree.root, point, ExperimentNumber);
        }

        public void SaveTree()
        {
            Tree.Save(TreeFileName);
        }

        #endregion

        #region experiment

        public int ExperimentNumber
        {
            get
            {
                var dict = Experiment.GetDict();
                if (dict.ContainsKey(LastSnapshotId)) return dict[LastSnapshotId].Number;
                else return -1;
            }
        }

        //private Experiment experiment;
        //public Experiment Experiment { get { return experiment; } }

        #endregion

        #region model

        private string modelFileName;
        private string ModelFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(modelFileName))
                {
                    var filesList = Directory.GetFiles(ModelDir, "*.inp");
                    if (filesList.Length > 0)
                    {
                        modelFileName = filesList[0];
                    }
                }

                return modelFileName;
            }
        }

        private string LayoutFileName
        {
            get { return Path.Combine(ModelDir, "vissim.ini"); }
        }

        private void LoadModel()
        {
            vissim.Instance.LoadNet(ModelFileName);
            vissim.Instance.LoadLayout(LayoutFileName);
        }

        private void SaveModel()
        {
            vissim.Instance.SaveLayout(LayoutFileName);
            vissim.Instance.SaveNet();
        }

        private void CreateModel(string modelName)
        {
            vissim.Instance.New();
            vissim.Instance.Net.Name = modelName;

            vissim.Instance.SaveNetAs(string.Format("{0}\\{1}{2}", ModelDir, modelName, modelName.EndsWith(".inp") ? string.Empty : ".inp"));
            vissim.Instance.SaveLayout(LayoutFileName);
        }

        #endregion

        #region events

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<EventArgs> Initialized;
        public event EventHandler<EventArgs> Initialization;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void OnInitialization()
        {
            gUI.Invoke((Action)(() =>
            {
                if (Initialization != null) Initialization(this, EventArgs.Empty);
            }));
        }

        private void OnInitialized()
        {
            gUI.Invoke((Action)(() =>
            {
                if (Initialized != null) Initialized(this, EventArgs.Empty);
            }));
        }

        #endregion

        public ProjectManager(Dispatcher disp) { this.gUI = disp; }

        public void Initialize(string projectFileName)
        {
            ThreadPool.QueueUserWorkItem(o =>
                {
                    OnInitialization();

                    projectDir = Path.GetDirectoryName(projectFileName);

                    Project = Project.Load(projectFileName);
                    Experiment.Load(ExperimentFileName, LastSnapshotId);
                    tree = ExperimentsTree.Load(SnapshotTreeFileName);

                    LoadModel();

                    if (Experiment.GetInstance(LastSnapshotId).HasBackup) TakeBackup(LastSnapshotId);

                    OnInitialized();
                });
        }

        public void Initialize(string projectName, string projectDir, ModelCreationMode modelMode, string model)
        {
            ThreadPool.QueueUserWorkItem(o =>
                {
                    OnInitialization();

                    Guid id = Guid.NewGuid();

                    this.projectDir = projectDir;
                    Project = new Project(projectName, id);
                    Project.Save(projectDir);
                    SaveProjectToSettings();

                    tree = new ExperimentsTree(TreeFileName, id);

                    bool pack = false;

                    switch (modelMode)
                    {
                        case ModelCreationMode.cmCreateNew:

                            pack = DirectoryPacker.Pack(ModelDir, SnapshotDataFileName, id);

                            CreateModel(model);
                            break;
                        case ModelCreationMode.cmClone:

                            pack = DirectoryPacker.Pack(model, SnapshotDataFileName, id);
                            if (pack) DirectoryPacker.UnPack(ModelDir, SnapshotDataFileName, id);

                            LoadModel();
                            break;
                    }

                    Experiment.Create(id, ExperimentFileName, MakeBackup(id), pack);

                    OnInitialized();
                });
        }

        //private void CreateExperiment(string projectDir, Guid id)
        //{
        //    this.experiment = new Experiment(id);
        //    experiment.Save(Path.Combine(projectDir, project.Files.ExperimentFileName));
        //}

        //public void LoadExperiment(Guid id)
        //{
        //    experiment = Experiment.Load(ExperimentFileName, id);
        //}

        //public void CreateNewProject(string projectName, string projectDir, string modelName)
        //{
            //vissim.Instance.New();
            //vissim.Instance.Net.Name = modelName;

            //CreateProject(projectName, projectDir);

            //string modelDir = Path.Combine(projectDir, project.Files.ModelDirectory);
            //if (!Directory.Exists(modelDir)) Directory.CreateDirectory(modelDir);

            //vissim.Instance.SaveNetAs(string.Format("{0}\\{1}{2}", modelDir, modelName,
            //    modelName.EndsWith(".inp") ? string.Empty : ".inp"));
            //vissim.Instance.SaveLayout(string.Format("{0}\\vissim.ini", modelDir));

            //tree = new ExperimentsTree(Path.Combine(projectDir, project.Files.SnapshotTreeFileName));

            //DirectoryPacker.Pack(
            //    modelDir,
            //    Path.Combine(projectDir, project.Files.SnapshotDataFileName),
            //    Tree.root.Id);

            //CreateExperiment(projectDir, Tree.root.Id);

            //project.CurrentExperimentId = Tree.root.Id;
            //project.Save(projectDir);

            //SaveProjectToSettings(project, projectDir);

            //OnProjectLoaded(string.Format("{0}\\{1}{2}", projectDir, projectName, project.FileExtension));
        //}

        //public void CloneProject(string projectName, string projectDir, string modelFileName)
        //{
            //CreateProject(projectName, projectDir);

            //string modelName = string.IsNullOrEmpty(modelFileName) ? vissim.Instance.GetInputFileName() : Path.GetFileName(modelFileName);
            //string modelDir = string.IsNullOrEmpty(modelFileName) ? vissim.Instance.GetWorkingDirectory() : Path.GetDirectoryName(modelFileName);
            //string snapshotDataFileName = string.Format("{0}\\{1}", projectDir, project.Files.SnapshotDataFileName);

            //tree = new ExperimentsTree(string.Format("{0}\\{1}", projectDir, project.Files.SnapshotTreeFileName));
            //DirectoryPacker.Pack(modelDir, snapshotDataFileName, Tree.root.Id);

            //modelDir = string.Format("{0}\\{1}", projectDir, project.Files.ModelDirectory);
            //if (!Directory.Exists(modelDir)) Directory.CreateDirectory(modelDir);
            //DirectoryPacker.UnPack(modelDir, snapshotDataFileName, Tree.root.Id);

            //vissim.Instance.LoadNet(string.Format("{0}\\{1}{2}", modelDir, modelName, modelName.EndsWith(".inp") ? string.Empty : ".inp"));

            //CreateExperiment(projectDir, Tree.root.Id);

            //project.CurrentExperimentId = Tree.root.Id;
            //project.Save(projectDir);

            //SaveProjectToSettings(project, projectDir);

            //OnProjectLoaded(string.Format("{0}\\{1}{2}", projectDir, project.Name, project.FileExtension));
        //}

        //public void LoadProject(string projectFileName)
        //{
        //    if (string.IsNullOrWhiteSpace(projectFileName)) return;

        //    if (!File.Exists(projectFileName))
        //    {
        //        if (LoadProjectFailed != null) LoadProjectFailed(this, new ProjectEventArgs(projectFileName));
        //        return;
        //    }

        //    projectDir = Path.GetDirectoryName(projectFileName);
        //    Project = Project.Load(projectFileName);
        //    experiment = Experiment.Load(Path.Combine(projectDir, project.Files.ExperimentFileName), project.CurrentExperimentId);

        //    string modelDir = Path.Combine(projectDir, project.Files.ModelDirectory);

        //    //TODO LASTPOINT
        //    if (vissim.Instance.GetWorkingDirectory() != string.Format("{0}\\", modelDir))
        //    {

        //        var filesList = Directory.GetFiles(modelDir, "*.inp");
        //        if (filesList.Length == 0) throw new FileNotFoundException("There are no input file in the model directory");

        //        string modelName = Path.GetFileName(filesList[0]);

        //        vissim.Instance.LoadNet(string.Format("{0}\\{1}{2}", modelDir, modelName,
        //            modelName.EndsWith(".inp") ? string.Empty : ".inp"));

        //        string layoutFileName = string.Format("{0}\\vissim.ini", modelDir);
        //        if (File.Exists(layoutFileName)) vissim.Instance.LoadLayout(layoutFileName);
        //    }

        //    SaveProjectToSettings(projectFileName);

        //    OnProjectLoaded(projectFileName);
        //}

        private bool MakeBackup(Guid id)
        {
#if (!DEBUG)
            var sb = new OleDbConnectionStringBuilder(vissim.Instance.Evaluation.Wrap().GetConnectionString());
            if (sb.ContainsKey("Password"))
            {
                return SQLAdmin.MakeBackup(null, id, ExperimentNumber,
                    sb.DataSource,
                    sb["Initial Catalog"].ToString(),
                    @"\\toshiba\English.Cafe.2008.MP3.64kbps\",
                    sb["User ID"].ToString(),
                    sb["Password"].ToString());
            }
            else return false;
#else
            return false;
#endif
        }

        private void TakeBackup(Guid id)
        {
#if (!DEBUG)
            var sb = new OleDbConnectionStringBuilder(vissim.Instance.Evaluation.Wrap().GetConnectionString());
            if (sb.ContainsKey("Password"))
            {
                SQLAdmin.MakeRestore(null, id,
                    sb.DataSource,
                    sb["Initial Catalog"].ToString(),
                    @"\\toshiba\English.Cafe.2008.MP3.64kbps\",
                    sb["User ID"].ToString(),
                    sb["Password"].ToString());
            }
#endif
        }

        public void MakeSnapshot()
        {
            SaveModel();

            Guid snapshotId = LastSnapshotId;

            var node = Tree.root.FindNode(snapshotId);
            Guid id = node.AddNewChild();
            Tree.Save(TreeFileName);

            Experiment.Create(
                id, 
                ExperimentFileName, 
                MakeBackup(id),
                DirectoryPacker.Pack(ModelDir, SnapshotDataFileName, id));

            LastSnapshotId = id;

            SaveProject();
        }

        public void TakeSnapshot(ExperimentsTree.ExperimentsTreeNode node)
        {
            node.Open();
            node.Load();
            Tree.Save(TreeFileName);

            if (Experiment.GetInstance(node.Id).HasSnapshot) Experiment.GetInstance(node.Id).HasSnapshot = DirectoryPacker.UnPack(ModelDir, SnapshotDataFileName, node.Id);
            if (Experiment.GetInstance(node.Id).HasBackup) TakeBackup(node.Id);

            LastSnapshotId = node.Id;
            SaveProject();

            //string modelName = vissim.Instance.GetInputFileName();
            //string modelDir = vissim.Instance.GetWorkingDirectory();

            LoadModel();
        }

        public void BrakeSnapshot(ExperimentsTree.ExperimentsTreeNode node)
        {

        }

        //private void SaveProjectToSettings(string projectFileName)
        //{
        //    Settings.Default.currentProject = projectFileName;
        //    Settings.Default.Save();
        //}
    }
}
