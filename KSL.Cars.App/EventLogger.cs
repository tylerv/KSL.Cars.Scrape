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

        public enum DestinationType
        {
            MessageBox,
            EventLog
        }

        /// <summary>
        /// Used to log an exception message to the event log or popup box.
        /// </summary>
        /// <param name="ex"></param>
        public static void LogEvent(Exception ex, EventLogEntryType level = EventLogEntryType.Warning, DestinationType destination = DestinationType.EventLog)
        {
            writeLogEntry(ex.Message, level, destination);
        }

        /// <summary>
        /// Used to log a string to the application event log or popup box.
        /// </summary>
        /// <param name="message"></param>
        public static void LogEvent(string message, EventLogEntryType level = EventLogEntryType.Warning, DestinationType destination = DestinationType.EventLog)
        {
            writeLogEntry(message, level, destination);
        }

        /// <summary>
        /// Logs the default message about not having a settings file available for command line use.
        /// </summary>
        public static void LogEvent() { writeLogEntry(); }

        /// <summary>
        /// Actually does the logging.
        /// </summary>
        /// <param name="logMessage"></param>
        private static void writeLogEntry(string logMessage = "Please run this program with the GUI to create settings file first", EventLogEntryType level = EventLogEntryType.Warning, DestinationType destination = DestinationType.EventLog)
        {
            appLog.Log = "Application";
            appLog.Source = "KSL.Cars.App";
            try
            {
                switch (destination)
                {
                    case DestinationType.EventLog:
                        appLog.WriteEntry(logMessage, level);
                        break;
                    case DestinationType.MessageBox:

                        MessageBoxIcon temp;

                        switch (level)
                        {
                            case EventLogEntryType.Error:
                                temp = MessageBoxIcon.Error;
                                break;
                            case EventLogEntryType.Information:
                                temp = MessageBoxIcon.Information;
                                break;
                            //We don't really expect to have Failure and Success Audit event types,
                            //but they are listed here for completeness.
                            case EventLogEntryType.FailureAudit:
                            case EventLogEntryType.SuccessAudit:
                            case EventLogEntryType.Warning:
                            default:
                                temp = MessageBoxIcon.Warning;
                                break;
                        }

                        MessageBox.Show(logMessage, "", MessageBoxButtons.OK, temp);
                        break;
                }
            }
            catch (System.Security.SecurityException ex)
            {
                TimedMessageBox.Show("Please run this application as administrator once to allow logging to the event log:\n\n" + ex.Message, "Run as Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning, 5000);
            }
        }
    }
}
