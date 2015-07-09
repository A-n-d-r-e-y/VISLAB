using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System;
using System.Diagnostics;

using VisLab.Classes.Implementation.Entities;
using VisLab.Classes.Implementation.Analysis.Controllers;
using vissim = VisLab.Classes.Integration.VissimSingleton;
using VisLab.Classes.Implementation.Design;
using VisLab.Classes.Implementation.Analysis.Boundaries.Controls;
using VisLab.Classes.Implementation.Design.Generics;
using VisLab.Windows;
using VisLab.Classes.Implementation.Design.BindingSources;

namespace VisLab.Controls
{
    public partial class TreeControl : UserControl
    {
        private Point startPoint = new Point(-1.123, -1.123);
        public ShadowMaker ShadowMaker { get; set; }
        public string modelName { get; private set; }
        private Experimenter experimenter { get; set; }

        public static readonly RoutedEvent PlayEvent =
            EventManager.RegisterRoutedEvent("Play", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(TreeControl));

        public static readonly RoutedEvent StopEvent =
            EventManager.RegisterRoutedEvent("Stop", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(TreeControl));

        public static readonly RoutedEvent MultiEvent =
            EventManager.RegisterRoutedEvent("Multi", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(TreeControl));

        public static readonly RoutedEvent PauseEvent =
            EventManager.RegisterRoutedEvent("Pause", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(TreeControl));

        public static readonly RoutedEvent NodeAnalyzeOnEvent =
            EventManager.RegisterRoutedEvent("NodeAnalyzeOn", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(TreeControl));

        public static readonly RoutedEvent NodeAnalyzeOffEvent =
            EventManager.RegisterRoutedEvent("NodeAnalyzeOff", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(TreeControl));

        public event RoutedEventHandler Play
        {
            add { AddHandler(PlayEvent, value); }
            remove { RemoveHandler(PlayEvent, value); }
        }

        public event RoutedEventHandler Multi
        {
            add { AddHandler(MultiEvent, value); }
            remove { RemoveHandler(MultiEvent, value); }
        }

        public event RoutedEventHandler Stop
        {
            add { AddHandler(StopEvent, value); }
            remove { RemoveHandler(StopEvent, value); }
        }

        public event RoutedEventHandler Pause
        {
            add { AddHandler(PauseEvent, value); }
            remove { RemoveHandler(PauseEvent, value); }
        }

        public event RoutedEventHandler NodeAnalyzeOn
        {
            add { AddHandler(NodeAnalyzeOnEvent, value); }
            remove { RemoveHandler(NodeAnalyzeOnEvent, value); }
        }

        public event RoutedEventHandler NodeAnalyzeOff
        {
            add { AddHandler(NodeAnalyzeOffEvent, value); }
            remove { RemoveHandler(NodeAnalyzeOffEvent, value); }
        }

        public TreeControl(Experimenter experimenter, Model model)
        {
            InitializeComponent();

            this.experimenter = experimenter;

            this.modelName = model.Name;

            this.DataContext = model;

            button.DataContext = player.DataContext = experimenter.GetSimulationState();

            InitShadowMaker();
        }

        private CreateExperimentControl CreateCreateExperimentControl(Experiment.ExperimentData expData)
        {
            var ctrl = new CreateExperimentControl();
            ctrl.DataContext = expData;

            //ctrl.tbkDuration.Text = duration.ToString();
            ctrl.btnCreateExperimenet.Click += (sender_, e_) =>
            {
                experimenter.CreateExperiment(expData);

                // refresh the model experiments tree
                Refresh();
                ShadowMaker.ShadowDown();
            };

            ctrl.btnCancel.Click += (sender_, e_) =>
            {
                Refresh();
                ShadowMaker.ShadowDown();
            };

            return ctrl;
        }

        private void InitShadowMaker()
        {
            ShadowMaker = new ShadowMaker();
            ccShadowHost.DataContext = ShadowMaker;
        }

        public void Refresh()
        {
            cnvTree.Children.Clear();
            experimenter.DrawExperiment(cnvTree);
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                startPoint = Mouse.GetPosition(this);
                this.Cursor = Cursors.SizeAll;
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            // UserControl_MouseDown never pressed - fake move -> get out
            if (startPoint.X == -1.123 && startPoint.Y == -1.123) return;

            Point curr = e.GetPosition(this);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var dif = curr - startPoint;

                ttModel.X += (dif.X * (1 / stModel.ScaleX));
                ttModel.Y += (dif.Y * (1 / stModel.ScaleY));

                startPoint = curr;
            }

