using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Microsoft.Win32;
using System.Linq;

using VisLab.Classes.Implementation.Design;
using VisLab.Controls;
using VisLab.Forms;
using VisLab.Classes.Implementation.Wrappers;
using VisLab.Classes.Implementation.Analysis.Controllers;
using VisLab.Classes.Implementation.Analysis.Boundaries.Controls;
using System.Windows.Controls;
using VisLab.Classes.Implementation.Design.Generics;
using System.Windows.Media.Animation;
using System;
using System.Windows.Media;
using VisLab.Classes.Integration.Entities;
using System.Collections.Generic;
using System.IO;
using VisLab.Classes.Implementation.Utilities;
using VisLab.Classes.Implementation.Entities;
using System.Diagnostics;
using System.Windows.Documents;
using System.Threading;
using System.Windows.Threading;
using VisLab.Classes.Integration.Utilities;

namespace VisLab
{
    public partial class MainWindow : Window
    {
        AsyncExceptionHandler exceptionHandler;
        VisualNavigator navigator;
        ProjectManager manager;
        ShadowMaker shadowMaker;
        Analyst analyst;

        public MainWindow()
        {
            InitializeComponent();

            CreateExceptionHandler();
            InitializeProjectManager();
            InitializeNavigator();
            InitShadowMaker();
        }

        #region Initializers

        private void InitShadowMaker()
        {
            shadowMaker = new ShadowMaker();
            ccTopLayerHost.DataContext = shadowMaker;
        }

        private TreeControl CreateTreeControl(ProjectManager manager, string modelName)
        {
            var tree = new TreeControl(
                manager.GetExperimenterForModel(modelName), 
                manager.GetModel(modelName));

            tree.ShadowMaker.Up += (sender_, e_) => navigator.IsEnabled = false;
            tree.ShadowMaker.Down += (sender_, e_) => navigator.IsEnabled = true;

            tree.NodeAnalyzeOn += (s_, e_) =>
                {
                    var exp = (e_ as RoutedEventArgs<Experiment>).Value;
                    analyst.AnalyzeExperiment(modelName, exp);
                };

            tree.NodeAnalyzeOff += (s_, e_) =>
                {
                    analyst.RemoveExperiment(modelName, (e_ as RoutedEventArgs<int>).Value);
                };

            tree.Refresh();

            return tree;
        }

        private AddModelControl CreateAddModelControl()
        {
            var amc = new AddModelControl();
            amc.btnAddModel.Click += (sender_, e_) => amc_btnAddModelClick(amc);
            amc.btnCancel.Click += (sender_, e_) => amc_btnCancelClick();

            return amc;
        }

        private ReportControl CreateReportControl()
        {
            var reportControl = new ReportControl()
            {
                DataContext = analyst.Host,
            };

            return reportControl;
        }

        private AsyncExceptionHandler CreateExceptionHandler()
        {
            exceptionHandler = new AsyncExceptionHandler(this.Dispatcher);
            exceptionHandler.Exception += (sender, e) =>
            {
                var exception = e.Value;
                string ownMessage = string.Empty;


                switch (exception.Message)
                {
                    case "Aggregation for Data Collection not possible!":
                        ownMessage = "This error means that no single parameter is selected for Data Collection evaluation.\n" +
                                    "To fix this error go to \"Data Collection - Configuration\" window\n" +
                                    "[Evaluation -> Files -> Data collection -> Configuration -> Configuration]\n" +
                                    "end select at least one parameter to evaluation";
                        break;
                }

                var result = MessageBox.Show(this,
                    "An error occured. The following information might help you decide to try to continue your work or not."
                    + "\n\nOriginal exception: " + exception.Message
                    + (string.IsNullOrWhiteSpace(ownMessage) ? string.Empty : "\n\nRemark: " + ownMessage)
                    + "\n\nPress [Yes] to try again or [No] to try to continue."
                    , exception.GetType().ToString(), MessageBoxButton.YesNo, MessageBoxImage.Error);

                e.Param.Value = (result == MessageBoxResult.Yes);
            };

            return exceptionHandler;
        }

