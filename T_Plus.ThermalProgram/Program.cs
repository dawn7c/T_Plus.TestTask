using Serilog;
using T_Plus.ThermalProgram.DatabaseContext;

namespace T_Plus.ThermalProgram
{
    public class Program
    {
         
        static async Task Main(string[] args)
        {
            var context = new ApplicationContext();

            Console.WriteLine("Ожидается запуск подпрограмм");
            var manager = new Repository.ThermalNodeProgramRepository(context, Log.Logger);
            manager.GetNamesThermalNodesAsync().Wait();
            await manager.RunAllSubprogramsAsync();
            
        }
    }
}
