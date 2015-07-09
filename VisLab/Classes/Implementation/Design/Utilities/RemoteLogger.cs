using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;

using VisLab.LoggingService;
using VisLab.Windows;
using VisLab.IssueTrackerService;
using VisLab.Classes.Integration.Wrappers;
using VisLab.Classes.Integration.Extensions;
using vissim = VisLab.Classes.Integration.VissimSingleton;

namespace VisLab.Classes.Implementation.Design.Utilities
{
    public enum MessageType { Message, Starting, Closing, Error, Warning }

    public static class RemoteLogger
    {
        public static void WriteLogAsync(MessageType messageType, string message, string trace)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                try
                {
                    var assembly = Assembly.GetExecutingAssembly().GetName();
                    string applicationName = string.Format("{0}.{1}.{2}.{3}", Environment.UserDomainName, Environment.MachineName,
                        assembly.Name, assembly.Version);
                    string userName = Environment.UserName.ToUpper();
                    string stringMessageType = Enum.GetName(typeof(MessageType), messageType);

                    var service = new ClientLineService();
                    service.WriteLogAsync(DateTime.Now, applicationName, userName, "VisLab", stringMessageType, message, trace);
                }
                catch
                {
                }
            });
        }

        public static void WriteLogAsync(MessageType messageType)
        {
            WriteLogAsync(messageType, "", "");
        }

        private static void ReportIssue(string errType, string errMessage, string errTrace)
        {
            try
            {
                using (var c = new MantisConnect())
                {
                    string
                        username = "RemoteUser",
                        password = "Op$1kB7%mM",
                        version = App.AssemblyVersion.Substring(2, 3);

                    var os = Environment.OSVersion;

                    //var projects = c.mc_projects_get_user_accessible(username, password);
                    //var vislabProject = (from p in projects where p.name == "VisLab" select p).FirstOrDefault();
                    //var severity = (from s in c.mc_enum_severities(username, password) where s.name == "crash" select s).FirstOrDefault();
                    //var priority = (from p in c.mc_enum_priorities(username, password) where p.name == "high" select p).FirstOrDefault();
                    //var viewState = (from v in c.mc_enum_view_states(username, password) where v.name == "private" select v).FirstOrDefault();

                    string biggestIssueId = c.mc_issue_get_biggest_id(username, password, "1"); //vislabProject.id);

                    c.mc_issue_add(username, password, new IssueData()
                    {
                        view_state = new ObjectRef()
                        {
                            id = "50", //viewState.id,
                            name = "private", //viewState.name,
                        },
                        category = "Bug",
                        summary = errType.Length > 128 ? errType.Substring(0, 128) : errType,
                        description = errMessage,
                        additional_information = errTrace,
                        version = version,
                        severity = new ObjectRef()
                        {
                            id = "70", //severity.id,
                            name = "crash", //severity.name,
                        },
                        priority = new ObjectRef()
                        {
                            id = "40", //priority.id,
                            name = "high", //priority.name,
                        },
                        id = biggestIssueId + 1,
                        project = new ObjectRef()
                        {
                            id = "1", //vislabProject.id,
                            name = "VisLab", //vislabProject.name
                        },
                        platform = Environment.Version.ToString(),
                        os = os.Platform.ToString(),
                        os_build = os.Version.ToString(),
                        notes = new IssueNoteData[]
                                    {
                                        new IssueNoteData()
                                        {
                                            text = string.Format("Is64BitOperatingSystem={0}\nIs64BitProcess={1}\nMachineName={2}\nProcessorCount={3}\nUserDomainName={4}\nUserName={5}\nPhysMemory={6}\nAssemblyVersion={7}\nVissimVersion={8}"
                                                , Environment.Is64BitOperatingSystem
                                                , Environment.Is64BitProcess
                                                , Environment.MachineName
                                                , Environment.ProcessorCount
                                                , Environment.UserDomainName
                                                , Environment.UserName
                                                , Environment.WorkingSet
                                                , App.AssemblyVersion
                                                , vissim.IsInstanciated ? vissim.Instance.Wrap().Version : "UNKNOWN"),
                                            view_state = new ObjectRef()
                                            {
                                                id = "50", //viewState.id,
                                                name = "private", //viewState.name,
                                            },
                                        },
                                    },
                    });
                }
            }
            catch { }
        }

        public static void ReportIssueAsync(Exception ex)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                ReportIssue(ex.GetType().ToString(), ex.Message, ex.StackTrace);
            });
        }

        public static void ReportIssue(Exception ex)
        {
            ReportIssue(ex.GetType().ToString(), ex.Message, ex.StackTrace);
        }
    }
}
