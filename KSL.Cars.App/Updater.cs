using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.Net;
using System.Windows.Forms;

namespace KSL.Cars.App
{
    static class Updater
    {
        /// <summary>
        /// Downloads the XML current version file, 
        /// and compares it to the version for this 
        /// running program.  If there is a newer 
        /// version available, propmpts for download.
        /// </summary>
        public static void GetUpdate(bool autoDownload = false)
        {
            Version newVersion = null;

            XmlDocument updateFile = getUpdateFile();

            if (updateFile.ChildNodes.Count > 0)
            {
                string version = updateFile.SelectSingleNode("//version").InnerText;

                string downloadUrl = updateFile.SelectSingleNode("//url").InnerText;

                if (version != null)
                {
                    newVersion = new Version(version);
                    Version applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;

                    if (applicationVersion.CompareTo(newVersion) < 0)
                    {
                        if (downloadUrl != null)
                        {
                            if (autoDownload) System.Diagnostics.Process.Start(downloadUrl);
                            else
                            {
                                var result = MessageBox.Show("Version " + newVersion.Major + "." + newVersion.Minor + "." + newVersion.Build + "." + newVersion.Revision + " is available for download. Download?", "Update Available!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                                if (result == DialogResult.Yes) { System.Diagnostics.Process.Start(downloadUrl); }
                            }
                        }
                        //return true;
                    }
                }
            }

            //return false;
        }

        public static XmlDocument getUpdateFile()
        {
            XmlDocument updateFile = new XmlDocument();

            try
            {
                string xml = new WebClient().DownloadString(Properties.Settings.Default.UpdateFileURL);

                updateFile.LoadXml(xml);
            }
            catch (System.Net.WebException ex)
            {
                EventLogger.LogEvent(ex.Message);
            }

            return updateFile;
        }

        public static bool RedHerring()
        {
            return false;
        }
    }
}
