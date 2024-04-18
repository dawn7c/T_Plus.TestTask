using Serilog;
using T_Plus.ThermalProgram.DatabaseContext;

namespace T_Plus.ThermalProgram
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("path_to_log_file.txt")
            .CreateLogger();

            var context = new ApplicationContext();

            
            var manager = new Repository.ThermalNodeProgramRepository(context, Log.Logger);

            await manager.RunAllSubprogramsAsync();
        }
    }
}
