using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

using VisLab.Classes.Implementation.Utilities;
using VisLab.Classes.Implementation.Design.Utilities;
using VisLab.Classes.Integration.Utilities;
using VisLab.Classes.Implementation.Entities;
using VisLab.Classes.Implementation.Design.Generics;
using VisLab.Classes.Implementation.Design.Interfaces;
using VisLab.Classes.Implementation.Design;

namespace VisLab.Classes.Implementation.Analysis.Controllers
{
    public interface IClerk
    {
        string GetModelDir(string modelName);
        string GetModelInputFileName(string modelName);
        string GetModelSettingsFileName(string modelName);
        string GetRootExperimentPath(string modelName);
        string GetExperimentModelDirName(string modelName, int experimentNumber);
        string GetExperimentInputFileName(string modelName, int experimentNumber);
        string GetExperimentSettingsFileName(string modelName, int experimentNumber);
        string GetSelectedExperimentRelativePath(string modelName);
        string GetLoadedExperimentRelativePath(string modelName);
        string GetLoadedExperimentAbsolutePath(string modelName);
        string GetLoadedExperimentModelDir(string modelName);
        string GetLoadedExperimentDataFileName(string modelName);
        string GetRelativeExperimentPath(string fullExperimentPath, string modelName);
    }

    public class ProjectManager : INotifyPropertyChanged, IClerk, IDogged
    {
        #region Fields, Properties and Events
        private const int EXP_START_NUMBER = 0;
        private readonly Dispatcher defaultContext;
        private readonly AsyncExceptionHandler aex;
        private string ProjectDir { get; set; }

        private Dictionary<string, Experimenter> experimenters = new Dictionary<string,Experimenter>();

        private Project project;
        public Project Project
        {
            get { return project; }
            private set
            {
                project = value;
                OnPropertyChanged("project");
                OnPropertyChanged("HasProject");
            }
        }

        public bool HasProject { get { return Project != null; } }

        public event EventHandler<EventArgs<string>> ModelDeleted;
        private void OnModelDeleted(string modelName)
        {
            if (ModelDeleted != null) ModelDeleted(this, new EventArgs<string>(modelName)); 
        }
        #endregion

        #region Constructors

        public ProjectManager(Dispatcher disp, AsyncExceptionHandler exceptionHandler)
        {
            this.aex = exceptionHandler;
            this.defaultContext = disp;
        }

        #endregion

        #region Private Methods

        private string GetModelName(string modelDir)
        {
            string result = string.Empty;

            var filesList = Directory.GetFiles(modelDir, "*.inp");
            if (filesList.Length > 0)
            {
                result = Path.GetFileNameWithoutExtension(filesList[0]);
            }

            return result;
        }

        private string GetExperimentsDir()
        {
            return Path.Combine(ProjectDir, Model.EXPERIMENTS_DIR);
        }

        private string GetRootExperimentDir(string modelName)
        {
            return Path.Combine(GetExperimentsDir(), string.Format(@"{0}#0", modelName));
        }

