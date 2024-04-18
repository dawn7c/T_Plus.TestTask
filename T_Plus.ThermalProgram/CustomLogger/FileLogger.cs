using Serilog;

namespace T_Plus.ThermalProgram.CustomLogger
{
    public class FileLogger
    {
        private static int logId = 1;
        public static void LogToFile()
        {
            int currentLogId = logId++;

            // Настройка Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File($"log_{currentLogId}.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Логирование информации о запуске приложения
            Log.Information($"[{DateTime.Now}] - Запуск приложения. Номер лога: {currentLogId}");

            // Здесь ваш основной код приложения

            // Закрываем логгер
            Log.CloseAndFlush();
        }
    }
}
