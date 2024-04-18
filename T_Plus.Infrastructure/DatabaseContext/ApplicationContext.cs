using Microsoft.EntityFrameworkCore;
using T_Plus.ThermalProgram.Models;


namespace T_Plus.Infrastructure.DatabaseContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ThermalNodeProgram> ThermalNodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=1337;Database=ItPlus.TestTask");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThermalNodeProgram>().HasNoKey();
        }
    }
}
