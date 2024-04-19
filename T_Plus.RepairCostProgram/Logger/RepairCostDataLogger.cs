using Serilog;

namespace T_Plus.RepairCostProgram.Logger
{
    public class RepairCostDataLogger
    {
        private static int logId = 1;
        public void RepairCostDataLogToFile()
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