        private string GetRelExperimentPath(string modelName, int expNumber)
        {
            return string.Format(@"{0}#{1}\", modelName, expNumber);
        }

        private void LoadModel(string modelName, bool loadVissim)
        {
            GetExperimenterForModel(modelName).LoadModelToVissim(loadVissim);
        }

        #endregion

        #region Project and Model Interop

        public void SaveProject()
        {
            if (Project != null) Project.Save(ProjectDir);
        }

        public Model GetModel(string modelName)
        {
            return Project.Models.Model.Where(m =>
            {
                return m.Name.ToUpper() == modelName.ToUpper();
            }).FirstOrDefault();
        }

        public void MarkExperimentAsSelected(string modelName, string experimentPath)
        {
            var model = GetModel(modelName);
            model.SelectedExperimentPath = GetRelativeExperimentPath(experimentPath, modelName);
        }

        public void MarkExperimentAsLoaded(string modelName, string experimentPath)
        {
            var model = GetModel(modelName);
            model.LoadedExperimentPath = GetRelativeExperimentPath(experimentPath, modelName);
        }

        #endregion

        #region Experimenters

        public Experimenter GetExperimenterForModel(string modelName)
        {
            if (!experimenters.ContainsKey(modelName)) experimenters[modelName] = new Experimenter(modelName, this, aex);
            return experimenters[modelName];
        }

        #endregion

        #region Main Responsibilities

        public void CreateNewProject(string projectName, string projectDir, string modelName)
        {
            this.ProjectDir = projectDir;

            Project = new Project(projectName);

            AddNewModel(modelName);
        }

        public string CloneProject(string projectName, string projectDir, string modelPath)
        {
            this.ProjectDir = projectDir;

            Project = new Project(projectName);

            string modelName = Path.GetFileNameWithoutExtension(modelPath); //GetModelName(modelPath);
            string modelDir = Path.GetDirectoryName(modelPath);
            string modelDir2 = GetModelDir(modelName);
            SysAdmin.CopyDirectory(modelDir, modelDir2, modelName);

            defaultContext.Invoke(DispatcherPriority.Background, 
                new ThreadStart(delegate()
            {
                Project.Models.Model.Add(new Model()
                {
                    Name = modelName,
                    LoadedExperimentPath = GetRelExperimentPath(modelName, EXP_START_NUMBER),
                    SelectedExperimentPath = GetRelExperimentPath(modelName, EXP_START_NUMBER)
                });
            }));
            LoadModel(modelName, false);

            Project.CurrentModelName = modelName;
            Project.Save(projectDir);

            // create experiment
            GetExperimenterForModel(modelName).CreateRootExperiment();

            return modelName;
        }

        public string OpenProject(string projectFileName)
        {
            this.ProjectDir = Path.GetDirectoryName(projectFileName);

            Project = Project.Load(projectFileName);

            string modelName = Project.CurrentModelName;

            GetExperimenterForModel(modelName).LoadModelToVissim(false);

            return modelName;
        }

        private bool AddNewModel(string modelName)
        {
            bool result = false;

            if (Project != null)
            {
                if (Project.Models.Model.Count(m => { return m.Name.ToUpper() == modelName.ToUpper(); }) == 0)
                {
                    defaultContext.Invoke(DispatcherPriority.Background, 
                        new ThreadStart(delegate()
                    {
                        Project.Models.Model.Add(new Model()
                        {
                            Name = modelName,
                            LoadedExperimentPath = GetRelExperimentPath(modelName, EXP_START_NUMBER),
                            SelectedExperimentPath = GetRelExperimentPath(modelName, EXP_START_NUMBER),
                        });
                    }));

                    Project.CurrentModelName = modelName;
                    Project.Save(ProjectDir);

                    var exp = GetExperimenterForModel(modelName);

                    exp.CreateModelInVissim();

                    //SysAdmin.CopyDirectory(GetModelDir(modelName), GetAbsExperimentPath(modelName, EXP_START_NUMBER));
                    exp.CreateRootExperiment();

                    result = true;
                }
            }

            return result;
        }

        public void AddNewModelAsync(string modelName, Dispatcher context, UserControl transitControl, Action<bool, string, UserControl> finalAction)
        {
            ThreadPool.QueueUserWorkItem(o =>
                {
                    aex.Protect(() =>
                    {
                        bool result = AddNewModel(modelName);

                        context.Invoke(DispatcherPriority.Background,
                            new ThreadStart(delegate()
                            {
                                finalAction(result, modelName, transitControl);
                            }));
                    });
                });
        }

        public void AddExistingModelAsync(string modelFileName, string altModelName, Dispatcher context, UserControl transitControl, Action doubledModelNameAction, Action<bool, string, UserControl> finalAction)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                aex.Protect(() =>
                    {
                        bool result = false;

                        if (Project != null)
                        {
                            string modelName = string.IsNullOrWhiteSpace(altModelName)
                                ? Path.GetFileNameWithoutExtension(modelFileName)
                                : altModelName;

                            if (Project.Models.Model.Count(m => { return m.Name.ToUpper() == modelName.ToUpper(); }) == 0)
                            {
                                string modelDir2 = Path.GetDirectoryName(modelFileName);
                                string modelDir = GetModelDir(modelName);

                                SysAdmin.CopyDirectory(modelDir2, modelDir, modelName);

                                if (!string.IsNullOrWhiteSpace(altModelName))
                                    SysAdmin.ReplaceFileNamesInDirectory(modelDir, Path.GetFileNameWithoutExtension(modelFileName), modelName);

                                context.Invoke(DispatcherPriority.Background,
                                    new ThreadStart(delegate()
                                {
                                    Project.Models.Model.Add(new Model()
                                    {
                                        Name = modelName,
                                        LoadedExperimentPath = GetRelExperimentPath(modelName, EXP_START_NUMBER),
                                        SelectedExperimentPath = GetRelExperimentPath(modelName, EXP_START_NUMBER)
                                    });
                                }));

                                LoadModel(modelName, false);

                                Project.CurrentModelName = modelName;
                                Project.Save(ProjectDir);

                                // create experiment
                                GetExperimenterForModel(modelName).CreateRootExperiment();

                                result = true;
                            }
                            else doubledModelNameAction();

                            context.Invoke(DispatcherPriority.Background,
                                new ThreadStart(delegate()
                            {
                                finalAction(result, modelName, transitControl);
                            }));
                        }
                    });
            });
        }

        public bool DeleteModel(string modelName)
        {
            bool result = false;

            // if model not last
            if (Project.Models.Model.Count > 1)
            {
                // if we can get model to remove
                var modelToRemove = Project.Models.Model.Where(m => m.Name.ToUpper() == modelName.ToUpper()).FirstOrDefault();
                if (modelToRemove != null)
                {
                    // if model loaded to VISSIM -> close VISSIM to release files before deleting
                    if (Project.CurrentModelName.ToUpper() == modelName.ToUpper())
                    {
                        var experimenter = GetExperimenterForModel(modelName);
                        experimenter.CloseVissimIfModelLoaded();
                    }

                    // delete model dir -> modelName.model
                    bool isDeleted = false;
                    while (!isDeleted)
                    {
                        try
                        {
                            Directory.Delete(GetModelDir(modelName), true);
                            isDeleted = true;
                        }
                        catch (Exception ex)
                        {
                            isDeleted = !OnAttempt(ex.Message + "\nDo you want to try this action again?", false);
                        }
                    }


                    Directory.Delete(GetRootExperimentDir(modelName), true);

                    // delete model record from modelName.vislab
                    Project.Models.Model.Remove(modelToRemove);
                    Project.Save(ProjectDir);

                    OnModelDeleted(modelName);
                    result = true;
                }
            }

            return result;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        #region IClerc

        public string GetModelDir(string modelName)
        {
            return string.Format(@"{0}\{1}{2}", ProjectDir, modelName, Model.MODEL_DIR_POSTFIX);
        }

        public string GetModelInputFileName(string modelName)
        {
            return Path.Combine(GetModelDir(modelName), string.Format("{0}.inp", modelName));
        }

        public string GetModelSettingsFileName(string modelName)
        {
            return Path.Combine(GetModelDir(modelName), "vissim.ini");
        }

        public string GetRootExperimentPath(string modelName)
        {
            return string.Format(@"{0}\{1}\{2}#0", ProjectDir, Model.EXPERIMENTS_DIR, modelName);
        }

        public string GetExperimentModelDirName(string modelName, int experimentNumber)
        {
            string root = GetRootExperimentPath(modelName);

            if (experimentNumber > 0)
            {
                root = Directory.GetDirectories(root, "#" + experimentNumber, SearchOption.AllDirectories).FirstOrDefault();
            }

            return Path.Combine(root, modelName + Model.MODEL_DIR_POSTFIX);
        }

        public string GetExperimentInputFileName(string modelName, int experimentNumber)
        {
            string expPath = GetExperimentModelDirName(modelName, experimentNumber);

            return string.Format(@"{0}\{1}.inp", expPath, modelName);
        }

        public string GetExperimentSettingsFileName(string modelName, int experimentNumber)
        {
            string expPath = GetExperimentModelDirName(modelName, experimentNumber);

            return string.Format(@"{0}\vissim.ini", expPath);
        }

        public string GetSelectedExperimentRelativePath(string modelName)
        {
            var model = GetModel(modelName);

            if (model != null) return model.SelectedExperimentPath;
            else return string.Empty;
        }

        public string GetLoadedExperimentRelativePath(string modelName)
        {
            var model = GetModel(modelName);

            if (model != null) return model.LoadedExperimentPath;
            else return string.Empty;
        }

        public string GetLoadedExperimentAbsolutePath(string modelName)
        {
            return string.Format(@"{0}\{1}\{2}"
                , ProjectDir
                , Model.EXPERIMENTS_DIR
                , GetLoadedExperimentRelativePath(modelName));
        }

        public string GetLoadedExperimentModelDir(string modelName)
        {
            return string.Format("{0}\\{1}{2}"
                , GetLoadedExperimentAbsolutePath(modelName)
                , modelName
                , Model.MODEL_DIR_POSTFIX);
        }

        public string GetLoadedExperimentDataFileName(string modelName)
        {
            return string.Format("{0}\\{1}{2}"
                , GetLoadedExperimentAbsolutePath(modelName)
                , modelName
                , Experiment.ExperimentData.EXPERIMENT_DATA_POSTFIX);
        }

        public string GetRelativeExperimentPath(string fullExperimentPath, string modelName)
        {
            return fullExperimentPath.Substring(fullExperimentPath.IndexOf(modelName + "#0")) + "\\";
        }

        #endregion

        #region IDogged

        public event EventHandler<EventArgs<string, bool>> Attempt;

        private bool OnAttempt(string message, bool permission)
        {
            var e = new EventArgs<string, bool>(message, permission);

            if (Attempt != null) Attempt(this, e);

            return e.Param;
        }

        #endregion
    }
}
