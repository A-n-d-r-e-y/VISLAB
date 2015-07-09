using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Threading;
using System.Collections.Specialized;
using System.Windows.Interop;
using System.IO;
using System.Windows.Media.Animation;

using VisLab.Controls;
using VisLab.Classes;
using VisLab.Properties;
using VisLab.Forms;
using vissim = VisLab.Classes.VissimSingleton;


namespace VisLab.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TreeControl treeCanvas1;
        private ModelControl modelCanvas1;
        private LoadingControl loadingControl;
        public LoadingControl LoadingControl
        {
            get
            {
                if (loadingControl == null) loadingControl = new LoadingControl();
                return loadingControl;
            }
        }
        private ShadingControl shadingControl;
        public ShadingControl ShadingControl
        {
            get
            {
                if (shadingControl == null)
                {
                    shadingControl = new ShadingControl();
                    shadingControl.Content = ProjectControl;
                }

                return shadingControl;
            }
        }
        private ProjectControl projectControl;
        public ProjectControl ProjectControl
        {
            get
            {
                if (projectControl == null)
                {
                    projectControl = new ProjectControl();
                    projectControl.btnCreateProject.Click += ShadowButton_CreateProject_Click;
                    projectControl.btnOpenProject.Click += ShadowButton_OpenProject_Click;
                }
                return projectControl;
            }
        }

        private readonly ProjectManager pm;
        private SliderPositions sliderPosition = SliderPositions.spTree;

        public void AddValueToRecentProjectsList(string value)
        {
            //var item = mntRecent.DropDownItems.Add(value);
            //item.Name = value;
            //item.Click += (sender, e) =>
            //{
            //    string projectFileName = (sender as ToolStripItem).Text;

            //    pm.LoadProject(projectFileName);
            //};
        }

        public MainWindow()
        {
            InitializeComponent();

            if (Settings.Default.recentProjectsList == null) Settings.Default.recentProjectsList = new StringCollection();
            foreach (string item in Settings.Default.recentProjectsList)
            {
                AddValueToRecentProjectsList(item);
            }

            pm = new ProjectManager(this.Dispatcher);
            pm.Initialized += pm_Initialized;
            pm.Initialization += pm_Initialization;

            //pm.LoadProjectFailed += (sender, e) =>
            //{
            //    if (Settings.Default.recentProjectsList.Contains(e.ProjectFileName))
            //    {
            //        if (MessageBox.Show(this,
            //            string.Format("Project file {0}\ndoes not exist anymore.\nDelete it from recent projects list?", e.ProjectFileName),
            //            "Project file missing",
            //            MessageBoxButton.YesNo,
            //            MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //        {
            //            Settings.Default.recentProjectsList.Remove(e.ProjectFileName);

            //            //if (mntRecent.DropDownItems.ContainsKey(e.ProjectFileName))
            //            //    mntRecent.DropDownItems.RemoveByKey(e.ProjectFileName);

            //            if (Settings.Default.currentProject == e.ProjectFileName) Settings.Default.currentProject = string.Empty;
            //        }
            //    }
            //};
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Приложение запускается НЕ в первый раз
            if (!string.IsNullOrWhiteSpace(Settings.Default.currentProject))
            {
                // Если установлена опция “запускать последний проект при старте”
                if (Settings.Default.OpenProjectOnStartup)
                {
                    // Если запомненный проект ещё существует
                    if (File.Exists(Settings.Default.currentProject))
                    {
                        pm.Initialize(Settings.Default.currentProject);
                    }
                    // Если уже не существует
                    else
                    {
                        //TODO copy code from old project
                        LoadShadingControl();
                    }
                }
                // Если НЕ установлена опция “запускать последний проект при старте”
                else
                {
                    LoadShadingControl();
                }           
            }
            // Приложение запускается в первый раз
            else
            {
                LoadShadingControl();
            }


            //// тут должен быть загружн проект
            //if (Settings.Default.OpenProjectOnStartup)
            //    if (!string.IsNullOrWhiteSpace(Settings.Default.currentProject))
            //        pm = new ProjectManager(Settings.Default.currentProject);
            //    else
            //    {
            //        miOpen_Click(miOpen, null);
            //    }

            //// если проект незагрузился - создаём новый
            //if (!pm.HasProject)
            //{
            //    var bc = new ButtonControl();
            //    bc.Content = "Hello from Mars!!!";
            //    bc.Click += new RoutedEventHandler(ButtonControl_Click);
            //    var sc = new ShadingControl();
            //    sc.Content = bc;
            //    Panel.SetZIndex(ccControlsHost, 606);
            //    ccControlsHost.Content = sc;
            //}
            //else
            //{
            //    lblExperiment.DataContext = pm.Experiment;
            //    //lblExperiment.Content = string.Format("Exp. Nr.{0}", pm.ExperimentNumber);
            //    LoadTreeControl();
            //}

        }

        void pm_Initialized(object sender, EventArgs e)
        {
            if (Panel.GetZIndex(ccControlsHost) > 500) Panel.SetZIndex(ccControlsHost, 500);

            lblExperiment.DataContext = (sender as ProjectManager);
            LoadTreeControl();
            treeCanvas1.stModel.ScaleX = treeCanvas1.stModel.ScaleY = (this.ActualHeight * 0.003);
        }

        void pm_Initialization(object sender, EventArgs e)
        {
            if (ccControlsHost.Content != ShadingControl) LoadShadingControl();

            LoadLoadingControl();
        }

        private void LoadModelControl()
        {
            var net = VissimSingleton.Instance.Net;
            if (modelCanvas1 == null)
            {
                modelCanvas1 = new ModelControl();
                net.Wrap().Draw(modelCanvas1.cnvModel, System.Windows.SystemColors.WindowTextBrush);
            }

            ccControlsHost.Content = modelCanvas1;
        }

        private void LoadTreeControl()
        {
            if (treeCanvas1 == null)
            {
                treeCanvas1 = new TreeControl(pm);
            }
            else treeCanvas1.Refresh();

            ccControlsHost.Content = treeCanvas1;
        }

        private void LoadLoadingControl()
        {
            (ccControlsHost.Content as ShadingControl).Content = LoadingControl;
            (ccControlsHost.Content as ShadingControl).ReleaseShadow();
        }

        private void LoadExperimentsControl()
        {
            Label lbl = new Label();
            lbl.Content = "Under Construction!!!";
            lbl.FontSize = 50;
            lbl.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            lbl.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            ccControlsHost.Content = lbl;
        }

        private void LoadShadingControl()
        {
            Panel.SetZIndex(ccControlsHost, 606);
            ccControlsHost.Content = ShadingControl;
        }

        private void LoadProjectControl()
        {
            ccControlsHost.Content = ProjectControl;
        }

        private void LeftArrowControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (sliderPosition)
            {
                case SliderPositions.spTree:
                    sliderPosition = SliderPositions.spExperiments;
                    //LoadExperimentsControl();
                    LoadProjectControl();
                    break;
                case SliderPositions.spModel:
                    sliderPosition = SliderPositions.spTree;
                    LoadTreeControl();
                    break;
                case SliderPositions.spExperiments:
                    sliderPosition = SliderPositions.spModel;
                    LoadModelControl();
                    break;
                default:
                    break;
            }
        }

        private void RightArrowControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (sliderPosition)
            {
                case SliderPositions.spTree:
                    sliderPosition = SliderPositions.spModel;
                    LoadModelControl();
                    break;
                case SliderPositions.spModel:
                    sliderPosition = SliderPositions.spExperiments;
                    LoadProjectControl();
                    //LoadExperimentsControl();
                    break;
                case SliderPositions.spExperiments:
                    sliderPosition = SliderPositions.spTree;
                    LoadTreeControl();
                    break;
                default:
                    break;
            }
        }

        private void wndMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged)
            {
                gExpanderGrid.Height = e.NewSize.Height * 0.2;
                lblExperiment.FontSize = e.NewSize.Height * 0.12;
                pcPlayer.Height = e.NewSize.Height * 0.2;
                pcPlayer.Width = pcPlayer.Height * 3;
                LoadingControl.Width = e.NewSize.Height * 0.2;

                ProjectControl.btnCreateProject.Height = ProjectControl.btnOpenProject.Height = e.NewSize.Height * 0.14;
                ProjectControl.btnCreateProject.FontSize = ProjectControl.btnOpenProject.FontSize = e.NewSize.Height * 0.085;
                ProjectControl.btnCreateProject.Width = ProjectControl.btnOpenProject.Width = ProjectControl.btnCreateProject.Height * 6;

                if (treeCanvas1 != null)
                {
                    //treeCanvas1.stModel.ScaleX += (e.NewSize.Height - e.PreviousSize.Height) * 0.003;
                    //treeCanvas1.stModel.ScaleY += (e.NewSize.Height - e.PreviousSize.Height) * 0.003;
                }
            }
        }

        private void pcPlayer_Click(object sender, RoutedEventArgs e)
        {
            switch ((e as PlayerRoutedEventArgs).ButtonType)
            {
                case PlayerByttonType.pbtPlay:
                    pbtPlay_Click();
                    break;
                case PlayerByttonType.pbtStop:
                    pbtStop_Click();
                    break;
                case PlayerByttonType.pbtPause:
                    break;
                default:
                    break;
            }
        }

        private void pbtStop_Click()
        {
            vissim.Instance.Simulation.Stop();
        }

        public void LoadingAnimationStart()
        {
            ccControlsHost2.Content = shadingControl;
            Panel.SetZIndex(ccControlsHost2, 700);
        }

        public void LoadingAnimationEnd()
        {
            Panel.SetZIndex(ccControlsHost2, 100);
            ccControlsHost2.Content = " ";
        }

        private void pbtPlay_Click()
        {
            LoadingAnimationStart();

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

                Dispatcher.Invoke(new ThreadStart(delegate()
                {
                    LoadingAnimationEnd();
                    treeCanvas1.Refresh();
                }));

                //MessageBox.Show("Ok");
            });
        }

        private void ShadowButton_OpenProject_Click(object sender, RoutedEventArgs e)
        {
            if (sliderPosition == SliderPositions.spExperiments) sliderPosition = SliderPositions.spTree;

            var dlgOpenFile = new OpenFileDialog();

            dlgOpenFile.Filter = "VISSIM Laboratory project file|*.vislab";
            if (dlgOpenFile.ShowDialog().Value)
            {
                pm.Initialize(dlgOpenFile.FileName);
            }
        }

        private void ShadowButton_CreateProject_Click(object sender, RoutedEventArgs e)
        {
            if (sliderPosition == SliderPositions.spExperiments) sliderPosition = SliderPositions.spTree;

            var wnd = new CreateProjectForm();

            var ww = new WindowWrapper(new WindowInteropHelper(this).Handle);
            if (wnd.ShowDialog(ww) == System.Windows.Forms.DialogResult.OK)
            {
                string projectName = wnd.tbxProjectName.Text;
                string projectDir = wnd.tbxProjectLocation.Text;
                string modelName = wnd.tbxNewModelName.Text;
                string modelFileName = wnd.tbxModelFile.Text;

                switch (wnd.checkedRadioButtonName)
                {
                    case "rbCreateNew":
                        pm.Initialize(projectName, projectDir, ModelCreationMode.cmCreateNew, modelName);
                        break;
                    case "rbSelectFromFile":
                        pm.Initialize(projectName, projectDir, ModelCreationMode.cmClone, System.IO.Path.GetDirectoryName(modelFileName));
                        break;
                    case "rbSelectCurrent":
                        pm.Initialize(projectName, projectDir, ModelCreationMode.cmClone, vissim.Instance.GetWorkingDirectory());
                        break;
                }
            }
        }

        private void border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (expander.IsExpanded) expander.IsExpanded = false;
        }
    }

    enum SliderPositions { spTree, spModel, spExperiments }
}
