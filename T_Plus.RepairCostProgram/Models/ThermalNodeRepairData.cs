namespace T_Plus.RepairCostProgram.Models
{
    public class ThermalNodeRepairData
    {
        

        public static double GenerateRandomCost()
        {
            Random random = new Random();
            return random.NextDouble() * (20000.0 - 5000.0) + 5000.0; // неправильно, должнол
        }

        public static async Task LogToFileAsync(string filePath, string message)
        {
            using (StreamWriter writer = File.AppendText(filePath))
            {
                await writer.WriteLineAsync(message);
            }
        }

        public static async Task UpdateCostInDatabaseAsync(int thermalNodeId, double newCost)
        {
            // Здесь может быть ваш код для записи нового значения затрат в БД
            // В данном примере просто выводится сообщение
            await Task.Delay(100); // Имитация задержки записи в БД
            Console.WriteLine($"Updated repair cost for Thermal Node {thermalNodeId} to {newCost} in the database.");
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
