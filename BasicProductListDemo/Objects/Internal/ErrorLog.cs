using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ProductListMVCDemo.Objects.Internal
{
    /// <summary>
    /// Basic text error logger.
    /// </summary>
    public class ErrorLog
    {
        private const string LOG_FILENAME = "errorLog.txt";

        private static object LogLock;

        static ErrorLog()
        {
            // Init lock
            LogLock = new object();
        }

        public static void LogError(Exception e)
        {
            // Format the exception into an error string
            StringBuilder errorSb = new StringBuilder();

            errorSb.Append("****** EXCEPTION ******");
            errorSb.Append(Environment.NewLine);
            errorSb.Append("Message: ");
            errorSb.Append(Environment.NewLine);
            errorSb.Append(e.Message);
            errorSb.Append(Environment.NewLine);
            errorSb.Append("Stack Trace: ");
            errorSb.Append(Environment.NewLine);
            errorSb.Append(e.StackTrace);

            // Log the error string
            LogError(errorSb.ToString());
        }

        /// <summary>
        /// Logs errors to the main error log
        /// </summary>
        public static void LogError(string error)
        {
            StringBuilder logSb = new StringBuilder();

            logSb.Append("Error at ");
            logSb.Append(DateTime.Now.ToString());
            logSb.Append(Environment.NewLine);

            lock (LogLock)
            {
                using (StreamWriter writer = new StreamWriter(LOG_FILENAME))
                {
                    // Write date as header
                    writer.Write(logSb.ToString());

                    // Write main error message
                    writer.WriteLine(error);
                }
            }
        }
    }
}