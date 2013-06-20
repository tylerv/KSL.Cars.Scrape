using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSL.Cars.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                frmMain myProgram = null;

                foreach (string arg in args)
                {
                    switch (arg.Replace("/", "").Replace("-", "").ToLower())
                    {
                        case "lastsearch":
                            if (System.IO.File.Exists(Properties.Settings.Default.SettingsFileName))
                            {
                                myProgram = new frmMain();
                                myProgram.LoadData(true);
                                string url = myProgram.buildURL();
                                if (url.Length > 0)
                                {
                                    myProgram.parsePage(url);
                                    myProgram.SaveData(true);
                                }
                                else EventLogger.LogEvent("Invalid URL! Do you have proper search parameters in the settings file?", System.Diagnostics.EventLogEntryType.Error);
                            }
                            else EventLogger.LogEvent();
                            break;
                        case "update":
                            //Check for a /y or /yes to suppress the yes/no prompt on downloading the update.
                            if (args.Contains("yes") || args.Contains("y")) Updater.GetUpdate(true);
                            
                            //otherwise, just check for updates and ask to download.
                            else Updater.GetUpdate();
                            break;
                        case "email":
                            if (myProgram != null)
                            {
                                myProgram.emailResults();
                            }
                            else EventLogger.LogEvent("Please make sure the 'lastsearch' parameter preceeds the 'email' parameter.");
                            break;
                    }
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
        }
    }
}
