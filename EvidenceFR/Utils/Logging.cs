using Rage;

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

        private static string pluginPrefix = "EvidenceFR: ";

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

            Game.LogTrivial(pluginPrefix + logLevelImportance + logMessage);
        }

    }
}
