using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.Text;
using System.Runtime.Hosting;
using System.IO;

using VisLab.Classes.Implementation.Design.Utilities;
using VisLab.LoggingService;
using VisLab.IssueTrackerService;
using vissim = VisLab.Classes.Integration.VissimSingleton;

namespace VisLab.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string AssemblyVersion
        {
            get
            {
                return string.Format("v.{0}", Assembly.GetExecutingAssembly().GetName().Version);
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {

#if (!DEBUG)
            RemoteLogger.WriteLogAsync(MessageType.Starting);
#else
            //RemoteLogger.ReportIssue("errType", "errMessage", "errTrace");
            //RemoteLogger.WriteLogAsync(MessageType.Error, "message", "trace");
#endif

            var mainWindow = new MainWindow();

            if (AppDomain.CurrentDomain != null
                && AppDomain.CurrentDomain.SetupInformation != null
                && AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null)
            {
                string[] activationData = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
                if (activationData != null && activationData.Length > 0)
                {
                    Uri uri = new Uri(activationData[0]);
                    string path = uri.LocalPath;

                    if (path.EndsWith(".vislab") && File.Exists(path))
                    {
                        mainWindow.OpenProject(path);
                    }
                }
            }

            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            RemoteLogger.ReportIssue(e.Exception);

            if (vissim.IsInstanciated) vissim.Instance.Exit();
        }
    }
}
