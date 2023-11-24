
#region Error_Logger

namespace HealthAndFitnessTracker.Classes
{
    public static class Logger
    {
        private static readonly string LogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "log.txt");

        public static void LogError(string message)
        {
            try
            {
                using StreamWriter writer = new(LogFilePath, true);
                
                    string logEntry = $"{DateTime.Now}: {message}";
                    writer.WriteLine(logEntry);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }

        public static void LogInfo(string infoMessage)
        {
            Log($"[Info] {DateTime.Now}: {infoMessage}");
        }

        private static void Log(string logMessage)
        {
            try
            {
                File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while writing to log file: {ex.Message}");
            }
        }

        public static void LogWarning(string warningMessage)
        {
            Log($"[Warning] {DateTime.Now}: {warningMessage}");
        }

        
    }
}
#endregion