            curr = e.GetPosition(this.cnvTree);
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            stModel.ScaleX += (e.Delta > 0) ? 0.1 * stModel.ScaleX : -0.1 * stModel.ScaleX;
            stModel.ScaleY += (e.Delta > 0) ? 0.1 * stModel.ScaleY : -0.1 * stModel.ScaleY;
        }

        private void NodeControl_Select(object sender, RoutedEventArgs e)
        {
            var nodeCtrl = (sender as NodeControl);
            var exp = (nodeCtrl.DataContext as Experiment);

            experimenter.SelectExperiment(exp);

            Refresh();
        }

        private void NodeControl_Load(object sender, RoutedEventArgs e)
        {
            var nodeCtrl = (sender as NodeControl);
            var exp = (nodeCtrl.DataContext as Experiment);

            experimenter.LoadExperiment(exp);

            Refresh();
        }

        private void NodeControl_Delete(object sender, RoutedEventArgs e)
        {
            var nodeCtrl = (sender as NodeControl);
            var exp = (nodeCtrl.DataContext as Experiment);

            if (!experimenter.DeleteExperiment(exp))
                MessageBox.Show(App.Current.MainWindow, "You can't delete the last Model in the Project", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!exp.IsRoot) Refresh();
        }

        private void NodeControl_Open(object sender, RoutedEventArgs e)
        {
            var nodeCtrl = (sender as NodeControl);
            var exp = (nodeCtrl.DataContext as Experiment);

            Process.Start(exp.Path);
        }

        private void NodeControl_AnalyzeOn(object sender, RoutedEventArgs e)
        {
            var nodeCtrl = (sender as NodeControl);
            var exp = (nodeCtrl.DataContext as Experiment);

            if (exp.Data != null)
            {
                exp.Data.Save(string.Format("{0}\\{1}{2}"
                    , exp.Path
                    , modelName
                    , Experiment.ExperimentData.EXPERIMENT_DATA_POSTFIX));
            }

            RaiseEvent(new RoutedEventArgs<Experiment>(exp, TreeControl.NodeAnalyzeOnEvent));
        }

        private void NodeControl_AnalyzeOff(object sender, RoutedEventArgs e)
        {
            var nodeCtrl = (sender as NodeControl);
            var exp = (nodeCtrl.DataContext as Experiment);
            if (exp.Data != null)
            {
                exp.Data.Save(string.Format("{0}\\{1}{2}"
                    , exp.Path
                    , modelName
                    , Experiment.ExperimentData.EXPERIMENT_DATA_POSTFIX));
            }

            RaiseEvent(new RoutedEventArgs<int>(exp.Number, TreeControl.NodeAnalyzeOffEvent));
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize.Width > 0 && e.PreviousSize.Height > 0)
            {
                ttModel.X += (e.NewSize.Width - e.PreviousSize.Width) / 2;
                ttModel.Y += (e.NewSize.Height - e.PreviousSize.Height) / 2;
            }
            ////else
            ////{
            ////    ttModel.X = e.NewSize.Width / 3;
            ////    ttModel.Y = e.NewSize.Height / 3;
            ////}
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            ShadowMaker.ShadowUp();
            ShadowMaker.ShowThis(new LoadingControl("Starting VISSIM..."));

            //???
            this.modelControlGrid.IsEnabled = false;

