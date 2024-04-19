using T_Plus.ThermalProgram.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Serilog;
using T_Plus.RepairCostProgram.Logger;

namespace T_Plus.RepairCostProgram.Models
{
    public class ThermalNodeRepairData
    {
        private readonly ApplicationContext _context;
        private readonly RepairCostDataLogger _logger;


        public ThermalNodeRepairData(ApplicationContext context, RepairCostDataLogger logger)
        {
            _context = context;
            _logger = logger;
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

    }
}
