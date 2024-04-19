using Serilog;
using Serilog.Core;
using T_Plus.RepairCostProgram.Models;
using T_Plus.RepairCostProgram.Validation;
using T_Plus.ThermalProgram.DatabaseContext;
using T_Plus.ThermalProgram.Models;
using T_Plus.ThermalProgram.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace T_Plus.RepairCostProgram
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                var context = new ApplicationContext();
                var logger = new LoggerConfiguration();
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
                    double initialCost = ThermalNodeRepairData.GenerateRandomCost();
                    double newCost = initialCost + 500.0;

                    var thermalNode = new ThermalNodeRepairData(context, (ILogger)logger);
                    await thermalNode.LogToFileAsync(logFilePath);
                    await thermalNode.UpdatePropertiesAsync(thermalNodeId, newCost);


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
