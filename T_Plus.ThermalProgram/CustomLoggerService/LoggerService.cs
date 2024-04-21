using Serilog;

namespace T_Plus.ThermalProgram.CustomLoggerService
{
    public class LoggerService
    {
        private ILogger _logger;
        
        public LoggerService(string logFilePath)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFilePath);

            if (!string.IsNullOrWhiteSpace(logFilePath))
            {
                loggerConfiguration.WriteTo.File(logFilePath);
            }

            _logger = loggerConfiguration.CreateLogger();
        }

        public void Information(string message)
        {
            _logger.Information(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }
    }
}
