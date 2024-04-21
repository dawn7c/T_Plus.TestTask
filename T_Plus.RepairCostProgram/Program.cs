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
            var repairVal = new RepairCostValidation();
            var thermalUpdate = new ThermalUpdateCost();
            foreach (var item in args)
            {
                await Console.Out.WriteLineAsync(item);
            }
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

            await thermalUpdate.Update(new Guid(args[0]), args[1]);
            Console.ReadLine();
        }
    }
}
