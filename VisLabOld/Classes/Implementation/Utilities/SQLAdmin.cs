using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Data;
//using Microsoft.SqlServer.Management;

namespace VisLab.Classes
{
    class SQLAdmin
    {
        public static bool MakeBackup(ServerMessageEventHandler onComplete, Guid id, int experimentNumber,
            string serverName, string dbName, string uncDir, string userName, string password)
        {
            bool result = false;

            try
            {
                var sc = new ServerConnection(serverName);
                try
                {
                    sc.LoginSecure = false;
                    sc.Login = userName;
                    sc.Password = password;

                    var srv = new Server(sc);
                    var bkp = new Backup();

                    bkp.Action = BackupActionType.Database;
                    bkp.Database = dbName;
                    bkp.BackupSetName = id.ToString();
                    bkp.BackupSetDescription = string.Format("VisLab auto-backup (experiment N{0}).", experimentNumber);
                    bkp.Devices.AddDevice(Path.Combine(uncDir, bkp.Database + ".bak"), DeviceType.File);
                    bkp.Incremental = false;
                    if (onComplete != null) bkp.Complete += onComplete;
                    bkp.SqlBackup(srv);
                    result = true;
                }
                finally
                {
                    sc.Disconnect();
                }
            }
            catch { }

            return result;
        }

        public static bool MakeRestore(ServerMessageEventHandler onComplete, Guid id,
            string serverName, string dbName, string uncDir, string userName, string password)
        {
            bool result = false;

            try
            {
                var sc = new ServerConnection(serverName);
                try
                {
                    sc.LoginSecure = false;
                    sc.Login = userName;
                    sc.Password = password;

                    var srv = new Server(sc);
                    var rest = new Restore();

                    rest.Action = RestoreActionType.Database;
                    rest.Database = dbName;
                    rest.Devices.AddDevice(Path.Combine(uncDir, rest.Database + ".bak"), DeviceType.File);
                    rest.ReplaceDatabase = true;

                    var headers = rest.ReadBackupHeader(srv);
                    var query = from DataRow row in headers.Rows
                                where (string)row["BackupName"] == id.ToString()
                                select (Int16)row["Position"];

                    rest.FileNumber = query.First();

                    if (onComplete != null) rest.Complete += onComplete;
                    rest.SqlRestore(srv);

                    result = true;
                }
                finally
                {
                    sc.Disconnect();
                }
            }
            catch { }

            return result;
        }
    }
}
