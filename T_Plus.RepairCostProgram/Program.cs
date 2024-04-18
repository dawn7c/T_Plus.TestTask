using T_Plus.RepairCostProgram.Models;

namespace T_Plus.RepairCostProgram
{
    public class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Usage: RepairCostProgram.exe <thermalNodeId> <logFilePath>");
                    return;
                }

                int thermalNodeId;
                if (!int.TryParse(args[0], out thermalNodeId))
                {
                    Console.WriteLine("Invalid thermalNodeId. Please provide a valid integer.");
                    return;
                }

                string logFilePath = args[1];

                try
                {
                    double initialCost = ThermalNodeRepairData.GenerateRandomCost();
                    double newCost = initialCost + 500.0; // Пример: добавляем 500.0 к исходному значению

                    await ThermalNodeRepairData.LogToFileAsync(logFilePath, $"[{DateTime.Now}] - Initial cost for Thermal Node {thermalNodeId}: {initialCost}");
                    await ThermalNodeRepairData.LogToFileAsync(logFilePath, $"[{DateTime.Now}] - New cost for Thermal Node {thermalNodeId}: {newCost}");

                    await ThermalNodeRepairData.UpdateCostInDatabaseAsync(thermalNodeId, newCost);

                    Console.WriteLine("Repair cost updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
           
        }
    }
}
