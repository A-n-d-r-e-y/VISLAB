using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisLab.Controls;
using VisLab.Classes.Implementation.Analysis.Controllers;
using System.Windows.Controls;
using VisLab.Classes.Implementation.Design.Utilities;
using System.ComponentModel;
using System.Collections.ObjectModel;
using VisLab.Classes.Integration.Entities;
using System.Windows.Data;
using System.Windows;
using VisLab.Classes.Implementation.Design.Generics;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Data;

using System.IO;
using System.Xml;
using System.Windows.Markup;
using System.Threading;
using System.Windows.Threading;
using VisLab.Classes.Implementation.Analysis.Boundaries.Controls;
using VisLab.Classes.Implementation.Entities;

namespace VisLab.Classes.Implementation.Design
{
    public class ReportBindingSource : INotifyPropertyChanged
    {
        public ObservableCollection<ModelControl> List { get; set; }

        private ShadowMaker bigShadow;

        private ModelControl masterModel;
        public ModelControl MasterModel
        {
            get { return masterModel; }
            set
            {
                masterModel = value;
                OnPropertyChanged("MasterModel");
            }
        }

        public string CountersMsg { get; set; }
        public string TrTimesMsg { get; set; }

        private bool isCounterButtonEnabled;
        public bool IsCounterButtonEnabled
        {
            get { return isCounterButtonEnabled; }
            set
            {
                isCounterButtonEnabled = value;
                OnPropertyChanged("IsCounterButtonEnabled");
            }
        }

        private bool isTrTimesButtonEnabled;
        public bool IsTrTimesButtonEnabled
        {
            get { return isTrTimesButtonEnabled; }
            set
            {
                isTrTimesButtonEnabled = value;
                OnPropertyChanged("IsTrTimesButtonEnabled");
            }
        }

        private bool areCountersGlobalVisible;
        public bool AreCountersGlobalVisible
        {
            get { return areCountersGlobalVisible; }
            set
            {
                areCountersGlobalVisible = value;

                if (areCountersGlobalVisible) CountersMsg = "Hide counters";
                else CountersMsg = "Show counters";

                foreach (var control in List)
                {
                    var ctrl = control;
                    RefreshCounters(ctrl);
                }

                OnPropertyChanged("CountersMsg");
                OnPropertyChanged("AreCountersGlobalVisible");
            }
        }

        private bool areTrTimesGlobalVisible;
        public bool AreTrTimesGlobalVisible
        {
            get { return areTrTimesGlobalVisible; }
            set
            {
                areTrTimesGlobalVisible = value;

                if (areTrTimesGlobalVisible) TrTimesMsg = "Hide tr. times";
                else TrTimesMsg = "Show tr. times";

                foreach (var control in List)
                {
                    var ctrl = control;
                    RefreshTrTimes(ctrl);
                }

                OnPropertyChanged("TrTimesMsg");
                OnPropertyChanged("AreTrTimesVisible");
            }
        }

        private AsyncExceptionHandler aex;

        private ProjectManager manager;

        public ReportBindingSource(ShadowMaker bigShadow, AsyncExceptionHandler exceptionHandler, ProjectManager manager)
        {
            this.aex = exceptionHandler;
            this.bigShadow = bigShadow;
            this.manager = manager;

            List = new ObservableCollection<ModelControl>();

            AreCountersGlobalVisible = false;
            AreTrTimesGlobalVisible = false;
        }

        public void CopyToMaster(ModelControl control)
        {
            if (control.Clone != null)
            {
                MasterModel = control.Clone as ModelControl;
                IsCounterButtonEnabled = (MasterModel.DataContext as Experiment).Data.IsCountersEvaluationEnabled;
                IsTrTimesButtonEnabled = (MasterModel.DataContext as Experiment).Data.IsTravelTimesEvaluationEnabled;

                //DrawControl(control);
            }
        }

