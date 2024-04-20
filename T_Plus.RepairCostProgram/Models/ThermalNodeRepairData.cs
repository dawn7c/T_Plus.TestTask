using T_Plus.ThermalProgram.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Serilog;
using T_Plus.ThermalProgram.Models;

namespace T_Plus.RepairCostProgram.Models
{
    public class ThermalNodeRepairData
    {
        private readonly ApplicationContext _context;
        private readonly ILogger _logger;
        private readonly DbSet<ThermalNodeProgram> _dbSet;


        public ThermalNodeRepairData(ApplicationContext context, Serilog.ILogger logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = _context.Set<ThermalNodeProgram>();
        }

        public static double GenerateRandomCost()
        {
            Random random = new Random();
            return random.NextDouble() * (20000.0 - 5000.0) + 5000.0;
        }

        public async Task LogToFileAsync(string message)
        {
            Console.WriteLine(message);
        }

        public async Task UpdatePropertiesAsync(Guid thermalNodeId, double newCost)
        {
            var node = await _context.ThermalNodes.FirstOrDefaultAsync(n => n.ThermalNodeId == thermalNodeId);

            if (node != null)
            {
                Log.Logger = new LoggerConfiguration()
                .WriteTo.File($"log_2.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
                Console.WriteLine($"Updated repair cost for Thermal Node {thermalNodeId} to {newCost} in the database.");
                Log.Information($"[{DateTime.Now}] - Приложение запущено. Исходное значение: {node.RepairCost}, Новое значение: {newCost}");
                Log.CloseAndFlush();
                node.RepairCost = newCost;
                node.DateModified = DateTime.Now.ToUniversalTime();
                _dbSet.Update(node);
                await _context.SaveChangesAsync();

            }
            else
            {
                Console.WriteLine($"Thermal Node {thermalNodeId} not found in the database.");
            }
        }

        public async Task<double?> GetInitialCostAsync(Guid thermalNodeId)
        {
            var thermalNode = await _context.ThermalNodes.FirstOrDefaultAsync(t => t.ThermalNodeId == thermalNodeId);
            if (thermalNode != null)
            {
                return thermalNode.RepairCost;
            }
            else
            {
                return null; 
            }
        }
    }
}
