using T_Plus.ThermalProgram.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using T_Plus.ThermalProgram.Models;
using T_Plus.ThermalProgram.CustomLoggerService;

namespace T_Plus.RepairCostProgram.Models
{
    public class ThermalNodeRepairData
    {
        private readonly ApplicationContext _context;
        private readonly LoggerService _logger;
        private readonly DbSet<ThermalNodeProgram> _dbSet;

        public ThermalNodeRepairData(ApplicationContext context, string filepath)
        {
            _context = context;
            _logger = new LoggerService(filepath);
            _dbSet = _context.Set<ThermalNodeProgram>();
        }

        public  double GenerateRandomCost()
        {
            Random random = new Random();
            return random.NextDouble() * (20000.0 - 5000.0) + 5000.0;
        }

        public async Task LogToConsoleAsync(string message)
        {
            Console.WriteLine(message);
        }

        public async Task UpdatePropertiesAsync(Guid thermalNodeId, double newCost)
        {
            var node = await _context.ThermalNodes.FirstOrDefaultAsync(n => n.ThermalNodeId == thermalNodeId);
            
            if (node != null)
            {
                
                _logger.Information($"[{DateTime.Now}] - Приложение запущено. Исходное значение: {node.RepairCost} | Новое значение: {newCost}");
                node.RepairCost = newCost;
                node.DateModified = DateTime.Now.ToUniversalTime();
                _dbSet.Update(node);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.Warning($"Thermal Node {thermalNodeId} not found in the database.");
            }
        }

        public async Task<double> GetInitialCostAsync(Guid thermalNodeId)
        {
            var thermalNode = await _context.ThermalNodes.FirstOrDefaultAsync(t => t.ThermalNodeId == thermalNodeId);
            return thermalNode.RepairCost;
        }
    }
}
