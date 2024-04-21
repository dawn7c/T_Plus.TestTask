using Serilog;
using T_Plus.ThermalProgram.DatabaseContext;

namespace T_Plus.RepairCostProgram.Models
{
    public  class ThermalUpdateCost
    {
        public async Task Update(Guid thermalNodeId, string filepath)
        {
            try
            {
                var context = new ApplicationContext();
                var repairData = new ThermalNodeRepairData(context, filepath);
                double initialCost = await repairData.GetInitialCostAsync(thermalNodeId);
                double newCost = repairData.GenerateRandomCost() + initialCost;
                await repairData.UpdatePropertiesAsync(thermalNodeId, newCost);
                Console.WriteLine("Repair cost updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
    }
}
