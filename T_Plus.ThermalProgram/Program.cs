using Serilog;
using T_Plus.ThermalProgram.CustomLogger;
using T_Plus.ThermalProgram.DatabaseContext;

namespace T_Plus.ThermalProgram
{
    public class Program
    {
         private static int logId = 1;
        static async Task Main(string[] args)
        {
            FileLogger.LogToFile();

            var context = new ApplicationContext();
            
            var manager = new Repository.ThermalNodeProgramRepository(context, Log.Logger);

            await manager.RunAllSubprogramsAsync();
        }
    }
}