            experimenter.LoadModelAsync(true, this.Dispatcher, () =>
                {
                    this.modelControlGrid.IsEnabled = true;
                    ShadowMaker.ShadowDown();
                });
        }

        private void button_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //var bc = new BrushConverter();

            //if (button.IsEnabled)
            //{
            //    button.Background = (Brush)bc.ConvertFrom("#FF7C7C7C");
            //    button.Foreground = Brushes.White;
            //    button.Content = "Load";
            //}
            //else
            //{
            //    button.Background = (Brush)bc.ConvertFrom("#FFFFFC03");
            //    button.Foreground = Brushes.Black;
            //    button.Content = "Loaded";
            //}
        }

        private void palyer_Click(object sender, RoutedEventArgs e)
        {
            switch ((e as PlayerRoutedEventArgs).ButtonType)
            {
                case PlayerByttonType.pbtPlay:
                    tc_Play();
                    break;
                case PlayerByttonType.pbtStop:
                    tc_Stop();
                    break;
                case PlayerByttonType.pbtPause:
                    tc_Pause();
                    break;
                case PlayerByttonType.pbtMulti:
                    tc_Multi();
                    break;
                default:
                    break;
            }
        }

        void tc_Pause()
        {
            RaiseEvent(new RoutedEventArgs(TreeControl.PauseEvent));
            experimenter.PauseSimulation();
        }

        void tc_Stop()
        {
            RaiseEvent(new RoutedEventArgs(TreeControl.StopEvent));
            experimenter.StopSimulation();
        }

        void tc_Multi()
        {
            RaiseEvent(new RoutedEventArgs(TreeControl.MultiEvent));

            ShadowMaker.ShadowUp();
            modelControlGrid.IsEnabled = false;

            var x = new StartMultirunBindingSource()
            {
                DisableAnimation = true,
                NumberOfRuns = 3
            };

            var startCtrl = new StartMultirunControl();
            startCtrl.DataContext = x;
            startCtrl.btnCancel.Click += (sender_, e_) =>
                {
                    ShadowMaker.ShadowDown();
                    modelControlGrid.IsEnabled = true;
                };
            startCtrl.btnOk.Click += (sender_, e_) =>
                {
                    ShadowMaker.ShadowDown();

                    if (x.NumberOfRuns > 0)
                    {
                        var lc = new LoadingControl();
                        if (!vissim.IsInstanciated) lc.Message = "Starting VISSIM ...";
                        else lc.Message = "Simulation ...";
                        ShadowMaker.ShowThis(lc);

                        experimenter.RunMultiAsync(x, this.Dispatcher
                            , StartAction
                            , (expData) =>
                                {
                                    var ctrl = CreateCreateExperimentControl(expData);
                                    ShadowMaker.ShowThis(ctrl);
                                }
                            , (message) =>
                                {
                                    lc.Message = message;
                                }
                            );
                    }
                    else ShadowMaker.ShowThis(startCtrl);

                    ShadowMaker.ShadowUp();
                };

            ShadowMaker.ShowThis(startCtrl);
        }

        void tc_Play()
        {
            RaiseEvent(new RoutedEventArgs(TreeControl.PlayEvent));

            modelControlGrid.IsEnabled = false;
            ShadowMaker.ShadowUp();

            if (!vissim.IsInstanciated)
            {
                ShadowMaker.ShowThis(new LoadingControl("Starting VISSIM ..."));
            }
            else ShadowMaker.ShowThis(new LoadingControl("Simulation ..."));

            experimenter.RunSimulationAsync(this.Dispatcher
                , StartAction
                , (expData) =>
                {
                    var ctrl = CreateCreateExperimentControl(expData);
                    ShadowMaker.ShowThis(ctrl);
                });
        }

        private bool StartAction()
        {
            //ShadowMaker.ShowThis(new LoadingControl("Simulation ..."));
            modelControlGrid.IsEnabled = true;

            if (!experimenter.CheckDataCollectionExists())
            {
                var result = MessageBox.Show(App.Current.MainWindow, "There are no Data Collection measurements defined in the Model.\nDo you really want to start simulation?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    ShadowMaker.ShadowDown();
                    return false;
                }
            }

            if (!experimenter.CheckCountersEvaluationEnabled())
            {
                var result = MessageBox.Show(App.Current.MainWindow, "Data Collection Evaluation is disabled.\nDo you want to enable Data Collection Evaluation?\n[Press Cancel to stop simulation]", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) experimenter.EnableCountersEvaluation();
                if (result == MessageBoxResult.Cancel)
                {
                    ShadowMaker.ShadowDown();
                    return false;
                }
            }

            return true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReturnTreeToCenter();
        }

        private void ReturnTreeToCenter()
        {
            if ((ttModel.X <= 0
                || ttModel.Y <= 0
                || ttModel.Y >= this.ActualHeight
                || ttModel.X >= this.ActualWidth) && cnvTree.Children.Count > 0)
            {
                double
                    duration = 0.4,
                    dX = (cnvTree.Children[0] as NodeControl).ActualWidth / 2,
                    dY = (cnvTree.Children[0] as NodeControl).ActualHeight / 2,
                    fromX = ttModel.X,
                    fromY = ttModel.Y,
                    toY = this.ActualHeight / 2 - dY,
                    toX = this.ActualWidth / 2 - dX;

                var transformYAnimation = new DoubleAnimation(fromY, toY, new Duration(TimeSpan.FromSeconds(duration)), FillBehavior.Stop);
                transformYAnimation.Completed += (sender__, e__) => ttModel.Y = toY;
                ttModel.BeginAnimation(TranslateTransform.YProperty, transformYAnimation);

                var transformXAnimation = new DoubleAnimation(fromX, toX, new Duration(TimeSpan.FromSeconds(duration)), FillBehavior.Stop);
                transformXAnimation.Completed += (sender__, e__) => ttModel.X = toX;
                ttModel.BeginAnimation(TranslateTransform.XProperty, transformXAnimation);
            }
        }
    }
}
