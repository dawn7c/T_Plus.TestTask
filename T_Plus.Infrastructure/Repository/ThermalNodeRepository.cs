using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using T_Plus.Infrastructure.DatabaseContext;

namespace T_Plus.Infrastructure.Repository
{
    public class ThermalNodeRepository
    {
        private readonly ApplicationContext _context;

        public ThermalNodeRepository()
        {
        }

        public ThermalNodeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task UpdateCostAsync(Guid thermalNodeId, double newCost)
        {
            var node = await _context.ThermalNodes.FirstOrDefaultAsync(n => n.ThermalNodeId == thermalNodeId);

            if (node != null)
            {
                node.RepairCost = newCost;
                node.DateModified = DateTime.Now;

                await _context.SaveChangesAsync();

                Console.WriteLine($"Updated repair cost for Thermal Node {thermalNodeId} to {newCost} in the database.");
            }
            else
            {
                Console.WriteLine($"Thermal Node {thermalNodeId} not found in the database.");
            }
        }

        public void RunSubprogram(string nodeName, string logFilePath)
        {
            // Запись в лог файл
            string logMessage = $"[{DateTime.Now}] - Запуск программы для теплового узла {nodeName}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);

            // Путь к запускаемому приложению (замените на свой путь)
            string subprogramPath = @"D:\andrey loh (projects)\ItPlus.TestTask\ItPlus.RepairCostProgram\bin\Debug\net8.0\ItPlus.RepairCostProgram.exe";
            //СДЕЛАТЬ ОТНОСИТЕЛЬНЫЙ ПУТЬ ВЕЗ
            // Запуск подпрограммы
            Process.Start(subprogramPath, nodeName);
        }

        public string[] GetThermalNodes()
        {
            var nodeNames = _context.ThermalNodes.Select(node => node.ThermalNodeName).ToArray();
            return nodeNames;
        }
    }
}
