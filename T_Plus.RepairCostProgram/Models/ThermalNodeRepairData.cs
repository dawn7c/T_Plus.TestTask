using T_Plus.ThermalProgram.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace T_Plus.RepairCostProgram.Models
{
    public class ThermalNodeRepairData
    {
        private readonly ApplicationContext _context;
        //private readonly Serilog.ILogger _logger;
        private static ILogger logger = new LoggerConfiguration()
        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day) // Указываете имя файла и интервал ротации (если нужно)
        .CreateLogger();

        public ThermalNodeRepairData(ApplicationContext context)
        {
            _context = context;
        }

        public static double GenerateRandomCost()
        {
            Random random = new Random();
            return random.NextDouble() * (20000.0 - 5000.0) + 5000.0; // неправильно, должнол
        }

        public async Task LogToFileAsync(string message)
        {
            logger.Information(message);
        }

        //public static async Task UpdateCostInDatabaseAsync(int thermalNodeId, double newCost)
        //{
        //    // Здесь может быть ваш код для записи нового значения затрат в БД
        //    // В данном примере просто выводится сообщение
        //    await Task.Delay(100); // Имитация задержки записи в БД
        //    Console.WriteLine($"Updated repair cost for Thermal Node {thermalNodeId} to {newCost} in the database.");
        //}

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
