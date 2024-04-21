﻿using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;
using System.Xml.Linq;
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
            _logger = new LoggerConfiguration()
                .WriteTo.File("log.txt") 
                .CreateLogger(); 
        }
        

        public async Task RunAllSubprogramsAsync()
        {
            var nodeNames = await GetNamesThermalNodesAsync();
            foreach (var nodeName in nodeNames)
            {
                _logger.Information($"[{DateTime.Now}] - Запуск приложения. Имя теплового узла:{nodeName}");
                var guid = await GetThermalNodeIdByNameAsync(nodeName);
                await RunSubprogram(nodeName, guid);
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        private async Task RunSubprogram(string nodeName, Guid guid) 
        {
            string logFilePath = Path.GetFullPath(@"D:\andrey loh (projects)\T_Plus.TestTask\T_Plus.RepairCostProgram\bin\Debug\net8.0\log_2.txt");
            try
            {
                 ProcessStartInfo startInfo = new ProcessStartInfo
                 {
                    FileName = "D:\\andrey loh (projects)\\T_Plus.TestTask\\T_Plus.RepairCostProgram\\bin\\Debug\\net8.0\\T_Plus.RepairCostProgram.exe",
                    Arguments = $"{guid}   \"{logFilePath} \"", 
                    CreateNoWindow = false,
                    UseShellExecute = false
                 };

                Process process = Process.Start(startInfo);
                if (process != null)
                {
                    int processId = process.Id;
                    await Task.Delay(TimeSpan.FromSeconds(10));
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while running subprogram: {ex.Message}");
            }
        }
        
        public async Task<List<string>> GetNamesThermalNodesAsync()
        {
            List<string> nodeNames = await _context.ThermalNodes.Select(node => node.ThermalNodeName).ToListAsync();
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