        private void RefreshCounters(ModelControl control)
        {
            if ((control.DataContext as Experiment).Data.IsCountersEvaluationEnabled)
            {
                if (AreCountersGlobalVisible)
                {
                    control.OpenCountersGrid();

                    if (!control.HasPoints || !control.HasCounters)
                    {
                        if (bigShadow.Shadow == null)
                        {
                            bigShadow.ShowThis(new LoadingControl("Drawing counters..."));
                            bigShadow.ShadowUp();
                        }

                        ThreadPool.QueueUserWorkItem(o =>
                        {
                            aex.Protect(() =>
                            {
                                if (!control.HasPoints) OnDrawPoints(control);
                                if (!control.HasCounters) OnDrawCounters(control);

                                control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
                                {
                                    bigShadow.ShadowDown();
                                }));
                            });
                        });
                    }

                    if (!control.ArePointsVisible || !control.AreCountersVisible)
                    {
                        if (!control.ArePointsVisible) control.ChangePointsVisibility(Visibility.Visible);
                        if (!control.AreCountersVisible) control.ChangeCountersVisibility(Visibility.Visible);
                    }
                }
                else
                {
                    control.CloseCountersGrid();

                    if (control.ArePointsVisible || control.AreCountersVisible)
                    {
                        if (control.ArePointsVisible) control.ChangePointsVisibility(Visibility.Hidden);
                        if (control.AreCountersVisible) control.ChangeCountersVisibility(Visibility.Hidden);
                    }
                }
            }
        }

        private void RefreshTrTimes(ModelControl control)
        {
            if ((control.DataContext as Experiment).Data.IsTravelTimesEvaluationEnabled)
            {
                if (AreTrTimesGlobalVisible)
                {
                    control.OpenTrTimesGrid();

                    if (!control.HasSections || !control.HasTravelTimes)
                    {
                        if (bigShadow.Shadow == null)
                        {
                            bigShadow.ShowThis(new LoadingControl("Drawing travel times..."));
                            bigShadow.ShadowUp();
                        }

                        ThreadPool.QueueUserWorkItem(o =>
                        {
                            aex.Protect(() =>
                            {
                                if (!control.HasSections) OnDrawSections(control);
                                if (!control.HasTravelTimes) OnDrawTrTimes(control);

                                control.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
                                {
                                    bigShadow.ShadowDown();
                                }));
                            });
                        });
                    }

                    if (!control.AreSectionsVisible || !control.AreTrTimesVisible)
                    {
                        if (!control.AreSectionsVisible) control.ChangeSectionsVisibility(Visibility.Visible);
                        if (!control.AreTrTimesVisible) control.ChangeTrTimesVisibility(Visibility.Visible);
                    }
                }
                else
                {
                    if ((control.DataContext as Experiment).Data.IsTravelTimesEvaluationEnabled) control.CloseTrTimesGrid();

                    if (control.AreSectionsVisible || control.AreTrTimesVisible)
                    {
                        if (control.AreSectionsVisible) control.ChangeSectionsVisibility(Visibility.Hidden);
                        if (control.AreTrTimesVisible) control.ChangeTrTimesVisibility(Visibility.Hidden);
                    }
                }
            }
        }

        public void ShowCountersReport()
        {
            if (bigShadow.Shadow == null)
            {
                bigShadow.ShowThis(new LoadingControl("Generating..."));
                bigShadow.ShadowUp();
            }

            var dt = new DataTableEx();

            ThreadPool.QueueUserWorkItem(o =>
            {
                aex.Protect(() =>
                    {
                        dt = OnGenerateCountersReport(dt);

                        MasterModel.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
                        {
                            var cReport = new CountersReportControl("Counters Report", manager, dt.Items);
                            cReport.datagrid.ItemsSource = CollectionViewSource.GetDefaultView(dt.dt);
                            cReport.btnClose.Click += (s_, e_) => bigShadow.ShadowDown();

                            bigShadow.ShowThis(cReport);
                        }));
                    });
            });
        }

        public void ShowTrTimesReport()
        {
            if (bigShadow.Shadow == null)
            {
                bigShadow.ShowThis(new LoadingControl("Generating..."));
                bigShadow.ShadowUp();
            }

            var dt = new DataTableEx();

            ThreadPool.QueueUserWorkItem(o =>
            {
                aex.Protect(() =>
                {
                    dt = OnGenerateTrTimesReport(dt);

                    MasterModel.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
                    {
                        var cReport = new CountersReportControl("Tr. Times Report", manager, dt.Items);
                        cReport.datagrid.ItemsSource = CollectionViewSource.GetDefaultView(dt.dt);
                        cReport.btnClose.Click += (s_, e_) => bigShadow.ShadowDown();

                        bigShadow.ShowThis(cReport);
                    }));
                });
            });
        }

        public void ClearMaster()
        {
            MasterModel = null;
        }

        public void MoveAllModels(Point point)
        {
            foreach (var control in List) MoveModelToPoint(control, point);
            MoveModelToPoint(MasterModel, point);
        }

        private void MoveModelToPoint(ModelControl control, Point point)
        {
            control.MoveToPoint(point);
        }

        #region events

        public event EventHandler<EventArgs<UserControl>> DrawPoints;

        private void OnDrawPoints(UserControl control)
        {
            if (DrawPoints != null) DrawPoints(this, new EventArgs<UserControl>(control));
        }

        public event EventHandler<EventArgs<UserControl>> DrawCounters;

        private void OnDrawCounters(UserControl control)
        {
            if (DrawCounters != null) DrawCounters(this, new EventArgs<UserControl>(control));
        }

        public event EventHandler<EventArgs<UserControl>> DrawSections;

        private void OnDrawSections(UserControl control)
        {
            if (DrawSections != null) DrawSections(this, new EventArgs<UserControl>(control));
        }

        public event EventHandler<EventArgs<UserControl>> DrawTrTimes;

        private void OnDrawTrTimes(UserControl control)
        {
            if (DrawTrTimes != null) DrawTrTimes(this, new EventArgs<UserControl>(control));
        }

        public event EventHandler<EventArgs<DataTableEx, Dispatcher>> GenerateCountersReport;

        private DataTableEx OnGenerateCountersReport(DataTableEx dt)
        {
            if (GenerateCountersReport != null)
            {
                var e = new EventArgs<DataTableEx, Dispatcher>(dt, MasterModel.Dispatcher);

                GenerateCountersReport(this, e);
            }

            return dt;
        }

        public event EventHandler<EventArgs<DataTableEx, Dispatcher>> GenerateTrTimesReport;

        private DataTableEx OnGenerateTrTimesReport(DataTableEx dt)
        {
            if (GenerateTrTimesReport != null)
            {
                var e = new EventArgs<DataTableEx, Dispatcher>(dt, MasterModel.Dispatcher);

                GenerateTrTimesReport(this, e);
            }

            return dt;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
