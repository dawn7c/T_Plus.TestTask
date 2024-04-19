using Serilog;
using Serilog.Core;
using T_Plus.RepairCostProgram.Logger;
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
            var context = new ApplicationContext();
            var logger = new RepairCostDataLogger();
            logger.RepairCostDataLogToFile();
            var termalNodeProgram = new ThermalNodeProgram();
            var repairVal = new RepairCostValidation();
            var validation = repairVal.CheckArgs(args);
            if(!validation.IsValid) 
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
            Guid thermalNodeId = termalNodeProgram.ThermalNodeId;
            string logFilePath = args[1];

                try
                {
                    double initialCost = ThermalNodeRepairData.GenerateRandomCost();
                    double newCost = initialCost + 500.0;

                    
                    var thermalNode = new ThermalNodeRepairData(context, logger);
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
