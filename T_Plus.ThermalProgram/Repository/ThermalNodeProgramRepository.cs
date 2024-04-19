using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;
using T_Plus.ThermalProgram.DatabaseContext;
using T_Plus.ThermalProgram.Models;


namespace T_Plus.ThermalProgram.Repository
{
    public class ThermalNodeProgramRepository
    {
        private readonly ApplicationContext _context;
        private readonly Serilog.ILogger _logger;

        public ThermalNodeProgramRepository(ApplicationContext context, Serilog.ILogger logger)
        {
            _context = context;
            _logger = logger;
            
        }

        public async Task RunAllSubprogramsAsync()
        {
            var thermalNodes = await _context.ThermalNodes.ToListAsync();
            var nodeNames = await GetNamesThermalNodesAsync();
            foreach (var nodeName in nodeNames)
            {
                var guid = await GetThermalNodeIdByNameAsync(nodeName);
                RunSubprogram(nodeName, guid);
                LogSubprogramStart(nodeName);
                //await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

        private void RunSubprogram(string nodeName, Guid guid) 
        {
            string logFilePath = Path.GetFullPath(@"T_Plus.TestTask\T_Plus.ThermalProgram\bin\Debug\net8.0\log_12320240419.exe");
            try
            {
                 ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "D:\\andrey loh (projects)\\T_Plus.TestTask\\T_Plus.RepairCostProgram\\bin\\Debug\\net8.0\\T_Plus.RepairCostProgram.exe",
                    Arguments = guid.ToString(),
                    CreateNoWindow = false,
                    UseShellExecute = false
                } ;
                 
                Process.Start(startInfo);

                string logMessage = $"Запуск программы для теплового узла {nodeName}";
                _logger.Information(logMessage);
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while running subprogram: {ex.Message}");
            }
        }
        
        private void LogSubprogramStart(string nodeName)
        {
            string logMessage = $"[{DateTime.Now}] - Запуск программы для теплового узла {nodeName}";
            _logger.Information(logMessage);
        }

        public async Task<List<string>> GetNamesThermalNodesAsync()
        {
            List<string> nodeNames = await _context.ThermalNodes.Select(node => node.ThermalNodeName).ToListAsync();
            foreach (var node in nodeNames)
            {
                Log.Logger = new LoggerConfiguration()
               .WriteTo.File($"log_123.txt", rollingInterval: RollingInterval.Day)
               .CreateLogger();
                Log.Information($"[{DateTime.Now}] - Запуск приложения. Имя теплового узла:{node}");
                Log.CloseAndFlush();
            }
            return nodeNames;
        }

        private async Task<Guid> GetThermalNodeIdByNameAsync(string nodeName)
        {
            return await _context.ThermalNodes
                                   .Where(node => node.ThermalNodeName == nodeName)
                                   .Select(node => node.ThermalNodeId)
                                   .FirstOrDefaultAsync();
        }
    }
}
