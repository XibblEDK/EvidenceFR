using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceFR.Utils
{
    public class Logging
    {
        internal enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error
        }

        internal static void Log(LogLevel logLevel, string logMessage)
        {
            string logLevelImportance = string.Empty;
            switch (logLevel)
            {
                case LogLevel.Debug:
                    logLevelImportance = "[DEBUG]: ";
                    break;
                case LogLevel.Info:
                    logLevelImportance = "[INFO]: ";
                    break;
                case LogLevel.Warning:
                    logLevelImportance = "[WARNING]: ";
                    break;
                case LogLevel.Error:
                    logLevelImportance = "[ERROR]: ";
                    break;
            }

            Game.LogTrivial(logLevelImportance + logMessage);
        }

    }
}
