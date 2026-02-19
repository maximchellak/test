using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarNet.Logging;

namespace DBBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            StarNet.Logging.Logger.Instance.Init("DBBackup", true);
            
            //load config
            var strUsername = System.Configuration.ConfigurationManager.AppSettings["username"];
            var strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
            var strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];
            var strDirectory = System.Configuration.ConfigurationManager.AppSettings["directory"];
            var strSchema = System.Configuration.ConfigurationManager.AppSettings["schemas"];
            var strVersion = System.Configuration.ConfigurationManager.AppSettings["version"];
            var strMinutesToWait = System.Configuration.ConfigurationManager.AppSettings["minutes_to_wait"];

            var strDestinationDir = System.Configuration.ConfigurationManager.AppSettings["backup_dir"];
            var strRetentionDays = System.Configuration.ConfigurationManager.AppSettings["retention_days"];

            //backup db
            Console.WriteLine("Backup");
            BackupOracleDb(strUsername, strPassword, strDatabase, strDirectory, strSchema, strVersion, strMinutesToWait);

            //delete old backups
            Console.WriteLine("Clean");
            CleanOldFiles(strDestinationDir, strRetentionDays, strSchema);

            StarNet.Logging.Logger.Instance.LogInfo("End");
            StarNet.Logging.Logger.Instance.LogInfo("--------------------------------");
            Console.WriteLine("End");
        }
        
        private static void BackupOracleDb(
            string strUsername, 
            string strPassword, 
            string strDatabase, 
            string strDirectory,
            string strSchema, 
            string strVersion,
            string strMinutesToWait)
        {
            StarNet.Logging.Logger.Instance.Init("DBBackup");
            StarNet.Logging.Logger.Instance.LogInfo("Start");
            
            System.Diagnostics.Process shellRun = new Process();

            //create dump file name
            var strFileName = string.Format("{0}.{1}.{2}-{3}.{4}-{5}-{6}.dmp",
                strSchema,
                strVersion,
                DateTime.Today.Year,
                DateTime.Today.Month,
                DateTime.Today.Day,
                DateTime.Now.Hour,
                DateTime.Now.Minute);

            //create oracle dump command
            var strCommand = string.Format("{0}/{1}@{2} directory={3} dumpfile={4} schemas={5}",
                strUsername,
                strPassword,
                strDatabase,
                strDirectory,
                strFileName,
                strSchema);

            if (!string.IsNullOrWhiteSpace(strVersion))
            {
                strCommand += string.Format(" version={0}", strVersion);
            }

            shellRun.StartInfo.WorkingDirectory = System.IO.Directory.GetCurrentDirectory();
            shellRun.StartInfo.FileName = "expdp";
            shellRun.StartInfo.Arguments = strCommand;

            //no window
            shellRun.StartInfo.CreateNoWindow = true;

            //redirect output
            shellRun.StartInfo.UseShellExecute = false;
            shellRun.StartInfo.RedirectStandardOutput = true;
            shellRun.StartInfo.RedirectStandardError = true;

            //handler for DataReceivedEvent 
            shellRun.OutputDataReceived += OutputDataReceived;
            shellRun.ErrorDataReceived += ErrorDataReceived;
            //all data returned as error data from expdp command

            try
            {
                shellRun.Start();

                //read the output
                shellRun.BeginOutputReadLine();
                shellRun.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                if (shellRun != null)
                {
                    shellRun.Kill();
                }
                StarNet.Logging.Logger.Instance.LogError("execution failed ", ex);
                Console.WriteLine(ex.Message);
            }

            int intMinutesToWait = 0;
            int.TryParse(strMinutesToWait, out intMinutesToWait);
            
            if (intMinutesToWait == 0)
            {
                //wait indefinitely
                shellRun.WaitForExit();
            }
            else
            {
                if (!shellRun.WaitForExit(1000 * 60 * intMinutesToWait))
                {
                    //force closed before finish
                    StarNet.Logging.Logger.Instance.LogError("timed out", null);
                }
            }
            
            if (shellRun != null)
            {//clean
                shellRun.Close();
                shellRun.Dispose();
                shellRun = null;
            }
            
            StarNet.Logging.Logger.Instance.LogInfo("End");
        }

        private static void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                //log messages
                StarNet.Logging.Logger.Instance.Init("DBBackup");
                StarNet.Logging.Logger.Instance.LogInfo(e.Data);
            }
        }

        private static void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                //log messages
                StarNet.Logging.Logger.Instance.Init("DBBackup");
                StarNet.Logging.Logger.Instance.LogInfo(e.Data);
            }
        }

        //delete all files in the strDestinationDir that start with strSchema and ends with '.dmp' and older that strRetentionDays
        private static void CleanOldFiles(string strDestinationDir, string strRetentionDays, string strSchema)
        {
            StarNet.Logging.Logger.Instance.LogInfo("Start");

            int intRetentionDays = 0;
            if (!int.TryParse(strRetentionDays, out intRetentionDays))
            {
                Console.WriteLine("retention_days is not a number");
                StarNet.Logging.Logger.Instance.LogError("retention_days is not a number", null);
                return;
            }

            if (intRetentionDays <= 0)
            {
                Console.WriteLine("retention_days must be greater than 0");
                StarNet.Logging.Logger.Instance.LogError("retention_days must be greater than 0", null);
                return;
            }

            if (!System.IO.Directory.Exists(strDestinationDir))
            {
                Console.WriteLine("backup_dir not found");
                StarNet.Logging.Logger.Instance.LogError("backup_dir not found", null);
                return;
            }

            var strFilter = string.Format("{0}.*.dmp", strSchema);

            var arrFiles = System.IO.Directory.GetFiles(strDestinationDir, strFilter, System.IO.SearchOption.TopDirectoryOnly);

            foreach (var item in arrFiles)
            {
                var objFileInfo = new System.IO.FileInfo(item);

                if (objFileInfo.LastWriteTime < DateTime.Now.AddDays(-intRetentionDays))
                {
                    try
                    {
                        System.IO.File.Delete(item);
                        StarNet.Logging.Logger.Instance.LogInfo("file deleted: " + item);
                    }
                    catch(Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        StarNet.Logging.Logger.Instance.LogError("can't delete file: " + item, exc);
                    }
                }
            }
            StarNet.Logging.Logger.Instance.LogInfo("End");
        }
    }
}
