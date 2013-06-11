using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

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
            
            if (args.Contains("/lastsearch"))
            {
                if (System.IO.File.Exists("KSL.Cars.App.settings"))
                {
                    frmMain myProgram = new frmMain();
                    myProgram.LoadData(true);
                    string url = myProgram.buildURL(true);
                    myProgram.parsePage(url);
                    myProgram.SaveData(true);
                }
                else EventLogger.LogEvent();
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
