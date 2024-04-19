﻿using Serilog;

namespace T_Plus.ThermalProgram.CustomLogger
{
    public class FileLogger
    {
        private static int logId = 1;
        public static void LogToFile()
        {
            int currentLogId = logId++;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File($"log_{currentLogId}.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information($"[{DateTime.Now}] - Запуск приложения. Номер лога: {currentLogId}");
            Log.CloseAndFlush();
        }
    }
}
