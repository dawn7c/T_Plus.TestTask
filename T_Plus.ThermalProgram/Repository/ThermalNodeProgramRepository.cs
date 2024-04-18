using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using T_Plus.ThermalProgram.DatabaseContext;


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

            foreach (var node in thermalNodes)
            {
                RunSubprogram(node.ThermalNodeName);
                LogSubprogramStart(node.ThermalNodeName);
                await Task.Delay(TimeSpan.FromMinutes(1)); 
            }
        }

        private void RunSubprogram(string nodeName)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "D:\\andrey loh (projects)\\T_Plus.TestTask\\T_Plus.RepairCostProgram\\bin\\Debug\\net8.0\\T_Plus.RepairCostProgram.exe",
                    CreateNoWindow = true,
                    UseShellExecute = true
                };

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

        public string[] GetThermalNodes()
        {
            var nodeNames = _context.ThermalNodes.Select(node => node.ThermalNodeName).ToArray();
            return nodeNames;
        }
    }
}