        private void InitializeProjectManager()
        {
            manager = new ProjectManager(this.Dispatcher, exceptionHandler);
            manager.ModelDeleted += (sender_, e_) => navigator.RemoveTreeControlByModelName(e_.Value);
            manager.Attempt += (sender_, e_) =>
                e_.Param = MessageBox.Show(this, e_.Value, "Failed Attempt", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

            this.DataContext = manager;
        }

        private void InitializeNavigator()
        {
            var projectControl = new ProjectControl();
            InitializeProjectControl(projectControl);

            navigator = new VisualNavigator(projectControl);
            navigator.FocusChanged += (sender_, e_) =>
                {
                    var control = e_.Value;
                    string modelName = control is TreeControl ? (control as TreeControl).modelName : null;

                    if (!string.IsNullOrWhiteSpace(modelName)) manager.Project.CurrentModelName = modelName;
                };

            LayoutRoot.DataContext = navigator;
        }

        private void InitializeProjectControl(ProjectControl prControl)
        {
            prControl.btnCreateProject.Click += new RoutedEventHandler(btnCreateProject_Click);
            prControl.btnOpenProject.Click += new RoutedEventHandler(btnOpenProject_Click);
            prControl.btnAddModel.Click += new RoutedEventHandler(btnAddModel_Click);

            prControl.DataContext = manager;
        }

        #endregion

        #region ProjectControl events

        void btnAddModel_Click(object sender, RoutedEventArgs e)
        {
            shadowMaker.ShadowUp();

            var ctrl = CreateAddModelControl();

            shadowMaker.ShowThis(ctrl);
        }

        void btnOpenProject_Click(object sender, RoutedEventArgs e)
        {
            shadowMaker.ShowThis(new UserControl()
                {
                    Content = "Opening project...",
                    FontSize = 40,
                    Foreground = Brushes.Gray,
                });
            shadowMaker.ShadowUp();

            var dlgOpenFile = new OpenFileDialog();

            dlgOpenFile.Filter = "VISSIM Laboratory project file|*.vislab";
            if (dlgOpenFile.ShowDialog().Value)
            {
                OpenProject(dlgOpenFile.FileName);
            }

            shadowMaker.ShadowDown();
        }

        public void OpenProject(string fileName)
        {
            string modelName = manager.OpenProject(fileName);

            InsertFirstTreeControlToNavigator(modelName);

            foreach (string mName in manager.Project.Models.Model.Where(m => m.Name != modelName).Select(m => m.Name))
            {
                var tc = CreateTreeControl(manager, mName);

                navigator.InsertTreeControl(tc);
            }
        }

        void btnCreateProject_Click(object sender, RoutedEventArgs e)
        {
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
                        manager.CreateNewProject(projectName, projectDir, modelName);
                        break;
                    case "rbSelectFromFile":
                        //string dir = System.IO.Path.GetDirectoryName(modelFileName);
                        modelName = manager.CloneProject(projectName, projectDir, modelFileName);
                        break;
                    case "rbSelectCurrent":
                        //pm.Initialize(projectName, projectDir, ModelCreationMode.cmClone, vissim.Instance.GetWorkingDirectory());
                        break;
                }

                InsertFirstTreeControlToNavigator(modelName);
            }
        }

        #endregion

        #region AddModelControl events

        private void amc_btnCancelClick()
        {
            shadowMaker.ShadowDown();
        }

        private void amc_btnAddModelClick(UserControl control)
        {
            shadowMaker.ShadowDown();

            shadowMaker.ShowThis(new LoadingControl("Loading VISSIM..."));
            shadowMaker.ShadowUp();

            ////////////////////////////////////////////////////////////////////
            var amc = control as AddModelControl;

            string modelName = string.Empty;

            if (amc.rbCreateNewModel.IsChecked.Value)
            {
                modelName = amc.tbxModelName.Text;
                manager.AddNewModelAsync(
                    modelName,
                    this.Dispatcher,
                    amc,
                    amc_FinalAction);
            }

            if (amc.rbLoadFromFile.IsChecked.Value)
            {
                string modelFileName = amc.tbxFileName.Text;
                modelName = amc.cbAltModelName.IsChecked.Value
                    ? amc.tbxAltModelName.Text
                    : Path.GetFileNameWithoutExtension(modelFileName);

                manager.AddExistingModelAsync(
                    modelFileName
                    , amc.cbAltModelName.IsChecked.Value ? amc.tbxAltModelName.Text : string.Empty
                    , this.Dispatcher
                    , amc
                    , () =>
                        {
                            this.Dispatcher.Invoke(DispatcherPriority.Background,
                                new ThreadStart(delegate()
                            {
                                MessageBox.Show(this, "Model with name \"" + modelName + "\" already exists!\nPlease define new name.",
                                "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
                            }));
                        }
                    , amc_FinalAction);
            }
        }

        private void amc_FinalAction(bool modelIsAdded, string modelName, UserControl amc)
        {
            if (modelIsAdded)
            {
                var treeControl = CreateTreeControl(manager, modelName);

                // go to existing tree control
                navigator.Navigate(LinkDirection.ldRight);

                navigator.InsertTreeControl(treeControl);

                shadowMaker.ShadowDown();
            }
            else
            {
                shadowMaker.ShowThis(amc);
            }
        }

        #endregion

        #region Arrows

        private void LeftArrowControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            navigator.Navigate(LinkDirection.ldLeft);
        }

        private void RightArrowControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            navigator.Navigate(LinkDirection.ldRight);
        }

        private void topArrowButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            navigator.Navigate(LinkDirection.ldTop);
        }

        private void bottomArrowButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            navigator.Navigate(LinkDirection.ldBottom);
        }

        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            manager.SaveProject();
        }

        private void InsertFirstTreeControlToNavigator(string modelName)
        {
            analyst = new Analyst(manager, shadowMaker, exceptionHandler);

            if (navigator.HasTreeControls())
            {
                navigator.Clear();
                InitializeNavigator();
            }

            var treeControl = CreateTreeControl(manager, modelName);

            navigator.LinkControl(LinkDirection.ldRight, treeControl);
            navigator.LinkControl(LinkDirection.ldLeft, treeControl);

            // navigate to treeControl
            navigator.Navigate(LinkDirection.ldRight);

            // loopback from bottom/top
            navigator.LinkControl(LinkDirection.ldBottom, treeControl);
            navigator.LinkControl(LinkDirection.ldTop, treeControl);

            //////////////////////////////////////////////////////////////////////

            var reportControl = CreateReportControl();

            navigator.LinkControl(LinkDirection.ldRight, reportControl);

            // navigate back to managerControl
            navigator.Navigate(LinkDirection.ldLeft);
            navigator.LinkControl(LinkDirection.ldLeft, reportControl);

            // navigate to treeControl
            navigator.Navigate(LinkDirection.ldRight);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start((sender as Hyperlink).NavigateUri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
