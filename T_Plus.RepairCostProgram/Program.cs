using Serilog;
using T_Plus.RepairCostProgram.Models;
using T_Plus.RepairCostProgram.Validation;
using T_Plus.ThermalProgram.DatabaseContext;

namespace T_Plus.RepairCostProgram
{
    public class Program
    {
        static async Task Main(string[] args)
        {
                var context = new ApplicationContext();
                var repairData = new ThermalNodeRepairData(context, Log.Logger);
                var repairVal = new RepairCostValidation();
                var validation = repairVal.CheckArgs(args);
                if (!validation.IsValid)
                {
                    Console.WriteLine(validation.Message);
                    return;
                }
                validation = repairVal.ValidateParse(args);
                if (!validation.IsValid)
                {
                    Console.WriteLine(validation.Message);
                    return;
                }

                Guid thermalNodeId = new Guid(args[0]);
                string logFilePath = args[1];

                try
                {
                    double initialCost = (double)await repairData.GetInitialCostAsync(thermalNodeId);
                    double newCost = ThermalNodeRepairData.GenerateRandomCost() + initialCost;


                    var thermalNode = new ThermalNodeRepairData(context, Log.Logger);
                    await thermalNode.LogToFileAsync(logFilePath);
                    await thermalNode.UpdatePropertiesAsync(thermalNodeId, newCost);
                    Console.WriteLine("Repair cost updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                Console.ReadLine();

        }
    }
}
