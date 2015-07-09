using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using vissim = VisLab.Classes.VissimSingleton;
using VisLab.Properties;
using System.ComponentModel;

namespace VisLab.Classes
{

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
        private string projectDir;
        public string ProjectDir { get { return projectDir; } }

        private Project project;
        private Project Project
        {
            get { return project; }
            set
            {
                project = value;
                OnPropertyChanged("HasProject");
                OnPropertyChanged("ProjectName");
                OnPropertyChanged("LastSnapshotId");
            }
        }

        private Experiment experiment;
        public Experiment Experiment { get { return experiment; } }

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

        //public string ProjectFileName
        //{
        //    get
        //    {
        //        string fileName = string.Empty;
        //        if (HasProject) fileName = string.Format("{0}\\{1}{2}", projectDir, project.Name, project.FileExtension);

        //        return fileName;
        //    }
        //}

        public string SnapshotTreeFileName
        {
            get
            {
                string fileName = string.Empty;
                if (HasProject) fileName = Path.Combine(projectDir, project.Files.SnapshotTreeFileName);

                return fileName;
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

                    OnPropertyChanged("LastSnapshotId");
                }
            }
        }

        public event EventHandler<ProjectEventArgs> LoadProjectFailed;
        public event EventHandler<ProjectEventArgs> ProjectLoaded;

        public ProjectManager() { }

        private void CreateProject(string projectName, string projectDir)
        {
            this.projectDir = projectDir;
            Project = new Project(projectName);
        }

        private void CreateExperiment(string projectDir, Guid id)
        {
            this.experiment = new Experiment(id);
            experiment.Save(Path.Combine(projectDir, project.Files.ExperimentFileName));
        }

        public void LoadExperiment(Guid id)
        {
            experiment = Experiment.Load(ExperimentFileName, id);
        }

        public void CreateNewProject(string projectName, string projectDir, string modelName)
        {
            vissim.Instance.New();
            vissim.Instance.Net.Name = modelName;

            CreateProject(projectName, projectDir);

            string modelDir = Path.Combine(projectDir, project.Files.ModelDirectory);
            if (!Directory.Exists(modelDir)) Directory.CreateDirectory(modelDir);

            vissim.Instance.SaveNetAs(string.Format("{0}\\{1}{2}", modelDir, modelName,
                modelName.EndsWith(".inp") ? string.Empty : ".inp"));
            vissim.Instance.SaveLayout(string.Format("{0}\\vissim.ini", modelDir));

            var tree = new ExperimentsTree(Path.Combine(projectDir, project.Files.SnapshotTreeFileName));
            DirectoryPacker.Pack(
                modelDir,
                Path.Combine(projectDir, project.Files.SnapshotDataFileName),
                tree.root.Id);

            CreateExperiment(projectDir, tree.root.Id);

            project.CurrentExperimentId = tree.root.Id;
            project.Save(projectDir);

            SaveProjectToSettings(project, projectDir);

            OnProjectLoaded(string.Format("{0}\\{1}{2}", projectDir, projectName, project.FileExtension));
        }

        public void CloneProject(string projectName, string projectDir, string modelFileName)
        {
            CreateProject(projectName, projectDir);

            string modelName = string.IsNullOrEmpty(modelFileName) ?
                vissim.Instance.GetInputFileName() : Path.GetFileName(modelFileName);
            string modelDir = string.IsNullOrEmpty(modelFileName) ?
                vissim.Instance.GetWorkingDirectory() : Path.GetDirectoryName(modelFileName);
            string snapshotDataFileName = string.Format("{0}\\{1}", projectDir, project.Files.SnapshotDataFileName);

            var tree = new ExperimentsTree(string.Format("{0}\\{1}", projectDir, project.Files.SnapshotTreeFileName));
            DirectoryPacker.Pack(modelDir, snapshotDataFileName, tree.root.Id);

            modelDir = string.Format("{0}\\{1}", projectDir, project.Files.ModelDirectory);
            if (!Directory.Exists(modelDir)) Directory.CreateDirectory(modelDir);
            DirectoryPacker.UnPack(modelDir, snapshotDataFileName, tree.root.Id);

            vissim.Instance.LoadNet(string.Format("{0}\\{1}{2}", modelDir, modelName,
                modelName.EndsWith(".inp") ? string.Empty : ".inp"));

            CreateExperiment(projectDir, tree.root.Id);

            project.CurrentExperimentId = tree.root.Id;
            project.Save(projectDir);

            SaveProjectToSettings(project, projectDir);

            OnProjectLoaded(string.Format("{0}\\{1}{2}", projectDir, project.Name, project.FileExtension));
        }

        public void LoadProject(string projectFileName)
        {
            if (string.IsNullOrWhiteSpace(projectFileName)) return;

            if (!File.Exists(projectFileName))
            {
                if (LoadProjectFailed != null) LoadProjectFailed(this, new ProjectEventArgs(projectFileName));
                return;
            }

            projectDir = Path.GetDirectoryName(projectFileName);
            Project = Project.Load(projectFileName);
            experiment = Experiment.Load(Path.Combine(projectDir, project.Files.ExperimentFileName), project.CurrentExperimentId);

            string modelDir = Path.Combine(projectDir, project.Files.ModelDirectory);

            if (vissim.Instance.GetWorkingDirectory() != string.Format("{0}\\", modelDir))
            {

                var filesList = Directory.GetFiles(modelDir, "*.inp");
                if (filesList.Length == 0) throw new FileNotFoundException("There are no input file in the model directory");

                string modelName = Path.GetFileName(filesList[0]);

                vissim.Instance.LoadNet(string.Format("{0}\\{1}{2}", modelDir, modelName,
                    modelName.EndsWith(".inp") ? string.Empty : ".inp"));

                string layoutFileName = string.Format("{0}\\vissim.ini", modelDir);
                if (File.Exists(layoutFileName)) vissim.Instance.LoadLayout(layoutFileName);
            }

            SaveProjectToSettings(projectFileName);

            OnProjectLoaded(projectFileName);
        }

        public void MakeSnapshot()
        {
            Guid snapshotId = LastSnapshotId;

            var tree = ExperimentsTree.Load(SnapshotTreeFileName);
            var node = tree.root.FindNode(snapshotId);
            Guid id = node.AddNewChild();
            tree.Save(SnapshotTreeFileName);

            experiment.Id = id;
            experiment.Save(Path.Combine(projectDir, project.Files.ExperimentFileName));
            if (Settings.Default.ResetNewExperiment) experiment.Reset();

            DirectoryPacker.Pack(ModelDir, SnapshotDataFileName, id);

            //SQLAdmin.MakeBackup(null, id, experiment.Number, @"TRANQUILLITY-PC\SQLEXPRESS",
            //    "VISSIM", @"\\tranquillity-pc\Temp\", "sa", "Macciato777");

            LastSnapshotId = id;
            SaveProject();
        }

        public void SaveProject()
        {
            project.Save(projectDir);
        }

        private void SaveProjectToSettings(string projectFileName)
        {
            Settings.Default.currentProject = projectFileName;
            Settings.Default.Save();
        }

        private void SaveProjectToSettings(Project pr, string projectDir)
        {
            if (pr != null)
            {
                Settings.Default.currentProject = string.Format("{0}\\{1}{2}", projectDir, pr.Name, pr.FileExtension);
                Settings.Default.Save();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void OnProjectLoaded(string ProjectFileName)
        {
            //string projectDir = Path.GetDirectoryName(ProjectFileName);
            //string modelDir = string.Format("{0}\\{1}", projectDir, Project.Files.ModelDirectory);

            this.projectDir = Path.GetDirectoryName(ProjectFileName);

            if (ProjectLoaded != null) ProjectLoaded(project, new ProjectEventArgs(ProjectFileName));
        }

        private void OnProjectCreated()
        {

        }
    }
}
