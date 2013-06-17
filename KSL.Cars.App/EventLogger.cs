using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace KSL.Cars.App
{
    static class EventLogger
    {
        static EventLog appLog = new EventLog();

        /// <summary>
        /// Used to log an exception message to the event log.
        /// </summary>
        /// <param name="ex"></param>
        public static void LogEvent(Exception ex, EventLogEntryType level = EventLogEntryType.Warning, bool ShowPopup = false)
        {
            writeLogEntry(ex.Message, level);
            if (ShowPopup) MessageBox.Show(ex.Message);
        }

        /// <summary>
        /// Used to log a string to the application event log.
        /// </summary>
        /// <param name="message"></param>
        public static void LogEvent(string message, EventLogEntryType level = EventLogEntryType.Warning, bool ShowPopup = false)
        {
            writeLogEntry(message, level);
            if (ShowPopup) MessageBox.Show(message);
        }

        /// <summary>
        /// Logs the default message about not having a settings file available for command line use.
        /// </summary>
        public static void LogEvent() { writeLogEntry(); }

        /// <summary>
        /// Actually does the logging.
        /// </summary>
        /// <param name="logMessage"></param>
        private static void writeLogEntry(string logMessage = "Please run this program with the GUI to create settings file first", EventLogEntryType level = EventLogEntryType.Warning)
        {
            appLog.Log = "Application";
            appLog.Source = "KSL.Cars.App";
            try
            {
                appLog.WriteEntry(logMessage, level);
            }
            catch (System.Security.SecurityException ex)
            {
                TimedMessageBox.Show("Please run this application as administrator once to allow logging to the event log:\n\n" + ex.Message, "Run as Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning, 5000);
            }
        }
    }
}
