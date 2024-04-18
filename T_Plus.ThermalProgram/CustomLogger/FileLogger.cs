namespace T_Plus.ThermalProgram.CustomLogger
{
    public class FileLogger
    {
        public static void LogToFile(string logFilePath, string message)
        {
            try
            {
                bool fileExists = File.Exists(logFilePath);
                using (StreamWriter writer = fileExists ? File.AppendText(logFilePath) : File.CreateText(logFilePath))
                {
                    writer.WriteLine($"[{DateTime.Now}] - {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to log file: {ex.Message}");
            }
        }
    }
}
