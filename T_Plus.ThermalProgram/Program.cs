using T_Plus.ThermalProgram.DatabaseContext;

namespace T_Plus.ThermalProgram
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Ожидается запуск подпрограмм");
            var context = new ApplicationContext();
            var manager = new Repository.ThermalNodeProgramRepository(context);
            manager.GetNamesThermalNodesAsync().Wait();
            await manager.RunAllSubprogramsAsync();
        }
    }
}
