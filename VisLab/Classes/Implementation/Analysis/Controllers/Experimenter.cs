using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Controls;
using System.ComponentModel;

using vissim = VisLab.Classes.Integration.VissimSingleton;
using VisLab.Classes.Implementation.Design.Generics;
using VisLab.Classes.Implementation.Entities;
using VisLab.Classes.Integration.Utilities;
using VisLab.Classes.Integration.Extensions;
using VisLab.Classes.Implementation.Utilities;
using VisLab.Classes.Implementation.Design.BindingSources;
using VisLab.Classes.Integration.Wrappers;
using VisLab.Classes.Implementation.Design;
using VisLab.Classes.Implementation.Design.Utilities;
using VisLab.Classes.Implementation.Design.Interfaces;

namespace VisLab.Classes.Implementation.Analysis.Controllers
{
    public class SimulationState : INotifyPropertyChanged
    {
        private bool isSimulationRunning;
        public bool IsSimulationRunning
        {
            get { return isSimulationRunning; }
            set
            {
                isSimulationRunning = value;
                OnPropertyChanged("IsSimulationRunning");
            }
        }

        public bool SimulationWasStopped { get; set; } 

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Experimenter : IDogged
    {
        private SimulationState simulationState = new SimulationState();

        public string modelName { get; private set; }
        private ProjectManager manager;
        private AsyncExceptionHandler aex;

        public Experimenter(string modelName, ProjectManager manager, AsyncExceptionHandler exceptionHandler)
        {
            this.aex = exceptionHandler;
            this.modelName = modelName;
            this.manager = manager;
        }

        /// <summary>
        /// Creates a new experiment folder,
        /// copy files to it from model dir.
        /// and marks it as loaded and selected in the project file
        /// </summary>
        public void CreateExperiment(Experiment.ExperimentData expData)
        {
            // get max experiment number
            int max = SysAdmin.GetMaxDirNumber(manager.GetRootExperimentPath(modelName));
            ++max;
            // create new experiment folder
            string s = manager.GetLoadedExperimentAbsolutePath(modelName);

            string experimentPath = string.Format("{0}#{1}", s, max);
            if (max == 0) experimentPath = string.Format("{0}{1}#{2}", s, modelName, max);
            
            var di = Directory.CreateDirectory(string.Format("{0}\\{1}{2}", experimentPath, modelName, Model.MODEL_DIR_POSTFIX));
            // copy main model folder to the new model folder
            s = manager.GetModelDir(modelName);
            SysAdmin.CopyDirectory(s, di.FullName);

            // create experiment data file in the experiment folder
            s = string.Format("{0}\\{1}{2}", experimentPath, modelName, Experiment.ExperimentData.EXPERIMENT_DATA_POSTFIX);
            expData.Save(s);
            // mark experiment as loaded
            manager.MarkExperimentAsLoaded(modelName, experimentPath);
            manager.MarkExperimentAsSelected(modelName, experimentPath);
        }

        public void LoadExperiment(Experiment exp)
        {
            LoadExperiment(exp.Path);
        }

        private void LoadExperiment(string expPath)
        {
            manager.MarkExperimentAsLoaded(modelName, expPath);

            string expDir = string.Format(@"{0}\{1}{2}", expPath, modelName, Model.MODEL_DIR_POSTFIX);
            string modelDir = manager.GetModelDir(modelName);
            SysAdmin.CopyDirectory(expDir, modelDir);

            LoadModelToVissim(false);
        }

        public void SelectExperiment(Experiment exp)
        {
            manager.MarkExperimentAsSelected(modelName, exp.Path);
        }

        public bool DeleteExperiment(Experiment exp)
        {
            bool result = false;

            exp.DeAnalyze(true);

            if (exp.IsRoot) result = manager.DeleteModel(modelName);
            else
            {
                string
                    rootPath = manager.GetRootExperimentPath(modelName),
                    expPath = exp.Path.Replace(rootPath, ""),
                    loadedPath = manager.GetLoadedExperimentRelativePath(modelName),
                    selectedPath = manager.GetSelectedExperimentRelativePath(modelName),
                    parentPath = Path.GetDirectoryName(exp.Path);

                // loaded path -> if exp belongs to loaded path -> exp.parent
                if (loadedPath.Contains(expPath)) LoadExperiment(parentPath);

                // selected path -> exp.parent
                if (selectedPath.Contains(expPath))
                {
                    manager.MarkExperimentAsSelected(modelName, parentPath);
                }

                //exp.DeAnalyze(true);

                bool isDeleted = false;
                while (!isDeleted)
                {
                    try
                    {
                        Directory.Delete(exp.Path, true);
                        isDeleted = true;
                    }
                    catch (Exception ex)
                    {
                        isDeleted = !OnAttempt(ex.Message + "\nDo you want to try this action again?", false);
                    }
                }

                result = true;
            }

            return result;
        }

        #region VISSIM

        public void LoadModelToVissim(bool forceLoadVissim)
        {
            if (forceLoadVissim || vissim.IsInstanciated)
            {
                string net = manager.GetModelInputFileName(modelName);
                string layout = manager.GetModelSettingsFileName(modelName);

                aex.Protect(() => vissim.Instance.LoadNet(net));
                aex.Protect(() => vissim.Instance.LoadLayout(layout));
            }
        }

        public void SaveModelToVissim()
        {
            string net = manager.GetModelInputFileName(modelName);
            string layout = manager.GetModelSettingsFileName(modelName);

            aex.Protect(() => vissim.Instance.SaveNetAs(net));
            aex.Protect(() => vissim.Instance.SaveLayout(layout));
        }

        public void CloseVissimIfModelLoaded()
        {
            if (vissim.IsInstanciated && vissim.Instance.Wrap().InputFileName.Contains(modelName))
                vissim.Instance.Exit();
        }

        public void CreateModelInVissim()
        {
            string modelDir = manager.GetModelDir(modelName);

            if (!Directory.Exists(modelDir)) Directory.CreateDirectory(modelDir);

            aex.Protect(() => vissim.Instance.New());
            vissim.Instance.Net.Name = modelName;

            SaveModelToVissim();
        }

        #endregion

        public void CreateRootExperiment()
        {
            // create new experiment folder
            var di = Directory.CreateDirectory(manager.GetLoadedExperimentModelDir(modelName));
            
            // copy main model folder to new model folder
            string s = manager.GetModelDir(modelName);
            SysAdmin.CopyDirectory(s, di.FullName);

            // create experiment data file in the experiment folder
            var expData = new Experiment.ExperimentData()
            {
                //Alanyze = false,
                CreatedOn = DateTime.Now,
                Description = "Zero Experiment (created automatically)",
                //Duration = new TimeSpan(0),
                //DurationTicks = 0
            };

            expData.Save(manager.GetLoadedExperimentDataFileName(modelName));
        }

        public void StopSimulation()
        {
            if (vissim.IsInstanciated)
            {
                simulationState.SimulationWasStopped = true;
                aex.Protect(() => vissim.Instance.Simulation.Stop());
            }
        }

        public void PauseSimulation()
        {
            if (vissim.IsInstanciated) aex.Protect(() => vissim.Instance.Simulation.RunSingleStep());
        }

        public void DrawExperiment(Canvas canvas)
        {
            string rootTreePath = (manager as IClerk).GetRootExperimentPath(modelName);
            string selectedExperimentPath = (manager as IClerk).GetSelectedExperimentRelativePath(modelName);
            string loadedExperimentPath = (manager as IClerk).GetLoadedExperimentRelativePath(modelName);

            TreePlotter.DrawTree(canvas, rootTreePath, selectedExperimentPath, loadedExperimentPath);
        }

        public bool CheckDataCollectionExists()
        {
            return vissim.IsInstanciated ? vissim.Instance.Net.DataCollections.Count > 0 : false;
        }

        public bool CheckCountersEvaluationEnabled()
        {
            // get .ini full path for model
            string settingsFileName = manager.GetModelSettingsFileName(modelName);
            string iniText = File.ReadAllText(settingsFileName);

            // get data Collection Evaluation (.ini)
            var evalMask = TextProcessor.GetVissimOptions(iniText);

            // if evaluation is not enabled - return true
            return (evalMask.HasFlag(VissimIniOptions.DataCollectionEvaluation));   
        }

        public void EnableCountersEvaluation()
        {
            // get .ini full path for model
            string settingsFileName = manager.GetModelSettingsFileName(modelName);
            var iniText = new StringBuilder(File.ReadAllText(settingsFileName));

            TextProcessor.SetVissimOptions(iniText, VissimIniOptions.DataCollectionEvaluation);

            File.WriteAllText(settingsFileName, iniText.ToString());
            if (vissim.IsInstanciated) aex.Protect(() => vissim.Instance.LoadLayout(settingsFileName));
        }

        #region Async

        public void LoadModelAsync(bool loadVissim, Dispatcher context, Action finalAction)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                aex.Protect(() =>
                    {
                        LoadModelToVissim(loadVissim);

                        context.Invoke(DispatcherPriority.Background,
                        new ThreadStart(delegate()
                        {
                            finalAction();
                        }));
                    });
            });
        }

        public void RunMultiAsync(StartMultirunBindingSource parameters, Dispatcher context, Func<bool> startAction, Action<Experiment.ExperimentData> endAction, Action<string> stateChangedAction)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                aex.Protect(() =>
                    {
                        // load model if VISSIM is not instanciated or save model otherwise
                        if (!vissim.IsInstanciated)
                        {
                            LoadModelToVissim(true);
                        }
                        else SaveModelToVissim();

                        // perform prep. actions (ask user for DataCollections, Evaluations and etc.)
                        bool canContinue = true;
                        context.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
                        {
                            canContinue = startAction();
                        }));
                        if (!canContinue) return;

                        var gr = vissim.Instance.Graphics.Wrap();
                        bool wasVisualizationEnabled = gr.IsVisualizationEnabled;
                        if (!gr.IsVisualizationEnabled ^ parameters.DisableAnimation) gr.IsVisualizationEnabled = !parameters.DisableAnimation;

                        var sim = vissim.Instance.Simulation;

                        long startTicks = DateTime.Now.Ticks;

                        //sim.RunMulti();

                        int i = 0;

                        simulationState.IsSimulationRunning = true;
                        try
                        {
                            while (parameters.NumberOfRuns > i && !simulationState.SimulationWasStopped)
                            {
                                sim.RunIndex = i++;
                                sim.RandomSeed = (int)DateTime.Now.Ticks;
                                sim.Comment = string.Format("Simulation random seed = {0}", sim.RandomSeed);

                                context.Invoke(DispatcherPriority.Normal, new ThreadStart(delegate()
                                {
                                    stateChangedAction(string.Format("Simulation ...\nRun {0} of {1}", i, parameters.NumberOfRuns));
                                }));

                                aex.Protect(() => sim.RunContinuous());

                                aex.Protect(() => vissim.Instance.DoEvents());
                            }
                        }
                        finally
                        {
                            if (wasVisualizationEnabled != gr.IsVisualizationEnabled) gr.IsVisualizationEnabled = wasVisualizationEnabled;
                            simulationState.IsSimulationRunning = false;
                            if (simulationState.SimulationWasStopped) simulationState.SimulationWasStopped = false;
                        }

                        var data = new Experiment.ExperimentData()
                        {
                            CreatedOn = DateTime.Now,
                            Duration = new TimeSpan(DateTime.Now.Ticks - startTicks),
                            //Period = new TimeSpan(0, 0, (int)sim.Period * i),
                            IsCountersEvaluationEnabled = vissim.Instance.Evaluation.Wrap().IsDataCollectionsEnabled,
                            IsTravelTimesEvaluationEnabled = vissim.Instance.Evaluation.Wrap().IsTravelTimeEnabled,
                            NumberOfRuns = i
                        };

                        context.Invoke(DispatcherPriority.Background,
                            new ThreadStart(delegate()
                        {
                            endAction(data);
                        }));
                    });
            });
        }

        public void RunSimulationAsync(Dispatcher context, Func<bool> startAction, Action<Experiment.ExperimentData> endAction)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                aex.Protect(() =>
                    {
                        // load model if VISSIM is not instanciated or save model otherwise
                        if (!vissim.IsInstanciated)
                        {
                            LoadModelToVissim(true);
                        }
                        else SaveModelToVissim();

                        // perform prep. actions (ask user for DataCollections, Evaluations and etc.)
                        bool canContinue = true;
                        context.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
                        {
                            canContinue = startAction();
                        }));
                        if (!canContinue) return;

                        var sim = vissim.Instance.Simulation;

                        long startTicks = DateTime.Now.Ticks;
                        simulationState.IsSimulationRunning = true;

                        try
                        {
                            aex.Protect(() => sim.RunContinuous());
                        }
                        finally
                        {
                            simulationState.IsSimulationRunning = false;
                            if (simulationState.SimulationWasStopped) simulationState.SimulationWasStopped = false;
                        }

                        var data = new Experiment.ExperimentData()
                        {
                            CreatedOn = DateTime.Now,
                            Duration = new TimeSpan(DateTime.Now.Ticks - startTicks),
                            //Period = new TimeSpan(0, 0, (int)sim.Period),
                            IsCountersEvaluationEnabled = vissim.Instance.Evaluation.Wrap().IsDataCollectionsEnabled,
                            IsTravelTimesEvaluationEnabled = vissim.Instance.Evaluation.Wrap().IsTravelTimeEnabled,
                            NumberOfRuns = 1
                        };

                        context.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
                        {
                            endAction(data);
                        }));
                    });
            });
        }

        #endregion

        public SimulationState GetSimulationState()
        {
            return simulationState;
        }

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
