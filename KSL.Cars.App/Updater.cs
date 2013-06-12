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
        public static bool CheckForUpdates()
        {
            Version newVersion = null;

            string downloadUrl = "";

            try
            {
                string xml = new WebClient().DownloadString("http://www.voorheis.info/files/update.xml");

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                string version = doc.SelectSingleNode("//version").InnerText;

                downloadUrl = doc.SelectSingleNode("//url").InnerText;

                if (version != null) { newVersion = new Version(version); }

            }
            catch (System.Net.WebException ex)
            {
                EventLogger.LogEvent(ex.Message);
            }

            Version applicationVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            if (applicationVersion.CompareTo(newVersion) < 0)
            {
                var result = MessageBox.Show("Version " + newVersion.Major + "." + newVersion.Minor + "." + newVersion.Build + "." + newVersion.Revision + " is available for download. Download?", "Update Available!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes && downloadUrl != null) { System.Diagnostics.Process.Start(downloadUrl); }

                return true;
            }

            return false;
        }

        public static bool RedHerring()
        {
            return false;
        }
    }
}
