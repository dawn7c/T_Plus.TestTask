using Serilog;
using T_Plus.RepairCostProgram.Models;
using T_Plus.ThermalProgram.DatabaseContext;
using T_Plus.ThermalProgram.Models;
using T_Plus.ThermalProgram.Repository;

namespace T_Plus.RepairCostProgram
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var context = new ApplicationContext();
            
            
                var t = new ThermalNodeProgram();
                if (args.Length != 2)
                {
                    Console.WriteLine("Usage: RepairCostProgram.exe <thermalNodeId> <logFilePath>");
                    return;
                }

                Guid thermalNodeId = t.ThermalNodeId;
                if (!Guid.TryParse(args[0], out thermalNodeId))
                {
                    Console.WriteLine("Invalid thermalNodeId. Please provide a valid integer.");
                    return;
                }

                string logFilePath = args[1];

                try
                {
                    double initialCost = ThermalNodeRepairData.GenerateRandomCost();
                    double newCost = initialCost + 500.0;

                    
                    var thermalNode = new ThermalNodeRepairData(context);
                    await thermalNode.LogToFileAsync(logFilePath);
                    await thermalNode.LogToFileAsync(logFilePath);

                    await thermalNode.UpdateCostAsync(thermalNodeId, newCost);

                    Console.WriteLine("Repair cost updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            
           
        }
    }
}
